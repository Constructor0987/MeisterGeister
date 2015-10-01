using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MeisterGeister.Model
{
    public partial class Polygon
    {
        private static Dictionary<Guid, System.Drawing.Region> RegionCache = new Dictionary<Guid, System.Drawing.Region>();

        public Polygon()
        {
            PolygonGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !PolygonGUID.ToString().StartsWith("00000000-0000-0000-00"); }
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
            if (Left == null || Right == null || Top == null || Bot == null)
                return false;
            //Bounding box
            if (Left > p.X + tolerance || Right < p.X - tolerance || Top < p.Y - tolerance || Bot > p.Y + tolerance)
                return false;
            if (String.IsNullOrEmpty(Data))
                return true;
            //Hittest
            System.Drawing.Region r = null;
            //check cache
            if (RegionCache.ContainsKey(PolygonGUID))
                r = RegionCache[PolygonGUID];

            if (r == null)
            {
                //parse points
                var path = new System.Drawing.Drawing2D.GraphicsPath();
                var points = ParseData(Data);
                path.AddPolygon(points);
                r = new System.Drawing.Region(path);
                RegionCache.Add(PolygonGUID, r);
            }
            
            var p2 = new System.Drawing.Point((int)(p.X * 10000), (int)(p.Y * -10000));
            if (tolerance <= 0)
                return r.IsVisible(p2);
            else
            {
                var rect = new System.Drawing.Rectangle(p2.X - (int)(tolerance * 5000), p2.Y - (int)(tolerance * 5000), (int)(tolerance * 10000), (int)(tolerance * 10000));
                return r.IsVisible(rect);
            }
        }

        static System.Drawing.Point[] ParseData(string data)
        {
            var points = new List<System.Drawing.Point>();

            int numStart = -1;
            bool firstD = true;
            double d1 = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == ')' || data[i] == ',' || data[i] == ' ' || data[i] == '(')
                {
                    if (numStart == -1)
                        continue;
                    //parse number
                    var d = Double.Parse(data.Substring(numStart, i - numStart), System.Globalization.CultureInfo.InvariantCulture);
                    if (firstD)
                    {
                        d1 = d;
                        firstD = false;
                    }
                    else
                    {
                        points.Add(new System.Drawing.Point((int)(d1 * 10000),(int)(d * -10000)));
                        firstD = true;
                    }
                    numStart = -1;
                }
                else
                {
                    if (numStart == -1)
                        numStart = i;
                }
            }
            return points.ToArray();
        }
    }
}
