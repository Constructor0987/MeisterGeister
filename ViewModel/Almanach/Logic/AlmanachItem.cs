using MeisterGeister.Model.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel.Almanach.Logic
{
    public class AlmanachItem : INotifyPropertyChanged
    {
        #region //---- FELDER ----

        private dynamic _item = null;
        private string _kategorie = string.Empty;

        /// <summary>
        /// Eine Zusammenführung aller durchsuchbaren Felder.
        /// </summary>
        private string _suchtext = string.Empty;

        #endregion

        #region //---- EIGENSCHAFTEN ----

        public dynamic Item
        {
            get { return _item; }
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
            set { _kategorie = value; }
            get { return _kategorie == null ? string.Empty : _kategorie; }
        }

        public string Tags
        {
            get { return Item == null || Item.Tags == null ? string.Empty : Item.Tags; }
        }

        public string Literatur
        {
            get { return Item == null || Item.Literatur == null ? string.Empty : Item.Literatur; }
        }

        public string LiteraturLang
        {
            get { return Model.Literatur.ReplaceAbkürzungen(Literatur); }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public AlmanachItem(dynamic item, string kat)
        {
            // Event-Handler zur DependentProperty-Notification
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;

            _item = item;
            _kategorie = kat;
            _suchtext = Name.ToLower() + Kategorie.ToLower();// + Tags.ToLower();
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
