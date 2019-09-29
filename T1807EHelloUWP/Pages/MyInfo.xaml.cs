using System;
using System.Net.Http;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using T1807EHelloUWP.Entities;
using Windows.UI.Xaml.Navigation;
using T1807EHelloUWP.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHelloUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyInfo : Page
    {
        private const string getApi = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/information";
        public MyInfo()
        {
            this.InitializeComponent();

            MemberServiceImp memberServiceImp = new MemberServiceImp();
            MyTextInfo.Text = memberServiceImp.FormGetInfo(getApi);
        }
    }
}
