using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Bodenplan;
using MeisterGeister.ViewModel.Kampf.Logic;
using K = MeisterGeister.ViewModel.Kampf.Logic.Kampf;
using MeisterGeister.ViewModel.AudioPlayer.Logic;
using MeisterGeister.ViewModel.Bodenplan.Logic;

namespace MeisterGeister.ViewModel.Kampf
{
    public class KampfViewModel : Base.ToolViewModelBase
    {

        public KampfViewModel() : this((k => new MeisterGeister.View.Kampf.GegnerWindow(k).ShowDialog()), View.General.ViewHelper.Confirm)
        {
        }

        public KampfViewModel(Action<K> showGegnerView, Func<string, string, bool> confirm)
            : base(confirm, View.General.ViewHelper.ShowError)
        {
            Global.CurrentKampf = this;
            this.showGegnerView = showGegnerView;
            _kampf = new K();
        }

        private K _kampf = null;
        public K Kampf
        {
            get { return _kampf; }
            set
            {
                if (_kampf != null)
                {

                }
                Set(ref _kampf, value);
                if (_kampf != null)
                {

                }
            }
        }

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
        
        private BattlegroundViewModel bodenplanViewModel = null;
        public BattlegroundViewModel BodenplanViewModel
        {
            get {
                if (bodenplanViewModel == null)
                    BodenplanViewModel = new BattlegroundViewModel();
                return bodenplanViewModel; 
            }
            private set
            {
                //if (
                Set(ref bodenplanViewModel, value);
                    //)
                    //value.KampfVM = this;
            }
        }

        private ICollection<IWesenPlaylist> _wesenPlaylist = null;
        public ICollection<IWesenPlaylist> WesenPlaylist
        {
            get
            {
                return ((SelectedKämpfer != null && SelectedKämpfer.Kämpfer != null) && (SelectedKämpfer.Kämpfer is Held)) ?
                    new ObservableCollection<IWesenPlaylist>((SelectedKämpfer.Kämpfer as Held).Held_Audio_Playlist.AsEnumerable<IWesenPlaylist>()) :

                    ((SelectedKämpfer != null && SelectedKämpfer.Kämpfer != null) && (SelectedKämpfer.Kämpfer is GegnerBase)) ?
                    new ObservableCollection<IWesenPlaylist>((SelectedKämpfer.Kämpfer as GegnerBase).GegnerBase_Audio_Playlist.AsEnumerable<IWesenPlaylist>()) :

                    ((SelectedKämpfer != null && SelectedKämpfer.Kämpfer != null) && (SelectedKämpfer.Kämpfer is Gegner)) ?
                    new ObservableCollection<IWesenPlaylist>((SelectedKämpfer.Kämpfer as Gegner).GegnerBase.GegnerBase_Audio_Playlist.AsEnumerable<IWesenPlaylist>()) :

                    null;
            }
            set
            {
                Set(ref _wesenPlaylist, value);
            }
        }



        private ManöverInfo selectedManöver;
        public ManöverInfo SelectedManöver
        {
            get { return selectedManöver; }
            set
            {
                Set(ref selectedManöver, value);
                if (value != null)
                    SelectedKämpfer = null;
            }
        }

        private KämpferInfo _selectedKämpfer;
        public KämpferInfo SelectedKämpfer
        {
            get
            {
                return _selectedKämpfer;
            }
            set
            {
                if (value != null && _selectedKämpfer != null &&
                    value.Kämpfer.Name == _selectedKämpfer.Kämpfer.Name) return;
                Set(ref _selectedKämpfer, value);
                if (value != null)
                {
                    SelectedManöver = null;
                    if (BodenplanViewModel.SelectedObject == null ||
                        ((BodenplanViewModel.SelectedObject is IKämpfer) &&
                         (BodenplanViewModel.SelectedObject as IKämpfer).Name != value.Kämpfer.Name))
                        BodenplanViewModel.SelectedObject = BodenplanViewModel.BattlegroundObjects
                            .Where(t => t is IKämpfer)
                            .FirstOrDefault(tt => tt as IKämpfer == value.Kämpfer);
                }
            }
        }
        
        private btnHotkeyVM _speedbtnAudio = new btnHotkeyVM();
        private btnHotkeyVM SpeedbtnAudio
        {
            get { return _speedbtnAudio; }
            set { Set(ref _speedbtnAudio, value); }
        }

        #region // ---- COMMANDS ----

