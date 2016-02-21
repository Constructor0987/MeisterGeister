using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Expression.Interactivity.Layout;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;
using System.Globalization;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.View.Arena
{


    public class CreatureCanvas : Canvas {

        private IKämpfer _creature;
        private ArenaViewer _arenaViewer;

        private Image _portrait;
        private Rectangle _shape;
        private Color _color;
        private Canvas _textLayer;
                
        private ContextMenu _contextMenu;
        private Canvas _movementCircles;

        private Point _lastDragBegunPoint = new Point(0,0);

        public CreatureCanvas(IKämpfer wesen, ArenaViewer arenaViewer) {
            _creature = wesen;
            _arenaViewer = arenaViewer;

            _color = wesen.Farbmarkierung != Color.FromArgb(0,0,0,0)? wesen.Farbmarkierung : (wesen is Model.Held ? ArenaViewer.DEFAULT_HERO_COLOR : ArenaViewer.DEFAULT_ENEMY_COLOR);
            
            MouseDragElementBehavior dragBehavior = new MouseDragElementBehavior();
            dragBehavior.Attach(this);
            dragBehavior.DragBegun += onDragBegun;
            dragBehavior.DragFinished += onDragFinished;
            dragBehavior.Dragging += onDragging;

            double wWidth = 1.0;
            double wHeight = 1.0;

            Width = wWidth * _arenaViewer.PixelsPerMeter;
            Height = wHeight * _arenaViewer.PixelsPerMeter;           

            createElements();

            Children.Add(_shape);
        }


        private void createElements(){ 
                       
            setPortrait();

            if (_portrait != null && _arenaViewer.PixelsPerMeter > 15) {
                Children.Add(_portrait);                
            } else {
                drawShape();
            }
            
            _contextMenu = new CreatureContextMenu(this);
            ContextMenu = _contextMenu;

            ToolTip = new TextBlock() { Text = _creature.Name };
        }
        /*
        private void createDirectionPointer() {
            
            _directionPointer = new Canvas();
            _directionPointer.Width = Width;
            _directionPointer.Height = Height;

            Line line = new Line();
            line.Stroke = new SolidColorBrush(_color);
            line.StrokeThickness = 2;
            line.X1 = _directionPointer.Width / 2;
            line.Y1 = 0;
            line.X2 = _directionPointer.Width / 2;
            line.Y2 = - _directionPointer.Height * 0.2;

            _directionPointer.Children.Add(line);
            

        }
         */

        protected override void OnMouseEnter(MouseEventArgs args) {
            if (!_arenaViewer.isGrabbed) {
                Cursor = Cursors.Hand;
                base.OnMouseEnter(args);
            } else {
                Cursor = Cursors.ScrollAll;
            }
        }

        protected override void OnMouseMove(MouseEventArgs args) {
            if (!_arenaViewer.isGrabbed) {
                Cursor = Cursors.Hand;
                base.OnMouseMove(args);
            } else {
                Cursor = Cursors.ScrollAll;
            }
        }

        private int getFrameWidth() {
            return Math.Min(5, (int)((double)_arenaViewer.PixelsPerMeter * 0.1));
        }

        private void setPortrait() {

            Image portrait = null;
            if (_creature is Model.Held){
                 portrait = _arenaViewer.GetHeldPortrait(((Model.Held)_creature).HeldGUID);
          
            } else if (_creature is Model.Gegner){
                portrait = _arenaViewer.GetGegnerPortrait(((Model.Gegner)_creature).GegnerBase.Name);
            }

            if (portrait != null) {
                _portrait = new Image();
                _portrait.Source = portrait.Source;
            }                      

            if (_portrait != null) {

                int frameWidth = getFrameWidth();

                _portrait.Width = Width - 2 * frameWidth;
                _portrait.Height = Height - 2 * frameWidth;

                Canvas.SetLeft(_portrait, frameWidth);
                Canvas.SetTop(_portrait, frameWidth);

                double wWidth = 1.0;
                double wHeight = 1.0;

                _shape = new Rectangle();
                _shape.RadiusX = _arenaViewer.PixelsPerMeter / 10;
                _shape.RadiusY = _arenaViewer.PixelsPerMeter / 10;
                _shape.StrokeThickness = frameWidth;
                _shape.Stroke = new SolidColorBrush(_color);
                _shape.Height = wWidth * _arenaViewer.PixelsPerMeter;
                _shape.Width = wHeight * _arenaViewer.PixelsPerMeter;
            }

        }


        private void drawShape() {
            _shape = new Rectangle();
            _shape.RadiusX = _arenaViewer.PixelsPerMeter / 7;
            _shape.RadiusY = _arenaViewer.PixelsPerMeter / 7;
            SolidColorBrush b = new SolidColorBrush();
            b.Color = _color;
            _shape.Fill = b;
            _shape.StrokeThickness = 2;
            _shape.Stroke = Brushes.Black;
            
            double wWidth = 1.0;
            double wHeight = 1.0;

            _shape.Width = wWidth * _arenaViewer.PixelsPerMeter;
            _shape.Height = wHeight * _arenaViewer.PixelsPerMeter;
        }

        public Color CreatureColor{
            get { return _color;}
            set { _color = value ;}
        }

        public IKämpfer Creature { 
            get { return _creature; } 
        }

        public Canvas MovementCircles {
            get { return _movementCircles; }
            set { _movementCircles = value; } 
        }

        private void onDragBegun(object sender, MouseEventArgs args) {
            _lastDragBegunPoint = args.GetPosition(_arenaViewer);
            args.Handled = true;
        }

        private void onDragFinished(object sender, MouseEventArgs args) {
            //Debug.WriteLine("drag finished!");

            if (args.GetPosition(_arenaViewer) != _lastDragBegunPoint){  
                _arenaViewer.OnCreatureDragFinished(this, getPositionOnViewer((MouseDragElementBehavior)sender));
            }
        }

        private void onDragging(object sender, MouseEventArgs args) {
            //Uneleganter Quick-Fix verhindert einen Absturz - Jonas
            if (Window.GetWindow(this) == null || PresentationSource.FromVisual(Window.GetWindow(this)) == null)
                return;

            Point pos = getPositionOnViewer((MouseDragElementBehavior)sender);

            if (pos.X < 0) {
                ((MouseDragElementBehavior)sender).X -= pos.X;
            } else {
                double xOverflow = pos.X + this.Width - _arenaViewer.ActualWidth;
                if (xOverflow > 0){
                    ((MouseDragElementBehavior)sender).X -= xOverflow;
                }
            }

            if (pos.Y < 0) {
                ((MouseDragElementBehavior)sender).Y -= pos.Y;
            } else {
                double yOverflow = pos.Y + this.Height - _arenaViewer.ActualHeight;
                if (yOverflow > 0) {
                    ((MouseDragElementBehavior)sender).Y -= yOverflow;
                }                    
            }             
        }

        private Point getPositionOnViewer(MouseDragElementBehavior sender){
            double vertOff;
            double horiOff;
            
            if (_arenaViewer.Width > _arenaViewer.CurrentScrollViewer.ActualWidth) {
                horiOff = _arenaViewer.CurrentScrollViewer.HorizontalOffset;
            } else {
                horiOff = -(_arenaViewer.CurrentScrollViewer.ActualWidth - _arenaViewer.ActualWidth) / 2;
            }

            if (_arenaViewer.Height > _arenaViewer.CurrentScrollViewer.ActualHeight) {
                vertOff = _arenaViewer.CurrentScrollViewer.VerticalOffset;
            } else {
                vertOff = -(_arenaViewer.CurrentScrollViewer.ActualHeight - _arenaViewer.ActualHeight) / 2;
            }

            Point screenPoint = PointToScreen(new Point(sender.X, sender.Y));
            Point pointOnViewer = _arenaViewer.CreatureCanvas.PointFromScreen(screenPoint);

            //we need to consider scrolling...
            pointOnViewer = new Point(pointOnViewer.X + horiOff, pointOnViewer.Y + vertOff);

            //I really don't know why we have to do this... but somehow the vector seems to be twice as long as it should be...
            pointOnViewer = new Point(pointOnViewer.X / 2, pointOnViewer.Y / 2);

            //in case anything went wrong...
           // pointOnViewer = new Point(pointOnViewer.X < 0 ? 0 : pointOnViewer.X, pointOnViewer.Y < 0 ? 0 : pointOnViewer.Y);

            return pointOnViewer;
        }

        public void AddTextLayer() {
            
            if (_creature is Model.Gegner) {

                if (Children.Contains(_textLayer))
                    Children.Remove(_textLayer);

                double wWidth = 1.0;
                double wHeight = 1.0;

                _textLayer = new Canvas();
                _textLayer.Height = wWidth * _arenaViewer.PixelsPerMeter;
                _textLayer.Width = wHeight * _arenaViewer.PixelsPerMeter;
                
                int frameWidth = getFrameWidth();

                Image myImage = new Image();

                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();

                FormattedText gegnerIndex = new FormattedText(_arenaViewer.Arena.GetEnemyIndex((Model.Gegner)_creature) + "",
                    new CultureInfo("de-de"),
                    FlowDirection.LeftToRight,
                    new Typeface(new FontFamily("Verdana"), FontStyles.Normal, FontWeights.Normal, new FontStretch()),
                    Math.Min(wHeight * _arenaViewer.PixelsPerMeter * 0.3, 22),
                    Brushes.Black);
                
                drawingContext.DrawText(gegnerIndex, new Point(frameWidth, frameWidth));

                //Morrandir: Wir zeigen den Namen der Kreatur nur dann, wenn die Darstellung "groß genug" ist und wenn kein Portrair vorhanden ist
                if (_arenaViewer.PixelsPerMeter >= 40 && _portrait == null) {

                    FormattedText gegnerName = new FormattedText(_creature.Name,
                        new CultureInfo("de-de"),
                        FlowDirection.LeftToRight,
                        new Typeface(new FontFamily("Verdana"), FontStyles.Normal, FontWeights.Normal, FontStretches.Condensed),
                        Math.Min(wHeight * _arenaViewer.PixelsPerMeter * 0.15, 16),
                        Brushes.Black);
                    gegnerName.MaxTextWidth = _textLayer.Width - 2 * frameWidth;
                    drawingContext.DrawText(gegnerName, new Point(frameWidth, frameWidth + wHeight * _arenaViewer.PixelsPerMeter * 0.35));
                }


                drawingContext.Close();

                //Morrandir: aus irgendeinem Grund muss man hier eine größere Breite angeben, sonst wird der Text abgeschnitten...
                RenderTargetBitmap bmp = new RenderTargetBitmap((int)_textLayer.Width * 2, (int)_textLayer.Height, 120, 96, PixelFormats.Pbgra32);
                bmp.Render(drawingVisual);
                myImage.Source = bmp;

                _textLayer.Children.Add(myImage);

                Children.Add(_textLayer);

            }
        }       

        public ArenaViewer ArenaViewer {
            get { return _arenaViewer; }
        }

    }
}
