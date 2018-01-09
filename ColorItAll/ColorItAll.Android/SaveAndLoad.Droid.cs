using System.IO;
using Xamarin.Forms;
using ColorItAll.Droid;
using ColorItAll.Interface;

[assembly: Dependency(typeof(SaveAndLoad))]
namespace ColorItAll.Droid
{
    public class SaveAndLoad : ISaveAndLoad
    {
        public static string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
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