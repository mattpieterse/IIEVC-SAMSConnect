using CommunityToolkit.Mvvm.Input;
using JetBrains.Annotations;
using NavigationService = SAMS.Connect.Core.Services.NavigationService;

namespace SAMS.Connect.MVVM.ViewModels;

[UsedImplicitly]
public sealed partial class HomeViewModel(
    NavigationService navigationService
)
{
    /* ATTRIBUTION:
     * Commands and command button bindings were adapted from YouTube.
     * - https://www.youtube.com/watch?v=HDSRG7GvPbo
     * - ToskersCorner (https://www.youtube.com/@ToskersCorner)
     */
    [RelayCommand]
    private void NavigateToIssueView() => navigationService.NavigateTo<TicketUpsertViewModel>();


    [RelayCommand]
    private void NavigateToEventView() => navigationService.NavigateTo<EventViewModel>();
}
