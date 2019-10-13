using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MusicApplication.Entities;
using MusicApplication.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicApplication.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SongCreate : Page
    {
        private SongService songService;
        private FileService fileService;
        private ValidateService validateService;
        public SongCreate()
        {
            this.InitializeComponent();
            this.songService = new SongService();
            this.fileService = new FileService();
            this.validateService = new ValidateService();
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

            Dictionary<String, String> errors = song.Validate();
            if (errors.Count == 0)
            {
                songService.CreateSong(song, fileService.ReadFromTxtFile());
                validateService.ValidateTrue();
                this.Frame.Navigate(typeof(SongListOfMine));
            }
            else
            {
                validateService.ValidateFalse(NameMessage,errors,"name");
                validateService.ValidateFalse(SingerMessage,errors, "single");
            }
        }
    }
}
