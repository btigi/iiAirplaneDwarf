namespace ii.AirplaneDwarf.Model
{
    public class RsprDescriptor
    {
        public string PrimarySprite { get; set; } = "";
        public string? SecondarySprite { get; set; }
        public uint Category { get; set; }
        public List<RsprSequenceStep> Sequence { get; set; } = [];

        public IEnumerable<string> GetSpriteNames()
        {
            if (!string.IsNullOrEmpty(PrimarySprite))
            {
                yield return PrimarySprite;
            }

            if (!string.IsNullOrEmpty(SecondarySprite))
            {
                yield return SecondarySprite;
            }
        }
    }
}