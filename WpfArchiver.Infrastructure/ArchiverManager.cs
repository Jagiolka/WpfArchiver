namespace WpfArchiver.Infrastructure;

using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

public static class ArchiverManager
{ 
  public static async Task CreateZipFileFromDirectoryAsync(string sourceDirectoryName, string destinationArchiveFileName)
  {
    using (ZipArchive archive = ZipFile.Open(destinationArchiveFileName, ZipArchiveMode.Update))
    {
      foreach (string file in Directory.EnumerateFiles(sourceDirectoryName))
      {
        archive.CreateEntryFromFile(file, Path.GetFileName(file));
      }
    }

    await Task.CompletedTask;
  }
}