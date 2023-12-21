namespace WpfArchiver.ViewModel;

using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfArchiver.Model;
using Ookii.Dialogs.Wpf;
using Quartz;
using System;
using Quartz.Util;
using System.IO;

public partial class ArchiveJobItemEditorViewModel : ObservableObject
{
    private readonly string dateTimeFormat = "dd.MM.yyy - HH:mm:ss";

    [ObservableProperty]
    private ArchiveJobItem archiveJobItem;

    private string cronExpressionString;
    public string CronExpressionString
    {
        get => this.cronExpressionString;
        set
        {
            SetProperty(ref this.cronExpressionString, value);
            if (!CronExpression.IsValidExpression(this.cronExpressionString))
            {
                return;
            }
            ArchiveJobItem.CronExpression = value;
            this.SetCronExpressionNextDates(this.cronExpressionString);
        }
    }

    private void SetCronExpressionNextDates(string cronExpressionString)
    {
        var cronExpression = new CronExpression(cronExpressionString);

        DateTimeOffset? dateTimeOffset = DateTimeOffset.Now;

        this.CronExpressionNextTime01 = this.GetAndSetNextValidTimeAfter(cronExpression, ref dateTimeOffset);
        this.CronExpressionNextTime02 = this.GetAndSetNextValidTimeAfter(cronExpression, ref dateTimeOffset);
        this.CronExpressionNextTime03 = this.GetAndSetNextValidTimeAfter(cronExpression, ref dateTimeOffset);
    }

    private string GetAndSetNextValidTimeAfter(CronExpression cronExpression, ref DateTimeOffset? dateTimeOffset)
    {
        if (dateTimeOffset is null)
        {
            return "";
        }

        string nextValidDateString = cronExpression.GetNextValidTimeAfter(dateTimeOffset.Value).Value.ToString(dateTimeFormat);
        dateTimeOffset = cronExpression.GetTimeAfter(dateTimeOffset.Value);

        return nextValidDateString;
    }

    private string cronExpressionNextTime01;
    public string CronExpressionNextTime01
    {
        get => this.cronExpressionNextTime01;
        set => SetProperty(ref this.cronExpressionNextTime01, value);
    }

    private string cronExpressionNextTime02;
    public string CronExpressionNextTime02
    {
        get => this.cronExpressionNextTime02;
        set => SetProperty(ref this.cronExpressionNextTime02, value);
    }

    private string cronExpressionNextTime03;
    public string CronExpressionNextTime03
    {
        get => this.cronExpressionNextTime03;
        set => SetProperty(ref this.cronExpressionNextTime03, value);
    }

    public bool IsSaveExit { get; set; }

    [RelayCommand]
    private void SaveExit(Window window)
    {
        if (this.IsEveryPropertyValid())
        {
            this.ArchiveJobItem.CronExpression = this.CronExpressionString;
            window.DialogResult = true;
            this.CloseWindow(window);
        }
    }

    [RelayCommand]
    private void CancelExit(Window window)
    {
        window.DialogResult = false;
        this.CloseWindow(window);
    }

    [RelayCommand]
    private void SelectSourceFolder()
    {
        this.ArchiveJobItem.SourcePath = this.GetPathFromFolderDialog();
    }

    [RelayCommand]
    private void SelectTargetFolder()
    {
        this.ArchiveJobItem.TargetPath = this.GetPathFromFolderDialog();
    }

    private string GetPathFromFolderDialog()
    {
        var folderBrowserDialog = new VistaFolderBrowserDialog();
        var result = folderBrowserDialog.ShowDialog();

        if (result.GetValueOrDefault())
        {
            return folderBrowserDialog.SelectedPath;
        }

        return string.Empty;
    }

    private bool IsEveryPropertyValid()
    {
        return
          !string.IsNullOrWhiteSpace(this.ArchiveJobItem.Name) &&
          Directory.Exists(this.ArchiveJobItem.TargetPath) &&
          Directory.Exists(this.ArchiveJobItem.SourcePath) &&
          !string.IsNullOrWhiteSpace(this.ArchiveJobItem.CronExpression) &&
          CronExpression.IsValidExpression(this.ArchiveJobItem.CronExpression);
    }

    private void CloseWindow(Window window)
    {
        if (window != null)
        {
            window.Close();
        }
    }
}
