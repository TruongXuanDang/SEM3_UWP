﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MusicApplication.Services
{
    interface IFileService
    {
        StorageFile WriteIntoTxtFile(string token);
        string ReadFromTxtFile();
        string GetLinkToUploadFile();
    }
}
