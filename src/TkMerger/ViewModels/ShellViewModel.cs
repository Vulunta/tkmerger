using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TkMerger.Core.IO;

namespace TkMerger.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private string _target = string.Empty;

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
