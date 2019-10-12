using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using MusicApplication.Constant;

namespace MusicApplication.Services
{
    class FileService:IFileService
    {
        public StorageFile WriteIntoTxtFile(string token)
        {
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.StorageFile sampleFile =
                storageFolder.CreateFileAsync("sample.txt",
                    Windows.Storage.CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();

            Windows.Storage.FileIO.WriteTextAsync(sampleFile, token).GetAwaiter().GetResult();
            return sampleFile;
        }

        public string ReadFromTxtFile()
        {
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                storageFolder.GetFileAsync("sample.txt").GetAwaiter().GetResult();

            string text = Windows.Storage.FileIO.ReadTextAsync(sampleFile).GetAwaiter().GetResult();
            return text;
        }

        public string GetLinkToUploadFile()
        {
            HttpClient httpClient = new HttpClient();
            var httpRequestMessage = httpClient.GetAsync(ApiUrl.API_UPLOAD_IMAGE_URL);
            var uploadURL = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            return uploadURL;
        }
    }
}
