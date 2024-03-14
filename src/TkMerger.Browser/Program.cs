using Avalonia;
using Avalonia.Browser;
using System.Runtime.Versioning;
using TkMerger;
using TkMerger.Browser.IO;
using TkMerger.Core.IO;

[assembly: SupportedOSPlatform("browser")]

internal partial class Program
{
    private static async Task Main()
    {
        await BrowserDataResolver.Init();
        DataResolver.Shared = new BrowserDataResolver();

        await BlockResourceMetadata.Load();

        await BuildAvaloniaApp()
            .WithInterFont()
            .StartBrowserAppAsync("out");
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}
