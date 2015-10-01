using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MeisterGeister.Model
{
    public partial class Gebiet
    {
        public Gebiet()
        {
            GebietGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !GebietGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Enthält das Gebiet den DG-Punkt?
        /// </summary>
        /// <param name="p">Punkt in DG-Koordinaten</param>
        /// <returns></returns>
        public bool Contains(Point p, double tolerance = 0)
        {
            //Bounding box
            if (Left > p.X + tolerance || Right < p.X - tolerance || Top < p.Y - tolerance || Bot > p.Y + tolerance)
                return false;
            if (Polygon == null || Polygon.Count == 0)
                return true;
            foreach (var poly in Polygon)
            {
                if (poly.Contains(p, tolerance))
                    return true;
            }
            return false;
        }
    }
}
