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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHelloUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MasterLayout : Page
    {
        public MasterLayout()
        {
            this.InitializeComponent();
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var hyperlinkButton = sender as HyperlinkButton;
            if (hyperlinkButton != null)
            {
                switch (hyperlinkButton.Tag)
                {
                    case "Login":
                        this.MainContent.Navigate(typeof(Login));
                        break;
                    case "Register":
                        this.MainContent.Navigate(typeof(Register));
                        break;
                    case "MyInfo":
                        this.MainContent.Navigate(typeof(MyInfo));
                        break;
                }
            }
        }

        private void showMenu(object sender, TappedRoutedEventArgs e)
        {
            this.Menu.IsPaneOpen = !this.Menu.IsPaneOpen;
        }
    }
}
