using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using JetBrains.Annotations;

namespace SAMS.Connect.Core.Services;

/* ATTRIBUTION:
 * SPA navigation using the service and DataTemplates were heavily adapted from GitHub.
 * - https://github.com/vain0x/wpf-navigation-example
 * - vain0x (https://github.com/vain0x)
 */
public sealed partial class NavigationService
    : ObservableObject
{
#region Internals

    [ObservableProperty]
    private object? _currentViewModel;


    [UsedImplicitly]
    public void NavigateTo<TViewModel>() where TViewModel : class {
        CurrentViewModel = Ioc.Default.GetService<TViewModel>();
    }

#endregion
}
