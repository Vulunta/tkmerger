using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    private async Task Save()
    {
        try {
            using BlockResource resource = await BlockResource.Open(Target);
            string output = Path.Combine("D:", "bin", "BlockResource", Target);
            DataResolver.Shared.WriteBytes(output, resource.GetData());
        }
        catch (Exception ex) {
            Console.WriteLine(ex);
        }
    }
}
