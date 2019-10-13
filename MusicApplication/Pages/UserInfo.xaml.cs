using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using MusicApplication.Entities;
using MusicApplication.Services;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicApplication.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserInfo : Page
    {
        private MemberService memberService;
        private FileService fileService;
        public UserInfo()
        {
            this.InitializeComponent();
            this.fileService = new FileService();
            this.memberService = new MemberService();
            var jsonResult = memberService.GetInformation(fileService.ReadFromTxtFile());
            var resUser = JsonConvert.DeserializeObject<User>(jsonResult);

            try
            {
                avatar.ImageSource = new BitmapImage(new Uri(resUser.avatar));
            }
            catch (Exception e)
            {
                avatar.ImageSource = new BitmapImage(new Uri("ms-appx:///Sources/boy.png"));
            }
            name.Text = resUser.firstName+" "+ resUser.lastName;
            switch (resUser.gender)
            {
                case 0:
                    gender.Text = "Male";
                    break;
                case 1:
                    gender.Text = "Female";
                    break;
                case 2:
                    gender.Text = "Other";
                    break;
            }
            introduction.Text = resUser.introduction;
            birthday.Text = resUser.birthday.Substring(0,10);
            phone.Text = resUser.phone;
            address.Text = resUser.address;
            email.Text = resUser.email;
        }
    }
}
