using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Kairos.UI.WorkOverview
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WorkOverviewPage : Page
    {
        public WorkOverviewPage()
        {
            this.InitializeComponent();
            
        }

        private async void BtnStartCamera_Click(object sender, RoutedEventArgs e)
        {
            //var captureUI = new CameraCaptureUI();
            //captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            //StorageFile file = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            var a = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

     //       Windows.Storage.StorageFolder storageFolder =
     //Windows.Storage.ApplicationData.Current.LocalFolder;
     //       Windows.Storage.StorageFile sampleFile =
     //           await storageFolder.GetFileAsync("sample.txt");
        }
    }
}
