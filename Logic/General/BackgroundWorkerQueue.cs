using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Logic.General
{
    public class BackgroundWorkerQueue : Queue<BackgroundWorkerQueueItem>
    {
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        public event ProgressChangedEventHandler ProgressChanged;

        public void QueueWorker(BackgroundWorkerQueueItem worker)
        {
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.ProgressChanged += (s, a) =>
            {
                if (ProgressChanged != null)
                    ProgressChanged(s, a);
            };

            worker.RunWorkerCompleted += (s, a) =>
            {
                Dequeue();
                if (RunWorkerCompleted != null)
                    RunWorkerCompleted(s, a);

                if (Count > 0)
                {
                    var next = Peek();
                    next.RunWorkerAsync(next.Sender);
                }
                else if (ProgressChanged != null)
                    ProgressChanged(s, new ProgressChangedEventArgs(100, "Vorgang abgeschlossen"));
            };

            Enqueue(worker);
            if (Count == 1)
            {
                var next = Peek();
                next.RunWorkerAsync(next.Sender);
            }
        }
    }
}
