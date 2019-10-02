using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWPMidTest.Entities;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Net.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPMidTest.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class ShowSongList : Page
    {
        private const string apiGetSongList = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/get-free-songs";
        public ObservableCollection<SongInfo> ListSongs = new ObservableCollection<SongInfo>();
        public ShowSongList()
        {
            this.InitializeComponent();

            var httpClient = new HttpClient();
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
