using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace MeisterGeister.ViewModel.Arena.Logic
{
    public abstract class ArenaHindernisAbstract
    {
        protected Color _farbe;
        protected Point _position;

        public Color Farbe { get { return _farbe; } }
        public Point Position { get { return _position; } }

        public abstract bool UmschließtPosition(Point p);
    }
}
