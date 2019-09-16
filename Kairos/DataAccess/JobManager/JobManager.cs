using Kairos.Business.Job;
using Kairos.Data.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kairos.DataAccess.JobManager
{
    public class JobManager : IJobManager
    {
        public ObservableCollection<JobTask> TaskCollection { get; set; }

        public Task SaveTaskCollection()
        {
            return new Task(() => System.Console.WriteLine());
        }
    }
}
