using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public class PathLine : BattlegroundBaseObject
    {
        //locals
        private PathGeometry _pathGeometryData = new PathGeometry();
        private PathFigureCollection _pathFigureCollection = new PathFigureCollection();
        private PathFigure _pathFigure = new PathFigure();
        private PathSegmentCollection _pathSegmentCollection = new PathSegmentCollection();

        public PathLine(Point p)
        {
            CreateNewPath(p);
        }

        #region PathGeometry
        


        public PathGeometry PathGeometryData
        {
            get { return _pathGeometryData; }
            set
            {
                _pathGeometryData = value;
                OnChanged("PathGeometryData");
            }
        }

        //Creates new path with p at start and endpoint
        public void CreateNewPath(Point p)
        {
            ZDisplayX = p.X + 10;
            ZDisplayY = p.Y + 10;
            _pathFigure.StartPoint = p;
            _pathSegmentCollection.Add(new LineSegment(p,true));
            _pathFigure.Segments = _pathSegmentCollection;
            _pathFigureCollection.Add(_pathFigure);
            PathGeometryData.Figures = _pathFigureCollection;
        }
         
        public void AddNewPointToSeries(Point p)
        {
            LineSegment l = new LineSegment(p, true);
            l.IsSmoothJoin = true;
            _pathSegmentCollection.Add(l);
            OnChanged("PathGeometryData");
        }

        public void ChangeFirstPoint(Point p)
        {
            _pathFigure.StartPoint = p;
            ZDisplayX = p.X+10;
            ZDisplayY = p.Y+10;
            OnChanged("PathGeometryData");
        }

        public void ChangeLastPoint(Point p) 
        {
            ((LineSegment)_pathSegmentCollection[_pathSegmentCollection.Count - 1]).Point = p;
            OnChanged("PathGeometryData");
        }

        public void MoveObject(double deltaX, double deltaY)
        {
            foreach (LineSegment l in _pathSegmentCollection.ToList())
            {
                Point newPoint = new Point(l.Point.X + deltaX, l.Point.Y+deltaY);
                l.Point = newPoint;
            }

            //change first Point at last cause of OnPropertyChanged Event...
            Point newStartPoint = new Point(_pathFigure.StartPoint.X + deltaX, _pathFigure.StartPoint.Y+deltaY);
            ChangeFirstPoint(newStartPoint);
        }

        #endregion

        /*public PathLine CreateDummyPathForTestPurpose(double seed)
            {

                PathFigure hexPathFigure1 = new PathFigure();
                hexPathFigure1.StartPoint = new Point(0, 0);
                PathSegmentCollection hexPathSegementCollection1 = new PathSegmentCollection();
                hexPathSegementCollection1.Add(new LineSegment(new Point(100, 100 + seed), true));
                hexPathSegementCollection1.Add(new LineSegment(new Point(200 - seed, 100), true));
                hexPathSegementCollection1.Add(new LineSegment(new Point(300 + seed, 150), true));
                hexPathSegementCollection1.Add(new LineSegment(new Point(200, 400), true));

                PathFigure hexPathFigure2 = new PathFigure();
                hexPathFigure2.StartPoint = new Point(250, 450);
                PathSegmentCollection hexPathSegementCollection2 = new PathSegmentCollection();
                hexPathSegementCollection2.Add(new LineSegment(new Point(600 - seed, 800 + seed), true));
                hexPathSegementCollection2.Add(new LineSegment(new Point(800 + seed, 1200 - seed), true));
                //hexPathSegementCollection2.Add(new LineSegment(new Point(0,p9.Y), true));

                hexPathFigure1.Segments = hexPathSegementCollection1;
                hexPathFigure2.Segments = hexPathSegementCollection2;

                PathFigureCollection hexFigureCollection = new PathFigureCollection();
                hexFigureCollection.Add(hexPathFigure1);
                hexFigureCollection.Add(hexPathFigure2);

                PathGeometry hexPathGeometry = new PathGeometry();
                hexPathGeometry.Figures = hexFigureCollection;

                var pathLine = new PathLine();
                pathLine.PathGeometryData = hexPathGeometry;

                return pathLine;
            }*/
    }
}
