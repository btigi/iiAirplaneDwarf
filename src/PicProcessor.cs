using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ii.AirplaneDwarf
{
    public class PicProcessor
    {
        private const int PlaneCount = 16;
        private const int MultiPlanePadding = 60;

        public Image<Rgba32> Read(byte[] data, Rgba32[] palette, int width = 0, int height = 0, int plane = 0)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (palette == null || palette.Length < 256)
            {
                throw new ArgumentException("Palette must contain 256 colours.", nameof(palette));
            }

            if (TryGetMultiPlaneCount(data, out var pixelCount))
            {
                ResolveDimensions(pixelCount, ref width, ref height);
                if (plane < 0 || plane >= PlaneCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(plane), "Plane must be 0..15.");
                }

                return ReadIndexed(data, 4 + plane * pixelCount, width, height, palette);
            }

            ResolveRawDimensions(data.Length, ref width, ref height);
            if (width * height != data.Length)
            {
                throw new InvalidDataException($"PIC size {data.Length} does not match {width}x{height} ({width * height} pixels).");
            }

            return ReadIndexed(data, 0, width, height, palette);
        }

        public static bool IsMultiPlane(byte[] data) => TryGetMultiPlaneCount(data, out _);

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

        private static bool TryGetMultiPlaneCount(byte[] data, out int pixelCount)
        {
            pixelCount = 0;
            if (data.Length < 4 + PlaneCount + MultiPlanePadding)
            {
                return false;
            }

            pixelCount = BitConverter.ToInt32(data, 0);
            if (pixelCount <= 0)
            {
                return false;
            }

            var expected = 4 + PlaneCount * pixelCount + MultiPlanePadding;
            return data.Length == expected;
        }

        private static void ResolveRawDimensions(int length, ref int width, ref int height)
        {
            if (width > 0 && height > 0)
            {
                return;
            }

            var side = (int)Math.Sqrt(length);
            if (side * side != length)
            {
                throw new InvalidDataException("Raw PIC dimensions unknown; pass width/height, or use a square pixel buffer.");
            }

            if (width <= 0)
            {
                width = side;
            }

            if (height <= 0)
            {
                height = side;
            }
        }

        private static void ResolveDimensions(int pixelCount, ref int width, ref int height)
        {
            if (width > 0 && height > 0)
            {
                if (width * height != pixelCount)
                {
                    throw new InvalidDataException($"Multi-plane PIC count {pixelCount} does not match {width}x{height}.");
                }

                return;
            }

            if (width > 0)
            {
                if (pixelCount % width != 0)
                {
                    throw new InvalidDataException($"Multi-plane PIC count {pixelCount} is not divisible by width {width}.");
                }

                height = pixelCount / width;
                return;
            }

            if (height > 0)
            {
                if (pixelCount % height != 0)
                {
                    throw new InvalidDataException($"Multi-plane PIC count {pixelCount} is not divisible by height {height}.");
                }

                width = pixelCount / height;
                return;
            }

            throw new InvalidDataException("Multi-plane PIC requires width and/or height (use DKX entry Width/Height).");
        }

        private static Image<Rgba32> ReadIndexed(byte[] data, int offset, int width, int height, Rgba32[] palette)
        {
            if (offset < 0 || offset + width * height > data.Length)
            {
                throw new InvalidDataException("PIC pixel data is truncated.");
            }

            var image = new Image<Rgba32>(width, height);
            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    image[col, row] = palette[data[offset + row * width + col]];
                }
            }

            return image;
        }
    }
}