using Autofac;
using Kairos.Business;
using Kairos.Business.Config;
using Kairos.UI.Map;
using Kairos.UI.Settings;
using Kairos.UI.WorkOverview;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Kairos
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellPage : Page
    {
        #region Fields
        private List<PageItem> PageItems { get; set; }
        private readonly IUserConfigManager _sampleService;
        #endregion

        public ShellPage()
        {
            this.InitializeComponent();
            PageItems = new List<PageItem>();
            _sampleService = App.Container.Resolve<IUserConfigManager>();
            Business.App.IAppCarrier d = App.Container.Resolve<Business.App.IAppCarrier>();
            d.CurrentApp = App.Current;


            PageItems.Add(new PageItem() { Name = "WorkOverview"  } );
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += ShellPage_BackRequested;
        }

        private void ShellPage_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
           
        }

        private void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void NavigationViewControl_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                frameMain.Navigate(typeof(SettingsPage));
            }
            else
            {
                string name = args.InvokedItem.ToString();

                switch (name)
                {
                    case "Home":
                        frameMain.Navigate(typeof(WorkOverviewPage));
                        break;
                    case "Map":
                        frameMain.Navigate(typeof(MapPage));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
