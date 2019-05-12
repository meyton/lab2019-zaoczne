using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using App10.Data;
using App10.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelperIOS))]
namespace App10.iOS
{
    public class FileHelperIOS : IFileHelper
    {
        public string GetLocalFilepath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}