using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPMidTest.Entities
{
    class ListSongs
    {
        public ObservableCollection<SongInfo> listSong
        {
            get => listSong;
            set => listSong = value;
        }
    }
}
