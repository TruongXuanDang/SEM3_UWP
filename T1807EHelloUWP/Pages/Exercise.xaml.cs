using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using T1807EHelloUWP.Entities;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHelloUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Exercise : Page
    {
        public Exercise()
        {
            this.InitializeComponent();
            
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var username = this.username.Text;
            var password = this.password.Password;
            var member = new Member {
                email = username,
                password = password
            };

            Debug.WriteLine(JsonConvert.SerializeObject(member));
            HttpClient httpClient = new HttpClient();
        }
    }
}
