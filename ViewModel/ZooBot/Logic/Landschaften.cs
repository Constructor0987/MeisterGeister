using System;
using System.Collections.Generic;
using System.Text;

namespace MeisterGeister.ViewModel.ZooBot.Logic.Landschaften
{
    public abstract class BasisLandschaft
    {
        private string m_Name;
        private string m_Geländekundig;

        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }
        public string Geländekundig
        {
            get { return this.m_Geländekundig; }
            set {  this.m_Geländekundig = value;}
        }
    }

    public class LandschaftEis : BasisLandschaft
    {
        public LandschaftEis()
        {
            this.Name = "Eis";
            this.Geländekundig = "Eiskundig";
        }
    }

    public class LandschaftWuesteUndWuestenrand : BasisLandschaft
    {
        public LandschaftWuesteUndWuestenrand()
        {
            this.Name = "Wüste und Wüstenrand";
            this.Geländekundig = "Wüstenkundig";
        }
    }

    public class LandschaftGebirge : BasisLandschaft
    {
        public LandschaftGebirge()
        {
            this.Name = "Gebirge";
            this.Geländekundig = "Gebirgskundig";
        }
    }

    public class LandschaftHochland : BasisLandschaft
    {
        public LandschaftHochland()
        {
            this.Name = "Hochland";
            this.Geländekundig = "";
        }
    }

    public class LandschaftSteppe : BasisLandschaft
    {
        public LandschaftSteppe()
        {
            this.Name = "Steppe";
            this.Geländekundig = "Steppenkundig";
        }
    }

    public class LandschaftGraslandWiesen : BasisLandschaft
    {
        public LandschaftGraslandWiesen()
        {
            this.Name = "Grasland, Wiesen";
            this.Geländekundig = "";
        }
    }

    public class LandschaftFlussSeeuferTeiche : BasisLandschaft
    {
        public LandschaftFlussSeeuferTeiche()
        {
            this.Name = "Fluss- und Seeufer, Teiche";
            this.Geländekundig = "";
        }
    }

    public class LandschaftKuesteStrand : BasisLandschaft
    {
        public LandschaftKuesteStrand()
        {
            this.Name = "Küste, Strand";
            this.Geländekundig = "";
        }
    }

    public class LandschaftFlussauen : BasisLandschaft
    {
        public LandschaftFlussauen()
        {
            this.Name = "Flussauen";
            this.Geländekundig = "";
        }
    }

    public class LandschaftSumpfMoor : BasisLandschaft
    {
        public LandschaftSumpfMoor()
        {
            this.Name = "Sumpf und Moor";
            this.Geländekundig = "Sumpfkundig";
        }
    }

    public class LandschaftRegenwald : BasisLandschaft
    {
        public LandschaftRegenwald()
        {
            this.Name = "Regenwald";
            this.Geländekundig = "Dschungelkundig";
        }
    }

    public class LandschaftWald : BasisLandschaft
    {
        public LandschaftWald()
        {
            this.Name = "Wald";
            this.Geländekundig = "Waldkundig";
        }
    }

    public class LandschaftWaldrand : BasisLandschaft
    {
        public LandschaftWaldrand()
        {
            this.Name = "Waldrand";
            this.Geländekundig = "Waldkundig";
        }
    }

    public class LandschaftMeer : BasisLandschaft
    {
        public LandschaftMeer()
        {
            this.Name = "Meer";
            this.Geländekundig = "Meereskundig";
        }
    }

    public class LandschaftHoehleFeucht : BasisLandschaft
    {
        public LandschaftHoehleFeucht()
        {
            this.Name = "Höhle (feucht)"; 
            this.Geländekundig = "Höhlenkundig";
        }
    }

    public class LandschaftHoehleTrocken : BasisLandschaft
    {
        public LandschaftHoehleTrocken()
        {
            this.Name = "Höhle (trocken)";
            this.Geländekundig = "Höhlenkundig";
        }
    }
}
