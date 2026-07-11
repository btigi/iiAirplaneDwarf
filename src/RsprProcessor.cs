using System.Text;
using ii.AirplaneDwarf.Model;

namespace ii.AirplaneDwarf
{
    public class RsprProcessor
    {
        public const int HeaderSize = 92;
        public const int NameLength = 32;
        public const int SequenceEntrySize = 12;
        public const int MagicOffset = 64;
        public const int VersionOffset = 68;
        public const int CategoryOffset = 72;
        public const int SequenceCountOffset = 76;
        public const uint ExpectedMagic = 90;
        public const uint ExpectedVersion = 49;

        public RsprDescriptor Read(string path) => Read(File.ReadAllBytes(path));

        public RsprDescriptor Read(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length < HeaderSize)
            {
                throw new ArgumentException($"RSPR data must be at least {HeaderSize} bytes.", nameof(data));
            }

            var sequenceCount = BitConverter.ToUInt32(data, SequenceCountOffset);
            var expectedLength = HeaderSize + (int)sequenceCount * SequenceEntrySize;
            if (data.Length != expectedLength)
            {
                throw new InvalidDataException($"RSPR length {data.Length} does not match header sequence count {sequenceCount}.");
            }

            var magic = BitConverter.ToUInt32(data, MagicOffset);
            if (magic != ExpectedMagic)
            {
                throw new InvalidDataException($"RSPR magic value {magic} is not {ExpectedMagic}.");
            }

            var version = BitConverter.ToUInt32(data, VersionOffset);
            if (version != ExpectedVersion)
            {
                throw new InvalidDataException($"RSPR version value {version} is not {ExpectedVersion}.");
            }

            return new RsprDescriptor
            {
                PrimarySprite = ReadName(data, 0),
                SecondarySprite = ReadOptionalName(data, NameLength),
                Category = BitConverter.ToUInt32(data, CategoryOffset),
                Sequence = ReadSequence(data, sequenceCount),
            };
        }

        private static string ReadName(byte[] data, int offset)
        {
            var name = ReadOptionalName(data, offset);
            if (name == null)
            {
                throw new InvalidDataException("RSPR primary sprite name is empty.");
            }

            return name;
        }

        private static string? ReadOptionalName(byte[] data, int offset)
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

            return Encoding.ASCII.GetString(data, offset, end - offset);
        }

        private static List<RsprSequenceStep> ReadSequence(byte[] data, uint sequenceCount)
        {
            var sequence = new List<RsprSequenceStep>((int)sequenceCount);
            var offset = HeaderSize;
            for (var i = 0; i < sequenceCount; i++)
            {
                var frameIndex = BitConverter.ToUInt32(data, offset);
                var parameter = BitConverter.ToUInt32(data, offset + 4);
                var layerType = BitConverter.ToUInt32(data, offset + 8);
                offset += SequenceEntrySize;

                if (frameIndex == 0 && parameter == 0 && layerType == 0)
                {
                    continue;
                }

                sequence.Add(new RsprSequenceStep
                {
                    FrameIndex = frameIndex,
                    Parameter = parameter,
                    LayerType = layerType,
                });
            }

            return sequence;
        }
    }
}

