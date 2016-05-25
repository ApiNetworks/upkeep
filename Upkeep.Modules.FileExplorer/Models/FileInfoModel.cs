using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Upkeep.Modules.FileExplorer.Models
{
    public class FileInfoModel : BindableBase
    {
        private readonly BackgroundWorker worker;

        public FileInfoModel()
        {
            this.MinProgressValue = 0;
            this.MaxProgressValue = 100;

            this.worker = new BackgroundWorker();
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += this.DoWork;
            this.worker.ProgressChanged += Worker_ProgressChanged;
            this.worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                CurrentProgress = 0;
                this.DisplayStatus = "Canceled!";
            }
            else if (!(e.Error == null))
            {
                CurrentProgress = 0;
                this.DisplayStatus = ("Error: " + e.Error.Message);
            }
            else
            {
                CurrentProgress = 100;
                this.DisplayStatus = "OK";
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentProgress = e.ProgressPercentage;
            this.DisplayStatus = String.Format("Loading: {0}%", CurrentProgress);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            for(int i=1; i<=10; i++)
            {
                Thread.Sleep(1000);
                this.worker.ReportProgress(i * 10);
            }
        }

        //private void DoWork(object sender, DoWorkEventArgs e)
        //{
        //    using (StreamReader sr = File.OpenText(FullName))
        //    {
        //        string s = String.Empty;
        //        while ((s = sr.ReadLine()) != null)
        //        {
        //            //we're just testing read speeds
        //        }
        //    }
        //}

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { SetProperty(ref this._fullName, value); }
        }

        private string _directory;
        public string Directory
        {
            get { return _directory; }
            set { SetProperty(ref this._directory, value); }
        }

        private string _extension;
        public string Extension
        {
            get { return _extension; }
            set { SetProperty(ref this._extension, value); }
        }

        private long _length;
        public long Length
        {
            get { return _length; }
            set { SetProperty(ref this._length, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref this._name, value); }
        }

        private bool _isInProgress;
        public bool IsInProgress
        {
            get { return _isInProgress; }
            set { SetProperty(ref this._isInProgress, value); }
        }

        private bool _isLoaded;
        public bool IsLoaded
        {
            get { return _isLoaded; }
            set { SetProperty(ref this._isLoaded, value); }
        }

        private int _currentProgress;
        public int CurrentProgress
        {
            get { return _currentProgress; }
            set { SetProperty(ref this._currentProgress, value); }
        }

        private int _maxProgressValue;
        public int MaxProgressValue
        {
            get { return _maxProgressValue; }
            set { SetProperty(ref this._maxProgressValue, value); }
        }

        private int _minProgressValue;
        public int MinProgressValue
        {
            get { return _minProgressValue; }
            set { SetProperty(ref this._minProgressValue, value); }
        }

        private string _displayStatus;
        public string DisplayStatus
        {
            get { return _displayStatus; }
            set { SetProperty(ref this._displayStatus, value); }
        }

        public event ProgressChangedEventHandler ProgressChanged
        {
            add { this.worker.ProgressChanged += value; }
            remove { this.worker.ProgressChanged -= value; }
        }

        public event RunWorkerCompletedEventHandler Completed
        {
            add { this.worker.RunWorkerCompleted += value; }
            remove { this.worker.RunWorkerCompleted -= value; }
        }
        public void StartAsync()
        {
            this.IsInProgress = true;
            this.worker.RunWorkerAsync();
        }
    }
}
