using MeisterGeister.Model;
using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class RoutingLineType : ViewModelBase
    {
        public SolidColorBrush Color
        {
            get
            {
                switch(Wegtyp.ID)
                {
                    // Wald
                    case 4:
                        return new SolidColorBrush(Colors.DarkGreen);
                    // Gebirge & Gebirgspass
                    case 5:
                    case 11:
                        return new SolidColorBrush(Colors.Brown);
                    // Wüste
                    case 6:
                        return new SolidColorBrush(Colors.BurlyWood);
                    // Fluss, Meer, See
                    case 7:
                    case 8:
                    case 9:
                    case 12:
                        return new SolidColorBrush(Colors.RoyalBlue);
                    default:
                        return new SolidColorBrush(Colors.Red);
                }
            }
        }

        public Wegtyp Wegtyp { get; private set; }

        public RoutingLineType(Wegtyp wegtyp)
        {
            this.Wegtyp = wegtyp;
        }
    }
}
