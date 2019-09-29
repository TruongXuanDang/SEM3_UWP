using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace T1807EHelloUWP.Services
{
    interface IFileService
    {
        string GetUploadURL(string api);

        void UploadFile(string url, string paramName, string contentType, StorageFile photo, Image image,
            TextBox textBox);
    }
}
