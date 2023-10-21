namespace WpfArchiver.Infrastructure.BackgroundJobs;

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfArchiver.Infrastructure.BusinessObjects;

public class ArchiveJobItem : ObservableObject
{
  private IList<ArchiveSetting> archiveSettings = new List<ArchiveSetting>();
  private bool isActiveArchivJob;
  private string name;
  private string sourcePath;
  private string targetPath;  
  private DateTime nextArchiveJobExecutionDateTime;

  public bool IsActiveArchivJob
  {
    get => this.isActiveArchivJob;
    set => SetProperty(ref this.isActiveArchivJob, value);
  }

  public string Name
  {
    get => this.name;
    set => SetProperty(ref this.name, value);
  }

  public string SourcePath
  {
    get => this.sourcePath;
    set => SetProperty(ref this.sourcePath, value);
  }

  public string TargetPath
  {
    get => this.targetPath;
    set => SetProperty(ref this.targetPath, value);
  }

  public IList<ArchiveSetting> ArchiveSettings
  {
    get => this.archiveSettings;
    set => SetProperty(ref this.archiveSettings, value);
  }

  public DateTime NextArchiveJobExecutionDateTime
  {
    get => this.nextArchiveJobExecutionDateTime;
    set => SetProperty(ref this.nextArchiveJobExecutionDateTime, value);
  }

  public string GetArchiveSettings() 
  {
    string ttt = string.Empty;
    foreach (var archiveSetting in ArchiveSettings) 
    {
      ttt += archiveSetting.ToString();
    }

    return ttt;
  }

  // < ProgressBar Height="2" Width="Auto" Value="{Binding WorkingProgressInPercent}" />
}
