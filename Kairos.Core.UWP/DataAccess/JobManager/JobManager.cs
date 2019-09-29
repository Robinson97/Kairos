using Kairos.Business.Job;
using Kairos.Core.Data.Task;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace Kairos.Core.DataAccess.JobManager
{
    public class JobManager : IJobManager
    {
        #region Propertys

        public JobStore JobStore { get; private set; }

        public Windows.Storage.StorageFolder JSONFileTarget { get; private set; }

        public string JSONFileName { get; private set; }

        public string CompletePathToJSON => Path.Combine(JSONFileTarget.Path, JSONFileName);

        #endregion Propertys

        public JobManager()
        {
            JSONFileName = "jobStorage.json";
            JSONFileTarget = Windows.Storage.ApplicationData.Current.LocalFolder;
            JobStore = ReadLocalJobCollection();
        }

        public async void AddJobToCollection(Job job)
        {
            JobStore.JobCollection.Add(job);
            var success = await SaveTaskCollection();
        }

        public async Task<bool> SaveTaskCollection()
        {
            bool success = true;
            if (JobStore == null)
                JobStore = new JobStore();

            Windows.Storage.StorageFile sampleFile =
                await JSONFileTarget.CreateFileAsync(JSONFileName,
                    Windows.Storage.CreationCollisionOption.ReplaceExisting);

            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, JsonConvert.SerializeObject(JobStore));

            return success;
        }

        private JobStore ReadLocalJobCollection()
        {
            JobStore store = new JobStore
            {
                JobCollection = new ObservableCollection<Job>()
            };

            if (File.Exists(CompletePathToJSON))
            {
                store = JsonConvert.DeserializeObject<JobStore>(File.ReadAllText(CompletePathToJSON));
            }

            return store;
        }
    }
}