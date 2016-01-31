using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class RoutingStage : RoutingPoint
    {
        public RoutingStage(double x, double y)
            : base(x, y)
        {
        }

        public RoutingStage(double x, double y, string image)
            : base(x, y, image)
        {
        }
    }
}
