using GalaSoft.MvvmLight.Command;
using Kairos.Business.Job;
using Kairos.Core.Business.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;

namespace Kairos.UI.WorkOverview
{
    public class WorkOverviewPageVM : ViewModelBase
    {
        #region Fields
        private IJobManager _jobManager;
        #endregion

        #region Command
        public ICommand CmdAddNewTaskItem { get; set; }
        #endregion

        public ObservableCollection<Kairos.Core.Data.Task.Job> JobCollection => _jobManager.JobStore.JobCollection;
        

        public WorkOverviewPageVM()
        {
            CmdAddNewTaskItem = new RelayCommand<object>(ShowPopUpForAdding);
            _jobManager = DependencyService.Get<IJobManager>();
        }

        private async void ShowPopUpForAdding(object obj)
        {
            AddJob.AddJobPage addJobPage = new AddJob.AddJobPage();
            ContentDialogResult result = await addJobPage.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                _jobManager.AddJobToCollection(addJobPage.CurrentJob);
                OnPropertyChanged(nameof(JobCollection));
            }
        }
    }
}
