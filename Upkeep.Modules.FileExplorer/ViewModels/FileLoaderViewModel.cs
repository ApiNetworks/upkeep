using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Upkeep.Shell.Infrastructure.Base;

namespace Upkeep.Modules.FileExplorer.ViewModels
{
    // UI example
    //<ProgressBar Value = "{Binding CurrentProgress, Mode=OneWay}" Visibility="{Binding ProgressVisibility}"/>

    class FileLoaderViewModel : BaseViewModel
    {
        private readonly BackgroundWorker worker;
        private readonly ICommand instigateWorkCommand;
        private int currentProgress;
       

        public FileLoaderViewModel()
        {
            this.instigateWorkCommand = new DelegateCommand<object>(OnInstigateWork);
            this.worker = new BackgroundWorker();
            this.worker.DoWork += this.DoWork;
            this.worker.ProgressChanged += this.ProgressChanged;
        }

        private void OnInstigateWork(object obj)
        {
            this.worker.RunWorkerAsync();
        }

        // your UI binds to this command in order to kick off the work
        public ICommand InstigateWorkCommand
        {
            get { return this.instigateWorkCommand; }
        }

        private int _currentProgress;
        public int CurrentProgress
        {
            get { return _currentProgress; }
            set { SetProperty(ref this._currentProgress, value); }
        }

        private bool _isInProgress;
        public bool IsInProgress
        {
            get { return _isInProgress; }
            set { SetProperty(ref this._isInProgress, value); }
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            // do time-consuming work here, calling ReportProgress as and when you can
            for(int i=0; i<10; i++)
            {
                Thread.Sleep(1000);
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.CurrentProgress = e.ProgressPercentage;
        }
    }
}
