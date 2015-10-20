using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using MeisterGeister.Model.Extensions;
using System.Text.RegularExpressions;

namespace MeisterGeister.ViewModel.Helden
{
    public class VorNachteileViewModel : Base.ViewModelBase
    {
        #region //---- COMMANDS ----

        private Base.CommandBase onDeleteVorNachteil;
        public Base.CommandBase OnDeleteVorNachteil
        {
            get { return onDeleteVorNachteil; }
        }

        private Base.CommandBase onAddVorteil;
        public Base.CommandBase OnAddVorteil
        {
            get { return onAddVorteil; }
        }

        private Base.CommandBase onAddNachteil;
        public Base.CommandBase OnAddNachteil
        {
            get { return onAddNachteil; }
        }

        private Base.CommandBase onOpenWiki;
        public Base.CommandBase OnOpenWiki
        {
            get { return onOpenWiki; }
        }

        #endregion

        #region //---- EIGENSCHAFTEN & FELDER ----

        // Selection
        [DependentProperty("VorNachteilListe"), DependentProperty("VorteilAuswahlListe"), DependentProperty("NachteilAuswahlListe")]
        public Model.Held SelectedHeld
        {
            get { return Global.SelectedHeld; }
            set
            {
                Global.SelectedHeld = value;
                OnChanged("SelectedHeld");
            }
        }
        Model.Held_VorNachteil _selectedHeldVorNachteil = null;
        public Model.Held_VorNachteil SelectedHeldVorNachteil
        {
            get { return _selectedHeldVorNachteil; }
            set { _selectedHeldVorNachteil = value; OnChanged("SelectedHeldVorNachteil"); } 
        }

        Model.VorNachteil _selectedAddVorteil = null;
        public Model.VorNachteil SelectedAddVorteil
        {
            get { return _selectedAddVorteil; }
            set { _selectedAddVorteil = value; OnChanged("SelectedAddVorteil"); }
        }

        Model.VorNachteil _selectedAddNAchteil = null;
        public Model.VorNachteil SelectedAddNachteil
        {
            get { return _selectedAddNAchteil; }
            set { _selectedAddNAchteil = value; OnChanged("SelectedAddNachteil"); }
        }

        // Listen
        public List<Model.Held_VorNachteil> VorNachteilListe
        {
            get { return SelectedHeld == null ? null : SelectedHeld.Held_VorNachteil.ToList(); }
        }

        public List<Model.VorNachteil> VorteilAuswahlListe
        {
            get { return SelectedHeld == null ? null : SelectedHeld.VorteileWählbar; }
        }

        public List<Model.VorNachteil> NachteilAuswahlListe
        {
            get { return SelectedHeld == null ? null : SelectedHeld.NachteileWählbar; }
        }

        private bool _isReadOnly = MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        // Funktion
        private Func<Model.VorNachteil, VorNachteilAuswahlItem> auswahlDialog;

        #endregion

        #region //---- KONSTRUKTOR ----

        public VorNachteileViewModel(Func<string, string, bool> confirm, Action<string, Exception> showError, Func<string, string, int, int, int, int?> inputIntDialog, Func<Model.VorNachteil, VorNachteilAuswahlItem> auswahlDialog) : base(confirm, showError, inputIntDialog)
        {
            onDeleteVorNachteil = new Base.CommandBase(DeleteVorNachteil, null);
            onAddVorteil = new Base.CommandBase(AddVorteil, null);
            onAddNachteil = new Base.CommandBase(AddNachteil, null);
            onOpenWiki = new Base.CommandBase(OpenWiki, null);

            this.auswahlDialog = auswahlDialog;
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            Global.HeldSelectionChanged += SelectedHeldChanged;
            MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnlyChanged += IsReadOnlyChanged;
            SelectedHeldChanged(this, new EventArgs());
        }
        public override void UnregisterEvents()
        {
            base.UnregisterEvents();
            Global.HeldSelectionChanged -= SelectedHeldChanged;
            MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnlyChanged -= IsReadOnlyChanged;
        }

        public void NotifyRefresh()
        {
            OnChanged("SelectedHeld");
            OnChanged("VorNachteilListe");
            OnChanged("VorteilAuswahlListe");
            OnChanged("NachteilAuswahlListe");
        }

        private void DeleteVorNachteil(object sender)
        {
            Model.Held_VorNachteil h = SelectedHeldVorNachteil;
            if (h != null && !IsReadOnly
                && Confirm("Vor-/Nachteil löschen", String.Format("Soll der Vor-/Nachteil {0} wirklich vom Helden entfernt werden?", h.VorNachteil.Name)))
            {
                SelectedHeld.DeleteVorNachteil(h);
                SelectedHeldVorNachteil = null;
                NotifyRefresh();
            }
        }

