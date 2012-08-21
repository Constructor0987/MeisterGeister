using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model;
using System.ComponentModel;

namespace MeisterGeister.ViewModel.Helden
{
    public class ListeViewModel : Base.ViewModelBase
    {

        private Held selectedHeld = new Held();
        private bool hasChanges = false;
        private List<Model.Held> _heldListe;

        public ListeViewModel()
        {
            onNewHeld = new Base.CommandBase(NewHeld, null);
            onDeleteHeld = new Base.CommandBase(DeleteHeld, null);

            LoadDaten();
            if (Global.SelectedHeld != null)
                selectedHeld = Global.SelectedHeld;
        }

        public Held SelectedHeld {
            get { return selectedHeld; }
            set {
                selectedHeld = value;
                Global.SelectedHeld = value;
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

        private Base.CommandBase onNewHeld;
        public Base.CommandBase OnNewHeld
        {
            get { return onNewHeld; }
        }

        private void NewHeld(object sender)
        {
            Held h = Global.ContextHeld.New<Held>();
            h.AddBasisTalente();
            if (Global.ContextHeld.Insert<Held>(h))
            {
                //Liste aktualisieren
                LoadDaten();
            }
        }

        private Base.CommandBase onDeleteHeld;
        public Base.CommandBase OnDeleteHeld
        {
            get { return onDeleteHeld; }
        }

        private void DeleteHeld(object sender)
        {
            Held h = SelectedHeld;
            if (h != null && Global.ContextHeld.Delete<Held>(h))
            {
                //Liste aktualisieren
                LoadDaten();
                SelectedHeld = HeldListe.FirstOrDefault();
            }
        }

        public string ExportHeld(string pfad)
//        private void ExportHeld(object sender)
        {
            Held h = SelectedHeld;
            if (h != null)
            {
                h.Export(pfad);
                return pfad;
            }
            return null;
        }

    }
}
