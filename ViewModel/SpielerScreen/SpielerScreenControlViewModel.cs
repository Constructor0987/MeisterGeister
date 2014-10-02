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
        private string _bildschirmInfo = "1 Bildschirm";
        private string _filePath = string.Empty;
        private string _imageFile = string.Empty;
        private BitmapImage _selectedImage = null;
        private bool _pathNotFound = true;

        // Listen
        private List<System.Windows.Forms.Screen> _screenList = System.Windows.Forms.Screen.AllScreens.ToList();
        private List<dynamic> _images = null;

        #endregion

        #region //---- EIGENSCHAFTEN ----

        public string BildschirmInfo
        {
            get { return _bildschirmInfo; }
        }

        public bool NurEinMonitor
        {
            get { return ScreenList.Count <= 1; }
        }

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnChanged("FilePath");
            }
        }

        public string ImageFile
        {
            get { return _imageFile; }
            set
            {
                _imageFile = value;
                OnChanged("ImageFile");
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
        public SpielerScreenControlViewModel(Action<string> popup, Func<string, string, bool> confirm, Func<string, string, int> confirmYesNoCancel, Func<string, string, bool, bool, string[], string> chooseFile, Action<string, Exception> showError) :
            base(popup, confirm, confirmYesNoCancel, chooseFile, showError)
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
            FilePath = Logic.Einstellung.Einstellungen.SpielerInfoBilderPfad;
            LoadImagesFromDir(FilePath);
        }

        private void OpenImage(object sender)
        {
            string pfad = ChooseFile("Bild auswähllen", "", false, false, Logic.Extensions.FileExtensions.EXTENSIONS_IMAGES);
            if (!String.IsNullOrEmpty(pfad))
                LoadImage(pfad);
        }

        private void SpielerInfoClose(object sender)
        {
            SpielerWindow.Hide();
        }

        public void LoadImage(string file)
        {
            FilePath = file;
            ImageFile = FilePath;
            try
            {
                // Bild
                SelectImage(ImageFile);
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