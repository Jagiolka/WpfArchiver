namespace WpfArchiver;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz.Impl.AdoJobStore.Common;
using System;
using System.Windows;
using WpfArchiver.View;
using WpfArchiver.ViewModel;

public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // viewModels
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<ArchiveJobItemEditorViewModel>();

                // views
                services.AddSingleton<MainWindow>(provider => new MainWindow
                {
                    DataContext = provider.GetRequiredService<MainWindowViewModel>()
                });
                services.AddTransient<ArchiveJobItemEditorView>(provider => new ArchiveJobItemEditorView 
                {
                    DataContext = provider.GetRequiredService<ArchiveJobItemEditorViewModel>()
                });
            })
        .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            await _host.StopAsync(TimeSpan.FromSeconds(5));
        }
        base.OnExit(e);
    }
}
