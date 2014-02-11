using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MeisterGeister.View.General
{
    /// <summary>
    /// Interaktionslogik für SelectImageDialog.xaml
    /// </summary>
    public partial class SelectImageDialog : Window
    {
        public SelectImageDialog()
        {
            InitializeComponent();
            VM = new SelectImageDialogViewModel(ViewHelper.InputDialog, ViewHelper.ChooseFile);
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public SelectImageDialogViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is SelectImageDialogViewModel))
                    return null;
                return DataContext as SelectImageDialogViewModel;
            }
            set { DataContext = value; }
        }

        public string SelectedPath
        {
            get { return VM.SelectedPath; }
            set { VM.SelectedPath = value; }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ImageHerokonOnline_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.herokon-online.com/");
        }

        private void ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender != null && !(sender is ListBox))
                return;

            ListBox listBox = sender as ListBox;
            string key = e.Key.ToString();
            key = key.Replace("NumPad", string.Empty)
                .Replace("OemQuotes", "Ä").Replace("Oem1", "Ü").Replace("Oem3", "Ö");
            if (key.Length == 2 && key.StartsWith("D"))
                key = key.Replace("D", string.Empty);
            try
            {
                foreach (dynamic item in listBox.Items)
                {
                    if (item.Name.StartsWith(key))
                    {
                        listBox.SelectedItem = item;
                        listBox.ScrollIntoView(item);
                        break;
                    }
                }
            }
            catch (Exception) { }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedPath = string.Empty;
            DialogResult = true;
        }

        private void WrapPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && e.LeftButton == MouseButtonState.Pressed)
                DialogResult = true;
        }
    }

    /// <summary>
    /// ViewModel für SelectImageDialog.
    /// </summary>
    public class SelectImageDialogViewModel : ViewModel.Base.ViewModelBase
    {
        public SelectImageDialogViewModel(Func<string, string, string, string> chooseWebLink, Func<string, string, bool, bool, string[], string> chooseFile)
            : base(null, null, null, chooseFile, null)
        {
            this.chooseWebLink = chooseWebLink;

            InitRessourceImageList();

        }

        private void InitRessourceImageList()
        {
            RessourceImageList = new List<dynamic>();
            RessourceImageList.AddRange(new dynamic[] {
                new { Name = "Achaz",               Path = "/DSA MeisterGeister;component/Images/Wesen/achaz.png" },
                new { Name = "Adler",               Path = "/DSA MeisterGeister;component/Images/Wesen/adler.png" },
                new { Name = "Affenmensch",         Path = "/DSA MeisterGeister;component/Images/Wesen/affenmensch.png" },
                new { Name = "Alligator",           Path = "/DSA MeisterGeister;component/Images/Wesen/alligator.png" },
                new { Name = "Bär",                 Path = "/DSA MeisterGeister;component/Images/Wesen/baer.png" },
                new { Name = "Barbar",              Path = "/DSA MeisterGeister;component/Images/Wesen/barbar.png" },
                new { Name = "Basilisk",            Path = "/DSA MeisterGeister;component/Images/Wesen/basilisk.png" },
                new { Name = "Baumwürger",          Path = "/DSA MeisterGeister;component/Images/Wesen/baumwürger.png" },
                new { Name = "Bodirwurm",           Path = "/DSA MeisterGeister;component/Images/Wesen/bodirwurm.png" },
                new { Name = "Borbaradmoskito",     Path = "/DSA MeisterGeister;component/Images/Wesen/borbaradmoskito.png" },
                new { Name = "Bürger",              Path = "/DSA MeisterGeister;component/Images/Wesen/buerger.png" },
                new { Name = "Dämon",               Path = "/DSA MeisterGeister;component/Images/Wesen/daemon.png" },
                new { Name = "Drache",              Path = "/DSA MeisterGeister;component/Images/Wesen/drache.png" },
                new { Name = "Elementar-Eis",       Path = "/DSA MeisterGeister;component/Images/Wesen/elementar_eis.png" },
                new { Name = "Elementar-Erz",       Path = "/DSA MeisterGeister;component/Images/Wesen/elementar_erz.png" },
                new { Name = "Elementar-Feuer",     Path = "/DSA MeisterGeister;component/Images/Wesen/elementar_feuer.png" },
                new { Name = "Elementar-Humus",     Path = "/DSA MeisterGeister;component/Images/Wesen/elementar_humus.png" },
                new { Name = "Elementar-Luft",      Path = "/DSA MeisterGeister;component/Images/Wesen/elementar_luft.png" },
                new { Name = "Elementar-Wasser",    Path = "/DSA MeisterGeister;component/Images/Wesen/elementar_wasser.png" },
                new { Name = "Elf",                 Path = "/DSA MeisterGeister;component/Images/Wesen/elf.png" },
                new { Name = "Falke",               Path = "/DSA MeisterGeister;component/Images/Wesen/falke.png" },
                new { Name = "Ferkina",             Path = "/DSA MeisterGeister;component/Images/Wesen/ferkina.png" },
                new { Name = "Feuermolch",          Path = "/DSA MeisterGeister;component/Images/Wesen/feuermolch.png" },
                new { Name = "Fledermaus",          Path = "/DSA MeisterGeister;component/Images/Wesen/fledermaus.png" },
                new { Name = "Frostwurm",           Path = "/DSA MeisterGeister;component/Images/Wesen/frostwurm.png" },
                new { Name = "Gardist",             Path = "/DSA MeisterGeister;component/Images/Wesen/gardist.png" },
                new { Name = "Gargyl",              Path = "/DSA MeisterGeister;component/Images/Wesen/gargyl.png" },
                new { Name = "Gepard",              Path = "/DSA MeisterGeister;component/Images/Wesen/gepard.png" },
                new { Name = "Ghul",                Path = "/DSA MeisterGeister;component/Images/Wesen/ghul.png" },
                new { Name = "Gletscherwurm",       Path = "/DSA MeisterGeister;component/Images/Wesen/gletscherwurm.png" },
                new { Name = "Goblin",              Path = "/DSA MeisterGeister;component/Images/Wesen/goblin.png" },
                new { Name = "Golem",               Path = "/DSA MeisterGeister;component/Images/Wesen/golem.png" },
                new { Name = "Greif",               Path = "/DSA MeisterGeister;component/Images/Wesen/greif.png" },
                new { Name = "Greifkatze",          Path = "/DSA MeisterGeister;component/Images/Wesen/greifkatze.png" },
                new { Name = "Grolm",               Path = "/DSA MeisterGeister;component/Images/Wesen/grolm.png" },
                new { Name = "Grubenwurm",          Path = "/DSA MeisterGeister;component/Images/Wesen/grubenwurm.png" },
                new { Name = "Gruftassel",          Path = "/DSA MeisterGeister;component/Images/Wesen/gruftassel.png" },
                new { Name = "Grünschrecke",        Path = "/DSA MeisterGeister;component/Images/Wesen/grünschrecke.png" },
                new { Name = "Halbelf",             Path = "/DSA MeisterGeister;component/Images/Wesen/halbelf.png" },
                new { Name = "Harpyie",             Path = "/DSA MeisterGeister;component/Images/Wesen/harpie.png" },
                new { Name = "Heshthot",            Path = "/DSA MeisterGeister;component/Images/Wesen/heshtot.png" },
                new { Name = "Hippogriff",          Path = "/DSA MeisterGeister;component/Images/Wesen/hippogriff.png" },
                new { Name = "Hund",                Path = "/DSA MeisterGeister;component/Images/Wesen/hund.png" },
                new { Name = "Jaguar",              Path = "/DSA MeisterGeister;component/Images/Wesen/jaguar.png" },
                new { Name = "Kaiman",              Path = "/DSA MeisterGeister;component/Images/Wesen/kaiman.png" },
                new { Name = "Katze",               Path = "/DSA MeisterGeister;component/Images/Wesen/katze.png" },
                new { Name = "Khoramsbestie",       Path = "/DSA MeisterGeister;component/Images/Wesen/khoramsbestie.png" },
                new { Name = "Klippechse",          Path = "/DSA MeisterGeister;component/Images/Wesen/klippechse.png" },
                new { Name = "Kobold",              Path = "/DSA MeisterGeister;component/Images/Wesen/kobold.png" },
                new { Name = "Kobra",               Path = "/DSA MeisterGeister;component/Images/Wesen/kobra.png" },
                new { Name = "Krakenmolch",         Path = "/DSA MeisterGeister;component/Images/Wesen/krakenmolch.png" },
                new { Name = "Löwe",                Path = "/DSA MeisterGeister;component/Images/Wesen/loewe.png" },
                new { Name = "Magier",              Path = "/DSA MeisterGeister;component/Images/Wesen/magier.png" },
                new { Name = "Malmer",              Path = "/DSA MeisterGeister;component/Images/Wesen/malmer.png" },
                new { Name = "Maraskantarantel",    Path = "/DSA MeisterGeister;component/Images/Wesen/maraskantarantel.png" },
                new { Name = "Maru",                Path = "/DSA MeisterGeister;component/Images/Wesen/maru.png" },
                new { Name = "Minotaurus",          Path = "/DSA MeisterGeister;component/Images/Wesen/minotaurus.png" },
                new { Name = "Morfu",               Path = "/DSA MeisterGeister;component/Images/Wesen/morfu.png" },
                new { Name = "Necker",              Path = "/DSA MeisterGeister;component/Images/Wesen/necker.png" },
                new { Name = "Oger",                Path = "/DSA MeisterGeister;component/Images/Wesen/oger.png" },
                new { Name = "Ork",                 Path = "/DSA MeisterGeister;component/Images/Wesen/ork.png" },
                new { Name = "Panther",             Path = "/DSA MeisterGeister;component/Images/Wesen/panther.png" },
                new { Name = "Pferd",               Path = "/DSA MeisterGeister;component/Images/Wesen/pferd.png" },
                new { Name = "Pirat",               Path = "/DSA MeisterGeister;component/Images/Wesen/pirat.png" },
                new { Name = "Purpurwurm",          Path = "/DSA MeisterGeister;component/Images/Wesen/purpurwurm.png" },
                new { Name = "Ratte",               Path = "/DSA MeisterGeister;component/Images/Wesen/ratte.png" },
                new { Name = "Riese",               Path = "/DSA MeisterGeister;component/Images/Wesen/riese.png" },
                new { Name = "Riesenameise",        Path = "/DSA MeisterGeister;component/Images/Wesen/riesenameise.png" },
                new { Name = "Riesenamoebe",        Path = "/DSA MeisterGeister;component/Images/Wesen/riesenamoebe.png" },
                new { Name = "Riesenhirschkäfer",   Path = "/DSA MeisterGeister;component/Images/Wesen/riesenhirschkäfer.png" },
                new { Name = "Riesenlindwurm",      Path = "/DSA MeisterGeister;component/Images/Wesen/Riesenlindwurm.png" },
                new { Name = "Risso",               Path = "/DSA MeisterGeister;component/Images/Wesen/risso.png" },
                new { Name = "Roter Maran",         Path = "/DSA MeisterGeister;component/Images/Wesen/roter_maran.png" },
                new { Name = "Schlange",            Path = "/DSA MeisterGeister;component/Images/Wesen/schlange.png" },
                new { Name = "Schneelaurer",        Path = "/DSA MeisterGeister;component/Images/Wesen/schneelaurer.png" },
                new { Name = "Schrat",              Path = "/DSA MeisterGeister;component/Images/Wesen/schrat.png" },
                new { Name = "Skelett",             Path = "/DSA MeisterGeister;component/Images/Wesen/skelett.png" },
                new { Name = "Skorpion",            Path = "/DSA MeisterGeister;component/Images/Wesen/skorpion.png" },
                new { Name = "Söldner",             Path = "/DSA MeisterGeister;component/Images/Wesen/soeldner.png" },
                new { Name = "Spinne",              Path = "/DSA MeisterGeister;component/Images/Wesen/spinne.png" },
                new { Name = "Strauß",              Path = "/DSA MeisterGeister;component/Images/Wesen/strauss.png" },
                new { Name = "Sumpfechse",          Path = "/DSA MeisterGeister;component/Images/Wesen/sumpfechse.png" },
                new { Name = "Sumpfranze",          Path = "/DSA MeisterGeister;component/Images/Wesen/sumpfranze.png" },
                new { Name = "Tatzelwurm",          Path = "/DSA MeisterGeister;component/Images/Wesen/tatzelwurm.png" },
                new { Name = "Tiger",               Path = "/DSA MeisterGeister;component/Images/Wesen/tiger.png" },
                new { Name = "Troll",               Path = "/DSA MeisterGeister;component/Images/Wesen/troll.png" },
                new { Name = "Trollzacker",         Path = "/DSA MeisterGeister;component/Images/Wesen/trollzacker.png" },
                new { Name = "Vampir",              Path = "/DSA MeisterGeister;component/Images/Wesen/vampir.png" },
                new { Name = "Wegelagerer",         Path = "/DSA MeisterGeister;component/Images/Wesen/wegelagerer.png" },
                new { Name = "Wildschwein",         Path = "/DSA MeisterGeister;component/Images/Wesen/wildschwein.png" },
                new { Name = "Wolf",                Path = "/DSA MeisterGeister;component/Images/Wesen/wolf.png" },
                new { Name = "Yeti",                Path = "/DSA MeisterGeister;component/Images/Wesen/yeti.png" },
                new { Name = "Zant",                Path = "/DSA MeisterGeister;component/Images/Wesen/zant.png" },
                new { Name = "Zombie",              Path = "/DSA MeisterGeister;component/Images/Wesen/zombie.png" },
                new { Name = "Zwerg",               Path = "/DSA MeisterGeister;component/Images/Wesen/zwerg.png" },
                new { Name = "Zyklop",              Path = "/DSA MeisterGeister;component/Images/Wesen/zyklop.png" }
            });
        }

        public List<dynamic> RessourceImageList { get; set; }

        public string _selectedPath = null;
        public string SelectedPath 
        {
            get { return _selectedPath; }
            set { _selectedPath = value; OnChanged("SelectedPath"); }
        }

        public string _selectedPathDatei = null;
        public string SelectedPathDatei
        {
            get { return _selectedPathDatei; }
            set 
            { 
                _selectedPathDatei = value;
                SelectedPath = value;
                OnChanged("SelectedPathDatei"); 
            }
        }

        public string _selectedPathWeb = null;
        public string SelectedPathWeb
        {
            get { return _selectedPathWeb; }
            set
            {
                _selectedPathWeb = value;
                SelectedPath = value;
                OnChanged("SelectedPathWeb");
            }
        }

        public dynamic _selectedRessource = null;
        public dynamic SelectedRessource
        {
            get { return _selectedRessource; }
            set
            {
                _selectedRessource = value;
                SelectedPath = value != null ? value.Path : null;
                OnChanged("SelectedRessource");
            }
        }

        private bool _isDatei = false;
        public bool IsDatei
        {
            get { return _isDatei; }
            set
            {
                _isDatei = value;
                if (value)
                {
                    _isWebLink = false;
                    _isRessource = false;
                }
                SelectedPath = SelectedPathDatei;
                OnChanged("IsDatei");
                OnChanged("IsWebLink");
                OnChanged("IsRessource");
            }
        }

        private bool _isWebLink = false;
        public bool IsWebLink
        {
            get { return _isWebLink; }
            set
            {
                _isWebLink = value;
                if (value)
                {
                    _isDatei = false;
                    _isRessource = false;
                }
                SelectedPath = SelectedPathWeb;
                OnChanged("IsDatei");
                OnChanged("IsWebLink");
                OnChanged("IsRessource");
            }
        }

        private bool _isRessource = true;
        public bool IsRessource
        {
            get { return _isRessource; }
            set
            {
                _isRessource = value;
                if (value)
                {
                    _isDatei = false;
                    _isWebLink = false;
                }
                SelectedPath = SelectedRessource != null ? SelectedRessource.Path : null;
                OnChanged("IsDatei");
                OnChanged("IsWebLink");
                OnChanged("IsRessource");
            }
        }

        #region // ---- COMMANDS ----

        private ViewModel.Base.CommandBase onChooseFile = null;
        public ViewModel.Base.CommandBase OnChooseFile
        {
            get
            {
                if (onChooseFile == null)
                    onChooseFile = new ViewModel.Base.CommandBase(ChoosePathFromFile, null);
                return onChooseFile;
            }
        }

        private void ChoosePathFromFile(object obj)
        {
            string pfad = ChooseFile("Bild aus Datei", "", false, true, "bmp", "gif", "jpg", "jpeg", "jpe", "jfif", "png", "tif", "tiff");
            if (pfad != null)
                SelectedPathDatei = pfad;
        }

        private ViewModel.Base.CommandBase onChooseWebLink = null;
        public ViewModel.Base.CommandBase OnChooseWebLink
        {
            get
            {
                if (onChooseWebLink == null)
                    onChooseWebLink = new ViewModel.Base.CommandBase(ChoosePathFromWeb, null);
                return onChooseWebLink;
            }
        }

        private void ChoosePathFromWeb(object obj)
        {
            if (chooseWebLink != null)
            {
                string pfad = chooseWebLink("Bild-Link vom Web", "Bitte den vollständigen Web-Link zum Bild angeben.", string.Empty);
                if (pfad != null)
                    SelectedPathWeb = pfad;
            }
        }

        private Func<string, string, string, string> chooseWebLink;

        #endregion // ---- COMMANDS ----
        
    }
}
