using MeisterGeister.Logic.General.Patterns;
using MeisterGeister.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class RoutingPointBuilder : IBuilder<RoutingOrt, RoutingPointBuilderArgs>
    {
        public RoutingOrt Build(RoutingPointBuilderArgs args)
        {
            RoutingOrt result = new RoutingOrt(args.X, args.Y, args.Name, args.Wegtyp, args.Strecke, args.RouteToEnd, args.MovementModifier, GetImage(args.PointType, args.Name), args.PointType);
            return result;
        }

        private string GetImage(string type, string name)
        {
            string result = null;
            if (!string.IsNullOrEmpty(name))
            {
                result = "/DSA MeisterGeister;component/Images/Icons/DereGlobus/";
                switch (type)
                {
                    case "Dorf":
                        result += "Dorf.png";
                        break;
                    case "Großstadt":
                        result += "Grossstadt.png";
                        break;
                    case "Kleinstadt":
                        result += "Kleinstadt.png";
                        break;
                    case "Metropole":
                        result += "Metropole.png";
                        break;
                    case "Stadt":
                        result += "Stadt.png";
                        break;
                    case "Sonderziel":
                        result += FurtherProceeding(type, name);
                        break;
                    default:
                        result += "Sonstige_Privathaus.png";
                        break;
                }
            }
            return result;
        }

        private string FurtherProceeding(string type, string name)
        {
            string result = null;

            if (string.IsNullOrEmpty(name))
                result = "";
            else if (IsCastle(name))
                result = "Sonstige_Staatliches_Bauwerk.png";
            else if (IsReligiousBuilding(name))
                result = "Sonstige_Sakralbauwerk.png";
            else
                result = "Sonstige_Ruine.png";
            return result;
        }

        private bool IsReligiousBuilding(string name)
        {
            return name.ContainsIgnoreCase("Kloster")
                || name.ContainsIgnoreCase("Abtei")
                || name.ContainsIgnoreCase("Arras des Mott");
        }

        private bool IsCastle(string name)
        {
            return name.ContainsIgnoreCase("Burg")
                || name.ContainsIgnoreCase("Feste")
                || name.ContainsIgnoreCase("Kurkum");
        }
    }
}
