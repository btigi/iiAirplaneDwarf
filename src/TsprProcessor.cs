using System.Text;

namespace ii.AirplaneDwarf
{
    public class TsprProcessor
    {
        public const int RecordSize = 68;
        public const int NameLength = 16;

        public List<string> Read(string path) => Read(File.ReadAllBytes(path));

        public List<string> Read(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var names = new List<string>();
            var pos = 0;
            while (pos + RecordSize <= data.Length)
            {
                var name = ReadName(data, pos);
                if (name == null)
                {
                    break;
                }

                names.Add(name);
                pos += RecordSize;
            }

            return names;
        }

        private static string? ReadName(byte[] data, int offset)
        {
            var end = offset;
            var limit = offset + NameLength;
            while (end < limit && data[end] != 0)
            {
                var c = data[end];
                if (c < 32 || c > 126)
                {
                    return null;
                }

                end++;
            }

            if (end == offset)
            {
                return null;
            }

            // Null terminated (potentially followed by unitialized junk)
            for (var i = end; i < limit; i++)
            {
                if (data[i] != 0)
                {
                    return null;
                }
            }

            return Encoding.ASCII.GetString(data, offset, end - offset);
        }
    }
}