using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Storage;

namespace Kairos.BackgroundTasks.Backup
{
    public class BackupBackgroundTask : IBackgroundTask
    {
        private BackgroundTaskDeferral _deferral;
        private Windows.Storage.StorageFolder _jsonFileTarget = Windows.Storage.ApplicationData.Current.LocalFolder;
        private StorageFolder _tempFolder = ApplicationData.Current.TemporaryFolder;
        private string _backupFileName = "jobStorage_backup.json";

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();

            await CreateBackupJSONAsync();

            taskInstance.Canceled += TaskInstance_Canceled;
            _deferral.Complete();
        }

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
        }

        /// <summary>
        /// Erstellt eine Backup-Datei der aktuellen JSON Datei
        /// </summary>
        /// <returns></returns>
        private async Task CreateBackupJSONAsync()
        {
            StorageFile storageFile =
                 await _jsonFileTarget.GetFileAsync("jobStorage.json");

            await storageFile.CopyAsync(_tempFolder, _backupFileName, NameCollisionOption.ReplaceExisting);
        }
    }
}