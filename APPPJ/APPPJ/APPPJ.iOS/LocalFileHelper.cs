using System;
using Xamarin.Forms;
using APPPJ.Helpers;
using System.IO;
using APPPJ.iOS;

[assembly: Dependency(typeof(LocalFileHelper))]
namespace APPPJ.iOS
{
    public class LocalFileHelper : ILocalFileHelper
    {
        public string GetLocalFilePath(string FileName)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, FileName);
        }
    }
}
