using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Helden.Logic;

namespace MeisterGeister.Model
{
    public partial class Held_Ausrüstung : IHatHeld
    {
        public Held_Ausrüstung()
        {
            this.PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
            this.HeldAusrüstungGUID = Guid.NewGuid();
        }

        public Ausrüstung Ausrüstung
        {
            get
            {
                if (Rüstung != null)
                    return Rüstung.Ausrüstung;
                if (Fernkampfwaffe != null)
                    return Fernkampfwaffe.Ausrüstung;
                if (Waffe != null)
                    return Waffe.Ausrüstung;
                if (Schild != null)
                    return Schild.Ausrüstung;
                return null;
            }
        }

        public Rüstung Rüstung
        {
            get
            {
                if (Held_Rüstung != null)
                    return Held_Rüstung.Rüstung;
                return null;
            }
        }

        public Fernkampfwaffe Fernkampfwaffe
        {
            get
            {
                if (Held_Fernkampfwaffe != null)
                    return Held_Fernkampfwaffe.Fernkampfwaffe;
                return null;
            }
        }

        public Waffe Waffe
        {
            get
            {
                if (Held_BFAusrüstung != null && Held_BFAusrüstung.Held_Waffe != null)
                    return Held_BFAusrüstung.Held_Waffe.Waffe;
                return null;
            }
        }

        public Schild Schild
        {
            get
            {
                if (Held_BFAusrüstung != null && Held_BFAusrüstung.Schild != null)
                    return Held_BFAusrüstung.Schild;
                return null;
            }
        }

        [DependentProperty("Eigenname")]
        public string Name
        {
            get
            {
                return Eigenname ?? ((Ausrüstung!=null)?Ausrüstung.Name:null);
            }
            set
            {
                Eigenname = value;
            }
        }

    }
}
