namespace WpfArchiver;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                // views
                services.AddSingleton<MainWindow>(provider => new MainWindow
                {
                  DataContext = provider.GetRequiredService<MainWindowViewModel>()
                });
                services.AddTransient<ArchiveJobItemEditorView>();

                // viewModels
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<ArchiveJobItemEditorViewModel>();
              })
              .Build();
  }

  protected override async void OnStartup(StartupEventArgs e)
  {
    await _host.StartAsync();

    var mainWindow = _host.Services.GetRequiredService<MainWindow>();
    mainWindow.Show();
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
