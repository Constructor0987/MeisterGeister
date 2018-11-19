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
using WPFExtensions.Controls;
using Application = System.Windows.Application;

namespace MeisterGeister.ViewModel.Bodenplan
{
    public class BattlegroundViewModel : Base.ViewModelBase, IDisposable
    {
        public const int ARENA_GRID_RESOLUTION = 10000;
        private bool _creatingNewFilledLine;

        private bool _creatingNewLine;

        private double _currentMousePositionX, _currentMousePositionY;

        private bool _freizeichnen = false;

        private bool _initDnD;

        //Vielleicht als Enum, wenn mehr als zwei Modi gebraucht werden? JO
        private bool _isEditorModeEnabled = true;

        private bool _isMoving;

        private bool _leftShiftPressed = false;

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

        public bool IsEditorModeEnabled
        {
            get { return _isEditorModeEnabled; }

            set
            {
                Set(ref _isEditorModeEnabled, value);
            }
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
                LabelWidth = 200
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
                SelectedObject.MoveObject(xNew - xOld, yNew - yOld, false);
            }
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
                    var pathLine = (PathLine)SelectedTempObject;
                    var endPoint = new Point(x2, y2);
                    Point startPoint = pathLine.GetStartPoint;

                    pathLine.ChangeLastPoint(endPoint);

