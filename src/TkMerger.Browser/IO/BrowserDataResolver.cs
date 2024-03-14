using System.Runtime.InteropServices.JavaScript;
using TkMerger.Core.IO;

namespace TkMerger.Browser.IO;

public partial class BrowserDataResolver : DataResolver
{
    private static readonly HttpClient _client = new() {
        BaseAddress = new Uri("https://localhost:5001/")
    };

    [JSImport("download", "tkmerger/downloader.js")]
    internal static partial void Download(string file, string bytesBase64);

    public static async Task Init()
    {
        await JSHost.ImportAsync("tkmerger/downloader.js", "./downloader.js");
    }

    public override async Task<Stream> GetResource(string resourceName)
    {
        return await _client.GetStreamAsync(resourceName);
    }

    public override void WriteBytes(string file, Span<byte> data)
    {
        Console.WriteLine(file);
        string base64 = Convert.ToBase64String(data);
        Download(Path.GetFileName(file), base64);
    }
}
