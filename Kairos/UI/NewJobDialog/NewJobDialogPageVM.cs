using GalaSoft.MvvmLight.Command;
using Kairos.Core.Business.Base;
using Kairos.Core.Data.Task;
using Kairos.Core.UWP.Business.Navigation;
using Kairos.Data.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kairos.UI.NewJobDialog
{ 
    public class NewJobDialogPageVM : ViewModelBase
    {
        private JobMediaTask _currentJob;

        public JobMediaTask CurrentJob
        {
            get
            {
                return _currentJob;
            }
            set
            {
                _currentJob = value;
                OnPropertyChanged();
            }
        }

        #region Commands
        public ICommand CmdAddLocalFile { get; set; }

        public ICommand CmdSaveTask { get; set; }

        public ICommand CmdTakePictureFromWebcam { get; set; }
        #endregion
        

        public NewJobDialogPageVM()
        {
            CurrentJob = new JobMediaTask();
            CmdSaveTask = new RelayCommand<object>(SaveCurrentTask);
            
        }

        private void SaveCurrentTask(object obj)
        {
            NavigationService.CurrentInstance.NavigateTo("WorkOverviewPage");
        }
    }
}
