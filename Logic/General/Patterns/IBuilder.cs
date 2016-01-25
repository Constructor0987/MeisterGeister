using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Logic.General.Patterns
{
    public interface IBuilder<TResult, TArgs>
    {
        TResult Build(TArgs args);
    }
}
