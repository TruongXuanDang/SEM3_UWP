using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPLastTest.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPLastTest.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page1 : Page
    {
        public List<string> ListNotes = new List<string>();
        public Page1()
        {
            this.InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string note = Note.Text;
            var dateTime = DateTime.Now;
            string fileName = dateTime.ToString("yyyy-MM-dd-HH-mm") + ".txt";

            WriteIntoTxtFile(note, fileName);
        }

        public StorageFile WriteIntoTxtFile(string note, string fileName)
        {
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.StorageFile sampleFile =
                storageFolder.CreateFileAsync(fileName,
                    Windows.Storage.CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();

            Windows.Storage.FileIO.WriteTextAsync(sampleFile, note).GetAwaiter().GetResult();
            return sampleFile;
        }

        public async void GetFile()
        {
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;

            // Get the first 20 files in the current folder, sorted by date.
            IReadOnlyList<StorageFile> sortedItems = await storageFolder.GetFilesAsync(CommonFileQuery.OrderByDate, 0, 20);

            // Iterate over the results and print the list of files
            // to the Visual Studio Output window.
            foreach (StorageFile file in sortedItems)
                Debug.WriteLine(file.Name + ", " + file.DateCreated);
        }
    }
}
