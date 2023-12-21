namespace WpfArchiver.ViewModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WpfArchiver.BusinessLogic;
using WpfArchiver.Model;
using WpfArchiver.View;
using WpfArchiver.Ressources;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using Microsoft.Extensions.DependencyInjection;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<ArchiveJobItem> archiveJobList = new ObservableCollection<ArchiveJobItem>();

    [ObservableProperty]
    private ArchiveJobItem? selectedArchiveJobItem;

    [ObservableProperty]
    private string listViewFilter = string.Empty;

    private readonly IServiceProvider serviceProvider;

    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        this.LoadAndSetArchiveJobList();
        this.SetSelectedArchiveJobItem(this.ArchiveJobList);
        //var loadedItemList = JobSaveManager.Load();
        //foreach (var archiveJobItem in loadedItemList)
        //{
        //    this.ArchiveJobList.Add(archiveJobItem);
        //}

        //if (ArchiveJobList.Count > 0)
        //{
        //    this.SelectedArchiveJobItem = ArchiveJobList[0];
        //}

        this.serviceProvider = serviceProvider;
    }

    [RelayCommand]
    private void AddNewArchiveJobItem()
    {
        ArchiveJobItem newArchiveJobItem = new()
        { 
            CronExpression = "0 0 * ? * *"
        };
        var dialogResult = this.OpenArchiveJobItemEditor(newArchiveJobItem, ConstantTexts.ArchiveJobItemEditorTitleAddNew);

        if (dialogResult)
        {
            this.ArchiveJobList.Add(newArchiveJobItem);
        }
    }

    // [RelayCommand(CanExecute = nameof(CanEditArchiveJobItem))]
    [RelayCommand]
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

    private void LoadAndSetArchiveJobList()
    {
        var loadedItemList = JobSaveManager.Load();
        foreach (var archiveJobItem in loadedItemList)
        {
            this.ArchiveJobList.Add(archiveJobItem);
        }
    }

    private void SetSelectedArchiveJobItem(ObservableCollection<ArchiveJobItem> archiveJobList)
    {
        if (ArchiveJobList.Count > 0)
        {
            this.SelectedArchiveJobItem = ArchiveJobList[0];
        }
    }

    private bool OpenArchiveJobItemEditor(ArchiveJobItem archiveJobItem, string title)
    {
        bool dialogResult = false;
        if (archiveJobItem != null)
        {
            ArchiveJobItemEditorView editorView = this.serviceProvider.GetRequiredService<ArchiveJobItemEditorView>();
            editorView.Title = title;

            var editorViewModelDataContext = (ArchiveJobItemEditorViewModel)editorView.DataContext;
            editorViewModelDataContext.ArchiveJobItem = archiveJobItem;
            editorViewModelDataContext.CronExpressionString = archiveJobItem.CronExpression;

            dialogResult = editorView.ShowDialog() ?? false;
        }

        return dialogResult;
    }
}
