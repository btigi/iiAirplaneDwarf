iiAirplaneDwarf
=========

iiAirplaneDwarf is a C# library supporting the modification of files relating to Submarine Titans, the 2000 RTS game developed by Ellipse Studios.

| Name   | Read | Write | Comment
|--------|:----:|-------|--------
| 3DX     | ✗   |   ✗   | 
| ANY     | ✗   |   ✗   | 
| AOBJ    | ✗   |   ✗   | 
| BIN     | ✗   |   ✗   | 
| BMP     | ✗   |   ✗   | 
| DAR     | ✗   |   ✗   | 
| DKD     | ✔   |   ✗   | Archive data
| DKX     | ✔   |   ✗   | Archive index
| FNT     | ✗   |   ✗   | 
| IMT     | ✗   |   ✗   | 
| MSK     | ✗   |   ✗   | 
| PIC     | ✗   |   ✗   | 
| RSPR    | ✗   |   ✗   | 
| SAR     | ✗   |   ✗   | 
| SPR     | ✗   |   ✗   | 
| SSPR    | ✗   |   ✗   | 
| TMAP    | ✗   |   ✗   | 
| TMSK    | ✗   |   ✗   | 
| TSPR    | ✗   |   ✗   | 
| WAV     | ✗   |   ✗   | 

## Usage

```csharp
var processor = new DkxProcessor();
foreach (var archive in archives)
{
    if (!File.Exists(archive))
    {
        Console.WriteLine($"skip missing {archive}");
        continue;
    }

    Console.WriteLine($"Reading {archive}");

    var dkdPath = Path.ChangeExtension(archive, "dkd");

    var entries = processor.Read(archive, dkdPath);
    var dest = Path.Combine(outRoot, Path.GetFileNameWithoutExtension(archive));
    Directory.CreateDirectory(dest);

    var written = 0;
    foreach (var entry in entries)
    {
        var payload = entry.Data;
        if (payload == null || payload.Length == 0)
        {
            continue;
        }

        var safe = string.Join("_", entry.Filename.Split(Path.GetInvalidFileNameChars()));
        var path = Path.Combine(dest, $"{safe}.{entry.Extension}");
        if (File.Exists(path))
        {
            path = Path.Combine(dest, $"{safe}_{entry.Type}.{entry.Extension}");
        }

        File.WriteAllBytes(path, payload);
        written++;
    }

    var types = entries.GroupBy(e => e.TypeName).OrderBy(g => g.Key);
    Console.WriteLine($"  {entries.Count} entries, wrote {written} files -> {dest}");
    Console.WriteLine("  " + string.Join(", ", types.Select(g => $"{g.Key}:{g.Count()}")));
}
```

## Compiling

To clone and run this application, you'll need [Git](https://git-scm.com) and [.NET](https://dotnet.microsoft.com/) installed on your computer. From your command line:

```
# Clone this repository
$ git clone https://github.com/btigi/iiAirplaneDwarf

# Go into the repository
$ cd src

# Build  the app
$ dotnet build
```

## Licencing

iiAirplaneDwarf is licenced under the MIT License. Full licence details are available in licence.md