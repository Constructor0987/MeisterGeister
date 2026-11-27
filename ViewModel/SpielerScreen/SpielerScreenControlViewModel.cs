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
using MeisterGeister.Logic.Einstellung;
using System.Windows;
using MeisterGeister.ViewModel.Karte;
using MeisterGeister.ViewModel.Base;

namespace MeisterGeister.ViewModel.SpielerScreen
{
    public class SpielerScreenControlViewModel : Base.SingletonToolViewModelBase<SpielerScreenControlViewModel>
    {
        #region //---- FELDER ----

        // Felder
        private bool _isInSlideShowAll = true;
        private bool _isKampfInfoShow = false;
        private string _textToShow = string.Empty;
        private string _bildschirmInfo = "x Bildschirm";
        private string _directoryPath = string.Empty;
        private string _selectedImagePath = string.Empty;
        private string _currentSlideShowImage = string.Empty;
        private BitmapImage _selectedImage = null;
        private MediaItem _selectedMediaObject = null;
        private Uri _selectedVideo = null;
        private bool _videoIsMuted = false;
        private MediaState _mediaPlayerLoadedBehavior = MediaState.Manual;
        private MediaState _mediaPlayerSpielerScreenLoadedBehavior = MediaState.Manual;
        private bool _pathNotFound = true;
        private bool _isImageStretch = true;
        private bool _slideShowRunning = false;
        private double _slideShowInterval = 6.0;
        private double _pointerDurchmesser = 25.0;

        // Listen
        private List<System.Windows.Forms.Screen> _screenList = System.Windows.Forms.Screen.AllScreens.ToList();
        private List<MediaItem> _images = null;

        #endregion

        #region //---- EIGENSCHAFTEN ----

        public bool IsKampfInfoShow
        {
            get { return _isKampfInfoShow; }
            set {
                Set(ref _isKampfInfoShow, value); }
        }

