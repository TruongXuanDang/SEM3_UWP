using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MusicApplication.Entities;
using Newtonsoft.Json;
using Windows.UI.Xaml.Navigation;
using MusicApplication.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicApplication.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private FileService fileService;
        private MemberService memberService;
        public Login()
        {
            this.InitializeComponent();
            this.fileService = new FileService();
            this.memberService = new MemberService();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var member = new LoginMember
            {
                email = Username.Text,
                password = Password.Password,
            };

            var jsonResult = memberService.Login(member);

            var resMember = JsonConvert.DeserializeObject<LoginMember>(jsonResult);
            var token = resMember.token;

            var sampleFile = fileService.WriteIntoTxtFile(token);
            var pathOfSampleFile = sampleFile.Path;
        }
    }
}
