using ii.AirplaneDwarf;

public class Test
{

    string[] extensions =
    {
    "ANY",  // 0
    "BMP",  // 1
    "WAV",  // 2
    "FNT",  // 3
    "PCX",  // 4
    "GIF",  // 5
    "SPR",  // 6
    "SSPR", // 7
    "MSPR", // 8
    "AME",  // 9
    "AMAP", // 10
    "IMT",  // 11
    "AOBJ", // 12
    "PLA",  // 13
    "BMT",  // 14
    "TMAP", // 15
    "ALIB", // 16
    "BMAP", // 17  0 files
    "MSK",  // 18
    "TSPR", // 19
    "DAR",  // 20
    "USPR", // 21
    "TMSK", // 22
    "SAR",  // 23  Group multiple equivalent sounds together, e.g. "yes sir", "affirmative", "affirmative sir"
    "SPAG", // 24  2 files, both 0-bytes
    "ZONE", // 25  0 files
    "JPG",  // 26  0 files
    "3DX",  // 27
    "PIC",  // 28
    "RSPR", // 29
    "QSPR", // 30  0 files
    "QMT"   // 31  0 files
};

    /*
    2 - WAV
    12, 94, 518, 95, 255 - WAV ?

    6, 22, 29 - image

    12, 23

    27 - map mesh
    29 - map texture
    */

