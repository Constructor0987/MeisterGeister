using System;
using System.ComponentModel;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Base;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Kampf.Logic.Manöver;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class ManöverInfo : ViewModelBase, IDisposable
    {
        #region Umwandeln

        private bool CanExecuteUmwandelnAktion(object o)
        {
            return Manöver.Typ == Logic.Manöver.ManöverTyp.Aktion;
        }

        private bool CanExecuteUmwandelnReaktion(object o)
        {
            return Manöver.Typ == Logic.Manöver.ManöverTyp.Reaktion;
        }

        private Base.CommandBase umwandelnZauber;
        public Base.CommandBase UmwandelnZauber
        {
            get
            {
                if (umwandelnZauber == null)
                {
                    umwandelnZauber = new CommandBase(ExecuteUmwandelnZauber, CanExecuteUmwandelnAktion);
                }
                return umwandelnZauber;
            }
        }

        private void ExecuteUmwandelnZauber(object o)
        {
            if (o is Held_Zauber)
                Manöver = new Manöver.Zauber(Manöver.Ausführender, (Held_Zauber)o);
            if (o is GegnerBase_Zauber)
                Manöver = new Manöver.Zauber(Manöver.Ausführender, (GegnerBase_Zauber)o);
        }

        private Base.CommandBase umwandelnFernkampf;
        public Base.CommandBase UmwandelnFernkampf
        {
            get
            {
                if (umwandelnFernkampf == null)
                {
                    umwandelnFernkampf = new CommandBase(ExecuteUmwandelnFernkampf, CanExecuteUmwandelnAktion);
                }
                return umwandelnFernkampf;
            }
        }

        private void ExecuteUmwandelnFernkampf(object o)
        {
            Manöver = new Manöver.FernkampfManöver(Manöver.Ausführender, (IFernkampfwaffe)o);
        }


        private Base.CommandBase umwandelnAttacke;
        public Base.CommandBase UmwandelnAttacke
        {
            get
            {
                if (umwandelnAttacke == null)
                    umwandelnAttacke = new CommandBase(ExecuteUmwandelnAttacke, CanExecuteUmwandelnAktion);
                return umwandelnAttacke;
            }
        }

        private void ExecuteUmwandelnAttacke(object o)
        {
            Manöver = new Manöver.Attacke(Manöver.Ausführender);
        }

        private Base.CommandBase umwandelnSonstiges;
        public Base.CommandBase UmwandelnSonstiges
        {
            get
            {
                if (umwandelnSonstiges == null)
                    umwandelnSonstiges = new CommandBase(ExecuteUmwandelnSonstiges, CanExecuteUmwandelnAktion);
                return umwandelnSonstiges;
            }
        }

        private void ExecuteUmwandelnSonstiges(object o)
        {
            Manöver = new Manöver.SonstigesManöver(Manöver.Ausführender);
        }

        private Base.CommandBase umwandelnParade;
        public Base.CommandBase UmwandelnParade
        {
            get
            {
                if (umwandelnParade == null)
                    umwandelnParade = new CommandBase(ExecuteUmwandelnParade, CanExecuteUmwandelnReaktion);
                return umwandelnParade;
            }
        }

        private void ExecuteUmwandelnParade(object o)
        {
            Manöver = new Manöver.Parade(Manöver.Ausführender);
        }

        private Base.CommandBase umwandelnGezieltesAusweichen;
        public Base.CommandBase UmwandelnGezieltesAusweichen
        {
            get
            {
                if (umwandelnGezieltesAusweichen == null)
                    umwandelnGezieltesAusweichen = new CommandBase(ExecuteUmwandelnGezieltesAusweichen, CanExecuteUmwandelnReaktion);
                return umwandelnGezieltesAusweichen;
            }
        }

        private void ExecuteUmwandelnGezieltesAusweichen(object o)
        {
            Manöver = new Manöver.GezieltesAusweichen(Manöver.Ausführender);
        }

        #endregion

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
                //return Start == End && Start == Kampf.AktuelleAktionszeit;

                //Hinweis: Aufsparen ist immer aktiviert da bei Helden mit Aufmerksamkeit passieren kann dass Aktionen aufgespart werden, und anschließend umgewandelt wird
                return true;
                
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
                    //Bei Abwehrmanövern sollen sich die Aktionszeiten nicht ändern
                    if (!(Manöver is Manöver.AbwehrManöver))
                    {
                        BerechneStart();
                        BerechneEnd();
                    }
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

        private CommandBase _ausführen;
        public CommandBase Ausführen
        {
            get { return _ausführen; }
        }

        public ManöverInfo(Manöver.Manöver m, int inimod, int kampfrunde)
        {
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
            _ausführen = new CommandBase(o => Manöver.IsAusgeführt = !manöver.IsAusgeführt, null);
            if (m.Ausführender != null)
                m.Ausführender.PropertyChanged += Kämpfer_PropertyChanged;
            Kampf = m.Ausführender.Kampf;

            InitiativeModStart = inimod;
            kampfrundeStart = kampfrunde;
            Manöver = m;

            Manöver.IsAusgeführt = false;
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
            if (args.PropertyName == "Initiative" && Manöver.Typ == ManöverTyp.Aktion)
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
