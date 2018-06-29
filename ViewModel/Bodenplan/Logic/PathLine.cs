using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    [Serializable]
    public class PathLine : BattlegroundBaseObject
    {
        //locals
        [NonSerialized]
        private PathGeometry _pathGeometryData = new PathGeometry();
        [NonSerialized]
        private PathFigureCollection _pathFigureCollection = new PathFigureCollection();
        [NonSerialized]
        private PathFigure _pathFigure = new PathFigure();
        [NonSerialized]
        private PathSegmentCollection _pathSegmentCollection = new PathSegmentCollection();

        //Objectname
        public override string ObjectName
        {
            get { return "Linie"; }
        }

        public PathLine(Point p)
        {
            CreateNewPath(p);
        }

        #region PathGeometry

        [XmlIgnore]
        public PathGeometry PathGeometryData
        {
            get { return _pathGeometryData; }
            set
            {
                _pathGeometryData = value;
                OnChanged("PathGeometryData");
            }
        }

        private Point _getStartPoint = new Point();
        public Point GetStartPoint
        {
            get { return _getStartPoint;}
            set { Set(ref _getStartPoint, value); }
        }

        //Creates new path with p at start and endpoint
        public void CreateNewPath(Point p)
        {
            ZDisplayX = p.X + 10;
            ZDisplayY = p.Y + 10;
            GetStartPoint = p;
            _pathFigure.StartPoint = p;
            _pathSegmentCollection.Add(new LineSegment(p, true));
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


        public override void MoveObject(double deltaX, double deltaY, bool stickAtCursor)
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

        #region XmlSaveAndRestoreStuff

        public List<Point> _pointList = new List<Point>();

        public PathLine()
        {
            RestoreThisElementUsingXMLSource();
        }

        //save point data before serialization
        public void StorePathGeometryForXMLSerialization()
        {
            _pointList = new List<Point>();
            _pointList.Add(_pathFigure.StartPoint);
            foreach (var s in _pathSegmentCollection)
            {
                _pointList.Add(new Point(System.Convert.ToInt32(((LineSegment)s).Point.X), System.Convert.ToInt32(((LineSegment)s).Point.Y)));
            }
        }

        //Create empty Pathline and fill it with data from xml deserialization
        public void RestoreThisElementUsingXMLSource()
        {
            bool first = true;
            foreach (var p in _pointList)
            {
                if (first)
                {
                    first = false;
                    CreateNewPath(p);
                }
                else AddNewPointToSeries(p);
            }
        }

        public override void RunBeforeXMLSerialization()
        {
            StorePathGeometryForXMLSerialization();
        }

        public override void RunAfterXMLDeserialization()
        {
            RestoreThisElementUsingXMLSource();
        }

        #endregion
    }
}