    public void Go()
    {
        var processor = new DkxProcessor();

        //var other = processor.ReadDkx(@"D:\Games\Submarine Titans\data\TASKS.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\data\TASKS.dkd", other);

        //var music = processor.ReadDkx(@"D:\Games\Submarine Titans\music\music.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\music\music.dkd", music);

        //var files = processor.ReadDkx(@"D:\Games\Submarine Titans\sound\sounds.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\sound\sounds.dkd", files);



        //var other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\2.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\2.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\Abyss_of_Despair.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\Abyss_of_Despair.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\Clash.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\Clash.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\Conflict_Zone.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\Conflict_Zone.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\Damnation.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\Damnation.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\Dance_of_Death.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\Dance_of_Death.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\Deep_Blue.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\Deep_Blue.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\Gauntlet.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\Gauntlet.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\Impact_Zone.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\Impact_Zone.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\Labyrinth.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\Labyrinth.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\Plasma_Burn.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\Plasma_Burn.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\Shark_Haven.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\Shark_Haven.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\Single_Ladder.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\Single_Ladder.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\Team_Ladder.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\Team_Ladder.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\Valley_of_Death.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\Valley_of_Death.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\War_Triangle.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\War_Triangle.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\_mClash_of_the_Titans.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\_mClash_of_the_Titans.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\_mCrescent_Duel.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\_mCrescent_Duel.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\_mDeep_Cut.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\_mDeep_Cut.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\_mSub_Duel.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\_mSub_Duel.dkd", other);

        //var other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\_mSunken_Arches.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\_mSunken_Arches.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\_mThe_Sea_Pit.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\_mThe_Sea_Pit.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\_mTsunami.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\_mTsunami.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\_mWar_in_the_Abyss.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\_mWar_in_the_Abyss.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\_mWatergeddon.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\_mWatergeddon.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\custom\_mWater_World.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\custom\_mWater_World.dkd", other);
        var other = processor.ReadDkx(@"D:\Games\Submarine Titans\data\BOAT.DKX");
        ThingGeneral(@"D:\Games\Submarine Titans\data\BOAT.DKD", other);
        other = processor.ReadDkx(@"D:\Games\Submarine Titans\data\CONTROLG.DKX");
        ThingGeneral(@"D:\Games\Submarine Titans\data\CONTROLG.DKD", other);
        other = processor.ReadDkx(@"D:\Games\Submarine Titans\data\EDITOR.DKX");
        ThingGeneral(@"D:\Games\Submarine Titans\data\EDITOR.DKD", other);
        other = processor.ReadDkx(@"D:\Games\Submarine Titans\data\NATURE.DKX");
        ThingGeneral(@"D:\Games\Submarine Titans\data\NATURE.DKD", other);
        other = processor.ReadDkx(@"D:\Games\Submarine Titans\data\OBJECT.DKX");
        ThingGeneral(@"D:\Games\Submarine Titans\data\OBJECT.DKD", other);
        other = processor.ReadDkx(@"D:\Games\Submarine Titans\data\OTHER.DKX");
        ThingGeneral(@"D:\Games\Submarine Titans\data\OTHER.DKD", other);
        other = processor.ReadDkx(@"D:\Games\Submarine Titans\data\TASKS.DKX");
        ThingGeneral(@"D:\Games\Submarine Titans\data\TASKS.DKD", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss101.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss101.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss102.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss102.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss103.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss103.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss104.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss104.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\miss105.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\miss105.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss106.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss106.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss107.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss107.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss108.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss108.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss109.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss109.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss110.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss110.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss201.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss201.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss202.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss202.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss203.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss203.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss204.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss204.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss205.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss205.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss206.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss206.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss207.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss207.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss208.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss208.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss209.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss209.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss210.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss210.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\miss301.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\miss301.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\miss302.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\miss302.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\miss303.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\miss303.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\miss304.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\miss304.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\miss305.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\miss305.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\miss306.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\miss306.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\miss307.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\miss307.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\miss308.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\miss308.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss309.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss309.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\Miss310.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\Miss310.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\tutor101.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\tutor101.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\tutor201.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\tutor201.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\missions\tutor301.dkx");
        //ThingGeneral(@"D:\Games\Submarine Titans\missions\tutor301.dkd", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\music\MUSIC.DKX");
        //ThingGeneral(@"D:\Games\Submarine Titans\music\MUSIC.DKD", other);
        other = processor.ReadDkx(@"D:\Games\Submarine Titans\savegame\Player\PL_LOG.DKX");
        ThingGeneral(@"D:\Games\Submarine Titans\savegame\Player\PL_LOG.DKD", other);
        other = processor.ReadDkx(@"D:\Games\Submarine Titans\savegame\Player\SAVE_MissWS01autosave.DKX");
        ThingGeneral(@"D:\Games\Submarine Titans\savegame\Player\SAVE_MissWS01autosave.DKD", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\sound\SOUNDS.DKX");
        //ThingGeneral(@"D:\Games\Submarine Titans\sound\SOUNDS.DKD", other);
        other = processor.ReadDkx(@"D:\Games\Submarine Titans\system\GENERATE.DKX");
        ThingGeneral(@"D:\Games\Submarine Titans\system\GENERATE.DKD", other);
        other = processor.ReadDkx(@"D:\Games\Submarine Titans\system\inter.DKX");
        ThingGeneral(@"D:\Games\Submarine Titans\system\inter.DKD", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\system\Land00.DKX");
        //ThingGeneral(@"D:\Games\Submarine Titans\system\Land00.DKD", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\system\Land01.DKX");
        //ThingGeneral(@"D:\Games\Submarine Titans\system\Land01.DKD", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\system\Land02.DKX");
        //ThingGeneral(@"D:\Games\Submarine Titans\system\Land02.DKD", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\system\Land03.DKX");
        //ThingGeneral(@"D:\Games\Submarine Titans\system\Land03.DKD", other);
        //other = processor.ReadDkx(@"D:\Games\Submarine Titans\system\Strategs.DKX");
        //ThingGeneral(@"D:\Games\Submarine Titans\system\Strategs.DKD", other);
    }

    public void ThingGeneral(string s, List<Dkx> files)
    {
        using var fs = new FileStream(s, FileMode.Open, FileAccess.Read);
        using var br = new BinaryReader(fs);
        foreach (var file in files)
        {
            try
            {
                fs.Seek(file.Offset, SeekOrigin.Begin);
                var data = br.ReadBytes(file.Length);

                var ext = "hex";
                byte[] outData = data;

                if (file.Type == 1)
                {
                    var filenameUpper = file.Filename.ToUpperInvariant();
                    if (filenameUpper.StartsWith("DEFAULT") || filenameUpper.StartsWith("FACE"))
                    {
                        int width = (file.Unknown2[5] | (file.Unknown2[6] << 8));
                        int height = (file.Unknown2[7] | (file.Unknown2[8] << 8));
                        int paletteSize = 1024;
                        int imageSize = width * height;
                        byte[] palette = new byte[paletteSize];
                        byte[] imageData = new byte[imageSize];
                        Array.Copy(data, 0, palette, 0, paletteSize);
                        Array.Copy(data, paletteSize, imageData, 0, imageSize);

                        using var ms = new MemoryStream();
                        WriteBmpHeader(ms, height, width, palette);

                        int bytesPerRow = width;
                        int paddingPerRow = (4 - (bytesPerRow % 4)) % 4;
                        int shift = 40;
                        for (int y = 0; y < height; y++)
                        {
                            int rowStart = y * width;
                            ms.Write(imageData, rowStart + shift, width - shift);
                            ms.Write(imageData, rowStart, shift);
                            for (int p = 0; p < paddingPerRow; p++)
                                ms.WriteByte(0);
                        }
                        outData = ms.ToArray();
                        ext = "bmp";
                    }
                    else
                    {
                        int width = (file.Unknown2[5] | (file.Unknown2[6] << 8));
                        int height = (file.Unknown2[7] | (file.Unknown2[8] << 8));

                        Console.WriteLine($"0:{file.Unknown2[0],3}  1:{file.Unknown2[1],3}  h:{height,3}  w:{width,3}  |  {file.Filename}");

                        int paletteSize = 1024;
                        int imageSize = width * height;
                        byte[] palette = new byte[paletteSize];
                        byte[] imageData = new byte[imageSize];
                        Array.Copy(data, 0, palette, 0, paletteSize);
                        Array.Copy(data, paletteSize, imageData, 0, imageSize);

                        using var ms = new MemoryStream();
                        WriteBmpHeader(ms, height, width, palette);
                        int shift = 40;
                        byte[] rowBuffer = new byte[width];
                        for (int y = 0; y < height; y++)
                        {
                            int rowStart = y * width;
                            // Rearrange: columns 60..width-1, then 0..59
                            Array.Copy(imageData, rowStart + shift, rowBuffer, 0, width - shift);
                            Array.Copy(imageData, rowStart, rowBuffer, width - shift, shift);
                            ms.Write(rowBuffer, 0, width);
                        }
                        outData = ms.ToArray();
                        ext = "bmp";
                    }
                }
                else if (file.Type == 2)
                {
                    using var ms = new MemoryStream();
                    WriteWavHeader(ms, file.Length);
                    ms.Write(data, 0, data.Length);
                    outData = ms.ToArray();
                    ext = "wav";
                }
                else
                {
                    ext = extensions[file.Type];
                }

                if (file.Type != 1)
                    continue;

                var fname = @$"D:\data\sub\x\{file.Filename}.{ext}";
                //while (File.Exists(fname))
                //{
                //    fname = fname.Insert(fname.Length - 4, "_");
                //}
                File.WriteAllBytes(fname, outData);
            }
            catch (Exception ex)
            {
            }
        }
    }

    public void WriteWavHeader(Stream stream, int size)
    {
        int sampleRate = 11025;
        short bitsPerSample = 16;
        short channels = 2;
        int byteRate = sampleRate * channels * bitsPerSample / 8;
        short blockAlign = (short)(channels * bitsPerSample / 8);

        // RIFF header
        stream.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"), 0, 4);
        stream.Write(BitConverter.GetBytes(size + 36), 0, 4); // File size - 8
        stream.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"), 0, 4);

        // fmt subchunk
        stream.Write(System.Text.Encoding.ASCII.GetBytes("fmt "), 0, 4);
        stream.Write(BitConverter.GetBytes(16), 0, 4);       // Subchunk1Size for PCM
        stream.Write(BitConverter.GetBytes((short)1), 0, 2); // AudioFormat PCM = 1
        stream.Write(BitConverter.GetBytes(channels), 0, 2);
        stream.Write(BitConverter.GetBytes(sampleRate), 0, 4);
        stream.Write(BitConverter.GetBytes(byteRate), 0, 4);
        stream.Write(BitConverter.GetBytes(blockAlign), 0, 2);
        stream.Write(BitConverter.GetBytes(bitsPerSample), 0, 2);

        // data subchunk
        stream.Write(System.Text.Encoding.ASCII.GetBytes("data"), 0, 4);
        stream.Write(BitConverter.GetBytes(size), 0, 4);
    }

    public void WriteBmpHeader(Stream stream, int height, int width, byte[] palette)
    {
        short bitsPerPixel = 8;
        int bytesPerRow = width;
        int paddingPerRow = (4 - (bytesPerRow % 4)) % 4;
        int paddedBytesPerRow = bytesPerRow + paddingPerRow;
        var imageDataSize = paddedBytesPerRow * height;
        var paletteSize = 256 * 4; // 256 colors, 4 bytes per color (BGRA)

        // BITMAPFILEHEADER
        stream.Write(System.Text.Encoding.ASCII.GetBytes("BM")); // Signature
        stream.Write(BitConverter.GetBytes(54 + paletteSize + imageDataSize)); // File size
        stream.Write(BitConverter.GetBytes(0));                  // Reserved
        stream.Write(BitConverter.GetBytes(54 + paletteSize));   // Pixel data offset

        // BITMAPINFOHEADER
        stream.Write(BitConverter.GetBytes(40));                 // Header size (40 bytes)
        stream.Write(BitConverter.GetBytes(width));
        stream.Write(BitConverter.GetBytes(height));             // Height
        stream.Write(BitConverter.GetBytes((short)1));           // Planes (always 1)
        stream.Write(BitConverter.GetBytes(bitsPerPixel));       // 8 bits per pixel
        stream.Write(BitConverter.GetBytes(0));                  // Compression (0 = none)
        stream.Write(BitConverter.GetBytes(imageDataSize));      // Image size
        stream.Write(BitConverter.GetBytes(0));                  // Horizontal resolution
        stream.Write(BitConverter.GetBytes(0));                  // Vertical resolution
        stream.Write(BitConverter.GetBytes(256));                // Color palette (256 colors for 8-bit)
        stream.Write(BitConverter.GetBytes(0));                  // Important colors

        // Write the palette (should be 1024 bytes, BGRA)
        stream.Write(palette, 0, 1024);
    }
}