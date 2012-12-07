using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using MeisterGeister.Logic.Extensions;
using System.Collections.Specialized;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class KämpferInfo : INotifyPropertyChanged, IDisposable
    {
        private IKämpfer _kämpfer;

        public IKämpfer Kämpfer
        {
            get { return _kämpfer; }
            private set { _kämpfer = value; AktionenBerechnen(); 
                OnChanged("Kämpfer"); 
            }
        }

        #region Initiative
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
            set { _team = value; OnChanged("Team"); }
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
                if (Kampf == null || Kampf.AktuelleAktion == null)
                    return false;
                return this == Kampf.AktuelleAktion.KämpferInfo; 
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
            Team = 1;
            Initiative = k.Initiative();
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
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
            set { _verbrauchteAngriffsaktionen = value; }
        }

        private int _verbrauchteAbwehraktionen = 0;
        public int VerbrauchteAbwehraktionen
        {
            get { return _verbrauchteAbwehraktionen; }
            set { _verbrauchteAbwehraktionen = value; }
        }

        private int _verbrauchteFreieAktionen = 0;
        public int VerbrauchteFreieAktionen
        {
            get { return _verbrauchteFreieAktionen; }
            set { _verbrauchteFreieAktionen = value; }
        }

        public void AktionenBerechnen()
        {
            int aktionen = 2;
            if (Initiative < 0)
            {
                aktionen = 1;
            }
            var m = ManöverInfos.Where(mi => mi.Manöver is Manöver.LängerfristigeHandlung && mi.Manöver.VerbleibendeDauer >= 2).OrderBy(mi => mi.Initiative).FirstOrDefault();
            if (m != null) //wenn man eine LängerfristigeHandlung Dauer >= 2 ausführt, dann 
            {
                if (m.InitiativeMod == 0)
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
                    _abwehraktionen = Math.Max(parierwaffenII?2:1, Abwehraktionen);
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

        public List<ManöverInfo> ManöverInfos
        {
            get {
                if (Kampf == null)
                    return new List<ManöverInfo>();
                return Kampf.InitiativListe.Where(mi => mi.KämpferInfo == this).OrderByDescending(mi => mi.Initiative).ToList(); 
            }
        }

        private void DeleteManöver(ref List<ManöverInfo> geplanteAktionen)
        {
            var ki = this;
            //manöver mit negativer INI löschen
            foreach (var mi in ManöverInfos.Where(mi => mi.Initiative < 0 && mi.InitiativeMod != 0).ToList())
                Kampf.InitiativListe.Remove(mi);

            var ersterAngriff = ki.ManöverInfos.Where(mi => mi.Manöver is Manöver.Angriffsaktion).FirstOrDefault();
            //löschen von Manövern, für die der falsche Kampfstil gewählt ist.
            if (ki.Kampfstil != Kampfstil.BeidhändigerKampf) //oder mehrhändig
            {
                foreach (var mi in ManöverInfos.Where(mi => mi.Manöver is Manöver.ZusätzlicheAngriffsaktion).ToList())
                    Kampf.InitiativListe.Remove(mi);
            }
            else
            {
                //ZusätzlicheAngriffsaktion löschen, für die die Bedingungen nicht erfüllt sind
                foreach (var mi in ManöverInfos.Where(mi => mi.Manöver is Manöver.ZusätzlicheAngriffsaktion && (ersterAngriff == null || mi.Initiative > ersterAngriff.Initiative-4)).ToList())
                    Kampf.InitiativListe.Remove(mi);
            }
            if (ki.Kampfstil != Kampfstil.Parierwaffenstil)
            {
                foreach (var mi in ManöverInfos.Where(mi => mi.Manöver is Manöver.TodVonLinks).ToList())
                    Kampf.InitiativListe.Remove(mi);
            }
            else
            {
                //TodVonLinks löschen, für die die Bedingungen nicht erfüllt sind
                foreach (var mi in ManöverInfos.Where(mi => mi.Manöver is Manöver.TodVonLinks && (Abwehraktionen != 1 || ersterAngriff == null || mi.Initiative > ersterAngriff.Initiative - 8)).ToList())
                    Kampf.InitiativListe.Remove(mi);
            }
            while (geplanteAktionen.Count >= 1 && geplanteAktionen.Count > ki.Angriffsaktionen)
            {
                var manöver = geplanteAktionen.FirstOrDefault();
                //Das letzte Manöver wird in KeineAktion umgewandelt um den Kämpfer weiterhin in der Liste zu haben.
                if (ki.Angriffsaktionen == 0 && geplanteAktionen.Count == 1)
                {
                    manöver.Manöver = new Manöver.KeineAktion(ki);
                    break;
                }
                //alle anderen Manöver, die zuviel sind, löschen.
                Kampf.InitiativListe.Remove(manöver);
                geplanteAktionen.Remove(manöver);
            }
            //wenn die Liste ganz leer ist, füge KeineAktion hinzu
            if (ki.Angriffsaktionen == 0 && Kampf.InitiativListe.Where(mi => mi.KämpferInfo == ki).Count() == 0)
                Kampf.InitiativListe.Add(ki, new Manöver.KeineAktion(ki), 0);
        }

        public void StandardAktionenSetzen()
        {
            var ki = this;
            var geplanteAktionen = ManöverInfos.Where(mi => mi.IsAktion).ToList().OrderBy(mi => mi.Initiative).ToList();
            //löschen von Manövern, für die der falsche Kampfstil gewählt ist. oder für die zu wenig aktionen vorhanden sind.
            DeleteManöver(ref geplanteAktionen);
            var lfh = ManöverInfos.Where(mi => mi.Manöver is Manöver.LängerfristigeHandlung && mi.Manöver.VerbleibendeDauer >= 2).OrderBy(mi => mi.Initiative).FirstOrDefault();
            if (lfh != null)
            {
                if (lfh.InitiativeMod != 0) //zweite aktion
                    return;
                if(Aktionen > 1)
                    Kampf.InitiativListe.Add(ki, lfh.Manöver, -8);
                return;
            }

            int zusatzAktionen = geplanteAktionen.Where(mi => mi.Manöver is Manöver.ZusätzlicheAngriffsaktion).Count();
            for (int i = geplanteAktionen.Count; i < ki.Angriffsaktionen; i++)
            {
                if (i == 0)
                {
                    var m = ManöverInfos.Where(mi => mi.Manöver is Manöver.KeineAktion).FirstOrDefault();
                    if (m == null)
                        Kampf.InitiativListe.Add(ki, new Manöver.Attacke(ki), 0);
                    else
                        m.Manöver = new Manöver.Attacke(ki);
                }
                else // i>=1
                {
                    var ersterAngriff = ki.ManöverInfos.Where(mi => mi.Manöver is Manöver.Angriffsaktion).FirstOrDefault();
                    if (ki.Kampfstil == Kampfstil.BeidhändigerKampf)
                    {
                        //normale Aktionen und zusatzaktionen getrennt zählen, dann ist es einfach
                        //wenn noch keine Angriffsaktion da ist, dann Attacke bei i*-8
                        //wenn schon eine Angriffsaktion in der Liste ist, und i<Aktionen, dann zusatzaktion bei erste Angriffsaktion-4. sonst Attacke bei -8
                        if (ersterAngriff == null)
                        {
                            Kampf.InitiativListe.Add(ki, new Manöver.Attacke(ki), Math.Min(i * -4, -8));
                        }
                        else
                        if (ersterAngriff != null)
                        {
                            if (zusatzAktionen > 0 && ki.Abwehraktionen==0 && i==ki.Angriffsaktionen-1) //es wurde umgewandelt.
                                Kampf.InitiativListe.Add(ki, new Manöver.Attacke(ki), -8);
                            else
                            {
                                //4 ini-phasen nach dem ersten angriff
                                Kampf.InitiativListe.Add(ki, new Manöver.ZusätzlicheAngriffsaktion(ki), ersterAngriff.InitiativeMod + (zusatzAktionen + 1) * -4);
                                zusatzAktionen++;
                            }
                        }
                    }
                    else if (ki.Kampfstil == Kampfstil.Parierwaffenstil)
                    {
                        if (ki.Abwehraktionen == 1 && ersterAngriff != null && (ki.Kämpfer is Model.Gegner || (ki.Kämpfer as Model.Held).HatSonderfertigkeit("Tod von Links")) && geplanteAktionen.Where(mi => mi.Manöver is Manöver.TodVonLinks).Count() == 0)
                            Kampf.InitiativListe.Add(ki, new Manöver.TodVonLinks(ki), Math.Min(i * -4, -8));
                        else
                            Kampf.InitiativListe.Add(ki, new Manöver.Attacke(ki), Math.Min(i * -4, -8));
                    }
                    else if (ki.Kämpfer is Model.Gegner && ki.Aktionen > 2)
                        Kampf.InitiativListe.Add(ki, new Manöver.Attacke(ki), i * -4);
                    else
                        Kampf.InitiativListe.Add(ki, new Manöver.Attacke(ki), Math.Min(i * -4, -8));
                }
            }

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
            Remove(this[k]);
        }

        public new void Clear()
        {
            foreach (KämpferInfo k in this.ToList())
                Remove(k);
        }
        #endregion

        private void OnKämpferInfoChanged(object o, System.ComponentModel.PropertyChangedEventArgs args)
        {
            if(args.PropertyName == "Initiative")
                Sort();
            if (args.PropertyName == "Angriffsaktionen")
                OnChanged("Angriffsaktionen", o);
        }

        public new void Sort()
        {
            base.Sort(CompareInitiative);
            OnChanged("Sort");
        }

        /// <summary>
        /// Höhere Initiative nach oben.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int CompareInitiative(KämpferInfo x, KämpferInfo y)
        {
            // prüfen auf null-Übergabe
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1;
            // Vergleich
            if (x.Initiative > y.Initiative)
                return -1;
            if (x.Initiative < y.Initiative)
                return 1;
            if (x.InitiativeBasis > y.InitiativeBasis)
                return -1;
            if (x.InitiativeBasis < y.InitiativeBasis)
                return 1;
            return x.Kämpfer.Name.CompareTo(y.Kämpfer.Name);
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
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
