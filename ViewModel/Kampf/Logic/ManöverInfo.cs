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

        public int Index
        {
            //TODO: PropertyChanged
            get { return KämpferInfo.Kampf.InitiativListe.IndexOf(this); }
        }

        public int DauerInKampfaktionen
        {
            get
            {
                //TODO: Implementieren
                return Längerfristig ? 10 : 1;
            }
        }

        private int iniModStart;
        public int InitiativeModStart
        {
            get
            {
                return iniModStart;
            }
            set
            {
                iniModStart = value;
                OnChanged("InitiativeModStart");
                OnChanged("InitiativeStart");
            }
        }

        private int kampfrundeStart;

        public int KampfrundeStart
        {
            get
            {
                return kampfrundeStart;
            }
            set
            {
                kampfrundeStart = value;
                OnChanged("KampfrundeStart");
            }
        }

        public int InitiativeStart
        {
            get { return ((KämpferInfo == null) ? 0 : KämpferInfo.Initiative) + InitiativeModStart; }
        }


        private int iniModEnd;

        public int InitiativeModEnd
        {
            get
            {
                return iniModEnd;
            }
            set
            {
                iniModEnd = value;
                OnChanged("InitiativeModEnd");
                OnChanged("InitiativeEnd");
            }
        }

        private int kampfrundeEnd;

        public int KampfrundeEnd
        {
            get
            {
                return kampfrundeEnd;
            }
            set
            {
                kampfrundeEnd = value;
                OnChanged("KampfrundeEnd");
            }
        }

        public int InitiativeEnd
        {
            get { return ((KämpferInfo == null) ? 0 : KämpferInfo.Initiative) + InitiativeModEnd; }
        }

        public bool Längerfristig { get; set; }


        //public int InitiativeBasis
        //{
        //    get { return ((KämpferInfo == null) ? 0 : KämpferInfo.InitiativeBasis); }
        //}


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
                //if (manöver != null)
                //    manöver.OnAusführung -= manöver_OnAusführung;
                manöver = value;
                //if (manöver != null)
                //    manöver.OnAusführung += manöver_OnAusführung;
                OnChanged("Manöver"); OnChanged("IsAktion");
            }
        }

        void manöver_OnAusführung(object sender)
        {
            //Ausgeführt = true;
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

        private Base.CommandBase _ausführen;
        public Base.CommandBase Ausführen
        {
            get { return _ausführen; }
        }

        public ManöverInfo(KämpferInfo ki, Manöver.Manöver m, int inimod, int kampfrunde)
        {
            _ausführen = new Base.CommandBase(o => Ausgeführt = !Ausgeführt, null);
            KämpferInfo = ki;
            if (ki != null)
                ki.PropertyChanged += OnKämpferInfoChanged;
            InitiativeModStart = inimod;
            KampfrundeStart = kampfrunde;
            Manöver = m;
            Ausgeführt = false;
            Längerfristig = false;
        }

        private bool ausgeführt = false;
        /// <summary>
        /// Die Aktion wurde in dieser Kampfrunde ausgeführt. Das Setzen auf true reduziert die verbleibende Dauer.
        /// </summary>
        public bool Ausgeführt
        {
            get { return ausgeführt; }
            set {
                if (ausgeführt == value)
                    return;
                ausgeführt = value;
                if (ausgeführt && Manöver != null)
                    Manöver.Ausführen();
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

        public bool IsAktuell
        {
            get
            {
                if (KämpferInfo == null || KämpferInfo.Kampf == null)
                    return false;
                return this == KämpferInfo.Kampf.AktuelleAktion;
            }
        }

        private void OnKämpferInfoChanged(object o, System.ComponentModel.PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Initiative")
                OnChanged("InitiativeStart");
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
