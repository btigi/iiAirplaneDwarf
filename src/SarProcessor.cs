using System.Text;
using ii.AirplaneDwarf.Model;

namespace ii.AirplaneDwarf
{
    public class SarProcessor
    {
        public const int HeaderSize = 24;

        public SarTable Read(string path) => Read(File.ReadAllBytes(path));

        public SarTable Read(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length < HeaderSize)
            {
                throw new ArgumentException($"SAR data must be at least {HeaderSize} bytes.", nameof(data));
            }

            if (BitConverter.ToUInt32(data, 0) != 0 || BitConverter.ToUInt32(data, 4) != 0)
            {
                throw new InvalidDataException("SAR header is not in the standard format (possibly a compressed Strategs variant).");
            }

            if (BitConverter.ToUInt32(data, 20) != 0)
            {
                throw new InvalidDataException("SAR header reserved field at offset 20 is non-zero.");
            }

            var slotCount = BitConverter.ToInt32(data, 8);
            var capacity = BitConverter.ToInt32(data, 12);
            var parameter = BitConverter.ToInt32(data, 16);
            if (slotCount < 0 || capacity < 0 || parameter < 0)
            {
                throw new InvalidDataException("SAR header contains negative values.");
            }

            var bitsetBytes = slotCount == 0 ? 0 : (slotCount + 7) / 8;
            if (HeaderSize + bitsetBytes > data.Length)
            {
                throw new InvalidDataException("SAR bitset is truncated.");
            }

            var activeIndices = new List<int>();
            for (var i = 0; i < slotCount; i++)
            {
                var b = data[HeaderSize + (i / 8)];
                var bit = (b >> (7 - (i % 8))) & 1;
                if (bit != 0)
                {
                    activeIndices.Add(i);
                }
            }

            var pos = HeaderSize + bitsetBytes;
            var entries = new List<SarEntry>(activeIndices.Count);
            foreach (var index in activeIndices)
            {
                if (pos >= data.Length)
                {
                    throw new InvalidDataException("SAR string table is truncated.");
                }

                var start = pos;
                while (pos < data.Length && data[pos] != 0)
                {
                    pos++;
                }

                var value = Encoding.ASCII.GetString(data, start, pos - start);
                if (pos < data.Length)
                {
                    pos++;
                }

                entries.Add(new SarEntry { Index = index, Value = value });
            }

            if (pos != data.Length)
            {
                throw new InvalidDataException($"SAR has {data.Length - pos} trailing bytes.");
            }

            return new SarTable
            {
                SlotCount = slotCount,
                Capacity = capacity,
                Parameter = parameter,
                Entries = entries,
            };
        }
    }
}

