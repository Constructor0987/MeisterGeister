using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Logic.General
{
    public class BackgroundWorkerQueueItem : BackgroundWorker
    {
        public BackgroundWorkerQueueItem(object sender)
        {
            this.Sender = sender;
        }

        public object Sender { get; private set; }
    }
}
