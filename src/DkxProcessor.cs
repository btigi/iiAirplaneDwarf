using System.Text;
using ii.AirplaneDwarf.Model;

namespace ii.AirplaneDwarf
{
    public class DkxProcessor
    {
        public const int HeaderSize = 0x40;
        public const int PageSize = 0x800;
        public const int PageHeaderSize = 20;
        public const int AttributeSize = 17;

        public List<DkxEntry> Read(string dkxPath, string dkdPath)
        {
            var index = File.ReadAllBytes(dkxPath);
            var data = File.ReadAllBytes(dkdPath);
            return ReadArchive(index, data);
        }

        private List<DkxEntry> ReadArchive(byte[] index, byte[] data)
        {
            if (index.Length < HeaderSize)
            {
                throw new InvalidDataException("DKX too small");
            }

            if (Encoding.ASCII.GetString(index, 0, 4) != "DKBT")
            {
                throw new InvalidDataException("DKX missing DKBT signature");
            }

            if (data.Length < 4 || Encoding.ASCII.GetString(data, 0, 4) != "DKFM")
            {
                throw new InvalidDataException("DKD missing DKFM signature");
            }

            var entries = new List<DkxEntry>();
            var visited = new HashSet<uint>();
            var root = BitConverter.ToUInt32(index, 0x24);
            WalkPage(index, root, entries, visited);

            foreach (var e in entries)
            {
                if (e.Offset < 0 || e.Length < 0 || (long)e.Offset + e.Length > data.Length)
                {
                    e.Data = [];
                    continue;
                }

                var raw = new byte[e.Length];
                Buffer.BlockCopy(data, e.Offset, raw, 0, e.Length);
                e.Data = e.Type switch
                {
                    DkxType.Bmp => WrapBmp(raw),
                    DkxType.Wav => WrapWav(raw),
                    _ => raw
                };
            }

            return Deduplicate(entries);
        }

        private static byte[] WrapWav(byte[] record)
        {
            if (record.Length < 26)
            {
                return record;
            }

            var formatTag = BitConverter.ToUInt16(record, 0);
            var channels = BitConverter.ToUInt16(record, 2);
            var sampleRate = BitConverter.ToUInt32(record, 4);
            var byteRate = BitConverter.ToUInt32(record, 8);
            var blockAlign = BitConverter.ToUInt16(record, 12);
            var bitsPerSample = BitConverter.ToUInt16(record, 14);
            var cbSize = BitConverter.ToUInt16(record, 16);
            var dataSize = BitConverter.ToUInt32(record, 18);

            var headerSize = 18 + 4 + 4;
            var extraAt = -1;
            if (formatTag == 2 && cbSize > 0)
            {
                headerSize += 4 + cbSize;
                extraAt = 18 + 4 + 4 + 4;
            }

            if (headerSize > record.Length)
            {
                headerSize = 26;
            }

            if (dataSize > (uint)(record.Length - headerSize))
            {
                dataSize = (uint)(record.Length - headerSize);
            }

            var fmtChunkSize = 16 + (cbSize > 0 ? 2 + cbSize : 0);
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);

            bw.Write(Encoding.ASCII.GetBytes("RIFF"));
            bw.Write(4 + (8 + fmtChunkSize) + (8 + (int)dataSize));
            bw.Write(Encoding.ASCII.GetBytes("WAVE"));

            bw.Write(Encoding.ASCII.GetBytes("fmt "));
            bw.Write(fmtChunkSize);
            bw.Write(formatTag);
            bw.Write(channels);
            bw.Write(sampleRate);
            bw.Write(byteRate);
            bw.Write(blockAlign);
            bw.Write(bitsPerSample);
            if (cbSize > 0)
            {
                bw.Write(cbSize);
                if (extraAt >= 0 && extraAt + cbSize <= record.Length)
                {
                    bw.Write(record, extraAt, cbSize);
                }
                else
                {
                    bw.Write(new byte[cbSize]);
                }
            }

            bw.Write(Encoding.ASCII.GetBytes("data"));
            bw.Write(dataSize);
            bw.Write(record, headerSize, (int)dataSize);
            return ms.ToArray();
        }

