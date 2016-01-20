using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace MeisterGeister
{
    // TODO: Icon-Pfade korrigieren

    /// <summary>
    /// Stellt die MeisterGeister-Tool-Liste dar
    /// </summary>
    public class Tool
    {
        public static string[] Gruppen = new string[] { "Wege des Meisters", "Wege des Kampfes", "Wege der Magie", "Wege des Handels", "Wege des Wanderers", "Eigene Wege" };

        #region //---- TOOLLISTE -----
        static Tool()
        {
            ToolListe = new Dictionary<string, Tool>();

            ToolListe.Add("Helden", new Tool()
            {
                Name = "Helden",
                Icon = "Icons/helden.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.Helden.HeldenView),
                ViewModelType = typeof(ViewModel.Helden.HeldenViewModel)
            });
            ToolListe.Add("Proben", new Tool()
            {
                Name = "Proben",
                Icon = "Icons/Wuerfel/w20.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.Proben.ProbenView),
                ViewModelType = typeof(ViewModel.Proben.ProbenViewModel)
            });
            ToolListe.Add("Kampf", new Tool()
            {
                Name = "Kampf",
                Icon = "Icons/nahkampf_01.png",
                MenuGruppe = "Wege des Kampfes",
                ViewType = typeof(View.Kampf.KampfView),
                ViewModelType = typeof(ViewModel.Kampf.KampfViewModel)
            });
            ToolListe.Add("Gegner", new Tool()
            {
                Name = "Gegner",
                Icon = "Icons/gegner.png",
                MenuGruppe = "Wege des Kampfes",
                ViewType = typeof(View.Kampf.Controls.GegnerView),
                ViewModelType = typeof(ViewModel.Kampf.GegnerViewModel)
            });
            ToolListe.Add("Notizen", new Tool()
            {
                Name = "Notizen",
                Icon = "Icons/notiz.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.Notiz.NotizView),
                ViewModelType = typeof(ViewModel.Notiz.NotizViewModel)
            });
            ToolListe.Add("Kalender", new Tool()
            {
                Name = "Kalender",
                Icon = "Icons/kalender.png",
                MenuGruppe = "Wege des Wanderers",
                ViewType = typeof(View.Kalender.KalenderView),
                ViewModelType = typeof(ViewModel.Kalender.KalenderViewModel)
            });
            ToolListe.Add("NSCs", new Tool()
            {
                Name = "NSCs",
                Icon = "Icons/meisterperson.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.NscGeneratorAlt.NscGeneratorAltView),
                ViewModelType = typeof(ViewModel.NscGeneratorAlt.NscGeneratorAltViewModel)
            });
            ToolListe.Add("Umrechner", new Tool()
            {
                Name = "Umrechner",
                Icon = "Icons/masse.png",
                MenuGruppe = "Wege des Handels",
                ViewType = typeof(View.Umrechner.UmrechnerView),
                ViewModelType = typeof(ViewModel.Umrechner.UmrechnerViewModel)
            });
            ToolListe.Add("Würfel", new Tool()
            {
                Name = "Würfel",
                Icon = "Icons/wuerfelbecher.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.Würfeln.WürfelView),
                ViewModelType = typeof(ViewModel.Würfeln.WürfelViewModel)
            });
            ToolListe.Add("SpielerInfo", new Tool()
            {
                Name = "SpielerInfo",
                Icon = "Icons/General/screen.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.SpielerScreen.SpielerScreenControlView),
                ViewModelType = typeof(ViewModel.SpielerScreen.SpielerScreenControlViewModel)
            });
            ToolListe.Add("Audio", new Tool()
            {
                Name = "Audio",
                Icon = "Icons/General/audio.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.AudioPlayer.AudioPlayerView),
                ViewModelType = typeof(ViewModel.AudioPlayer.AudioPlayerViewModel)
            });
            ToolListe.Add("Basar", new Tool()
            {
                Name = "Basar",
                Icon = "Icons/muenzen.png",
                MenuGruppe = "Wege des Handels",
                ViewType = typeof(View.Basar.BasarView),
                ViewModelType = typeof(ViewModel.Basar.BasarViewModel)
            });
            ToolListe.Add("Schmiede", new Tool()
            {
                Name = "Schmiede",
                Icon = "Icons/schmiede.png",
                MenuGruppe = "Wege des Handels",
                ViewType = typeof(View.Schmiede.SchmiedeView),
                ViewModelType = typeof(ViewModel.Schmiede.SchmiedeViewModel)
            });
            ToolListe.Add("Globus", new Tool()
            {
                Name = "Globus",
                Icon = "Logos/DereGlobus_Icon2.png",
                MenuGruppe = "Wege des Wanderers",
                ViewType = typeof(View.Globus.GlobusView),
                ViewModelType = typeof(ViewModel.Globus.GlobusViewModel)
            });
            ToolListe.Add("Artefakte", new Tool()
            {
                Name = "Artefakte",
                Icon = "Icons/artefakt.png",
                MenuGruppe = "Wege der Magie",
                ViewType = typeof(View.ArtGen.ArtGenView),
                ViewModelType = typeof(ViewModel.ArtGen.ArtGenViewModel)
            });
            //ToolListe.Add("ZooBot", new Tool()
            //{
            //    Name = "ZooBot",
            //    Icon = "Icons/kraeutersuche.png",
            //    MenuGruppe = "Wege des Wanderers",
            //    ViewType = typeof(View.ZooBotAlt.ZooBotView),
            //    ViewModelType = typeof(ViewModel.Helden.HeldenViewModel)
            //});
            ToolListe.Add("Zauberzeichen", new Tool()
            {
                Name = "Zauberzeichen",
                Icon = "Icons/zauberzeichen.png",
                MenuGruppe = "Wege der Magie",
                ViewType = typeof(View.Zauberzeichen.ZauberzeichenView),
                ViewModelType = typeof(ViewModel.Zauberzeichen.ZauberzeichenViewModel)
            });
            ToolListe.Add("Ares", new Tool()
            {
                Name = "Ares",
                Icon = "Logos/Ares_Logo.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.AresPlayer.AresPlayerView),
                ViewModelType = typeof(ViewModel.AresPlayer.AresPlayerViewModel)
            });
            ToolListe.Add("Almanach", new Tool()
            {
                Name = "Almanach",
                Icon = "Icons/hesinde.png",
                MenuGruppe = "Wege des Meisters",
                ViewType = typeof(View.Almanach.AlmanachView),
                ViewModelType = typeof(ViewModel.Almanach.AlmanachViewModel)
            });
            ToolListe.Add("Alchimie", new Tool()
            {
                Name = "Alchimie",
                Icon = "Icons/alchimie.png",
                MenuGruppe = "Wege der Magie",
                ViewType = typeof(View.Alchimie.AlchimieView),
                ViewModelType = typeof(ViewModel.Alchimie.AlchimieViewModel)
            });
            ToolListe.Add("Beschwörung", new Tool()
            {
                Name = "Beschwörung",
                Icon = "Icons/magie.png",
                MenuGruppe = "Wege der Magie",
                ViewType = typeof(View.Beschwörung.BeschwörungView),
                ViewModelType = typeof(ViewModel.Beschwörung.BeschwörungViewModel)
            });
            //ToolListe.Add("Reise", new Tool()
            //{
            //    Name = "Reise",
            //    Icon = "Icons/kartenzeichnen.png",
            //    MenuGruppe = "Wege des Wanderers",
            //    ViewType = typeof(View.Reise.ReiseView)
            //});
            ToolListe.Add("Karte", new Tool()
            {
                Name = "Karte",
                Icon = "Icons/kartenzeichnen.png",
                MenuGruppe = "Wege des Wanderers",
                ViewType = typeof(View.Karte.KarteView),
                ViewModelType = typeof(ViewModel.Karte.KarteViewModel)
            });
            if (Global.INTERN) // im Release-Modus ausblenden
            {
                ToolListe.Add("ZooBotNeu", new Tool()
                {
                    Name = "ZooBot (neu)",
                    Icon = "Icons/kraeutersuche.png",
                    MenuGruppe = "Wege des Wanderers",
                    ViewType = typeof(View.ZooBot.ZooBotView),
                    ViewModelType = typeof(ViewModel.ZooBot.ZooBotViewModel)
                });
                ToolListe.Add("Abenteuer", new Tool()
                {
                    Name = "Abenteuer",
                    Icon = "Icons/meistertools_02.png",
                    MenuGruppe = "Wege des Meisters",
                    ViewType = typeof(View.Abenteuer.AbenteuerView),
                    ViewModelType = typeof(ViewModel.Abenteuer.AbenteuerViewModel)
                });
                ToolListe.Add("Generator", new Tool()
                {
                    Name = "Generator",
                    Icon = "Icons/meisterperson.png",
                    MenuGruppe = "Wege des Meisters",
                    ViewType = typeof(View.Generator.GeneratorView),
                    ViewModelType = typeof(ViewModel.Generator.GeneratorViewModel)
                });
            }
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

        public ToolViewModelBase CreateToolViewModel()
        {
            ToolViewModelBase toolControl = null;

            object toolObject = Activator.CreateInstance(ViewModelType);
            if (toolObject is ToolViewModelBase)
                toolControl = (ToolViewModelBase)toolObject;
            toolControl.Tool = this;
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
        /// Typ des Standard Views.
        /// </summary>
        public Type ViewType { get; private set; }

        /// <summary>
        /// Typ des ViewModels.
        /// </summary>
        public Type ViewModelType { get; private set; }

        /// <summary>
        /// Gibt an unter welchem Menü-Punkt das Tool erscheinen soll.
        /// </summary>
        public string MenuGruppe { get; private set; }

        #endregion
    }

}
