using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.View.General;
using MeisterGeister.View.Kampf;
using MeisterGeister.ViewModel.Bodenplan.Logic;
using MeisterGeister.ViewModel.Kampf;
using MeisterGeister.ViewModel.Kampf.Logic;
using MeisterGeister.ViewModel.SpielerScreen;
using WPFExtensions.Controls;
using Application = System.Windows.Application;

namespace MeisterGeister.ViewModel.Bodenplan
{
    public class BattlegroundViewModel : Base.ViewModelBase, IDisposable
    {
        public const int ARENA_GRID_RESOLUTION = 10000;

        // *Hintergrundfarbe
        // *ZLevel bei initialisierung fix
        // *Bilder Fixen!
        // *Größe Kreaturen
        public BattlegroundViewModel() : base()
        {
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;

            //alte Fog-of-War Bilder löschen
            foreach (var s in Directory.GetFiles(Ressources.GetFullApplicationPathForPictures()).ToList()
                .FindAll(t => t.StartsWith(Ressources.GetFullApplicationPathForPictures() + "Fog-of-War")))
            {
                try
                { File.Delete(s); }
                catch
                { }
            }
            ReLoadImages();
            Global.CurrentKampf.BodenplanViewModel = this;

        }

        private IWaffe _miWaffeSelected = null;
        public IWaffe miWaffeSelected
        {
            get { return _miWaffeSelected; }
            set { Set(ref _miWaffeSelected, value); }
        }

        public bool CreatingNewFilledLine
        {
            get { return _creatingNewFilledLine; }

            set
            {
                Set(ref _creatingNewFilledLine, value);
                if (!_creatingNewFilledLine)
                {
                    SelectedObject = null;
                }
            }
        }

        public bool CreatingNewLine
        {
            get { return _creatingNewLine; }

            set
            {
                Set(ref _creatingNewLine, value);
                if (!_creatingNewLine)
                {
                    SelectedObject = null;
                }
            }
        }

        public double CurrentMousePositionX
        {
            get { return _currentMousePositionX; }
            set { Set(ref _currentMousePositionX, value); }
        }

        public double CurrentMousePositionY
        {
            get { return _currentMousePositionY; }
            set { Set(ref _currentMousePositionY, value); }
        }

        public bool Freizeichnen
        {
            get { return _freizeichnen; }

            set
            {
                Set(ref _freizeichnen, value);
            }
        }

        public bool InitDnD
        {
            get { return _initDnD; }
            set { Set(ref _initDnD, value); }
        }

        public bool InitLineal
        {
            get { return _initLineal; }
            set { Set(ref _initLineal, value); }
        }

        public bool IsEditorModeEnabled
        {
            get { return _isEditorModeEnabled; }

            set
            {
                Set(ref _isEditorModeEnabled, value);
            }
        }

        public double BewegungZuvor
        {
            get { return _bewegungZuvor; }
            set { Set(ref _bewegungZuvor, value); }
        }

        public bool IsMoving
        {
            get { return _isMoving; }
            set { Set(ref _isMoving, value); }
        }

        public bool LeftShiftPressed
        {
            get { return _leftShiftPressed; }
            set { Set(ref _leftShiftPressed, value); }
        }

        public void ChangeEbeneHeight(bool raise)
        {
            if (SelectedObject == null)
            {
                return;
            }

            for (var i = 0; i < BattlegroundObjects.Count; i++)
            {
                if (BattlegroundObjects[i].Equals(SelectedObject))
                {
                    BattlegroundBaseObject b = SelectedObject;
                    if (raise && i != BattlegroundObjects.Count - 1)
                    {
                        BattlegroundObjects.Remove(SelectedObject);
                        BattlegroundObjects.Insert(i + 1, b);
                        SelectedObject = BattlegroundObjects[i + 1];
                    }
                    else if (!raise && i > 0)
                    {
                        BattlegroundObjects.Remove(SelectedObject);
                        BattlegroundObjects.Insert(i - 1, b);
                        SelectedObject = BattlegroundObjects[i - 1];
                    }
                    return;
                }
            }
        }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }

