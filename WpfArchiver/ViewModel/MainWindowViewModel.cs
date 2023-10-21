using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfArchiver.Infrastructure.BackgroundJobs;
using static Quartz.Logging.OperationName;

namespace WpfArchiver.ViewModel
{
  public partial class MainWindowViewModel : ObservableObject
  {
    [ObservableProperty]
    private IList<ArchiveJobItem> archiveJobList = new List<ArchiveJobItem>();

    [ObservableProperty]
    private ArchiveJobItem selectedArchiveJobItem = new ArchiveJobItem();

    public MainWindowViewModel()
    {
      ArchiveJobList.Add(new ArchiveJobItem { Name = "TestJob1", IsActiveArchivJob = true, ArchiveType = "Zip", SourcePath = @"E:\tmp\5", TargetPath = @"E:\tmp\", NextArchiveJobExecutionDateTime = System.DateTime.Now.AddHours(1) });
      ArchiveJobList.Add(new ArchiveJobItem { Name = "TestJob2", IsActiveArchivJob = false, ArchiveType = "7Zip", SourcePath = @"E:\tmp\6", TargetPath = @"E:\tamp\", NextArchiveJobExecutionDateTime = System.DateTime.Now.AddDays(1) });


      if(ArchiveJobList.Count > 0) 
      {
        this.SelectedArchiveJobItem = ArchiveJobList[0];
      }
    }

    [RelayCommand]
    private void ToggleIsActiveArchiveJob(ArchiveJobItem archiveJobItem)
    {
      if (archiveJobItem != null)
      {
        archiveJobItem.IsActiveArchivJob = !archiveJobItem.IsActiveArchivJob;
      }

      this.OnPropertyChanged();
    }
  }
}
