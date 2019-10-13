using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MusicApplication.Services
{
    class ValidateService
    {
        public async void ValidateTrue()
        {
            MessageDialog dialog = new MessageDialog("Succeed");
            await dialog.ShowAsync();
        }

        public void ValidateFalse(TextBlock textBlock, Dictionary<String, String> errors,string key)
        {
            if (errors.ContainsKey(key))
            {
                textBlock.Text = errors[key];
                textBlock.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock.Visibility = Visibility.Collapsed;
            }
        }
    }
}
