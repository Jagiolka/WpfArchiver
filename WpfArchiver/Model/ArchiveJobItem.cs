namespace WpfArchiver.Model;

using CommunityToolkit.Mvvm.ComponentModel;

public class ArchiveJobItem : ObservableObject
{
    private ArchiveSetting archiveSetting;
    private string name;
    private string sourcePath;
    private string targetPath;
    private string cronExpression;

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

    public string CronExpression
    {
        get => this.cronExpression;
        set => SetProperty(ref this.cronExpression, value);
    }

    public ArchiveSetting ArchiveSetting
    {
        get => this.archiveSetting;
        set => SetProperty(ref this.archiveSetting, value);
    }
}
