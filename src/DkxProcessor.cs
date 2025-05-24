namespace ii.AirplaneDwarf
{
    public class DkxProcessor
    {
        public List<Dkx> ReadDkx(string filename)
        {
            var result = new List<Dkx>();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);

            // Skip to offset 0x54
            fs.Seek(0x54, SeekOrigin.Begin);

            var i = 0;
            try
            {
                while (fs.Position < fs.Length)
                {
                    if (i == 491)
                        break;

                    if (i > 0 && i % 52 == 0)
                    {
                        _ = br.ReadBytes(20);
                    }

                    var dkx = new Dkx();

                    dkx.Offset = br.ReadInt32();
                    dkx.Unknown1 = br.ReadByte();
                    dkx.Length = br.ReadInt32();
                    if (dkx.Unknown1 == 24)
                    {
                        dkx.Unknown2 = br.ReadBytes(16);
                    }
                    else if (dkx.Unknown1 == 0)
                    {
                        dkx.Unknown2 = br.ReadBytes(19);
                    }
                    else if (dkx.Unknown1 == 1)
                    {
                        dkx.Unknown2 = br.ReadBytes(17);
                    }
                    else
                    {
                        dkx.Unknown2 = br.ReadBytes(17);
                    }
                    dkx.FilenameLength = br.ReadInt16();
                    dkx.Filename = System.Text.Encoding.UTF8.GetString(br.ReadBytes(dkx.FilenameLength));
                    dkx.Unknown3 = br.ReadInt32();

                    result.Add(dkx);
                    i++;
                }
            }
            catch
            {

            }
            return result;
        }
    }

    public class Dkx
    {
        public int Offset { get; set; }
        public byte Unknown1 { get; set; }
        public int Length { get; set; }
        public byte[] Unknown2 { get; set; }
        public Int16 FilenameLength { get; set; }
        public string Filename { get; set; }
        public int Unknown3 { get; set; }
    }
}