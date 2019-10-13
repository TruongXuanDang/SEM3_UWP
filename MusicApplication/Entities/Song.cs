using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MusicApplication.Constant;

namespace MusicApplication.Entities
{
    public class Song
    {
        public string name { get; set; }
        public string description { get; set; }
        public string singer { get; set; }
        public string author { get; set; }
        public string link { get; set; }
        public string thumbnail { get; set; }
        public Dictionary<string, string> Validate()
        {
            var errors = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(name))
            {
                errors.Add("name", "Name is required!");
            }
            else if (name.Length > 30)
            {
                errors.Add("name", "Name must be under 30 characters!");
            }
            if (string.IsNullOrEmpty(singer))
            {
                errors.Add("single", "Single is required!");
            }
            if (string.IsNullOrEmpty(thumbnail))
            {
                errors.Add("thumbnail", "Thumbnail is required!");
            }
            if (string.IsNullOrEmpty(link))
            {
                errors.Add("link", "Link is required!");
            }
            else if (!link.Contains(".mp3"))
            {
                errors.Add("link", "Link need to be ended with '.mp3'");
            }
            if (string.IsNullOrEmpty(author))
            {
                errors.Add("author", "Author is required!");
            }
            return errors;
        }
    }
}
