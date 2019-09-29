using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using T1807EHelloUWP.Entities;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.Media.Capture;
using Windows.Storage;
using System.Net;
using Windows.UI.Xaml.Media.Imaging;
using T1807EHelloUWP.Services;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHelloUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register :Page
    {

        private const string api = "https://2-dot-backup-server-003.appspot.com/_api/v2/members";
        private const string apiForUploadURL = "https://2-dot-backup-server-003.appspot.com/get-upload-token";
        
        public Register()
        {
            this.InitializeComponent();
        }

        private void ButtonBase_JsonOnClick(object sender, RoutedEventArgs e)
        {
            var user = new User
            {
                firstName = FirstName.Text,
                lastName = LastName.Text,
                avatar =
                    AvatarUrl.Text,
                phone = Phone.Text,
                address = Address.Text,
                introduction = Introduction.Text,
                gender = Int32.Parse(Gender.Text),
                birthday = Birthday.Text,
                email = Email.Text,
                password = Password.Password
            };
            MemberServiceImp memberServiceImp = new MemberServiceImp();
            memberServiceImp.FormRegister(user,api);
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }

            FileServiceImp fileServiceImp = new FileServiceImp();
            string UploadURL = fileServiceImp.GetUploadURL(apiForUploadURL);
            
            fileServiceImp.UploadFile(UploadURL, "myFile", "image/png", photo,Avatar,AvatarUrl);
        }
    }
}
