using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Logic.Einstellung
{
    public delegate void EinstellungChangedHandler(object sender, EinstellungChangedEventArgs e);

    public class EinstellungChangedEventArgs : EventArgs
    {
        public string PropertyName = null;
        public string EinstellungName = null;

        public EinstellungChangedEventArgs(string propertyName, string einstellungName) : base()
        {
            EinstellungName = einstellungName;
            PropertyName = propertyName;
        }
    }

}
