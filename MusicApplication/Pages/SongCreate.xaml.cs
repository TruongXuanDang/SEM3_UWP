using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MusicApplication.Entities;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicApplication.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SongCreate : Page
    {
        private const string apiCreateNewSong = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs";
        public SongCreate()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }
        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var song = new Song
            {
                name = Name.Text,
                description = Description.Text,
                singer = Singer.Text,
                author = Author.Text,
                thumbnail = Thumbnail.Text,
                link = Link.Text,
            };
            if (ValidateInputData(song) == true)
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", ReadFromTXTFile());
                HttpContent content = new StringContent(JsonConvert.SerializeObject(song), Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> httpRequestMessageToCreateNewSong = httpClient.PostAsync(apiCreateNewSong, content);
                var jsonResultToCreateNewSong = httpRequestMessageToCreateNewSong.Result.Content.ReadAsStringAsync().Result;
                MessageDialog dialog = new MessageDialog("Succeeded");
                await dialog.ShowAsync();
            }
            else
            {
                MessageDialog dialog = new MessageDialog("Name, thumbnail, link need to be validated");
                await dialog.ShowAsync();
            }

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

        private bool ValidateInputData(Song songInfo)
        {
            if (songInfo.name != "" && songInfo.thumbnail != "" && songInfo.link != "" &&
                songInfo.name.Length <= 50 && songInfo.link.EndsWith(".mp3"))
            {
                return true;
            }

            //return false;
            return true;
        }
    }
}
