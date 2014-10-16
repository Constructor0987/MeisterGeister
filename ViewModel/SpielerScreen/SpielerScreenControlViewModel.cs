using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
//Eigene usings
using MeisterGeister.ViewModel.Almanach.Logic;
using Base = MeisterGeister.ViewModel.Base;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MeisterGeister.View.SpielerScreen;

namespace MeisterGeister.ViewModel.SpielerScreen
{
    public class SpielerScreenControlViewModel : Base.ViewModelBase
    {
        #region //---- FELDER ----

        // Felder
        private string _textToShow = string.Empty;
        private string _bildschirmInfo = "1 Bildschirm";
        private string _directoryPath = string.Empty;
        private string _selectedImagePath = string.Empty;
        private BitmapImage _selectedImage = null;
        private bool _pathNotFound = true;
        private bool _isImageStretch = true;

        // Listen
        private List<System.Windows.Forms.Screen> _screenList = System.Windows.Forms.Screen.AllScreens.ToList();
        private List<dynamic> _images = null;

        #endregion

        #region //---- EIGENSCHAFTEN ----

        public string TextToShow
        {
            get { return _textToShow; }
            set
            {
                _textToShow = value;
                OnChanged("TextToShow");
            }
        }

        public string BildschirmInfo
        {
            get { return _bildschirmInfo; }
        }

        public bool NurEinMonitor
        {
            get { return ScreenList.Count <= 1; }
        }

        public string DirectoryPath
        {
            get { return _directoryPath; }
            set
            {
                _directoryPath = value;
                OnChanged("DirectoryPath");
            }
        }

        public string SelectedImagePath
        {
            get { return _selectedImagePath; }
            set
            {
                _selectedImagePath = value;
                OnChanged("SelectedImagePath");
            }
        }

