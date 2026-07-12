namespace ii.AirplaneDwarf.Model
{
    public class SarTable
    {
        public int SlotCount { get; set; }
        public int Capacity { get; set; }
        public int Parameter { get; set; }
        public List<SarEntry> Entries { get; set; } = [];

        public IEnumerable<string> GetValues() => Entries.Select(e => e.Value);
    }
}