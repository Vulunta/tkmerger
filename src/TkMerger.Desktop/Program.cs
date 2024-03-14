﻿using Avalonia;

namespace TkMerger.Desktop;

class Program
{
    [STAThread]
    public static void Main(string[] args)
        => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont();
    }
}
