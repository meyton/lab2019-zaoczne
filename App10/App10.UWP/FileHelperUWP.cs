using App10.Data;
using App10.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelperUWP))]
namespace App10.UWP
{
    public class FileHelperUWP : IFileHelper
    {
        public string GetLocalFilepath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
