using GalaSoft.MvvmLight.Command;
using Kairos.Core.Business.Base;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Kairos.UI.Settings
{
    public class SettingsPageVM : ViewModelBase
    {
        private ElementTheme _isDarkMode;
        private bool _isDarkSelected;
        private bool _isLigthSelected;
        private bool _isDefaultSelected;

        #region Propertys
        public ElementTheme SelectedTheme
        {
            get
            {
                return _isDarkMode;
            }
            set
            {
                _isDarkMode = value;
            }
        }

        public bool IsDarkSelected
        {
            get
            {
                return _isDarkSelected;
            }
            set
            {
                _isDarkSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsLigthSelected
        {
            get
            {
                return _isLigthSelected;
            }
            set
            {
                _isLigthSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsDefaultSelected
        {
            get
            {
                return _isDefaultSelected;
            }
            set
            {
                _isDefaultSelected = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand CmdThemeRadioButtoActivated { get; set; }

        #endregion

        public SettingsPageVM()
        {
            CmdThemeRadioButtoActivated = new RelayCommand<object>(ChangeWindowTheme);
            LoadSettings();
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
            if (param is string)
            {
                ElementTheme elementTheme = ElementTheme.Default;

                switch (param)
                {
                    case "dark":
                        elementTheme = ElementTheme.Dark;
                        break;

                    case "ligth":
                        elementTheme = ElementTheme.Light;
                        break;

                    case "deafult":
                        elementTheme = ElementTheme.Default;
                        break;

                    default:
                        break;

                }

                SelectedTheme = elementTheme;
                    if (Window.Current.Content is FrameworkElement frameworkElement)
                        frameworkElement.RequestedTheme = elementTheme;

                SaveSettings();
            }
        }

        /// <summary>
        /// Speichert die vorgenommenen Einstellungen
        /// </summary>
        private void SaveSettings()
        {
            ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue();
            composite["Theme"] = SelectedTheme.ToString();
            roamingSettings.Values["ApplicationSettings"] = composite;
        }

        private void LoadSettings()
        {
            ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)roamingSettings.Values["ApplicationSettings"];

            if (composite != null)
            {
                string theme = composite["Theme"] as string;

                switch (theme)
                {
                    case "Dark":
                        IsDarkSelected = true;
                        break;

                    case "Light":
                        IsLigthSelected = true;
                        break;
                    case "Default":
                        IsDefaultSelected = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //IsDarkMode = true;
                SaveSettings();
            }
           
        }
        #endregion
    }
}
