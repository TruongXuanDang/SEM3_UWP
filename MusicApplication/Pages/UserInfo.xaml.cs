using Windows.UI.Xaml.Controls;
using MusicApplication.Constant;
using MusicApplication.Services;

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

            MyTextInfo.Text = memberService.GetInformation(fileService.ReadFromTxtFile());
        }
    }
}
