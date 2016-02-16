using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MeisterGeister.View.General
{
    public class LazyUpdate
    {
        private int invalidate = 0;
        private Action action;

        public LazyUpdate(Action action)
        {
            this.action = action;
        }

        public void Do()
        {
            invalidate++;
            Dispatcher.CurrentDispatcher.BeginInvoke((Action)delegate
            {
                if(--invalidate == 0)
                {
                    action();
                }
            });
        }
    }
}
