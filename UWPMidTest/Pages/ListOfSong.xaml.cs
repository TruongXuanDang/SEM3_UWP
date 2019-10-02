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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPMidTest.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListOfSong : Page
    {
        private const string apiCreateNewSong = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/post-free";
        private const string apiGetSongList = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/get-free-songs";
        public ObservableCollection<SongInfo> ListSongs = new ObservableCollection<SongInfo>();
        public ListOfSong()
        {
            this.InitializeComponent();

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
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
            var httpClient = new HttpClient();
                HttpContent content = new StringContent(JsonConvert.SerializeObject(songInfo), Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> httpRequestMessageToCreateNewSong = httpClient.PostAsync(apiCreateNewSong, content);
            var jsonResultToCreateNewSong = httpRequestMessageToCreateNewSong.Result.Content.ReadAsStringAsync().Result;

            //var listOfSong = new ObservableCollection<SongInfo>();
            //listOfSong.Add(songInfo);
            Task<HttpResponseMessage> httpRequestMessageToGetSongList = httpClient.GetAsync(apiGetSongList);
            var jsonResultToGetSongList = httpRequestMessageToGetSongList.Result.Content.ReadAsStringAsync().Result;
            ObservableCollection<SongInfo> listSong = JsonConvert.DeserializeObject<ObservableCollection<SongInfo>>(jsonResultToGetSongList);
            foreach (SongInfo item in listSong)
            {
                ListSongs.Add(item);
            }
        }
    }
}