        private static byte[] WrapBmp(byte[] record)
        {
            if (record.Length < 40)
            {
                return record;
            }

            var biSize = BitConverter.ToInt32(record, 0);
            if (biSize != 40)
            {
                return record;
            }

            var bpp = BitConverter.ToInt16(record, 14);
            var colorsUsed = BitConverter.ToInt32(record, 32);
            if (colorsUsed == 0 && bpp <= 8)
            {
                colorsUsed = 1 << bpp;
            }

            var paletteBytes = bpp <= 8 ? colorsUsed * 4 : 0;
            var pixelOffset = 14 + biSize + paletteBytes;

            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);
            bw.Write(Encoding.ASCII.GetBytes("BM"));
            bw.Write(14 + record.Length);
            bw.Write(0);
            bw.Write(pixelOffset);
            bw.Write(record);
            return ms.ToArray();
        }

        private static void WalkPage(byte[] index, uint page, List<DkxEntry> entries, HashSet<uint> visited)
        {
            if (!IsPage(index, page) || !visited.Add(page))
            {
                return;
            }

            var used = BitConverter.ToUInt16(index, (int)page + 8);
            var next = BitConverter.ToUInt32(index, (int)page + 4);
            var left = BitConverter.ToUInt32(index, (int)page + 16);
            var keys = ParseEntries(index, (int)page, used);

            var isInternal = IsPage(index, left) || keys.Count(k => IsPage(index, k.ChildPage)) >= Math.Max(1, keys.Count / 2);

            if (isInternal)
            {
                WalkPage(index, left, entries, visited);
                foreach (var key in keys)
                {
                    WalkPage(index, key.ChildPage, entries, visited);
                }

                if (IsPage(index, next))
                {
                    WalkPage(index, next, entries, visited);
                }
            }
            else
            {
                foreach (var key in keys)
                {
                    entries.Add(ToEntry(key));
                }
            }
        }

        private static DkxEntry ToEntry(RawEntry key) => new()
        {
            Offset = key.Offset,
            Type = (DkxType)key.Type,
            Length = key.Length,
            Length2 = key.Length2,
            Flags = key.Flags,
            Width = key.Width,
            Height = key.Height,
            Attributes = key.Attributes,
            Filename = key.Name,
            ChildPage = key.ChildPage
        };

        private static List<RawEntry> ParseEntries(byte[] index, int pageOff, int used)
        {
            var list = new List<RawEntry>();
            var pos = pageOff + PageHeaderSize;
            var end = Math.Min(pageOff + PageHeaderSize + used, index.Length);

            while (pos + 4 + 1 + 4 + AttributeSize + 2 + 4 <= end)
            {
                var offset = BitConverter.ToInt32(index, pos);
                pos += 4;
                var type = index[pos++];
                var length = BitConverter.ToInt32(index, pos);
                pos += 4;

                var attr = new byte[AttributeSize];
                Buffer.BlockCopy(index, pos, attr, 0, AttributeSize);
                pos += AttributeSize;

                var nameLen = BitConverter.ToInt16(index, pos);
                pos += 2;
                if (nameLen <= 0 || nameLen > 128 || pos + nameLen + 4 > index.Length)
                {
                    break;
                }

                var name = Encoding.ASCII.GetString(index, pos, nameLen);
                pos += nameLen;
                if (!IsPrintableName(name))
                {
                    break;
                }

                var child = BitConverter.ToUInt32(index, pos);
                pos += 4;

                list.Add(new RawEntry
                {
                    Offset = offset,
                    Type = type,
                    Length = length,
                    Length2 = BitConverter.ToInt32(attr, 1),
                    Flags = attr[0],
                    Width = BitConverter.ToUInt16(attr, 5),
                    Height = BitConverter.ToUInt16(attr, 7),
                    Attributes = attr,
                    Name = name,
                    ChildPage = child
                });
            }

            return list;
        }

        private static bool IsPage(byte[] index, uint page)
        {
            if (page == 0 || page == 0xFFFFFFFFu)
            {
                return false;
            }

            if (page < HeaderSize || page + PageHeaderSize > index.Length)
            {
                return false;
            }

            if ((page - HeaderSize) % PageSize != 0)
            {
                return false;
            }

            var used = BitConverter.ToUInt16(index, (int)page + 8);
            return used <= PageSize - PageHeaderSize;
        }

        private static List<DkxEntry> Deduplicate(List<DkxEntry> entries)
        {
            var seen = new HashSet<string>(StringComparer.Ordinal);
            var result = new List<DkxEntry>();
            foreach (var e in entries)
            {
                if (string.IsNullOrEmpty(e.Filename))
                {
                    continue;
                }

                var key = (byte)e.Type + "\0" + e.Filename;
                if (!seen.Add(key))
                {
                    continue;
                }

                result.Add(e);
            }

            return result;
        }

        private static bool IsPrintableName(string name)
        {
            foreach (var c in name)
            {
                if (c < 32 || c > 126)
                {
                    return false;
                }
            }

            return name.Length > 0;
        }

        private class RawEntry
        {
            public int Offset;
            public byte Type;
            public int Length;
            public int Length2;
            public byte Flags;
            public ushort Width;
            public ushort Height;
            public byte[] Attributes = Array.Empty<byte>();
            public string Name = "";
            public uint ChildPage;
        }
    }
}