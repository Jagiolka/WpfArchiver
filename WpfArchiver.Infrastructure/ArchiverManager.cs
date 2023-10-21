namespace ConsoleApp1
{
  using System.IO;
  using System.IO.Compression;
  using System.Threading.Tasks;

  public static class ArchiverManager
  { 
    public static async Task CreateZipFileFromDirectoryAsync(string sourceDirectoryName, string destinationArchiveFileName)
    {
      // Erstellen Sie ein ZipArchive im Update-Modus
      using (ZipArchive archive = ZipFile.Open(destinationArchiveFileName, ZipArchiveMode.Update))
      {
        // Durchlaufen Sie alle Dateien im Quellverzeichnis
        foreach (string file in Directory.EnumerateFiles(sourceDirectoryName))
        {
          // Erstellen Sie eine neue ZipArchiveEntry für jede Datei und fügen Sie sie dem ZipArchive hinzu
          archive.CreateEntryFromFile(file, Path.GetFileName(file));
        }
      }
      // Warten Sie auf die Fertigstellung der asynchronen Ausführung
      await Task.CompletedTask;
    }
  }
}