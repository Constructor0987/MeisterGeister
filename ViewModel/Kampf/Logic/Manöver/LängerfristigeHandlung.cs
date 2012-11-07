using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.Kampf.Logic.Manöver
{
    public class LängerfristigeHandlung : Manöver
    {
        public LängerfristigeHandlung(KämpferInfo ausführender, double dauer)
            : base(ausführender, dauer)
        {
            //this.PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        //Verbraucht immer 1
        //[DependentProperty("VerbleibendeDauer")]
        //public override int Angriffsaktionen
        //{
        //    get
        //    {
        //        return (int)Math.Min(VerbleibendeDauer, 2);
        //    }
        //}

        public override String Name
        {
            get { return "Längerfristige Handlung"; }
        }

        public override string ToString()
        {
            return String.Format("{2} ({0}/{1})", Dauer-VerbleibendeDauer, Dauer, Name);
        }
    }
}
