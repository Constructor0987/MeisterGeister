using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace MeisterGeister.ViewModel.Arena.Logic
{
    public class ArenaHindernisRechteckig : ArenaHindernisAbstract
    {

        private double _breite;
        private double _höhe;

        public ArenaHindernisRechteckig(Point position, double breite, double höhe, Color farbe)
        {
            _position = position;
            _breite = breite;
            _höhe = höhe;
            _farbe = farbe;
        }

        public double Breite { get { return _breite; } }
        public double Höhe { get { return _höhe; } }

        public override bool UmschließtPosition(Point p)
        {
            return p.X >= _position.X && p.X <= _position.X + _breite && p.Y >= _position.Y && p.Y <= _position.Y + _höhe;
        }
    }
}
