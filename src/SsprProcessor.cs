using System.Text;

namespace ii.AirplaneDwarf
{
    public class SsprProcessor
    {
        public const int RecordSize = 49;
        public const int NameLength = 16;
        public const int IndexPaddingOffset = 32;
        public const int FrameCountOffset = 35;

        public List<string> Read(string path) => Read(File.ReadAllBytes(path));

        public List<string> Read(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length < RecordSize)
            {
                throw new ArgumentException($"SSPR data must be at least {RecordSize} bytes.", nameof(data));
            }

            var prefix = ReadPrefix(data);
            var indexPadding = BitConverter.ToUInt16(data, IndexPaddingOffset);
            var frameCount = data[FrameCountOffset];
            return BuildFrameNames(prefix, frameCount, indexPadding);
        }

        private static string ReadPrefix(byte[] data)
        {
            var end = 0;
            while (end < NameLength && data[end] != 0)
            {
                var c = data[end];
                if (c < 32 || c > 126)
                {
                    throw new InvalidDataException("SSPR name prefix contains invalid characters.");
                }

                end++;
            }

            if (end == 0)
            {
                throw new InvalidDataException("SSPR name prefix is empty.");
            }

            return Encoding.ASCII.GetString(data, 0, end);
        }

        private static List<string> BuildFrameNames(string prefix, int frameCount, int indexPadding)
        {
            if (frameCount < 0)
            {
                throw new InvalidDataException("SSPR frame count is negative.");
            }

            if (indexPadding != 1 && indexPadding != 2)
            {
                throw new InvalidDataException($"Unsupported SSPR index padding value {indexPadding}.");
            }

            var names = new List<string>(frameCount);
            for (var i = 0; i < frameCount; i++)
            {
                var suffix = indexPadding == 1 ? i.ToString() : i.ToString("D2");
                names.Add(prefix + suffix);
            }

            return names;
        }
    }
}