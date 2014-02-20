using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.Inventar
{
    public class AusrüstungViewModel : Base.ViewModelBase
    {
        /// <summary>
        /// Auf einem detachten Objekt arbeiten und dann speichern oder direkt?
        /// </summary>
        public AusrüstungViewModel()
        {
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        private Ausrüstung ausrüstung;
        public Ausrüstung Ausrüstung
        {
            get { return ausrüstung; }
            set { Set(ref ausrüstung, value); }
        }

        private Waffe nahkampfwaffe;
        public Waffe Nahkampfwaffe
        {
            get { return nahkampfwaffe; }
            set { Set(ref nahkampfwaffe, value); }
        }

        private Fernkampfwaffe fernkampfwaffe;
        public Fernkampfwaffe Fernkampfwaffe
        {
            get { return fernkampfwaffe; }
            set { Set(ref fernkampfwaffe, value); }
        }

        private Schild schild;
        public Schild Schild
        {
            get { return schild; }
            set { Set(ref schild, value); }
        }

        private Rüstung rüstung;
        public Rüstung Rüstung
        {
            get { return rüstung; }
            set { Set(ref rüstung, value); }
        }

        [DependentProperty("Nahkampfwaffe")]
        public bool IsNahkampfwaffe
        {
            get { return Nahkampfwaffe != null; }
            set {
                if (value && Nahkampfwaffe == null)
                    Ausrüstung.Waffe = new Waffe(); //TODO in Methode mit korrekten defaults auslagern.
                else if (!value && Nahkampfwaffe != null)
                    Nahkampfwaffe = null;
            }
        }

        private bool isFernkampfwaffe;
        public bool IsFernkampfwaffe
        {
            get { return isFernkampfwaffe; }
            set { Set(ref isFernkampfwaffe, value); }
        }

        private bool isSchild;
        public bool IsSchild
        {
            get { return isSchild; }
            set { Set(ref isSchild, value); }
        }

        private bool isRüstung;
        public bool IsRüstung
        {
            get { return isRüstung; }
            set { Set(ref isRüstung, value); }
        }


    }
}
