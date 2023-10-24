namespace WpfArchiver.ViewModel
{
  using CommunityToolkit.Mvvm.ComponentModel;
  using CommunityToolkit.Mvvm.Input;
  using System.Windows;
  using WpfArchiver.BusinessLogic;
  using WpfArchiver.Model;
  using WpfArchiver.View;
  using WpfArchiver.Ressources;
  using System.Collections.ObjectModel;
  using System.Linq;

  public partial class MainWindowViewModel : ObservableObject
  {
    [ObservableProperty]
    private ObservableCollection<ArchiveJobItem> archiveJobList = new ObservableCollection<ArchiveJobItem>();

    [ObservableProperty]
    private ArchiveJobItem? selectedArchiveJobItem;

    [ObservableProperty]
    private string listViewFilter = string.Empty;

    public MainWindowViewModel()
    {
      var loadedItemList = JobSaveManager.Load();
      foreach(var archiveJobItem in loadedItemList) 
      {
        this.ArchiveJobList.Add(archiveJobItem);
      }

      if (ArchiveJobList.Count > 0)
      {
        this.SelectedArchiveJobItem = ArchiveJobList[0];
      }
    }

    [RelayCommand]
    private void AddNewArchiveJobItem()
    {
      ArchiveJobItem newArchiveJobItem = new();
      this.OpenArchiveJobItemEditor(newArchiveJobItem, ConstantTexts.ArchiveJobItemEditorTitleAddNew);
      this.ArchiveJobList.Add(newArchiveJobItem);
    }

    [RelayCommand(CanExecute = nameof(CanEditArchiveJobItem))]
    private void EditArchiveJobItem(ArchiveJobItem archiveJobItem)
    {
      this.OpenArchiveJobItemEditor(archiveJobItem, ConstantTexts.ArchiveJobItemEditorTitleEdit);
    }

    [RelayCommand]
    private void DeleteArchiveJobItem(ArchiveJobItem archiveJobItem)
    {
      if (archiveJobItem != null) 
      {
        this.ArchiveJobList.Remove(archiveJobItem);
      }
    }

    [RelayCommand]
    private void SaveArchiveJobList()
    {
      if (this.ArchiveJobList is not null)
      {
        JobSaveManager.Save(this.ArchiveJobList.ToList());
      }
    }

    [RelayCommand]
    private void CloseApplication()
    {
      var result = MessageBox.Show("Möchten Sie die Anwendung beenden?", "Beenden", MessageBoxButton.YesNo, MessageBoxImage.Question);

      if (result == MessageBoxResult.Yes)
      {
        Application.Current.MainWindow.Close();
      }
    }

    private bool CanEditArchiveJobItem()
    {
      return this.SelectedArchiveJobItem is not null;
    }

    private void OpenArchiveJobItemEditor(ArchiveJobItem archiveJobItem, string title) 
    {
      if (archiveJobItem != null)
      {
        ArchiveJobItemEditorViewModel viewModel = new();
        viewModel.ArchiveJobItem = archiveJobItem;
        ArchiveJobItemEditorView view = new()
        {
          Title = title,
          DataContext = viewModel,
        };

        view.ShowDialog();

        if(viewModel.IsSaveExit) 
        {
          archiveJobItem = viewModel.ArchiveJobItem;
        }
      }
    }
  }
}
