using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using MusicApplication.Entities;

namespace MusicApplication.Services
{
    interface ISongService
    {
        string CreateSong(Song song, string token);
        ObservableCollection<Song> GetSongs(string token, string api);
        MediaSource GetMediaSourceToPlaySong(Song song);
    }
}
