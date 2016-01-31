using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class RoutingPoint : ViewModelBase
    {
        public virtual double X { get; set; }
        public virtual double Y { get; set; }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    Set(ref _isSelected, value);
                }
            }
        }
        public string Image { get; private set; }

        public RoutingPoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
            this.IsSelected = false;
        }

        public RoutingPoint(double x, double y, string image)
            : this(x, y)
        {
            this.Image = image;
        }
    }
}
