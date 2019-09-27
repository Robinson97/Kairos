using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kairos.Data.Task
{
    public class JobMediaTask : Kairos.Core.Data.Task.Job
    {
        public bool ContainsAttachment { get; set; }

        public string PathToAttachment { get; set; }
    }
}
