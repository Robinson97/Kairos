using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kairos.Core.UWP.FileAccess
{
    public static class FileAccessManager
    {
        public static async Task WriteFileAsync(string path, string content)
        {
            Windows.Storage.StorageFolder storageFolder =
                    Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.StorageFile sampleFile =
                await storageFolder.CreateFileAsync(path, Windows.Storage.CreationCollisionOption.ReplaceExisting);
        }
    }
}
