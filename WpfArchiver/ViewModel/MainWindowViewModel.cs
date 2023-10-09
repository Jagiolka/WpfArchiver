namespace WpfArchiver.ViewModel
{
  using CommunityToolkit.Mvvm.ComponentModel;
  using Microsoft.Extensions.Logging;

  public class MainWindowViewModel : ObservableObject
  {
    private readonly ILogger<MainWindowViewModel> logger;

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger) 
    {
      this.logger = logger;
    }
  }
}
