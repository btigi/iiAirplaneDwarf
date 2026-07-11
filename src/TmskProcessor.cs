using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ii.AirplaneDwarf
{
    public class TmskProcessor
    {
        public const int DefaultWidth = 32;
        public const int DefaultHeight = 32;

        private static readonly Rgba32 Solid = new(255, 255, 255, 255);
        private static readonly Rgba32 Empty = new(0, 0, 0, 0);

        public List<Image<Rgba32>> Read(string path, int width = DefaultWidth, int height = DefaultHeight) => Read(File.ReadAllBytes(path), width, height);

        public List<Image<Rgba32>> Read(byte[] data, int width = DefaultWidth, int height = DefaultHeight)
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
            var frameBytes = bytesPerRow * height;
            if (frameBytes <= 0)
            {
                throw new ArgumentException("Frame size is invalid.");
            }

            if (data.Length == frameBytes)
            {
                return [DecodeFrame(data, 0, width, height, bytesPerRow)];
            }

            var frames = new List<Image<Rgba32>>();
            var pos = 0;
            while (pos + 4 <= data.Length)
            {
                var size = BitConverter.ToInt32(data, pos);
                if (size != frameBytes)
                {
                    throw new InvalidDataException(
                        $"TMSK frame at {pos} has size {size}, expected {frameBytes} for {width}x{height}.");
                }

                pos += 4;
                if (pos + size > data.Length)
                {
                    throw new InvalidDataException("TMSK frame data is truncated.");
                }

                frames.Add(DecodeFrame(data, pos, width, height, bytesPerRow));
                pos += size;
            }

            if (pos != data.Length)
            {
                throw new InvalidDataException($"TMSK has {data.Length - pos} trailing bytes.");
            }

            if (frames.Count == 0)
            {
                throw new InvalidDataException("TMSK contains no frames.");
            }

            return frames;
        }

        private static Image<Rgba32> DecodeFrame(byte[] data, int offset, int width, int height, int bytesPerRow)
        {
            var image = new Image<Rgba32>(width, height);
            for (var row = 0; row < height; row++)
            {
                var rowOffset = offset + row * bytesPerRow;
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

