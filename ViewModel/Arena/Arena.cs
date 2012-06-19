using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.ViewModel.Arena.Logic;
using MeisterGeister.ViewModel.Kampf.LogicAlt;

namespace MeisterGeister.ViewModel.Arena
{

    public class Arena {

        private int _width;     //in meters!
        private int _height;    //in meters!

        private HashSet<Held> _helden;
        private HashSet<Gegner> _gegner;
        private HashSet<ArenaHindernisAbstract> _hindernisse;

        private Dictionary<Wesen, Point> _positionen;
        private Dictionary<Wesen, double> _viewingDirections;    //in radian measure from "north" clockwise
        private Dictionary<Gegner, int> _gegnerIndex;
        private int _nextEnemyIndex = 1;

        public Arena(int width, int height) {
            _width = width;
            _height = height;

            _helden = new HashSet<Held>();
            _gegner = new HashSet<Gegner>();
            _hindernisse = new HashSet<ArenaHindernisAbstract>();
            _positionen = new Dictionary<Wesen, Point>();
            _viewingDirections = new Dictionary<Wesen, double>();
            _gegnerIndex = new Dictionary<Gegner, int>();

        }

        public int GetEnemyIndex(Gegner e) {
            return _gegnerIndex[e];
        }

        public int Width {
            get { return _width; }
        }

        public int Height {
            get { return _height; }
        }


        public void AddHeld(Held held, Point pos) {
            _helden.Add(held);
            _positionen.Add(held, pos);
            _viewingDirections.Add(held, 0.0);
        }
        public void AddGegner(Gegner gegner, Point pos)
        {
            _gegner.Add(gegner);
            _positionen.Add(gegner, pos);
            _viewingDirections.Add(gegner, 0.0);
            _gegnerIndex.Add(gegner, _nextEnemyIndex);
            _nextEnemyIndex++;
        }

        public void RemoveCreature(Wesen creature)
        {
            if (creature is Held)
                _helden.Remove((Held)creature);
            else if (creature is Gegner)
                _gegner.Remove((Gegner)creature);

            _positionen.Remove(creature);
        }

        public HashSet<Held> Heroes {
            get { return _helden; }
        }

        public HashSet<Gegner> Enemies {
            get { return _gegner; }
        }

        public Dictionary<Wesen, Point> Positions {
            get { return _positionen; }
        }

        public Boolean Contains(Wesen c) {
            return _gegner.Contains(c) || _helden.Contains(c);
        }

        public HashSet<ArenaHindernisAbstract> Hindernisse {
            get { return _hindernisse; }
        }

        public Boolean ContainsHeldWidthId(Guid heldId) {
            foreach (Held held in _helden) {
                if (held.Id == heldId)
                    return true;
            }
            return false;
        }

        public void Populate(MeisterGeister.ViewModel.Kampf.LogicAlt.Kampf kampf) {
            foreach (IKämpfer kämpfer in kampf.KämpferListe) {
                if (kämpfer is Held) {
                    AddHeld((Held)kämpfer, new Point(10, 10));
                }
                else if (kämpfer is Gegner) {
                    AddGegner((Gegner)kämpfer, new Point(10, 10));
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
