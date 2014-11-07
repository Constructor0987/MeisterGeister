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

namespace MeisterGeister.ViewModel.Bodenplan
{
    public class BattlegroundViewModel:INotifyPropertyChanged
    {
//  *Hintergrundfarbe
//  *ZLevel bei initialisierung fix
//  *Bilder Fixen!
//  *Größe Kreaturen

        private bool _pathLine = false;
        private bool _filledPathLine = false;
        private double _currentMousePositionX, _currentMousePositionY;
        private PathGeometry _tilePathData = new PathGeometry();
        private Color _selectedColor = Colors.DarkGray, _selectedFillColor = Colors.LightGray;
        private bool _loadWithoutPictures = false, _saveWithoutPictures = false;
        private KämpferInfoListe _kaempferliste;
        private List<BattlegroundBaseObject> stickyHeroesTempList = new List<BattlegroundBaseObject>();

        public List<BattlegroundBaseObject> StickyListBoxBattlegroundObjects 
        {
            get { return BattlegroundObjects.Where(x => x is Held && x.IsSticked).ToList(); }
            set { value = BattlegroundObjects.Where(x => x is Held && x.IsSticked).ToList(); OnPropertyChanged("StickyListBoxBattlegroundObjects"); }
        }

        void _selectedListBoxBattlegroundObjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(String.Format("CollectionChanged: {0} {1}", e.Action, e.NewItems)); 
        }

        ObservableCollection<BattlegroundBaseObject> _battlegroundObjects;
        public ObservableCollection<BattlegroundBaseObject> BattlegroundObjects
        {
            get { return _battlegroundObjects ?? (_battlegroundObjects = new ObservableCollection<BattlegroundBaseObject>()); }
            set { _battlegroundObjects = value; }
        }

        private Kampf.KampfViewModel _kampfVM;

        public Kampf.KampfViewModel KampfVM
        {
            get { return _kampfVM; }
            set
            {
                if (KampfVM != null)
                {
                    _kampfVM.KämpferListe.CollectionChanged -= OnKämpferListeChanged;
                    _kampfVM.PropertyChanged -= OnKampfPropertyChanged;
                }
                _kampfVM = value;
                AddAllCreatures();
                if (KampfVM != null)
                {
                    _kampfVM.KämpferListe.CollectionChanged += OnKämpferListeChanged;
                    _kampfVM.PropertyChanged += OnKampfPropertyChanged;
                }
            }
        }

