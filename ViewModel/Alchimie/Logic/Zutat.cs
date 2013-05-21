using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using MeisterGeister.Model.Extensions;
// Eigene Usings

namespace MeisterGeister.ViewModel.Alchimie.Logic
{
    public class Zutat : INotifyPropertyChanged
    {
        #region //---- FELDER ----

        private string _name;
        private int _anzahl;
        private string _einheit;
        private Substitution _substitution = Substitution.gleichwertig;
        public string mod = "0";
        //Commands

        #endregion

        #region //---- EIGENSCHAFTEN ----

        public string Name   {   get   {  return _name; }  }
        public int Anzahl { get { return _anzahl; } }
        public string Einheit { get { return _einheit; } }
        
        public Substitution Substitution
        {
            get { return _substitution; }
            set { _substitution = value; OnChanged("Substitution"); }
        }

        //Commands

        #endregion

        #region //---- KONSTRUKTOR ----

        public Zutat(int anzahl, string einheit, string name )
        {
            _anzahl = anzahl;
            _einheit = einheit;
            _name = name;
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        #endregion

        #region //---- INotifyPropertyChanged Member ----

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChanged(String info)
        {
            if (PropertyChanged != null)
            {
                int n =0;
                switch (Substitution.ToString())
                {
                    case "optimierend": n = -3; mod = n.ToString(); break;
                    case "gleichwertig": n = 0; mod = n.ToString(); break;
                    case "sinnvoll": n = +3; mod = n.ToString(); break;
                    case "möglich": n = +6; mod = n.ToString(); break;
                    case "unsinnig": mod = "nicht möglich"; break;
                    default: n = 0; mod = n.ToString(); break;
                }
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }

}
