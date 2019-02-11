using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MeisterGeister.View.Bodenplan;
using MeisterGeister.Logic.Extensions;
using System.Collections.Specialized;
using System.ComponentModel;
using MeisterGeister.ViewModel.Base;
using MeisterGeister.Model.Extensions;
using System.Windows.Data;
using System.Globalization;

using MeisterGeister.ViewModel.Bodenplan.Logic;
using MeisterGeister.Model;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using MeisterGeister.ViewModel.Kampf.Logic.Manöver;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class Kampf : ViewModelBase, IDisposable
    {
        /*
         * Die Klasse hält und kontrolliert alle globalen Kampfinformationen und -Aktionen
         * neue KR(), nächster Kämpfer(), ...
         * aktueller Kämpfer, aktuelle KR, aktuelle INI ...
         * 
         * Liste der angesagten Aktionen pro KR, Liste der ausgeführten Aktionen pro KR
         * 
         * Eine Liste der Kämpfer
         * Zur Unterstützung einer beliebigen Anzahl an Parteien gehört zu jedem Teilnehmer eine Teamnummer
         */
        public Kampf()
        {
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
            Kämpfer = new KämpferInfoListe(this);
            Kämpfer.CollectionChangedExtended += Kämpfer_CollectionChangedExtended;
            InitiativListe = new InitiativListe(this);
            KampfNeuStarten();
        }
        public View.Bodenplan.BattlegroundView Bodenplan { get; set; }

        private ObservableCollection<string> _kampfLog = new ObservableCollection<string>();
        public ObservableCollection<string> KampfLog
        {
            get { return _kampfLog; }
        }

        private InitiativListe _initiativListe;
        public InitiativListe InitiativListe
        {
            get { return _initiativListe; }
            private set
            {
                _initiativListe = value;
                SortedInitiativListe = InitiativListe != null ?
                    (Kampfrunde == 0 ?
                    InitiativListe.OrderByDescending(t => t.Start.InitiativPhase) :
                    InitiativListe.Where(t => t.AktKampfrunde == Kampfrunde).OrderByDescending(t => t.Start.InitiativPhase)
                    )                  
                    : null;
            }
        }

        private ManöverInfo _aktiverManöverInfo;
        public ManöverInfo AktiverManöverInfo
        {
            get { return _aktiverManöverInfo; }
            set { Set(ref _aktiverManöverInfo, value); }
        }

        private ManöverInfo _selectedManöverInfo;
        public ManöverInfo SelectedManöverInfo
        {
            get { return _selectedManöverInfo; }
            set { Set(ref _selectedManöverInfo, value);
                // value.Manöver.Ausführender.Kämpfer = IKämpfer  as Wesen ==> BattlegroundObjects.where(t => (t as Wesen).IsAnDerReihe)
            }
        }

        private IEnumerable<ManöverInfo> _sortedInitiativListe;
        public IEnumerable<ManöverInfo> SortedInitiativListe
        {
            get { return _sortedInitiativListe; }
            set { Set(ref _sortedInitiativListe, value); }
        }

        private KämpferInfoListe _kämpfer;
        public KämpferInfoListe Kämpfer
        {
            get { return _kämpfer; }
            private set
            {
                _kämpfer = value;
                SortedInitiativListe = InitiativListe != null ?
                    (Kampfrunde == 0 ?
                    InitiativListe.OrderByDescending(t => t.Start.InitiativPhase) :
                    InitiativListe.Where(t => t.AktKampfrunde == Kampfrunde).OrderByDescending(t => t.Start.InitiativPhase)
                    )
                    : null; //InitiativListe != null ? InitiativListe.Where(t => t.Kampf.Kampfrunde == AktuelleAktionszeit.Kampfrunde).OrderByDescending(t => t.Start.InitiativPhase) : null;
            }
        }

        private int _kampfrunde;
        public int Kampfrunde
        {
            get { return _kampfrunde; }
            private set
            {
                _kampfrunde = value;
                Kampfzeit = new TimeSpan(0, 0, 3 * Math.Max(value - 1, 0));
                OnChanged("Kampfrunde");
            }
        }

        private TimeSpan _kampfzeit = new TimeSpan();
        public TimeSpan Kampfzeit
        {
            get { return _kampfzeit; }
            private set
            {
                Set(ref _kampfzeit, value);
            }
        }

        private ZeitImKampf aktuelleAktionszeit;
        public ZeitImKampf AktuelleAktionszeit
        {
            get { return aktuelleAktionszeit; }
            private set
            {
                Set(ref aktuelleAktionszeit, value);
            }
        }

        private KämpferInfo _aktIniKämpfer = null;
        public KämpferInfo AktIniKämpfer
        {
            get { return _aktIniKämpfer; }
            set { Set(ref _aktIniKämpfer, value); }
        }

        [DependentProperty("AktuelleAktionszeit")]
        private IEnumerable<ManöverInfo> AktuelleAktionen
        {
            get { return InitiativListe.Where(mi => mi.Aktionszeiten.Contains(AktuelleAktionszeit)); }
        }

        
        private Lichtstufe licht = Lichtstufe.Tageslicht;
        public Lichtstufe Licht
        {
            get { return licht; }
            set
            {
                Set(ref licht, value);
                foreach (ManöverInfo mi in InitiativListe)
                {
                    KampfManöver<IWaffe> manöver = mi.Manöver as KampfManöver<IWaffe>;
                    if (manöver != null)
                        ((ManöverModifikator<Lichtstufe, IWaffe>)manöver.Mods[KampfManöver<IWaffe>.LICHT_MOD]).Value = value;
                }
            }
        }

        private Sichtstufe sicht = 0;
        public Sichtstufe Sicht
        {
            get { return sicht; }
            set 
            {
                Set(ref sicht, value);
                foreach (ManöverInfo mi in InitiativListe)
                {
                    KampfManöver<IWaffe> manöver = mi.Manöver as KampfManöver<IWaffe>;
                    if (manöver != null)
                        ((ManöverModifikator<Sichtstufe, IWaffe>)manöver.Mods[KampfManöver<IWaffe>.SICHT_MOD]).Value = value;
                }
            }
        }
        
        /// <summary>
        /// Speichert einen Text im Kampf-Log.
        /// </summary>
        /// <param name="msg">Zu speichernder Log-Text.</param>
        public void Log(string msg)
        {
            KampfLog.Insert(0, string.Format("{0}.{1}: {2}", Kampfrunde, AktuelleAktionszeit.InitiativPhase, msg));
        }
        
        List<KämpferInfo> lstKämpferGleicheIni = new List<KämpferInfo>();

        List<ManöverInfo> lstMI = new List<Logic.ManöverInfo>();
        public void Next()
        {
            OnChanged("SortedInitiativListe");
            List<ManöverInfo> Smi = new List<Logic.ManöverInfo>();
            Smi.AddRange(SortedInitiativListe);

            foreach (ManöverInfo mi in AktuelleAktionen)
            {
                ((Wesen)mi.Manöver.Ausführender.Kämpfer).AktManöverInfo = mi;

                //Es werden nur aktive Manöver ausgeführt (also keine Paraden)
                if (!mi.Manöver.IsAusgeführt && mi.Manöver.Typ == ManöverTyp.Aktion)
                     mi.Manöver.Aktion();
                if (mi.Manöver.GetType() == typeof(Manöver.FernkampfManöver))
                {
                    if (mi.Manöver.Typ == ManöverTyp.Aktion)
                    {
                        //TODO: Check wann die Zahl ausgeblendet werden sollte
                        if (mi.Manöver.VerbleibendeDauer == 0)
                            ((Wesen)mi.Manöver.Ausführender.Kämpfer).AktVerbleibendeDauer = null;    
                        else
                            ((Wesen)mi.Manöver.Ausführender.Kämpfer).AktVerbleibendeDauer = mi.Manöver.VerbleibendeDauer;
                    }

                    //Neuer Fernangriff innerhalb der aktiven KR -> 1aktive Aktion zum Ende
                    if (mi.Start.Kampfrunde == mi.End.Kampfrunde &&
                        mi.Start.InitiativPhase != mi.End.InitiativPhase)
                        //&&
                        //mi.Aktionszeiten.Where(t => t.InitiativPhase == mi.End.InitiativPhase).Count()==1)
                    {
                        if (this.InitiativListe.Where(t => t.Aktionszeiten.Contains(new ZeitImKampf(mi.AktKampfrunde, mi.End.InitiativPhase)))
                            .Where(z => z.Manöver.Ausführender == mi.Manöver.Ausführender).Count() == 1)
                        {
                            ManöverInfo minfo = new ManöverInfo(new Attacke(mi.Manöver.Ausführender), 0, mi.AktKampfrunde);
                            if (mi.Manöver.GetType() == typeof(Manöver.FernkampfManöver))
                            {
                                minfo.Manöver = new Manöver.FernkampfManöver(mi.Manöver.Ausführender,
                                    (IFernkampfwaffe)(mi.Manöver as Manöver.FernkampfManöver).FernkampfWaffeSelected);
                            }
                            else
                            if (mi.Manöver.GetType() == typeof(Manöver.Zauber))
                            {
                                minfo.Manöver = new Manöver.Zauber(mi.Manöver.Ausführender,
                                    (Held_Zauber)(mi.Manöver as Manöver.Zauber).Held_Zauber);
                            }
                            //Nur 1 Aktion für das Würfeln der längerfriatige Handlung
                            minfo.Start = mi.End;
                            minfo.End = mi.End;
                            //In der 1.Akt muss die längerfristige Handlung aktiv werden, die 2. Akt wird ebenfalls als aktive Aktion freigeschaltet

                            minfo.Manöver.Dauer = 1;
                            minfo.Manöver.VerbleibendeDauer = 1;
                            this.InitiativListe.Add(new ManöverInfo(minfo.Manöver, 8, mi.AktKampfrunde));
                            SortedInitiativListe = InitiativListe != null ?
                                (Kampfrunde == 0 ?
                                InitiativListe.OrderByDescending(t => t.Start.InitiativPhase) :
                                InitiativListe.Where(a => a.AktKampfrunde == Kampfrunde).OrderByDescending(t => t.Start.InitiativPhase)
                                )
                                : null;

                            //else
                            ////Die Längerfristige Handlung Endet in der KR - daher muss die 2. Akt ein aktives Manöver werden
                            //if (mi.Manöver.Ausführender.Aktionen >= 2 && mi.Manöver.VerbleibendeDauer == 2)
                            //{
                            //    mi.Manöver.Dauer = 1;
                            //    mi.Manöver.VerbleibendeDauer = 1;
                            //    yield return new ManöverInfo(mi.Manöver, 8, mi.AktKampfrunde);
                            //}
                            //Parade-Manöver setzen
                            mi.Manöver.Ausführender.AbwehrManöver.Clear();
                            break;
                        }
                    }
                }                
            }

            ZeitImKampf next = default(ZeitImKampf);

            if (AktuelleAktionszeit != default(ZeitImKampf))
            {
                if (lstKämpferGleicheIni.Count > 1)
                {
                    next = AktuelleAktionszeit;
                    lstKämpferGleicheIni.RemoveAt(0);
                }
                else
                {
                    var nextActions = InitiativListe.Aktionszeiten.Where(zeit => zeit > AktuelleAktionszeit && zeit.Kampfrunde == Kampfrunde).OrderBy(zeit => zeit);
                    next = nextActions.FirstOrDefault();
                    lstKämpferGleicheIni.Clear();
                }
            }
            else
                next = InitiativListe.Aktionszeiten.Where(zeit => zeit.Kampfrunde == Kampfrunde).FirstOrDefault();

            if (next != default(ZeitImKampf) && lstKämpferGleicheIni.Count == 0)
            {                   
                lstMI = (List<ManöverInfo>)Smi.Where(k => k.AktKampfrunde == next.Kampfrunde).ToList().FindAll(t => t.Start.InitiativPhase == next.InitiativPhase);
                List<KämpferInfo> lstK = lstMI.Select(t => t.Manöver.Ausführender).ToList();   
                
                //Längerfristige Handlung von Kämpfer
                //Dieser wird nicht in der INI angezeigt, soll aber abgefragt werden, ob er weiter die Handlung durchführen will
                if (lstMI.Count == 0 && lstK.Count == 0)
                {
                    List<ManöverInfo> lstLängerKämpfer = new List<ManöverInfo>();
                    lstLängerKämpfer.AddRange(InitiativListe.Where(t => t.Aktionszeiten.Contains(next)));

                    lstK = lstLängerKämpfer.Select(t => t.Manöver.Ausführender).ToList();
                    AktiverManöverInfo = lstLängerKämpfer[0]; 
                }

                if (lstK.Count > 0)
                    lstKämpferGleicheIni = lstK;

                if (lstMI.Count == 1)
                    AktiverManöverInfo = lstMI[0];

            }
            AktuelleAktionszeit = next;
            if (next == default(ZeitImKampf))
                NeueKampfrunde();
                        
            if (AktIniKämpfer != null)
            {
                ((Wesen)AktIniKämpfer.Kämpfer).IsAnDerReihe = false;
            }

            ManöverInfo nextManöver = null;
            if (lstMI.Count > 1)
                nextManöver = lstMI[lstMI.IndexOf(lstMI.FirstOrDefault(t => t.IsAnDerReihe)) + 1];

            Smi.ToList().ForEach(delegate (ManöverInfo mi) { mi.IsAnDerReihe = false; });

            //var ini = InitiativListe.Aktionszeiten.Select(t => t.Kampfrunde).Where(kr => kr == Kampfrunde);
            
            if (lstKämpferGleicheIni.Count != 0)
                AktIniKämpfer = lstKämpferGleicheIni[0];
            else
                AktIniKämpfer = null;

            if (AktIniKämpfer != null)
                ((Wesen)AktIniKämpfer.Kämpfer).IsAnDerReihe = true;

            if (lstMI.Count > 1)
            //Nächsten Kämpfer in der Reihe der Aktuellen INI selektieren
            {
                nextManöver.IsAnDerReihe = true;
                if (lstMI.Count > 0) lstMI.RemoveAt(0);
            }
            else
            //Nächsten Kämpfer der nächsten INI selektieren
            { 
                AktIniLängereHandlung = null;
                ManöverInfo aktmit = lstMI.FirstOrDefault(t => t.Start.InitiativPhase == AktuelleAktionszeit.InitiativPhase);
                if (aktmit != null)
                    aktmit.IsAnDerReihe = true;
                else
                {
                    if (AktuelleAktionszeit.Kampfrunde != 0)
                    //Temporär werden alle Kämpfer -aus mit längerfristigen Handlungen- übersprungen
                        Next();
                }
            }                
        }

        private string _aktIniLängereHandlung = null;
        public string AktIniLängereHandlung
        {
            get { return _aktIniLängereHandlung; }
            set { Set(ref _aktIniLängereHandlung, value); }
        }

        public Nullable<Position> tempP;
        public void NeueKampfrunde()
        {
            ////Alle Manöver, die noch nicht ausgeführt wurden
            //var nichtAusgeführt = InitiativListe.Where(mi => mi.Ausgeführt == false);
            ////TODO JT: entweder warnen
            //foreach (var mi in nichtAusgeführt)
            //{
            //    //oder einfach als ausgeführt setzen
            //    if (mi.Manöver != null)
            //    {
            //        var probe = mi.Manöver.Ausführen();
            //        //Siehe kommentar in next() ... das design ist mist.
            //        //Wenn die probe ignoriert wird passiert auch nichts weiter, ausser dass die Verbleibenden Aktionen sinken.
            //    }
            //    else
            //        mi.Ausgeführt = true;
            //}
            //Alte Ansagen löschen
            //InitiativListe.LöscheBeendeteManöver();

            Kampfrunde++;

            //Wir speichern unsere neuen Manöver zuerst hier zwischen und fügen dann den kompletten Satz an neuen Aktionen auf einmach zur IniListe hinzu
            //Das verhindert dass bei jedem Manöver ein separates Change-Event ausgelöst wird, welches in der GUI Performance kostet
            List<ManöverInfo> neueManöver = new List<ManöverInfo>();

            foreach (KämpferInfo ki in Kämpfer)
            {
                //Modifikatoren entfernen
                ki.Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.IEndetMitKampfrunde);

                //sollte in KämpferInfo rein
                ki.VerbrauchteAbwehraktionen = 0;
                ki.VerbrauchteAngriffsaktionen = 0;
                ki.VerbrauchteFreieAktionen = 0;

                //aktuelle Position des Helden notieren
                tempP = ki.PositionSelbst.Value;
                
                neueManöver.AddRange(ki.StandardAktionenSetzen(Kampfrunde));

                //setzen auf vorherige Position
                Global.CurrentKampf.BodenplanViewModel.DoChangeModPositionSelbst = true;
                ((IKämpfer)Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects.FirstOrDefault(t => t as IKämpfer == ki.Kämpfer)).Position = tempP.Value;
                ki.Kämpfer.Position = tempP.Value;
                tempP = null;
                Global.CurrentKampf.SelectedManöver = null;

                ki.VerbrauchteAbwehraktionen = 0;
                ki.VerbrauchteAngriffsaktionen = 0;
                ki.VerbrauchteFreieAktionen = 0;
                //Im UI sollten kämpfer ohne Ansage leicht an der Farbe erkennbar sein
                //Kämpfer mit Aufmerksamkeit oder Kampfgespür müssen nicht markiert werden (höchstens mit einer leichten tönung)                  
            }
            List<ManöverInfo> lstToRemove = InitiativListe.Where(t => t.End.Kampfrunde < Kampfrunde).ToList();
            if (lstToRemove.Count > 0)
                InitiativListe.RemoveRange(lstToRemove);
            
            if (neueManöver.Count > 0)
            {
                InitiativListe.AddRange(neueManöver);
                SortedInitiativListe = InitiativListe != null ?
                    (Kampfrunde == 0 ?
                    InitiativListe.OrderByDescending(t => t.Start.InitiativPhase) :
                    InitiativListe.Where(a => a.AktKampfrunde == Kampfrunde).OrderByDescending(t => t.Start.InitiativPhase)
                    )
                    : null;
            }

            //if (InitiativListe.Count > 0)
            //    INIPhase = InitiativListe[0].InitiativeStart;
            //else
            //    INIPhase = 0;
            //im UI markieren, dass man nun bis zur ersten Aktions-Ansage umwandeln kann
            //UmwandelnMöglich = true;
            //eventuell in die Property?

            Log(string.Format("Kampfrunde {0} gestartet", Kampfrunde));
        }

        public void KampfNeuStarten()
        {
            KampfEnde();
            AktuelleAktionszeit = default(ZeitImKampf);
            InitiativListe.Clear();
                        
            Kampfrunde = 0;

            // INI neu ermitteln
            foreach (KämpferInfo kämpferInfo in Kämpfer)
            {
                if (kämpferInfo != null)
                    kämpferInfo.Initiative = kämpferInfo.Kämpfer.Initiative();
            }

            // KR abschließen
            NeueKampfrunde();
        }

        private void KampfEnde()
        {
            foreach (var ki in Kämpfer)
                ki.Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.IEndetMitKampf);
        }

        public void Orientieren(IKämpfer k)
        {
            Orientieren(Kämpfer[k]);
        }

        public void Orientieren(KämpferInfo ki)
        {
            ki.Initiative = ki.Kämpfer.InitiativeMax();
            ki.Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.IEndetMitAktion);
        }

        public void Kämpfer_CollectionChangedExtended(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                InitiativListe.RemoveAll(mi => args.OldItems.Contains(mi.Manöver.Ausführender));
            }
            else if (args.Action == NotifyCollectionChangedAction.Add)
            {
                InitiativListe.AddRange(args.NewItems.Cast<KämpferInfo>().SelectMany(ki => ki.StandardAktionenSetzen(Kampfrunde)));
            }
            //InitiativListe.Add((KämpferInfo)args.NewItems[0], new Manöver.KeineAktion(((KämpferInfo)args.NewItems[0]).Kämpfer), 0);

            SortedInitiativListe  = InitiativListe != null ?
                    (Kampfrunde == 0 ?
                    InitiativListe.OrderByDescending(t => t.Start.InitiativPhase) :
                    InitiativListe.Where(t => t.AktKampfrunde == Kampfrunde).OrderByDescending(t => t.Start.InitiativPhase)
                    )
                    : null;
            //= InitiativListe.Where(t => t.Kampf.Kampfrunde == AktuelleAktionszeit.Kampfrunde).OrderByDescending(t => t.Start.InitiativPhase); 
        }

        public void Dispose()
        {
            //TODO ??: ich finde diese Lösung noch nicht optimal. Das geht schief, wenn man speichern und laden möchte.
            //Alle Gegenerinstanzen löschen
            foreach (var k in Kämpfer.Where(ki => ki.Kämpfer is Model.Gegner).Select(ki => ki.Kämpfer))
            {
                Kämpfer.Remove(k);
            }

            Kämpfer.CollectionChanged -= Kämpfer_CollectionChangedExtended;

            //Alles aus der Ini-Liste löschen damit die Events abgemeldet werden
            while (InitiativListe.Count > 0)
                InitiativListe.RemoveAt(0);
        }

    }

    public class IsIntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          CultureInfo culture)
        {
            return value != null? (double)value == System.Convert.ToDouble(parameter): false;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          CultureInfo culture)
        {
            return (bool)value ? parameter : 0;
        }
    }
    public class DataTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          CultureInfo culture)
        {
            return value != null? value.GetType(): null;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DoubleToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            value = System.Convert.ToDouble(value.ToString().Replace(".", ","));
            return (int)(Math.Round((double)value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value;
        }
    }


    public class DoubleSichtConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)(Sichtstufe)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Sichtstufe)(int)Math.Round((double)value);
        }
    }
    
    public class DoubleLichtConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)(Lichtstufe)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Lichtstufe)(int)Math.Round((double)value);
        }
    }

    [Flags]
    public enum TrefferpunkteOptions
    {
        Default = 0x00,
        Ausdauerschaden = 0x01,
        IgnoriertRüstung = 0x02,
        VerringerteWundschwelle = 0x04,
        KeineWunden = 0x08,
        AusdauerschadenMachtKeineEchtenSchadenspunkte = 0x10
    }
}
