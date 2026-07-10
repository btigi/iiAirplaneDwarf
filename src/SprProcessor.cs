using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using ii.AirplaneDwarf.Model;

namespace ii.AirplaneDwarf
{
    public class SprProcessor
    {
        private const int FrameHeaderSize = 22;

        public List<Image<Rgba32>> Read(byte[] data, Rgba32[] palette, byte transparentIndex = 255)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (palette == null || palette.Length < 256)
            {
                throw new ArgumentException("Palette must contain 256 colours.", nameof(palette));
            }

            var frames = new List<Image<Rgba32>>();
            var pos = 0;
            while (pos + FrameHeaderSize <= data.Length)
            {
                var size = BitConverter.ToInt32(data, pos);
                if (size < FrameHeaderSize || pos + size > data.Length)
                {
                    break;
                }

                var canvasW = BitConverter.ToInt32(data, pos + 4);
                var canvasH = BitConverter.ToInt32(data, pos + 8);
                var prefix = data[pos + 13];
                var clipX = BitConverter.ToUInt16(data, pos + 14);
                var clipY = BitConverter.ToUInt16(data, pos + 16);
                var clipWidth = BitConverter.ToUInt16(data, pos + 18);
                var clipHeight = BitConverter.ToUInt16(data, pos + 20);

                if (canvasW <= 0 || canvasH <= 0 || clipWidth == 0 || clipHeight == 0)
                {
                    pos += size;
                    continue;
                }

                // Byte 13 indicates the number of int16s we need to skip (as it's unclear what they are for).
                var prefixBytes = prefix / 2;
                var dataStart = pos + FrameHeaderSize + prefixBytes;
                var dataEnd = pos + size;
                if (dataStart > dataEnd)
                {
                    pos += size;
                    continue;
                }

                var pixels = DecodeHybrid(data, dataStart, dataEnd, clipWidth * clipHeight, transparentIndex);
                var image = new Image<Rgba32>(canvasW, canvasH);
                for (var row = 0; row < clipHeight; row++)
                {
                    for (var col = 0; col < clipWidth; col++)
                    {
                        var index = pixels[row * clipWidth + col];
                        if (index == transparentIndex)
                        {
                            continue;
                        }

                        var px = clipX + col;
                        var py = clipY + row;
                        if ((uint)px < (uint)canvasW && (uint)py < (uint)canvasH)
                        {
                            image[px, py] = palette[index];
                        }
                    }
                }

                frames.Add(image);
                pos += size;
            }

            return frames;
        }

        public List<Image<Rgba32>> Read(DkxEntry entry, Rgba32[] palette)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            var transparent = entry.Attributes.Length > 13 ? entry.Attributes[13] : (byte)255;
            return Read(entry.Data, palette, transparent);
        }

        public static Rgba32[] LoadPaletteFromBmp(byte[] bmp)
        {
            if (bmp == null || bmp.Length < 54)
            {
                throw new InvalidDataException("BMP too small to contain a palette.");
            }

            var infoHeaderSize = BitConverter.ToInt32(bmp, 14);
            var paletteOffset = 14 + infoHeaderSize;
            if (paletteOffset + 256 * 4 > bmp.Length)
            {
                throw new InvalidDataException("BMP does not contain a 256-colour palette.");
            }

            var palette = new Rgba32[256];
            for (var i = 0; i < 256; i++)
            {
                var o = paletteOffset + i * 4;
                palette[i] = new Rgba32(bmp[o + 2], bmp[o + 1], bmp[o], 255);
            }

            return palette;
        }

        public static Rgba32[] LoadPaletteFromBmp(string path) => LoadPaletteFromBmp(File.ReadAllBytes(path));

        // 0x00-0x7F = skip N pixels (leave transparent)
        // 0x80-0xBF = copy (cmd & 0x3F) literals (0 means 64)
        // 0xC0-0xFF = run (cmd & 0x3F) of the next byte (0 means 64)
        private static byte[] DecodeHybrid(byte[] sourceBuffer, int start, int limit, int expected, byte fill)
        {
            var destinationBuffer = new byte[expected];
            Array.Fill(destinationBuffer, fill);
            var i = start;
            var p = 0;
            while (p < expected)
            {
                if (i >= limit)
                {
                    break;
                }

                var cmd = sourceBuffer[i++];
                if (cmd < 0x80)
                {
                    p += cmd;
                    if (p > expected)
                    {
                        p = expected;
                    }
                }
                else if (cmd < 0xC0)
                {
                    var n = cmd & 0x3F;
                    if (n == 0)
                    {
                        n = 64;
                    }

                    for (var k = 0; k < n; k++)
                    {
                        if (i >= limit || p >= expected)
                        {
                            return destinationBuffer;
                        }

                        destinationBuffer[p++] = sourceBuffer[i++];
                    }
                }
                else
                {
                    var n = cmd & 0x3F;
                    if (n == 0)
                    {
                        n = 64;
                    }

                    if (i >= limit)
                    {
                        break;
                    }

                    var val = sourceBuffer[i++];
                    for (var k = 0; k < n && p < expected; k++)
                    {
                        destinationBuffer[p++] = val;
                    }
                }
            }

            return destinationBuffer;
        }
    }
}