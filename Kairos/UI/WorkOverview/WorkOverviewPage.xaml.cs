using System;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Kairos.UI.WorkOverview
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WorkOverviewPage : Page
    {
        #region Propertys

        public WorkOverviewPageVM ViewModel { get; set; }

        #endregion Propertys

        public WorkOverviewPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel = new WorkOverviewPageVM();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == nameof(BackgroundTasks.Backup.BackupBackgroundTask))
                {
                    break;
                }
                else
                {
                    RegisterBackupBackgroundTask();
                    break;
                }
            }

            base.OnNavigatedTo(e);
        }

        private void RegisterBackupBackgroundTask()
        {
            var builder = new BackgroundTaskBuilder
            {
                Name = nameof(BackgroundTasks.Backup.BackupBackgroundTask),
                TaskEntryPoint = "Kairos.BackgroundTasks.Backup.BackupBackgroundTask"
            };

            builder.SetTrigger(new SystemTrigger(SystemTriggerType.NetworkStateChange, false));
            builder.AddCondition(new SystemCondition(SystemConditionType.InternetNotAvailable));
            BackgroundTaskRegistration task = builder.Register();
            task.Completed += Task_Completed;
        }

        private void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            Console.WriteLine("+++++++++++ Breaking News +++++++++++ ");
            Console.WriteLine("+++++++++++ Kein Internet verfügbar +++++++++++ ");
            Console.WriteLine("+++++++++++ Breaking News +++++++++++ ");
        }
    }
}