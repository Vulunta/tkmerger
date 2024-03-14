using TkMerger.Core.IO;

namespace TkMerger.Desktop.IO;

public class DesktopDataResolver : DataResolver
{
    public override Task<Stream> GetResource(string resourceName)
    {
        string file = Path.Combine("Resources", resourceName);
        return Task.FromResult<Stream>(File.OpenRead(file));
    }

    public override void WriteBytes(string file, Span<byte> data)
    {
        if (Path.GetDirectoryName(file) is string folder) {
            Directory.CreateDirectory(folder);
        }

        using FileStream fs = File.Create(file);
        fs.Write(data);
    }
}
