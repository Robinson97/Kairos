using GalaSoft.MvvmLight;
using Kairos.Data.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kairos.UI.AddJob
{
    public class AddJobPageVM : ViewModelBase
    {
        public Kairos.Core.Data.Task.Job CurrentJob
        {
            get;set;
        }

        #region Commands

        #endregion

        public AddJobPageVM()
        {

        }
    }
}
