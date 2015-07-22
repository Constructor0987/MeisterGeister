using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Karte.Logic
{
    public class Karte
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        int breite, höhe;

        public int Höhe
        {
            get { return höhe; }
            set { höhe = value; }
        }

        public int Breite
        {
            get { return breite; }
            set { breite = value; }
        }
        string bildpfad;

        public string Bildpfad
        {
            get { return bildpfad; }
            set { bildpfad = value; }
        }

        public Karte(string name, string bildpfad, int breite, int höhe)
        {
            Name = name;
            Bildpfad = bildpfad;
            Breite = breite;
            Höhe = höhe;
        }
    }
}
