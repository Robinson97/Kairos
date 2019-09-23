using Kairos.Core.Data.Task;
using Kairos.Data.Task;
using System;
using System.Collections.Generic;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Kairos.UI.AddJob
{
    public sealed partial class AddJobPage : ContentDialog
    {
        #region Fields
        private AddJobPageVM _viewModel;
        #endregion

        public Job CurrentJob
        {
            get
            {
                return _viewModel.CurrentJob;
            }
        }


        public AddJobPage()
        {
            this.InitializeComponent();
            this.DataContext = _viewModel = new AddJobPageVM();
            _viewModel.CurrentJob = new Job()
            {
                CreationDate = DateTime.Now
            };
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