        public BitmapImage SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
                OnChanged("SelectedImage");
            }
        }

        public bool PathNotFound
        {
            get { return _pathNotFound; }
            set
            {
                _pathNotFound = value;
                OnChanged("PathNotFound");
            }
        }

        public bool IsImageStretch
        {
            get { return _isImageStretch; }
            set
            {
                _isImageStretch = value;
                OnChanged("IsImageStretch");
            }
        }

        public List<System.Windows.Forms.Screen> ScreenList
        {
            get { return _screenList; }
        }

        public List<dynamic> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                OnChanged("Images");
            }
        }

        #endregion

        #region //---- COMMANDS ----

        private Base.CommandBase onReLoadImages = null;
        public Base.CommandBase OnReLoadImages
        {
            get
            {
                if (onReLoadImages == null)
                    onReLoadImages = new Base.CommandBase(ReLoadImages, null);
                return onReLoadImages;
            }
        }

        private Base.CommandBase onOpenImage = null;
        public Base.CommandBase OnOpenImage
        {
            get
            {
                if (onOpenImage == null)
                    onOpenImage = new Base.CommandBase(OpenImage, null);
                return onOpenImage;
            }
        }

        private Base.CommandBase onSpielerInfoClose = null;
        public Base.CommandBase OnSpielerInfoClose
        {
            get
            {
                if (onSpielerInfoClose == null)
                    onSpielerInfoClose = new Base.CommandBase(SpielerInfoClose, null);
                return onSpielerInfoClose;
            }
        }

        private Base.CommandBase onSpielerInfoOpen = null;
        public Base.CommandBase OnSpielerInfoOpen
        {
            get
            {
                if (onSpielerInfoOpen == null)
                    onSpielerInfoOpen = new Base.CommandBase(SpielerInfoOpen, null);
                return onSpielerInfoOpen;
            }
        }

        private Base.CommandBase onOpenDirectory = null;
        public Base.CommandBase OnOpenDirectory
        {
            get
            {
                if (onOpenDirectory == null)
                    onOpenDirectory = new Base.CommandBase(OpenDirectory, null);
                return onOpenDirectory;
            }
        }

        private Base.CommandBase onShowKampf = null;
        public Base.CommandBase OnShowKampf
        {
            get
            {
                if (onShowKampf == null)
                    onShowKampf = new Base.CommandBase(ShowKampf, null);
                return onShowKampf;
            }
        }

        private Base.CommandBase onShowBodenplan = null;
        public Base.CommandBase OnShowBodenplan
        {
            get
            {
                if (onShowBodenplan == null)
                    onShowBodenplan = new Base.CommandBase(ShowBodenplan, null);
                return onShowBodenplan;
            }
        }

        private Base.CommandBase onShowText = null;
        public Base.CommandBase OnShowText
        {
            get
            {
                if (onShowText == null)
                    onShowText = new Base.CommandBase(ShowText, null);
                return onShowText;
            }
        }

        private Base.CommandBase onShowImage = null;
        public Base.CommandBase OnShowImage
        {
            get
            {
                if (onShowImage == null)
                    onShowImage = new Base.CommandBase(ShowImage, null);
                return onShowImage;
            }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public SpielerScreenControlViewModel()
        {
            Init();
        }

        ///// <summary>
        ///// ViewModel mit Callbacks.
        ///// </summary>
        ///// <param name="popup">Ein OK-Popup-Dialog. (Nachricht)</param>
        ///// <param name="confirm">Bestätigung einer Ja-Nein-Frage. (Fenstertitel, Frage)</param>
        ///// <param name="confirmYesNoCancel">Bestätigen eines YesNoCancel-Dialoges (cancel=0, no=1, yes=2). (Fenstertitel, Frage)</param>
        ///// <param name="chooseFile">Wahl einer Datei. (Fenstertitel, Dateiname, zum speichern, Dateierweiterungen ...)</param>
        public SpielerScreenControlViewModel(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, bool, string[], string> chooseFile, Func<string, bool, string> chooseDirectory, Action<string, Exception> showError) :
            base(popup, confirm, confirmYesNoCancel, chooseFile, chooseDirectory, showError)
        {
            Init();
        }

        #endregion

        #region //---- METHODEN ----

        public void Refresh()
        {

        }

        public void Init()
        {
            // Bildschirminfos
            _bildschirmInfo = string.Format("{0} Bildschirm{1}", ScreenList.Count, ScreenList.Count == 1 ? string.Empty : "e");

            // Letzten Bilderpfad laden
            DirectoryPath = Logic.Einstellung.Einstellungen.SpielerInfoBilderPfad;
            LoadImagesFromDir(DirectoryPath);
        }

        private void OpenImage(object sender = null)
        {
            string pfad = ChooseFile("Bild auswähllen", "", false, false, Logic.Extensions.FileExtensions.EXTENSIONS_IMAGES);
            if (!String.IsNullOrEmpty(pfad))
                LoadImage(pfad);
        }

        private void OpenDirectory(object sender = null)
        {
            string path = ChooseDirectory(Logic.Einstellung.Einstellungen.SpielerInfoBilderPfad, true);

            Logic.Einstellung.Einstellungen.SpielerInfoBilderPfad = path;
            DirectoryPath = path;
            LoadImagesFromDir(path);
        }

        public void SpielerInfoClose(object sender = null)
        {
            SpielerWindow.Hide();
        }

        public void SpielerInfoOpen(object sender = null)
        {
            SpielerWindow.ReOpen();
        }

        public void ShowKampf(object sender = null)
        {
            SpielerWindow.SetKampfInfoView();
        }

        public void ShowBodenplan(object sender = null)
        {
            SpielerWindow.SetBodenplanView();
        }

        public void ShowImage(object sender = null)
        {
            SpielerWindow.SetImage(SelectedImagePath, (IsImageStretch == true) ? Stretch.Uniform : Stretch.None);
        }

        public void ShowText(object sender = null)
        {
            SpielerWindow.SetText(TextToShow);
        }

        public void LoadImage(string file)
        {
            SelectedImagePath = file;
            FileInfo fInfo = new FileInfo(file);
            DirectoryPath = fInfo.DirectoryName;
            try
            {
                // Bild
                SelectImage(SelectedImagePath);
            }
            catch
            {
                PopUp("Laden des Bildes fehlgeschlagen!");
            }
        }

        private void SelectImage(string path)
        {
            System.Windows.Threading.DispatcherOperation op =
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate()
                {
                    try
                    {
                        BitmapImage bmi = new BitmapImage();
                        bmi.BeginInit();
                        bmi.CacheOption = BitmapCacheOption.OnLoad;
                        bmi.UriSource = new Uri(path, UriKind.Relative);
                        bmi.EndInit();

                        bmi.Freeze();		// freeze image source, used to move it across the thread
                        SelectedImage = bmi;
                    }
                    catch (Exception)
                    {
                        PopUp("Bild konnte nicht geladn werden:\n" + path);
                    }
                });
        }

        private void ReLoadImages(object sender = null)
        {
            LoadImagesFromDir(DirectoryPath);
        }

        public void LoadImagesFromDir(string pfad)
        {
            if (string.IsNullOrWhiteSpace(pfad) || !Directory.Exists(pfad))
            {
                PathNotFound = true;
                return;
            }

            PathNotFound = false;

            string[] filesBmp = Directory.GetFiles(pfad, "*.bmp");
            string[] filesGif = Directory.GetFiles(pfad, "*.gif");
            string[] filesJpg = Directory.GetFiles(pfad, "*.jpg");
            string[] filesJpeg = Directory.GetFiles(pfad, "*.jpeg");
            string[] filesJpe = Directory.GetFiles(pfad, "*.jpe");
            string[] filesJfif = Directory.GetFiles(pfad, "*.jfif");
            string[] filesPng = Directory.GetFiles(pfad, "*.png");
            string[] filesTif = Directory.GetFiles(pfad, "*.tif");
            string[] filesTiff = Directory.GetFiles(pfad, "*.tiff");

            List<dynamic> fileList = new List<dynamic>();
            AddImages(fileList, filesBmp);
            AddImages(fileList, filesBmp);
            AddImages(fileList, filesGif);
            AddImages(fileList, filesJpg);
            AddImages(fileList, filesJpeg);
            AddImages(fileList, filesJpe);
            AddImages(fileList, filesJfif);
            AddImages(fileList, filesPng);
            AddImages(fileList, filesTif);
            AddImages(fileList, filesTiff);

            Images = fileList.OrderBy(img => img.Name).ToList();
        }

        private void AddImages(List<dynamic> fileList, string[] files)
        {
            foreach (string file in files)
                fileList.Add(new { Pfad = file, Name = Path.GetFileNameWithoutExtension(file) });
        }

        #endregion
    }
}