using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JetBrains.Annotations;
using SAMS.Connect.Core.Services;

namespace SAMS.Connect.MVVM.ViewModels;

[UsedImplicitly]
public sealed partial class EventViewModel(
    NavigationService navigationService
)
    : ObservableObject
{
#region ICommands

    [RelayCommand]
    private void NavigateHome() => navigationService.NavigateTo<HomeViewModel>();

#endregion
}
