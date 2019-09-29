using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using T1807EHelloUWP.Entities;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;

namespace T1807EHelloUWP.Services
{
    class MemberServiceImp:IMemberService
    {
        //Ham login, Register, GetInfo
        public async void FormLogin(Member member, string api)
        {
            var response = GetObjectResultFromHTTPPostRequest<Member>(member, api);
            var token = response.token;

            var sampleFile = WriteIntoTXTFile(token);
            Debug.WriteLine(sampleFile.Path);
        }

        public void FormRegister(User user, string api)
        {
            var resMember = GetObjectResultFromHTTPPostRequest<User>(user, api);
            Debug.WriteLine(resMember.email);
        }

        public string FormGetInfo(string api)
        {
            var getResult = GetObjectResultFromHTTPGetRequest(api);

            return getResult;
        }

        public T GetObjectResultFromHTTPPostRequest<T>(T test, string api)
        {
            var httpClient = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(test), Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> httpRequestMessage = httpClient.PostAsync(api, content);
            var jsonResult = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;

            var resMember = JsonConvert.DeserializeObject<T>(jsonResult);
            return resMember;
        }

        public string GetObjectResultFromHTTPGetRequest(string api)
        {
            var httpClient = new HttpClient();
            Task<HttpResponseMessage> httpRequestMessage = httpClient.GetAsync(api);
            var jsonResult = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;

            return jsonResult;
        }

        public StorageFile WriteIntoTXTFile(string token)
        {
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.StorageFile sampleFile =
                storageFolder.CreateFileAsync("sample.txt",
                    Windows.Storage.CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();

            Windows.Storage.FileIO.WriteTextAsync(sampleFile, token).GetAwaiter().GetResult();
            return sampleFile;
        }

        public string ReadFromTXTFile()
        {
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                storageFolder.GetFileAsync("sample.txt").GetAwaiter().GetResult();

            string text = Windows.Storage.FileIO.ReadTextAsync(sampleFile).GetAwaiter().GetResult();
            return text;
        }
    }
}
