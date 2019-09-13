using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Kairos.UI.Settings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //ShowToast();

        }

        private static void ShowToast()
        {
            ToastContent toastContent = new ToastContent()
            {
                Launch = "app-defined-string",
                DisplayTimestamp = DateTimeOffset.Now,
                Audio = new ToastAudio()
                {
                    Src = new Uri("ms-winsoundevent:Notification.Mail"),
                    Loop = false
                },


                Actions = new ToastActionsCustom()
                {
                    Buttons =
                        {
                            new ToastButton("Annehmen", "action=viewdetails&contentId=351")
                            {
                                ActivationType = ToastActivationType.Foreground
                            },

                            new ToastButton("Ablehnen", "action=remindlater&contentId=351")
                            {
                                ActivationType = ToastActivationType.Background
                            }
                        }
                },

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "Ein schönes Bild",
                                HintMaxLines = 1
                            },

                            new AdaptiveText()
                            {
                                Text = "+++++++++++++++++++++++++"
                            },


                        }
                    }


                }

            };

            toastContent.Visual.BindingGeneric.HeroImage  = new ToastGenericHeroImage()
            {
                Source = "https://picsum.photos/364/180?image=1043"
            };



            ToastNotification toast = new ToastNotification(toastContent.GetXml())
            {
                ExpirationTime = DateTime.Now.AddSeconds(10)
            };


            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private void ScEnableDarkMode_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                ElementTheme elementTheme;
                elementTheme = (toggleSwitch.IsOn) ? ElementTheme.Dark : ElementTheme.Light;

                if (Window.Current.Content is FrameworkElement frameworkElement)
                    frameworkElement.RequestedTheme = elementTheme;
                
            }
        }
    }
}
