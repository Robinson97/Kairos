using Autofac;
using GalaSoft.MvvmLight.Command;
using Kairos.Business.Job;
using Kairos.Core;
using Kairos.Core.Business.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Kairos.UI.WorkOverview
{
    public class WorkOverviewPageVM : ViewModelBase
    {
        #region Fields

        private IJobManager _jobManager;
        private double _dailyProgress;
        private double _monthlyProgress;

        #endregion Fields

        #region Propertys

        public ObservableCollection<Kairos.Core.Data.Task.Job> JobCollection => _jobManager.JobStore.JobCollection;

        /// <summary>
        /// Daily progress of work
        /// </summary>
        public double DailyProgress
        {
            get => _dailyProgress;
            set
            {
                _dailyProgress = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Monthly progress of work
        /// </summary>
        public double MonthlyProgress
        {
            get => _monthlyProgress;
            set
            {
                _monthlyProgress = value;
                OnPropertyChanged();
            }
        }

        #endregion Propertys

        #region Command

        public ICommand CmdAddNewTaskItem { get; set; }

        #endregion Command

        public WorkOverviewPageVM()
        {
            CmdAddNewTaskItem = new RelayCommand<object>(ShowPopUpForAdding);

            using (var scope = ServiceProvider.Container.BeginLifetimeScope())
            {
                _jobManager = scope.Resolve<IJobManager>();
            }

            CalcCurrentDailyProgressOfWork();
            CalcCurrentMonthlyProgressOfWork();
        }

        private async void ShowPopUpForAdding(object obj)
        {
            AddJob.AddJobPage addJobPage = new AddJob.AddJobPage();
            ContentDialogResult result = await addJobPage.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    _jobManager.AddJobToCollection(addJobPage.CurrentJob);
                    OnPropertyChanged(nameof(JobCollection));
                    CalcAllAnalyticProgressStatus();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Calculates the daily Work-Time-Progress
        /// </summary>
        private void CalcCurrentDailyProgressOfWork()
        {
            IEnumerable<Core.Data.Task.Job> jobsFromToday
                = _jobManager.JobStore.JobCollection.Where(job => job.CreationDate.LocalDateTime.Date == DateTime.Now.Date);

            if (jobsFromToday.Any())
            {
                double sum = jobsFromToday.Sum(x => x.Duration);

                DailyProgress = (sum > 0) ? (sum * 100) / 8 : 0;
            }
        }

        /// <summary>
        /// Calculates the daily Work-Time-Progress
        /// </summary>
        private void CalcCurrentMonthlyProgressOfWork()
        {
            IEnumerable<Core.Data.Task.Job> jobsFromThisMonth
                = _jobManager.JobStore.JobCollection.Where(job =>
                    job.CreationDate.LocalDateTime.Date.Year == DateTime.Now.Date.Date.Year
                    && job.CreationDate.LocalDateTime.Date.Month == DateTime.Now.Date.Month);

            if (jobsFromThisMonth.Any())
            {
                double sum = jobsFromThisMonth.Sum(x => x.Duration);

                MonthlyProgress = (sum > 0) ? (sum * 100) / 40 : 0;
            }
        }

        private void CalcAllAnalyticProgressStatus()
        {
            CalcCurrentDailyProgressOfWork();
            CalcCurrentMonthlyProgressOfWork();
        }
    }
}