namespace WpfArchiver
{
  using Microsoft.Extensions.DependencyInjection;
  using System.Windows;
  using WpfArchiver.ViewModel;

  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private readonly ServiceProvider _serviceProvider;

    public App() 
    {
      IServiceCollection services = new ServiceCollection();

      // views
      services.AddSingleton<MainWindow>(provider => new MainWindow
      {
        DataContext = provider.GetRequiredService<MainWindowViewModel>()
      });

      // viewModels
      services.AddSingleton<MainWindowViewModel>();

      _serviceProvider = services.BuildServiceProvider();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
      var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
      mainWindow.Show();
      base.OnStartup(e);
    }
  }
}
