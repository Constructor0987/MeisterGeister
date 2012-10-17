using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Input;
using System.Globalization;
// Eigene Usings
using MeisterGeister.Logic.General;
using VM = MeisterGeister.ViewModel;
using MeisterGeister.ViewModel.Arena.Logic;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.View.Arena
{
    public class ArenaViewer : Canvas {

        private static int DEFAULT_HELD_GS = 8;

        private static Color BG_COLOR = Colors.DarkGreen;
        public static Color DEFAULT_HERO_COLOR = Colors.DarkBlue;
        public static Color DEFAULT_ENEMY_COLOR = Colors.DarkRed;

        public static int MIN_PIXELS_PER_METER = 10;
        public static int MAX_PIXELS_PER_METER = 128;
        private static int DEFAULT_PIXELS_PER_METER = 30;

        private Boolean _isGrabbed;
        private Point _grabbedMousePos;
        private Point _mouseDownPos;

        private Canvas _bGLayer;
        private Canvas _gridLayer;
        private Canvas _creatureLayer;
        private Canvas _fxLayer;
        private Canvas _hindernisLayer;

        private Dictionary<IKämpfer, CreatureCanvas> _creatureHasCreatureCanvas;
        private HashSet<IKämpfer> _creaturesShowingCircles;
        private Dictionary<Guid, Image> _heldIdHasPortrait;
        private Dictionary<string, Image> _gegnerNameHasPortrait;

        private ScrollViewer _currentScrollViewer;

        private VM.Arena.Arena _arena;
        private int _pixelsPerMeterInit;
        private double _zoomFactor;

        public event EventHandler<CreatureEventArgs> CreatureRemoved;

        public ArenaViewer() : this(DEFAULT_PIXELS_PER_METER) { }

        public ArenaViewer(int pixelsPerMeterInit) {
            _zoomFactor = 1.0;
            _pixelsPerMeterInit = Math.Min(Math.Max(pixelsPerMeterInit, MIN_PIXELS_PER_METER), MAX_PIXELS_PER_METER);
            _creaturesShowingCircles = new HashSet<IKämpfer>();
            _creatureHasCreatureCanvas = new Dictionary<IKämpfer, CreatureCanvas>();
            _heldIdHasPortrait = new Dictionary<Guid, Image>();
            _gegnerNameHasPortrait = new Dictionary<string, Image>();
            MouseLeftButtonDown += OnLeftMouseButtonDown;
            MouseLeftButtonUp += OnLeftMouseButtonUp;
            MouseMove += onMouseMove;
            MouseEnter += onMouseEnter;        
        }               

        private void drawBG() {
            if (Children.Contains(_bGLayer))
                Children.Remove(_bGLayer);

            double squareSize = PixelsPerMeter;
            int numSquaresHorizontal = _arena.Width;
            int numSquaresVertical = _arena.Height;

            _bGLayer = new Canvas();

            _bGLayer.Width = numSquaresHorizontal * squareSize;
            _bGLayer.Height = numSquaresVertical * squareSize;
            _bGLayer.Background = new SolidColorBrush(BG_COLOR);

            //Listener zum Zeichnen neuer Hindernisse
            _bGLayer.MouseRightButtonDown += OnRightMouseButtonDown;    
            
            Children.Add(_bGLayer);
        }

        public ScrollViewer CurrentScrollViewer {
            get { return _currentScrollViewer; }
            set { _currentScrollViewer = value; }
        }

        private void drawGrid() {

            if (Children.Contains(_gridLayer))
                Children.Remove(_gridLayer);

            _gridLayer = new Canvas();

            double squareSize = PixelsPerMeter;
            int numSquaresHorizontal = _arena.Width;
            int numSquaresVertical = _arena.Height;

            //draw vertical lines
            for (int i = 0; i <= numSquaresHorizontal; i++) {
                Line l = new Line();
                l.Stroke = System.Windows.Media.Brushes.Black;
                l.StrokeThickness = i == 0 || i == numSquaresHorizontal ? 2.0 : 0.5;
                l.X1 = i * squareSize;
                l.Y1 = 0;
                l.X2 = i * squareSize;
                l.Y2 = numSquaresVertical * squareSize;
                _gridLayer.Children.Add(l);
            }

            //draw horizontal lines
            for (int i = 0; i <= numSquaresVertical; i++) {
                Line l = new Line();
                l.Stroke = System.Windows.Media.Brushes.Black;
                l.StrokeThickness = i == 0 || i == numSquaresVertical ? 2.0 : 0.5;
                l.X1 = 0;
                l.Y1 = i * squareSize;
                l.X2 = numSquaresHorizontal * squareSize;
                l.Y2 = i * squareSize;
                _gridLayer.Children.Add(l);
            }


            Children.Add(_gridLayer);

        }

        private void drawCreatures() {

            _creatureLayer = new Canvas();
            _creatureLayer.Width = _bGLayer.Width;
            _creatureLayer.Height = _bGLayer.Height;

            foreach (Model.Held hero in _arena.Heroes) {
                DrawCreature(hero, _arena.Positions[hero]);
            }

            foreach (Model.Gegner enemy in _arena.Enemies) {
                DrawCreature(enemy, _arena.Positions[enemy]);
            }

            Children.Add(_creatureLayer);
        }

        private void drawMovementCircles(IKämpfer creature) {

            int GS = DEFAULT_HELD_GS;

            Canvas circles = new Canvas();
            circles.Width = 3 * GS * 2 * PixelsPerMeter;
            circles.Height = 3 * GS * 2 * PixelsPerMeter;

            for (int i = 1; i <= 3; i++) {
                Ellipse circle = new Ellipse();
                circle.StrokeThickness = 1;
                
                circle.Stroke = new SolidColorBrush(_creatureHasCreatureCanvas[creature].CreatureColor);
                circle.Height = i * GS * 2 * PixelsPerMeter;
                circle.Width = i * GS * 2 * PixelsPerMeter;

                Canvas.SetLeft(circle, circles.Width / 2 - circle.Width / 2);
                Canvas.SetTop(circle, circles.Height / 2 - circle.Height / 2);

                circles.Children.Add(circle);


            }

            Canvas.SetLeft(circles, _arena.Positions[creature].X * PixelsPerMeter - circles.Width / 2);
            Canvas.SetTop(circles, _arena.Positions[creature].Y * PixelsPerMeter - circles.Height / 2);

            _fxLayer.Children.Add(circles);

        }

        private void DrawCreature(IKämpfer creature, Point position) {

            CreatureCanvas cc = new CreatureCanvas(creature, this);

            double wWidth = 1.0;
            double wHeight = 1.0;

            Canvas.SetLeft(cc, (position.X - wWidth / 2) * PixelsPerMeter);
            Canvas.SetTop(cc, (position.Y - wHeight / 2) * PixelsPerMeter);

            if (creature is Model.Gegner)
                cc.AddTextLayer();

            _creatureLayer.Children.Add(cc);
            _creatureHasCreatureCanvas[creature] = cc;
        }

        public Canvas CreatureCanvas {
            get { return _creatureLayer; }
        }

        public void DrawArena() {
            Children.Clear();

            drawBG();
            drawGrid();
            drawFX();
            DrawHindernisLayer();
            drawCreatures();
            Width = PixelsPerMeter * _arena.Width;
            Height = PixelsPerMeter * _arena.Height;

            Debug.WriteLine("Arena redrawn!");
        }

        private void drawFX() {
            _fxLayer = new Canvas();
            _fxLayer.Width = PixelsPerMeter * _arena.Width;
            _fxLayer.Height = PixelsPerMeter * _arena.Height;

            drawCircles();

            Children.Add(_fxLayer);
        }

        private void drawCircles() {
            HashSet<IKämpfer> newCreaturesShowingCircles = new HashSet<IKämpfer>();

            foreach (IKämpfer w in _creaturesShowingCircles)
                if (_arena.Contains(w))
                    newCreaturesShowingCircles.Add(w);

            _creaturesShowingCircles = newCreaturesShowingCircles;

            foreach (IKämpfer w in _creaturesShowingCircles)
                drawMovementCircles(w);
        }

        public int PixelsPerMeter {
            get { return (int)(_pixelsPerMeterInit * _zoomFactor); }
        }

        public void OnCreatureDragFinished(CreatureCanvas creatureCanvas, Point _ccPosistion) {
            //we update the creatures position in the arena model
            _arena.Positions[creatureCanvas.Creature] = new Point((_ccPosistion.X + creatureCanvas.Width / 2) / PixelsPerMeter, (_ccPosistion.Y + creatureCanvas.Height / 2) / PixelsPerMeter);
            //then we redraw with the updated model... just to be sure
            DrawArena();
        }

        public VM.Arena.Arena Arena {
            get { return _arena; }
            set { _arena = value; }
        }

        public double ZoomFactor {
            get { return _zoomFactor; }
            set { _zoomFactor = value; }
        }

        public void SwitchShowCircles(IKämpfer c) {
            if (_creaturesShowingCircles.Contains(c))
                _creaturesShowingCircles.Remove(c);
            else
                _creaturesShowingCircles.Add(c);
        }

        private void OnRightMouseButtonDown(object sender, MouseEventArgs args) {
            Point position = args.GetPosition(_gridLayer);
            _arena.AddOrRemoveObstacle(new Point(position.X / PixelsPerMeter, position.Y / PixelsPerMeter));
            DrawHindernisLayer();

            if (Children.Contains(_creatureLayer))
                Children.Remove(_creatureLayer);

            Children.Add(_creatureLayer);

        }

        private void OnLeftMouseButtonDown(object sender, MouseEventArgs args) {
            _isGrabbed = true;
            _mouseDownPos = args.GetPosition(this);

            if (ActualWidth > _currentScrollViewer.ActualWidth && ActualHeight > _currentScrollViewer.ActualHeight)
                Cursor = Cursors.ScrollAll;
            else if (ActualWidth > _currentScrollViewer.ActualWidth)
                Cursor = Cursors.ScrollWE;
            else if (ActualHeight > _currentScrollViewer.ActualHeight)
                Cursor = Cursors.ScrollNS;

            _grabbedMousePos = args.GetPosition(_currentScrollViewer);
        }

        private void onMouseEnter(object sender, MouseEventArgs args) {
            if (args.LeftButton != MouseButtonState.Pressed) {
                _isGrabbed = false;
                Cursor = Cursors.Arrow;
            }
        }

        private void onMouseMove(object sender, MouseEventArgs args) {
            if (_isGrabbed) {
                Point currPos = args.GetPosition(_currentScrollViewer);
                Vector diff = _grabbedMousePos - currPos;
                _currentScrollViewer.ScrollToHorizontalOffset(_currentScrollViewer.HorizontalOffset + diff.X);
                _currentScrollViewer.ScrollToVerticalOffset(_currentScrollViewer.VerticalOffset + diff.Y);
                _grabbedMousePos = currPos;
            }
        }

        private void OnLeftMouseButtonUp(object sender, MouseEventArgs args) {   
            _isGrabbed = false;
            Cursor = Cursors.Arrow;
        }

        private void DrawHindernisLayer() {
            if (Children.Contains(_hindernisLayer))
                Children.Remove(_hindernisLayer);

            _hindernisLayer = new Canvas();
            _hindernisLayer.Width = _arena.Width * PixelsPerMeter;
            _hindernisLayer.Height = _arena.Height * PixelsPerMeter;
            
            foreach (ArenaHindernisAbstract hindernis in _arena.Hindernisse) {

                if (hindernis is ArenaHindernisRechteckig) {
                    Rectangle rect = new Rectangle();
                    SolidColorBrush b = new SolidColorBrush(hindernis.Farbe);
                    b.Opacity = 0.5;
                    rect.Fill = b;
                    rect.Height = ((ArenaHindernisRechteckig)hindernis).Breite * PixelsPerMeter;
                    rect.Width = ((ArenaHindernisRechteckig)hindernis).Höhe * PixelsPerMeter;
                    Canvas.SetLeft(rect, hindernis.Position.X * PixelsPerMeter);
                    Canvas.SetTop(rect, hindernis.Position.Y * PixelsPerMeter);
                    _hindernisLayer.Children.Add(rect);
                }
            }

            //Listener zum Löschen von Hindernissen
            _hindernisLayer.MouseRightButtonDown += OnRightMouseButtonDown;    
            Children.Add(_hindernisLayer);
        }

        public Boolean isGrabbed {
            get { return _isGrabbed; }
        }

        public Point CurrentlyDisplayedCenter {
            get {
                double x;
                double y;

                if (ActualWidth < CurrentScrollViewer.ActualWidth) {
                    x = ActualWidth / 2;
                }
                else {
                    x = _currentScrollViewer.HorizontalOffset + _currentScrollViewer.ActualWidth / 2;
                }

                if (ActualHeight < CurrentScrollViewer.ActualHeight) {
                    y = ActualHeight / 2;
                }
                else {
                    y = _currentScrollViewer.VerticalOffset + _currentScrollViewer.ActualHeight / 2;
                }

                return new Point(x / PixelsPerMeter, y / PixelsPerMeter);
            }

            set {
                if (ActualWidth > CurrentScrollViewer.ActualWidth) {
                    _currentScrollViewer.ScrollToHorizontalOffset(value.X * PixelsPerMeter - _currentScrollViewer.ActualWidth / 2);
                }
                if (ActualHeight > CurrentScrollViewer.ActualHeight) {
                    _currentScrollViewer.ScrollToVerticalOffset(value.Y * PixelsPerMeter - _currentScrollViewer.ActualHeight / 2);
                }
            }
        }

        public void RemoveCreature(CreatureCanvas cc) {
            Arena.RemoveCreature(cc.Creature);
            DrawArena();
            OnCreatureRemoved(cc);
        }

        protected virtual void OnCreatureRemoved(CreatureCanvas creatureCanvas) {
            EventHandler<CreatureEventArgs> handler = CreatureRemoved;

            if (handler != null) {
                var args = new CreatureEventArgs() { CC = creatureCanvas };
                handler(this, args);
            }
        }

        public void AddHeldPortrait(Guid heldId, string portraitFilename) {
            try {
                _heldIdHasPortrait[heldId] = LoadImage(new Uri(portraitFilename, UriKind.Absolute));
            } catch {                
                _heldIdHasPortrait[heldId] = LoadImage(new Uri(@ArenaWindow.CREATURE_IMAGE_DIR + "question_mark_portrait.jpg", UriKind.Relative));
            }
        }

        public void AddGegnerPortrait(string bestiarumName, string portraitFilename)
        {

            string filename = getBestiarumBildLink(bestiarumName);
            if (filename != null){
                try {
                    _gegnerNameHasPortrait[bestiarumName] = LoadImage(new Uri(portraitFilename, UriKind.Absolute));
                }
                catch {
                    _gegnerNameHasPortrait[bestiarumName] = LoadImage(new Uri(@ArenaWindow.CREATURE_IMAGE_DIR + "question_mark_portrait.jpg", UriKind.Relative));
                }
            }
        }

        public Image GetHeldPortrait(Guid heldId) {
            if (_heldIdHasPortrait.ContainsKey(heldId))
                return _heldIdHasPortrait[heldId];
            else
                return null;

        }

        public Image GetGegnerPortrait(string bestiarumName) {
            if (_gegnerNameHasPortrait.ContainsKey(bestiarumName))
                return _gegnerNameHasPortrait[bestiarumName];
            else
                return null;
        }

        private Image LoadImage(Uri uri) {
            Image portrait = new Image();
            //portrait.Width = Width - 2 * frameWidth;
            //portrait.Height = Height - 2 * frameWidth;

            BitmapImage myBitmapImage = new BitmapImage();

            try {

                myBitmapImage.BeginInit();
                //myBitmapImage.UriSource = new Uri(@ArenaWindow.PORTRAIT_DIR + filename, UriKind.Relative);
                myBitmapImage.UriSource = uri;
                myBitmapImage.DecodePixelWidth = 256;// (int)portrait.Width;
                myBitmapImage.DecodePixelHeight = 256;// (int)portrait.Height;
                myBitmapImage.EndInit();
                portrait.Source = myBitmapImage;

            } catch {
                portrait = null;
            }

            return portrait;
        }


        private static string getBestiarumBildLink(string bestiarumName) {

            if (bestiarumName == "Krähe")
                return "crow.png";            
            
            return null;            
        }


        public class CreatureEventArgs : EventArgs{
            public CreatureCanvas CC { get; set; }
        }
    }
}