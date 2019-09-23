using Kairos.Data.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kairos.Core.Data.Task;

namespace Kairos.Business.Job
{
    public interface IJobManager
    {
        JobStore JobStore { get; }

        /// <summary>
        /// Add a job to the TaskCollection and saves it
        /// </summary>
        /// <param name="job"></param>
        void AddJobToCollection(Kairos.Core.Data.Task.Job job);

        /// <summary>
        /// Saves the TaskCollection
        /// </summary>
        /// <returns></returns>
        Task SaveTaskCollection();
    }
}
