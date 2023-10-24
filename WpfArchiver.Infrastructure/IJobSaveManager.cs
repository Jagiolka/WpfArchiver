namespace WpfArchiver.Model;

using WpfArchiver.Model;

public interface IJobSaveManager
{
  public void Save(List<ArchiveJobItem> archiveJobItems);

  public IList<ArchiveJobItem> Load();
}
