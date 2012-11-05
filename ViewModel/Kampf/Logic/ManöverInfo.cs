using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class ManöverInfo : INotifyPropertyChanged, IDisposable
    {
        private KämpferInfo _kämpferInfo;

        public KämpferInfo KämpferInfo
        {
            get { return _kämpferInfo; }
            private set { _kämpferInfo = value; }
        }

        private int _inimod;
        public int InitiativeMod
        {
            get
            {
                return _inimod;
            }
            set
            {
                _inimod = value;
                OnChanged("Initiative");
            }
        }

        public int Initiative
        {
            get { return ((KämpferInfo == null) ? 0 : KämpferInfo.Initiative) + InitiativeMod; }
        }
        public int InitiativeBasis
        {
            get { return ((KämpferInfo == null) ? 0 : KämpferInfo.InitiativeBasis); }
        }
        public string KämpferName
        {
            get { return ((KämpferInfo == null) ? "Effekt" : KämpferInfo.Kämpfer.Name); }
        }


        private Manöver.Manöver manöver;
        public Manöver.Manöver Manöver
        {
            get { return manöver; }
            set
            {
                if (manöver != null)
                    manöver.OnAusführung -= manöver_OnAusführung;
                manöver = value;
                if (manöver != null)
                    manöver.OnAusführung += manöver_OnAusführung;
                OnChanged("Manöver"); OnChanged("IsAktion");
            }
        }

        void manöver_OnAusführung(object sender)
        {
            Ausgeführt = true;
        }

        /// <summary>
        /// Ein Hack um zwei Anzeigen in der InitiativListe (TreeView) zu bekommen.
        /// </summary>
        public ICollection<ManöverInfo> ThisAsList
        {
            get
            {
                return new List<ManöverInfo>() { this };
            }
        }

        public bool IsAktion
        {
            get { return !(Manöver is Manöver.KeineAktion); }
        }

        public ManöverInfo(KämpferInfo ki, Manöver.Manöver m, int inimod)
        {
            KämpferInfo = ki;
            if (ki != null)
                ki.PropertyChanged += OnKämpferInfoChanged;
            InitiativeMod = inimod;
            Manöver = m;
            Ausgeführt = false;
        }

        private bool ausgeführt = false;
        /// <summary>
        /// Die Aktion wurde in dieser Kampfrunde ausgeführt. Das Setzen auf true reduziert die verbleibende Dauer.
        /// </summary>
        public bool Ausgeführt
        {
            get { return ausgeführt; }
            //sollte von einem Manöver-Event OnAusführung gesetzt werden
            set {
                ausgeführt = value;
                OnChanged("Ausgeführt"); 
            }
        }

        private bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnChanged("IsSelected");
            }
        }


        private void OnKämpferInfoChanged(object o, System.ComponentModel.PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Initiative")
                OnChanged("Initiative");
            else if (args.PropertyName == "Angriffsaktionen")
                OnChanged("Angriffsaktionen");
            else if (args.PropertyName == "Name")
                OnChanged("KämpferName");
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        public void Dispose()
        {
            if (KämpferInfo != null)
                KämpferInfo.PropertyChanged -= OnKämpferInfoChanged;
        }
    }
}
