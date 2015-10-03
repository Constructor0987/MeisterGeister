using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister.ViewModel.Karte
{
    public class PflanzenAnOrtViewModel : Base.ViewModelBase
    {
        /// <summary>
        /// Zeigt alle an einem Punkt p auffindbaren Pflanzen an.
        /// </summary>
        /// <param name="p">Punkt in DG-Koordinaten</param>
        /// <param name="tolerance">Toleranz in Grad</param>
        public PflanzenAnOrtViewModel(Point p, double tolerance = 0.2)
        {
            Point = p;
            Tolerance = tolerance;
            LadePflanzen();
        }

        #region Properties
        private Point point;
        public Point Point
        {
            get { return point; }
            set { Set(ref point, value); }
        }

        private double tolerance = 0.2;
        public double Tolerance
        {
            get { return tolerance; }
            set { Set(ref tolerance, value); }
        }

        ObservableCollection<Model.Pflanze> pflanzen;
        public ObservableCollection<Model.Pflanze> Pflanzen
        {
            get {
                if(pflanzen == null)
                    pflanzen = new ObservableCollection<Model.Pflanze>();
                return pflanzen; 
            }
            protected set { Set(ref pflanzen, value); }
        }
        #endregion

        #region Methoden
        void LadePflanzen()
        {
            Pflanzen.Clear();
            var gebiete = Global.ContextZooBot.GetGebiete(Point, Tolerance);
            HashSet<Guid> pset = new HashSet<Guid>();
            foreach(var g in gebiete)
            {
                foreach (var p in g.Pflanze)
                {
                    if (pset.Contains(p.PflanzeGUID))
                        continue;
                    pset.Add(p.PflanzeGUID);
                    Pflanzen.Add(p);
                }
            }
        }
        #endregion
    }
}