                    SetBewegungslaenge(startPoint, endPoint);
                }
            }
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

        private void _selectedListBoxBattlegroundObjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("CollectionChanged: {0} {1}", e.Action, e.NewItems));
        }

        private void SetBewegungslaenge(Point startPoint, Point endPoint)
        {
            var label = BattlegroundObjects.Last(x => x.GetType() == typeof(TextLabel)) as TextLabel;
            label.LabelPositionX = (startPoint.X + endPoint.X - label.LabelWidth) / 2;
            label.LabelPositionY = (startPoint.Y + endPoint.Y - label.LabelHeight) / 2;
            label.TextInLabel = $"{Math.Round(Math.Sqrt(Math.Pow((endPoint.X - startPoint.X), 2) + Math.Pow((endPoint.Y - startPoint.Y), 2)) / 100, 1).ToString()} Schritt";
        }

        #region Fog

        private Grid _arenaGrid = new Grid();
        private string _backgroundFilename;
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

        private Window _spielerScreenWindow = null;

        private bool _useFog = false;

        public Grid AreanGrid
        {
            get { return _arenaGrid; }
            set { Set(ref _arenaGrid, value); }
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

        #endregion Fog

        #region Objektlisten/-ViewSources

        private ObservableCollection<BattlegroundBaseObject> _battlegroundObjects;
        private CollectionViewSource kämpferliste = null;

        private CollectionViewSource objektliste = null;

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

        private bool _doChangeModPositionSelbst = false;
        private KampfViewModel _kampfVM;

        public bool DoChangeModPositionSelbst
        {
            get { return _doChangeModPositionSelbst; }
            set { Set(ref _doChangeModPositionSelbst, value); }
        }

        public KampfViewModel KampfVM
        {
            get { return _kampfVM; }

            set
            {
                if (object.Equals(_kampfVM, value))
                {
                    return;
                }

                if (KampfVM != null)
                {
                    _kampfVM.Kampf.Kämpfer.CollectionChanged -= OnKämpferListeChanged;
                    RemoveCreatureAll();
                }
                _kampfVM = value;
                if (KampfVM != null)
                {
                    _kampfVM.Kampf.Kämpfer.CollectionChanged += OnKämpferListeChanged;
                    AddAllCreatures();
                }
                UpdateCreaturesFromChangedKampferlist();
            }
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
                ((Held)kämpfer).LoadBattlegroundPortrait(((Held)kämpfer).Bild, true);
                BattlegroundObjects.Add(((Held)kämpfer));
            }
            else
            {
                ((Gegner)kämpfer).LoadBattlegroundPortrait(((Gegner)kämpfer).Bild, false);
                BattlegroundObjects.Add(((Gegner)kämpfer));
            }
        }

        public void ClearBattleground()
        {
            BattlegroundObjects.Where(x => !(x is BattlegroundCreature)).ToList().ForEach(x => BattlegroundObjects.Remove(x));
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

        private bool _ignorZLevel = true;

        private List<string> _possibleZLevels = new List<string>();

        private bool _showZ;

        private string _visibleZLevels = "10";

        public bool IgnorZLevel
        {
            get { return _ignorZLevel; }

            set
            {
                if (Set(ref _ignorZLevel, value))
                {
                    Ressources.SetVisibilityDependetOnZLevelSelection(ref _battlegroundObjects, VisibleZLevels, IgnorZLevel);
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

        #endregion Z-Level

        #region Überflüssig?

        //TODO Die getrennte Speicherung von SelectedCreature UND SelectedObject misfällt mir, da beides eigentlich Objekte sind. JO
        private string _currentlySelectedCreature = "";

        public string CurrentlySelectedCreature
        {
            get { return _currentlySelectedCreature; }
            set { Set(ref _currentlySelectedCreature, value); }
        }

        #endregion Überflüssig?

        #region Sichtbereich-Optionen

        private bool _showSightArea = true;
        private double _sightAreaLenght = 120;

        public bool ShowSightArea
        {
            get { return _showSightArea; }

            set
            {
                Set(ref _showSightArea, value);
            }
        }

        public double SightAreaLenght
        {
            get { return _sightAreaLenght; }

            set
            {
                if (Set(ref _sightAreaLenght, value))
                {
                    Ressources.SetNewSightAreaLength(ref _battlegroundObjects, SightAreaLenght);
                }
            }
        }

        #endregion Sichtbereich-Optionen

        #region Anzeigeoptionen

        private bool _showCreatureName = true;

        public bool ShowCreatureName
        {
            get { return _showCreatureName; }

            set
            {
                Set(ref _showCreatureName, value);
            }
        }

        #endregion Anzeigeoptionen

        #region SelectedObject und dessen Eigenschaften und Delete

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
                    return;
                }

                BattlegroundObjects.ToList().ForEach(x => x.IsHighlighted = false);
                _selectedObject = value;
                if (SelectedObject is BattlegroundCreature)
                {
                    Global.CurrentKampf.SelectedKämpfer = Global.CurrentKampf.Kampf.Kämpfer.FirstOrDefault(ki => ki.Kämpfer == ((IKämpfer)SelectedObject));
                }

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

        #endregion SelectedObject und dessen Eigenschaften und Delete

        #region Laden/Speichern

        private bool _loadWithoutPictures = false, _saveWithoutPictures = false;

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
                if (!(element is ImageObject && LoadWithoutPictures))
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

                wbmap.CopyPixels(FogPixelData, widthInByte, 0);
                System.Drawing.Bitmap bm = BitmapFromWriteableBitmap(wbmap);
                var img = (System.Drawing.Image)bm;
                FogImage = wbmap;
                useFog = true;
            }

            ObservableCollection<double> lstFogSettings = bg.LoadSettingsFromXML(filename);
            if (lstFogSettings.Count == 0)
            {
                return;
            }

            BackgroundOffsetSize = lstFogSettings[0];
            BackgroundOffsetX = lstFogSettings[1];
            InvBackgroundOffsetY = lstFogSettings[2];

            PlayerGridOffsetX = lstFogSettings[3];
            PlayerGridOffsetY = lstFogSettings[4];
            ScaleSpielerGrid = lstFogSettings[5];
            ScaleKampfGrid = lstFogSettings[6];
            RechteckGrid = lstFogSettings[7] == 1;
        }

        public void SaveBattlegroundToXML(string filename)
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
                        io.IsVisible = false;
                    }
                }
                lstSettings.Add(PlayerGridOffsetX);
                lstSettings.Add(PlayerGridOffsetY);
                lstSettings.Add(ScaleSpielerGrid);
            }
            else
            {
                lstSettings.AddRange(new List<double>() { 0, 0, 0 });
            }

            lstSettings.Add(ScaleKampfGrid);
            lstSettings.Add(RechteckGrid ? 1 : 0);
            var bg = new BattlegroundXMLLoadSave();
            bg.SaveMapToXML(BattlegroundObjects, filename, SaveWithoutPictures, lstSettings);
        }

        #endregion Laden/Speichern

        #region Grid

        private const double HEXAGON_PERIMETER = 3.4641016151377545870548926830117;
        private Color gridColor = Color.FromRgb(255, 255, 255);
        private bool hexGrid = false;
        private bool rechteckGrid = true;
        private PathGeometry rechteckPath = null, hextilePath = null;

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

        #endregion Grid

        #region ZeichenModi

        private ZeichenModus _zeichenModus;

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
                    //eventuell aktionen ausführen, wie Bild platzieren
                    //oder Properties auf dem model umsetzen wie CreateLine = true
                }
            }
        }

        #endregion ZeichenModi

        #region -- Screen Pointer --

        private bool _isPointerVisible = false;
        private double _pointerDurchmesser = 25.0;

        private System.Windows.Thickness _pointerMargin = new Thickness();

        private System.Windows.Visibility _pointerVisibility = Visibility.Collapsed;

        private double _xScale = 1;

        private double _yScale = 1;

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

        #endregion -- Screen Pointer --

        //TODO fixme or replace me

        #region Sticky Stuff, der gerade nicht richtig funktioniert

        private readonly List<BattlegroundBaseObject> stickyHeroesTempList = new List<BattlegroundBaseObject>();

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

        private Base.CommandBase _onBtnCenterMeisterView = null;
        private Base.CommandBase _onBtnCenterPlayerView = null;
        private Base.CommandBase _onBtnPosIniWindow = null;
        private Base.CommandBase _onBtnUmwandelnAttacke = null;

        private Base.CommandBase _onBtnUmwandelnFernkampf = null;

        private Base.CommandBase _onBtnUmwandelnSonstiges = null;

        private Base.CommandBase _onSetBackgroundClick = null;

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

        private void BtnPosIniWindow(object obj)
        {
            PosIniWindow++;
            if (PosIniWindow == 5)
            {
                PosIniWindow = 1;
            }

            SetIniWindowPosition();
        }

        private void CenterMeisterView(object obj)
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
            BackgroundImage = ViewHelper.ChooseFile("Hintergrundbild setzen", "", false, new string[9] { "bmp", "gif", "jpg", "jpeg", "jpe", "jfif", "png", "tif", "tiff" });
            if (string.IsNullOrEmpty(BackgroundImage))
            {
                return;
            }

            BackgroundFilename = new FileInfo(BackgroundImage).Name ?? "";
            BackgroundOffsetX = 0;
            ImageObject io = CreateImageObject(BackgroundImage, new Point(BackgroundOffsetX, BackgroundOffsetY));
            io.IsBackgroundPicture = true;
            io.IsVisible = false;
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

        private bool disposed = false;

        ~BattlegroundViewModel()
        {
            Dispose(false);
        }

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

        #endregion Dispose
    }
}
