using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.ViewModel.Arena.Logic;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.ViewModel.Arena
{

    public class Arena {

        private int _width;     //in meters!
        private int _height;    //in meters!

        private HashSet<Model.Held> _helden;
        private HashSet<Model.Gegner> _gegner;
        private HashSet<ArenaHindernisAbstract> _hindernisse;

        private Dictionary<IKämpfer, Point> _positionen;
        private Dictionary<IKämpfer, double> _viewingDirections;    //in radian measure from "north" clockwise
        private Dictionary<Model.Gegner, int> _gegnerIndex;
        private int _nextEnemyIndex = 1;

        public View.Arena.ArenaWindow BodenplanWindow { get; set; }

        private ViewModel.Kampf.Logic.Kampf _kampf;
        public ViewModel.Kampf.Logic.Kampf Kampf { get { return _kampf; } }

        private ViewModel.Kampf.KampfViewModel _kampfViewModel;
        public ViewModel.Kampf.KampfViewModel KampfViewModel { get { return _kampfViewModel; } }

        public Arena(int width, int height) {
            _width = width;
            _height = height;

            _helden = new HashSet<Model.Held>();
            _gegner = new HashSet<Model.Gegner>();
            _hindernisse = new HashSet<ArenaHindernisAbstract>();
            _positionen = new Dictionary<IKämpfer, Point>();
            _viewingDirections = new Dictionary<IKämpfer, double>();
            _gegnerIndex = new Dictionary<Model.Gegner, int>();

        }

        public int GetEnemyIndex(Model.Gegner e) {
            return _gegnerIndex[e];
        }

        public int Width {
            get { return _width; }
            set { _width = value; }
        }

        public int Height {
            get { return _height; }
            set { _height = value; }
        }


        public void AddHeld(Model.Held held, Point pos) {
            _helden.Add(held);
            if (!_positionen.ContainsKey(held))
                _positionen.Add(held, pos);
            if (!_viewingDirections.ContainsKey(held))
                _viewingDirections.Add(held, 0.0);
        }
        public void AddGegner(Model.Gegner gegner, Point pos)
        {
            _gegner.Add(gegner);
            if (!_positionen.ContainsKey(gegner))
                _positionen.Add(gegner, pos);
            if (!_viewingDirections.ContainsKey(gegner))
                _viewingDirections.Add(gegner, 0.0);
            if (!_gegnerIndex.ContainsKey(gegner))
                _gegnerIndex.Add(gegner, _nextEnemyIndex);
            _nextEnemyIndex++;
        }

        public void RemoveCreature(IKämpfer creature)
        {
            if (creature is Model.Held)
                _helden.Remove((Model.Held)creature);
            else if (creature is Model.Gegner)
                _gegner.Remove((Model.Gegner)creature);

            _positionen.Remove(creature);

            // aus Kampf entfernen
            if (_kampf.Kämpfer.Kämpfer.Contains(creature))
                _kampf.Kämpfer.Remove(creature);
        }

        public void RemoveCreatureAll()
        {
            _helden.Clear();
            _gegner.Clear();

            _positionen.Clear();
        }

        public HashSet<Model.Held> Heroes
        {
            get { return _helden; }
            set { _helden = value; }
        }

        public HashSet<Model.Gegner> Enemies
        {
            get { return _gegner; }
            set { _gegner = value; }
        }

        public Dictionary<IKämpfer, Point> Positions {
            get { return _positionen; }
            set { _positionen = value; }
        }

        public Boolean Contains(IKämpfer c) {
            return _gegner.Contains(c) || _helden.Contains(c);
        }

        public HashSet<ArenaHindernisAbstract> Hindernisse {
            get { return _hindernisse; }
            set { _hindernisse = value; }
        }

        public Boolean ContainsHeldWidthId(Guid heldId) {
            foreach (Model.Held held in _helden)
            {
                if (held.HeldGUID == heldId)
                    return true;
            }
            return false;
        }

        public void Populate(ViewModel.Kampf.KampfViewModel kampfVM)
        {
            _kampfViewModel = kampfVM;
            _kampf = kampfVM.Kampf;

            Point mitte = new Point(_width / 2, _height / 2);

            // Punkte-Liste erstellen, die dann spiralförmig sortiert wird
            // entlang dieser Liste können die Tokens dann platziert werden
            List<Point> pointList = new List<Point>(_width * _height);
            for (int x = _width / -2; x < _width / 2; x++)
                for (int y = _height / -2; y < _height / 2; y++)
                    pointList.Add(new Point(x, y));
            pointList.Sort(new PointComparer());

            int i = 0;

            // Helden einfügen
            foreach (KämpferInfo kämpferInfo in _kampf.Kämpfer) {
                if (kämpferInfo.Kämpfer is Model.Held)
                {
                    if (i >= pointList.Count)
                        i = 0;
                    AddHeld((Model.Held)kämpferInfo.Kämpfer, new Point(pointList[i].X + mitte.X, pointList[i].Y + mitte.Y));
                    i++;
                }
            }

            // Gegner einfügen
            i += pointList.Count / 4; // Gegner auf Abstand setzen
            foreach (KämpferInfo kämpferInfo in _kampf.Kämpfer)
            {
                if (kämpferInfo.Kämpfer is Model.Gegner)
                {
                    if (i >= pointList.Count)
                        i = 0;
                    AddGegner((Model.Gegner)kämpferInfo.Kämpfer, new Point(pointList[i].X + mitte.X, pointList[i].Y + mitte.Y));
                    i++;
                }
            }
        }

        public void AddOrRemoveObstacle(Point p) {
            ArenaHindernisAbstract obst = GetObstacleUnderPoint(p);

            //Morrandir: Wenn es hier noch kein Hindernis gibt
            if (obst == null) {
                Point hindernisPos = new Point(Math.Floor(p.X), Math.Floor(p.Y));
                obst = new ArenaHindernisRechteckig(hindernisPos, 1.0, 1.0, Colors.Black);
                _hindernisse.Add(obst);
            }
            else {
                _hindernisse.Remove(obst);
            }
        }

        private ArenaHindernisAbstract GetObstacleUnderPoint(Point p) {
            foreach (ArenaHindernisAbstract hindernis in _hindernisse) {
                if (hindernis.UmschließtPosition(p))
                    return hindernis;
            }
            return null;
        }

        #region Subklasse zum Vergleichen von zwei Punkten

        class PointComparer : IComparer<Point>
        {
            public int Compare(Point x, Point y)
            {
                //compare distance between two points
                return ((int)x.X * (int)x.X + (int)x.Y * (int)x.Y) - ((int)y.X * (int)y.X + (int)y.Y * (int)y.Y);
            }
        }

        #endregion

    }
}
