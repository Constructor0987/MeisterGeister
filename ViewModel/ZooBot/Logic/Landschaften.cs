using System;
using System.Collections.Generic;
using System.Text;

namespace MeisterGeister.ViewModel.ZooBot.Logic.Landschaften
{
    public abstract class BasisLandschaft
    {
        private string m_Name;
        private string m_Gel�ndekundig;

        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }
        public string Gel�ndekundig
        {
            get { return this.m_Gel�ndekundig; }
            set {  this.m_Gel�ndekundig = value;}
        }
    }

    public class LandschaftEis : BasisLandschaft
    {
        public LandschaftEis()
        {
            this.Name = "Eis";
            this.Gel�ndekundig = "Eiskundig";
        }
    }

    public class LandschaftWuesteUndWuestenrand : BasisLandschaft
    {
        public LandschaftWuesteUndWuestenrand()
        {
            this.Name = "W�ste und W�stenrand";
            this.Gel�ndekundig = "W�stenkundig";
        }
    }

    public class LandschaftGebirge : BasisLandschaft
    {
        public LandschaftGebirge()
        {
            this.Name = "Gebirge";
            this.Gel�ndekundig = "Gebirgskundig";
        }
    }

    public class LandschaftHochland : BasisLandschaft
    {
        public LandschaftHochland()
        {
            this.Name = "Hochland";
            this.Gel�ndekundig = "";
        }
    }

    public class LandschaftSteppe : BasisLandschaft
    {
        public LandschaftSteppe()
        {
            this.Name = "Steppe";
            this.Gel�ndekundig = "Steppenkundig";
        }
    }

    public class LandschaftGraslandWiesen : BasisLandschaft
    {
        public LandschaftGraslandWiesen()
        {
            this.Name = "Grasland, Wiesen";
            this.Gel�ndekundig = "";
        }
    }

    public class LandschaftFlussSeeuferTeiche : BasisLandschaft
    {
        public LandschaftFlussSeeuferTeiche()
        {
            this.Name = "Fluss- und Seeufer, Teiche";
            this.Gel�ndekundig = "";
        }
    }

    public class LandschaftKuesteStrand : BasisLandschaft
    {
        public LandschaftKuesteStrand()
        {
            this.Name = "K�ste, Strand";
            this.Gel�ndekundig = "";
        }
    }

    public class LandschaftFlussauen : BasisLandschaft
    {
        public LandschaftFlussauen()
        {
            this.Name = "Flussauen";
            this.Gel�ndekundig = "";
        }
    }

    public class LandschaftSumpfMoor : BasisLandschaft
    {
        public LandschaftSumpfMoor()
        {
            this.Name = "Sumpf und Moor";
            this.Gel�ndekundig = "Sumpfkundig";
        }
    }

    public class LandschaftRegenwald : BasisLandschaft
    {
        public LandschaftRegenwald()
        {
            this.Name = "Regenwald";
            this.Gel�ndekundig = "Dschungelkundig";
        }
    }

    public class LandschaftWald : BasisLandschaft
    {
        public LandschaftWald()
        {
            this.Name = "Wald";
            this.Gel�ndekundig = "Waldkundig";
        }
    }

    public class LandschaftWaldrand : BasisLandschaft
    {
        public LandschaftWaldrand()
        {
            this.Name = "Waldrand";
            this.Gel�ndekundig = "Waldkundig";
        }
    }

    public class LandschaftMeer : BasisLandschaft
    {
        public LandschaftMeer()
        {
            this.Name = "Meer";
            this.Gel�ndekundig = "Meereskundig";
        }
    }

    public class LandschaftHoehleFeucht : BasisLandschaft
    {
        public LandschaftHoehleFeucht()
        {
            this.Name = "H�hle (feucht)"; 
            this.Gel�ndekundig = "H�hlenkundig";
        }
    }

    public class LandschaftHoehleTrocken : BasisLandschaft
    {
        public LandschaftHoehleTrocken()
        {
            this.Name = "H�hle (trocken)";
            this.Gel�ndekundig = "H�hlenkundig";
        }
    }
}
