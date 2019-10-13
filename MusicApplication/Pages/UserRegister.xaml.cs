using System;
using System.Collections.Generic;
using System.IO;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MusicApplication.Entities;
using Windows.Media.Capture;
using Windows.Storage;
using System.Net;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using MusicApplication.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicApplication.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private MemberService memberService;
        private FileService fileService;
        private ValidateService validateService;
        private int gender=0;
        public Register()
        {
            this.InitializeComponent();
            this.memberService = new MemberService();
            this.fileService = new FileService();
            this.validateService = new ValidateService();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private async void ButtonBase_TakePhotoOnClick(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null)
            {
                return;
            }

            var uploadURL = fileService.GetLinkToUploadFile();
            this.UploadFile(uploadURL, "myFile", "image/png", photo, Avatar, AvatarUrl);
        }

        public async void UploadFile(string url, string paramName, string contentType, StorageFile photo, Image image, TextBox textBox)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";

            Stream rs = await wr.GetRequestStreamAsync();
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", paramName, "path_file", contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            // write file.
            Stream fileStream = await photo.OpenStreamForReadAsync();
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);

            WebResponse wresp = null;
            try
            {
                wresp = await wr.GetResponseAsync();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                string imageUrl = reader2.ReadToEnd();
                image.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                textBox.Text = imageUrl;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error uploading file", ex.StackTrace);
                Debug.WriteLine("Error uploading file", ex.InnerException);
                if (wresp != null)
                {
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }

        private async void ButtonBase_RegisterClick(object sender, RoutedEventArgs e)
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
                gender = gender,
                birthday = Birthday.Date.ToString("yyyy-MM-dd"),
                email = Email.Text,
                password = Password.Password
            };
            Dictionary<String, String> errors = user.Validate();
            if (errors.Count == 0)
            {
                memberService.Register(user);
                validateService.ValidateTrue();
            }
            else
            {
                validateService.ValidateFalse(FirstNameMessage, errors,"firstName");
                validateService.ValidateFalse(LastNameMessage, errors,"lastName");
                validateService.ValidateFalse(EmailMessage, errors,"email");
                validateService.ValidateFalse(PasswordMessage, errors,"password");
                validateService.ValidateFalse(BirthdayMessage, errors,"birthday");

            }
        }

        private void BGRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null)
            {
                string colorName = rb.Tag.ToString();
                switch (colorName)
                {
                    case "Male":
                        gender = 0;
                        break;
                    case "Female":
                        gender = 1;
                        break;
                    case "Other":
                        gender = 2;
                        break;
                    
                }
            }
        }
    }
}
