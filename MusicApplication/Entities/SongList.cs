using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApplication.Entities
{
    class SongList
    {
        public ObservableCollection<Song> listSong
        {
            get => listSong;
            set => listSong = value;
        }
    }
}
