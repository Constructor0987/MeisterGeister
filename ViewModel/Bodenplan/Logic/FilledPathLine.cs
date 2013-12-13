using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public class FilledPathLine : BattlegroundBaseObject
    {
        private PathGeometry _pathGeometryData = new PathGeometry();
        private PathFigureCollection _pathFigureCollection = new PathFigureCollection();
        private PathFigure _pathFigure = new PathFigure();
        private PathSegmentCollection _pathSegmentCollection = new PathSegmentCollection();

        public FilledPathLine(Point p)
        {
            CreateNewPath(p);
        }

        #region PathGeometry

        public PathGeometry FilledPathGeometryData
        {
            get { return _pathGeometryData; }
            set
            {
                _pathGeometryData = value;
                OnPropertyChanged("FilledPathGeometryData");
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
            FilledPathGeometryData.Figures = _pathFigureCollection;
        }
         
        public void AddNewPointToSeries(Point p)
        {
            LineSegment l = new LineSegment(p, true);
            l.IsSmoothJoin = true;
            _pathSegmentCollection.Add(l);
            OnPropertyChanged("FilledPathGeometryData");
        }

        public void ChangeFirstPoint(Point p)
        {
            ZDisplayX = p.X + 10;
            ZDisplayY = p.Y + 10;
            _pathFigure.StartPoint = p;
            OnPropertyChanged("FilledPathGeometryData");
        }

        public void ChangeLastPoint(Point p) 
        {
            ((LineSegment)_pathSegmentCollection[_pathSegmentCollection.Count - 1]).Point = p;
            OnPropertyChanged("FilledPathGeometryData");
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


  


    }
}
