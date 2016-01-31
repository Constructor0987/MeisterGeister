using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using MeisterGeister.Logic.Extensions;
using System.Collections.Specialized;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Kampf.Logic.Manöver;
using System.Collections.ObjectModel;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class KämpferInfo : INotifyPropertyChanged, IDisposable
    {
        private IKämpfer _kämpfer;

        public IKämpfer Kämpfer
        {
            get { return _kämpfer; }
            private set
            {
                _kämpfer = value; AktionenBerechnen();
                OnChanged("Kämpfer");
            }
        }

        #region Initiative

        private decimal Kommastellen(int initiative)
        {
            decimal kommas = initiative;
            decimal random = new Random(GetHashCode()).Next(0, 1000);
            return kommas / 100 + random / 100000;
        }

        public decimal InitiativeMitKommas
        {
            get
            {
                return Initiative + Kommastellen(InitiativeBasis);
            }
        }

        private int _initiative;
        public int Initiative
        {
            get
            {
                return _initiative;
            }
            set
            {
                _initiative = value;
                int bonus = Math.Max((int)Math.Floor((_initiative - 11) / 10.0), 0);
                FreieAktionen = 2 + bonus;
                Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.PABonusDurchHoheIni);
                if (bonus > 0)
                    Kämpfer.Modifikatoren.Add(new Mod.PABonusDurchHoheIni(bonus));
                //Wenn INI < 0 -> Kämpfer verliert eine (Angriffs)Aktion
                AktionenBerechnen();
                OnChanged("Angriffsaktionen"); OnChanged("Abwehraktionen"); OnChanged("Aktionen");
                OnChanged("Initiative");
            }
        }
        public int InitiativeBasis
        {
            get { return Kämpfer.InitiativeBasis; }
        }

        public int InitiativeWurf
        {
            get { return Kämpfer.InitiativeWurf; }
        }
        #endregion

        private int _team;
        public int Team
        {
            get { return _team; }
            set
            {
                _team = value;
                OnChanged("Team");
                //ZLevel für den Bodenplan abhängig vom Team setzen und aktualisieren.
                if (Kämpfer != null)
                {
                    var w = Kämpfer as Wesen;
                    if (w != null)
                        w.ZLevel = 100 + _team;
                }
            }
        }

        public int Index
        {
            get
            {
                return Kampf.Kämpfer.IndexOf(this);
            }
        }

        private Kampf kampf;
        public Kampf Kampf
        {
            get { return kampf; }
            set { kampf = value; OnChanged("Kampf"); }
        }

        public bool IsAktuell
        {
            get
            {
                return this == Kampf.AktuelleAktion.Manöver.Ausführender;
            }
        }

        public KämpferInfo(IKämpfer k, Kampf kampf)
        {
            if (k == null)
                throw new ArgumentNullException("IKämpfer k darf nicht null sein.");
            if (kampf == null)
                throw new ArgumentNullException("Kampf kampf darf nicht null sein.");
            Kämpfer = k;
            Kampf = kampf;
            Kämpfer.PropertyChanged += Kämpfer_PropertyChanged;
            Kampf.PropertyChanged += Kampf_PropertyChanged;
            Team = 1;
            Initiative = k.Initiative();
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
            PropertyChanged += KämpferInfo_PropertyChanged;
        }

        private void Kampf_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AktuelleAktion")
                OnChanged("IsAktuell");
        }

        private void KämpferInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Angriffsaktionen")
                StandardAktionenSetzen(Kampf.Kampfrunde);
        }

        private void Kämpfer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "InitiativeBasis" || e.PropertyName == "InitiativeWurf")
            {
                Initiative = InitiativeBasis + InitiativeWurf;
                OnChanged(e.PropertyName);
            }
        }

        #region Aktionen
        private int _aktionen = 2;
        public int Aktionen
        {
            get
            {
                return _aktionen;
            }
            private set
            {
                if (value < 0)
                    value = 0;
                if (_aktionen == value)
                    return;
                _aktionen = value;
                if (_aktionen < Abwehraktionen + Angriffsaktionen)
                {
                    if (Abwehraktionen + Angriffsaktionen == 0)
                        Angriffsaktionen = 0;
                    else
                        Angriffsaktionen = (int)Math.Round(Math.Min((double)Angriffsaktionen / (double)(Abwehraktionen + Angriffsaktionen), 1) * Aktionen, MidpointRounding.AwayFromZero);
                }
                //OnChanged("Aktionen"); absichtlich nicht.
            }
        }

        private int _freieAktionen = 2;
        public int FreieAktionen
        {
            get
            {
                return _freieAktionen;
            }
            private set
            {
                _freieAktionen = value;
                OnChanged("FreieAktionen");
            }
        }

        private int _angriffsaktionen = 1;
        public int Angriffsaktionen
        {
            get { return _angriffsaktionen; }
            set
            {
                if (value < 0)
                    value = 0;
                if (value > Aktionen)
                    value = Aktionen;
                _angriffsaktionen = value;
                _abwehraktionen = Aktionen - _angriffsaktionen;
                AktionenBerechnen();
                _abwehraktionen = Aktionen - _angriffsaktionen;
                OnChanged("Abwehraktionen"); OnChanged("Angriffsaktionen"); OnChanged("Aktionen");
            }
        }

        private int _abwehraktionen = 1;
        public int Abwehraktionen
        {
            get { return _abwehraktionen; }
            set
            {
                if (value < 0)
                    value = 0;
                if (value > Aktionen)
                    value = Aktionen;
                _abwehraktionen = value;
                _angriffsaktionen = Aktionen - _abwehraktionen;
                AktionenBerechnen();
                _angriffsaktionen = Aktionen - _abwehraktionen;
                OnChanged("Abwehraktionen"); OnChanged("Angriffsaktionen"); OnChanged("Aktionen");
            }
        }

        private int _verbrauchteAngriffsaktionen = 0;
        public int VerbrauchteAngriffsaktionen
        {
            get { return _verbrauchteAngriffsaktionen; }
            set { _verbrauchteAngriffsaktionen = value; OnChanged("VerbrauchteAngriffsaktionen"); }
        }

        [DependentProperty("VerbrauchteAngriffsaktionen"), DependentProperty("Angriffsaktionen")]
        public int AngriffsaktionenÜbrig
        {
            get { return Angriffsaktionen - VerbrauchteAngriffsaktionen; }
        }

        private int _verbrauchteAbwehraktionen = 0;
        public int VerbrauchteAbwehraktionen
        {
            get { return _verbrauchteAbwehraktionen; }
            set { _verbrauchteAbwehraktionen = value; OnChanged("VerbrauchteAbwehraktionen"); }
        }

        [DependentProperty("VerbrauchteAbwehraktionen"), DependentProperty("Abwehraktionen")]
        public int AbwehraktionenÜbrig
        {
            get { return Abwehraktionen - VerbrauchteAbwehraktionen; }
        }

        private int _verbrauchteFreieAktionen = 0;
        public int VerbrauchteFreieAktionen
        {
            get { return _verbrauchteFreieAktionen; }
            set { _verbrauchteFreieAktionen = value; OnChanged("VerbrauchteFreieAktionen"); }
        }

        [DependentProperty("VerbrauchteFreieAktionen"), DependentProperty("FreieAktionen")]
        public int FreieAktionenÜbrig
        {
            get { return FreieAktionen - VerbrauchteFreieAktionen; }
        }

        public void AktionenBerechnen()
        {
            int aktionen = 2;
            if (Initiative < 0)
            {
                aktionen = 1;
            }
            var m = AngriffsManöver.Where(mi => mi.Manöver.VerbleibendeDauer >= 2).OrderBy(mi => mi.Start).FirstOrDefault();
            if (m != null) //wenn man eine LängerfristigeHandlung Dauer >= 2 ausführt, dann 
            {
                if (m.InitiativeModStart == 0)
                {
                    //als erste Aktion: Angriffsaktionen = Math.Min(Math.Max(VerbleibendeDauer, 2), Aktionen)
                    Aktionen = aktionen;
                    _angriffsaktionen = Math.Min(2, Aktionen);
                    _abwehraktionen = 0;
                    return;
                }
                //als zweite Aktion: Abwehraktionen = 0
                _abwehraktionen = 0;
            }

            bool parierwaffenII = false; bool todVonLinks = false;
            if (Kämpfer is Model.Gegner)
            {
                Aktionen = (Kämpfer as Model.Gegner).Aktionen;
                if (Initiative < 0)
                    Aktionen--;
                if (Aktionen != Abwehraktionen + Angriffsaktionen)
                {
                    if (Abwehraktionen + Angriffsaktionen == 0)
                    {
                        _angriffsaktionen = 0; _abwehraktionen = Aktionen;
                    }
                    else
                    {
                        _angriffsaktionen = (int)Math.Round(Math.Min((double)Angriffsaktionen / (double)(Abwehraktionen + Angriffsaktionen), 1) * Aktionen, MidpointRounding.AwayFromZero);
                        _abwehraktionen = Aktionen - _angriffsaktionen;
                    }
                }
                return;
            }
            //TODO JT: Sicherstellen, dass auch zwei Waffen geführt werden
            else if (Kampfstil == Kampfstil.BeidhändigerKampf && ((Kämpfer is Model.Held) && (Kämpfer as Model.Held).HatSonderfertigkeitUndVoraussetzungen("Beidhändiger Kampf II")))
            {
                if (Initiative < 0) //ini < 0: 2 Paraden, 2 akt
                {
                    Aktionen = 2;
                    _abwehraktionen = 2;
                }
                else
                    Aktionen = 3;
                if (Initiative < 4) //ini < 4: min 2 paraden, 3 akt
                    _abwehraktionen = Math.Max(Abwehraktionen, 2);
                if (Initiative < 8) //ini < 8: min 1 parade, 3 akt
                    _abwehraktionen = Math.Max(Abwehraktionen, 1);

                if (Abwehraktionen + Angriffsaktionen != Aktionen)
                    _angriffsaktionen = Aktionen - Abwehraktionen;

            }
            else if (Kampfstil == Kampfstil.Schildkampf && ((Kämpfer is Model.Held) && (Kämpfer as Model.Held).HatSonderfertigkeitUndVoraussetzungen("Schildkampf II") && Abwehraktionen >= 1))
            {
                if (Angriffsaktionen >= 2 && Initiative >= 8)
                    Aktionen = 2; //danach ist es automatisch 2/0
                else
                    Aktionen = 3; //hier 1/2

                if (Initiative < 0) //ini < 0: 2 Paraden, 2 akt
                {
                    Aktionen = 2;
                    _angriffsaktionen = 0;
                }
                else if (Initiative < 8) //ini < 8: min 2 paraden, 3 akt
                {
                    Aktionen = 3;
                    _abwehraktionen = Math.Max(2, Abwehraktionen);
                    _angriffsaktionen = Aktionen - Abwehraktionen;
                }

                if (Abwehraktionen + Angriffsaktionen != Aktionen)
                    _abwehraktionen = Aktionen - Angriffsaktionen;
            }
            else if (Kampfstil == Kampfstil.Parierwaffenstil && (Kämpfer is Model.Held) && (((parierwaffenII = (Kämpfer as Model.Held).HatSonderfertigkeitUndVoraussetzungen("Parierwaffen II")) && Abwehraktionen >= 1) || ((todVonLinks = (Kämpfer as Model.Held).HatSonderfertigkeitUndVoraussetzungen("Tod von Links")) && Angriffsaktionen >= 1)))
            {
                Aktionen = 3;
                //ini < 0: 2 Paraden, 2 akt
                if (Initiative < 0)
                {
                    if (parierwaffenII)
                        Aktionen = 2;
                    else
                        Aktionen = 1;
                    _abwehraktionen = Aktionen;
                    _angriffsaktionen = 0;
                }
                else if (Initiative < 8) //ini < 8: min 1 parade, 2 oder 3 akt
                {
                    if (!parierwaffenII)
                        Aktionen = 2;
                    _abwehraktionen = Math.Max(parierwaffenII ? 2 : 1, Abwehraktionen);
                    _angriffsaktionen = Aktionen - Abwehraktionen;
                }
                if (Aktionen == 3 && Abwehraktionen == 0) // tod von links darf nur verwendet werden, wenn nicht umgewandelt wurde
                    Aktionen = 2;
                if (Abwehraktionen + Angriffsaktionen != Aktionen)
                    if (parierwaffenII)
                        _abwehraktionen = Aktionen - Angriffsaktionen;
                    else
                        _angriffsaktionen = Aktionen - Abwehraktionen;
            }
            else
            {
                Aktionen = 2;
                if (Initiative < 0) //ini < 0: 1 Parade, 1 akt
                {
                    Aktionen = 1;
                    _abwehraktionen = 1;
                }
                else if (Initiative < 8) //ini < 8: min 1 parade
                    _abwehraktionen = Math.Max(1, Abwehraktionen);
                if (Abwehraktionen + Angriffsaktionen != Aktionen)
                {
                    _abwehraktionen = 1;
                    _angriffsaktionen = Aktionen - Abwehraktionen;
                }
            }
            //TODO JT: Myranor: Mehrhändig hinzufügen sicherstellen, dass auch entsprechend viele Waffen geführt werden
        }

        //TODO Kampfstile in Kämpfer verschieben
        private Kampfstil _kampfstil;
        public Kampfstil Kampfstil
        {
            get { return _kampfstil; }
            set
            {
                if (_kampfstil == value)
                    return;
                _kampfstil = value;
                AktionenBerechnen();
                OnChanged("Kampfstil");
                OnChanged("Abwehraktionen"); OnChanged("Angriffsaktionen"); OnChanged("Aktionen");
            }
        }

        private WaffenloserKampfstil _waffenloserKampfstil;
        public WaffenloserKampfstil WaffenloserKampfstil
        {
            get { return _waffenloserKampfstil; }
            set { _waffenloserKampfstil = value; }
        }

        public IEnumerable<ManöverInfo> AngriffsManöver
        {
            get
            {
                if (Kampf == null)
                    return new List<ManöverInfo>();
                return Kampf.InitiativListe.Where(mi => mi.Manöver.Ausführender == this).OrderByDescending(mi => mi.Start);
            }
        }

        public IEnumerable<ZeitImKampf> Aktionszeiten
        {
            get
            {
                //TODO: PropertyChanged
                return AngriffsManöver.SelectMany(mi => mi.Aktionszeiten);
            }
        }

        private ObservableCollection<ManöverInfo> _abwehrManöver = new ObservableCollection<ManöverInfo>();
        public ObservableCollection<ManöverInfo> AbwehrManöver
        {
            get
            {
                return _abwehrManöver;
            }
        }

        private void DeleteManöver(ref List<ManöverInfo> geplanteAktionen)
        {
            //manöver mit negativer INI löschen
            foreach (var mi in AngriffsManöver.Where(mi => mi.Start.InitiativPhase < 0 && mi.InitiativeModStart != 0).ToList())
                Kampf.InitiativListe.Remove(mi);

            var ersterAngriff = AngriffsManöver.Where(mi => mi.Manöver is Angriffsaktion).FirstOrDefault();
            //löschen von Manövern, für die der falsche Kampfstil gewählt ist.
            if (Kampfstil != Kampfstil.BeidhändigerKampf) //oder mehrhändig
            {
                foreach (var mi in AngriffsManöver.Where(mi => mi.Manöver is ZusätzlicheAngriffsaktion).ToList())
                    Kampf.InitiativListe.Remove(mi);
            }
            else
            {
                //ZusätzlicheAngriffsaktion löschen, für die die Bedingungen nicht erfüllt sind
                foreach (var mi in AngriffsManöver.Where(mi => mi.Manöver is ZusätzlicheAngriffsaktion && (ersterAngriff == null || mi.Start.InitiativPhase > ersterAngriff.Start.InitiativPhase - 4)).ToList())
                    Kampf.InitiativListe.Remove(mi);
            }
            if (Kampfstil != Kampfstil.Parierwaffenstil)
            {
                foreach (var mi in AngriffsManöver.Where(mi => mi.Manöver is TodVonLinks).ToList())
                    Kampf.InitiativListe.Remove(mi);
            }
            else
            {
                //TodVonLinks löschen, für die die Bedingungen nicht erfüllt sind
                foreach (var mi in AngriffsManöver.Where(mi => mi.Manöver is TodVonLinks && (Abwehraktionen != 1 || ersterAngriff == null || mi.Start.InitiativPhase > ersterAngriff.Start.InitiativPhase - 8)).ToList())
                    Kampf.InitiativListe.Remove(mi);
            }
            while (geplanteAktionen.Count >= 1 && geplanteAktionen.Count > Angriffsaktionen)
            {
                var manöver = geplanteAktionen.FirstOrDefault();
                //Das letzte Manöver wird in KeineAktion umgewandelt um den Kämpfer weiterhin in der Liste zu haben.
                if (Angriffsaktionen == 0 && geplanteAktionen.Count == 1)
                {
                    manöver.Manöver = new KeineAktion(this);
                    break;
                }
                //alle anderen Manöver, die zuviel sind, löschen.
                Kampf.InitiativListe.Remove(manöver);
                geplanteAktionen.Remove(manöver);
            }
            //wenn die Liste ganz leer ist, füge KeineAktion hinzu
            //if (Angriffsaktionen == 0 && Kampf.InitiativListe.Where(mi => mi.KämpferInfo == this).Count() == 0)
            //    Kampf.InitiativListe.Add(this, new Manöver.KeineAktion(this), 0);

            AbwehrManöver.Clear();
        }

        public void StandardAktionenSetzen(int kampfrunde)
        {
            var ki = this;
            var geplanteAktionen = AngriffsManöver.Where(mi => mi.IsAktion).ToList().OrderBy(mi => mi.Start).ToList();
            //löschen von Manövern, für die der falsche Kampfstil gewählt ist. oder für die zu wenig aktionen vorhanden sind.
            DeleteManöver(ref geplanteAktionen);
            //var lfh = AngriffsManöver.Where(mi => mi.Manöver.VerbleibendeDauer >= 2).OrderBy(mi => mi.InitiativeStart).FirstOrDefault();
            //if (lfh != null)
            //{
            //    if (lfh.InitiativeModStart == 0 && Aktionen > 1)//zweite Aktion
            //        Kampf.InitiativListe.Add(lfh.Manöver, -8, kampfrunde);
            //    return;
            //}

            int zusatzAktionen = geplanteAktionen.Where(mi => mi.Manöver is Manöver.ZusätzlicheAngriffsaktion).Count();
            for (int i = geplanteAktionen.Count; i < ki.Angriffsaktionen; i++)
            {
                if (i == 0)
                {
                    var m = AngriffsManöver.Where(mi => mi.Manöver is Manöver.KeineAktion).FirstOrDefault();
                    if (m == null)
                        Kampf.InitiativListe.Add(new Attacke(ki), 0, kampfrunde);
                    else
                        m.Manöver = new Attacke(ki);
                }
                else // i>=1
                {
                    var ersterAngriff = ki.AngriffsManöver.Where(mi => mi.Manöver is Manöver.Angriffsaktion).FirstOrDefault();
                    if (ki.Kampfstil == Kampfstil.BeidhändigerKampf)
                    {
                        //normale Aktionen und zusatzaktionen getrennt zählen, dann ist es einfach
                        //wenn noch keine Angriffsaktion da ist, dann Attacke bei i*-8
                        //wenn schon eine Angriffsaktion in der Liste ist, und i<Aktionen, dann zusatzaktion bei erste Angriffsaktion-4. sonst Attacke bei -8
                        if (ersterAngriff == null)
                        {
                            Kampf.InitiativListe.Add(new Attacke(ki), Math.Min(i * -4, -8), kampfrunde);
                        }
                        else
                        if (ersterAngriff != null)
                        {
                            if (zusatzAktionen > 0 && ki.Abwehraktionen == 0 && i == ki.Angriffsaktionen - 1) //es wurde umgewandelt.
                                Kampf.InitiativListe.Add(new Attacke(ki), -8, kampfrunde);
                            else
                            {
                                //4 ini-phasen nach dem ersten angriff
                                Kampf.InitiativListe.Add(new ZusätzlicheAngriffsaktion(ki), ersterAngriff.InitiativeModStart + (zusatzAktionen + 1) * -4, kampfrunde);
                                zusatzAktionen++;
                            }
                        }
                    }
                    else if (ki.Kampfstil == Kampfstil.Parierwaffenstil)
                    {
                        if (ki.Abwehraktionen == 1 && ersterAngriff != null && (ki.Kämpfer is Model.Gegner || (ki.Kämpfer as Model.Held).HatSonderfertigkeit("Tod von Links")) && geplanteAktionen.Where(mi => mi.Manöver is Manöver.TodVonLinks).Count() == 0)
                            Kampf.InitiativListe.Add(new TodVonLinks(ki), Math.Min(i * -4, -8), kampfrunde);
                        else
                            Kampf.InitiativListe.Add(new Attacke(ki), Math.Min(i * -4, -8), kampfrunde);
                    }
                    else if (ki.Kämpfer is Model.Gegner && ki.Aktionen > 2)
                        Kampf.InitiativListe.Add(new Attacke(ki), i * -4, kampfrunde);
                    else
                        Kampf.InitiativListe.Add(new Attacke(ki), Math.Min(i * -4, -8), kampfrunde);
                }
            }

            //Parade-Manöver setzen
            for (int i = 0; i < Abwehraktionen; i++)
                AbwehrManöver.Add(new ManöverInfo(new Parade(this), 0, Kampf.Kampfrunde));
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChanged(String info, object sender = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender ?? this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        public void Dispose()
        {
            if (Kämpfer != null)
            {
                Kämpfer.PropertyChanged -= Kämpfer_PropertyChanged;
                Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.PABonusDurchHoheIni);
            }
            Kampf.PropertyChanged -= Kampf_PropertyChanged;
            PropertyChanged -= DependentProperty.PropagateINotifyProperyChanged;
        }
    }

    public class KämpferInfoListe : List<KämpferInfo>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        private Dictionary<IKämpfer, KämpferInfo> _kämpfer_kämpferinfo;

        public KämpferInfoListe(Kampf kampf)
        {
            _kampf = kampf;
            _kämpfer_kämpferinfo = new Dictionary<IKämpfer, KämpferInfo>();
        }

        public KämpferInfo this[IKämpfer k]
        {
            get
            {
                return _kämpfer_kämpferinfo[k];
            }
        }

        public IEnumerable<IKämpfer> Kämpfer
        {
            get { return _kämpfer_kämpferinfo.Keys; }
        }

        private Kampf _kampf;
        public Kampf Kampf
        {
            get { return _kampf; }
            set { _kampf = value; }
        }

        #region Add and Remove
        public new void Add(KämpferInfo ki)
        {
            base.Add(ki);
            _kämpfer_kämpferinfo.Add(ki.Kämpfer, ki);
            ki.PropertyChanged += OnKämpferInfoChanged;
            Sort();
            OnCollectionChanged(NotifyCollectionChangedAction.Add, ki);
        }

        public void Add(IKämpfer k)
        {
            Add(k, 1);
        }

        public void Add(IKämpfer k, int team)
        {
            var ki = new KämpferInfo(k, Kampf) { Team = team };
            Add(ki);
            ki.Abwehraktionen = 1;
            ki.Angriffsaktionen = Math.Max(ki.Aktionen - ki.Abwehraktionen, 0);
        }

        public new void Remove(KämpferInfo ki)
        {
            ki.PropertyChanged -= OnKämpferInfoChanged;
            _kämpfer_kämpferinfo.Remove(ki.Kämpfer);
            //TODO JT: sobald ein Kampf abgespeichert werden kann muss dies weg
            Model.Gegner g = ki.Kämpfer as Model.Gegner;
            if (g != null)
                Global.ContextKampf.Delete<Model.Gegner>(g);

            base.Remove(ki);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, ki);
        }

        public new void RemoveAt(int index)
        {
            var removed = this[index];
            removed.PropertyChanged -= OnKämpferInfoChanged;
            _kämpfer_kämpferinfo.Remove(removed.Kämpfer);
            //TODO JT: sobald ein Kampf abgespeichert werden kann muss dies weg
            Model.Gegner g = removed.Kämpfer as Model.Gegner;
            if (g != null)
                Global.ContextKampf.Delete<Model.Gegner>(g);

            base.RemoveAt(index);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, removed);
        }

        public new void RemoveAll(Predicate<KämpferInfo> match)
        {
            foreach (KämpferInfo k in this.Where(ki => match(ki)).ToList())
                Remove(k);
        }

        public new void RemoveRange(int index, int range)
        {
            throw new NotImplementedException();
        }

        public void Remove(IKämpfer k)
        {
            if (_kämpfer_kämpferinfo.ContainsKey(k))
                Remove(this[k]);
        }

        public new void Clear()
        {
            foreach (KämpferInfo k in this.ToList())
                Remove(k);
        }
        #endregion

        private void OnKämpferInfoChanged(object o, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Initiative")
                Sort();
        }

        public new void Sort()
        {
            base.Sort(CompareInitiative);
        }

        /// <summary>
        /// Höhere Initiative nach oben.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int CompareInitiative(KämpferInfo x, KämpferInfo y)
        {
            return y.InitiativeMitKommas.CompareTo(x.InitiativeMitKommas);
            // prüfen auf null-Übergabe
            //if (x == null && y == null) return 0;
            //if (x == null) return 1;
            //if (y == null) return -1;
            //// Vergleich
            //if (x.Initiative > y.Initiative)
            //    return -1;
            //if (x.Initiative < y.Initiative)
            //    return 1;
            //if (x.InitiativeBasis > y.InitiativeBasis)
            //    return -1;
            //if (x.InitiativeBasis < y.InitiativeBasis)
            //    return 1;
            //return x.Kämpfer.Name.CompareTo(y.Kämpfer.Name);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChanged(String info, object sender = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender ?? this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object element)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, element));

                if (action == NotifyCollectionChangedAction.Add ||
                    action == NotifyCollectionChangedAction.Remove ||
                    action == NotifyCollectionChangedAction.Reset)
                    OnChanged("Count");

                //TODO: Optimieren. Nur die IndexChanged-Events in Gang setzen, die sich auch wirklich geändert haben
                foreach (KämpferInfo info in this)
                    info.OnChanged("Index");
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
