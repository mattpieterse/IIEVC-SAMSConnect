using CommunityToolkit.Mvvm.DependencyInjection;
using SAMS.Connect.MVVM.ViewModels;

namespace SAMS.Connect.MVVM.Views;

public sealed partial class TicketUpsertView
{
    public TicketUpsertView() {
        DataContext = Ioc.Default.GetService<TicketUpsertViewModel>();
        InitializeComponent();
    }
}
