using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Logic.General
{
    interface ILongLivingProcess
    {
        event EventHandler<DoWorkEventArgs> DoWork;
        event EventHandler<ProgressChangedEventArgs> ProgressChanged;
        event EventHandler<RunWorkerCompletedEventArgs> RunWorkerCompleted;
    }
}
