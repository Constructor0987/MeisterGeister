using System.Windows;
using System.Windows.Media;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public class RectangleObject : BattlegroundBaseObject
    {
        private Point _p1 = new Point( 0, 0 );
        private Point _p2 = new Point(160, 90);
        private double _rectPositionX = 0;
        private double _rectPositionY = 0;
        private double _height = 90;
        private double _width = 160;

        public RectangleObject()
        { }

        public RectangleObject(Point p1, Point p2)
        {
            _p1 = p1;
            _p2 = p2;
            RectPositionX = _p1.X;
            RectPositionY = _p1.Y;

            RectHeight = p2.Y - p1.Y;
            RectWidth = p2.X - p1.X;
        }

        public double RectPositionX
        {
            get { return _rectPositionX; }
            set { Set(ref _rectPositionX, value); }
        }
        public double RectPositionY
        {
            get { return _rectPositionY; }
            set { Set(ref _rectPositionY, value); }
        }
        public double RectHeight
        {
            get { return _height; }
            set { Set(ref _height, value); }
        }
        public double RectWidth
        {
            get { return _width; }
            set { Set(ref _width, value); }
        }


        public Point P1
        {
            get { return _p1; }
            set { Set(ref _p1, value); }
        }

        public Point P2
        {
            get { return _p2; }
            set { Set(ref _p2, value); }
        }

        public override string ObjectName
        {
            get { return "Spieler Monitor Rect"; }
        }

        public override void MoveObject(double deltaX, double deltaY, bool stickAtCursor)
        {
         
        }

        public override void RunAfterXMLDeserialization()
        {
        }

        public void ChangeLastPoint(Point p)
        {
            RectHeight = p.Y - P1.Y;
            RectWidth = p.X - P1.X;
        }

        public override void RunBeforeXMLSerialization()
        {
            //nothing special to take care of...
        }
    }
}
