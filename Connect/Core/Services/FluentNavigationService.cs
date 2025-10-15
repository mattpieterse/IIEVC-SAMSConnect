using SAMS.Connect.MVVM.ViewModels;
using SAMS.Connect.MVVM.Views;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace SAMS.Connect.Core.Services;

public class FluentNavigationService
{
#region Variables

    private NavigationView? _navigationView;
    private ShellViewModel? _shellViewModel;

#endregion


#region Lifecycle

    public void Initialize(
        NavigationView navigationView,
        ShellViewModel shellViewModel
    ) {
        _shellViewModel = shellViewModel;
        _navigationView = navigationView;
    }

#endregion


#region Functions

    public Task NavigateToAsync<TPage>(object? parameter = null) where TPage : class {
        if (_navigationView == null) throw new InvalidOperationException("NavigationView not initialized");
        var pageType = typeof(TPage);

        if (_shellViewModel is not null) {
            _shellViewModel.NavigationBlocked = typeof(TPage) == typeof(TicketUpsertView);
        }

        _navigationView.Navigate(pageType, parameter);
        return Task.CompletedTask;
    }


    public Task GoBackAsync() {
        _navigationView?.GoBack();
        return Task.CompletedTask;
    }

#endregion
}