        public VorNachteilAuswahlItem Auswahl(Model.VorNachteil vn)
        {
            if (auswahlDialog != null)
                return auswahlDialog(vn);
            return null;
        }

        private void AddVorteil(object sender)
        {
            AddVorNachteil("Vorteil");
        }

        private void AddVorNachteil(string typ)
        {
            Model.VorNachteil vn = null;
            if (typ == "Vorteil")
                vn = SelectedAddVorteil;
            else if (typ == "Nachteil")
                vn = SelectedAddNachteil;

            if (SelectedHeld != null && vn != null && !IsReadOnly)
            {
                bool hatVorNachteil = SelectedHeld.HatVorNachteil(vn);
                if ((vn.HatWert ?? false) || (!hatVorNachteil))
                {
                    if (hatVorNachteil && !(vn.DarfMehrfach ?? false))
                    {
                        if (Confirm(string.Format("{0} mehrfach einfügen", typ), string.Format("Der {0} '{1}' darf laut Regeln nicht mehrfach eingefügt werden (siehe {2}). Soll er trotzdem eingefüt werden?",
                            typ, vn.Name, vn.Literatur)) == false)
                            return;
                    }

                    string wert = null;
                    double? kosten = vn.Kosten;

                    // evtl. Auswahl treffen
                    if (!string.IsNullOrEmpty(vn.Auswahl))
                    {
                        if (Regex.IsMatch(vn.Auswahl, @"^\[.*?\]$"))
                        {
                            VorNachteilAuswahlItem auswahl = Auswahl(vn);
                            if (auswahl != null)
                            {
                                wert = auswahl.NameMitAuswahl;
                                if (kosten == null || kosten == 0)
                                    kosten = auswahl.Kosten; // Kosten nur setzen, wenn VorNachteil keine Fixkosten hat
                            }
                        }
                    }
                    
                    if ((kosten ?? 0) == 0)
                    { // keine Kosten hinterlegt, also beim User nachfragen
                        string kostenText = string.Format("{0}-Wert", Global.Text_Generierungseinheit_Abk);
                        kosten = InputIntDialog(kostenText, string.Format("Der {0} für den {1} '{2}' konnte nicht automatisch festgelegt werden. Bitte gib einen Wert ein (siehe {3}).",
                            kostenText, typ, vn.Name, vn.Literatur), 0, (vn.Vorteil ?? false) ? 0 : int.MinValue, (vn.Nachteil ?? false) ? 0 : int.MaxValue);
                    }
                    SelectedHeld.AddVorNachteil(vn, kosten, wert);
                }
                
                NotifyRefresh();
            }
        }

        private void AddNachteil(object sender)
        {
            AddVorNachteil("Nachteil");
        }

        private void OpenWiki(object sender)
        {
            if (SelectedHeldVorNachteil != null)
                WikiAventurica.OpenBrowser(SelectedHeldVorNachteil.VorNachteil.Name);
        }

        #endregion

        #region //---- EVENTS ----

        private void IsReadOnlyChanged(object sender, EventArgs e)
        {
            _isReadOnly = MeisterGeister.Logic.Einstellung.Einstellungen.IsReadOnly;
            OnChanged("IsReadOnly");
        }

        private void SelectedHeldChanged(object sender, EventArgs e)
        {
            NotifyRefresh();
        }

        #endregion
    }

    #region SUBKLASSEN

    public class VorNachteilAuswahlItem
    {
        private double v;

        public VorNachteilAuswahlItem(string name, double? kosten, string literatur)
        {
            Name = name;
            Kosten = kosten;
            Literatur = literatur;
            HatWert = false;
        }
        public VorNachteilAuswahlItem(Model.Talent t, double? kosten)
        {
            Name = t.Name;
            Kosten = kosten;
            Literatur = t.Literatur;
            HatWert = false;
        }
        public VorNachteilAuswahlItem(Model.VorNachteil vn, Model.VorNachteilAuswahl vna)
        {
            Name = vna.Name;
            Kosten = ((vna.Kosten ?? 0) == 0) ? vn.Kosten : vna.Kosten;
            Literatur = vna.Literatur;
            HatWert = vna.HatWert ?? false;
            Auswahl = vna.Auswahl;
        }

        public string NameMitAuswahl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Auswahl))
                    return Name;
                return string.Format("{0} ({1})", Name, Auswahl);
            }
        }
        public string Name { get; set; }
        public double? Kosten { get; set; }
        public string Literatur { get; set; }
        public bool HatWert { get; set; }
        public string Auswahl { get; set; }
    }

    #endregion

}
