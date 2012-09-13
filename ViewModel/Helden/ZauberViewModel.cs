using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.Helden
{
    public class ZauberViewModel : Base.ViewModelBase
    {
        #region //---- COMMANDS ----

        private Base.CommandBase onDeleteZauber;
        public Base.CommandBase OnDeleteZauber
        {
            get { return onDeleteZauber; }
        }

        private Base.CommandBase onAddZauber;
        public Base.CommandBase OnAddZauber
        {
            get { return onAddZauber; }
        }

        private Base.CommandBase onOpenWiki;
        public Base.CommandBase OnOpenWiki
        {
            get { return onOpenWiki; }
        }

        #endregion

        #region //---- EIGENSCHAFTEN & FELDER ----

        // Selection
        public Model.Held SelectedHeld
        {
            get { return Global.SelectedHeld; }
            set
            {
                Global.SelectedHeld = value;
                OnChanged("SelectedHeld");
            }
        }
        Model.Held_Zauber _selectedHeldZauber = null;
        public Model.Held_Zauber SelectedHeldZauber
        {
            get { return _selectedHeldZauber; }
            set { _selectedHeldZauber = value; OnChanged("SelectedHeldZauber"); }
        }

        Model.Zauber _selectedAddZauber = null;
        public Model.Zauber SelectedAddZauber
        {
            get { return _selectedAddZauber; }
            set { _selectedAddZauber = value; OnChanged("SelectedAddZauber"); }
        }

        // Listen
        public List<Model.Held_Zauber> ZauberListe
        {
            get { return SelectedHeld == null ? null : SelectedHeld.Held_Zauber.ToList(); }
        }

        // TODO MT
        //public List<Model.Zauber> ZauberAuswahlListe
        //{
        //    get { return SelectedHeld == null ? null : SelectedHeld.ZauberWählbar; }
        //}

        #endregion

        #region //---- KONSTRUKTOR ----

        public ZauberViewModel(Func<string, string, bool> confirm, Action<string, Exception> showError)
            : base(confirm, showError)
        {
            // EventHandler für SelectedHeld registrieren
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };

            onDeleteZauber = new Base.CommandBase(DeleteZauber, null);
            onAddZauber = new Base.CommandBase(AddZauber, null);
            onOpenWiki = new Base.CommandBase(OpenWiki, null);
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Init()
        {

        }

        public void NotifyRefresh()
        {
            OnChanged("SelectedHeld");
            OnChanged("ZauberListe");
            OnChanged("ZauberAuswahlListe");
        }

        private void DeleteZauber(object sender)
        {
            Model.Held_Zauber h = SelectedHeldZauber;
            if (h != null
                && Confirm("Zauber löschen", String.Format("Soll die Zauber {0} wirklich vom Helden entfernt werden?", h.Zauber.Name))
                && Global.ContextHeld.Delete<Model.Held_Zauber>(h))
            {
                SelectedHeldZauber = null;
                NotifyRefresh();
            }
        }

        private void AddZauber(object sender)
        {
            if (SelectedHeld != null && SelectedAddZauber != null)
            {
                // TODO MT
                //if (!SelectedHeld.HatZauber(SelectedAddZauber))
                //    SelectedHeld.AddZauber(SelectedAddZauber, null);

                NotifyRefresh();
            }
        }

        private void OpenWiki(object sender)
        {
            if (SelectedHeldZauber != null)
                System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/" + SelectedHeldZauber.Zauber.Name);
        }

        #endregion

        #region //---- EVENTS ----

        private void SelectedHeldChanged()
        {
            NotifyRefresh();
        }

        #endregion

    }

}
