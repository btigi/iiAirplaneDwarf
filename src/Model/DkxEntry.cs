namespace ii.AirplaneDwarf.Model
{
    public class DkxEntry
    {
        public int Offset { get; set; }
        public DkxType Type { get; set; }
        public int Length { get; set; }
        public int Length2 { get; set; }
        public byte Flags { get; set; }
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public byte[] Attributes { get; set; } = [];
        public string Filename { get; set; } = "";
        public uint ChildPage { get; set; }
        public byte[] Data { get; set; } = [];

        public string TypeName =>
            Type switch
            {
                DkxType.Dx3 => "3DX",
                _ when Enum.IsDefined(Type) => Type.ToString().ToUpperInvariant(),
                _ => $"TYPE_{(byte)Type}"
            };

        public string Extension =>
            Type switch
            {
                DkxType.Bmp => "bmp",
                DkxType.Wav => "wav",
                DkxType.Dx3 => "3dx",
                _ when Enum.IsDefined(Type) => Type.ToString().ToLowerInvariant(),
                _ => "bin"
            };
    }
}