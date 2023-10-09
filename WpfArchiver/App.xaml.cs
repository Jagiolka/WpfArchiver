namespace WpfArchiver
{
  using Microsoft.Extensions.DependencyInjection;
  using System;
  using System.Windows;
  using WpfArchiver.ViewModel;
  using Microsoft.Extensions.Configuration;
  using System.IO;
  using Serilog;

  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private readonly ServiceProvider _serviceProvider;

    public App() 
    {
      var configuration = this.GetConfigurationBuilder().Build();

      IServiceCollection services = new ServiceCollection();      
      this.SetLoggerSerilog(services, configuration);
      this.RegisterViewModels(services);
      this.RegisterViews(services);

      _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
      var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
      mainWindow.Show();
      base.OnStartup(e);
    }

    private IConfigurationBuilder GetConfigurationBuilder() 
    {
      return new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true);
    }

    private void SetLoggerSerilog(IServiceCollection services, IConfiguration configuration)
    {
      var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

      services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(logger, true));
    }

    private void RegisterViews(IServiceCollection services)
    {
      services.AddSingleton<MainWindow>(provider => new MainWindow
      {
        DataContext = provider.GetRequiredService<MainWindowViewModel>()
      });
    }

    private void RegisterViewModels(IServiceCollection services)
    {
      services.AddSingleton<MainWindowViewModel>();
    }
  }
}
