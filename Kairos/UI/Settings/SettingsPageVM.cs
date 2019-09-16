using GalaSoft.MvvmLight.Command;
using Kairos.Business.Base;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Kairos.UI.Settings
{
    public class SettingsPageVM : ViewModelBase
    {
        #region Commands
        public ICommand CmdThemeToggleActivated { get; set; }

        #endregion

        public SettingsPageVM()
        {
            CmdThemeToggleActivated = new RelayCommand<object>(ChangeWindowTheme);
        }

        #region Methods
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

            toastContent.Visual.BindingGeneric.HeroImage = new ToastGenericHeroImage()
            {
                Source = "https://picsum.photos/364/180?image=1043"
            };

            ToastNotification toast = new ToastNotification(toastContent.GetXml())
            {
                ExpirationTime = DateTime.Now.AddSeconds(10)
            };

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private void ChangeWindowTheme(object param)
        {
            if (param is Windows.UI.Xaml.RoutedEventArgs ev)
            {
                if(ev.OriginalSource is ToggleSwitch toggleButton)
                {
                    ElementTheme elementTheme;
                    elementTheme = (toggleButton.IsOn) ? ElementTheme.Dark : ElementTheme.Light;

                    if (Window.Current.Content is FrameworkElement frameworkElement)
                        frameworkElement.RequestedTheme = elementTheme;
                }
            }
        }
        #endregion
    }
}
