using System;
using System.ComponentModel;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Base;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class ManöverInfo : ViewModelBase, IDisposable
    {
        #region Commands

        private Base.CommandBase umwandelnZauber;
        public Base.CommandBase UmwandelnZauber
        {
            get
            {
                if (umwandelnZauber == null)
                {
                    umwandelnZauber = new CommandBase(ExecuteUmwandelnZauber, null);
                }
                return umwandelnZauber;
            }
        }

        private void ExecuteUmwandelnZauber(object o)
        {
            if (o is Held_Zauber)
                Manöver = new Manöver.Zauber(Manöver.Ausführender, (Held_Zauber)o);
        }

        private Base.CommandBase umwandelnFernkampf;
        public Base.CommandBase UmwandelnFernkampf
        {
            get
            {
                if (umwandelnFernkampf == null)
                {
                    umwandelnFernkampf = new CommandBase(ExecuteUmwandelnFernkampf, null);
                }
                return umwandelnFernkampf;
            }
        }

        private void ExecuteUmwandelnFernkampf(object o)
        {
            Manöver = new Manöver.FernkampfManöver(Manöver.Ausführender);
        }


        private Base.CommandBase umwandelnAttacke;
        public Base.CommandBase UmwandelnAttacke
        {
            get
            {
                if (umwandelnAttacke == null)
                    umwandelnAttacke = new CommandBase(ExecuteUmwandelnAttacke, null);
                return umwandelnAttacke;
            }
        }

        private void ExecuteUmwandelnAttacke(object o)
        {
            Manöver = new Manöver.Attacke(Manöver.Ausführender);
        }



        private Base.CommandBase aktivieren;
        public Base.CommandBase Aktivieren
        {
            get
            {
                if (aktivieren == null)
                    aktivieren = new CommandBase(o => ToggleAktiv(), o => CanToggleAktiv());
                return aktivieren;
            }
        }

        public bool CanToggleAktiv()
        {
            if (Kampf.AktuelleAktionszeit == default(ZeitImKampf))
                return false;

            //Aufsparen
            if (Kampf.InitiativListe.Contains(this))
            {
                //Ein Manöver kann nur aufgespart werden wenn es dran ist
                //Längerfristige Aktionen können nicht aufgespart werden
                //Länger Zielen und Zauber bereithalten können erreicht werden indem die Aktionsdauer verändert wird
                return Start == End && Start == Kampf.AktuelleAktionszeit;
            }
            //Aktivieren
            else
            {
                //Aktivieren geht nicht wenn der Ausführende in der gleichen Aktion schon etwas anderes macht
                return !Manöver.Ausführender.AngriffsManöver.Any(m => m.Start <= Kampf.AktuelleAktionszeit && m.End >= Kampf.AktuelleAktionszeit);
            }
        }

        public void ToggleAktiv()
        {
            if (Kampf.InitiativListe.Contains(this))
            {
                Kampf.InitiativListe.Remove(this);
                Manöver.Ausführender.AbwehrManöver.Add(this);
            }
            else
            {
                Manöver.Ausführender.AbwehrManöver.Remove(this);
                kampfrundeStart = kampfrundeEnd = Kampf.AktuelleAktionszeit.Kampfrunde;
                Start = new ZeitImKampf(kampfrundeStart, Kampf.AktuelleAktionszeit.InitiativPhase);
                End = new ZeitImKampf(kampfrundeEnd, Kampf.AktuelleAktionszeit.InitiativPhase);
                Kampf.InitiativListe.Add(this);
            }
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
                var aktionszeiten = Kampf.InitiativListe.Aktionszeiten.Distinct();
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
                if (Manöver != null)
                    BerechneStart();
            }
        }

        private int kampfrundeStart;

        private ZeitImKampf start;
        public ZeitImKampf Start
        {
            get
            {
                return start;
            }
            set
            {
                Set(ref start, value);
            }
        }
        private void BerechneStart()
        {
            Start = new ZeitImKampf(kampfrundeStart, Manöver.Ausführender.InitiativeMitKommas - InitiativeModStart);
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
                BerechneEnd();
            }
        }

        private int kampfrundeEnd;

        private ZeitImKampf end;
        public ZeitImKampf End
        {
            get
            {
                return end;
            }
            set
            {
                Set(ref end, value);
            }
        }

        private void BerechneEnd()
        {
            kampfrundeEnd = kampfrundeStart + (manöver.Dauer - (InitiativeModStart == 0 ? 1 : 0)) / 2;
            iniModEnd = (InitiativeModStart + (manöver.Dauer % 2 == 0 ? 8 : 0)) % 16;
            End = new ZeitImKampf(kampfrundeEnd, Manöver.Ausführender.InitiativeMitKommas - InitiativeModEnd);
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
            Aktivieren.Invalidate();
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
                    BerechneStart();
                    BerechneEnd();
                }

                OnChanged("Manöver");
            }
        }

        private void Manöver_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Dauer")
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
                return Aktionszeiten.Contains(Kampf.AktuelleAktionszeit);
            }
        }

        private void Kämpfer_PropertyChanged(object o, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Initiative")
            {
                BerechneStart();
                BerechneEnd();
                OnChanged("Aktionszeiten");
            }
            else if (args.PropertyName == "Angriffsaktionen")
                OnChanged("Angriffsaktionen");
        }

        private void Kampf_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AktuelleAktion")
                OnChanged("IsAktuell");
            if (e.PropertyName == "AktuelleAktionszeit")
            {
                Aktivieren.Invalidate();
            }
        }


        public void Dispose()
        {
            Kampf = null;
            Manöver = null;
        }
    }
}
