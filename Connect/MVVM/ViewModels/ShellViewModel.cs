using CommunityToolkit.Mvvm.ComponentModel;
using NavigationService = SAMS.Connect.Core.Services.NavigationService;

namespace SAMS.Connect.MVVM.ViewModels;

public sealed class ShellViewModel
    : ObservableObject
{
#region Internals

    public NavigationService NavGraph { get; }


    public ShellViewModel(
        NavigationService navigationService
    ) {
        NavGraph = navigationService;
        NavGraph.NavigateTo<HomeViewModel>();
    }

#endregion
}
