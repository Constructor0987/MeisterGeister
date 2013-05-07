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
    public class Zutat
    {
        #region //---- FELDER ----

        private string _name;
        private int _anzahl;
        private string _einheit;
        //Commands

        #endregion

        #region //---- EIGENSCHAFTEN ----

        public string Name   {   get   {  return _name; }  }
        public int Anzahl { get { return _anzahl; } }
        public string Einheit { get { return _einheit; } }

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

 /*       public event PropertyChangedEventHandler PropertyChanged;

        private void OnChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
*/
        #endregion
    }

}
