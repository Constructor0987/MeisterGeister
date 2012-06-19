using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using MeisterGeister.Model.Extensions;
// Eigene Usings

namespace MeisterGeister.ViewModel.Basar.Logic
{
    public class BasarItem : INotifyPropertyChanged
    {
        #region //---- FELDER ----

        private Preis _preis;
        private double _rabattAufschlag = 0.0;
        private double _anzahl = 1.0;

        //Commands
        private Base.CommandBase _onInventarAdd;

        #endregion

        #region //---- EIGENSCHAFTEN ----

        public IHandelsgut Item { get; set; }

        public Type ItemType 
        {
            get { return Item == null ? null : Item.GetType(); }
        }

        public string Name
        {
            get { return Item == null || Item.Name == null ? string.Empty : Item.Name; }
        }

        public string Kategorie
        {
            get { return Item == null || Item.Kategorie == null ? string.Empty : Item.Kategorie; }
        }

        public string Tags
        {
            get { return Item == null || Item.Tags == null ? string.Empty : Item.Tags; }
        }

        public double? Gewicht
        {
            get { return Item == null ? null : Item.Gewicht; }
        }

        public string ME_Text
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Item.ME))
                    return "pro " + Item.ME;
                else
                    return Item.ME;
            }
        }

        public string Bemerkung
        {
            get { return Item == null || Item.Bemerkung == null ? string.Empty : Item.Bemerkung; }
        }

        public string Literatur
        {
            get { return Item == null || Item.Literatur == null ? string.Empty : Item.Literatur; }
        }

        public string LiteraturLang
        {
            get { return MeisterGeister.Logic.General.Literatur.ReplaceAbkürzungen(Literatur); }
        }

        // Wird zum Ein-/Ausblendung in der GUI benötigt.
        public bool HatBemerkung
        {
            get { return !string.IsNullOrEmpty(Bemerkung); }
        }

        public Preis Preis
        {
            get
            {
                if (_preis == null)
                    _preis = new Preis(Item == null ? string.Empty : Item.Preis);
                return _preis;
            }
        }

        public double RabattAufschlag
        {
            get { return _rabattAufschlag; }
            set
            {
                if (_rabattAufschlag == value)
                    return;
                _rabattAufschlag = value;
                NotifyPropertyChanged("RabattAufschlag");
            }
        }

        public double Anzahl
        {
            get { return _anzahl; }
            set
            {
                if (_anzahl == value || value < 0.0)
                    return;
                _anzahl = value;
                NotifyPropertyChanged("Anzahl");
            }
        }

        [DependentProperty("Preis"), DependentProperty("RabattAufschlag")]
        public Preis PreisMod
        {
            get
            {
                if (RabattAufschlag == 0)
                    return Preis;
                return Preis * (RabattAufschlag / 100.0 + 1.0);
            }
        }

        [DependentProperty("Anzahl"), DependentProperty("PreisMod")]
        public Preis PreisGesamt
        {
            get
            {
                if (Anzahl == 1)
                    return PreisMod;
                return PreisMod * Anzahl;
            }
        }

        //Commands
        public Base.CommandBase OnInventarAdd
        {
            get { return _onInventarAdd; }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public BasarItem()
        {
            // Event-Handler zur DependentProperty-Notification
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;

            _onInventarAdd = new Base.CommandBase(InventarAdd, null);
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        /// <summary>
        /// Prüft, ob 'suchWort' im Namen, der Kategorie oder in den Tags vorkommt.
        /// </summary>
        /// <param name="suchWort"></param>
        /// <returns></returns>
        public bool Contains(string suchWort)
        {
            return Name.ToLower().Contains(suchWort)
                || Kategorie.ToLower().Contains(suchWort)
                || Tags.ToLower().Contains(suchWort);
        }

        /// <summary>
        /// Prüft, ob die 'suchWorte' im Namen, der Kategorie oder in den Tags vorkommt.
        /// Es wird dabei eine UND-Prüfung durchgeführt.
        /// </summary>
        /// <param name="suchWorte"></param>
        /// <returns></returns>
        public bool Contains(string[] suchWorte)
        {
            foreach (string wort in suchWorte)
            {
                if (!Contains(wort))
                    return false;
            }
            return true;
        }

        void InventarAdd(object sender)
        {
            // TODO ??: Insert ins Helden-Inventar
        }

        #endregion

        #region //---- INotifyPropertyChanged Member ----

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }

}
