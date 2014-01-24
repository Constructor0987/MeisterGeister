using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Model.Extensions
{
    public delegate void ValidatePropertyChangingEventHandler(object sender, string propertyName, object currentValue, object newValue);
}
