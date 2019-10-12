﻿using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using MusicApplication.Constant;
using MusicApplication.Entities;
using MusicApplication.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicApplication.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SongListOfMine : Page
    {
        private FileService fileService;
        private SongService songService;
        public ObservableCollection<Song> ListSongs = new ObservableCollection<Song>();
        public SongListOfMine()
        {
            this.InitializeComponent();
            this.fileService = new FileService();
            this.songService = new SongService();

            ObservableCollection<Song> listSong = songService.GetSongs(fileService.ReadFromTxtFile(),ApiUrl.API_GET_ALL_MY_SONG);
            foreach (Song song in listSong)
            {
                ListSongs.Add(song);
            }
        }

        private void ListOfSongs_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListView view = (ListView)sender;
                Song clickedSong = view.SelectedItem as Song;

                mediaPlayer.Source = songService.GetMediaSourceToPlaySong(clickedSong);
                mediaPlayer.MediaPlayer.Play();

            }
            catch (Exception exception)
            {

            }
        }
    }
}
