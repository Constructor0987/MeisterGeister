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
        private double _anzahl;
        private string _einheit;
        private Substitution _substitution = Substitution.gleichwertig;
        private int mod = 0;
        //Commands

        #endregion

        #region //---- EIGENSCHAFTEN ----

        public string Name   {   get   {  return _name; }  }
        public double Anzahl { get { return _anzahl; } }
        public string Einheit { get { return _einheit; } }

        public int Mod
        {
            get { return mod; }
            set
            {
                mod = value;
                OnChanged("Mod");
            }
        }
        
        public Substitution Substitution
        {
            get { return _substitution; }
            set { 
                _substitution = value;
                switch (value)
                {
                    case Substitution.optimierend:
                        Mod = -3;
                        break;
                    case Substitution.gleichwertig:
                        Mod = 0;
                        break;
                    case Substitution.sinnvoll:
                        Mod = +3;
                        break;
                    case Substitution.möglich:
                        Mod = +6;
                        break;
                    default:
                        Mod = 0;
                        break;
                }
                OnChanged("Substitution"); 
            }
        }

        //Commands

        #endregion

        #region //---- KONSTRUKTOR ----

        public Zutat(double anzahl, string einheit, string name )
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
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }

}