        public ImageObject CreateImageObject(string picurl, Point p)
        {
            var brush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(picurl, UriKind.Relative))
            };

            var imageobject =
                new ImageObject(picurl, ((-1) * MeisterZoomTransX + 200) / MeisterZoom, ((-1) * MeisterZoomTransY + 200) / MeisterZoom);
            if (brush.ImageSource.Width >= brush.ImageSource.Height)
            {
                imageobject.ImageWidth = 100;
                imageobject.ImageHeight = 100 * (brush.ImageSource.Height / brush.ImageSource.Width);
            }
            else
            {
                imageobject.ImageHeight = 100;
                imageobject.ImageWidth = 100 * (brush.ImageSource.Width / brush.ImageSource.Height);
            }
            imageobject._imageOriginalWidth = imageobject.ImageWidth;
            imageobject._imageOriginalHeigth = imageobject.ImageHeight;

            imageobject.ObjectSize = ObjectSize;
            BattlegroundObjects.Add(imageobject);
            return imageobject;
        }


        public MP4Object CreateVideoObject(string vidurl, Point p)
        {
            var melement = new MP4Object(vidurl, 0, 0);
            BattlegroundObjects.Add(melement);
            return melement;
        }

        public void CreateNewFilledLine(double x1, double y1)
        {
            var filledpathline = new FilledPathLine(new Point(x1, y1))
            {
                ObjectColor = new SolidColorBrush(SelectedColor),
                FillColor = SelectedFillColor,
                StrokeThickness = StrokeThickness,
                Opacity = Opacity,
                IsNew = true
            };
            SelectedObject = filledpathline;
            BattlegroundObjects.Add(filledpathline);
        }

        public void CreateNewPathLine(double x1, double y1)
        {
            var pathline = new PathLine(new Point(x1, y1))
            {
                ObjectColor = new SolidColorBrush(SelectedColor),
                StrokeThickness = StrokeThickness,
                Opacity = Opacity,
                IsNew = true
            };
            SelectedObject = pathline;
            BattlegroundObjects.Add(pathline);
        }

        public void CreateNewTempPathLine(double x1, double y1)
        {
            var th = (SelectedObject as BattlegroundCreature).CreatureHeight <= (SelectedObject as BattlegroundCreature).CreatureWidth ?
                (SelectedObject as BattlegroundCreature).CreatureHeight : (SelectedObject as BattlegroundCreature).CreatureWidth;

            var pathline = new PathLine(new Point(x1, y1))
            {
                ObjectColor = new SolidColorBrush(Colors.DarkBlue),
                StrokeThickness = th,
                Opacity = (SelectedObject as BattlegroundCreature).ki.IstUnsichtbar ? .02 : .2,
                IsNew = true
            };
            SelectedTempObject = pathline;
            BattlegroundObjects.Add(pathline);
        }

        public void CreateNewTempLinealLine(double x1, double y1)
        {
            var pathline = new PathLine(new Point(x1, y1))
            {
                ObjectColor = new SolidColorBrush(LinealColor),
                StrokeThickness = 10,
                Opacity = .2,
                IsNew = true
            };
            SelectedTempObject = pathline;
            BattlegroundObjects.Add(pathline);
        }

        public void CreateNewTempTextLabel(double x1, double y1)
        {
            var label = new TextLabel("0 Schritt", x1, y1)
            {
                IsNew = true,
                LabelWidth = 200,
                Opacity = (SelectedObject as BattlegroundCreature).ki.IstUnsichtbar ? .02 : 1
            };
            BattlegroundObjects.Add(label);
        }
        public void CreateNewTempLinealLabel(double x1, double y1)
        {
            var label = new TextLabel("0 Schritt", x1, y1)
            {
                IsNew = true,
                LabelWidth = 200,
                Opacity = 1
            };
            BattlegroundObjects.Add(label);
        }

        public void FinishCurrentPathLine()
        {
            if (SelectedObject != null)
            {
                SelectedObject.IsNew = false;
                SelectedObject = null;
                RemoveNewObjects();
            }
        }

        public void FinishCurrentTempPathLine()
        {
            if (SelectedTempObject != null && SelectedTempObject is PathLine)
            {
                SelectedTempObject.IsNew = true;
                SelectedTempObject = null;
                RemoveNewObjects();
                Global.CurrentKampf.LabelInfo = null;
            }
            SelectedTempObject = null;
        }

        public void MoveLastObjectBehindCreatures()
        {
            BattlegroundBaseObject bbo = BattlegroundObjects[BattlegroundObjects.Count - 1];

            var x = BattlegroundObjects.IndexOf(BattlegroundObjects.FirstOrDefault(t => t is BattlegroundCreature));
            if (x >= 0)
            {
                BattlegroundObjects.Move(BattlegroundObjects.Count - 1, x);
            }
        }

        public void MoveObject(double xOld, double yOld, double xNew, double yNew)
        {
            if (SelectedObject != null)
            {
                if (!HeldenInFormationBewegen)
                    SelectedObject.MoveObject(xNew - xOld, yNew - yOld, false);
                else
                    BattlegroundObjects.Where(t => t as Held != null).ToList()
                        .ForEach(delegate (BattlegroundBaseObject bgObject)
                        { bgObject.MoveObject(xNew - xOld, yNew - yOld, false); });

            }
        }
        public void MoveAllHelden(double xDelta, double yDelta)
        {
        }

        /// <summary>
        /// brings selected Object to Top and calls UpdateCreatureLevelToTop
        /// </summary>
        /// <param name="toTop"></param>
        public void MoveSelectedObjectToTop(bool toTop)
        {
            if (SelectedObject == null)
            {
                return;
            }

            for (var i = BattlegroundObjects.Count - 1; i >= 0; i--)
            {
                if (BattlegroundObjects[i].Equals(SelectedObject))
                {
                    BattlegroundBaseObject b = BattlegroundObjects[i];
                    BattlegroundObjects.Remove(BattlegroundObjects[i]);
                    var position = toTop ? BattlegroundObjects.Count - 1 : 0;
                    BattlegroundObjects.Insert(position, b);
                    UpdateCreatureLevelToTop();
                }
            }
        }

        public void MoveWhileDrawing(double x2, double y2, bool sketchDrawing)
        {
            if (SelectedObject != null)
            {
                if (SelectedObject is PathLine)
                {
                    if (sketchDrawing)
                    {
                        ((PathLine)SelectedObject).AddNewPointToSeries(new Point(x2, y2));
                    }
                    else
                    {
                        ((PathLine)SelectedObject).ChangeLastPoint(new Point(x2, y2));
                    }
                }
                else if (SelectedObject is FilledPathLine)
                {
                    if (sketchDrawing)
                    {
                        ((FilledPathLine)SelectedObject).AddNewPointToSeries(new Point(x2, y2));
                    }
                    else
                    {
                        ((FilledPathLine)SelectedObject).ChangeLastPoint(new Point(x2, y2));
                    }
                }
                else if (SelectedTempObject is PathLine)
                {
                    AlterPathLine(x2, y2);
                }
            }
        }

        public void AlterPathLine(double x2, double y2)
        {
            Console.WriteLine(x2 + ", " + y2);

            var pathLine = (PathLine)SelectedTempObject;
            var endPoint = new Point(x2, y2);
            Point startPoint = pathLine.GetStartPoint;

            pathLine.ChangeLastPoint(endPoint);

            SetBewegungslaenge(startPoint, endPoint);
        }

        public void RemoveNewObjects()
        {
            BattlegroundObjects.Where(x => x.IsNew).ToList().ForEach(x => BattlegroundObjects.Remove(x));
        }

        public void SelectionChangedUpdateSliders()
        {
            OnChanged(nameof(StrokeThickness));
            OnChanged(nameof(ObjectSize));
            OnChanged(nameof(Opacity));
            OnChanged(nameof(ZLevel));
        }

        public void SetIniWindowPosition()
        {
            //Get DPI Scaling from MainProgramm
            Matrix m = PresentationSource.FromVisual(Application.Current.MainWindow).CompositionTarget.TransformToDevice;
            var dx = m.M11;
            var dy = m.M22;

            System.Windows.HorizontalAlignment h = System.Windows.HorizontalAlignment.Right;
            VerticalAlignment v = VerticalAlignment.Top;

            switch (PosIniWindow)
            {
                case 1:
                    {
                        h = System.Windows.HorizontalAlignment.Right;
                        v = VerticalAlignment.Top;
                        break;
                    }
                case 2:
                    {
                        h = System.Windows.HorizontalAlignment.Right;
                        v = VerticalAlignment.Bottom;
                        break;
                    }
                case 3:
                    {
                        h = System.Windows.HorizontalAlignment.Left;
                        v = VerticalAlignment.Bottom;
                        break;
                    }
                case 4:
                    {
                        h = System.Windows.HorizontalAlignment.Left;
                        v = VerticalAlignment.Top;
                        break;
                    }
            }
            var maxRight = Math.Max(
                 Screen.AllScreens[0].WorkingArea.Width / dx,
                 Screen.AllScreens.Length > 1 ?
                     Screen.AllScreens[1].WorkingArea.Right / dx : 0);

            var minRight = Screen.AllScreens.Length == 1 ? 0 : Screen.AllScreens[1].WorkingArea.Left / dx;

            if (KampfWindow == null)
            {
                return;
            }

            KampfWindow.SizeToContent = SizeToContent.Manual;

            KampfWindow.Left = (((h == System.Windows.HorizontalAlignment.Left) ? minRight :
                maxRight - ((KampfWindow.MinWidth > KampfWindow.Width) ? KampfWindow.MinWidth : KampfWindow.Width)));

            KampfWindow.Top = (((v == System.Windows.VerticalAlignment.Top) ? 0 :
                (Screen.AllScreens.Length == 1 ?
                    Screen.AllScreens[0].WorkingArea.Height / dy :
                    Screen.AllScreens[1].WorkingArea.Height / dy) - KampfWindow.ActualHeight));
        }

        public void SetIniWindowWidth(bool doWindowMove = false)
        {
            if (doWindowMove)
            {
                if ((Screen.AllScreens.Length > 1 &&
                     KampfWindow.Left > Screen.AllScreens[0].WorkingArea.Width +
                        Screen.AllScreens[1].WorkingArea.Width * .5) ||
                    (Screen.AllScreens.Length == 1 &&
                     KampfWindow.Left > Screen.AllScreens[0].WorkingArea.Width * .5))
                {
                    KampfWindow.Left = (Screen.AllScreens.Length > 1) ?
                        Screen.AllScreens[0].WorkingArea.Width +
                        Screen.AllScreens[1].WorkingArea.Width - KampfWindow.Width
                         :

                        Screen.AllScreens[0].WorkingArea.Width - KampfWindow.Width;
                    if (Screen.AllScreens.Length > 1 &&
                        Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive)
                    {
                        Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Width =
                            Screen.AllScreens[1].WorkingArea.Width - KampfWindow.Width;
                    }
                }
            }

            KampfWindow.MinWidth = 430 * ScaleKampfGrid;

            var anzInisInKR = Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Where(kr => kr.Kampfrunde == Global.CurrentKampf.Kampf.Kampfrunde).Count();
            var width1Ini = (((KampfInfoView)KampfWindow.Content).scrViewer.ExtentWidth /
                Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Where(kr => kr.Kampfrunde <= Global.CurrentKampf.Kampf.Kampfrunde).Count()) * ScaleKampfGrid;
            KampfWindow.Width = width1Ini * anzInisInKR + 248 * ScaleKampfGrid;
            ((KampfInfoView)KampfWindow.Content).scrViewer.ScrollToRightEnd();
        }

        /// <summary>
        /// keeps heroes and monsters always on top
        /// </summary>
        public void UpdateCreatureLevelToTop()
        {
            for (var i = BattlegroundObjects.Count - 1; i >= 0; i--)
            {
                if (BattlegroundObjects[i] is BattlegroundCreature)
                {
                    BattlegroundBaseObject b = BattlegroundObjects[i];
                    BattlegroundObjects.Remove(BattlegroundObjects[i]);
                    BattlegroundObjects.Insert(BattlegroundObjects.Count, b);
                }
            }
            SelectedObject = null;
        }

        private bool _creatingNewFilledLine;

        private bool _creatingNewLine;

        private double _currentMousePositionX, _currentMousePositionY;

        private bool _freizeichnen = false;

        private bool _initDnD;

        private bool _initLineal = true;

        //Vielleicht als Enum, wenn mehr als zwei Modi gebraucht werden? JO
        private bool _isEditorModeEnabled = true;

        private double _bewegungZuvor = 0;

        private bool _isMoving;

        private bool _leftShiftPressed = false;

        private void _selectedListBoxBattlegroundObjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("CollectionChanged: {0} {1}", e.Action, e.NewItems));
        }

        private void SetBewegungslaenge(Point startPoint, Point endPoint)
        {
            var label = BattlegroundObjects.Last(x => x.GetType() == typeof(TextLabel)) as TextLabel;
            label.LabelPositionX = (startPoint.X + endPoint.X - label.LabelWidth) / 2;
            label.LabelPositionY = (startPoint.Y + endPoint.Y - label.LabelHeight) / 2;
            label.TextInLabel = (BerechneLänge(startPoint, endPoint) + BewegungZuvor).ToString() + " Schritt";
            Global.CurrentKampf.LabelInfo = "Strg-Taste klicken für Richtungswechsel";
        }

        public double BerechneLänge(Point StartPunkt, Point EndPunkt)
        {
            return
                Math.Round(Math.Sqrt(Math.Pow((EndPunkt.X - StartPunkt.X), 2) + Math.Pow((EndPunkt.Y - StartPunkt.Y), 2)) / 100, 1);
        }

        #region Thumbnails Images

        private List<ImageItem> _images = null;
        public List<ImageItem> Images
        {
            get { return _images; }
            set { Set(ref _images, value); }
        }

        private List<ImageItem> _filteredImages = null;
        public List<ImageItem> FilteredImages
        {
            get { return _filteredImages; }
            set { Set(ref _filteredImages, value); }
        }

        private string _suchText = string.Empty;
        public string SuchText
        {
            get { return _suchText; }
            set
            {
                Set(ref _suchText, value);
                FilterListe();
            }
        }

        public void LoadImagesFromDir(string pfad)
        {
            if (string.IsNullOrWhiteSpace(pfad) || !Directory.Exists(pfad))
            {
                return;
            }

            SearchOption dirOption = SearchOption.AllDirectories;

            string[] filesBmp = Directory.GetFiles(pfad, "*.bmp", dirOption);
            string[] filesGif = Directory.GetFiles(pfad, "*.gif", dirOption);
            string[] filesJpg = Directory.GetFiles(pfad, "*.jpg", dirOption);
            string[] filesJpeg = Directory.GetFiles(pfad, "*.jpeg", dirOption);
            string[] filesJpe = Directory.GetFiles(pfad, "*.jpe", dirOption);
            string[] filesJfif = Directory.GetFiles(pfad, "*.jfif", dirOption);
            string[] filesPng = Directory.GetFiles(pfad, "*.png", dirOption);
            string[] filesTif = Directory.GetFiles(pfad, "*.tif", dirOption);
            string[] filesTiff = Directory.GetFiles(pfad, "*.tiff", dirOption);

            List<ImageItem> fileList = new List<ImageItem>();
            AddImages(fileList, filesBmp, pfad);
            AddImages(fileList, filesBmp, pfad);
            AddImages(fileList, filesGif, pfad);
            AddImages(fileList, filesJpg, pfad);
            AddImages(fileList, filesJpeg, pfad);
            AddImages(fileList, filesJpe, pfad);
            AddImages(fileList, filesJfif, pfad);
            AddImages(fileList, filesPng, pfad);
            AddImages(fileList, filesTif, pfad);
            AddImages(fileList, filesTiff, pfad);

            Images = fileList.OrderBy(img => img.Name).ToList();
            FilterListe();
        }

        private void AddImages(List<ImageItem> fileList, string[] files, string DirectoryPath)
        {
            foreach (string file in files)
                fileList.Add(new ImageItem(file, DirectoryPath));
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

        private void ReLoadImages(object sender = null)
        {
            LoadImagesFromDir(String.IsNullOrEmpty(Ressources.GetFullApplicationPath()) ? "Daten\\Bodenplan" : Ressources.GetFullApplicationPath() + "Daten\\Bodenplan");
        }

        #endregion

        #region Fog

        public Grid AreanGrid
        {
            get { return _arenaGrid; }
            set { Set(ref _arenaGrid, value); }
        }

        private MediaState _backgroundMP4LoadedBehavior = MediaState.Play;
        public MediaState BackgroundMP4LoadedBehavior
        { 
            get { return _backgroundMP4LoadedBehavior; }
            set { Set(ref _backgroundMP4LoadedBehavior, value); }
        }

        private Color _backgroundColor = Color.FromArgb(0xFF, 0x36, 0x75, 0x36);
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                Set(ref _backgroundColor, value);
                BackgroundBrush = new SolidColorBrush(value);
            }
        }
        
        private Brush _backgroundBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x36, 0x75, 0x36));
        public Brush BackgroundBrush
        {
            get { return _backgroundBrush; }
            set { Set(ref _backgroundBrush, value); }
        }

        public bool HasMP4Background
        {
            get { return _hasMP4Background; }
            set { Set(ref _hasMP4Background, value); }
        }

        public bool BackgroundMp4Mute
        {
            get { return _backgroundMp4Mute; }
            set { Set(ref _backgroundMp4Mute, value);
                BattlegroundBaseObject bg1 = BattlegroundObjects.Where(t => t is MP4Object).Where(t => (t as MP4Object).IsBackgroundPicture).FirstOrDefault();
                if (bg1 != null)
                    (bg1 as MP4Object).IsMute = value;
            }
        }

        public string BackgroundFilename
        {
            get { return _backgroundFilename; }
            set { Set(ref _backgroundFilename, value); }
        }

        public string BackgroundImage
        {
            get { return _backgroundImage; }

            set
            {
                Set(ref _backgroundImage, value);
                BackgroundFilename = value ?? Path.GetFileNameWithoutExtension(value);
            }
        }

        public double BackgroundOffsetSize
        {
            get { return _backgroundOffsetSize; }

            set
            {
                Set(ref _backgroundOffsetSize, value);
                OffsetBackgroudMargin = new Thickness(_backgroundOffsetX, _backgroundOffsetY, -_backgroundOffsetX - 20000, -_backgroundOffsetY - 20000);
            }
        }

        public double BackgroundOffsetX
        {
            get { return _backgroundOffsetX; }

            set
            {
                Set(ref _backgroundOffsetX, value);
                OffsetBackgroudMargin = new Thickness(_backgroundOffsetX, _backgroundOffsetY, -_backgroundOffsetX - 20000, -_backgroundOffsetY - 20000);
            }
        }

        public double BackgroundOffsetY
        {
            get { return _backgroundOffsetY; }

            set
            {
                Set(ref _backgroundOffsetY, value);
                OffsetBackgroudMargin = new Thickness(_backgroundOffsetX, _backgroundOffsetY, -_backgroundOffsetX - 20000, -_backgroundOffsetY - 20000);
            }
        }

        public Kampf.Logic.Kampf CurrKampf
        {
            get { return Global.CurrentKampf.Kampf; }
        }

        public int FogFreeSize
        {
            get { return _fogFreeSize; }
            set { Set(ref _fogFreeSize, value); }
        }

        public bool FogFreimachen
        {
            get { return _fogFreimachen; }
            set { Set(ref _fogFreimachen, value); }
        }

        public WriteableBitmap FogImage
        {
            get { return _fogImage; }
            set { Set(ref _fogImage, value); }
        }

        public string FogImageFilename
        {
            get { return _fogImageFilename; }
            set { Set(ref _fogImageFilename, value); }
        }

        public double FogOffsetSize
        {
            get { return _fogOffsetSize; }

            set
            {
                Set(ref _fogOffsetSize, value);
                OffsetFogMargin = new Thickness(_fogOffsetX, _fogOffsetY, -_fogOffsetX, -_fogOffsetY - 20000);
            }
        }

        public double FogOffsetX
        {
            get { return _fogOffsetX; }

            set
            {
                Set(ref _fogOffsetX, value);
                OffsetBackgroudMargin = new Thickness(_fogOffsetX, _fogOffsetY, -_fogOffsetX - 20000, -_fogOffsetY - 20000);
            }
        }

        public double FogOffsetY
        {
            get { return _fogOffsetY; }

            set
            {
                Set(ref _fogOffsetY, value);
                OffsetBackgroudMargin = new Thickness(_fogOffsetX, _fogOffsetY, -_fogOffsetX - 20000, -_fogOffsetY - 20000);
            }
        }

        public int[] FogPixelData
        {
            get { return _fogPixelData; }
            set { Set(ref _fogPixelData, value); }
        }

        public double FontSize
        {
            get { return _fontSize; }
            set { Set(ref _fontSize, value); }
        }

        public string InfoText
        {
            get { return _infoText; }
            set { Set(ref _infoText, value); }
        }

        public double IniHeightStart
        {
            get { return _iniHeightStart; }
            set { Set(ref _iniHeightStart, value); }
        }

        public double IniWidthStart
        {
            get { return _iniWidthStart; }
            set { Set(ref _iniWidthStart, value); }
        }

        public double InvBackgroundOffsetY
        {
            get { return _invBackgroundOffsetY; }

            set
            {
                Set(ref _invBackgroundOffsetY, value);
                BackgroundOffsetY = value * -1;
            }
        }

        public bool InvertPlayerScrolling
        {
            get { return _invertPlayerScrolling; }
            set { Set(ref _invertPlayerScrolling, value); }
        }

        public bool IsShowIniKampf
        {
            get { return _isShowIniKampf; }
            set { Set(ref _isShowIniKampf, value); }
        }

        public KampfViewModel kampf
        {
            get { return _kampf; }
            set { Set(ref _kampf, value); }
        }

        public Point KämpferDnDTempPos
        {
            get { return _kämpferDnDTempPos; }
            set { Set(ref _kämpferDnDTempPos, value); }
        }

        public KampfInfoView KampfIniInfoView
        {
            get { return _kampfIniInfoView; }
            set { Set(ref _kampfIniInfoView, value); }
        }

        public Window KampfWindow
        {
            get { return _kampfWindow; }
            set { Set(ref _kampfWindow, value); }
        }

        public ZoomControl MeisterArenaZoomControl
        {
            get { return _meisterArenaZoomControl; }
            set { Set(ref _meisterArenaZoomControl, value); }
        }

        public double MeisterZoom
        {
            get { return _meisterZoom; }

            set
            {
                Set(ref _meisterZoom, value);
                FontSize = Math.Round(14 / value, 0);
            }
        }

        public double MeisterZoomTransX
        {
            get { return _meisterZoomTransX; }
            set { Set(ref _meisterZoomTransX, value); }
        }

        public double MeisterZoomTransY
        {
            get { return _meisterZoomTransY; }
            set { Set(ref _meisterZoomTransY, value); }
        }

        private int _backgroundMp4MaxPosition = 999;
        public int BackgroundMp4MaxPosition
        {
            get { return _backgroundMp4MaxPosition; }
            set
            {
                Set(ref _backgroundMp4MaxPosition, value);
                BattlegroundBaseObject bg1 = BattlegroundObjects.Where(t => t is MP4Object).Where(t => (t as MP4Object).IsBackgroundPicture).FirstOrDefault();
                if (bg1 != null)
                    (bg1 as MP4Object).MaxPosition = Convert.ToDouble(value);
            }
        }
        private int _backgroundMp4MinPosition = 0;
        public int BackgroundMp4MinPosition
        {
            get { return _backgroundMp4MinPosition; }
            set
            {
                Set(ref _backgroundMp4MinPosition, value);
                BattlegroundBaseObject bg1 = BattlegroundObjects.Where(t => t is MP4Object).Where(t => (t as MP4Object).IsBackgroundPicture).FirstOrDefault();
                if (bg1 != null)
                    (bg1 as MP4Object).MinPosition = Convert.ToDouble(value);
            }
        }

        private int _backgroundMp4Length = 999;
        public int BackgroundMp4Length
        {
            get { return _backgroundMp4Length; }
            set { Set(ref _backgroundMp4Length, value);
                BattlegroundBaseObject bg1 = BattlegroundObjects.Where(t => t is MP4Object).Where(t => (t as MP4Object).IsBackgroundPicture).FirstOrDefault();
                if (bg1 != null)
                    (bg1 as MP4Object).VideoLength = Convert.ToDouble(value);
            }
        }

        private double _backgroundMp4Speed = 1;
        public double BackgroundMp4Speed
        {
            get { return _backgroundMp4Speed; }
            set
            {
                Set(ref _backgroundMp4Speed, value);
                BattlegroundBaseObject bg1 = BattlegroundObjects.Where(t => t is MP4Object).Where(t => (t as MP4Object).IsBackgroundPicture).FirstOrDefault();
                if (bg1 != null)
                    (bg1 as MP4Object).VideoSpeedRatio = value;
            }
        }

        public Thickness OffsetBackgroudMargin
        {
            get { return _offsetBackgroudMargin; }

            set
            {
                Set(ref _offsetBackgroudMargin, value);
                BattlegroundBaseObject bgO = BattlegroundObjects.Where(t => t is ImageObject).Where(t => (t as ImageObject).IsBackgroundPicture).FirstOrDefault();
                if (bgO != null)
                {
                    bgO.ZDisplayX = BackgroundOffsetX;
                    bgO.ZDisplayY = BackgroundOffsetY;
                    (bgO as ImageObject).ObjectSize = BackgroundOffsetSize;
                }
                else
                {
                    BattlegroundBaseObject bg1 = BattlegroundObjects.Where(t => t is MP4Object).Where(t => (t as MP4Object).IsBackgroundPicture).FirstOrDefault();
                    if (bg1 != null)
                    {
                        bg1.ZDisplayX = BackgroundOffsetX;
                        bg1.ZDisplayY = BackgroundOffsetY;                        
                        (bg1 as MP4Object).VideoPositionX = BackgroundOffsetX;
                        (bg1 as MP4Object).VideoPositionY = BackgroundOffsetY;
                        (bg1 as MP4Object).ObjectSize = BackgroundOffsetSize;
                    }
                }
                if (InvBackgroundOffsetY != BackgroundOffsetY * (-1))
                    InvBackgroundOffsetY = BackgroundOffsetY * (-1);
            }
        }

        public Thickness OffsetFogMargin
        {
            get { return _offsetFogMargin; }

            set
            {
                Set(ref _offsetFogMargin, value);
                BattlegroundBaseObject bgO = BattlegroundObjects.Where(t => t is ImageObject).Where(t => (t as ImageObject).IsBackgroundPicture).FirstOrDefault();
                if (bgO != null)
                {
                    bgO.ZDisplayX = FogOffsetX;
                    bgO.ZDisplayY = FogOffsetY;
                    (bgO as ImageObject).ObjectSize = FogOffsetSize;
                }
            }
        }

        public double PlayerGridOffsetX
        {
            get { return _playerGridOffsetX; }

            set
            {
                Set(ref _playerGridOffsetX, value);
                PlayerOffsetGridMargin = new Thickness(-_playerGridOffsetX, _playerGridOffsetY, 0, 0);
            }
        }

        public double PlayerGridOffsetY
        {
            get { return _playerGridOffsetY; }

            set
            {
                Set(ref _playerGridOffsetY, value);
                PlayerOffsetGridMargin = new Thickness(-_playerGridOffsetX, _playerGridOffsetY, 0, 0);
            }
        }

        public Thickness PlayerOffsetGridMargin
        {
            get { return _playerOffsetGridMargin; }
            set { Set(ref _playerOffsetGridMargin, value); }
        }

        public int PosIniWindow
        {
            get { return _posIniWindow; }
            set { Set(ref _posIniWindow, value); }
        }

        public double ScaleKampfGrid
        {
            get { return _scaleKampfGrid; }

            set
            {
                Set(ref _scaleKampfGrid, Math.Round(value, 2));
                if (IsShowIniKampf)
                {
                    ((KampfInfoView)KampfWindow.Content).grdMain.LayoutTransform = new ScaleTransform(value, value);
                    KampfWindow.SizeToContent = SizeToContent.Height;
                    // SizeToContent muss wieder auf Manual gesetzt werden da das Window sonst immer
                    // größer wird
                    KampfWindow.SizeToContent = SizeToContent.Manual;
                    SetIniWindowPosition();
                }
            }
        }

        public double ScaleSpielerGrid
        {
            get { return _scaleSpielerGrid; }
            set { Set(ref _scaleSpielerGrid, Math.Round(value < .2 ? .2 : value, 2)); }
        }

        public Kampf.Logic.ManöverInfo SelManöver
        {
            get { return Global.CurrentKampf.SelectedManöver; }
        }

        public bool HeldenInFormationBewegen
        {
            get { return _heldenInFormationBewegen; }
            set { Set(ref _heldenInFormationBewegen, value); }
        }

        public bool LinealAktiv
        {
            get { return _linealAktiv; }
            set { 
                Set(ref _linealAktiv, value);
                if (!value)
                {
                    FinishCurrentTempPathLine();
                    InitLineal = true;
                }
            }
        }

        public bool SpielerScreenActive
        {
            get { return _spielerScreenActive; }
            set { Set(ref _spielerScreenActive, value); }
        }

        public Window SpielerScreenWindow
        {
            get { return _spielerScreenWindow; }
            set { Set(ref _spielerScreenWindow, value); }
        }

        public bool useFog
        {
            get { return _useFog; }

            set
            {
                Set(ref _useFog, value);
                if (value && FogPixelData == null)
                {
                    CreateFogOfWar();
                }
            }
        }

        private Grid _arenaGrid = new Grid();
        private string _backgroundFilename;
        private bool _hasMP4Background;
        private bool _backgroundMp4Mute;
        private string _backgroundImage;
        private double _backgroundOffsetSize = ARENA_GRID_RESOLUTION;
        private double _backgroundOffsetX = 0;
        private double _backgroundOffsetY = 0;
        private int _fogFreeSize = 1;
        private bool _fogFreimachen = false;
        private WriteableBitmap _fogImage;
        private string _fogImageFilename;
        private double _fogOffsetSize = ARENA_GRID_RESOLUTION;
        private double _fogOffsetX = 0;
        private double _fogOffsetY = 0;
        private int[] _fogPixelData;
        private double _fontSize = 14;
        private string _infoText = null;
        private double _iniHeightStart = 0;
        private double _iniWidthStart = 0;
        private double _invBackgroundOffsetY = 0;
        private bool _invertPlayerScrolling = false;
        private bool _isShowIniKampf = false;
        private KampfViewModel _kampf = null;
        private Point _kämpferDnDTempPos = new Point(0, 0);
        private KampfInfoView _kampfIniInfoView = null;
        private Window _kampfWindow = null;
        private ZoomControl _meisterArenaZoomControl = new ZoomControl();
        private double _meisterZoom = .5;

        private double _meisterZoomTransX = 0;

        private double _meisterZoomTransY = 0;

        private Thickness _offsetBackgroudMargin = new Thickness(0, 0, 0, 0);

        private Thickness _offsetFogMargin = new Thickness(0, 0, 0, 0);

        private double _playerGridOffsetX = 0;

        private double _playerGridOffsetY = 0;

        private Thickness _playerOffsetGridMargin = new Thickness(0, 0, 0, 0);

        private int _posIniWindow = 1;

        private double _scaleKampfGrid = 1;

        private double _scaleSpielerGrid = 1;

        private bool _spielerScreenActive = false;

        private bool _heldenInFormationBewegen = false;

        private bool _linealAktiv;

        private Window _spielerScreenWindow = null;

        private bool _useFog = false;

        #endregion Fog

        #region Objektlisten/-ViewSources

        public ObservableCollection<BattlegroundBaseObject> BattlegroundObjects
        {
            get { return _battlegroundObjects ?? (_battlegroundObjects = new ObservableCollection<BattlegroundBaseObject>()); }

            set
            {
                if (Set(ref _battlegroundObjects, value, true))
                {
                    kämpferliste = null;
                    objektliste = null;
                    OnChanged();
                }
            }
        }

        [DependentProperty(nameof(BattlegroundObjects))]
        public CollectionViewSource KämpferListe
        {
            get
            {
                if (kämpferliste == null)
                {
                    kämpferliste = new CollectionViewSource() { Source = BattlegroundObjects };
                    kämpferliste.Filter += filterKämpfer;
                }
                return kämpferliste;
            }
        }

        [DependentProperty(nameof(BattlegroundObjects))]
        public CollectionViewSource ObjektListe
        {
            get
            {
                if (objektliste == null)
                {
                    objektliste = new CollectionViewSource() { Source = BattlegroundObjects };
                    objektliste.Filter += filterObjekt;
                }
                return objektliste;
            }
        }

        private ObservableCollection<BattlegroundBaseObject> _battlegroundObjects;
        private CollectionViewSource kämpferliste = null;

        private CollectionViewSource objektliste = null;

        private void filterKämpfer(object sender, FilterEventArgs f)
        {
            f.Accepted = f.Item is IKämpfer;
        }

        private void filterObjekt(object sender, FilterEventArgs f)
        {
            f.Accepted = !(f.Item is IKämpfer);
        }

        #endregion Objektlisten/-ViewSources

        #region Kampf mit Eventverbindung auf KämpferListe

        public bool DoChangeModPositionSelbst
        {
            get { return _doChangeModPositionSelbst; }
            set { Set(ref _doChangeModPositionSelbst, value); }
        }

        public void UpdateCreaturesFromChangedKampferlist()
        {
            foreach (KämpferInfo k in Global.CurrentKampf.Kampf.Kämpfer)
            {
                ((Wesen)k.Kämpfer).PropertyChanged -= OnWesenPropertyChanged;
            }
            foreach (KämpferInfo k in Global.CurrentKampf.Kampf.Kämpfer)
            {
                ((Wesen)k.Kämpfer).PropertyChanged += OnWesenPropertyChanged;
            }
        }

        private bool _doChangeModPositionSelbst = false;
        private KampfViewModel _kampfVM;

        private void OnKämpferListeChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    System.Collections.IList l1 = e.NewItems;
                    foreach (var item in l1)
                    {
                        AddCreature(((KämpferInfo)item).Kämpfer);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    System.Collections.IList l2 = e.OldItems;
                    foreach (var item in l2)
                    {
                        RemoveCreature(((KämpferInfo)item).Kämpfer);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    System.Collections.IList l3 = e.OldItems;
                    foreach (var item in l3)
                    {
                        RemoveCreature(((KämpferInfo)item).Kämpfer);
                    }

                    System.Collections.IList l4 = e.NewItems;
                    foreach (var item in l4)
                    {
                        AddCreature(((KämpferInfo)item).Kämpfer);
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    RemoveCreatureAll();
                    AddAllCreatures();
                    break;

                case NotifyCollectionChangedAction.Move:
                    //TODO: args.. what exaclty?
                    break;

                default:
                    return;
            }
        }

        /// <summary>
        /// Event gets fired when Wesen.PropertyChanged event fires...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWesenPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Position")
            {
                DoChangeModPositionSelbst = true;
                KämpferInfo ki = Global.CurrentKampf.Kampf.Kämpfer.FirstOrDefault(t => t.Kämpfer == (sender as IKämpfer));
            }
        }

        #endregion Kampf mit Eventverbindung auf KämpferListe

        #region Creature Add/Remove, Clear All

        public void AddAllCreatures()
        {
            foreach (KämpferInfo k in Global.CurrentKampf.Kampf.Kämpfer)
            {
                AddCreature(k.Kämpfer);
            }
            CenterMeisterView(null);
        }

        public void AddCreature(IKämpfer kämpfer)
        {
            if (((Wesen)kämpfer).IsHeld)
            {
                if (BattlegroundObjects.FirstOrDefault(t => t is Held && ((Held)t).HeldGUID == ((Held)kämpfer).HeldGUID) == null)
                {
                    ((Held)kämpfer).LoadBattlegroundPortrait(((Held)kämpfer).Bild, true);
                    BattlegroundObjects.Add(((Held)kämpfer));

                    //ToDo: Initiative in der Manöverliste aktuelisieren

                    //Set Aktuelle Initiative auf geladenen Wert
                    //KämpferInfo kiCurrKampf = CurrKampf.Kämpfer.Where(
                    //    t => ((Wesen)t.Kämpfer).IsHeld &&
                    //         t.Kämpfer.Name == ((Held)kämpfer).Name).FirstOrDefault(
                    //    t => ((Held)t.Kämpfer).HeldGUID == ((Held)kämpfer).HeldGUID);

                    //kiCurrKampf.Initiative = ((Wesen)kämpfer).ki.Initiative;
                }
            }
            else
            {
                ((Gegner)kämpfer).LoadBattlegroundPortrait(((Gegner)kämpfer).Bild, false);
                BattlegroundObjects.Add(((Gegner)kämpfer));

                //ToDo: Initiative in der Manöverliste aktuelisieren

                //Set Aktuelle Initiative auf geladenen Wert
                //KämpferInfo kiCurrKampf = CurrKampf.Kämpfer.Where(
                //    t => !((Wesen)t.Kämpfer).IsHeld &&
                //    t.Kämpfer.Name == ((Gegner)kämpfer).Name).FirstOrDefault(
                //    t => ((Gegner)t.Kämpfer).GegnerBaseGUID == ((Gegner)kämpfer).GegnerBaseGUID);

                //kiCurrKampf.Initiative = ((Wesen)kämpfer).ki.Initiative;
            }
            //Lichtquelle nach Generierung des Kämpfers erstellen
            if (((Wesen)kämpfer).ki.LichtquelleMeter > 0)
                ((Wesen)kämpfer).ki.LichtquelleMeter = ((Wesen)kämpfer).ki.LichtquelleMeter;

        }

        public void ClearBattleground()
        {
            BattlegroundObjects.Where(x => !(x is BattlegroundCreature)).ToList().ForEach(x => BattlegroundObjects.Remove(x));
            BattlegroundObjects.Clear();
            BackgroundImage = null;
            BackgroundFilename = null;
            BackgroundColor = Color.FromArgb(0xFF, 0x36, 0x75, 0x36);
            BackgroundMp4Length = 999;
            BackgroundMp4MaxPosition = 999;
            BackgroundMp4MinPosition = 0;
            BackgroundMp4Mute = false;
            BackgroundMp4Opacity = 1;
            BackgroundMp4Speed = 1;
            BackgroundOffsetX = 0;
            BackgroundOffsetY = 0;
            BackgroundOffsetSize = ARENA_GRID_RESOLUTION;
            HasMP4Background = false;

            FogFreeSize = 1;
            FogImage = null;
            FogOffsetSize = ARENA_GRID_RESOLUTION;
            FogOffsetX = 0;
            FogOffsetY = 0;
            FogImageFilename = null;
            useFog = false;
            Global.CurrentKampf.Kampf.Kämpfer.Clear();
        }

        public void RemoveCreature(IKämpfer creature)
        {
            BattlegroundObjects.Remove((Wesen)creature);
        }

        public void RemoveCreatureAll()
        {
            for (var i = BattlegroundObjects.Count - 1; i >= 0; i--)
            {
                if (BattlegroundObjects[i] is BattlegroundCreature)
                {
                    BattlegroundObjects.RemoveAt(i);
                }
            }
        }

        #endregion Creature Add/Remove, Clear All

        #region Z-Level

        public bool IgnorZLevel
        {
            get { return _ignorZLevel; }

            set
            {
                if (Set(ref _ignorZLevel, value))
                {
                    Ressources.SetVisibilityDependetOnZLevelSelection(ref _battlegroundObjects, VisibleZLevels, IgnorZLevel);
                    if (!value)
                        VisibleZLevels = "";
                    else
                        VisibleZLevels = string.Join(",", PossibleZLevels);

                }
            }
        }

        public List<string> PossibleZLevels
        {
            get { return _possibleZLevels; }

            set
            {
                Set(ref _possibleZLevels, value);
            }
        }

        public bool ShowZ
        {
            get { return _showZ; }

            set
            {
                PossibleZLevels = Ressources.GetPossibleZLevels(BattlegroundObjects);
                Set(ref _showZ, value);
            }
        }

        public string VisibleZLevels
        {
            get { return _visibleZLevels; }

            set
            {
                if (Set(ref _visibleZLevels, value))
                {
                    Ressources.SetVisibilityDependetOnZLevelSelection(ref _battlegroundObjects, VisibleZLevels, IgnorZLevel);
                }
            }
        }

        public double ZLevel
        {
            get { return SelectedObject != null ? SelectedObject.ZLevel : 10; }

            set
            {
                if (SelectedObject != null)
                {
                    SelectedObject.ZLevel = value;
                    PossibleZLevels = Ressources.GetPossibleZLevels(BattlegroundObjects);
                }
                OnChanged();
            }
        }

        private bool _ignorZLevel = true;

        private List<string> _possibleZLevels = new List<string>();

        private bool _showZ;

        private string _visibleZLevels = "10";

        #endregion Z-Level

        #region Überflüssig?

        public string CurrentlySelectedCreature
        {
            get { return _currentlySelectedCreature; }
            set { Set(ref _currentlySelectedCreature, value); }
        }

        //TODO Die getrennte Speicherung von SelectedCreature UND SelectedObject misfällt mir, da beides eigentlich Objekte sind. JO
        private string _currentlySelectedCreature = "";

        #endregion Überflüssig?

        #region Sichtbereich-Optionen

        public bool ShowSightArea
        {
            get { return _showSightArea; }

            set
            {
                Set(ref _showSightArea, value);
            }
        }

        public double SightAreaLength
        {
            get { return _sightAreaLength; }

            set
            {
                if (Set(ref _sightAreaLength, value))
                {
                    Ressources.SetNewSightAreaLength(ref _battlegroundObjects, SightAreaLength);
                }
            }
        }

        private bool _showSightArea = true;
        private double _sightAreaLength = 120;

        #endregion Sichtbereich-Optionen

        #region Anzeigeoptionen

        public bool ShowCreatureName
        {
            get { return _showCreatureName; }

            set
            {
                Set(ref _showCreatureName, value);
            }
        }

        private bool _showCreatureName = true;

        #endregion Anzeigeoptionen

        #region SelectedObject und dessen Eigenschaften und Delete

        public BattlegroundCommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new BattlegroundCommand(Delete)); }
        }

        public double ObjectSize
        {
            get
            {
                if (SelectedObject != null)
                {
                    if (SelectedObject is ImageObject)
                    {
                        return ((ImageObject)SelectedObject).ObjectSize;
                    }
                    if (SelectedObject is MP4Object)
                    {
                        return ((MP4Object)SelectedObject).ObjectSize;
                    }

                    if (SelectedObject is BattlegroundCreature)
                    {
                        return ((BattlegroundCreature)SelectedObject).ObjectSize;
                    }
                }
                return _objectSize;
            }

            set
            {
                if (SelectedObject != null)
                {
                    if (SelectedObject is ImageObject)
                    {
                        ((ImageObject)SelectedObject).ObjectSize = value;
                    }

                    if (SelectedObject is BattlegroundCreature)
                    {
                        ((BattlegroundCreature)SelectedObject).ObjectSize = value;
                    }
                }
                Set(ref _objectSize, value);
            }
        }

        private double _backgroundMp4Opacity = 1;
        public double BackgroundMp4Opacity
        { 
            get { return _backgroundMp4Opacity; }
            set
            {
                Set(ref _backgroundMp4Opacity, value);
                BattlegroundBaseObject bg1 = BattlegroundObjects.Where(t => t is MP4Object).Where(t => (t as MP4Object).IsBackgroundPicture).FirstOrDefault();
                if (bg1 != null)
                    (bg1 as MP4Object).Opacity = value;
            }
        }


        public double Opacity
        {
            get { return SelectedObject != null ? SelectedObject.Opacity : 1; }
            set
            {
                if (SelectedObject != null)
                {
                    SelectedObject.Opacity = value;
                }
                OnChanged(nameof(Opacity));
            }
        }

        public BattlegroundCommand PlayerScreenDownCommand
        {
            get { return _playerScreenDownCommand ?? (_playerScreenDownCommand = new BattlegroundCommand(Down)); }
        }

        public BattlegroundCommand PlayerScreenLeftCommand
        {
            get { return _playerScreenLeftCommand ?? (_playerScreenLeftCommand = new BattlegroundCommand(Left)); }
        }

        public BattlegroundCommand PlayerScreenRightCommand
        {
            get { return _playerScreenRightCommand ?? (_playerScreenRightCommand = new BattlegroundCommand(Right)); }
        }

        public BattlegroundCommand PlayerScreenUpCommand
        {
            get { return _playerScreenUpCommand ?? (_playerScreenUpCommand = new BattlegroundCommand(Up)); }
        }

        public KämpferInfo SelectectedKämpferInfo
        {
            get { return _selectectedKämpferInfo; }
            set { Set(ref _selectectedKämpferInfo, value); }
        }

        public Color SelectedColor
        {
            get { return _selectedColor; }

            set
            {
                if (Set(ref _selectedColor, value))
                {
                    if (SelectedObject != null)
                    {
                        SelectedObject.ObjectColor = new SolidColorBrush(value);
                    }
                }
            }
        }

        public Color SelectedFillColor
        {
            get { return _selectedFillColor; }

            set
            {
                if (Set(ref _selectedFillColor, value))
                {
                    if (SelectedObject != null)
                    {
                        SelectedObject.FillColor = value;
                    }
                }
            }
        }

        [DependentProperty(nameof(SelectedFillColor))]
        public float SelectedFillColorAlpha
        {
            get { return SelectedFillColor.ScA; }

            set
            {
                Color c = SelectedFillColor;
                c.ScA = value;
                SelectedFillColor = c;
            }
        }

        public BattlegroundBaseObject SelectedObject
        {
            get { return _selectedObject; }

            set
            {
                if (_selectedObject != null && _selectedObject.IsMoving)
                {
                    if (Global.CurrentKampf.LabelInfo != null)
                        Global.CurrentKampf.LabelInfo = null;
                    return;
                }

                BattlegroundObjects.ToList().ForEach(x => x.IsHighlighted = false);
                _selectedObject = value;
                if (SelectedObject is BattlegroundCreature)
                {
                    Global.CurrentKampf.SelectedKämpfer = Global.CurrentKampf.Kampf.Kämpfer.FirstOrDefault(ki => ki.Kämpfer == ((IKämpfer)SelectedObject));             
                    Global.CurrentKampf.LabelInfo = null;
                }
                else
                    Global.CurrentKampf.LabelInfo = "Objekt: Verschieben über Maus, Drehen mit Rechtsklick, Entfernen mit der Entf.-Taste";

                if (SelectedObject != null)
                {
                    SelectedFillColor = SelectedObject.FillColor;
                    SelectedColor = SelectedObject.ObjectXMLColor;
                    StrokeThickness = SelectedObject.StrokeThickness;

                    SelectedObject.IsSelected = true;
                    if (SelectedObject is BattlegroundCreature)
                    {
                        Global.CurrentKampf.SelectedManöverInfo = Global.CurrentKampf.Kampf.SortedInitiativListe
                            .FirstOrDefault(ki => ki.Manöver.Ausführender.Kämpfer == ((IKämpfer)SelectedObject));
                    }
                }
                OnChanged(nameof(SelectedObject));
                try
                {
                    if (SelectedObject is BattlegroundCreature)
                    {
                        if (BattlegroundObjects.IndexOf(SelectedObject) != BattlegroundObjects.Count - 1)
                        {
                            var indexObj = BattlegroundObjects.IndexOf(SelectedObject);
                            var lastPos = BattlegroundObjects.Count - 1;
                            if (lastPos - 1 != indexObj)
                            {
                                BattlegroundObjects.Move(indexObj, lastPos);
                            }
                        }
                        Global.CurrentKampf.SelectedKämpfer = (SelectedObject as BattlegroundCreature).ki;
                    }
                }
                catch (Exception ex)
                { ViewHelper.ShowError("BattlegroundObjects.Move Befehl generierte einen Fehler", ex); }
            }
        }

        public BattlegroundBaseObject SelectedTempObject
        {
            get { return _selectedTempObject; }
            set { Set(ref _selectedTempObject, value); }
        }

        public double StrokeThickness
        {
            get { return _strokeThickness; }

            set
            {
                if (Set(ref _strokeThickness, value))
                {
                    if (SelectedObject != null)
                    {
                        SelectedObject.StrokeThickness = value;
                    }
                }
            }
        }

        public void Delete()
        {
            if (SelectedObject != null)
            {
                if (SelectedObject is BattlegroundCreature)
                {
                    Global.CurrentKampf.DeleteKämpfer();
                }
                else
                {
                    BattlegroundObjects.Remove(SelectedObject);
                }
            }
        }

        public void Down()
        { PlayerGridOffsetY = PlayerGridOffsetY - 80 > -ARENA_GRID_RESOLUTION ? PlayerGridOffsetY - 80 : -ARENA_GRID_RESOLUTION; }

        public void Left()
        { PlayerGridOffsetX = PlayerGridOffsetX - 80 > 0 ? PlayerGridOffsetX - 80 : 0; }

        public void Right()
        { PlayerGridOffsetX = PlayerGridOffsetX + 80 <= ARENA_GRID_RESOLUTION ? PlayerGridOffsetX + 80 : ARENA_GRID_RESOLUTION; }

        public void Up()
        { PlayerGridOffsetY = PlayerGridOffsetY + 80 <= 0 ? PlayerGridOffsetY + 80 : 0; }

        private BattlegroundCommand _deleteCommand;

        private double _objectSize = 1;
        private BattlegroundCommand _playerScreenDownCommand;
        private BattlegroundCommand _playerScreenLeftCommand;
        private BattlegroundCommand _playerScreenRightCommand;
        private BattlegroundCommand _playerScreenUpCommand;

        private KämpferInfo _selectectedKämpferInfo;

        private Color _selectedColor = Colors.DarkGray, _selectedFillColor = Colors.LightGray;

        private BattlegroundBaseObject _selectedObject;

        private BattlegroundBaseObject _selectedTempObject;

        private double _strokeThickness = 20;

        #endregion SelectedObject und dessen Eigenschaften und Delete

        #region Laden/Speichern

        public bool LoadWithoutPictures
        {
            get { return _loadWithoutPictures; }
            set { Set(ref _loadWithoutPictures, value); }
        }

        public bool SaveWithoutPictures
        {
            get { return _saveWithoutPictures; }
            set { Set(ref _saveWithoutPictures, value); }
        }

        public Position GetPositionFromImage(string posString)
        {
            Position p = Kampf.Logic.Position.Stehend;

            if (posString.Contains("Floating") || posString == "Schwebend")
            {
                p = Position.Schwebend;
            }
            else
                if (posString.Contains("Flying") || posString == "Fliegend")
            {
                p = Position.Fliegend;
            }
            else
                    if (posString.Contains("Kneeling") || posString == "Kniend")
            {
                p = Position.Kniend;
            }
            else
                        if (posString.Contains("OnTheGround") || posString == "Liegend")
            {
                p = Position.Liegend;
            }
            else
                            if (posString.Contains("Riding") || posString == "Reitend")
            {
                p = Position.Reitend;
            }
            else
            {
                p = Position.Stehend;
            }

            return p;
        }

        public void LoadBattlegroundFromXML(string filename)
        {
            ClearBattleground();

            var bg = new BattlegroundXMLLoadSave();
            ObservableCollection<BattlegroundBaseObject> ocloaded = bg.LoadMapFromXML(filename, LoadWithoutPictures);

            foreach (BattlegroundBaseObject element in ocloaded)
            {
                if (!((element is ImageObject || element is MP4Object) && LoadWithoutPictures))
                {
                    System.Console.WriteLine("LOAD FROM XML: " + element.ToString());
                    BattlegroundObjects.Add(element);
                }
            }
            PossibleZLevels = Ressources.GetPossibleZLevels(BattlegroundObjects);

            //Sichtbereich auf Creature setzen
            foreach (BattlegroundBaseObject bgOb in BattlegroundObjects)
            {
                if (bgOb is BattlegroundCreature)
                {
                    (bgOb as BattlegroundCreature).CreatureX = (bgOb as BattlegroundCreature).CreatureX;
                    (bgOb as BattlegroundCreature).CreatureY = (bgOb as BattlegroundCreature).CreatureY;
                    (bgOb as BattlegroundCreature).MidCreatureX = (bgOb as BattlegroundCreature).CreatureX;
                    (bgOb as BattlegroundCreature).MidCreatureY = (bgOb as BattlegroundCreature).CreatureY;
                    if ((bgOb as BattlegroundCreature) is Held)
                    {
                        (bgOb as BattlegroundCreature).PortraitFileName = ((bgOb as BattlegroundCreature) as Held).Bild;
                        ((bgOb as BattlegroundCreature) as Held).LoadBattlegroundPortrait((bgOb as BattlegroundCreature).PortraitFileName, true);
                        ((bgOb as BattlegroundCreature) as Held).Position = GetPositionFromImage((bgOb as BattlegroundCreature).CreaturePosition);
                    }
                    else
                    {
                        (bgOb as BattlegroundCreature).PortraitFileName = ((bgOb as BattlegroundCreature) as Gegner).Bild;
                        ((bgOb as BattlegroundCreature) as Gegner).LoadBattlegroundPortrait((bgOb as BattlegroundCreature).PortraitFileName, false);
                        ((bgOb as BattlegroundCreature) as Gegner).Position = GetPositionFromImage((bgOb as BattlegroundCreature).CreaturePosition);
                    }

                }
            }

            BattlegroundBaseObject bgO = BattlegroundObjects.Where(t => t is ImageObject).Where(t => (t as ImageObject).IsBackgroundPicture).FirstOrDefault();
            if (bgO != null)
            {
                BackgroundImage = (bgO as ImageObject).PictureUrl;
                BackgroundOffsetX = bgO.ZDisplayX;
                BackgroundOffsetY = bgO.ZDisplayY;
                BackgroundOffsetSize = (bgO as ImageObject).ObjectSize;
                (bgO as BattlegroundBaseObject).IsVisible = false;
            }
            else
            {
                BattlegroundBaseObject bg0MP4 = BattlegroundObjects.Where(t => t is MP4Object).Where(t => (t as MP4Object).IsBackgroundPicture).FirstOrDefault();
                if (bg0MP4 != null)
                {
                    OnSetBackgroundClick.Execute((bg0MP4 as MP4Object));                     
                    BackgroundOffsetX = bg0MP4.ZDisplayX;
                    BackgroundOffsetY = bg0MP4.ZDisplayY;
                    BackgroundOffsetSize = (bg0MP4 as MP4Object).ObjectSize;
                    BackgroundMp4Length = Convert.ToInt32((bg0MP4 as MP4Object).VideoLength);
                    BackgroundMp4MinPosition = Convert.ToInt32((bg0MP4 as MP4Object).MinPosition);
                    BackgroundMp4MaxPosition = Convert.ToInt32((bg0MP4 as MP4Object).MaxPosition);
                    BackgroundMp4Mute = (bg0MP4 as MP4Object).IsMute;
                    BackgroundMp4Opacity = (bg0MP4 as MP4Object).Opacity;
                    BackgroundMp4Speed = (bg0MP4 as MP4Object).VideoSpeedRatio;
                    (bg0MP4 as BattlegroundBaseObject).IsVisible = false;
                }
            }

            BattlegroundBaseObject bg1 = BattlegroundObjects.Where(t => t is ImageObject).Where(t => (t as ImageObject).IsFogPicture).FirstOrDefault();
            if (bg1 != null)
            {
                FogImageFilename = (bg1 as ImageObject).PictureUrl;
                FogOffsetX = bg1.ZDisplayX;
                FogOffsetY = bg1.ZDisplayY;
                FogOffsetSize = (bg1 as ImageObject).ObjectSize;

                // Load Fog-of-War //
                var originalImage = new BitmapImage();
                originalImage.BeginInit();
                originalImage.UriSource = new Uri(FogImageFilename, UriKind.Absolute);
                originalImage.EndInit();
                BitmapSource bitmapSource = new FormatConvertedBitmap(originalImage, PixelFormats.Bgra32, null, 0);
                var wbmap = new WriteableBitmap(bitmapSource);

                var h = wbmap.PixelHeight;
                var w = wbmap.PixelWidth;
                FogPixelData = new int[w * h];
                var widthInByte = 4 * w;
                (bg1 as ImageObject).ZLevel = 103;

                wbmap.CopyPixels(FogPixelData, widthInByte, 0);
                System.Drawing.Bitmap bm = BitmapFromWriteableBitmap(wbmap);
                var img = (System.Drawing.Image)bm;
                FogImage = wbmap;
                useFog = true;
                (bg1 as BattlegroundBaseObject).IsVisible = false;
            }

            ObservableCollection<double> lstSettings = bg.LoadSettingsFromXML(filename);
            if (lstSettings.Count == 0)
            {
                return;
            }

            BackgroundOffsetSize = lstSettings[0];
            BackgroundOffsetX = lstSettings[1];
            InvBackgroundOffsetY = lstSettings[2];

            PlayerGridOffsetX = lstSettings[3];
            PlayerGridOffsetY = lstSettings[4];
            ScaleSpielerGrid = lstSettings[5];
            ScaleKampfGrid = lstSettings[6];
            //lstSettings 1 = Recheck,   0 = Hex,   -1 = none
            RechteckGrid = lstSettings[7] == 1;
            HexGrid = lstSettings[7] == 0;
            if (lstSettings.Count <= 8)
                return;
            //Zusätzliche Battlemap Settings laden
            Color gridCol = new Color()
            {
                A = Convert.ToByte(lstSettings[8]),
                B = Convert.ToByte(lstSettings[9]),
                G = Convert.ToByte(lstSettings[10]),
                R = Convert.ToByte(lstSettings[11])
            };
            GridColor = gridCol;
            ShowSightArea = lstSettings[12] == 1;
            SightAreaLength = lstSettings[13];
            ShowCreatureName = lstSettings[14] == 1;
            useFog = lstSettings[15] == 1;

            //Kampf auf KR setzen
            CurrKampf.KampfNeuStarten(false);
            while (CurrKampf.Kampfrunde < lstSettings[16])
                CurrKampf.NeueKampfrunde();
            IsEditorModeEnabled = lstSettings[17] == 1;

            //Background Color
            if (lstSettings.Count <= 18)
                return;
            Color backCol = new Color()
            {
                A = Convert.ToByte(lstSettings[18]),
                B = Convert.ToByte(lstSettings[19]),
                G = Convert.ToByte(lstSettings[20]),
                R = Convert.ToByte(lstSettings[21])
            };
            BackgroundColor = backCol;
        }

        public void SaveBattlegroundToXML(string filename, bool GiveFeedback = true)
        {
            var lstSettings = new List<double>
            {
                BackgroundOffsetSize,
                BackgroundOffsetX,
                InvBackgroundOffsetY
            };

            if (useFog)
            {
                for (var i = BattlegroundObjects.Count - 1; i >= 0; i--)
                {
                    if (BattlegroundObjects[i] is ImageObject && ((ImageObject)BattlegroundObjects[i]).IsFogPicture)
                    {
                        BattlegroundObjects.Remove(BattlegroundObjects[i]);

                        var olfile = FogImageFilename;
                        Nullable<int> x = null;
                        while (File.Exists(Ressources.GetFullApplicationPathForPictures() + "Fog-of-War_" + DateTime.Today.ToShortDateString() + x + ".png"))
                        {
                            if (x == null)
                            {
                                x = 0;
                            }
                            else
                            {
                                x++;
                            }
                        }

                        FogImageFilename = Ressources.GetFullApplicationPathForPictures() + "Fog-of-War_" + DateTime.Today.ToShortDateString() + x + ".png";
                        CreateThumbnail(FogImageFilename, FogImage.Clone());
                        //File.Delete(olfile);
                        ImageObject io = CreateImageObject(FogImageFilename, new Point(FogOffsetX, FogOffsetY));
                        io.IsFogPicture = true;
                        io.ZLevel = 103;
                        io.IsVisible = false;
                    }
                }
            }

            lstSettings.Add(PlayerGridOffsetX);
            lstSettings.Add(PlayerGridOffsetY);
            lstSettings.Add(ScaleSpielerGrid);

            lstSettings.Add(ScaleKampfGrid);
            lstSettings.Add(RechteckGrid ? 1 : HexGrid? 0: -1);

            //Zusätzliche 
            lstSettings.Add(GridColor.A);
            lstSettings.Add(GridColor.B);
            lstSettings.Add(GridColor.G);
            lstSettings.Add(GridColor.R);
            lstSettings.Add(ShowSightArea ? 1 : 0);
            lstSettings.Add(SightAreaLength);
            lstSettings.Add(ShowCreatureName ? 1 : 0);
            lstSettings.Add(useFog ? 1 : 0);
            lstSettings.Add(CurrKampf.Kampfrunde);
            lstSettings.Add(IsEditorModeEnabled ? 1 : 0);
            //Background Color
            lstSettings.Add(BackgroundColor.A);
            lstSettings.Add(BackgroundColor.B);
            lstSettings.Add(BackgroundColor.G);
            lstSettings.Add(BackgroundColor.R);

            var bg = new BattlegroundXMLLoadSave();
            bg.SaveMapToXML(BattlegroundObjects, filename, SaveWithoutPictures, lstSettings, GiveFeedback);
        }

        private bool _loadWithoutPictures = false, _saveWithoutPictures = false;

        #endregion Laden/Speichern

        #region Grid

        /// <summary>
        /// Farbe des HexGrids bzw des RechteckGrids
        /// </summary>
        private Color _linealColor = Colors.DarkBlue;
        public Color LinealColor
        {
            get { return _linealColor; }
            set { Set(ref _linealColor, value); }
        }


        /// <summary>
        /// Farbe des HexGrids bzw des RechteckGrids
        /// </summary>
        public Color GridColor
        {
            get { return gridColor; }
            set
            {
                Set(ref gridColor, value);
                if (RechteckGrid)
                {
                    RechteckGrid = true;
                }
                else
                {
                    HexGrid = true;
                }
            }
        }

        public bool HexGrid
        {
            get { return hexGrid; }

            set
            {
                if (value)
                {
                    RechteckGrid = false;
                }

                Set(ref hexGrid, value);
            }
        }

        public bool RechteckGrid
        {
            get { return rechteckGrid; }

            set
            {
                if (value)
                {
                    HexGrid = false;
                }

                Set(ref rechteckGrid, value);
            }
        }

        [DependentProperty(nameof(RechteckGrid)), DependentProperty(nameof(HexGrid))]
        public PathGeometry TilePathData
        {
            get
            {
                if (RechteckGrid)
                {
                    if (rechteckPath == null)
                    {
                        rechteckPath = BattlegroundUtilities.RechteckCellTile(100);
                    }

                    return rechteckPath;
                }
                else
                    if (HexGrid)
                {
                    if (hextilePath == null)
                    {
                        hextilePath = BattlegroundUtilities.HexCellTile(100);
                    }

                    return hextilePath;
                }
                else
                {
                    return null;
                }
            }
        }

        [DependentProperty(nameof(RechteckGrid)), DependentProperty(nameof(HexGrid))]
        public Rect TileViewPort
        {
            get
            {
                var scale = 57.735;
                if (RechteckGrid)
                {
                    return new Rect(0, 0, HEXAGON_PERIMETER * scale, HEXAGON_PERIMETER * scale);
                }
                else
                {
                    return new Rect(0, 0, 3 * scale, HEXAGON_PERIMETER * scale);
                }
            }
        }

        private const double HEXAGON_PERIMETER = 3.4641016151377545870548926830117;
        private Color gridColor = Color.FromRgb(255, 255, 255);
        private bool hexGrid = false;
        private bool rechteckGrid = true;
        private PathGeometry rechteckPath = null, hextilePath = null;

        #endregion Grid

        #region ZeichenModi

        [DependentProperty(nameof(ZeichenModus))]
        public bool CreateFilledLine
        {
            get { return ZeichenModus == ZeichenModus.Fläche; }
        }

        [DependentProperty(nameof(ZeichenModus))]
        public bool CreateLine
        {
            get { return ZeichenModus == ZeichenModus.Linie; }
        }

        public ZeichenModus ZeichenModus
        {
            get { return _zeichenModus; }

            set
            {
                if (Set(ref _zeichenModus, value))
                {
                    if (value == ZeichenModus.Linie || value == ZeichenModus.Fläche)
                    {
                        if (SelectedColor.ToString() == "#00FFFFFF")
                            SelectedColor = Colors.DarkGray;
                        if (value == ZeichenModus.Fläche && SelectedFillColor.ToString() == "#00FFFFFF")
                            SelectedFillColor = Colors.LightGray;
                        Global.CurrentKampf.LabelInfo =
                            value == ZeichenModus.Linie ?
                            "Linie zeichnen: ziehe mit der Maus eine Linie auf die Battlemap":
                            "Fläche zeichnen: halte die linke Maustaste gedückt um die Flächenzeichnung zu beginnen";
                    }
                    else
                        Global.CurrentKampf.LabelInfo = null;

                    //eventuell aktionen ausführen, wie Bild platzieren
                    //oder Properties auf dem model umsetzen wie CreateLine = true
                }
            }
        }

        private ZeichenModus _zeichenModus;

        #endregion ZeichenModi

        #region -- Screen Pointer --

        public bool IsPointerVisible
        {
            get { return _isPointerVisible; }

            set
            {
                SelectedObject = null;
                _isPointerVisible = value;
                PointerVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                OnChanged(nameof(IsPointerVisible));
            }
        }

        public double PointerDurchmesser
        {
            get { return _pointerDurchmesser; }
            set { Set(ref _pointerDurchmesser, value); }
        }

        public System.Windows.Thickness PointerMargin
        {
            get { return _pointerMargin; }
            set { Set(ref _pointerMargin, value); }
        }

        public System.Windows.Visibility PointerVisibility
        {
            get { return _pointerVisibility; }
            set { Set(ref _pointerVisibility, value); }
        }
        
        public void SetPointer(object parameter)
        {
            if (parameter == null || !(parameter is Grid))
            {
                return;
            }

            var grid = (Grid)parameter;
            Point mousePos = System.Windows.Input.Mouse.GetPosition(grid);
            _xScale = mousePos.X / grid.ActualWidth;
            _yScale = mousePos.Y / grid.ActualHeight;
            PointerMargin = new Thickness(mousePos.X - PointerDurchmesser / 2, mousePos.Y - PointerDurchmesser / 2, 0, 0);
        }

        private bool _isPointerVisible = false;
        private double _pointerDurchmesser = 25.0;

        private System.Windows.Thickness _pointerMargin = new Thickness();

        private System.Windows.Visibility _pointerVisibility = Visibility.Collapsed;

        private double _xScale = 1;

        private double _yScale = 1;

        #endregion -- Screen Pointer --

        //TODO fixme or replace me

        #region Sticky Stuff, der gerade nicht richtig funktioniert

        public List<BattlegroundBaseObject> StickyListBoxBattlegroundObjects
        {
            get { return BattlegroundObjects.Where(x => x is Held && x.IsSticked).ToList(); }
            set { value = BattlegroundObjects.Where(x => x is Held && x.IsSticked).ToList(); OnChanged(nameof(StickyListBoxBattlegroundObjects)); }
        }

        public void StickEnemies()
        {
            if (BattlegroundObjects.Where(x => x is Held && x.IsSticked).Any())
            {
                return;
            }

            if (!BattlegroundObjects.Where(x => x is Gegner).Any())
            {
                return;
            }

            foreach (BattlegroundBaseObject h in BattlegroundObjects)
            {
                if (h is Gegner)
                {
                    h.IsSticked = true;
                }
            }
            CurrentlySelectedCreature = ((Gegner)BattlegroundObjects.Where(x => x is Gegner && x.IsSticked).First()).Name;
        }

        public void StickHeroes()
        {
            if (BattlegroundObjects.Where(x => x is Gegner && x.IsSticked).Any())
            {
                return;
            }

            if (!BattlegroundObjects.Where(x => x is Held).Any())
            {
                return;
            }

            foreach (BattlegroundBaseObject h in BattlegroundObjects)
            {
                if (h is Held)
                {
                    h.IsSticked = true;
                }
            }
            CurrentlySelectedCreature = ((Held)BattlegroundObjects.Where(x => x is Held && x.IsSticked).First()).Name;
        }

        private readonly List<BattlegroundBaseObject> stickyHeroesTempList = new List<BattlegroundBaseObject>();

        private System.Drawing.Bitmap BitmapFromWriteableBitmap(WriteableBitmap writeBmp)
        {
            System.Drawing.Bitmap bmp;
            using (var outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(writeBmp));
                enc.Save(outStream);
                bmp = new System.Drawing.Bitmap(outStream);
            }
            return bmp;
        }

        private BitmapImage ConvertWriteableBitmapToBitmapImage(WriteableBitmap wbm)
        {
            var bmImage = new BitmapImage();
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(wbm));
                encoder.Save(stream);
                bmImage.BeginInit();
                bmImage.CacheOption = BitmapCacheOption.OnLoad;
                bmImage.StreamSource = stream;
                bmImage.EndInit();
                bmImage.Freeze();
            }
            return bmImage;
        }

        private void CreateFogOfWar()
        {
            //Fog of War
            var originalImage = new BitmapImage();
            originalImage.BeginInit();
            originalImage.UriSource = new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Bodenplan/FogOfWar.png", UriKind.Absolute);
            originalImage.EndInit();
            BitmapSource bitmapSource = new FormatConvertedBitmap(originalImage, PixelFormats.Bgra32, null, 0);
            var wbmap = new WriteableBitmap(bitmapSource);

            var h = wbmap.PixelHeight;
            var w = wbmap.PixelWidth;
            FogPixelData = new int[w * h];
            var widthInByte = 4 * w;

            wbmap.CopyPixels(FogPixelData, widthInByte, 0);
            System.Drawing.Bitmap bm = BitmapFromWriteableBitmap(wbmap);
            var img = (System.Drawing.Image)bm;
            FogImage = wbmap;
            Nullable<int> i = null;
            while (File.Exists(Ressources.GetFullApplicationPathForPictures() + "Fog-of-War" + i + ".png"))
            {
                if (i == null)
                {
                    i = 0;
                }
                else
                {
                    i++;
                }
            }

            FogImageFilename = Ressources.GetFullApplicationPathForPictures() + "Fog-of-War" + i + ".png";
            CreateThumbnail(FogImageFilename, FogImage.Clone());
            ImageObject io = CreateImageObject(FogImageFilename, new Point(FogOffsetX, FogOffsetY));
            io.IsFogPicture = true;
            io.ZLevel = 103;
            io.IsVisible = false;
        }

        private void CreateThumbnail(string filename, BitmapSource image5)
        {
            if (filename != string.Empty)
            {
                var encoder5 = new PngBitmapEncoder();
                encoder5.Frames.Add(BitmapFrame.Create(image5));
                using (Stream stream = File.Create(filename))
                {
                    encoder5.Save(stream);
                    stream.Dispose();
                }
            }
        }

        #endregion Sticky Stuff, der gerade nicht richtig funktioniert

        #region Commands

        private Base.CommandBase onSaveXML_Battlemap = null;
        public Base.CommandBase OnSaveXML_Battlemap
        {
            get
            {
                if (onSaveXML_Battlemap == null)
                    onSaveXML_Battlemap = new Base.CommandBase(SaveXML_Battlemap, null);
                return onSaveXML_Battlemap;
            }
        }

        private void SaveXML_Battlemap(object sender)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "Battleground_" + System.DateTime.Now.ToShortDateString(), // Default file name
                DefaultExt = ".xml",
                Filter = "XML Files (.xml)|*.xml"
            };
            var result = dlg.ShowDialog();

            if (result == true)
                SaveBattlegroundToXML(dlg.FileName);
        }

        private Base.CommandBase onLoadXML_Battlemap = null;
        public Base.CommandBase OnLoadXML_Battlemap
        {
            get
            {
                if (onLoadXML_Battlemap == null)
                    onLoadXML_Battlemap = new Base.CommandBase(LoadXML_Battlemap, null);
                return onLoadXML_Battlemap;
            }
        }

        private void LoadXML_Battlemap(object sender)
        {
            var result = false;
            var filename = sender ?? null;
            if (sender == null)
            {
                var dlg = new Microsoft.Win32.OpenFileDialog
                {
                    DefaultExt = ".xml",
                    Filter = "XML Files (.xml)|*.xml"
                };
                result = dlg.ShowDialog() ?? false;
                if (result)
                    filename = dlg.FileName;
            }
            else
                result = true;

            if (result == true)
            {
                IsLoading = true;
                Global.CurrentKampf.Kampf.Kämpfer.Clear();
                Global.CurrentKampf.BodenplanViewModel.RemoveCreatureAll();
                BackgroundImage = null;
                LoadBattlegroundFromXML(filename as string);
                UpdateCreatureLevelToTop();
                BattlegroundBaseObject bg0MP4 = BattlegroundObjects.Where(t => t is MP4Object).Where(t => (t as MP4Object).IsBackgroundPicture).FirstOrDefault();
                if (bg0MP4 != null)
                    SetBackgroundClick(bg0MP4);
                    //IsLoadingwird im View auf False zurückgesetzt
                else
                    IsLoading = false;
            }
        }


        private Base.CommandBase onLoadLastKRXML_Battlemap = null;
        public Base.CommandBase OnLoadLastKRXML_Battlemap
        {
            get
            {
                if (onLoadLastKRXML_Battlemap == null)
                    onLoadLastKRXML_Battlemap = new Base.CommandBase(LoadLastKRXML_Battlemap, null);
                return onLoadLastKRXML_Battlemap;
            }
        }

        private void LoadLastKRXML_Battlemap(object sender)
        {
            if (!ViewHelper.Confirm("Laden der letzten KR des letzten Kampfes",
                "Wollen Sie den momentanen Kampf verwerfen und die letzte KR des letzten Kampfes laden?"))
                return;

            string bodenplanPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) +
                @"\Daten\Bodenplan\Battleground_Letzte_KR.xml";
            if (Directory.Exists(Path.GetDirectoryName(bodenplanPath)) && File.Exists(bodenplanPath))
            {
                LoadXML_Battlemap(bodenplanPath);
                //IsLoading = true;
                //Global.CurrentKampf.Kampf.Kämpfer.Clear();
                //Global.CurrentKampf.BodenplanViewModel.RemoveCreatureAll();
                //BackgroundImage = null;
                //LoadBattlegroundFromXML(bodenplanPath);
                //UpdateCreatureLevelToTop();
                //BattlegroundBaseObject bg0MP4 = BattlegroundObjects.Where(t => t is MP4Object).Where(t => (t as MP4Object).IsBackgroundPicture).FirstOrDefault();
                //if (bg0MP4 != null)
                //    SetBackgroundClick(bg0MP4);
                ////IsLoadingwird im View auf False zurückgesetzt
                //else
                //    IsLoading = false;
            }
            else
                ViewHelper.Popup("Die temporäre Datei " + Environment.NewLine + bodenplanPath + Environment.NewLine + " konnte nicht gefunden werden");
        }

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
        private Base.CommandBase _onBtnLinealFarbeAnpassen = null;
        public Base.CommandBase OnBtnLinealFarbeAnpassen
        {
            get
            {
                if (_onBtnLinealFarbeAnpassen == null)
                {
                    _onBtnLinealFarbeAnpassen = new Base.CommandBase(LinealFarbeAnpassen, null);
                }
                return _onBtnLinealFarbeAnpassen;
            }
        }

        private void LinealFarbeAnpassen(object obj)
        {
            Xceed.Wpf.Toolkit.ColorPicker colorPicker = new Xceed.Wpf.Toolkit.ColorPicker();
            colorPicker.DisplayColorAndName = true;
            colorPicker.IsOpen = true;
            colorPicker.SelectedColor = Colors.Red;
            colorPicker.ShowAdvancedButton = true;

        }


        public Base.CommandBase OnBtnRenewFogOfWar
        {
            get
            {
                if (_onBtnRenewFogOfWar == null)
                {
                    _onBtnRenewFogOfWar = new Base.CommandBase(RenewFogOfWar, null);
                }
                return _onBtnRenewFogOfWar;
            }
        }

        private void RenewFogOfWar(object obj)
        {
            if (!ViewHelper.Confirm("Fog-of-War erneuern", "Soll der aktuelle Fog-of-War gelöscht und von neuem begonnen werden?")) return;
            CreateFogOfWar();
        }

        public Base.CommandBase OnBtnCenterMeisterView
        {
            get
            {
                if (_onBtnCenterMeisterView == null)
                {
                    _onBtnCenterMeisterView = new Base.CommandBase(CenterMeisterView, null);
                }
                return _onBtnCenterMeisterView;
            }
        }

        public Base.CommandBase OnBtnCenterPlayerView
        {
            get
            {
                if (_onBtnCenterPlayerView == null)
                {
                    _onBtnCenterPlayerView = new Base.CommandBase(CenterPlayerView, null);
                }
                return _onBtnCenterPlayerView;
            }
        }

        public Base.CommandBase onBtnPosIniWindow
        {
            get
            {
                if (_onBtnPosIniWindow == null)
                {
                    _onBtnPosIniWindow = new Base.CommandBase(BtnPosIniWindow, null);
                }
                return _onBtnPosIniWindow;
            }
        }

        public Base.CommandBase OnBtnUmwandelnAttacke
        {
            get
            {
                if (_onBtnUmwandelnAttacke == null)
                {
                    _onBtnUmwandelnAttacke = new Base.CommandBase(UmwandelnAttacke, null);
                }
                return _onBtnUmwandelnAttacke;
            }
        }

        public Base.CommandBase OnBtnUmwandelnFernkampf
        {
            get
            {
                if (_onBtnUmwandelnFernkampf == null)
                {
                    _onBtnUmwandelnFernkampf = new Base.CommandBase(UmwandelnFernkampf, null);
                }
                return _onBtnUmwandelnFernkampf;
            }
        }

        public Base.CommandBase OnBtnUmwandelnSonstiges
        {
            get
            {
                if (_onBtnUmwandelnSonstiges == null)
                {
                    _onBtnUmwandelnSonstiges = new Base.CommandBase(UmwandelnSonstiges, null);
                }
                return _onBtnUmwandelnSonstiges;
            }
        }

        public Base.CommandBase OnSetBackgroundClick
        {
            get
            {
                if (_onSetBackgroundClick == null)
                {
                    _onSetBackgroundClick = new Base.CommandBase(SetBackgroundClick, null);
                }
                return _onSetBackgroundClick;
            }
        }

        private Base.CommandBase _onBtnRenewFogOfWar = null;
        private Base.CommandBase _onBtnCenterMeisterView = null;
        private Base.CommandBase _onBtnCenterPlayerView = null;
        private Base.CommandBase _onBtnPosIniWindow = null;
        private Base.CommandBase _onBtnUmwandelnAttacke = null;

        private Base.CommandBase _onBtnUmwandelnFernkampf = null;

        private Base.CommandBase _onBtnUmwandelnSonstiges = null;

        private Base.CommandBase _onSetBackgroundClick = null;
        private Base.CommandBase _onVideoTestClick = null;

        private void BtnPosIniWindow(object obj)
        {
            PosIniWindow++;
            if (PosIniWindow == 5)
            {
                PosIniWindow = 1;
            }

            SetIniWindowPosition();
        }

        public void CenterMeisterView(object obj)
        {
            MeisterZoom = 1;
            if (Global.ContextHeld.HeldenGruppeListe.Count == 0)
            {
                return;
            }

            var xMin = Global.ContextHeld.HeldenGruppeListe.Min(t => t.CreatureX) - Global.ContextHeld.HeldenGruppeListe[0].CreatureWidth;
            var yMin = Global.ContextHeld.HeldenGruppeListe.Min(t => t.CreatureY) - Global.ContextHeld.HeldenGruppeListe[0].CreatureHeight;
            MeisterZoomTransX = -xMin;
            MeisterZoomTransY = -yMin;
            MeisterZoom = .5;
            CenterPlayerView(null);
        }

        private void CenterPlayerView(object obj)
        {
            double x1 = ARENA_GRID_RESOLUTION;
            double y1 = ARENA_GRID_RESOLUTION;
            double x2 = ARENA_GRID_RESOLUTION;
            double y2 = ARENA_GRID_RESOLUTION;

            foreach (Held h in Global.ContextHeld.HeldenGruppeListe)
            {
                if (h.CreatureX < x1)
                {
                    x1 = h.CreatureX - h.CreatureWidth;
                }

                if (h.CreatureY < y1)
                {
                    y1 = h.CreatureY + h.CreatureHeight;
                }

                if (h.CreatureX > x2)
                {
                    x2 = h.CreatureX + h.CreatureWidth;
                }

                if (h.CreatureY < y2)
                {
                    y2 = h.CreatureY - h.CreatureHeight;
                }
            }
            PlayerGridOffsetX = (x1);
            PlayerGridOffsetY = (-y2);
        }
        

        private void SetBackgroundClick(object obj)
        {
            if (obj == null) //Button von User gedrückt
            {
                HasMP4Background = false;
                //Löschen vorhandener Background-Objekte
                BattlegroundObjects.Remove(BattlegroundObjects.Where(t => t is ImageObject && (t as ImageObject).IsBackgroundPicture).FirstOrDefault());            
                BattlegroundObjects.Remove(BattlegroundObjects.Where(t => t is MP4Object && (t as MP4Object).IsBackgroundPicture).FirstOrDefault());

                BackgroundImage = ViewHelper.ChooseFile("Hintergrundbild setzen", "", false, new string[10] { "bmp", "gif", "jpg", "jpeg", "jpe", "jfif", "png", "tif", "tiff", "mp4" });

                BackgroundMp4MinPosition = 0;
                BackgroundMp4MaxPosition = BackgroundMp4Length;
            }
            else
            {
                BackgroundImage = (obj as MP4Object).VideoUrl;
            }

            BackgroundMP4LoadedBehavior = MediaState.Play;
            if (string.IsNullOrEmpty(BackgroundImage))
            {
                BackgroundImage = null;
                return;
            }

            BackgroundFilename = new FileInfo(BackgroundImage).Name ?? "";
            if (!BackgroundImage.ToLower().EndsWith(".mp4"))
            {
                ImageObject io = CreateImageObject(BackgroundImage, new Point(BackgroundOffsetX, BackgroundOffsetY));
                io.IsBackgroundPicture = true;
                io.IsVisible = false;
            }
            else
            //Video-Datei ausgewählt
            {
                if (obj == null)//Button von User gedrückt
                {
                    MP4Object me = CreateVideoObject(BackgroundImage, new Point(BackgroundOffsetX, BackgroundOffsetY));
                    me.IsBackgroundPicture = true;
                    me.IsVisible = false;
                }
                HasMP4Background = true;
            }
        }

        private void UmwandelnAttacke(object obj)
        {
            ManöverInfo mi = Global.CurrentKampf.Kampf.InitiativListe.FirstOrDefault(t => t.Manöver.Ausführender.Kämpfer == SelectedObject as IKämpfer);
            if (mi == null)
            {
                return;
            }

            mi.Manöver.VerbleibendeDauer = 0;
            mi.UmwandelnAttacke.Execute(obj);
            Global.CurrentKampf.SelectedManöver = mi;
            Global.CurrentKampf.Kampf.SelectedManöverInfo = mi;
        }

        private void UmwandelnFernkampf(object obj)
        {
            ManöverInfo mi = Global.CurrentKampf.Kampf.InitiativListe.FirstOrDefault(t => t.Manöver.Ausführender.Kämpfer == SelectedObject as IKämpfer);
            if (mi == null)
            {
                return;
            }

            mi.UmwandelnFernkampf.Execute(null);
        }

        private void UmwandelnSonstiges(object obj)
        {
            ManöverInfo mi = Global.CurrentKampf.Kampf.InitiativListe.FirstOrDefault(t => t.Manöver.Ausführender.Kämpfer == SelectedObject as IKämpfer);
            if (mi == null)
            {
                return;
            }

            mi.Manöver.VerbleibendeDauer = 0;
            mi.UmwandelnSonstiges.Execute(null);
        }

        #endregion Commands

        #region Dispose

        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                // Free any other managed objects here.
                if (Global.CurrentKampf != null)
                {
                    Global.CurrentKampf.Kampf.Kämpfer.CollectionChanged -= OnKämpferListeChanged;
                }
            }

            // Free any unmanaged objects here.
            disposed = true;
        }

        private bool disposed = false;

        ~BattlegroundViewModel()
        {
            Dispose(false);
        }

        #endregion Dispose
    }
}