        //private Base.CommandBase _onDeleteKämpfer = null;
        //public Base.CommandBase OnDeleteKämpfer
        //{
        //    get
        //    {
        //        if (_onDeleteKämpfer == null)
        //            _onDeleteKämpfer = new Base.CommandBase(DeleteKämpfer, null);
        //        return _onDeleteKämpfer;
        //    }
        //}

        //private void DeleteKämpfer(object obj)
        //{
        //    if (BodenplanViewModel != null)
        //        BodenplanViewModel.RemoveCreature(SelectedKämpfer.Kämpfer);
        //    Kampf.Kämpfer.Remove(SelectedKämpfer.Kämpfer);

        //}

        private Base.CommandBase _onWesenPlaylistClick = null;
        public Base.CommandBase OnWesenPlaylistClick
        {
            get
            {
                if (_onWesenPlaylistClick == null)
                    _onWesenPlaylistClick = new Base.CommandBase(WesenPlaylistClick, null);
                return _onWesenPlaylistClick;
            }
        }

        private void WesenPlaylistClick(object obj)
        {
            SpeedbtnAudio.aPlaylistGuid = (obj as IWesenPlaylist).Audio_Playlist.Audio_PlaylistGUID;
            SpeedbtnAudio.aPlaylist = (obj as IWesenPlaylist).Audio_Playlist;
            SpeedbtnAudio.OnBtnClick(SpeedbtnAudio);
        }

        private Base.CommandBase onAddHelden = null;
        public Base.CommandBase OnAddHelden
        {
            get
            {
                if (onAddHelden == null)
                    onAddHelden = new Base.CommandBase((o) => AddHelden(), null);
                return onAddHelden;
            }
        }

        private void AddHelden()
        {
            KämpferInfo ki = null;
            foreach (Model.Held held in Global.ContextHeld.HeldenGruppeListe)
            {
                if (!Kampf.Kämpfer.Any(k => k.Kämpfer == held))
                {
                    ki = new KämpferInfo(held, Kampf);
                    Kampf.Kämpfer.Add(held);
                }
            }
            if (BodenplanViewModel != null)
                BodenplanViewModel.AddAllCreatures();
        }

        private Base.CommandBase onDeleteKämpfer = null;
        public Base.CommandBase OnDeleteKämpfer
        {
            get
            {
                if (onDeleteKämpfer == null)
                    onDeleteKämpfer = new Base.CommandBase((o) => DeleteKämpfer(), null);
                return onDeleteKämpfer;
            }
        }

        public void DeleteKämpfer()
        {
            if (SelectedKämpfer != null && Confirm("Kämpfer entfernen", String.Format("Soll der Kämpfer {0} entfernt werden?", SelectedKämpfer.Kämpfer.Name)))
            {
                IKämpfer k = SelectedKämpfer.Kämpfer;
                if (BodenplanViewModel != null)
                    BodenplanViewModel.RemoveCreature(k);
                Kampf.Kämpfer.Remove(SelectedKämpfer);
            }
        }

        private Base.CommandBase onDeleteAllKämpfer = null;
        public Base.CommandBase OnDeleteAllKämpfer
        {
            get
            {
                if (onDeleteAllKämpfer == null)
                    onDeleteAllKämpfer = new Base.CommandBase((o) => DeleteAllKämpfer(), null);
                    return onDeleteAllKämpfer;
            }
        }

