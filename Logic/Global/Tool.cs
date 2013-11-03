using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace MeisterGeister
{
    // TODO MT: erweitern, dass das Menü automatisch aus der Liste heraus erzeugt wird

    /// <summary>
    /// Stellt die MeisterGeister-Tool-Liste dar
    /// </summary>
    public class Tool
    {
        #region //---- TOOLLISTE -----
        static Tool()
        {
            ToolListe = new Dictionary<string, Tool>();

            ToolListe.Add("Helden", new Tool()
            {
                Name = "Helden",
                Icon = "Icons/helden.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.Helden.HeldenView)
            });
            ToolListe.Add("Proben", new Tool()
            {
                Name = "Proben",
                Icon = "Icons/Wuerfel/w20.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.Proben.ProbenView)
            });
            ToolListe.Add("Kampf", new Tool()
            {
                Name = "Kampf",
                Icon = "Icons/nahkampf_01.png",
                MenuGruppe = "Wege des Kampfes",
                ViewType = typeof(View.Kampf.KampfView)
            });
            ToolListe.Add("Gegner", new Tool()
            {
                Name = "Abenteuer",
                Icon = "Icons/gegner.png",
                MenuGruppe = "Wege des Kampfes",
                ViewType = typeof(View.Kampf.Controls.GegnerView)
            });
            ToolListe.Add("Notizen", new Tool()
            {
                Name = "Notizen",
                Icon = "Icons/notiz.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.Notiz.NotizView)
            });
            ToolListe.Add("Kalender", new Tool()
            {
                Name = "Kalender",
                Icon = "Icons/kalender.png",
                MenuGruppe = "Wege des Wanderers",
                ViewType = typeof(View.Kalender.KalenderView)
            });
            ToolListe.Add("NSCs", new Tool()
            {
                Name = "NSCs",
                Icon = "Icons/meisterperson.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.NscGeneratorAlt.NscGeneratorAltView)
            });
            ToolListe.Add("Umrechner", new Tool()
            {
                Name = "Umrechner",
                Icon = "Icons/masse.png",
                MenuGruppe = "Wege des Handels",
                ViewType = typeof(View.Umrechner.UmrechnerView)
            });
            ToolListe.Add("Würfel", new Tool()
            {
                Name = "Würfel",
                Icon = "Icons/wuerfelbecher.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.Würfeln.WürfelView)
            });
            ToolListe.Add("SpielerInfo", new Tool()
            {
                Name = "SpielerInfo",
                Icon = "Icons/General/screen.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.SpielerScreen.SpielerScreenControlView)
            });
            ToolListe.Add("Basar", new Tool()
            {
                Name = "Basar",
                Icon = "Icons/muenzen.png",
                MenuGruppe = "Wege des Handels",
                ViewType = typeof(View.Basar.BasarView)
            });
            ToolListe.Add("Schmiede", new Tool()
            {
                Name = "Schmiede",
                Icon = "Icons/schmiede.png",
                MenuGruppe = "Wege des Handels",
                ViewType = typeof(View.Schmiede.SchmiedeView)
            });
            ToolListe.Add("Globus", new Tool()
            {
                Name = "Globus",
                Icon = "Logos/DereGlobus_Icon2.png",
                MenuGruppe = "Wege des Wanderers",
                ViewType = typeof(View.Globus.GlobusView)
            });
            ToolListe.Add("Artefakte", new Tool()
            {
                Name = "Artefakte",
                Icon = "Icons/artefakt.png",
                MenuGruppe = "Wege der Magie",
                ViewType = typeof(View.ArtGen.ArtGenView)
            });
            ToolListe.Add("ZooBot", new Tool()
            {
                Name = "ZooBot",
                Icon = "Icons/kraeutersuche.png",
                MenuGruppe = "Wege des Wanderers",
                ViewType = typeof(View.ZooBot.ZooBotView)
            });
            ToolListe.Add("Zauberzeichen", new Tool()
            {
                Name = "Zauberzeichen",
                Icon = "Icons/zauberzeichen.png",
                MenuGruppe = "Wege der Magie",
                ViewType = typeof(View.Zauberzeichen.ZauberzeichenView)
            });
            ToolListe.Add("Ares", new Tool()
            {
                Name = "Ares",
                Icon = "Logos/Ares_Logo.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.AresPlayer.AresPlayerView)
            });
#if (DEBUG || INTERN || TEST) // im Release-Modus ausblenden
            ToolListe.Add("Alchimie", new Tool()
            {
                Name = "Alchimie",
                Icon = "Icons/alchimie.png",
                MenuGruppe = "Wege der Magie",
                ViewType = typeof(View.Alchimie.AlchimieView)
            });
            ToolListe.Add("Beschwörung", new Tool()
            {
                Name = "Beschwörung",
                Icon = "Icons/magie.png",
                MenuGruppe = "Wege der Magie",
                ViewType = typeof(View.Beschwörung.BeschwörungView)
            });
            ToolListe.Add("Audio", new Tool()
            {
                Name = "Audio",
                Icon = "Icons/General/audio.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.AudioPlayer.AudioPlayerView)
            });
            ToolListe.Add("Abenteuer", new Tool()
            {
                Name = "Abenteuer",
                Icon = "Icons/meistertools_02.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.Abenteuer.AbenteuerView)
            });
            ToolListe.Add("NSCs_neu", new Tool()
            {
                Name = "NSCs_neu",
                Icon = "Icons/meisterperson.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.NscGenerator.NscGeneratorView)
            });
            ToolListe.Add("Reise", new Tool()
            {
                Name = "Reise",
                Icon = "Icons/kartenzeichnen.png",
                MenuGruppe = "Wege des Wanderers",
                ViewType = typeof(View.Reise.ReiseView)
            });
#endif
        }

        /// <summary>
        /// Eine Liste mit allen MeisterGeister-Tools.
        /// </summary>
        public static Dictionary<string, Tool> ToolListe { get; private set; }

        #endregion

        #region //---- METHODEN ----

        // Konstruktor private, damit Tools nur über das Dictionary abgefrufen werden können
        private Tool() { }

        public Control CreateToolView()
        {
            Control toolControl = null;

            object toolObject = Activator.CreateInstance(ViewType);
            if (toolObject is Control)
                toolControl = (Control)toolObject;

            return toolControl;
        }

        #endregion

        #region //---- EIGENSCHAFTEN ----

        /// <summary>
        /// Name des Tools.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Pfad des Tool-Icons.
        /// </summary>
        public string Icon { get; private set; }

        /// <summary>
        /// Typ des Tools.
        /// </summary>
        public Type ViewType { get; private set; }

        /// <summary>
        /// Gibt an unter welchem Menü-Punkt das Tool erscheinen soll.
        /// </summary>
        public string MenuGruppe { get; private set; }

        #endregion
    }

}
