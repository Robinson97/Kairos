using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kairos.Data.Task
{
    public class JobTask
    {
        #region Propertys
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// The Creationdate of the Task
        /// </summary>
        public DateTime CreationDate { get; set; }
        
        /// <summary>
        /// ID from the TFS
        /// </summary>
        public int TFSID { get; set; }
        
        /// <summary>
        /// Duration of the Task
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// Has the Job been archivated
        /// </summary>
        public bool Archivated { get; set; }
        #endregion


        public JobTask()
        {
            ID = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
    }
}
