using System;
using System.Collections.Generic;
using System.Text;

namespace MeisterGeister.ViewModel.ZooBot.Logic.Landschaften
{
    public abstract class BasisLandschaft
    {
        private string m_Name;

        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }
    }

    public class LandschaftEis : BasisLandschaft
    {
        public LandschaftEis()
        {
            this.Name = "Eis";
        }
    }

    public class LandschaftWuesteUndWuestenrand : BasisLandschaft
    {
        public LandschaftWuesteUndWuestenrand()
        {
            this.Name = "Wüste und Wüstenrand";
        }
    }

    public class LandschaftGebirge : BasisLandschaft
    {
        public LandschaftGebirge()
        {
            this.Name = "Gebirge";
        }
    }

    public class LandschaftHochland : BasisLandschaft
    {
        public LandschaftHochland()
        {
            this.Name = "Hochland";
        }
    }

    public class LandschaftSteppe : BasisLandschaft
    {
        public LandschaftSteppe()
        {
            this.Name = "Steppe";
        }
    }

    public class LandschaftGraslandWiesen : BasisLandschaft
    {
        public LandschaftGraslandWiesen()
        {
            this.Name = "Grasland, Wiesen";
        }
    }

    public class LandschaftFlussSeeuferTeiche : BasisLandschaft
    {
        public LandschaftFlussSeeuferTeiche()
        {
            this.Name = "Fluss- und Seeufer, Teiche";
        }
    }

    public class LandschaftKuesteStrand : BasisLandschaft
    {
        public LandschaftKuesteStrand()
        {
            this.Name = "Küste, Strand";
        }
    }

    public class LandschaftFlussauen : BasisLandschaft
    {
        public LandschaftFlussauen()
        {
            this.Name = "Flussauen";
        }
    }

    public class LandschaftSumpfMoor : BasisLandschaft
    {
        public LandschaftSumpfMoor()
        {
            this.Name = "Sumpf und Moor";
        }
    }

    public class LandschaftRegenwald : BasisLandschaft
    {
        public LandschaftRegenwald()
        {
            this.Name = "Regenwald";
        }
    }

    public class LandschaftWald : BasisLandschaft
    {
        public LandschaftWald()
        {
            this.Name = "Wald";
        }
    }

    public class LandschaftWaldrand : BasisLandschaft
    {
        public LandschaftWaldrand()
        {
            this.Name = "Waldrand";
        }
    }

    public class LandschaftMeer : BasisLandschaft
    {
        public LandschaftMeer()
        {
            this.Name = "Meer";
        }
    }

    public class LandschaftHoehleFeucht : BasisLandschaft
    {
        public LandschaftHoehleFeucht()
        {
            this.Name = "Höhle (feucht)";
        }
    }

    public class LandschaftHoehleTrocken : BasisLandschaft
    {
        public LandschaftHoehleTrocken()
        {
            this.Name = "Höhle (trocken)";
        }
    }
}
