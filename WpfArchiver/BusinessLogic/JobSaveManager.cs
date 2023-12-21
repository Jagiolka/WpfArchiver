namespace WpfArchiver.BusinessLogic;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using WpfArchiver.Model;

public static class JobSaveManager
{
    private const string FileName = "archiveJobList.json";

    public static List<ArchiveJobItem> Load()
    {
        List<ArchiveJobItem> archiveJobItems = new List<ArchiveJobItem>();
        if (File.Exists(FileName))
        {
            string json = File.ReadAllText(FileName);
            archiveJobItems = JsonConvert.DeserializeObject<List<ArchiveJobItem>>(json) ?? new List<ArchiveJobItem>();
        }

        return archiveJobItems;
    }

    public static void Save(List<ArchiveJobItem> archiveJobItems)
    {
        string json = JsonConvert.SerializeObject(archiveJobItems, Formatting.Indented);
        File.WriteAllText(FileName, json);
    }
}
