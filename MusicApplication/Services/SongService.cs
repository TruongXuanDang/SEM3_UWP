using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Windows.Media.Core;
using MusicApplication.Constant;
using MusicApplication.Entities;
using Newtonsoft.Json;
using System;

namespace MusicApplication.Services
{
    class SongService:ISongService
    {

        public ObservableCollection<Song> GetSongs(string token, string api)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization","Basic "+token);
            var httpRequestMessage = httpClient.GetAsync(api);
            var jsonResult = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            ObservableCollection<Song> songList = JsonConvert.DeserializeObject<ObservableCollection<Song>>(jsonResult);
            return songList;
        }

        public MediaSource GetMediaSourceToPlaySong(Song song)
        {
            string path = song.link;
            Uri uri = new Uri(path);
            MediaSource mediaSource = MediaSource.CreateFromUri(uri);
            return mediaSource;
        }

        public string CreateSong(Song song,string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization","Basic "+token);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(song),Encoding.UTF8,"application/json");
            var httpRequestMessage = httpClient.PostAsync(ApiUrl.API_CREATE_SONG, content);
            var jsonResult = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            return jsonResult;
        }
    }
}