        public bool IsInSlideShowAll
        {
            get { return _isInSlideShowAll; }
            set
            {
                _isInSlideShowAll = value;
                OnChanged("IsInSlideShowAll");
            }
        }

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
                LoadMediaFromDir(_directoryPath);
                OnChanged("DirectoryPath");
            }
        }

        public string SelectedImagePath
        {
            get { return _selectedImagePath; }
            set
            {
                _selectedImagePath = value;
                LoadImage();
                OnChanged("SelectedImagePath");
            }
        }

        public string CurrentSlideShowImage
        {
            get { return _currentSlideShowImage; }
            set
            {
                _currentSlideShowImage = value;
                OnChanged("CurrentSlideShowImage");
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

        public Uri SelectedVideo
        {
            get { return _selectedVideo; }
            set
            {
                _selectedVideo = value;
                OnChanged("SelectedVideo");
            }
        }

        public bool VideoIsMuted
        {
            get { return _videoIsMuted; }
            set
            {
                _videoIsMuted = value;
                OnChanged("VideoIsMuted");
            }
        }

        public MediaState MediaPlayerLoadedBehavior
        {
            get { return _mediaPlayerLoadedBehavior; }
            set
            {
                _mediaPlayerLoadedBehavior = value;
                OnChanged("MediaPlayerLoadedBehavior");
            }
        }

        public MediaState MediaPlayerSpielerScreenLoadedBehavior
        {
            get { return _mediaPlayerSpielerScreenLoadedBehavior; }
            set
            {
                _mediaPlayerSpielerScreenLoadedBehavior = value;
                OnChanged("MediaPlayerSpielerScreenLoadedBehavior");
            }
        }

        public MediaItem SelectedMediaObject
        {
            get { return _selectedMediaObject; }
            set
            {
                _selectedMediaObject = value;
                if (value != null)
                    SelectedImagePath = value.Pfad;
                OnChanged("SelectedMediaObject");
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

        public bool IsUnterordnerEinbeziehen
        {
            get { return Einstellungen.SpielerScreenUnterordnerEinbeziehen; }
            set
            {
                Einstellungen.SpielerScreenUnterordnerEinbeziehen = value;
                ReLoadMedia();
                OnChanged("IsUnterordnerEinbeziehen");
            }
        }

        private bool _isPointerVisible = false;
        public bool IsPointerVisible
        {
            get { return _isPointerVisible; }
            set
            {
                _isPointerVisible = value;
                PointerVisibility = value == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                OnChanged("IsPointerVisible");
            }
        }

        private System.Windows.Visibility _pointerVisibility = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility PointerVisibility
        {
            get { return _pointerVisibility; }
            set
            {
                _pointerVisibility = value;
                OnChanged("PointerVisibility");
            }
        }

        public bool SlideShowRunning
        {
            get { return _slideShowRunning; }
            set
            {
                _slideShowRunning = value;
                OnChanged("SlideShowRunning");
                OnChanged("SlideShowStopped");
            }
        }

        public double SlideShowInterval
        {
            get { return _slideShowInterval; }
            set
            {
                if (value <= 0)
                    value = 0.5;
                _slideShowInterval = value;
                if (!SlideShowRunning)
                    _slideShowTimer.Interval = _slideShowInterval * 1000;
                Logic.Einstellung.Einstellungen.SlideShowInterval = value;
                OnChanged("SlideShowInterval");
            }
        }

        public double PointerDurchmesser
        {
            get
            {
                return _pointerDurchmesser;
            }
            set
            {
                _pointerDurchmesser = value;
                OnChanged("PointerDurchmesser");
            }
        }

        public bool SlideShowStopped
        {
            get { return !_slideShowRunning; }
        }

        public List<System.Windows.Forms.Screen> ScreenList
        {
            get { return _screenList; }
        }

        private System.Windows.Forms.Screen _spielerScreen = null;
        public System.Windows.Forms.Screen SpielerScreen
        {
            get
            {
                if (_spielerScreen == null)
                {
                    if (ScreenList.Count <= 1)
                        _spielerScreen = ScreenList.FirstOrDefault();
                    else
                    {
                        foreach (System.Windows.Forms.Screen objActualScreen in ScreenList)
                        {
                            if (!objActualScreen.Primary)
                                _spielerScreen = objActualScreen;
                        }
                    }
                }
                return _spielerScreen;
            }
        }

        public List<MediaItem> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                OnChanged("Images");
            }
        }

        private List<MediaItem> _filteredImages = null;
        public List<MediaItem> FilteredImages
        {
            get { return _filteredImages; }
            set
            {
                _filteredImages = value;
                OnChanged("FilteredImages");
            }
        }

        private string _suchText = string.Empty;
        public string SuchText
        {
            get { return _suchText; }
            set
            {
                _suchText = value;
                OnChanged("SuchText");
                FilterListe();
            }
        }

        #endregion

        #region //---- COMMANDS ----

        private Base.CommandBase onReLoadMedia = null;
        public Base.CommandBase OnReLoadMedia
        {
            get
            {
                if (onReLoadMedia == null)
                    onReLoadMedia = new Base.CommandBase(ReLoadMedia, null);
                return onReLoadMedia;
            }
        }

        private Base.CommandBase onOpenPath = null;
        public Base.CommandBase OnOpenPath
        {
            get
            {
                if (onOpenPath == null)
                    onOpenPath = new Base.CommandBase(OpenPath, null);
                return onOpenPath;
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

        private Base.CommandBase onShowCharakter = null;
        public Base.CommandBase OnShowCharakter
        {
            get
            {
                if (onShowCharakter == null)
                    onShowCharakter = new Base.CommandBase(ShowCharakter, null);
                return onShowCharakter;
            }
        }

        private Base.CommandBase onShowImage = null;
        public Base.CommandBase OnShowImage
        {
            get
            {
                if (onShowImage == null)
                    onShowImage = new Base.CommandBase(ShowMedia, null);
                return onShowImage;
            }
        }

        private Base.CommandBase onOpenImageExtern = null;
        public Base.CommandBase OnOpenImageExtern
        {
            get
            {
                if (onOpenImageExtern == null)
                    onOpenImageExtern = new Base.CommandBase(OpenImageExtern, null);
                return onOpenImageExtern;
            }
        }

        private Base.CommandBase onShowSlideShow = null;
        public Base.CommandBase OnShowSlideShow
        {
            get
            {
                if (onShowSlideShow == null)
                    onShowSlideShow = new Base.CommandBase(ShowSlideShow, null);
                return onShowSlideShow;
            }
        }

        private Base.CommandBase onSetPointer = null;
        public Base.CommandBase OnSetPointer
        {
            get
            {
                if (onSetPointer == null)
                    onSetPointer = new Base.CommandBase(SetPointer, null);
                return onSetPointer;
            }
        }

        private Base.CommandBase onIsInSlideShowAll = null;
        public Base.CommandBase OnIsInSlideShowAll
        {
            get
            {
                if (onIsInSlideShowAll == null)
                    onIsInSlideShowAll = new Base.CommandBase(SetInSlideShowAll, null);
                return onIsInSlideShowAll;
            }
        }
        private Base.CommandBase onVideoMuting = null;
        public Base.CommandBase OnVideoMuting
        {
            get
            {
                if (onVideoMuting == null)
                    onVideoMuting = new Base.CommandBase(VideoMuting, null);
                return onVideoMuting;
            }
        }

        private Base.CommandBase onVideoPlay = null;
        public Base.CommandBase OnVideoPlay
        {
            get
            {
                if (onVideoPlay == null)
                    onVideoPlay = new Base.CommandBase(VideoPlay, null);
                return onVideoPlay;
            }
        }

        private Base.CommandBase onVideoPause = null;
        public Base.CommandBase OnVideoPause
        {
            get
            {
                if (onVideoPause == null)
                    onVideoPause = new Base.CommandBase(VideoPause, null);
                return onVideoPause;
            }
        }

        private Base.CommandBase onVideoStop = null;
        public Base.CommandBase OnVideoStop
        {
            get
            {
                if (onVideoStop == null)
                    onVideoStop = new Base.CommandBase(VideoStop, null);
                return onVideoStop;
            }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public SpielerScreenControlViewModel() : base()
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
            _bildschirmInfo = string.Format("{0} Bildschirm{1}", ScreenList.Count, ScreenList.Count == 1 ? string.Empty : "e");
            
            // Letzten Bilderpfad laden
            DirectoryPath = Logic.Einstellung.Einstellungen.SpielerInfoBilderPfad;

            SlideShowInterval = Logic.Einstellung.Einstellungen.SlideShowInterval;
        }

        private void FilterListe()
        {
            if (Images == null)
                return;

            string suchText = SuchText.ToLower().Trim();
            string[] suchWorte = suchText.Split(' ');

            if (suchText == string.Empty) // kein Suchwort
                FilteredImages = Images.AsParallel().OrderBy(n => n.Name).ToList();
            else if (suchWorte.Length == 1) // nur ein Suchwort
                FilteredImages = Images.AsParallel().Where(s => s.Contains(suchWorte[0])).OrderBy(n => n.Name).ToList();
            else // mehrere Suchwörter
                FilteredImages = Images.AsParallel().Where(s => s.Contains(suchWorte)).OrderBy(n => n.Name).ToList();
        }

        private void OpenImage(object sender = null)
        {
            string pfad = ChooseFile("Bild auswählen", "", false, false, Logic.Extensions.FileExtensions.EXTENSIONS_IMAGES);
            if (!String.IsNullOrEmpty(pfad))
                SelectedImagePath = pfad;
        }

        private void OpenDirectory(object sender = null)
        {
            string path = ChooseDirectory(Logic.Einstellung.Einstellungen.SpielerInfoBilderPfad, true);

            Logic.Einstellung.Einstellungen.SpielerInfoBilderPfad = path;
            DirectoryPath = path;
        }

        public void SpielerInfoClose(object sender = null)
        {
            SpielerWindow.Hide();
            SlideShowStop();
        }

        public void SpielerInfoOpen(object sender = null)
        {
            SpielerWindow.ReOpen();
        }

        public void ShowKampf(object sender = null)
        {
            IsKampfInfoShow = true;
            SpielerWindow.SetKampfInfoView(SpielerScreen);
            SlideShowStop();
        }

        public void ShowBodenplan(object sender = null)
        {
            //Der Bodenplan wie er aktuell existiert wird geändert
            //In der neuen Version soll der komplette Kampf an der UI gezeigt werden, nicht nur der Bodenplan
            if (Global.CurrentKampf != null &&
                Global.CurrentKampf.BodenplanViewModel != null)
            {
                SpielerWindow.Close();
                Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive = true;
                SpielerWindow.Show();
                SlideShowStop();
            }
        }

        public void ShowCharakter(object sender = null)
        {
            SpielerWindow.SetImage(Global.SelectedHeld.Bild, (IsImageStretch == true) ? Stretch.Uniform : Stretch.None);
            SlideShowStop();
        }
        public void ShowMedia(object sender = null)
        {
            if (SelectedMediaObject.IsVideo)
                SpielerWindow.SetVideo(SelectedImagePath, this, (IsImageStretch == true) ? Stretch.Uniform : Stretch.None);
            else
                SpielerWindow.SetImage(SelectedImagePath, (IsImageStretch == true) ? Stretch.Uniform : Stretch.None);
            SlideShowStop();
        }

        public void ShowText(object sender = null)
        {
            SpielerWindow.SetText(TextToShow);
            SlideShowStop();
        }

        public void OpenImageExtern(object sender = null)
        {
            try
            {
                System.Diagnostics.Process.Start(SelectedImagePath);
            }
            catch (Exception ex)
            {
                ShowError("Beim Starten eines externen Programms ist ein Fehler aufgetreten!", ex);
            }
        }

        private void LoadImage()
        {
            FileInfo fInfo = new FileInfo(SelectedImagePath);
            
            try
            {
                // Bild
                SelectMedia(SelectedImagePath);
            }
            catch
            {
                PopUp("Laden des Bildes fehlgeschlagen!");
            }
        }

        private void SelectMedia(string path)
        {
            System.Windows.Threading.DispatcherOperation op =
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
                {
                    try
                    {
                        if (!SelectedMediaObject.IsVideo)
                        {
                            SelectedVideo = null;

                            BitmapImage bmi = new BitmapImage();
                            bmi.BeginInit();
                            bmi.CacheOption = BitmapCacheOption.OnLoad;
                            bmi.UriSource = new Uri(path, UriKind.Relative);
                            bmi.EndInit();

                            bmi.Freeze();       // freeze image source, used to move it across the thread
                            SelectedImage = bmi;
                        }
                        else
                        {
                            SelectedImage = null;

                            MediaPlayerLoadedBehavior = MediaState.Play;
                            SelectedVideo = new Uri(path, UriKind.Relative);
                        }
                    }
                    catch (Exception)
                    {
                        PopUp("Bild/Video konnte nicht geladn werden:\n" + path);
                    }
                });
        }

        private void OpenPath(object sender = null)
        {
            try
            {
                if (string.IsNullOrEmpty(DirectoryPath))
                {
                    PopUp("Es ist kein Ordner ausgewählt, deshalb kann auch keiner im Explorer geöffnet werden.");
                    return;
                }
                if (!System.IO.Directory.Exists(DirectoryPath))
                {
                    PopUp(string.Format("Der Ordner '{0}' existiert nicht, deshalb kann er nicht im Explorer geöffnet werden.", DirectoryPath));
                    return;
                }
                System.Diagnostics.Process.Start(DirectoryPath);
            }
            catch (Exception ex)
            {
                ShowError("Beim Öffnen des Dateipfads ist ein Fehler aufgetreten!", ex);
            }
        }

        private void ReLoadMedia(object sender = null)
        {
            LoadMediaFromDir(DirectoryPath);
        }

        public void LoadMediaFromDir(string pfad)
        {
            if (string.IsNullOrWhiteSpace(pfad) || !Directory.Exists(pfad))
            {
                PathNotFound = true;
                return;
            }

            PathNotFound = false;

            SearchOption dirOption = SearchOption.TopDirectoryOnly;
            if (IsUnterordnerEinbeziehen)
                dirOption = SearchOption.AllDirectories;
            List<MediaItem> fileList = new List<MediaItem>();

            // Bilder
            string[] filesBmp = Directory.GetFiles(pfad, "*.bmp", dirOption);
            string[] filesGif = Directory.GetFiles(pfad, "*.gif", dirOption);
            string[] filesJpg = Directory.GetFiles(pfad, "*.jpg", dirOption);
            string[] filesJpeg = Directory.GetFiles(pfad, "*.jpeg", dirOption);
            string[] filesJpe = Directory.GetFiles(pfad, "*.jpe", dirOption);
            string[] filesJfif = Directory.GetFiles(pfad, "*.jfif", dirOption);
            string[] filesPng = Directory.GetFiles(pfad, "*.png", dirOption);
            string[] filesTif = Directory.GetFiles(pfad, "*.tif", dirOption);
            string[] filesTiff = Directory.GetFiles(pfad, "*.tiff", dirOption);

            AddMedia(fileList, filesBmp);
            AddMedia(fileList, filesBmp);
            AddMedia(fileList, filesGif);
            AddMedia(fileList, filesJpg);
            AddMedia(fileList, filesJpeg);
            AddMedia(fileList, filesJpe);
            AddMedia(fileList, filesJfif);
            AddMedia(fileList, filesPng);
            AddMedia(fileList, filesTif);
            AddMedia(fileList, filesTiff);

            // Videos
            // https://learn.microsoft.com/de-de/windows/win32/medfound/supported-media-formats-in-media-foundation
            string[] filesAvi = Directory.GetFiles(pfad, "*.avi", dirOption);
            string[] filesWmv = Directory.GetFiles(pfad, "*.wmv", dirOption);
            string[] filesMp4 = Directory.GetFiles(pfad, "*.mp4", dirOption);
            string[] filesMov = Directory.GetFiles(pfad, "*.mov", dirOption);

            AddMedia(fileList, filesAvi, true);
            AddMedia(fileList, filesWmv, true);
            AddMedia(fileList, filesMp4, true);
            AddMedia(fileList, filesMov, true);

            Images = fileList.OrderBy(img => img.Name).ToList();

            FilterListe();
        }

        private void AddMedia(List<MediaItem> fileList, string[] files, bool isVideo = false)
        {
            foreach (string file in files)
                fileList.Add(new MediaItem(file, DirectoryPath, isVideo));
        }

        // TODO: Der Laserpointer sollte überarbeitet werden, da das Feature 'quick & dirty' implementiert ist
        public void SetPointer(object parameter)
        {
            if (parameter == null || !(parameter is Grid))
                return;
            Grid grid = (Grid)parameter;
            System.Windows.Point mousePos = System.Windows.Input.Mouse.GetPosition(grid);
            _xScale = mousePos.X / grid.ActualWidth;
            _yScale = mousePos.Y / grid.ActualHeight;
            PointerMargin = new System.Windows.Thickness(mousePos.X - PointerDurchmesser / 2, mousePos.Y - PointerDurchmesser / 2, 0, 0);
        }

        private double _xScale = 1;
        private double _yScale = 1;

        private System.Windows.Thickness _pointerMargin = new System.Windows.Thickness();
        public System.Windows.Thickness PointerMargin
        {
            get
            {
                return _pointerMargin;
            }
            set
            {
                _pointerMargin = value;
                OnChanged("PointerMargin");
                if (SpielerWindow.Instance.Content is Grid)
                {
                    Grid g = (Grid)SpielerWindow.Instance.Content;
                    Image img = new Image();
                    foreach (var item in g.Children)
                    {
                        if (item is Image)
                        {
                            img = (Image)item;
                            break;
                        }
                    }
                    PointerMarginSpieler = new System.Windows.Thickness(img.ActualWidth * _xScale + (g.ActualWidth - img.ActualWidth) / 2 - PointerDurchmesser / 2,
                        img.ActualHeight * _yScale + (g.ActualHeight - img.ActualHeight) / 2 - PointerDurchmesser / 2, 0, 0);
                }
            }
        }

        private System.Windows.Thickness _pointerMarginSpieler = new System.Windows.Thickness();
        public System.Windows.Thickness PointerMarginSpieler
        {
            get
            {
                return _pointerMarginSpieler;
            }
            set
            {
                _pointerMarginSpieler = value;
                OnChanged("PointerMarginSpieler");
            }
        }
        private void VideoMuting(object sender = null)
        {
            VideoIsMuted = !VideoIsMuted;
        }

        private void VideoPlay(object sender = null)
        {
            MediaPlayerLoadedBehavior = MediaState.Play;
            MediaPlayerSpielerScreenLoadedBehavior = MediaState.Play;
        }

        private void VideoPause(object sender = null)
        {
            MediaPlayerLoadedBehavior = MediaState.Pause;
            MediaPlayerSpielerScreenLoadedBehavior = MediaState.Pause;
        }

        private void VideoStop(object sender = null)
        {
            MediaPlayerLoadedBehavior = MediaState.Stop;
            MediaPlayerSpielerScreenLoadedBehavior = MediaState.Stop;
        }

        private void SetInSlideShowAll(object sender = null)
        {
            foreach (var item in Images)
            {
                item.IsInSlideShow = IsInSlideShowAll;
            }
        }

        public void ShowSlideShow(object sender = null)
        {
            if (SlideShowRunning)
                SlideShowStop();
            else
                SlideShowStart();
        }

        private System.Timers.Timer _slideShowTimer = new System.Timers.Timer();
        private List<MediaItem>.Enumerator _imagesEnumerator = new List<MediaItem>.Enumerator();

        private void SlideShowStart()
        {
            if (Images == null)
                return;

            SlideShowRunning = true;
            _slideShowTimer.Elapsed += SlideShowTimer_Elapsed;

            MediaItem selectedImage = null;
            if (!string.IsNullOrEmpty(SelectedImagePath))
                selectedImage = Images.Where(img => img.Pfad == SelectedImagePath).FirstOrDefault();

            _imagesEnumerator = Images.GetEnumerator();
            while (_imagesEnumerator.MoveNext())
            {
                if (!_imagesEnumerator.Current.IsInSlideShow) // nicht für SlideShow ausgewählt, dann weiter
                    continue;

                if (selectedImage != null && selectedImage.IsInSlideShow) // ein Bild ist selektiert und für SlideShow ausgewählt
                {
                    if (SelectedImagePath != _imagesEnumerator.Current.Pfad)
                        continue;
                }

                CurrentSlideShowImage = _imagesEnumerator.Current.Pfad;
                SpielerWindow.SetSlideShow(this);
                _slideShowTimer.Start();
                break;
            }
        }

        private void SlideShowStop()
        {
            SlideShowRunning = false;
            _slideShowTimer.Elapsed -= SlideShowTimer_Elapsed;
            _slideShowTimer.Stop();
        }

        private void SlideShowMove()
        {
            while (_imagesEnumerator.MoveNext())
            {
                if (_imagesEnumerator.Current.IsInSlideShow)
                {
                    CurrentSlideShowImage = _imagesEnumerator.Current.Pfad;
                    return;
                }
                else
                    continue;
            }

            if (Images == null)
                return;

            _imagesEnumerator = Images.GetEnumerator();
            while (_imagesEnumerator.MoveNext() && _imagesEnumerator.Current.IsInSlideShow)
            {
                CurrentSlideShowImage = _imagesEnumerator.Current.Pfad;
                break;
            }
        }

        private void SlideShowTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            SlideShowMove();
            if (_slideShowInterval * 1000 != _slideShowTimer.Interval)
                _slideShowTimer.Interval = _slideShowInterval * 1000;
        }

        private Base.CommandBase onShowMap = null;
        public Base.CommandBase OnShowMap
        {
            get
            {
                if (onShowMap == null)
                    onShowMap = new Base.CommandBase(ShowMap, null);
                return onShowMap;
            }
        }
        private void ShowMap(object sender)
        {
            foreach (ToolViewModelBase b in Global.MainVM.OpenTools)
            {
                if (b.GetType() == typeof(KarteViewModel))
                {
                    SpielerWindow.SetContent(View.General.ViewHelper.GetImageFromControl((FrameworkElement)(((KarteViewModel)b).MapZoomControl)));
                    SlideShowStop();
                    return;
                }
            }
        }

        #endregion

    }

    #region // MediaItem

    public class MediaItem : ViewModel.Base.ViewModelBase
    {
        /// <summary>
        /// Eine Zusammenführung aller durchsuchbaren Felder.
        /// </summary>
        private string _suchtext = string.Empty;
        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                SetSuchtext();
                OnChanged("Name");
            }
        }
        public string Pfad { get; set; }

        private bool _isInSlideShow = true;
        public bool IsInSlideShow
        {
            get { return _isInSlideShow; }
            set
            {
                _isInSlideShow = value;
                OnChanged("IsInSlideShow");
            }
        }

        public bool IsVideo { get; set; }
        public bool IsImage
        {
            get
            {
                return !IsVideo;
            }
        }

        public MediaItem(string file, string rootDir, bool isVideo = false)
        {
            Pfad = file;
            string dirTags = file.Remove(0, rootDir.Length + 1)
                .Replace(Path.GetFileName(file), string.Empty)
                .Replace("\\", " \\ "); // Unterverzeihnisse als Namenstags hinzufügen
            _name = dirTags + Path.GetFileNameWithoutExtension(file);
            IsInSlideShow = true;
            IsVideo = isVideo;
            SetSuchtext();
        }

        private void SetSuchtext()
        {
            _suchtext = _name.ToLower();
        }

        /// <summary>
        /// Prüft, ob 'suchWort' im Namen vorkommt.
        /// </summary>
        /// <param name="suchWort"></param>
        /// <returns></returns>
        public bool Contains(string suchWort)
        {
            return _suchtext.Contains(suchWort);
        }

        /// <summary>
        /// Prüft, ob die 'suchWorte' im Namen, der Kategorie oder in den Tags vorkommt.
        /// Es wird dabei eine UND-Prüfung durchgeführt.
        /// </summary>
        /// <param name="suchWorte"></param>
        /// <returns></returns>
        public bool Contains(string[] suchWorte)
        {
            foreach (string wort in suchWorte)
            {
                if (!Contains(wort))
                    return false;
            }
            return true;
        }
    }

    #endregion
}
