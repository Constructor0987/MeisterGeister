using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;

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

        #endregion

        #region //---- EIGENSCHAFTEN & FELDER ----

        // Selection
        [DependentProperty("VorNachteilListe")]
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

        // Listen
        public List<Model.Held_VorNachteil> VorNachteilListe
        {
            get { return SelectedHeld == null ? null : SelectedHeld.Held_VorNachteil.ToList(); }
        }

        List<Model.VorNachteil> _vorteilAuswahlListe = null;
        public List<Model.VorNachteil> VorteilAuswahlListe
        {
            get { return _vorteilAuswahlListe; }
            set { _vorteilAuswahlListe = value; OnChanged("VorteilAuswahlListe"); }
        }

        List<Model.VorNachteil> _nachteilAuswahlListe = null;
        public List<Model.VorNachteil> NachteilAuswahlListe
        {
            get { return _nachteilAuswahlListe; }
            set { _nachteilAuswahlListe = value; OnChanged("NachteilAuswahlListe"); }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public VorNachteileViewModel()
        {
            // EventHandler für SelectedHeld registrieren
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };

            onDeleteVorNachteil = new Base.CommandBase(DeleteVorNachteil, null);
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Init()
        {
            // TODO MT: Bereits vorhandene VorNachteile aus Auswahl entfernen
            VorteilAuswahlListe = Global.ContextVorNachteil.VorNachteilListe.Where(v => v.Vorteil == true).OrderBy(v => v.Name).ToList();
            NachteilAuswahlListe = Global.ContextVorNachteil.VorNachteilListe.Where(n => n.Nachteil == true).OrderBy(n => n.Name).ToList();
        }

        public void Refresh()
        {
            OnChanged("SelectedHeld");
            OnChanged("VorNachteilListe");
        }

        private void DeleteVorNachteil(object sender)
        {
            // TODO MT: Lösch-Frage einbauen

            Model.Held_VorNachteil h = SelectedHeldVorNachteil;
            if (h != null && Global.ContextHeld.Delete<Model.Held_VorNachteil>(h))
            {
                //Liste aktualisieren
                //Init();
            }
        }

        #endregion

        #region //---- EVENTS ----

        private void SelectedHeldChanged()
        {
            Refresh();
        }

        #endregion

    }
    
}
