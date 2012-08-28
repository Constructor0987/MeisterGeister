using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.Helden
{
    public class VorNachteileViewModel : Base.ViewModelBase
    {
        #region //---- FELDER ----

        // Felder

        // Listen

        // Commands


        #endregion

        #region //---- EIGENSCHAFTEN ----

        public Model.Held SelectedHeld
        {
            get { return Global.SelectedHeld; }
            set
            {
                Global.SelectedHeld = value;
                OnChanged("SelectedHeld");
                Init();
            }
        }

        // Listen
        public List<Model.Held_VorNachteil> VorNachteilListe
        {
            get { return SelectedHeld == null ? null : SelectedHeld.Held_VorNachteil.ToList(); }
        }

        // Commands


        #endregion

        #region //---- KONSTRUKTOR ----

        public VorNachteileViewModel()
        {
            // EventHandler für SelectedHeld registrieren
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Init()
        {
            OnChanged("VorNachteilListe");
        }

        #endregion

        #region //---- EVENTS ----

        private void SelectedHeldChanged()
        {
            OnChanged("SelectedHeld");
            Init();
        }

        #endregion

    }
    
}
