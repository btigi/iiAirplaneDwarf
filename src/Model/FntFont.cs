namespace ii.AirplaneDwarf.Model
{
    public class FntFont
    {
        public List<FntPage> Pages { get; set; } = [];
        public ushort AtlasWidth { get; set; }
        public ushort Unknown102 { get; set; }
        public List<FntGlyph> Glyphs { get; set; } = [];

        public FntGlyph? FindGlyph(char c) => Glyphs.FirstOrDefault(g => g.CharCode == c);
    }
}