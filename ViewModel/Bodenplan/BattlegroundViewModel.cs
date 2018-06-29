using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Bodenplan.Logic;
using MeisterGeister.ViewModel.Kampf.Logic;
using MeisterGeister.Model.Extensions;
using System.Windows.Data;
using MeisterGeister.View.General;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using MeisterGeister.View.Kampf;
using MeisterGeister.ViewModel.Kampf;
using WPFExtensions.Controls;
using Application = System.Windows.Application;
using MeisterGeister.View.Bodenplan;
using MeisterGeister.View.SpielerScreen;

namespace MeisterGeister.ViewModel.Bodenplan
{
    public class BattlegroundViewModel : Base.ViewModelBase, IDisposable
    {
        //  *Hintergrundfarbe
        //  *ZLevel bei initialisierung fix
        //  *Bilder Fixen!
        //  *Größe Kreaturen
        public BattlegroundViewModel() : base()
        {
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;

            //alte Fog-of-War Bilder löschen
            foreach (string s in Directory.GetFiles(Ressources.GetFullApplicationPathForPictures()).ToList()
                .FindAll(t => t.StartsWith(Ressources.GetFullApplicationPathForPictures() + "Fog-of-War")))
            {
                try
                { File.Delete(s); }
                catch
                { }
            }

        }

        void _selectedListBoxBattlegroundObjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(String.Format("CollectionChanged: {0} {1}", e.Action, e.NewItems));
        }

        #region Fog

        private ZoomControl _meisterArenaZoomControl = new ZoomControl();
        public ZoomControl MeisterArenaZoomControl
        {
            get { return _meisterArenaZoomControl; }
            set { Set(ref _meisterArenaZoomControl, value); }
        }

        private Point _kämpferDnDTempPos = new Point(0, 0);
        public Point KämpferDnDTempPos 
        {
            get { return _kämpferDnDTempPos; }
            set { Set(ref _kämpferDnDTempPos, value); }
        }

        private double _meisterZoom = .5;
        public double MeisterZoom
        {
            get { return _meisterZoom; }
            set { Set(ref _meisterZoom, value); }
        }

        private double _meisterZoomTransX = 0;
        public double MeisterZoomTransX
        {
            get { return _meisterZoomTransX; }
            set { Set(ref _meisterZoomTransX, value); }
        }

        private double _meisterZoomTransY = 0;
        public double MeisterZoomTransY
        {
            get { return _meisterZoomTransY; }
            set { Set(ref _meisterZoomTransY, value); }
        }

        private Grid _arenaGrid = new Grid();
        public Grid AreanGrid
        {
            get { return _arenaGrid; }
            set { Set(ref _arenaGrid, value); }
        }

        private int _fogFreeSize = 1;
        public int FogFreeSize
        {
            get { return _fogFreeSize; }
            set { Set(ref _fogFreeSize, value); }
        }

        private int[] _fogPixelData;
        public int[] FogPixelData
        {
            get { return _fogPixelData; }
            set { Set(ref _fogPixelData, value); }
        }

        private bool _isShowIniKampf = false;
        public bool IsShowIniKampf
        {
            get { return _isShowIniKampf; }
            set { Set(ref _isShowIniKampf, value); }
        }

        //private bool _isShowSpielerScreenWindow = false;
        //public bool IsShowSpielerScreenWindow
        //{
        //    get { return _isShowSpielerScreenWindow; }
        //    set { Set(ref _isShowSpielerScreenWindow, value); }
        //}

        private bool _spielerScreenActive = false;
        public bool SpielerScreenActive
        {
            get { return _spielerScreenActive; }
            set { Set(ref _spielerScreenActive, value); }
        }

        private bool _useFog = false;
        public bool useFog
        {
            get { return _useFog; }
            set
            {
                Set(ref _useFog, value);
                if (value && FogPixelData == null)
                    CreateFogOfWar();
            }
        }

        private bool _fogFreimachen = false;
        public bool FogFreimachen
        {
            get { return _fogFreimachen; }
            set { Set(ref _fogFreimachen, value); }
        }

        private string _backgroundImage;
        public string BackgroundImage
        {
            get { return _backgroundImage; }
            set
            {
                Set(ref _backgroundImage, value);
                BackgroundFilename = value ?? System.IO.Path.GetFileNameWithoutExtension(value);
            }
        }

        private WriteableBitmap _fogImage;
        public WriteableBitmap FogImage
        {
            get { return _fogImage; }
            set { Set(ref _fogImage, value); }
        }
        private string _fogImageFilename;
        public string FogImageFilename
        {
            get { return _fogImageFilename; }
            set { Set(ref _fogImageFilename, value); }
        }
        private string _backgroundFilename;
        public string BackgroundFilename
        {
            get { return _backgroundFilename; }
            set { Set(ref _backgroundFilename, value); }
        }
        
        private Window _spielerScreenWindow = null;
        public Window SpielerScreenWindow
        {
            get { return _spielerScreenWindow; }
            set { Set(ref _spielerScreenWindow, value); }
        }


        private KampfInfoView _kampfIniInfoView = null;
        public KampfInfoView KampfIniInfoView 
        {
            get { return _kampfIniInfoView; }
            set { Set(ref _kampfIniInfoView, value); }            
        }


        private bool _invertPlayerScrolling = false;
        public bool InvertPlayerScrolling
        {
            get { return _invertPlayerScrolling; }
            set { Set(ref _invertPlayerScrolling, value); }
        }

        private KampfViewModel _kampf = null;
        public KampfViewModel kampf
        {
            get { return _kampf; }
            set { Set(ref _kampf, value); }
        }

        public Kampf.Logic.Kampf CurrKampf
        {
            get { return Global.CurrentKampf.Kampf; }
        }

        public Kampf.Logic.ManöverInfo SelManöver
        {
            get { return Global.CurrentKampf.SelectedManöver; }
        }

        private Window _kampfWindow = null;
        public Window KampfWindow
        {
            get { return _kampfWindow; }
            set { Set(ref _kampfWindow, value); }
        }

