using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using SAMS.Connect.Core.Services;
using SAMS.Connect.MVVM.ViewModels;
using SAMS.Connect.MVVM.Views;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace SAMS.Connect;

public sealed partial class Shell
{
#region Lifecycle

    public Shell() {
        var model = Ioc.Default.GetService<ShellViewModel>();
        DataContext = model;
        InitializeComponent();

        Ioc.Default.GetService<FluentNavigationService>()?.Initialize(NavigationView, model!);

        var toast = new SnackbarService();
        toast.SetSnackbarPresenter(Toaster);
        toast.GetSnackbarPresenter();
        toast.Show(
            title: "Welcome!",
            message: "ST100257002 proudly brings you... SAMS Connect!",
            appearance: ControlAppearance.Secondary,
            icon: new SymbolIcon(SymbolRegular.Emoji24),
            timeout: TimeSpan.FromSeconds(5D)
        );
    }

#endregion

#region Events

    private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e) =>
        NavigationView.Navigate(typeof(HomeView));

#endregion
}
