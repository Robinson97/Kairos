using Autofac;
using Kairos.Business;
using Kairos.Core;
using Kairos.Core.Business.Config;
using Kairos.Core.UWP.Business.Navigation;
using Kairos.UI.Map;
using Kairos.UI.Settings;
using Kairos.UI.WorkOverview;
using System;
using System.Collections.Generic;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

        #endregion Fields

        public ShellPage()
        {
            this.InitializeComponent();
            PageItems = new List<PageItem>();

            using (var scope = ServiceProvider.Container.BeginLifetimeScope())
            {
                _sampleService = scope.Resolve<IUserConfigManager>();
            }

            PageItems.Add(new PageItem() { Name = "WorkOverview" });
            NavigationService.CurrentInstance.NavigationFrame = frameMain;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += ShellPage_BackRequested;
            ElementTheme elementTheme = LoadBackgroundSetting();
            SetBackgroundTheme(elementTheme);
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PlayStartSound("Assets/Sound/correct.mp3");
        }

        private void PlayStartSound(string fileName)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///{fileName}", UriKind.RelativeOrAbsolute));
            mediaPlayer.AudioCategory = MediaPlayerAudioCategory.Alerts;
            //mediaPlayer.Volume = 100.0;
            mediaPlayer.Play();
        }

        private ElementTheme LoadBackgroundSetting()
        {
            ElementTheme elementThemeToDisplay;
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.ApplicationDataCompositeValue composite = (Windows.Storage.ApplicationDataCompositeValue)roamingSettings.Values["ApplicationSettings"];

            if (composite != null)
            {
                string theme = (composite["Theme"] as string);

                if (theme != null)
                {
                    elementThemeToDisplay = 
                        (theme.Equals(ElementTheme.Dark.ToString())) ? ElementTheme.Dark : 
                        (theme.Equals(ElementTheme.Light.ToString())) ? ElementTheme.Light : 
                        ElementTheme.Default;
                }
                else
                {
                    elementThemeToDisplay = SetDefaultTheme();
                }
            }
            else
            {
                elementThemeToDisplay = SetDefaultTheme();
            }
            return elementThemeToDisplay;
        }

        private void SetBackgroundTheme(ElementTheme elementTheme)
        {
            if (Window.Current.Content is FrameworkElement frameworkElement)
                frameworkElement.RequestedTheme = elementTheme;
        }

        private ElementTheme SetDefaultTheme()
        {
            ElementTheme elementTheme = ElementTheme.Dark;
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.ApplicationDataCompositeValue composites = new Windows.Storage.ApplicationDataCompositeValue();
            composites["Theme"] = ElementTheme.Dark.ToString();
            roamingSettings.Values["ApplicationSettings"] = composites;
            return elementTheme;
        }
    }
}