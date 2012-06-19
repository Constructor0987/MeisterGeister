﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Helden
{
    public class HeldenViewModel : Base.ViewModelBase
    {
        #region //---- FELDER ----

        // Felder
        

        // Listen
        private List<Model.Held> _heldListe;

        #endregion

        #region //---- EIGENSCHAFTEN ----

        //---- LISTEN ----

        public List<Model.Held> HeldListe
        {
            get { return _heldListe; }
            set
            {
                _heldListe = value;
                OnChanged("HeldListe");
            }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public HeldenViewModel()
        {

        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void LoadDaten()
        {
            HeldListe = Global.ContextHeld.HeldenListe.OrderBy(h => h.Name).ToList();
        }

        #endregion
    }
}
