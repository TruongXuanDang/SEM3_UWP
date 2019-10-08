using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using MusicApplication.Entities;
using Newtonsoft.Json;
using Windows.Media.Core;
using Windows.Media.Playback;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicApplication.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SongList : Page
    {
        private const string apiGetSongList = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/get-free-songs";
        public ObservableCollection<Song> ListSongs = new ObservableCollection<Song>();

        public SongList()
        {
            this.InitializeComponent();

            var httpClient = new HttpClient();
            Task<HttpResponseMessage> httpRequestMessageToGetSongList = httpClient.GetAsync(apiGetSongList);
            var jsonResultToGetSongList = httpRequestMessageToGetSongList.Result.Content.ReadAsStringAsync().Result;
            ObservableCollection<Song> listSong = JsonConvert.DeserializeObject<ObservableCollection<Song>>(jsonResultToGetSongList);
            foreach (Song song in listSong)
            {
                ListSongs.Add(song);
            }
        }


        private void ListOfSongs_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListView view = (ListView) sender;
                Song clicked = view.SelectedItem as Song;
                MediaPlayer player = new MediaPlayer();
                string path = clicked.link;
                Uri outUri;

                Uri uri = new Uri(path);
                MediaSource mediaSource = MediaSource.CreateFromUri(uri);
                mediaPlayer.Source = mediaSource;
                mediaPlayer.MediaPlayer.Play();

            }
            catch (Exception exception)
            {
                
            }
        }
    }
}
