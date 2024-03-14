using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;
using TkMerger.Core.IO;

namespace TkMerger.ViewModels;

public enum Mode
{
    BYML,
    MSBT,
    SARC
}

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private string _target = string.Empty;

    [ObservableProperty]
    private Mode _mode = Mode.BYML;

    [ObservableProperty]
    private Mode[] _modes = Enum.GetValues<Mode>();

    [RelayCommand]
    private void Save()
    {
        using BlockResource resource = new(Target);

        string output = Path.Combine("D:", "bin", "BlockResource", Target);
        if (Path.GetDirectoryName(output) is string folder) {
            Directory.CreateDirectory(folder);
        }

        using FileStream fs = File.Create(output);
        fs.Write(resource.Data);
    }
}
