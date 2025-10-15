using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SAMS.Connect.Core.Services;
using Wpf.Ui.Appearance;
using NavigationService = SAMS.Connect.Core.Services.NavigationService;

namespace SAMS.Connect.MVVM.ViewModels;

public sealed partial class ShellViewModel
    : ObservableObject
{
#region Internals

    [ObservableProperty]
    private bool _navigationBlocked;


    public NavigationService NavGraph { get; }


    public ShellViewModel(
        NavigationService navigationService
    ) {
        NavGraph = navigationService;
        NavGraph.NavigateTo<HomeViewModel>();
        NavigationBlocked = false;
    }

#endregion

#region ICommand

    [RelayCommand]
    private static void ToggleAppTheme() {
        ApplicationThemeManager.Apply(
            applicationTheme: ApplicationThemeManager.GetAppTheme() == ApplicationTheme.Dark
                ? ApplicationTheme.Light
                : ApplicationTheme.Dark
        );
    }


    [RelayCommand]
    private static async Task AboutAppDialog() {
        await DialogService.ShowSuccessAsync(
            heading: "About",
            message: "ST10257002"
        );
    }

#endregion
}
