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
            TilePathData = BattlegroundUtilities.HexCellTile(100);
        }

        private double _currentMousePositionX, _currentMousePositionY;
        private PathGeometry _tilePathData = new PathGeometry();
        private Color _selectedColor = Colors.DarkGray, _selectedFillColor = Colors.LightGray;
        private bool _loadWithoutPictures = false, _saveWithoutPictures = false;
        private List<BattlegroundBaseObject> stickyHeroesTempList = new List<BattlegroundBaseObject>();

        public List<BattlegroundBaseObject> StickyListBoxBattlegroundObjects
        {
            get { return BattlegroundObjects.Where(x => x is Held && x.IsSticked).ToList(); }
            set { value = BattlegroundObjects.Where(x => x is Held && x.IsSticked).ToList(); OnChanged("StickyListBoxBattlegroundObjects"); }
        }

        void _selectedListBoxBattlegroundObjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(String.Format("CollectionChanged: {0} {1}", e.Action, e.NewItems));
        }

        ObservableCollection<BattlegroundBaseObject> _battlegroundObjects;
        public ObservableCollection<BattlegroundBaseObject> BattlegroundObjects
        {
            get { return _battlegroundObjects ?? (_battlegroundObjects = new ObservableCollection<BattlegroundBaseObject>()); }
            set
            {
                if (Set(ref _battlegroundObjects, value))
                {
                    kämpferliste = null;
                    objektliste = null;
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

        private Kampf.KampfViewModel _kampfVM;
        public Kampf.KampfViewModel KampfVM
        {
            get { return _kampfVM; }
            set
            {
                if (KampfVM != null)
                {
                    _kampfVM.Kampf.Kämpfer.CollectionChanged -= OnKämpferListeChanged;
                    //_kampfVM.PropertyChanged -= OnKampfPropertyChanged;
                }
                _kampfVM = value;
                AddAllCreatures();
                if (KampfVM != null)
                {
                    _kampfVM.Kampf.Kämpfer.CollectionChanged += OnKämpferListeChanged;
                    //_kampfVM.PropertyChanged += OnKampfPropertyChanged;
                }
                UpdateCreaturesFromChangedKampferlist();
            }
        }

        public void UpdateCreaturesFromChangedKampferlist()
        {
            foreach (var k in KampfVM.Kampf.Kämpfer)
            {
                ((Wesen)((KämpferInfo)k).Kämpfer).PropertyChanged -= OnWesenPropertyChanged;
            }
            foreach (var k in KampfVM.Kampf.Kämpfer)
            {
                ((Wesen)((KämpferInfo)k).Kämpfer).PropertyChanged += OnWesenPropertyChanged;
            }

        }


        //Event gets fired when Wesen.PropertyChanged event fires...
        private void OnWesenPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Position") ((BattlegroundCreature)sender).UpdateCreaturePosition();
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
            foreach (var k in KampfVM.Kampf.Kämpfer)
            {
                AddCreature(k.Kämpfer);
            }
        }

        public void RemoveCreature(IKämpfer creature)
        {

            BattlegroundObjects.Remove((Wesen)creature);
        }

        public void RemoveCreatureAll()
        {
            for (int i = BattlegroundObjects.Count - 1; i >= 0; i--)
                if (BattlegroundObjects[i] is BattlegroundCreature)
                {
                    BattlegroundObjects.Remove(BattlegroundObjects[i]);
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

        private String _currentlySelectedCreature = "";
        public String CurrentlySelectedCreature
        {
            get { return _currentlySelectedCreature; }
            set { Set(ref _currentlySelectedCreature, value); }
        }

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
                OnChanged("ZLevel");
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

        private List<string> _possibleZLevels = new List<string>();
        public List<string> PossibleZLevels
        {
            get { return _possibleZLevels; }
            set
            {
                Set(ref _possibleZLevels, value);
            }
        }

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
                if(Set(ref _ignorZLevel, value))
                    Ressources.SetVisibilityDependetOnZLevelSelection(ref _battlegroundObjects, VisibleZLevels, IgnorZLevel);
            }
        }

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

        private bool _showCreatureName = true;
        public bool ShowCreatureName
        {
            get { return _showCreatureName; }
            set
            {
                Set(ref _showCreatureName, value);
            }
        }


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

        public double ObjectSize
        {
            get
            {
                if (SelectedObject != null)
                {
                    if (SelectedObject is ImageObject) return ((ImageObject)SelectedObject).ObjectSize;
                    if (SelectedObject is BattlegroundCreature) return ((BattlegroundCreature)SelectedObject).ObjectSize;
                }
                return 1;
            }
            set
            {
                if (SelectedObject != null)
                {
                    if (SelectedObject is ImageObject) ((ImageObject)SelectedObject).ObjectSize = value;
                    if (SelectedObject is BattlegroundCreature) ((BattlegroundCreature)SelectedObject).ObjectSize = value;
                }
                OnChanged("ObjectSize");
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

        public PathGeometry TilePathData
        {
            get { return _tilePathData; }
            set
            {
                Set(ref _tilePathData, value);
            }
        }

        public double CurrentMousePositionX
        {
            get { return _currentMousePositionX; }
            set
            {
                Set(ref _currentMousePositionX, value);
            }
        }

        public double CurrentMousePositionY
        {
            get { return _currentMousePositionY; }
            set
            {
                Set(ref _currentMousePositionY, value);
            }
        }

        //get / set selected Object
        private BattlegroundBaseObject _selectedObject;
        public BattlegroundBaseObject SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                if (!IsMoving)
                {
                    BattlegroundObjects.ToList().ForEach(x => x.IsHighlighted = false);
                    _selectedObject = value;
                    if (SelectedObject is BattlegroundCreature) KampfVM.SelectedKämpfer = KampfVM.Kampf.Kämpfer.First(ki => ki.Kämpfer == ((IKämpfer)SelectedObject));
                    if(SelectedObject != null)
                    {
                        SelectedFillColor = SelectedObject.FillColor;
                        SelectedColor = SelectedObject.ObjectXMLColor;
                        StrokeThickness = SelectedObject.StrokeThickness;
                    }
                    OnChanged("SelectedObject");
                }
            }
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
                if (SelectedObject is BattlegroundCreature) KampfVM.DeleteKämpfer();
                else BattlegroundObjects.Remove(SelectedObject);
        }

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
            var imageobject =
                new ImageObject(picurl, p.X, p.Y);
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

        public void MoveObject(double xOld, double yOld, double xNew, double yNew)
        {
            if (SelectedObject != null)
            {
                if (SelectedObject is PathLine)
                {
                    ((PathLine)SelectedObject).MoveObject(xNew - xOld, yNew - yOld);
                }
                else if (SelectedObject is FilledPathLine)
                {
                    ((FilledPathLine)SelectedObject).MoveObject(xNew - xOld, yNew - yOld);
                }
                else if (SelectedObject is ImageObject)
                {
                    ((ImageObject)SelectedObject).MoveObject(xNew - xOld, yNew - yOld);
                }
                else if (SelectedObject is BattlegroundCreature)
                {
                    ((BattlegroundCreature)SelectedObject).MoveObject(xNew - xOld, yNew - yOld, false);
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
            }
        }

        public void RemoveNewObjects()
        {
            BattlegroundObjects.Where(x => x.IsNew).ToList().ForEach(x => BattlegroundObjects.Remove(x));
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

        public void SaveBattlegroundToXML(String filename)
        {
            BattlegroundXMLLoadSave bg = new BattlegroundXMLLoadSave();
            bg.SaveMapToXML(BattlegroundObjects, filename, SaveWithoutPictures);
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

        }

        public void SelectionChangedUpdateSliders()
        {
            OnChanged("StrokeThickness");
            OnChanged("ObjectSize");
            OnChanged("Opacity");
            OnChanged("ZLevel");
        }

        public void ClearBattleground()
        {
            BattlegroundObjects.Where(x => !(x is BattlegroundCreature)).ToList().ForEach(x => BattlegroundObjects.Remove(x));
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
                if (KampfVM != null)
                {
                    _kampfVM.Kampf.Kämpfer.CollectionChanged -= OnKämpferListeChanged;
                    //_kampfVM.PropertyChanged -= OnKampfPropertyChanged;
                    foreach (var k in KampfVM.Kampf.Kämpfer)
                    {
                        ((Wesen)((KämpferInfo)k).Kämpfer).PropertyChanged -= OnWesenPropertyChanged;
                    }
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
