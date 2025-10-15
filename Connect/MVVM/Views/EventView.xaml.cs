using CommunityToolkit.Mvvm.DependencyInjection;
using SAMS.Connect.MVVM.ViewModels;

namespace SAMS.Connect.MVVM.Views;

public sealed partial class EventView
{
    public EventView() {
        DataContext = Ioc.Default.GetService<EventViewModel>();
        InitializeComponent();
    }
}
