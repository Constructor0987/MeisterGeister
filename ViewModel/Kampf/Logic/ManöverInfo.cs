using System;
using System.ComponentModel;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Base;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class ManöverInfo : ViewModelBase, IDisposable
    {
        #region Umwandeln

        private Base.CommandBase umwandeln;
        public Base.CommandBase Umwandeln
        {
            get
            {
                if (umwandeln == null)
                {
                    umwandeln = new CommandBase(o => ExecuteUmwandeln(), o => CanExecuteUmwandeln());
                }
                return umwandeln;
            }
        }

        private void ExecuteUmwandeln()
        {
            Manöver = new Manöver.Zauber(Manöver.Ausführender, null);
        }

        private bool CanExecuteUmwandeln()
        {
            return true;
        }

        #endregion

        #region Kampfaktionen

        [DependentProperty("Start")]
        public int IndexInKampfaktionen
        {
            get { return Kampf.InitiativListe.Aktionszeiten.Count(zeit => zeit < Start); }
        }

        [DependentProperty("Start")]
        [DependentProperty("End")]
        public int DauerInKampfaktionen
        {
            get
            {
                var aktionszeiten = Kampf.InitiativListe.Aktionszeiten;
                int dauer = aktionszeiten.Count(zeit => zeit >= Start && zeit <= End);
                return dauer;
            }
        }

        public void NotifyKampfaktionenChanged()
        {
            OnChanged("DauerInKampfaktionen");
            OnChanged("IndexInKampfaktionen");
        }

        #endregion

        #region Initiative

        private int iniModStart;
        public int InitiativeModStart
        {
            get
            {
                return iniModStart;
            }
            set
            {
                Set(ref iniModStart, value);
            }
        }

        private int kampfrundeStart;

        [DependentProperty("InitiativeModStart")]
        [DependentProperty("Manöver")]
        public ZeitImKampf Start
        {
            get
            {
                return new ZeitImKampf(kampfrundeStart, Manöver.Ausführender.InitiativeMitKommas - InitiativeModStart);
            }
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
                Set(ref iniModEnd, value);
            }
        }

        private int kampfrundeEnd;

        [DependentProperty("InitiativeModEnd")]
        [DependentProperty("Manöver")]
        public ZeitImKampf End
        {
            get
            {
                return new ZeitImKampf(kampfrundeEnd, Manöver.Ausführender.InitiativeMitKommas - InitiativeModEnd);
            }
        }

        private void BerechneEnd()
        {
            kampfrundeEnd = kampfrundeStart + (manöver.Dauer - (InitiativeModStart == 0 ? 1 : 0)) / 2;
            InitiativeModEnd = (InitiativeModStart + (manöver.Dauer % 2 == 0 ? 8 : 0)) % 16;
        }

        [DependentProperty("Start")]
        [DependentProperty("End")]
        public IEnumerable<ZeitImKampf> Aktionszeiten
        {
            get
            {
                ZeitImKampf zeit = Start;
                yield return zeit;
                while (zeit < End)
                {
                    if (zeit.InitiativPhase == Manöver.Ausführender.InitiativeMitKommas)
                        zeit.InitiativPhase -= 8;
                    else
                    {
                        zeit.Kampfrunde++;
                        zeit.InitiativPhase = Manöver.Ausführender.InitiativeMitKommas;
                    }
                    yield return zeit;
                }
            }
        }

        #endregion

        private Kampf kampf;
        public Kampf Kampf
        {
            get { return kampf; }
            private set
            {
                if (kampf != null)
                {
                    Kampf.PropertyChanged -= Kampf_PropertyChanged;
                    kampf.InitiativListe.CollectionChanged -= InitiativListe_CollectionChanged;
                }
                kampf = value;
                if (kampf != null)
                {
                    kampf.InitiativListe.CollectionChanged += InitiativListe_CollectionChanged;
                    Kampf.PropertyChanged += Kampf_PropertyChanged;
                }
                NotifyKampfaktionenChanged();
                OnChanged("Kampf");
            }
        }

        private void InitiativListe_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyKampfaktionenChanged();
        }

        private Manöver.Manöver manöver;
        public Manöver.Manöver Manöver
        {
            get { return manöver; }
            set
            {
                if (manöver != null)
                {
                    manöver.Ausführender.PropertyChanged -= Kämpfer_PropertyChanged;
                    manöver.PropertyChanged -= Manöver_PropertyChanged;
                }
                manöver = value;
                if (manöver != null)
                {
                    manöver.PropertyChanged += Manöver_PropertyChanged;
                    manöver.Ausführender.PropertyChanged += Kämpfer_PropertyChanged;
                    BerechneEnd();
                }

                OnChanged("Manöver");
            }
        }

        private void Manöver_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Dauer")
            {
                BerechneEnd();
            }
        }

        void manöver_OnAusführung(object sender)
        {
            //Ausgeführt = true;
        }

        [DependentProperty("Manöver")]
        public bool IsAktion
        {
            get { return !(Manöver is Manöver.KeineAktion); }
        }

        private CommandBase _ausführen;
        public CommandBase Ausführen
        {
            get { return _ausführen; }
        }

        public ManöverInfo(Manöver.Manöver m, int inimod, int kampfrunde)
        {
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
            _ausführen = new CommandBase(o => Ausgeführt = !Ausgeführt, null);
            if (m.Ausführender != null)
                m.Ausführender.PropertyChanged += Kämpfer_PropertyChanged;
            Kampf = m.Ausführender.Kampf;

            InitiativeModStart = inimod;
            kampfrundeStart = kampfrunde;
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
            set
            {
                if (ausgeführt == value)
                    return;
                ausgeführt = value;
                if (ausgeführt && Manöver != null)
                    Manöver.Ausführen();
                OnChanged("Ausgeführt");
            }
        }

        //private bool isSelected = false;
        //public bool IsSelected
        //{
        //    get { return isSelected; }
        //    set
        //    {
        //        isSelected = value;
        //        OnChanged("IsSelected");
        //    }
        //}

        public bool IsAktuell
        {
            get
            {
                return this == Kampf.AktuelleAktion;
            }
        }

        private void Kämpfer_PropertyChanged(object o, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Initiative")
            {
                OnChanged("Start");
                OnChanged("End");
                OnChanged("Aktionszeiten");
            }
            else if (args.PropertyName == "Angriffsaktionen")
                OnChanged("Angriffsaktionen");
        }

        private void Kampf_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AktuelleAktion")
                OnChanged("IsAktuell");
        }


        public void Dispose()
        {
            Kampf = null;
            Manöver = null;
        }
    }
}
