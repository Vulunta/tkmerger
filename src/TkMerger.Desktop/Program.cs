using Avalonia;
using TkMerger.Core.IO;
using TkMerger.Desktop.IO;

namespace TkMerger.Desktop;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        DataResolver.Shared = new DesktopDataResolver();

        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont();
}
