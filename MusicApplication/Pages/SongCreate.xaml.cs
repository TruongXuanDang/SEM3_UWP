using System;
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
        public SongCreate()
        {
            this.InitializeComponent();
            this.songService = new SongService();
            this.fileService = new FileService();
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
                songService.CreateSong(song, fileService.ReadFromTxtFile());
                MessageDialog dialog = new MessageDialog("Succeeded");
                await dialog.ShowAsync();
            }
            else
            {
                MessageDialog dialog = new MessageDialog("Name, thumbnail, link need to be validated");
                await dialog.ShowAsync();
            }

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
