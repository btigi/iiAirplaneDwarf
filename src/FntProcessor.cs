using ii.AirplaneDwarf.Model;

namespace ii.AirplaneDwarf
{
    public class FntProcessor
    {
        public const int HeaderSize = 100;
        public const int GlyphSize = 10;

        public FntFont Read(string path) => Read(File.ReadAllBytes(path));

        public FntFont Read(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length < HeaderSize + GlyphSize)
            {
                throw new ArgumentException($"FNT data must be at least {HeaderSize + GlyphSize} bytes.", nameof(data));
            }

            var pageCount = BitConverter.ToUInt16(data, 0);
            if (2 + pageCount * 4 > HeaderSize)
            {
                throw new InvalidDataException($"FNT page count {pageCount} exceeds header capacity.");
            }

            var pages = new List<FntPage>(pageCount);
            for (var i = 0; i < pageCount; i++)
            {
                var o = 2 + i * 4;
                pages.Add(new FntPage
                {
                    ValueA = BitConverter.ToUInt16(data, o),
                    ValueB = BitConverter.ToUInt16(data, o + 2),
                });
            }

            var atlasWidth = BitConverter.ToUInt16(data, HeaderSize);
            var unknown102 = BitConverter.ToUInt16(data, HeaderSize + 2);

            var glyphs = new List<FntGlyph>();
            var pos = HeaderSize + 6;
            while (pos + GlyphSize <= data.Length)
            {
                var width = BitConverter.ToUInt16(data, pos);
                var height = BitConverter.ToUInt16(data, pos + 2);
                var charCode = BitConverter.ToUInt16(data, pos + 4);
                var atlasOffset = BitConverter.ToUInt16(data, pos + 6);
                var reserved = BitConverter.ToUInt16(data, pos + 8);
                pos += GlyphSize;

                if (width == 0 && height == 0 && charCode == 0 && atlasOffset == 0 && reserved == 0)
                {
                    break;
                }

                if (reserved != 0)
                {
                    throw new InvalidDataException($"FNT glyph reserved field is {reserved}, expected 0.");
                }

                glyphs.Add(new FntGlyph
                {
                    Width = width,
                    Height = height,
                    CharCode = charCode,
                    AtlasOffset = atlasOffset,
                });
            }

            if (glyphs.Count == 0)
            {
                throw new InvalidDataException("FNT contains no glyphs.");
            }

            return new FntFont
            {
                Pages = pages,
                AtlasWidth = atlasWidth,
                Unknown102 = unknown102,
                Glyphs = glyphs,
            };
        }
    }
}

