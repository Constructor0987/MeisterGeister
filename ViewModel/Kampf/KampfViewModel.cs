using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Bodenplan;
using MeisterGeister.ViewModel.Kampf.Logic;
using K = MeisterGeister.ViewModel.Kampf.Logic.Kampf;

namespace MeisterGeister.ViewModel.Kampf
{
    public class KampfViewModel : Base.ViewModelBase
    {
        private K _kampf = null;

        public KampfViewModel() : this(null, View.General.ViewHelper.Confirm)
        {
        }

        public KampfViewModel(Action<K> showGegnerView, Func<string, string, bool> confirm) 
            : this(null, showGegnerView, confirm)
        {
        }

        public KampfViewModel(Action<KampfViewModel> showBodenplanView, Action<K> showGegnerView, Func<string, string, bool> confirm)
            : base(confirm, View.General.ViewHelper.ShowError)
        {
            this.showGegnerView = showGegnerView;
            this.showBodenplanView = showBodenplanView;

            _kampf = new K();
            _kampf.OnNeueKampfrunde += _kampf_OnNeueKampfRunde;
            Kampf.PropertyChanged += Kampf_PropertyChanged;
            InitiativListe.PropertyChanged += InitiativListe_PropertyChanged;
        }

        void Kampf_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "INIPhase") OnChanged("INIPhase");
        }

        void _kampf_OnNeueKampfRunde(object sender, int kampfrunde)
        {
            OnChanged("Kampfrunde");
        }

        private void InitiativListe_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnChanged("InitiativListe");
        }

        //private View.Arena.ArenaWindow _bodenplanWindow;
        //public View.Arena.ArenaWindow BodenplanWindow
        //{
        //    get { return _bodenplanWindow; }
        //    set 
        //    { 
        //        _bodenplanWindow = value;
        //        Kampf.Bodenplan = value == null ? null : value.Arena;
        //        OnChanged("BodenplanWindow"); }
        //}

        private View.Bodenplan.BattlegroundWindow _bodenplanWindow;
        public View.Bodenplan.BattlegroundWindow BodenplanWindow
        {
            get { return _bodenplanWindow; }
            set
            {
                _bodenplanWindow = value;
                Kampf.Bodenplan = value ?? null;
                OnChanged("BodenplanWindow");
            }
        }
        

        public K Kampf
        {
            get { return _kampf; }
            set { _kampf = value; OnChanged("Kampf"); }
        }

        [DependentProperty("Kampf")]
        public InitiativListe InitiativListe
        {
            get { return Kampf != null ? Kampf.InitiativListe : null; }
        }

        public float INIPhase
        {
            get { return Kampf != null ? Kampf.Kampfrunde : 0; }
        }

        public KämpferInfoListe KämpferListe
        {
            get { return Kampf != null ? Kampf.Kämpfer : null; }
        }

        public IKämpfer SelectedKämpfer
        {
            get { return (SelectedKämpferInfo != null) ? SelectedKämpferInfo.Kämpfer : null; }
            set
            {
                foreach (var mi in InitiativListe)
                {
                    if (mi.KämpferInfo.Kämpfer == value)
                    {
                        SelectedManöverInfo = mi;
                        KämpferSelected = true;
                        break;
                    }
                }
                OnChanged("SelectedKämpfer");
            }
        }

        public KämpferInfo SelectedKämpferInfo
        {
            get { return (SelectedManöverInfo != null) ? SelectedManöverInfo.KämpferInfo : null; }
            //set { SelectedTreeItem = value; }
        }

        private ManöverInfo _selectedManöverInfo = null;
        public ManöverInfo SelectedManöverInfo
        {
            get { return _selectedManöverInfo; }
            set 
            { 
                _selectedManöverInfo = value;
                OnChanged("SelectedManöverInfo"); OnChanged("SelectedKämpferInfo"); OnChanged("SelectedKämpfer");
            }
        }
        
        private bool kämpferSelected = false;
        /// <summary>
        /// Um ManöverInfo und auch KämpferInfo unterscheiden zu können.
        /// </summary>
        public bool KämpferSelected
        {
            get { return kämpferSelected; }
            set { kämpferSelected = value; OnChanged("KämpferSelected"); }
        }

        private int schaden = 5;
        public int Schaden
        {
            get { return schaden; }
            set { schaden = value; OnChanged("Schaden"); }
        }

        private TrefferpunkteOptions wundschwellenOption = TrefferpunkteOptions.Default;
        public TrefferpunkteOptions WundschwellenOption
        {
            get { return wundschwellenOption; }
            set { wundschwellenOption = value; OnChanged("WundschwellenOption"); }
        }

        private TrefferpunkteOptions ausdauerSchadenMachtKeineSchadenspunkte = TrefferpunkteOptions.Default;
        public TrefferpunkteOptions? AusdauerSchadenMachtKeineSchadenspunkte
        {
            get { return ausdauerSchadenMachtKeineSchadenspunkte; }
            set { ausdauerSchadenMachtKeineSchadenspunkte = value ?? TrefferpunkteOptions.Default; OnChanged("AusdauerSchadenMachtKeineSchadenspunkte"); }
        }

        private Trefferzone selectedTrefferzone = Trefferzone.Unlokalisiert;
        public Trefferzone SelectedTrefferzone
        {
            get { return selectedTrefferzone; }
            set { selectedTrefferzone = value; OnChanged("SelectedTrefferzone"); }
        }

        #region // ---- COMMANDS ----

        private Base.CommandBase onAddHelden = null;
        public Base.CommandBase OnAddHelden
        {
            get
            {
                if (onAddHelden == null)
                    onAddHelden = new Base.CommandBase(AddHelden, null);
                return onAddHelden;
            }
        }

        private void AddHelden(object obj)
        {
            KämpferInfo ki = null;
            foreach (Model.Held held in Global.ContextHeld.HeldenGruppeListe)
            {
                if (!KämpferListe.Kämpfer.Contains(held))
                {
                    ki = new KämpferInfo(held, Kampf);
                    KämpferListe.Add(held);

                    //if (BodenplanWindow != null)
                    //   ((BattlegroundViewModel)BodenplanWindow.battlegroundView1.DataContext).AddHero(held);
                    
                }
            }
            var k = KämpferListe.FirstOrDefault();
        }

        private Base.CommandBase onDeleteKämpfer = null;
        public Base.CommandBase OnDeleteKämpfer
        {
            get
            {
                if (onDeleteKämpfer == null)
                    onDeleteKämpfer = new Base.CommandBase(DeleteKämpfer, null);
                return onDeleteKämpfer;
            }
        }

        private void DeleteKämpfer(object obj)
        {
            if (SelectedKämpferInfo != null && Confirm("Kämpfer entfernen", String.Format("Soll der Kämpfer {0} entfernt werden?", SelectedKämpferInfo.Kämpfer.Name)))
            {
                IKämpfer k = SelectedKämpferInfo.Kämpfer;
                KämpferListe.Remove(SelectedKämpferInfo);
                //if (BodenplanWindow != null)
                //{
                //    ((BattlegroundViewModel)BodenplanWindow.battlegroundView1.DataContext).RemoveCreature(k);
                //}
            }
        }

        private Base.CommandBase onDeleteAllKämpfer = null;
        public Base.CommandBase OnDeleteAllKämpfer
        {
            get
            {
                if (onDeleteAllKämpfer == null)
                    onDeleteAllKämpfer = new Base.CommandBase(DeleteAllKämpfer, null);
                return onDeleteAllKämpfer;
            }
        }

        private void DeleteAllKämpfer(object obj)
        {
            if (Confirm("Liste leeren", "Sollen alle Kämpfer entfernt werden?"))
            {
                KämpferListe.Clear();
                //if (BodenplanWindow != null)
                //{
                //    ((BattlegroundViewModel)BodenplanWindow.battlegroundView1.DataContext).RemoveCreatureAll();
                //}
            }
        }

        private Base.CommandBase onEinfärbenKämpfer = null;
        public Base.CommandBase OnEinfärbenKämpfer
        {
            get
            {
                if (onEinfärbenKämpfer == null)
                    onEinfärbenKämpfer = new Base.CommandBase(EinfärbenKämpfer, null);
                return onEinfärbenKämpfer;
            }
        }

        private void EinfärbenKämpfer(object obj)
        {
            if (SelectedKämpferInfo != null && SelectedKämpferInfo.Kämpfer != null && obj != null)
            {
                System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(obj.ToString());
                SelectedKämpferInfo.Kämpfer.Farbmarkierung = color;
            }

        }

        private Base.CommandBase onShowGegnerView = null;
        public Base.CommandBase OnShowGegnerView
        {
            get
            {
                if (onShowGegnerView == null)
                    onShowGegnerView = new Base.CommandBase(ShowGegnerView, null);
                return onShowGegnerView;
            }
        }

        private Base.CommandBase onShowBodenplanView = null;
        public Base.CommandBase OnShowBodenplanView
        {
            get
            {
                if (onShowBodenplanView == null)
                    onShowBodenplanView = new Base.CommandBase(ShowBodenplanView, null);
                return onShowBodenplanView;
            }
        }

        private ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> recentColors = Farbmarkierungen.RecentColors;
        public ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> RecentColors
        {
            get { return recentColors; }
            set
            {
                recentColors = value;
                Farbmarkierungen.RecentColors = value;
                OnChanged("RecentColors");
            }
        }

        private ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> standardColors = Farbmarkierungen.StandardColors;
        public ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> StandardColors
        {
            get { return standardColors; }
            set {
                Farbmarkierungen.StandardColors = value;
                standardColors = value;
                OnChanged("StandardColors");
            }
        }


        private void ShowGegnerView(object obj)
        {
            if (showGegnerView != null)
                showGegnerView(Kampf);
        }

        private Action<K> showGegnerView;

        private Action<KampfViewModel> showBodenplanView;

        private void ShowBodenplanView(object obj)
        {
            if (showBodenplanView != null)
                showBodenplanView(this);
        }


        private Base.CommandBase onNext = null;
        public Base.CommandBase OnNext
        {
            get
            {
                if (onNext == null)
                    onNext = new Base.CommandBase(Next, null);
                return onNext;
            }
        }

        private void Next(object obj)
        {
            var mi = Kampf.Next();
            if (mi != null)
            {
                //if(SelectedManöverInfo != null)
                    //SelectedManöverInfo.IsSelected = false;
                //mi.IsSelected = true;
                KämpferSelected = false;
                SelectedManöverInfo = mi;
            }
        }

        private Base.CommandBase onNextKampfrunde = null;
        public Base.CommandBase OnNextKampfrunde
        {
            get
            {
                if (onNextKampfrunde == null)
                    onNextKampfrunde = new Base.CommandBase(NextKampfrunde, null);
                return onNextKampfrunde;
            }
        }

        private void NextKampfrunde(object obj)
        {
            Kampf.NeueKampfrunde();
        }

        private Base.CommandBase onNewKampf = null;
        public Base.CommandBase OnNewKampf
        {
            get
            {
                if (onNewKampf == null)
                    onNewKampf = new Base.CommandBase(NewKampf, null);
                return onNewKampf;
            }
        }

        private void NewKampf(object obj)
        {
            Kampf.KampfNeuStarten();
        }

        private Base.CommandBase onAktionAusführen = null;
        public Base.CommandBase OnAktionAusführen
        {
            get
            {
                if (onAktionAusführen == null)
                    onAktionAusführen = new Base.CommandBase(AktionAusführen, null);
                return onAktionAusführen;
            }
        }

        private void AktionAusführen(object obj)
        {
            if (SelectedManöverInfo == null || SelectedManöverInfo.Manöver == null)
                return;
            SelectedManöverInfo.Manöver.Ausführen();
        }

        private Base.CommandBase onTrefferpunkte = null;
        public Base.CommandBase OnTrefferpunkte
        {
            get
            {
                if (onTrefferpunkte == null)
                    onTrefferpunkte = new Base.CommandBase(Trefferpunkte, null);
                return onTrefferpunkte;
            }
        }

        private void Trefferpunkte(object obj)
        {
            if (SelectedKämpferInfo == null)
                return;
            TrefferpunkteOptions opt = TrefferpunkteOptions.Default;
            if (obj is TrefferpunkteOptions)
                opt = (TrefferpunkteOptions)obj;
            opt |= WundschwellenOption | ausdauerSchadenMachtKeineSchadenspunkte;
            Kampf.Trefferpunkte(SelectedKämpferInfo.Kämpfer, Schaden, SelectedTrefferzone, opt);
        }

        private Base.CommandBase onKarmaenergieAbziehen = null;
        public Base.CommandBase OnKarmaenergieAbziehen
        {
            get
            {
                if (onKarmaenergieAbziehen == null)
                    onKarmaenergieAbziehen = new Base.CommandBase(KarmaenergieAbziehen, null);
                return onKarmaenergieAbziehen;
            }
        }

        private void KarmaenergieAbziehen(object obj)
        {
            if (SelectedKämpferInfo == null)
                return;
            SelectedKämpferInfo.Kämpfer.KarmaenergieAktuell -= Math.Max(Schaden, 0);
        }

        private Base.CommandBase onAstralenergieAbziehen = null;
        public Base.CommandBase OnAstralenergieAbziehen
        {
            get
            {
                if (onAstralenergieAbziehen == null)
                    onAstralenergieAbziehen = new Base.CommandBase(AstralenergieAbziehen, null);
                return onAstralenergieAbziehen;
            }
        }

        private void AstralenergieAbziehen(object obj)
        {
            if (SelectedKämpferInfo == null)
                return;
            SelectedKämpferInfo.Kämpfer.AstralenergieAktuell -= Math.Max(Schaden, 0);
        }

        private Base.CommandBase onInitiativeWürfeln = null;
        public Base.CommandBase OnInitiativeWürfeln
        {
            get
            {
                if (onInitiativeWürfeln == null)
                    onInitiativeWürfeln = new Base.CommandBase(InitiativeWürfeln, null);
                return onInitiativeWürfeln;
            }
        }

        private void InitiativeWürfeln(object obj)
        {
            if (SelectedKämpferInfo == null || SelectedKämpfer == null)
                return;
            SelectedKämpferInfo.Initiative = SelectedKämpfer.Initiative(true);
        }

        private Base.CommandBase onOrientieren = null;
        public Base.CommandBase OnOrientieren
        {
            get
            {
                if (onOrientieren == null)
                    onOrientieren = new Base.CommandBase(Orientieren, null);
                return onOrientieren;
            }
        }

        private void Orientieren(object obj)
        {
            if (SelectedKämpferInfo == null || SelectedKämpfer == null)
                return;
            int? ini = SelectedKämpfer.Orientieren(true);
            if (ini.HasValue)
                SelectedKämpferInfo.Initiative = ini.Value;
        }

        #endregion // ---- COMMANDS ----

        #region Subklassen
        public class KämpferNahkampfwaffe : Logic.INahkampfwaffe
        {
            private Held _held;
            private Waffe _waffe;
            private GegnerBase_Angriff _gegner_angriff;

            public KämpferNahkampfwaffe(Held held, Waffe waffe)
            {
                _held = held; _waffe = waffe;
            }

            public KämpferNahkampfwaffe(GegnerBase_Angriff ga)
            {
                _gegner_angriff = ga;
            }

            //public Logic.IKämpfer Kämpfer
            //{
            //    get
            //    {
            //        if (_gegner_angriff != null)
            //            return _gegner_angriff.Gegner;
            //        return _held;
            //    }
            //}

            public Logic.Distanzklasse Distanzklasse
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.Distanzklasse;
                    return _waffe.Distanzklasse;
                }
            }

            public string Name
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.Name;
                    return _waffe.Name;
                }
            }

            public int TPWürfel
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.TPWürfel;
                    return _waffe.TPWürfel;
                }
            }

            public int TPWürfelAnzahl
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.TPWürfelAnzahl;
                    return _waffe.TPWürfelAnzahl;
                }
            }

            public int TPBonus
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.TPBonus;
                    return _waffe.TPBonus;
                }
            }

            public int TPKKBonus
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.TPKKBonus;
                    return _waffe.TPKKBonus(_held);
                }
            }

            public int AT
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.AT;
                    return 0;
                }
            }

            public int PA
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.PA;
                    return 0;
                }
            }
        }

        public class KämpferFernkampfwaffe : Logic.IFernkampfwaffe
        {
            private Held _held;
            private Model.Fernkampfwaffe _waffe;
            private GegnerBase_Angriff _gegner_angriff;

            public KämpferFernkampfwaffe(Held held, Model.Fernkampfwaffe waffe)
            {
                _held = held; _waffe = waffe;
            }

            public KämpferFernkampfwaffe(GegnerBase_Angriff ga)
            {
                _gegner_angriff = ga;
            }

            //public Logic.IKämpfer Kämpfer
            //{
            //    get
            //    {
            //        if (_gegner_angriff != null)
            //            return _gegner_angriff.Gegner;
            //        return _held;
            //    }
            //}

            public int? RWSehrNah
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.RWSehrNah;
                    return _waffe.RWSehrNah;
                }
            }

            public int? RWNah
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.RWNah;
                    return _waffe.RWNah;
                }
            }

            public int? RWMittel
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.RWMittel;
                    return _waffe.RWMittel;
                }
            }

            public int? RWWeit
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.RWWeit;
                    return _waffe.RWWeit;
                }
            }

            public int? RWSehrWeit
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.RWSehrWeit;
                    return _waffe.RWSehrWeit;
                }
            }

            public int? TPSehrNah
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.TPSehrNah;
                    return _waffe.TPSehrNah;
                }
            }

            public int? TPNah
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.TPNah;
                    return _waffe.TPNah;
                }
            }

            public int? TPMittel
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.TPMittel;
                    return _waffe.TPMittel;
                }
            }

            public int? TPWeit
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.TPWeit;
                    return _waffe.TPWeit;
                }
            }

            public int? TPSehrWeit
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.TPSehrWeit;
                    return _waffe.TPSehrWeit;
                }
            }

            public string Name
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.Name;
                    return _waffe.Name;
                }
            }

            public int TPWürfel
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.TPWürfel;
                    return _waffe.TPWürfel ?? 0;
                }
            }

            public int TPWürfelAnzahl
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.TPWürfelAnzahl;
                    return _waffe.TPWürfelAnzahl ?? 0;
                }
            }

            public int TPBonus
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.TPBonus;
                    return _waffe.TPBonus ?? 0;
                }
            }

            public int TPKKBonus
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.TPKKBonus;
                    return _waffe.TPKKBonus(_held);
                }
            }

            public int AT
            {
                get
                {
                    if (_gegner_angriff != null)
                        return _gegner_angriff.AT;
                    return 0;
                }
            }
        }
        #endregion

        //Command NeueKampfrunde
    }

    public class TrefferpunkteOptionsConverter : System.Windows.Data.IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TrefferpunkteOptions opt = TrefferpunkteOptions.Default;
            foreach (object o in values)
            {
                opt |= (TrefferpunkteOptions)o;
            }
            return opt;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            List<TrefferpunkteOptions> opt = new List<TrefferpunkteOptions>();
            foreach (var o in Enum.GetValues(typeof(TrefferpunkteOptions)))
            {
                if (((TrefferpunkteOptions)value & (TrefferpunkteOptions)o) == (TrefferpunkteOptions)o)
                    opt.Add((TrefferpunkteOptions)o);
            }
            if(opt.Count == 0)
                return new object[] {TrefferpunkteOptions.Default};
            return opt.Select(a => (object)a).ToArray();

        }
    }
}
