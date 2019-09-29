using System;
using System.IO;
using APPPJ.Helpers;
using APPPJ.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalFileHelper))]
namespace APPPJ.Droid
{
    public class LocalFileHelper : ILocalFileHelper
    {
        public string GetLocalFilePath(string FileName)
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DBREC.db3");
        }
    }
}
