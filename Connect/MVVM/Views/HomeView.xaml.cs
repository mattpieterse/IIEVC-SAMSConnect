using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using SAMS.Connect.MVVM.ViewModels;

namespace SAMS.Connect.MVVM.Views;

public sealed partial class HomeView
{
    public HomeView() {
        DataContext = Ioc.Default.GetService<HomeViewModel>();
        InitializeComponent();
    }
}
