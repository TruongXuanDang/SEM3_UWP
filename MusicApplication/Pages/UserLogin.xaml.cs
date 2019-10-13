using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MusicApplication.Entities;
using Newtonsoft.Json;
using Windows.UI.Xaml.Navigation;
using MusicApplication.Services;
using System.Collections.Generic;
using Windows.UI.Xaml.Input;

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
        private ValidateService validateService;
        public Login()
        {
            this.InitializeComponent();
            this.fileService = new FileService();
            this.memberService = new MemberService();
            this.validateService = new ValidateService();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var member = new LoginMember
            {
                email = Email.Text,
                password = Password.Password,
            };

            Dictionary<string, string> errors = member.Validate();
            if (errors.Count == 0)
            {
                var jsonResult = memberService.Login(member);

                var resMember = JsonConvert.DeserializeObject<LoginMember>(jsonResult);
                var token = resMember.token;

                var sampleFile = fileService.WriteIntoTxtFile(token);
                var pathOfSampleFile = sampleFile.Path;
                validateService.ValidateTrue();
                this.Frame.Navigate(typeof(SongList));
            }
            else
            {
                validateService.ValidateFalse(EmailMessage, errors, "email");
                validateService.ValidateFalse(PasswordMessage, errors, "password");
            }
            
        }

        private void TextBlock_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Register));
        }
    }
}