        private void OnKampfPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "SelectedKämpferInfo"&&KampfVM.SelectedKämpfer!=null)
            { 
                //TODO: Update SelectedObject
                //SelectedObject = BattlegroundObjects.Select(x=>x is BattlegroundCreature).First(x=>((BattlegroundCreature)x) == KampfVM.SelectedKämpfer);
                //Console.WriteLine("Ein anderer Kämpfer ({0}) wurde ausgewählt", KampfVM.SelectedKämpfer.Name );
            }
            //Console.WriteLine("Property Changed");
        }

        private void OnKämpferListeChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
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
            if (((Wesen) kämpfer).IsHeld)
            {
                ((Held)kämpfer).LoadBattlegroundPortrait(((Held)kämpfer).Bild,true);
                BattlegroundObjects.Add(((Held) kämpfer));
            }
            else
            {
                ((Gegner)kämpfer).LoadBattlegroundPortrait(((Gegner)kämpfer).Bild, true);
                BattlegroundObjects.Add(((Gegner)kämpfer));
            }
        }

        public void AddAllCreatures()
        {
            foreach (var k in KampfVM.KämpferListe)
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
            for (int i = BattlegroundObjects.Count-1; i >= 0;i--)
                if (BattlegroundObjects[i] is BattlegroundCreature)
                {
                    BattlegroundObjects.Remove(BattlegroundObjects[i]);
                    Console.WriteLine("Remove All Creatures");
                }
            
        }

        private bool _leftShiftPressed=false;
        public bool LeftShiftPressed
        {
            get { return _leftShiftPressed; }
            set
            {
                _leftShiftPressed = value;
                OnPropertyChanged("LeftShiftPressed");
            }
        }

        private String _currentlySelectedCreature = "";
        public String CurrentlySelectedCreature
        {
            get { return _currentlySelectedCreature; }
            set { _currentlySelectedCreature = value; OnPropertyChanged("CurrentlySelectedCreature"); }
        }

        //get / set ZLevel
        public double ZLevel
        {
            get { return SelectedObject != null ? SelectedObject.ZLevel : 10; }
            set 
            { 
                if (SelectedObject != null)
                {
                    SelectedObject.ZLevel = value ;
                    PossibleZLevels = Ressources.GetPossibleZLevels(BattlegroundObjects);
                }
                OnPropertyChanged("ZLevel");
            }
        }

        private string _visibleZLevels = "10";
        public string VisibleZLevels
        {
            get { return _visibleZLevels; }
            set
            {
                _visibleZLevels = value;
                OnPropertyChanged("VisibleZLevels");
                Ressources.SetVisibilityDependetOnZLevelSelection(ref _battlegroundObjects, VisibleZLevels, IgnorZLevel);
            }

        }

        private string _selectedObjectsFromListBox = "";
        public string SelectedObjectsFromListBox
        {
            get { return _selectedObjectsFromListBox; }
            set
            {
                _selectedObjectsFromListBox = value;
                OnPropertyChanged("SelectedObjectsFromListBox");
                Console.WriteLine(_selectedObjectsFromListBox);
            }
        }

        private List<string> _possibleZLevels = new List<string>();
        public List<string> PossibleZLevels
        {
            get { return _possibleZLevels; }
            set
            {
                _possibleZLevels = value;
                OnPropertyChanged("PossibleZLevels");
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
                _showZ = value;
                OnPropertyChanged("ShowZ");
            }
        }

        private bool _ignorZLevel = true;
        public bool IgnorZLevel
        {
            get { return _ignorZLevel; }
            set
            {
                _ignorZLevel = value;
                OnPropertyChanged("IgnorZLevel");
                Ressources.SetVisibilityDependetOnZLevelSelection(ref _battlegroundObjects, VisibleZLevels, IgnorZLevel);
            }
        }

        private bool _showSightArea = true;
        public bool ShowSightArea
        {
            get { return _showSightArea; }
            set
            {
                _showSightArea = value;
                OnPropertyChanged("ShowSightArea");
            }
        }

        private double _sightAreaLenght = 120;
        public double SightAreaLenght
        {
            get { return _sightAreaLenght; }
            set 
            { 
                _sightAreaLenght = value;
                OnPropertyChanged("SightAreaLenght");
                Ressources.SetNewSightAreaLength(ref _battlegroundObjects, SightAreaLenght);
            }
        }

        private bool _isEditorModeEnabled = true;
        public bool IsEditorModeEnabled
        {
            get { return _isEditorModeEnabled; }
            set
            {                
                _isEditorModeEnabled = value;
                OnPropertyChanged("IsEditorModeEnabled");
                //Ressources.SetEditorMode(ref _battlegroundObjects, _isEditorModeEnabled);
            }
        }

        private bool _showCreatureName = true;
        public bool ShowCreatureName
        {
            get { return _showCreatureName; }
            set
            {
                _showCreatureName = value;
                OnPropertyChanged("ShowCreatureName");
            }
        }
        

        //get / set stroke thickness
        public double StrokeThickness
        {
            get { return SelectedObject != null ? SelectedObject.StrokeThickness : 20; }
            set
            {
                if (SelectedObject != null) SelectedObject.StrokeThickness = value;
                OnPropertyChanged("StrokeThickness");
            }
        }

        public double ObjectSize
        {
            get
            {
                if (SelectedObject != null)
                {
                    if (SelectedObject is ImageObject) return ((ImageObject)SelectedObject).ObjectSize;
                    if (SelectedObject is BattlegroundCreature) return ((BattlegroundCreature) SelectedObject).ObjectSize;
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
                OnPropertyChanged("ObjectSize");
            }
        }

        public double Opacity
        {
            get { return SelectedObject != null ? SelectedObject.Opacity : 1; }
            set
            {
                if (SelectedObject != null)
                {
                    Console.WriteLine("SetOpacity from: {0} to {1}", Opacity, value);
                    SelectedObject.Opacity = value;
                }
                
                OnPropertyChanged("Opacity");
            }
        }

        public Color SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
                if (SelectedObject != null) SelectedObject.ObjectColor = new SolidColorBrush(value); 
                OnPropertyChanged("SelectedColor");
            }
        }

        public Color SelectedFillColor
        {
            get { return _selectedFillColor; }
            set
            {               
                _selectedFillColor = value;
                Console.WriteLine(_selectedFillColor.ScA);
                if (SelectedObject != null) SelectedObject.FillColor = value;
                OnPropertyChanged("SelectedFillColor");
            }
        }

        public float SelectedFillColorAlpha
        {
            get { return SelectedFillColor.ScA; }
            set
            {
                Color c = SelectedFillColor;
                c.ScA = value;
                SelectedFillColor = c;
                Console.WriteLine("Alpha: " + value + " || " + SelectedFillColor.ScA.ToString());
                OnPropertyChanged("SelectedFillColorAlpha");
            }
        }

        public bool LoadWithoutPictures
        {
            get { return _loadWithoutPictures; }
            set { _loadWithoutPictures = value; OnPropertyChanged("LoadWithoutPictures"); }
        }

        public bool SaveWithoutPictures
        {
            get { return _saveWithoutPictures; }
            set { _saveWithoutPictures = value; OnPropertyChanged("SaveWithoutPictures"); }
        }

        public PathGeometry TilePathData
        {
            get { return _tilePathData; }
            set
            {
                _tilePathData = value;
                OnPropertyChanged("TilePathData");
            }
        }

        public double CurrentMousePositionX 
        {
            get { return _currentMousePositionX; }
            set 
            { 
                _currentMousePositionX = value;
                OnPropertyChanged("CurrentMousePositionX");
            }
        }

        public double CurrentMousePositionY
        {
            get { return _currentMousePositionY; }
            set
            {
                _currentMousePositionY = value;
                OnPropertyChanged("CurrentMousePositionY");
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
                    if (SelectedObject is BattlegroundCreature) KampfVM.SelectedKämpfer = ((IKämpfer)SelectedObject);
                    OnPropertyChanged("SelectedObject");
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
            if (SelectedObject !=null)
                if(SelectedObject is BattlegroundCreature) KampfVM.DeleteKämpfer(null);
                else BattlegroundObjects.Remove(SelectedObject);
        }

        #region different lines 
        public bool CreateLine
        {
            get { return _pathLine; }
            set
            {
                _pathLine = value;
                if(_pathLine) CreateFilledLine = false;
                OnPropertyChanged("CreateLine");
                IsEditorModeEnabled = !value;
            }
        }


        public bool CreateFilledLine
        {
            get { return _filledPathLine; }
            set
            {
                _filledPathLine = value;
                if (_filledPathLine) CreateLine = false;
                OnPropertyChanged("CreateFilledLine");
                IsEditorModeEnabled = !value;
            }
        }

        #endregion

        private bool _creatingNewLine;
        public bool CreatingNewLine
        {
            get { return _creatingNewLine; }
            set
            { 
                _creatingNewLine = value;
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
                _creatingNewFilledLine = value;
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
                new ImageObject(picurl,p.X,p.Y);
            BattlegroundObjects.Add(imageobject);
            return imageobject;
        }

        //Move Objects
        private bool _isMoving;
        public bool IsMoving
        {
            get { return _isMoving; }
            set { _isMoving = value; }
        }

        public void MoveObject(double xOld, double yOld, double xNew, double yNew)
        {
            if (SelectedObject != null)
            {
                if (SelectedObject is PathLine)
                {
                    ((PathLine)SelectedObject).MoveObject(xNew-xOld,yNew-yOld);
                }else if (SelectedObject is FilledPathLine)
                {
                    ((FilledPathLine)SelectedObject).MoveObject(xNew - xOld, yNew - yOld);
                }
                else if (SelectedObject is ImageObject)
                {
                    ((ImageObject)SelectedObject).MoveObject(xNew - xOld, yNew - yOld);
                }
                else if (SelectedObject is BattlegroundCreature)
                {
                    ((BattlegroundCreature)SelectedObject).MoveObject(xNew - xOld, yNew - yOld,false);
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
                        ((PathLine) SelectedObject).ChangeLastPoint(new Point(x2, y2));
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
            
                for (int i = 0; i < BattlegroundObjects.Count;i++ )
                {
                    if (BattlegroundObjects[i].Equals(SelectedObject))
                    {
                        BattlegroundBaseObject b = SelectedObject;
                        if (raise&&i!=BattlegroundObjects.Count-1)
                        {
                            BattlegroundObjects.Remove(SelectedObject);
                            BattlegroundObjects.Insert(i + 1, b);
                            SelectedObject = BattlegroundObjects[i + 1];
                        }
                        else if(!raise&&i>0)
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
            for (int i = BattlegroundObjects.Count-1; i >= 0 ; i--)
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
                    int position = toTop ? BattlegroundObjects.Count-1 : 0;
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
            var ocloaded= bg.LoadMapFromXML(filename, LoadWithoutPictures);

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
            OnPropertyChanged("StrokeThickness");
            OnPropertyChanged("ObjectSize");
            OnPropertyChanged("Opacity");
            OnPropertyChanged("ZLevel");
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

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        #endregion
    }
}
