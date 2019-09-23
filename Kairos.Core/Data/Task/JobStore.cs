using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Kairos.Core.Data.Task
{
    public class JobStore
    {
        public ObservableCollection<Job> JobCollection { get; set; }
    }
}
