using System;
using System.Collections.Generic;
using System.Linq;
//Eigene Usings
using MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;

namespace MeisterGeister
{
    public static class Global
    {

        #region //CONTEXTE

        public static Service.AudioService ContextAudio;
        public static Service.HeldService ContextHeld;
        public static Service.InventarService ContextInventar;
        public static Service.KampfService ContextKampf;
        //TODO ??: aus der Schmiede die Referenz entfernen, dann löschen
        public static Service.WaffeService ContextWaffe;
        public static Service.FernkampfwaffeService ContextFernkampfwaffe;
        public static Service.SchildService ContextSchild;
        public static Service.RüstungService ContextRüstung;
        public static Service.TalentService ContextTalent;
        public static Service.HandelsgutService ContextHandelsgut;
        public static Service.NscService ContextNsc;
        public static Service.NamenService ContextNamen;
        public static Service.RegelnService ContextRegeln;
        public static Service.NotizService ContextNotizen;

        // MenuLink
        public static Service.MenuLinkService _contextMenuLink;
        public static Service.MenuLinkService ContextMenuLink
        {
            get
            {
                if (_contextMenuLink == null)
                    _contextMenuLink = new Service.MenuLinkService();
                return _contextMenuLink;
            }
            set { _contextMenuLink = value; }
        }


        #endregion

        #region //FELDER

        private static Model.Held _selectedHeld;
        private static Guid _selectedHeldGUID;

        #endregion

        #region //EIGENSCHAFTSMETHODEN

        public static Model.Held SelectedHeld
        {
            get { return _selectedHeld; }
            set
            {
                // Falls der gleiche Held erneut gesetzt werden soll -> abbrechen
                if (_selectedHeld == value)
                    return;

                if (HeldSelectionChanging != null)
                    HeldSelectionChanging(null, new EventArgs());
                
                _selectedHeld = value;
                
                if (HeldSelectionChanged != null)
                    HeldSelectionChanged(null, new EventArgs());
            }
        }
        public static Guid SelectedHeldGUID
        {
            get { return _selectedHeldGUID; }
            set
            {
                _selectedHeldGUID = value;

                SelectedHeld = ContextHeld.HeldenListe.Where(h => h.HeldGUID == value).FirstOrDefault();
            }
        }

        #endregion

        #region //KONSTRUKTOR

        static Global()
        {

        }

        #endregion

        #region //KLASSENMETHODEN

        /// <summary>
        /// INIT lädt Daten aus der Datenbank, 1x aus APP aufgerufen beim start
        /// </summary>
        public static void Init()
        {
            ContextAudio = new Service.AudioService();
            ContextHeld = new Service.HeldService();
            ContextInventar = new Service.InventarService();
            ContextKampf = new Service.KampfService();
            //TODO ??: Services besser gruppieren
            ContextWaffe = new Service.WaffeService();
            ContextFernkampfwaffe = new Service.FernkampfwaffeService();
            ContextSchild = new Service.SchildService();
            ContextRüstung = new Service.RüstungService();

            ContextTalent = new Service.TalentService();
            ContextHandelsgut = new Service.HandelsgutService();
            ContextNsc = new Service.NscService();
            ContextNamen = new Service.NamenService();
            ContextRegeln = new Service.RegelnService();
            ContextNotizen = new Service.NotizService();
        }

        #endregion

        #region //EVENTS

        public static event EventHandler HeldSelectionChanged;
        public static event EventHandler HeldSelectionChanging;

        #endregion

    }
}
