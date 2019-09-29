using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using T1807EHelloUWP.Entities;
using T1807EHelloUWP.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHelloUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private const string api = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/authentication";
        

        public Login()
        {
            this.InitializeComponent();
        }

        private async void ButtonBase_LoginOnClick(object sender, RoutedEventArgs e)
        {
            var member = new Member
            {
                email = Username.Text,
                password = Password.Password,
            };
            MemberServiceImp memberServiceImp = new MemberServiceImp();
            memberServiceImp.FormLogin(member,api);
        }
    }
}
