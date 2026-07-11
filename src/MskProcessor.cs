using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ii.AirplaneDwarf
{
    public class MskProcessor
    {
        private static readonly Rgba32 Solid = new(255, 255, 255, 255);
        private static readonly Rgba32 Empty = new(0, 0, 0, 0);

        public Image<Rgba32> Read(string path, int width, int height) =>
            Read(File.ReadAllBytes(path), width, height);

        public Image<Rgba32> Read(byte[] data, int width, int height)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Width and height must be positive.");
            }

            var bytesPerRow = (width + 7) / 8;
            var expected = bytesPerRow * height;
            if (data.Length != expected)
            {
                throw new InvalidDataException(
                    $"MSK size {data.Length} does not match {width}x{height} ({expected} bytes).");
            }

            var image = new Image<Rgba32>(width, height);
            for (var row = 0; row < height; row++)
            {
                var rowOffset = row * bytesPerRow;
                for (var col = 0; col < width; col++)
                {
                    var b = data[rowOffset + (col / 8)];
                    var bit = (b >> (7 - (col % 8))) & 1;
                    image[col, row] = bit != 0 ? Solid : Empty;
                }
            }

            return image;
        }
    }
}