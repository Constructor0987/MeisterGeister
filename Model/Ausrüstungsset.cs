using MeisterGeister.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Model
{
    public partial class Ausrüstungsset
    {
        public Ausrüstungsset()
        {
            AusrüstungssetGUID = Guid.NewGuid();
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        public IEnumerable<Held_Waffe> Nahkampfwaffen
        {
            get { return Held_Ausrüstung.Where(ha => ha.Held_BFAusrüstung != null && ha.Held_BFAusrüstung.Held_Waffe != null).Select(ha => ha.Held_BFAusrüstung.Held_Waffe); }
        }

        public IEnumerable<Held_Fernkampfwaffe> Fernkampfwaffen
        {
            get { return Held_Ausrüstung.Where(ha => ha.Held_Fernkampfwaffe != null).Select(ha => ha.Held_Fernkampfwaffe); }
        }

        public IEnumerable<Held_BFAusrüstung> Schilde
        {
            get { return Held_Ausrüstung.Where(ha => ha.Held_BFAusrüstung != null && ha.Held_BFAusrüstung.Schild != null).Select(ha => ha.Held_BFAusrüstung); }
        }

        public IEnumerable<Held_Rüstung> Rüstungen
        {
            get { return Held_Ausrüstung.Where(ha => ha.Held_Rüstung != null).Select(ha => ha.Held_Rüstung); }
        }

        public Held Held
        {
            get { return Held_Ausrüstung.Select(ha => ha.Held).Distinct().Single(); }
        }

        [DependentProperty("Held_Ausrüstung")]
        public bool? Angelegt
        {
            get
            {
                bool foundAngelegt = false;
                bool foundAbgelegt = false;
                foreach (Held_Ausrüstung ha in Held_Ausrüstung)
                    if (ha.Angelegt)
                        foundAngelegt = true;
                    else
                        foundAbgelegt = true;
                if (foundAbgelegt)
                {
                    if (foundAngelegt)
                        return null;
                    else return false;
                }
                else return true;
            }
            set
            {
                if (value.HasValue)
                    foreach (Held_Ausrüstung ha in Held_Ausrüstung)
                        ha.Angelegt = value.Value;
            }
        }
    }
}
