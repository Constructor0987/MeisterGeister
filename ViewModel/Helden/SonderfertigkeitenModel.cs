using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.Helden
{
    public class SonderfertigkeitenViewModel : Base.ViewModelBase
    {
        #region //---- COMMANDS ----

        private Base.CommandBase onDeleteSonderfertigkeit;
        public Base.CommandBase OnDeleteSonderfertigkeit
        {
            get { return onDeleteSonderfertigkeit; }
        }

        private Base.CommandBase onAddSonderfertigkeit;
        public Base.CommandBase OnAddSonderfertigkeit
        {
            get { return onAddSonderfertigkeit; }
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
        Model.Held_Sonderfertigkeit _selectedHeldSonderfertigkeit = null;
        public Model.Held_Sonderfertigkeit SelectedHeldSonderfertigkeit
        {
            get { return _selectedHeldSonderfertigkeit; }
            set { _selectedHeldSonderfertigkeit = value; OnChanged("SelectedHeldSonderfertigkeit"); } 
        }

        Model.Sonderfertigkeit _selectedAddSonderfertigkeit = null;
        public Model.Sonderfertigkeit SelectedAddSonderfertigkeit
        {
            get { return _selectedAddSonderfertigkeit; }
            set { _selectedAddSonderfertigkeit = value; OnChanged("SelectedAddSonderfertigkeit"); }
        }

        // Listen
        public List<Model.Held_VorNachteil> VorNachteilListe
        {
            get { return SelectedHeld == null ? null : SelectedHeld.Held_VorNachteil.ToList(); }
        }

        public List<Model.Sonderfertigkeit> SonderfertigkeitAuswahlListe
        {
            get { return SelectedHeld == null ? null : SelectedHeld.SonderfertigkeitenWählbar; }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public SonderfertigkeitenViewModel()
        {
            // EventHandler für SelectedHeld registrieren
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };

            onDeleteSonderfertigkeit = new Base.CommandBase(DeleteSonderfertigkeit, null);
            onAddSonderfertigkeit = new Base.CommandBase(AddSonderfertigkeit, null);
            onOpenWiki = new Base.CommandBase(OpenWiki, null);
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Init()
        {
            
        }

        public void NotifyRefresh()
        {
            //OnChanged("SelectedHeld");
            //OnChanged("VorNachteilListe");
            //OnChanged("VorteilAuswahlListe");
            //OnChanged("NachteilAuswahlListe");
        }

        private void DeleteSonderfertigkeit(object sender)
        {
            // TODO MT: Lösch-Frage einbauen

            Model.Held_Sonderfertigkeit h = SelectedHeldSonderfertigkeit;
            if (h != null && Global.ContextHeld.Delete<Model.Held_Sonderfertigkeit>(h))
                NotifyRefresh();
        }

        private void AddSonderfertigkeit(object sender)
        {
            if (SelectedHeld != null && SelectedAddSonderfertigkeit != null)
            {
                if (!SelectedHeld.HatSonderfertigkeit(SelectedAddSonderfertigkeit))
                    SelectedHeld.AddSonderfertigkeit(SelectedAddSonderfertigkeit, null);

                NotifyRefresh();
            }
        }

        private void OpenWiki(object sender)
        {
            if (SelectedHeldSonderfertigkeit != null)
                System.Diagnostics.Process.Start("http://www.wiki-aventurica.de/wiki/" + SelectedHeldSonderfertigkeit.Sonderfertigkeit.Name);
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