        private double _scaleKampfGrid = 1;
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
                    //SizeToContent muss wieder auf Manual gesetzt werden da das Window sonst immer größer wird
                    KampfWindow.SizeToContent = SizeToContent.Manual;
                  //  SetIniWindowPosition(); ???
                    SetIniWindowPosition();
                }
            }
        }

        private string _infoText = null;
        public string InfoText
        {
            get { return _infoText; }
            set { Set(ref _infoText, value); }
        }


        private int _posIniWindow = 1;
        public int PosIniWindow
        {
            get { return _posIniWindow; }
            set { Set(ref _posIniWindow, value); }
        }

        private double _iniHeightStart = 0;
        public double IniHeightStart
        {
            get { return _iniHeightStart; }
            set { Set(ref _iniHeightStart, value); }
        }

        private double _iniWidthStart = 0;
        public double IniWidthStart
        {
            get { return _iniWidthStart; }
            set { Set(ref _iniWidthStart, value); }
        }

        private double _scaleSpielerGrid = 1;
        public double ScaleSpielerGrid
        {
            get { return _scaleSpielerGrid; }
            set { Set(ref _scaleSpielerGrid, Math.Round(value<.2?.2:value, 2)); }
        }

        private double _fogOffsetX = 0;
        public double FogOffsetX
        {
            get { return _fogOffsetX; }
            set
            {
                Set(ref _fogOffsetX, value);
                OffsetBackgroudMargin = new Thickness(_fogOffsetX, _fogOffsetY, -_fogOffsetX - 20000, -_fogOffsetY - 20000);
            }
        }

        private double _fogOffsetY = 0;
        public double FogOffsetY
        {
            get { return _fogOffsetY; }
            set
            {
                Set(ref _fogOffsetY, value);
                OffsetBackgroudMargin = new Thickness(_fogOffsetX, _fogOffsetY, -_fogOffsetX - 20000, -_fogOffsetY - 20000);
            }
        }
        
        private double _playerGridOffsetX = 0;
        public double PlayerGridOffsetX
        {
            get { return _playerGridOffsetX; }
            set
            {
                Set(ref _playerGridOffsetX, value);
                PlayerOffsetGridMargin = new Thickness(-_playerGridOffsetX , _playerGridOffsetY, 0, 0);
            }
        }
        private double _playerGridOffsetY = 0;
        public double PlayerGridOffsetY
        {
            get { return _playerGridOffsetY; }
            set
            {
                Set(ref _playerGridOffsetY, value);
                PlayerOffsetGridMargin = new Thickness(-_playerGridOffsetX, _playerGridOffsetY, 0, 0);
            }
        }

        private double _backgroundOffsetX = 0;
        public double BackgroundOffsetX
        {
            get { return _backgroundOffsetX; }
            set
            {
                Set(ref _backgroundOffsetX, value);
                OffsetBackgroudMargin = new Thickness(_backgroundOffsetX , _backgroundOffsetY , -_backgroundOffsetX - 20000, -_backgroundOffsetY - 20000);
            }
        }

        private double _backgroundOffsetY = 0;
        public double BackgroundOffsetY
        {
            get { return _backgroundOffsetY; }
            set
            {
                Set(ref _backgroundOffsetY, value);
                OffsetBackgroudMargin = new Thickness(_backgroundOffsetX , _backgroundOffsetY , -_backgroundOffsetX - 20000, -_backgroundOffsetY - 20000);
            }
        }
        private double _backgroundOffsetSize = 10000;
        public double BackgroundOffsetSize
        {
            get { return _backgroundOffsetSize; }
            set
            {
                Set(ref _backgroundOffsetSize, value);
                OffsetBackgroudMargin = new Thickness(_backgroundOffsetX , _backgroundOffsetY , -_backgroundOffsetX - 20000, -_backgroundOffsetY - 20000);
                //FogOffsetSize = value;
            }
        }

        private double _invBackgroundOffsetY = 0;
        public double InvBackgroundOffsetY
        {
            get { return _invBackgroundOffsetY; }
            set
            {
                Set(ref _invBackgroundOffsetY, value);
                BackgroundOffsetY = value * -1;
            }
        }
        private double _fogOffsetSize = 10000;
        public double FogOffsetSize
        {
            get { return _fogOffsetSize; }
            set
            {
                Set(ref _fogOffsetSize, value);
                OffsetFogMargin = new Thickness(_fogOffsetX, _fogOffsetY, -_fogOffsetX, -_fogOffsetY - 20000);
            }
        }

        private Thickness _offsetFogMargin = new Thickness(0, 0, 0, 0);
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
        
        private Thickness _playerOffsetGridMargin = new Thickness(0, 0, 0, 0);
        public Thickness PlayerOffsetGridMargin
        {
            get { return _playerOffsetGridMargin; }
            set { Set(ref _playerOffsetGridMargin, value); }
        }

        private Thickness _offsetBackgroudMargin = new Thickness(0, 0, 0, 0);
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

        #endregion

        #region Objektlisten/-ViewSources

        ObservableCollection<BattlegroundBaseObject> _battlegroundObjects;
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

        private void filterKämpfer(object sender, FilterEventArgs f)
        {
            f.Accepted = f.Item is IKämpfer;
        }

        private void filterObjekt(object sender, FilterEventArgs f)
        {
            f.Accepted = !(f.Item is IKämpfer);
        }

        private CollectionViewSource kämpferliste = null;
        [DependentProperty("BattlegroundObjects")]
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

        private CollectionViewSource objektliste = null;
        [DependentProperty("BattlegroundObjects")]
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

        #endregion

        #region Kampf mit Eventverbindung auf KämpferListe


        private KampfViewModel _kampfVM;
        public KampfViewModel KampfVM
        {
            get { return _kampfVM; }
            set
            {
                if (object.Equals(_kampfVM, value)) return;
               
                if (KampfVM != null)
                {
                    _kampfVM.Kampf.Kämpfer.CollectionChanged -= OnKämpferListeChanged;
                    //_kampfVM.PropertyChanged -= OnKampfPropertyChanged;
                    RemoveCreatureAll();
                }
                _kampfVM = value;
                if (KampfVM != null)
                {
                    _kampfVM.Kampf.Kämpfer.CollectionChanged += OnKämpferListeChanged;
                    //_kampfVM.PropertyChanged += OnKampfPropertyChanged;
                    AddAllCreatures();
                }
                UpdateCreaturesFromChangedKampferlist();
            }
        }

        public void UpdateCreaturesFromChangedKampferlist()
        {
            foreach (var k in Global.CurrentKampf.Kampf.Kämpfer)
            {
                ((Wesen)((KämpferInfo)k).Kämpfer).PropertyChanged -= OnWesenPropertyChanged;
            }
            foreach (var k in Global.CurrentKampf.Kampf.Kämpfer)
            { 
                ((Wesen)((KämpferInfo)k).Kämpfer).PropertyChanged += OnWesenPropertyChanged;
            }
        }

        //Event gets fired when Wesen.PropertyChanged event fires...
        private void OnWesenPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Position")
            {
                DoChangeModPositionSelbst = true;
                KämpferInfo ki = Global.CurrentKampf.Kampf.Kämpfer.FirstOrDefault(t => t.Kämpfer == (sender as IKämpfer));
            }
        }

        private void OnKämpferListeChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    //e.NewItems
                    var l1 = e.NewItems;
                    foreach (var item in l1)
                    {
                        AddCreature(((KämpferInfo)item).Kämpfer);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    //e.OldItems
                    var l2 = e.OldItems;
                    foreach (var item in l2)
                    {
                        RemoveCreature(((KämpferInfo)item).Kämpfer);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    var l3 = e.OldItems;
                    foreach (var item in l3)
                    {
                        RemoveCreature(((KämpferInfo)item).Kämpfer);
                    }

                    var l4 = e.NewItems;
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

        private bool _doChangeModPositionSelbst = false;
        public bool DoChangeModPositionSelbst
        {
            get { return _doChangeModPositionSelbst; }
            set { Set(ref _doChangeModPositionSelbst, value); }
        }
        #endregion

        #region Creature Add/Remove, Clear All

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

        public void AddAllCreatures()
        {

            foreach (var k in Global.CurrentKampf.Kampf.Kämpfer)
            {
                AddCreature(k.Kämpfer);
            }
            CenterMeisterView(null);
        }

        public void RemoveCreature(IKämpfer creature)
        {
            BattlegroundObjects.Remove((Wesen)creature);
            //creature.PropertyChanged -= OnWesenPropertyChanged;
        }

        public void RemoveCreatureAll()
        {
            for (int i = BattlegroundObjects.Count - 1; i >= 0; i--)
                if (BattlegroundObjects[i] is BattlegroundCreature)
                {
                    BattlegroundObjects.RemoveAt(i);
                    //(BattlegroundObjects[i] as BattlegroundCreature).PropertyChanged += OnWesenPropertyChanged;
                }

        }

        public void ClearBattleground()
        {
            BattlegroundObjects.Where(x => !(x is BattlegroundCreature)).ToList().ForEach(x => BattlegroundObjects.Remove(x));
        }
        #endregion

        #region Z-Level
        //get / set ZLevel
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

        private string _visibleZLevels = "10";
        public string VisibleZLevels
        {
            get { return _visibleZLevels; }
            set
            {
               if(Set(ref _visibleZLevels, value))
                   Ressources.SetVisibilityDependetOnZLevelSelection(ref _battlegroundObjects, VisibleZLevels, IgnorZLevel);
            }

        }

        private List<string> _possibleZLevels = new List<string>();
        public List<string> PossibleZLevels
        {
            get { return _possibleZLevels; }
            set
            {
                Set(ref _possibleZLevels, value);
            }
        }

        private bool _showZ;
        public bool ShowZ
        {
            get { return _showZ; }
            set
            {
                PossibleZLevels = Ressources.GetPossibleZLevels(BattlegroundObjects);
                Set(ref _showZ, value);
            }
        }

        private bool _ignorZLevel = true;
        public bool IgnorZLevel
        {
            get { return _ignorZLevel; }
            set
            {
                if (Set(ref _ignorZLevel, value))
                    Ressources.SetVisibilityDependetOnZLevelSelection(ref _battlegroundObjects, VisibleZLevels, IgnorZLevel);
            }
        }

        #endregion
        
        private bool _leftShiftPressed = false;
        public bool LeftShiftPressed
        {
            get { return _leftShiftPressed; }
            set { Set(ref _leftShiftPressed, value); }
        }

        #region Überflüssig?

        //TODO Die getrennte Speicherung von SelectedCreature UND SelectedObject misfällt mir, da beides eigentlich Objekte sind. JO
        private String _currentlySelectedCreature = "";
        public String CurrentlySelectedCreature
        {
            get { return _currentlySelectedCreature; }
            set { Set(ref _currentlySelectedCreature, value); }
        }

        //TODO Vermutlich überflüssig. JO
        private string _selectedObjectsFromListBox = "";
        public string SelectedObjectsFromListBox
        {
            get { return _selectedObjectsFromListBox; }
            set
            {
                Set(ref _selectedObjectsFromListBox, value);
                System.Diagnostics.Debug.WriteLine(_selectedObjectsFromListBox);
            }
        }


        //TODO Vermutlich überflüssig. JO
        private List<string> _paintedObjects = new List<string>();
        public List<string> PaintedObjects
        {
            get { return _paintedObjects; }
            set
            {
                foreach (var o in BattlegroundObjects)
                {

                }
            }
        }

        #endregion

        #region Sichtbereich-Optionen

        private bool _showSightArea = true;
        public bool ShowSightArea
        {
            get { return _showSightArea; }
            set
            {
                Set(ref _showSightArea, value);
            }
        }

        private double _sightAreaLenght = 120;
        public double SightAreaLenght
        {
            get { return _sightAreaLenght; }
            set
            {
                if(Set(ref _sightAreaLenght, value))
                    Ressources.SetNewSightAreaLength(ref _battlegroundObjects, SightAreaLenght);
            }
        }

        #endregion

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

        #endregion

        //Vielleicht als Enum, wenn mehr als zwei Modi gebraucht werden? JO
        private bool _isEditorModeEnabled = true;
        public bool IsEditorModeEnabled
        {
            get { return _isEditorModeEnabled; }
            set
            {
                Set(ref _isEditorModeEnabled, value);
                //Ressources.SetEditorMode(ref _battlegroundObjects, _isEditorModeEnabled);
            }
        }

        private bool _freizeichnen = false;
        public bool Freizeichnen
        {
            get { return _freizeichnen; }
            set
            {
                Set(ref _freizeichnen, value);
            }
        }

        #region SelectedObject und dessen Eigenschaften und Delete

        private BattlegroundBaseObject _selectedTempObject;
        public BattlegroundBaseObject SelectedTempObject
        {
            get { return _selectedTempObject; }
            set { Set(ref _selectedTempObject, value); }
        }
        

        //get / set selected Object
        private BattlegroundBaseObject _selectedObject;
        public BattlegroundBaseObject SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                if (_selectedObject != null && _selectedObject.IsMoving) return;
                
                    BattlegroundObjects.ToList().ForEach(x => x.IsHighlighted = false);
                    _selectedObject = value;
                    if (SelectedObject is BattlegroundCreature) Global.CurrentKampf.SelectedKämpfer = Global.CurrentKampf.Kampf.Kämpfer.FirstOrDefault(ki => ki.Kämpfer == ((IKämpfer)SelectedObject));
                    if (SelectedObject != null)
                    {
                        SelectedFillColor = SelectedObject.FillColor;
                        SelectedColor = SelectedObject.ObjectXMLColor;
                        StrokeThickness = SelectedObject.StrokeThickness;

                        SelectedObject.IsSelected = true;
                        if (SelectedObject is BattlegroundCreature)
                            Global.CurrentKampf.SelectedManöverInfo = Global.CurrentKampf.Kampf.SortedInitiativListe.FirstOrDefault(ki => ki.Manöver.Ausführender.Kämpfer == ((IKämpfer)SelectedObject));
                    }
                    OnChanged("SelectedObject");
                    try
                    {
                        if (SelectedObject is BattlegroundCreature)
                        {
                            if (BattlegroundObjects.IndexOf(SelectedObject) != BattlegroundObjects.Count - 1)
                            {
                                int indexObj = BattlegroundObjects.IndexOf(SelectedObject);
                                int lastPos = BattlegroundObjects.Count - 1;
                                if (lastPos -1 != indexObj)
                                    BattlegroundObjects.Move(indexObj, lastPos);
                                else
                                { }
                            }
                        }
                    }
                    catch (Exception ex)
                        { ViewHelper.ShowError("BattlegroundObjects.Move Befehel generierte einen Fehler", ex); }
                
            }
        }

        private KämpferInfo _selectectedKämpferInfo;
        public KämpferInfo SelectectedKämpferInfo
        {
            get { return _selectectedKämpferInfo; }
            set { Set(ref _selectectedKämpferInfo, value); }
        }

        //get / set delete Command 
        private BattlegroundCommand _deleteCommand;
        public BattlegroundCommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new BattlegroundCommand(Delete)); }
        }
        public void Delete()
        {
            if (SelectedObject != null)
                if (SelectedObject is BattlegroundCreature) Global.CurrentKampf.DeleteKämpfer();
                else BattlegroundObjects.Remove(SelectedObject);
        }


        private BattlegroundCommand _playerScreenLeftCommand;
        public BattlegroundCommand PlayerScreenLeftCommand
        {
            get { return _playerScreenLeftCommand ?? (_playerScreenLeftCommand = new BattlegroundCommand(Left)); }
        }
        public void Left()
        { PlayerGridOffsetX = PlayerGridOffsetX - 80 > 0 ? PlayerGridOffsetX - 80 : 0; }

        private BattlegroundCommand _playerScreenRightCommand;
        public BattlegroundCommand PlayerScreenRightCommand
        {
            get { return _playerScreenRightCommand ?? (_playerScreenRightCommand = new BattlegroundCommand(Right)); }
        }
        public void Right()
        { PlayerGridOffsetX = PlayerGridOffsetX + 80 <= 10000 ? PlayerGridOffsetX + 80 : 10000; }

        private BattlegroundCommand _playerScreenUpCommand;
        public BattlegroundCommand PlayerScreenUpCommand
        {
            get { return _playerScreenUpCommand ?? (_playerScreenUpCommand = new BattlegroundCommand(Up)); }
        }
        public void Up()
        { PlayerGridOffsetY = PlayerGridOffsetY + 80 <= 0 ? PlayerGridOffsetY + 80 : 0; }

        private BattlegroundCommand _playerScreenDownCommand;
        public BattlegroundCommand PlayerScreenDownCommand
        {
            get { return _playerScreenDownCommand ?? (_playerScreenDownCommand = new BattlegroundCommand(Down)); }
        }
        public void Down()
        { PlayerGridOffsetY = PlayerGridOffsetY - 80 > -10000 ? PlayerGridOffsetY - 80 : -10000; }


        //get / set stroke thickness
        private double _strokeThickness = 20;
        public double StrokeThickness
        {
            get { return _strokeThickness; }
            set
            {
                if(Set(ref _strokeThickness, value))
                    if (SelectedObject != null)
                        SelectedObject.StrokeThickness = value;
            }
        }

        private double _objectSize = 1;
        public double ObjectSize
        {
            get
            {
                if (SelectedObject != null)
                {
                    if (SelectedObject is ImageObject) return ((ImageObject)SelectedObject).ObjectSize;
                    if (SelectedObject is BattlegroundCreature) return ((BattlegroundCreature)SelectedObject).ObjectSize;
                }
                return _objectSize;
            }
            set
            {
                if (SelectedObject != null)
                {
                    if (SelectedObject is ImageObject) ((ImageObject)SelectedObject).ObjectSize = value;
                    if (SelectedObject is BattlegroundCreature) ((BattlegroundCreature)SelectedObject).ObjectSize = value;
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
                OnChanged("Opacity");
            }
        }

        private Color _selectedColor = Colors.DarkGray, _selectedFillColor = Colors.LightGray;
        public Color SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                if(Set(ref _selectedColor, value))
                    if (SelectedObject != null)
                        SelectedObject.ObjectColor = new SolidColorBrush(value);
            }
        }

        public Color SelectedFillColor
        {
            get { return _selectedFillColor; }
            set
            {
                if(Set(ref _selectedFillColor, value))
                    if (SelectedObject != null)
                        SelectedObject.FillColor = value;
            }
        }

        [DependentProperty("SelectedFillColor")]
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

        #endregion

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

        public void SaveBattlegroundToXML(String filename)
        {
            List<double> lstSettings = new List<double>();
            lstSettings.Add(BackgroundOffsetSize);
            lstSettings.Add(BackgroundOffsetX);
            lstSettings.Add(InvBackgroundOffsetY);

            if (useFog)
            {
                for (int i = BattlegroundObjects.Count - 1; i >= 0; i--)
                {
                    if (BattlegroundObjects[i] is ImageObject && ((ImageObject)BattlegroundObjects[i]).IsFogPicture)
                    {
                        BattlegroundObjects.Remove(BattlegroundObjects[i]);

                        string olfile = FogImageFilename;
                        Nullable<int> x = null;
                        while (File.Exists(Ressources.GetFullApplicationPathForPictures() + "Fog-of-War_" + DateTime.Today.ToShortDateString() + x + ".png")) if (x == null) x = 0; else x++;
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
                lstSettings.AddRange(new List<double>() { 0, 0, 0 });
            lstSettings.Add(ScaleKampfGrid);
            lstSettings.Add(RechteckGrid? 1 : 0);
            BattlegroundXMLLoadSave bg = new BattlegroundXMLLoadSave();
            bg.SaveMapToXML(BattlegroundObjects, filename, SaveWithoutPictures, lstSettings);
        }

        public void LoadBattlegroundFromXML(String filename)
        {
            ClearBattleground();

            BattlegroundXMLLoadSave bg = new BattlegroundXMLLoadSave();
            var ocloaded = bg.LoadMapFromXML(filename, LoadWithoutPictures);

            foreach (var element in ocloaded)
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
                BitmapImage originalImage = new BitmapImage();
                originalImage.BeginInit();
                originalImage.UriSource = new Uri(FogImageFilename, UriKind.Absolute);
                originalImage.EndInit();
                BitmapSource bitmapSource = new FormatConvertedBitmap(originalImage, PixelFormats.Bgra32, null, 0);
                WriteableBitmap wbmap = new WriteableBitmap(bitmapSource);

                int h = wbmap.PixelHeight;
                int w = wbmap.PixelWidth;
                FogPixelData = new int[w * h];
                int widthInByte = 4 * w;

                wbmap.CopyPixels(FogPixelData, widthInByte, 0);
                System.Drawing.Bitmap bm = BitmapFromWriteableBitmap(wbmap);
                System.Drawing.Image img = (System.Drawing.Image)bm;
                FogImage = wbmap;
                useFog = true;
            }

            ObservableCollection<double> lstFogSettings = bg.LoadSettingsFromXML(filename);
            if (lstFogSettings.Count == 0) return;
            BackgroundOffsetSize = lstFogSettings[0];
            BackgroundOffsetX = lstFogSettings[1];
            InvBackgroundOffsetY = lstFogSettings[2];

            PlayerGridOffsetX = lstFogSettings[3];
            PlayerGridOffsetY = lstFogSettings[4];
            ScaleSpielerGrid = lstFogSettings[5];
            ScaleKampfGrid = lstFogSettings[6];
            RechteckGrid = lstFogSettings[7] == 1;            
        }

        public Position GetPositionFromImage(string posString)
        {
            Position p = Kampf.Logic.Position.Stehend;

            if (posString.Contains("Floating") || posString == "Schwebend") p = Position.Schwebend;
            else
                if (posString.Contains("Flying") || posString == "Fliegend") p = Position.Fliegend;
                else
                    if (posString.Contains("Kneeling") || posString == "Kniend") p = Position.Kniend;
                    else
                        if (posString.Contains("OnTheGround") || posString == "Liegend") p = Position.Liegend;
                        else
                            if (posString.Contains("Riding") || posString == "Reitend") p = Position.Reitend;
                            else
                                p = Position.Stehend;
            return p;
        }

        #endregion

        #region Grid

        PathGeometry rechteckPath = null, hextilePath = null;
        [DependentProperty("RechteckGrid"), DependentProperty("HexGrid")]
        public PathGeometry TilePathData
        {
            get {
                if (RechteckGrid)
                {
                    if(rechteckPath == null)
                        rechteckPath = BattlegroundUtilities.RechteckCellTile(100);
                    return rechteckPath;
                }
                else
                    if (HexGrid)
                    {
                        if (hextilePath == null)
                            hextilePath = BattlegroundUtilities.HexCellTile(100);
                        return hextilePath;
                    }
                    else
                    {
                        return null;
                    }
            }
        }

        [DependentProperty("RechteckGrid"), DependentProperty("HexGrid")]
        public Rect TileViewPort
        {
            get {
                double scale = 57.735;
                if (RechteckGrid)
                    return new Rect(0, 0, 3.4641016151377545870548926830117 * scale, 3.4641016151377545870548926830117 * scale);
                else
                    return new Rect(0, 0, 3 * scale, 3.4641016151377545870548926830117 * scale);
            }
        }

        private Color gridColor = Color.FromRgb(255, 255, 255);
        /// <summary>
        /// Farbe des HexGrids bzw des RechteckGrids
        /// </summary>
        public Color GridColor
        {
            get { return gridColor;}
            set { Set(ref gridColor, value); if (RechteckGrid) RechteckGrid = true; else HexGrid = true; }
        }

        private bool rechteckGrid = true;
        public bool RechteckGrid
        {
            get { return rechteckGrid; }
            set 
            {
                if (value)
                    HexGrid = false;
                Set(ref rechteckGrid, value); 
            }
        }
        private bool hexGrid = false;
        public bool HexGrid
        {
            get { return hexGrid; }
            set
            {
                if (value)
                    RechteckGrid = false;
                Set(ref hexGrid, value); 
            }
        }

        #endregion

        #region ZeichenModi

        private ZeichenModus zeichenModus;
        public ZeichenModus ZeichenModus
        {
            get { return zeichenModus; }
            set {
                if(Set(ref zeichenModus, value))
                {
                    //eventuell aktionen ausführen, wie Bild platzieren
                    //oder Properties auf dem model umsetzen wie CreateLine = true
                }
            }
        }

        [DependentProperty("ZeichenModus")]
        public bool CreateLine
        {
            get { return ZeichenModus == Logic.ZeichenModus.Linie; }
        }

        [DependentProperty("ZeichenModus")]
        public bool CreateFilledLine
        {
            get { return ZeichenModus == Logic.ZeichenModus.Fläche; }
        }

        #endregion

        private double _currentMousePositionX, _currentMousePositionY;
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

        private bool _creatingNewLine;
        public bool CreatingNewLine
        {
            get { return _creatingNewLine; }
            set
            {
                Set(ref _creatingNewLine, value);
                if (!_creatingNewLine) SelectedObject = null;
            }
        }
        private string _bewegungslaenge = null;
        public string Bewegungslaenge // Nullable<double>
        {
            get { return _bewegungslaenge; }
            set { Set(ref _bewegungslaenge, value); }
        }

        public PathLine CreateNewTempPathLine(double x1, double y1)
        {
            double th = (SelectedObject as BattlegroundCreature).CreatureHeight <= (SelectedObject as BattlegroundCreature).CreatureWidth ?
                (SelectedObject as BattlegroundCreature).CreatureHeight : (SelectedObject as BattlegroundCreature).CreatureWidth;

            var pathline = new PathLine(new Point(x1, y1))
            {
                ObjectColor = new SolidColorBrush(Colors.DarkBlue),
                StrokeThickness = th, 
                Opacity = .2
            };
            SelectedTempObject = pathline;
            BattlegroundObjects.Add(pathline);
            return pathline;
        }

        public PathLine CreateNewPathLine(double x1, double y1)
        {
            var pathline = new PathLine(new Point(x1, y1))
            {
                ObjectColor = new SolidColorBrush(SelectedColor),
                StrokeThickness = StrokeThickness,
                Opacity = Opacity
            };
            SelectedObject = pathline;
            BattlegroundObjects.Add(pathline);
            return pathline;
        }

        private bool _creatingNewFilledLine;
        public bool CreatingNewFilledLine
        {
            get { return _creatingNewFilledLine; }
            set
            {
                Set(ref _creatingNewFilledLine, value);
                if (!_creatingNewFilledLine) SelectedObject = null;
            }
        }

        public FilledPathLine CreateNewFilledLine(double x1, double y1)
        {
            var filledpathline = new FilledPathLine(new Point(x1, y1))
            {
                ObjectColor = new SolidColorBrush(SelectedColor),
                FillColor = SelectedFillColor,
                StrokeThickness = StrokeThickness,
                Opacity = Opacity
            };
            SelectedObject = filledpathline;
            BattlegroundObjects.Add(filledpathline);
            return filledpathline;
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
        public void FinishCurrentPathLine()
        {
            if (SelectedObject != null)
            {
                SelectedObject.IsNew = false;
                SelectedObject = null;
                RemoveNewObjects();
            }
        }

        public ImageObject CreateImageObject(string picurl, Point p)
        {
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(picurl, UriKind.Relative));

            var imageobject =
                new ImageObject(picurl, ((-1) * MeisterZoomTransX+200) / MeisterZoom, ((-1) * MeisterZoomTransY+200) / MeisterZoom );
            if (brush.ImageSource.Width >= brush.ImageSource.Height)
            {
                imageobject.ImageWidth = 100;
                imageobject.ImageHeight = 100* (brush.ImageSource.Height / brush.ImageSource.Width);
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

        //Move Objects
        private bool _isMoving;
        public bool IsMoving
        {
            get { return _isMoving; }
            set { Set(ref _isMoving, value); }
        }

        private bool _initDnD;
        public bool InitDnD
        {
            get { return _initDnD; }
            set { Set(ref _initDnD, value); }
        }

        public void MoveObject(double xOld, double yOld, double xNew, double yNew)
        {
            if (SelectedObject != null)
            {
                SelectedObject.MoveObject(xNew - xOld, yNew - yOld, false);
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
                    ((PathLine)SelectedTempObject).ChangeLastPoint(new Point(x2, y2));


                    //Berechnung der Länge der PathLine                    
                    Point sP = (SelectedTempObject as PathLine).GetStartPoint;
                    Bewegungslaenge = Math.Round(Math.Sqrt(Math.Pow((x2 - sP.X),2) + Math.Pow((y2 - sP.Y),2)) / 100,1).ToString() + " Schritt";
                }
            }
        }

        public void RemoveNewObjects()
        {
            BattlegroundObjects.Where(x => x.IsNew).ToList().ForEach(x => BattlegroundObjects.Remove(x));
        }


        public void SetIniWindowPosition()
        {
            //Get DPI Scaling from MainProgramm
            Matrix m = PresentationSource.FromVisual(Application.Current.MainWindow).CompositionTarget.TransformToDevice;
            double dx = m.M11;
            double dy = m.M22;

            System.Windows.HorizontalAlignment h = System.Windows.HorizontalAlignment.Right;
            VerticalAlignment v = VerticalAlignment.Top;

            switch (PosIniWindow)
            {
                case 1: {  
                    h = System.Windows.HorizontalAlignment.Right; 
                    v = VerticalAlignment.Top;
                    break; 
                }
                case 2:{
                    h = System.Windows.HorizontalAlignment.Right; 
                    v = VerticalAlignment.Bottom;
                    break; 
                }
                case 3:{
                    h = System.Windows.HorizontalAlignment.Left; 
                    v = VerticalAlignment.Bottom;
                    break; 
                }
                case 4:{
                    h = System.Windows.HorizontalAlignment.Left; 
                    v = VerticalAlignment.Top;
                    break; 
                }
            }            
           double maxRight = Math.Max(
                Screen.AllScreens[0].WorkingArea.Width/dx, 
                Screen.AllScreens.Length > 1 ?
                    Screen.AllScreens[1].WorkingArea.Right / dx : 0);

            double minRight = Screen.AllScreens.Length == 1 ? 0 : Screen.AllScreens[1].WorkingArea.Left/dx;
            
            if (KampfWindow == null) return;
            
        //    KampfWindow.SizeToContent = SizeToContent.Width;
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
               // Global.CurrentKampf.BodenplanViewModel.KampfWindow.Height = Global.CurrentKampf.BodenplanViewModel.KampfWindow.Height + 1;
               // Global.CurrentKampf.BodenplanViewModel.KampfWindow.Height = Global.CurrentKampf.BodenplanViewModel.KampfWindow.Height - 1;
                if ((System.Windows.Forms.Screen.AllScreens.Length > 1 &&
                     KampfWindow.Left > System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width +
                        System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Width * .5) ||
                    (System.Windows.Forms.Screen.AllScreens.Length == 1 &&
                     KampfWindow.Left > System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width * .5))
                {
                    KampfWindow.Left = (System.Windows.Forms.Screen.AllScreens.Length > 1) ?
                        System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width +
                        System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Width - KampfWindow.Width
                         : // +(e.PreviousSize.Width - e.NewSize.Width)

                        System.Windows.Forms.Screen.AllScreens[0].WorkingArea.Width - KampfWindow.Width;
                    if (System.Windows.Forms.Screen.AllScreens.Length > 1 &&
                        Global.CurrentKampf.BodenplanViewModel.SpielerScreenActive)
                    {
                        Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.Width =
                            System.Windows.Forms.Screen.AllScreens[1].WorkingArea.Width - KampfWindow.Width;
                    }
                }
            }
            
            KampfWindow.MinWidth = 430 * ScaleKampfGrid;

            int anzInisInKR = Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Where(kr => kr.Kampfrunde == Global.CurrentKampf.Kampf.Kampfrunde).Count();
            double width1Ini = (((KampfInfoView)KampfWindow.Content).scrViewer.ExtentWidth /
                Global.CurrentKampf.Kampf.InitiativListe.Aktionszeiten.Where(kr => kr.Kampfrunde <= Global.CurrentKampf.Kampf.Kampfrunde).Count()) * ScaleKampfGrid;
            KampfWindow.Width = width1Ini * anzInisInKR + 248 * ScaleKampfGrid; //246
            ((KampfInfoView)KampfWindow.Content).scrViewer.ScrollToRightEnd();
        }

        public void ChangeEbeneHeight(bool raise)
        {
            if (SelectedObject == null) return;

            for (int i = 0; i < BattlegroundObjects.Count; i++)
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

        public void MoveLastObjectBehindCreatures()
        {
            BattlegroundBaseObject bbo = BattlegroundObjects[BattlegroundObjects.Count - 1];

            int x = BattlegroundObjects.IndexOf(BattlegroundObjects.FirstOrDefault(t => t is BattlegroundCreature));
            if (x >= 0) BattlegroundObjects.Move(BattlegroundObjects.Count - 1, x);
        }

        //keeps heroes and monsters always on top
        public void UpdateCreatureLevelToTop()
        {
            for (int i = BattlegroundObjects.Count - 1; i >= 0; i--)
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

        //brings selected Object to Top and calls UpdateCreatureLevelToTop
        public void MoveSelectedObjectToTop(bool toTop)
        {
            if (SelectedObject == null) return;
            for (int i = BattlegroundObjects.Count - 1; i >= 0; i--)
            {
                if (BattlegroundObjects[i].Equals(SelectedObject))
                {
                    BattlegroundBaseObject b = BattlegroundObjects[i];
                    BattlegroundObjects.Remove(BattlegroundObjects[i]);
                    int position = toTop ? BattlegroundObjects.Count - 1 : 0;
                    BattlegroundObjects.Insert(position, b);
                    UpdateCreatureLevelToTop();
                }
            }
        }
                
        public void SelectionChangedUpdateSliders()
        {
            OnChanged("StrokeThickness");
            OnChanged("ObjectSize");
            OnChanged("Opacity");
            OnChanged("ZLevel");
        }

        #region -- Screen Pointer --

        private bool _isPointerVisible = false;
        public bool IsPointerVisible
        {
            get { return _isPointerVisible; }
            set
            {
                SelectedObject = null;
                _isPointerVisible = value;
                PointerVisibility = value? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;                
                OnChanged("IsPointerVisible");
            }
        }

        private System.Windows.Visibility _pointerVisibility = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility PointerVisibility
        {
            get { return _pointerVisibility; }
            set { Set(ref _pointerVisibility, value); }
        }

        private double _pointerDurchmesser = 25.0;
        public double PointerDurchmesser
        {
            get { return _pointerDurchmesser; }
            set { Set(ref _pointerDurchmesser, value); }
        }
        
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
            get { return _pointerMargin; }
            set { Set(ref _pointerMargin, value); }
        }

        #endregion

        //TODO fixme or replace me
        #region Sticky Stuff, der gerade nicht richtig funktioniert

        private List<BattlegroundBaseObject> stickyHeroesTempList = new List<BattlegroundBaseObject>();
        public List<BattlegroundBaseObject> StickyListBoxBattlegroundObjects
        {
            get { return BattlegroundObjects.Where(x => x is Held && x.IsSticked).ToList(); }
            set { value = BattlegroundObjects.Where(x => x is Held && x.IsSticked).ToList(); OnChanged("StickyListBoxBattlegroundObjects"); }
        }

        public void StickHeroes()
        {
            if (BattlegroundObjects.Where(x => x is MeisterGeister.Model.Gegner && x.IsSticked).Any()) return;
            if (!BattlegroundObjects.Where(x => x is MeisterGeister.Model.Held).Any()) return;
            foreach (var h in BattlegroundObjects)
            {
                if (h is Held)
                {
                    h.IsSticked = true;
                }
            }
            CurrentlySelectedCreature = ((MeisterGeister.Model.Held)BattlegroundObjects.Where(x => x is MeisterGeister.Model.Held && x.IsSticked).First()).Name;
        }

        public void StickEnemies()
        {
            if (BattlegroundObjects.Where(x => x is MeisterGeister.Model.Held && x.IsSticked).Any()) return;
            if (!BattlegroundObjects.Where(x => x is MeisterGeister.Model.Gegner).Any()) return;
            foreach (var h in BattlegroundObjects)
            {
                if (h is Gegner)
                {
                    h.IsSticked = true;
                }
            }
            CurrentlySelectedCreature = ((MeisterGeister.Model.Gegner)BattlegroundObjects.Where(x => x is MeisterGeister.Model.Gegner && x.IsSticked).First()).Name;
        }


        private void CreateFogOfWar()
        {
            //Fog of War
            BitmapImage originalImage = new BitmapImage();
            originalImage.BeginInit();
            originalImage.UriSource = new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Bodenplan/FogOfWar.png", UriKind.Absolute);
            originalImage.EndInit();
            BitmapSource bitmapSource = new FormatConvertedBitmap(originalImage, PixelFormats.Bgra32, null, 0);
            WriteableBitmap wbmap = new WriteableBitmap(bitmapSource);

            int h = wbmap.PixelHeight;
            int w = wbmap.PixelWidth;
            FogPixelData = new int[w * h];
            int widthInByte = 4 * w;

            wbmap.CopyPixels(FogPixelData, widthInByte, 0);
            System.Drawing.Bitmap bm = BitmapFromWriteableBitmap(wbmap);
            System.Drawing.Image img = (System.Drawing.Image)bm;
            FogImage = wbmap;
            Nullable<int> i = null;
            while (File.Exists(Ressources.GetFullApplicationPathForPictures() + "Fog-of-War" + i + ".png"))
                if (i == null) i = 0; else i++;
            FogImageFilename = Ressources.GetFullApplicationPathForPictures() + "Fog-of-War" + i + ".png";
            CreateThumbnail(FogImageFilename, FogImage.Clone());
            ImageObject io = CreateImageObject(FogImageFilename, new Point(FogOffsetX, FogOffsetY));
            io.IsFogPicture = true;
            io.IsVisible = false;
        }

        private System.Drawing.Bitmap BitmapFromWriteableBitmap(WriteableBitmap writeBmp)
        {
            System.Drawing.Bitmap bmp;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create((BitmapSource)writeBmp));
                enc.Save(outStream);
                bmp = new System.Drawing.Bitmap(outStream);
            }
            return bmp;
        }

        private void CreateThumbnail(string filename, BitmapSource image5)
        {
            if (filename != string.Empty)
            {
                PngBitmapEncoder encoder5 = new PngBitmapEncoder();
                encoder5.Frames.Add(BitmapFrame.Create(image5));
                using (Stream stream = File.Create(filename))
                {
                    encoder5.Save(stream);
                    stream.Dispose();
                }
            }
        }
        private BitmapImage ConvertWriteableBitmapToBitmapImage(WriteableBitmap wbm)
        {
            BitmapImage bmImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
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


        #endregion
        
        #region Commands

        private Base.CommandBase _onBtnPosIniWindow = null;
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
        void BtnPosIniWindow(object obj)
        {            
            PosIniWindow++;
            if (PosIniWindow == 5) PosIniWindow = 1;
            SetIniWindowPosition();
        }


        private Base.CommandBase _onBtnUmwandelnFernkampf = null;
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
        void UmwandelnFernkampf(object obj)
        {
            ManöverInfo mi = Global.CurrentKampf.Kampf.InitiativListe.FirstOrDefault(t => t.Manöver.Ausführender.Kämpfer == SelectedObject as IKämpfer);
            if (mi == null) return;
            mi.UmwandelnFernkampf.Execute(null);
        }

        private Base.CommandBase _onBtnUmwandelnSonstiges = null;
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
        void UmwandelnSonstiges(object obj)
        {
            ManöverInfo mi = Global.CurrentKampf.Kampf.InitiativListe.FirstOrDefault(t => t.Manöver.Ausführender.Kämpfer == SelectedObject as IKämpfer);
            if (mi == null) return;
            mi.Manöver.VerbleibendeDauer = 0;
            mi.UmwandelnSonstiges.Execute(null);
        }

        private Base.CommandBase _onBtnUmwandelnAttacke = null;
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
        void UmwandelnAttacke(object obj)
        {
            ManöverInfo mi = Global.CurrentKampf.Kampf.InitiativListe.FirstOrDefault(t => t.Manöver.Ausführender.Kämpfer == SelectedObject as IKämpfer);
            if (mi == null) return;
            mi.Manöver.VerbleibendeDauer = 0;
            mi.UmwandelnAttacke.Execute(obj); 
            Global.CurrentKampf.SelectedManöver = mi;
            Global.CurrentKampf.Kampf.SelectedManöverInfo = mi;
        }

        private Base.CommandBase _onBtnCenterMeisterView = null;
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
        void CenterMeisterView(object obj)
        {
            MeisterZoom = 1;
            if (Global.ContextHeld.HeldenGruppeListe.Count == 0) return;
            double xMin = Global.ContextHeld.HeldenGruppeListe.Min(t => t.CreatureX) - Global.ContextHeld.HeldenGruppeListe[0].CreatureWidth;
            double yMin = Global.ContextHeld.HeldenGruppeListe.Min(t => t.CreatureY) - Global.ContextHeld.HeldenGruppeListe[0].CreatureHeight;
            MeisterZoomTransX = -xMin;
            MeisterZoomTransY = -yMin;
            MeisterZoom = .5;
            CenterPlayerView(null);
        }

        private Base.CommandBase _onBtnCenterPlayerView = null;
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
        void CenterPlayerView(object obj)
        {
//NEUUUU
//            double M_Left = (-1) * MeisterZoomTransX * MeisterZoom;
//           double M_Top = (-1) * MeisterZoomTransY * MeisterZoom;
//            double M_Width = (-1) * M_Left + (Global.CurrentKampf.BodenplanView.ActualWidth - 30) / MeisterZoom;
//            double M_Height = (-1) * M_Top + (Global.CurrentKampf.BodenplanView.ActualHeight  - 30) / MeisterZoom;
            
//            double M_anzFelderWidth = M_Width / 100;
//            double M_anzFelderHeight = M_Height / 100;

            double x1 = 10000;
            double y1 = 10000;
            double x2 = 10000;
            double y2 = 10000;

            foreach (Held h in Global.ContextHeld.HeldenGruppeListe)
            {
                if (h.CreatureX < x1) x1 = h.CreatureX - h.CreatureWidth;
                if (h.CreatureY < y1) y1 = h.CreatureY + h.CreatureHeight;
                if (h.CreatureX > x2) x2 = h.CreatureX + h.CreatureWidth;
                if (h.CreatureY < y2) y2 = h.CreatureY - h.CreatureHeight;
            }
            PlayerGridOffsetX = (x1);
            PlayerGridOffsetY = (-y2);

//            PlayerGridOffsetX = M_Left;
//            PlayerGridOffsetY = M_Top;

//            if (Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow != null)
//            {
//                double ScaleSpielerWidth = Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.ActualWidth / M_anzFelderWidth / 100;
//                double ScaleSpielerHeight = Global.CurrentKampf.BodenplanViewModel.SpielerScreenWindow.ActualHeight / M_anzFelderHeight / 100;


//                ScaleSpielerGrid = Math.Min(ScaleSpielerWidth, ScaleSpielerHeight);// ScaleSpielerGrid;
//            }
        }


        private Base.CommandBase _onSetBackgroundClick = null;
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
        void SetBackgroundClick(object obj)
        {
            BackgroundImage = ViewHelper.ChooseFile("Hintergrundbild setzen", "", false, new String[9] { "bmp", "gif", "jpg", "jpeg", "jpe", "jfif", "png", "tif", "tiff" });
            if (string.IsNullOrEmpty(BackgroundImage)) return;

            BackgroundFilename = new FileInfo(BackgroundImage).Name ?? "";
            BackgroundOffsetX = 0;
            ImageObject io = CreateImageObject(BackgroundImage, new Point(BackgroundOffsetX, BackgroundOffsetY));
            io.IsBackgroundPicture = true;
            io.IsVisible = false;
        }

        #endregion
        
        #region Dispose
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                if (Global.CurrentKampf != null)
                {
                    Global.CurrentKampf.Kampf.Kämpfer.CollectionChanged -= OnKämpferListeChanged;
                    //_kampfVM.PropertyChanged -= OnKampfPropertyChanged;
                }
            }

            // Free any unmanaged objects here. 
            //
            disposed = true;
        }

        ~BattlegroundViewModel()
        {
            Dispose(false);
        }
        #endregion
    }
}
