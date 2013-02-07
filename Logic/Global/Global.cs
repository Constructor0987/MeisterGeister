using System;
using System.Collections.Generic;
using System.Linq;
using MeisterGeister.Logic.General;
//Eigene Usings
using MeisterGeister.Model;
using Service = MeisterGeister.Model.Service;

namespace MeisterGeister
{
    public static class Global
    {

        #region //CONTEXTE

        public static Service.AudioService ContextAudio;
        public static Service.DataService ContextHeld;
        public static Service.InventarService ContextInventar;
        public static Service.KampfService ContextKampf;
        public static Service.TalentService ContextTalent;
        public static Service.HandelsgutService ContextHandelsgut;
        public static Service.NscService ContextNsc;
        public static Service.NamenService ContextNamen;
        public static Service.RegelnService ContextRegeln;
        public static Service.NotizService ContextNotizen;
        public static Service.VorNachteilService ContextVorNachteil;
        public static Service.ZauberService ContextZauber;

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

        #endregion

        #region //EIGENSCHAFTSMETHODEN
        public static bool IsInitialized
        {
            get;
            private set;
        }

        /// <summary>
        /// Ruft den aktuell ausgewählten Helden ab, oder legt ihn fest.
        /// </summary>
        public static Model.Held SelectedHeld
        {
            get { return _selectedHeld; }
            set
            {                                    
                // Falls der gleiche Held erneut gesetzt werden soll
                // -> abbrechen, da keine Änderung erfolgt
                if (_selectedHeld == value)
                    return;
                
                // Event vor der Held-Änderung werfen
                if (HeldSelectionChanging != null)
                    HeldSelectionChanging(null, new EventArgs());

                // neuen Helden setzen und Änderungen in DB speichern
                _selectedHeld = value;
                if (_selectedHeld != null)
                {
                    Global.ContextHeld.Update<Model.Held>(SelectedHeld);
                    Logic.Settings.Einstellungen.SelectedHeld = value.HeldGUID.ToString();
                }
                else
                    Logic.Settings.Einstellungen.SelectedHeld = null;

                // Event nach der Held-Änderung werfen
                if (HeldSelectionChanged != null)
                    HeldSelectionChanged(null, new EventArgs());
            }
        }

        /// <summary>
        /// Ruft die GUID des aktuell ausgewählten Helden ab, oder legt ihn über die GUID fest.
        /// </summary>
        public static Guid SelectedHeldGUID
        {
            get { return SelectedHeld == null ? Guid.Empty : SelectedHeld.HeldGUID; }
            set
            {
                SelectedHeld = ContextHeld.Liste<Held>().Where(h => h.HeldGUID == value).FirstOrDefault();
            }
        }

        #endregion

        #region //KONSTRUKTOR

        static Global()
        {
            IsInitialized = false;
        }

        #endregion

        #region //KLASSENMETHODEN

        /// <summary>
        /// INIT lädt Daten aus der Datenbank, 1x aus APP aufgerufen beim start
        /// </summary>
        public static void Init()
        {
            ContextAudio = new Service.AudioService();
            ContextHeld = new Service.DataService();
            ContextInventar = new Service.InventarService();
            ContextKampf = new Service.KampfService();
            ContextTalent = new Service.TalentService();
            ContextVorNachteil = new Service.VorNachteilService();
            ContextHandelsgut = new Service.HandelsgutService();
            ContextNsc = new Service.NscService();
            ContextNamen = new Service.NamenService();
            ContextRegeln = new Service.RegelnService();
            ContextNotizen = new Service.NotizService();
            ContextZauber = new Service.ZauberService();

            IsInitialized = true;

            if (Logic.Settings.Einstellungen.SelectedHeld != null)
            {
                Guid heldguid;
                if (Guid.TryParse(Logic.Settings.Einstellungen.SelectedHeld, out heldguid))
                    SelectedHeldGUID = heldguid;
            }
        }

        /// <summary>
        /// Versetzt die Anwendung in einen "Beschäftig-Status" bzw. entfernt diesen Status.
        /// Das Hauptfenster zeigt dabei ein Busy-Overlay.
        /// </summary>
        /// <param name="isBusy">'True' falls Anwendung gerade arbeitet.</param>
        /// <param name="info">Info Text.</param>
        public static void SetIsBusy(bool isBusy, string info = "Beschäftigt...")
        {
            if (App.Current.MainWindow != null
                && App.Current.MainWindow is View.MainView && App.Current.MainWindow.IsLoaded)
            {
                (App.Current.MainWindow as View.MainView).IsBusyInfoText = info;
                (App.Current.MainWindow as View.MainView).IsBusy = isBusy;
            }
        }

        /// <summary>
        /// Wechselt ins Proben-Tool und würfelt eine Probe.
        /// </summary>
        /// <param name="probe">Die zu würfelnde Probe.</param>
        public static void WürfelGruppenProbe(Probe probe)
        {
            if (App.Current.MainWindow == null
                || !(App.Current.MainWindow is View.MainView))
                return;

            (App.Current.MainWindow as View.MainView).StarteTab("Proben");

            if (GruppenProbeWürfeln != null)
                GruppenProbeWürfeln(probe, new EventArgs());
        }

        #endregion

        #region //EVENTS

        public static event EventHandler HeldSelectionChanged;
        public static event EventHandler HeldSelectionChanging;

        public static event GruppenProbeWürfelnEventHandler GruppenProbeWürfeln;

        #endregion

    }

    public delegate void GruppenProbeWürfelnEventHandler(Probe probe, EventArgs e);

}
