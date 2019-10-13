using System;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using MusicApplication.Entities;
using Windows.UI.Xaml;
using MusicApplication.Constant;
using MusicApplication.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicApplication.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SongList : Page
    {
        private SongService songService;
        private FileService fileService;
        public ObservableCollection<Song> ListSongs = new ObservableCollection<Song>();
        public bool PlayingStatus = false;
        public int CurrentSongIndex = 0;

        public SongList()
        {
            this.InitializeComponent();
            this.songService = new SongService();
            this.fileService = new FileService();

            ObservableCollection<Song> listSong = songService.GetSongs(fileService.ReadFromTxtFile(), ApiUrl.API_GET_ALL_SONG);
            foreach (Song song in listSong)
            {
                ListSongs.Add(song);
            }
        }


        private async void ListOfSongs_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView view = (ListView)sender;
            Song clickedSong = view.SelectedItem as Song;
            CurrentSongIndex = ListSongs.IndexOf(clickedSong);
            ControlLabel.Text = clickedSong.name + " is playing...";
            try
            {
                mediaPlayer.Source = songService.GetMediaSourceToPlaySong(clickedSong);
                PlaySong();
            }
            catch (Exception exception)
            {
                MessageDialog dialog = new MessageDialog(exception.Message);
                await dialog.ShowAsync();
            }
        }

        private async void PreviousButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentSongIndex <= ListSongs.Count - 1 && 0 < CurrentSongIndex)
            {
                CurrentSongIndex--;
            }
            else if (CurrentSongIndex == 0)
            {
                CurrentSongIndex = ListSongs.Count - 1;
            }

            ListOfSongs.SelectedIndex = CurrentSongIndex;
            try
            {
                mediaPlayer.Source = songService.GetMediaSourceToPlaySong(ListSongs[CurrentSongIndex]);
                PlaySong();

            }
            catch (Exception exception)
            {
                MessageDialog dialog = new MessageDialog(exception.Message);
                await dialog.ShowAsync();
            }
        }

        private void StatusButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (PlayingStatus == false)
            {
                PlaySong();
            }
            else
            {
                PauseSong();
            }
        }

        private async void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentSongIndex < ListSongs.Count - 1 && 0 <= CurrentSongIndex)
            {
                CurrentSongIndex++;
            }
            else if (CurrentSongIndex == ListSongs.Count - 1)
            {
                CurrentSongIndex = 0;
            }

            ListOfSongs.SelectedIndex = CurrentSongIndex;
            try
            {
                mediaPlayer.Source = songService.GetMediaSourceToPlaySong(ListSongs[CurrentSongIndex]);
                PlaySong();

            }
            catch (Exception exception)
            {
                MessageDialog dialog = new MessageDialog(exception.Message);
                await dialog.ShowAsync();
            }
        }

        private void PauseSong()
        {
            mediaPlayer.MediaPlayer.Pause();
            PlayingStatus = false;
            StatusButton.Icon = new SymbolIcon(Symbol.Play);
        }

        private void PlaySong()
        {
            mediaPlayer.MediaPlayer.Play();
            PlayingStatus = true;
            StatusButton.Icon = new SymbolIcon(Symbol.Pause);
        }
    }
}
