using System;
using System.IO;
using Xamarin.Forms;
using ColorItAll.iOS;

[assembly: Dependency(typeof(SaveAndLoad))]
namespace ColorItAll.iOS
{
    public class SaveAndLoad : ISaveAndLoad
    {
        public static string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }

        public void SaveText(string filename, string text)
        {
            File.WriteAllText(GetLocalFilePath(filename), text);
        }

        public string LoadText(string filename)
        {
            return File.ReadAllText(GetLocalFilePath(filename));
        }
    }
}