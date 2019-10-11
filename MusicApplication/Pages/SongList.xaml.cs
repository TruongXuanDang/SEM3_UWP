using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using MusicApplication.Entities;
using Newtonsoft.Json;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;

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
        public bool PlayingStatus = false;
        public int CurrentSongIndex = 0;

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
                string path = clicked.link;

                Uri uri = new Uri(path);
                MediaSource mediaSource = MediaSource.CreateFromUri(uri);
                mediaPlayer.Source = mediaSource;
                mediaPlayer.MediaPlayer.Play();
                
                PlayingStatus = true;
                StatusButton.Icon = new SymbolIcon(Symbol.Pause);
                CurrentSongIndex = ListSongs.IndexOf(clicked);
            }
            catch (Exception exception)
            {
                
            }
        }

        private void PreviousButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentSongIndex <= ListSongs.Count - 1 && 0 < CurrentSongIndex)
            {
                CurrentSongIndex--;
            }
            else if (CurrentSongIndex == 0)
            {
                CurrentSongIndex = ListSongs.Count - 1;
            }

            string path = ListSongs[CurrentSongIndex].link;
            Uri uri = new Uri(path);
            MediaSource mediaSource = MediaSource.CreateFromUri(uri);
            mediaPlayer.Source = mediaSource;
            ListOfSongs.SelectedIndex = CurrentSongIndex;
            mediaPlayer.MediaPlayer.Play();
        }

        private void StatusButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (PlayingStatus == false)
            {
                mediaPlayer.MediaPlayer.Play();
                PlayingStatus = true;
                StatusButton.Icon = new SymbolIcon(Symbol.Pause);
            }
            else
            {
                mediaPlayer.MediaPlayer.Pause();
                PlayingStatus = false;
                StatusButton.Icon = new SymbolIcon(Symbol.Play);
            }
        }

        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentSongIndex < ListSongs.Count - 1 && 0 <= CurrentSongIndex)
            {
                CurrentSongIndex++;
            }
            else if (CurrentSongIndex == ListSongs.Count - 1)
            {
                CurrentSongIndex = 0;
            }

            string path = ListSongs[CurrentSongIndex].link;
            Uri uri = new Uri(path);
            MediaSource mediaSource = MediaSource.CreateFromUri(uri);
            mediaPlayer.Source = mediaSource;
            ListOfSongs.SelectedIndex = CurrentSongIndex;
            mediaPlayer.MediaPlayer.Play();
        }
    }
}
