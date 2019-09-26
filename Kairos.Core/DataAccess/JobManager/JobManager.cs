using Kairos.Business.Job;
using Kairos.Core.Data.Task;
using Kairos.Core.DataAccess.JobManager;
using Kairos.Data.Task;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(JobManager))]
namespace Kairos.Core.DataAccess.JobManager
{
    public class JobManager : IJobManager
    {
        #region Fields
       
        #endregion

        public JobStore JobStore { get; private set; }

        public string JSONFileTarget { get; private set; }

        public string JSONFileName { get; private set; }

        public string CompletePathToJSON => Path.Combine(JSONFileTarget, JSONFileName);

        public async void AddJobToCollection(Job job)
        {
            JobStore.JobCollection.Add(job);
            await SaveTaskCollection();
        }

        public async Task<bool> SaveTaskCollection()
        {
            bool success = false;
                if (JobStore == null)
                    JobStore = new JobStore();


            await UWP.FileAccess.FileAccessManager.WriteFileAsync(CompletePathToJSON, JsonConvert.SerializeObject(JobStore));


            return success;
        }

        public JobManager()
        {
            JSONFileName = "jobStorage.json";
            JSONFileTarget = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            JobStore = ReadLocalJobCollection();
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
