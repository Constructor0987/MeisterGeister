using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model;
using System.ComponentModel;

namespace MeisterGeister.ViewModel.Helden.Controls
{
    public class ListeViewModel : Base.ViewModelBase
    {

        private Held selectedHeld = new Held();
        private bool hasChanges = false;
        private List<Model.Held> _heldListe;

        public ListeViewModel()
        {
        }

        public Held SelectedHeld {
            get { return selectedHeld; }
            set {
                selectedHeld = value;
                OnChanged("SelectedHeld");
            }
        }

        private void SelectedHeldChanged()
        {
            SelectedHeld = Global.SelectedHeld;
            if (SelectedHeld != null)
                SelectedHeld.PropertyChanged += OnSelectedHeldPropertyChanged;
        }
        
        private void SelectedHeldChanging()
        {
            if (Global.SelectedHeld != null)
            {
                if(hasChanges)
                    Global.ContextHeld.Update<Model.Held>(SelectedHeld);
                hasChanges = false;
                Global.SelectedHeld.PropertyChanged -= OnSelectedHeldPropertyChanged;
            }
        }

        private void OnSelectedHeldPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            //if (new string[] { "Name", "BildLink", "Rasse", "Kultur", "Profession", "AktiveHeldengruppe" }.Contains(args.PropertyName))
            //    hasChanges = true;
        }

        public List<Model.Held> HeldListe
        {
            get { return _heldListe; }
            set
            {
                _heldListe = value;
                OnChanged("HeldListe");
            }
        }

        public void LoadDaten()
        {
            HeldListe = Global.ContextHeld.HeldenListe.OrderBy(h => h.Name).ToList();
        }

    }
}
