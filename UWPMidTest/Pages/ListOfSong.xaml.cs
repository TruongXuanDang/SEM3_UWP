using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UWPMidTest.Entities;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Windows.Data.Json;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPMidTest.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListOfSong : Page
    {
        private const string apiCreateNewSong = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/post-free";
        public ListOfSong()
        {
            this.InitializeComponent();

        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var songInfo = new SongInfo
            {
                name = Name.Text,
                description = Description.Text,
                singer = Singer.Text,
                author = Author.Text,
                thumbnail = Thumbnail.Text,
                link = Link.Text,
            };
            if (ValidateInputData(songInfo) == true)
            {
                var httpClient = new HttpClient();
                HttpContent content = new StringContent(JsonConvert.SerializeObject(songInfo), Encoding.UTF8, "application/json");
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

        private bool ValidateInputData(SongInfo songInfo)
        {
            if (songInfo.name != "" && songInfo.thumbnail != "" && songInfo.link != "" &&
                songInfo.name.Length <= 50 && songInfo.link.EndsWith(".mp3"))
            {
                return true;
            }

            return false;
        }
    }
}
