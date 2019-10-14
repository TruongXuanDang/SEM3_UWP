using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPLastTest.Constant
{
    class ApiUrl
    {
        public const string API_LOGIN = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/authentication";
        public const string API_REGISTER = "https://2-dot-backup-server-003.appspot.com/_api/v2/members";
        public const string API_UPLOAD_IMAGE_URL = "https://2-dot-backup-server-003.appspot.com/get-upload-token";
        public const string API_GET_INFO = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/information";
        public const string API_CREATE_SONG = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs";
        public const string API_GET_ALL_SONG = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/get-free-songs";
        public const string API_GET_ALL_MY_SONG = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/get-mine";
    }
}
