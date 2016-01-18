using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte
{
    public class RoutingPoint : ViewModelBase
    {
        public double ActualX { get; private set; }
        public double ActualY { get; private set; }
        public double X { get { return ActualX - 5; } }
        public double Y { get { return ActualY - 5; } }
        public string Name { get; set; }

        public RoutingPoint(double x, double y, string name)
        {
            this.ActualX = x;
            this.ActualY = y;
            this.Name = name;
        }
    }
}
