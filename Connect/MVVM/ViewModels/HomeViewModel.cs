using CommunityToolkit.Mvvm.Input;
using JetBrains.Annotations;
using SAMS.Connect.Core.Services;
using SAMS.Connect.MVVM.Views;
using NavigationService = SAMS.Connect.Core.Services.NavigationService;

namespace SAMS.Connect.MVVM.ViewModels;

[UsedImplicitly]
public sealed partial class HomeViewModel(
    FluentNavigationService fluentNavigationService
)
{
#region ICommand

    /* ATTRIBUTION:
     * Commands and command button bindings were adapted from YouTube.
     * - https://www.youtube.com/watch?v=HDSRG7GvPbo
     * - ToskersCorner (https://www.youtube.com/@ToskersCorner)
     */
    [RelayCommand]
    private void NavigateToIssueView() => fluentNavigationService.NavigateToAsync<TicketUpsertView>();


    [RelayCommand]
    private void NavigateToEventView() => fluentNavigationService.NavigateToAsync<EventView>();

#endregion
}
