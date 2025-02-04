using System;
using System.ComponentModel;

namespace VTS.QLNS.CTC.App.Helper
{
    public static class BackgroundWorkerHelper
    {
        public static BackgroundWorker Run(
            DoWorkEventHandler doWork,
            RunWorkerCompletedEventHandler completed = null,
            ProgressChangedEventHandler progressChanged = null)
        {
            using (BackgroundWorker backgroundWorker = new BackgroundWorker())
            {
                backgroundWorker.DoWork += doWork;
                if (completed != null)
                {
                    backgroundWorker.RunWorkerCompleted += completed;
                }
                if (progressChanged != null)
                {
                    backgroundWorker.WorkerSupportsCancellation = true;
                    backgroundWorker.WorkerReportsProgress = true;
                    backgroundWorker.ProgressChanged += progressChanged;
                }
                backgroundWorker.RunWorkerAsync();
                return backgroundWorker;
            }
        }

        public static void Run(
            Action doWork,
            RunWorkerCompletedEventHandler completed = null,
            ProgressChangedEventHandler progressChanged = null)
        {
            using (BackgroundWorker backgroundWorker = new BackgroundWorker())
            {
                backgroundWorker.DoWork += (DoWorkEventHandler)((s, e) => doWork());
                if (completed != null)
                    backgroundWorker.RunWorkerCompleted += completed;
                if (progressChanged != null)
                {
                    backgroundWorker.WorkerReportsProgress = true;
                    backgroundWorker.ProgressChanged += progressChanged;
                }
                backgroundWorker.RunWorkerAsync();
            }
        }
    }
}
