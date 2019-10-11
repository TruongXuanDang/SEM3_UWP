using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MusicApplication.Entities;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicApplication.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private const string api = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/authentication";
        public Login()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var member = new LoginMember
            {
                email = Username.Text,
                password = Password.Password,
            };
            var httpClient = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> httpRequestMessage = httpClient.PostAsync(api, content);
            var jsonResult = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;

            var resMember = JsonConvert.DeserializeObject<LoginMember>(jsonResult);
            var token = resMember.token;

            var sampleFile = WriteIntoTxtFile(token);
            Debug.WriteLine(sampleFile.Path);
        }

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
    }
}
