using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.Helden
{
    public class ZauberViewModel : Base.ViewModelBase, Logic.IChangeListener
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

        private Base.CommandBase onWürfelProbe;
        public Base.CommandBase OnWürfelProbe
        {
            get { return onWürfelProbe; }
        }

        private Base.CommandBase onWürfelGruppenProbe;
        public Base.CommandBase OnWürfelGruppenProbe
        {
            get { return onWürfelGruppenProbe; }
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
            get { return SelectedHeld == null ? null : SelectedHeld.Held_Zauber.OrderBy(hz => hz.Zauber.Name).ThenBy(hz => hz.Repräsentation).ToList(); }
        }

        public List<Model.Zauber> ZauberAuswahlListe
        {
            get { return SelectedHeld == null ? null : SelectedHeld.ZauberWählbar; }
        }

        public Dictionary<string,string> RepräsentationAuswahlListe
        {
            get { return Repräsentationen.RepräsentationenListe; }
        }

        KeyValuePair<string, string> _selectedRepräsentation;
        public KeyValuePair<string, string> SelectedRepräsentation
        {
            get { return _selectedRepräsentation; }
            set { _selectedRepräsentation = value; OnChanged("SelectedRepräsentation"); }
        }

        private bool _isReadOnly = MeisterGeister.Logic.Settings.Einstellungen.IsReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public ZauberViewModel(Action<string> popup, Func<string, string, bool> confirm, Func<Probe, Model.Held, ProbenErgebnis> showProbeDialog, Action<string, Exception> showError)
            : base(popup, confirm, showProbeDialog, showError)
        {
            // EventHandler für SelectedHeld registrieren
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };
            MeisterGeister.Logic.Settings.Einstellungen.IsReadOnlyChanged += IsReadOnlyChanged;

            onDeleteZauber = new Base.CommandBase(DeleteZauber, null);
            onAddZauber = new Base.CommandBase(AddZauber, null);
            onOpenWiki = new Base.CommandBase(OpenWiki, null);
            onWürfelProbe = new Base.CommandBase(WürfelProbe, null);
            onWürfelGruppenProbe = new Base.CommandBase(WürfelGruppenProbe, null);
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

            // Standard Repräsentation
            if (SelectedHeld != null)
                SelectedRepräsentation = RepräsentationAuswahlListe.SingleOrDefault(r => r.Key == SelectedHeld.RepräsentationStandard);
        }

        private void DeleteZauber(object sender)
        {
            Model.Held_Zauber h = SelectedHeldZauber;
            if (h != null && !IsReadOnly
                && Confirm("Zauber löschen", String.Format("Soll die Zauber '{0}' wirklich vom Helden entfernt werden?", h.Zauber.Name))
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
                if (SelectedRepräsentation.Key == null)
                    PopUp("Es muss eine Repräsentation ausgewählt sein!");
                else if (SelectedHeld.HatZauber(SelectedAddZauber, SelectedRepräsentation.Key))
                    PopUp(String.Format("Der Held hat den Zauber '{0}' in der Repräsentation '{1}' bereits.", SelectedAddZauber.Name, SelectedRepräsentation.Key));
                else
                {
                    SelectedHeld.AddZauber(SelectedAddZauber, 0, SelectedRepräsentation.Key);
                    NotifyRefresh();
                }
            }
        }

        private void OpenWiki(object sender)
        {
            if (SelectedHeldZauber != null)
                WikiAventurica.OpenBrowser(SelectedHeldZauber.Zauber);
        }

        private void WürfelGruppenProbe(object obj)
        {
            Global.WürfelGruppenProbe(SelectedHeldZauber);
        }

        private void WürfelProbe(object obj)
        {
            ShowProbeDialog(SelectedHeldZauber, SelectedHeld);
        }

        #endregion

        #region //---- EVENTS ----

        private void IsReadOnlyChanged(object sender, EventArgs e)
        {
            _isReadOnly = MeisterGeister.Logic.Settings.Einstellungen.IsReadOnly;
            OnChanged("IsReadOnly");
        }

        private void SelectedHeldChanged()
        {
            if (!ListenToChangeEvents)
                return;
            NotifyRefresh();
        }

        #endregion

        private bool listenToChangeEvents = true;

        public bool ListenToChangeEvents
        {
            get { return listenToChangeEvents; }
            set { listenToChangeEvents = value; SelectedHeldChanged(); }
        }
        
    }

}
