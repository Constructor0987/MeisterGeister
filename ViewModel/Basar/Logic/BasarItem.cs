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

        private IHandelsgut _item = null;

        /// <summary>
        /// Eine Zusammenführung aller durchsuchbaren Felder.
        /// </summary>
        private string _suchtext = string.Empty;

        private Preis _preis;
        private double _rabattAufschlag = 0.0;
        private double _anzahl = 1.0;

        private double _währungsFaktor = 1.0;
        private string _währungsCode = "S";
        private string _währungsText = "Silbertaler";

        #endregion

        #region //---- EIGENSCHAFTEN ----

        public IHandelsgut Item 
        {
            get { return _item; }
            set
            {
                _item = value;
                _suchtext = Name.ToLower() + Kategorie.ToLower() + Tags.ToLower();
            }

        }

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
            get { return Model.Literatur.ReplaceAbkürzungen(Literatur); }
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

                Preis _preisWM = new Preis();

                if (WährungsFaktor != 0)
                {
                    _preisWM.ObererPreis = _preis.ObererPreis / WährungsFaktor;
                    _preisWM.UntererPreis = _preis.UntererPreis / WährungsFaktor;
                }
                else
                    _preisWM = _preis;

                _preisWM.WährungSuffix = _währungsCode;
                
                return _preisWM;
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
                OnChanged("RabattAufschlag");
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
                OnChanged("Anzahl");
            }
        }
        
        public double WährungsFaktor
        {
            get { return _währungsFaktor; }
            set
            {
                if (_währungsFaktor == value || value < 0.0)
                    return;
                _währungsFaktor = value;
                OnChanged("WährungsFaktor");

            }
        }

        public string WährungsCode
        {
            get { return _währungsCode; }
            set
            {
                _währungsCode = value;
            }
        }

        public string WährungsText
        {
            get { return _währungsText; }
            set
            {
                _währungsText = value;
            }
        }

        private bool _imGebietVorhanden;
        public bool ImGebietVorhanden
        {
            get { return _imGebietVorhanden; }
            set
            {
                _imGebietVorhanden = value;
                OnChanged("ImGebietVorhanden");
            }
        }

        public string VerbreitungsRegion
        {
            get
            {
                return  (Item is Model.Waffe)? (Item as Model.Waffe).Verbreitung ?? "nicht definiert" :
                        (Item is Model.Fernkampfwaffe) ? (Item as Model.Fernkampfwaffe).Verbreitung ?? "nicht definiert" :
                        (Item is Model.Rüstung) ? (Item as Model.Rüstung).Verbreitung?? "nicht definiert" :
                        (Item is Model.Schild) ? (Item as Model.Schild).Verbreitung ?? "nicht definiert" : 
                        "Überregional";                
            }
        }

        [DependentProperty("Preis"), DependentProperty("RabattAufschlag")]
        public Preis PreisMod
        {
            get
            {
                if (RabattAufschlag == 0)
                {
                    return Preis;
                }
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

        #endregion

        #region //---- COMMANDS ----

        private Base.CommandBase _onInventarAdd;
        public Base.CommandBase OnInventarAdd
        {
            get 
            { 
                if (_onInventarAdd == null)
                    _onInventarAdd = new Base.CommandBase(InventarAdd, null);
                return _onInventarAdd; 
            }
        }

        private Base.CommandBase _onFilterKategorie;
        public Base.CommandBase OnFilterKategorie
        {
            get
            {
                if (_onFilterKategorie == null)
                    _onFilterKategorie = new Base.CommandBase(FilterKategorie, null);
                return _onFilterKategorie;
            }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public BasarItem()
        {
            // Event-Handler zur DependentProperty-Notification
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
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
            return _suchtext.Contains(suchWort);
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

        private void InventarAdd(object sender)
        {
            if (InventarAddEvent != null)
                InventarAddEvent(this, new EventArgs());
        }

        public event EventHandler InventarAddEvent;


        private void FilterKategorie(object obj)
        {
            if (FilterKategorieEvent != null)
                FilterKategorieEvent(this, new EventArgs());
        }

        public event EventHandler FilterKategorieEvent;

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
