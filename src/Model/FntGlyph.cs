namespace ii.AirplaneDwarf.Model
{
    public class FntGlyph
    {
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public ushort CharCode { get; set; }
        public ushort AtlasOffset { get; set; }

        public char Character => CharCode <= char.MaxValue ? (char)CharCode : '?';
    }
}