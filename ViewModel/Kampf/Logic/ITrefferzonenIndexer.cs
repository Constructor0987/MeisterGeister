using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public interface ITrefferzonenIndexer<T>
    {
        T this[Trefferzone zone]
        {
            get;
            set;
        }
    }
}
