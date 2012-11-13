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

        private ViewModel.Kampf.Logic.Kampf _kampf;

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
        }

        public int Height {
            get { return _height; }
        }


        public void AddHeld(Model.Held held, Point pos) {
            _helden.Add(held);
            _positionen.Add(held, pos);
            _viewingDirections.Add(held, 0.0);
        }
        public void AddGegner(Model.Gegner gegner, Point pos)
        {
            _gegner.Add(gegner);
            _positionen.Add(gegner, pos);
            _viewingDirections.Add(gegner, 0.0);
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
            _kampf.Kämpfer.Remove(creature);
        }

        public HashSet<Model.Held> Heroes
        {
            get { return _helden; }
        }

        public HashSet<Model.Gegner> Enemies
        {
            get { return _gegner; }
        }

        public Dictionary<IKämpfer, Point> Positions {
            get { return _positionen; }
        }

        public Boolean Contains(IKämpfer c) {
            return _gegner.Contains(c) || _helden.Contains(c);
        }

        public HashSet<ArenaHindernisAbstract> Hindernisse {
            get { return _hindernisse; }
        }

        public Boolean ContainsHeldWidthId(Guid heldId) {
            foreach (Model.Held held in _helden)
            {
                if (held.HeldGUID == heldId)
                    return true;
            }
            return false;
        }

        public void Populate(ViewModel.Kampf.Logic.Kampf kampf)
        {
            _kampf = kampf;

            foreach (KämpferInfo kämpferInfo in _kampf.Kämpfer) {
                if (kämpferInfo.Kämpfer is Model.Held)
                {
                    AddHeld((Model.Held)kämpferInfo.Kämpfer, new Point(10, 10));
                }
                else if (kämpferInfo.Kämpfer is Model.Gegner)
                {
                    AddGegner((Model.Gegner)kämpferInfo.Kämpfer, new Point(10, 10));
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
    }
}
