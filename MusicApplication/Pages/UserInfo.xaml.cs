using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using MusicApplication.Constant;
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

            avatar.Source = new BitmapImage(new Uri(resUser.avatar));
            firstName.Text = resUser.firstName;
            lastName.Text = resUser.lastName;
            gender.Text = (resUser.gender).ToString();
            introduction.Text = resUser.introduction;
        }
    }
}
