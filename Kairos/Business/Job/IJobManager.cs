using Kairos.Data.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kairos.Business.Job
{
    public interface IJobManager
    {
        ObservableCollection<JobTask> TaskCollection { get; set; }

        Task SaveTaskCollection();
    }
}
