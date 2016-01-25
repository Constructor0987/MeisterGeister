using MeisterGeister.Logic.General.AStar;
using MeisterGeister.Model;
using MeisterGeister.Model.Service;
using MeisterGeister.ViewModel.Karte.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister.Logic.Karte
{
    public class SearchParametersRouting : SearchParameters
    {
        public List<Wegtyp> WegtypenNotAllowed { get; set; }
        public Fortbewegung Fortbewegung { get; set; }

        public SearchParametersRouting(Size boundaries, Point startPoint, Point endPoint, TravelType travelType,
            bool isRiverAllowed, bool isSeaAllowed, bool isMountainAllowed, bool isForrestAllowed)
            : this(boundaries, null, null, travelType, isRiverAllowed, isSeaAllowed, isMountainAllowed, isForrestAllowed)
        {
            this.StartNode = GetClosestOrt(startPoint);
            this.EndNode = GetClosestOrt(endPoint);
        }

        public SearchParametersRouting(Size boundaries, Node startNode, Node endNode, TravelType travelType,
            bool isRiverAllowed, bool isSeaAllowed, bool isMountainAllowed, bool isForrestAllowed)
            : base(boundaries, startNode, endNode)
        {
            int travelTypeId = (int)travelType;
            this.Fortbewegung = Global.ContextGeo.Liste<Fortbewegung>().Single(f => f.ID == travelTypeId);
            IEnumerable<Wegtyp> allWegtypen = Global.ContextGeo.Liste<Wegtyp>();
            this.WegtypenNotAllowed = new List<Wegtyp>();

            if (!isRiverAllowed)
                WegtypenNotAllowed.AddRange(allWegtypen.Where(w => w.ID == 7 || w.ID == 8));
            if (!isSeaAllowed)
                WegtypenNotAllowed.AddRange(allWegtypen.Where(w => w.ID == 9));
            if (!isMountainAllowed)
                WegtypenNotAllowed.AddRange(allWegtypen.Where(w => w.ID == 5 || w.ID == 11));
            if (!isForrestAllowed)
                WegtypenNotAllowed.AddRange(allWegtypen.Where(w => w.ID == 4));
        }

        private Ort GetClosestOrt(Point actualStart)
        {
            return Global.ContextGeo.LoadClosestOrt(actualStart);
        }
    }
}
