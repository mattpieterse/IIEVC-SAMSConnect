using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using SAMS.Connect.MVVM.ViewModels;
using SAMS.Connect.Core.Services;
using AppContext = SAMS.Connect.Core.Data.AppContext;

namespace SAMS.Connect;

[UsedImplicitly]
public sealed partial class App
{
    private void OnStartup(
        object sender,
        StartupEventArgs e
    ) {
        /* Attribution: MVVM Community Toolkit Dependency Injection
         * - https://platform.uno/docs/articles/external/uno.extensions/doc/Learn/Tutorials/DependencyInjection/HowTo-CommunityToolkit.html
         */
        Ioc.Default.ConfigureServices(
            new ServiceCollection()
                .AddSingleton<AppContext>()
                .AddSingleton<ShellViewModel>()
                .AddTransient<HomeViewModel>()
                .AddTransient<EventViewModel>()
                .AddTransient<TicketUpsertViewModel>()
                .AddSingleton<NavigationService>()
                .BuildServiceProvider()
        );
        
        new Shell {
            DataContext = Ioc.Default.GetService<ShellViewModel>()
        }.Show();
    }
}