        private void DeleteAllKämpfer()
        {
            if (Confirm("Liste leeren", "Sollen alle Kämpfer entfernt werden?"))
            {
                Kampf.Kämpfer.Clear();
                if (BodenplanViewModel != null)
                    BodenplanViewModel.RemoveCreatureAll();
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
            if (SelectedKämpfer != null && SelectedKämpfer.Kämpfer != null && obj != null)
            {
                System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(obj.ToString());
                SelectedKämpfer.Kämpfer.Farbmarkierung = color;
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
            set
            {
                Farbmarkierungen.StandardColors = value;
                standardColors = value;
                OnChanged("StandardColors");
            }
        }


        private void ShowGegnerView(object obj)
        {
            if (showGegnerView != null)
                showGegnerView(Kampf);
            if (BodenplanViewModel != null)
                BodenplanViewModel.AddAllCreatures();
        }

        private Action<K> showGegnerView;
        public Action<K> ShowGegnerViewAction
        {
            get { return showGegnerView; }
            set { showGegnerView = value; }
        }
        
        private Action<KampfViewModel> showBodenplanView;

        public Action<KampfViewModel> ShowBodenplanViewAction
        {
            get { return showBodenplanView; }
            set { showBodenplanView = value; }
        }

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
            Kampf.Next();
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
            Global.CurrentKampf = this;
        }

        //private Base.CommandBase onAktionAusführen = null;
        //public Base.CommandBase OnAktionAusführen
        //{
        //    get
        //    {
        //        if (onAktionAusführen == null)
        //            onAktionAusführen = new Base.CommandBase(AktionAusführen, null);
        //        return onAktionAusführen;
        //    }
        //}

        //private void AktionAusführen(object obj)
        //{
        //    if (SelectedManöverInfo == null || SelectedManöverInfo.Manöver == null)
        //        return;
        //    SelectedManöverInfo.Manöver.Ausführen();
        //}

        //private Base.CommandBase onTrefferpunkte = null;
        //public Base.CommandBase OnTrefferpunkte
        //{
        //    get
        //    {
        //        if (onTrefferpunkte == null)
        //            onTrefferpunkte = new Base.CommandBase(Trefferpunkte, null);
        //        return onTrefferpunkte;
        //    }
        //}

        //private void Trefferpunkte(object obj)
        //{
        //    if (SelectedKämpferInfo == null)
        //        return;
        //    TrefferpunkteOptions opt = TrefferpunkteOptions.Default;
        //    if (obj is TrefferpunkteOptions)
        //        opt = (TrefferpunkteOptions)obj;
        //    opt |= WundschwellenOption | ausdauerSchadenMachtKeineSchadenspunkte;
        //    Kampf.Trefferpunkte(SelectedKämpferInfo.Kämpfer, Schaden, SelectedTrefferzone, opt);
        //}

        //private Base.CommandBase onKarmaenergieAbziehen = null;
        //public Base.CommandBase OnKarmaenergieAbziehen
        //{
        //    get
        //    {
        //        if (onKarmaenergieAbziehen == null)
        //            onKarmaenergieAbziehen = new Base.CommandBase(KarmaenergieAbziehen, null);
        //        return onKarmaenergieAbziehen;
        //    }
        //}

        //private void KarmaenergieAbziehen(object obj)
        //{
        //    if (SelectedKämpferInfo == null)
        //        return;
        //    SelectedKämpferInfo.Kämpfer.KarmaenergieAktuell -= Math.Max(Schaden, 0);
        //}

        //private Base.CommandBase onAstralenergieAbziehen = null;
        //public Base.CommandBase OnAstralenergieAbziehen
        //{
        //    get
        //    {
        //        if (onAstralenergieAbziehen == null)
        //            onAstralenergieAbziehen = new Base.CommandBase(AstralenergieAbziehen, null);
        //        return onAstralenergieAbziehen;
        //    }
        //}

        //private void AstralenergieAbziehen(object obj)
        //{
        //    if (SelectedKämpferInfo == null)
        //        return;
        //    SelectedKämpferInfo.Kämpfer.AstralenergieAktuell -= Math.Max(Schaden, 0);
        //}

        //private Base.CommandBase onInitiativeWürfeln = null;
        //public Base.CommandBase OnInitiativeWürfeln
        //{
        //    get
        //    {
        //        if (onInitiativeWürfeln == null)
        //            onInitiativeWürfeln = new Base.CommandBase(InitiativeWürfeln, null);
        //        return onInitiativeWürfeln;
        //    }
        //}

        //private void InitiativeWürfeln(object obj)
        //{
        //    if (SelectedKämpferInfo == null || SelectedKämpfer == null)
        //        return;
        //    SelectedKämpferInfo.Initiative = SelectedKämpfer.Initiative(true);
        //}

        //private Base.CommandBase onOrientieren = null;
        //public Base.CommandBase OnOrientieren
        //{
        //    get
        //    {
        //        if (onOrientieren == null)
        //            onOrientieren = new Base.CommandBase(Orientieren, null);
        //        return onOrientieren;
        //    }
        //}

        //private void Orientieren(object obj)
        //{
        //    if (SelectedKämpferInfo == null || SelectedKämpfer == null)
        //        return;
        //    int? ini = SelectedKämpfer.Orientieren(true);
        //    if (ini.HasValue)
        //        SelectedKämpferInfo.Initiative = ini.Value;
        //}

        #endregion // ---- COMMANDS ----

        //Command NeueKampfrunde
    }

    public class MultiBooleanAndConverter : System.Windows.Data.IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            foreach (object value in values)
            {
                if (((value is bool) && (bool)value == false) || value == null)
                {
                    return false;
                }
            }
            return true;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("BooleanAndConverter is a OneWay converter.");
        }
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
            if (opt.Count == 0)
                return new object[] { TrefferpunkteOptions.Default };
            return opt.Select(a => (object)a).ToArray();

        }
    }
}
