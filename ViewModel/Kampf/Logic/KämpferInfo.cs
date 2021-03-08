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
using System.Diagnostics;
using System.Windows;
using MeisterGeister.View.General;
using MeisterGeister.ViewModel.Base;
using MeisterGeister.ViewModel.AudioPlayer.Logic;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Bodenplan.Logic;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    [Serializable]
    public class KämpferInfo : ViewModelBase, IDisposable
    {
        #region Commands

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

        private SchadenMachen schadenMachen;
        public SchadenMachen SchadenMachen
        {
            get
            {
                if (schadenMachen == null)
                    schadenMachen = new SchadenMachen(Kämpfer);
                return schadenMachen;
            }
        }

        private CommandBase entfernen;
        public CommandBase Entfernen
        {
            get
            {
                if (entfernen == null)
                    entfernen = new CommandBase((o) => DeleteKämpfer(), null);

                return entfernen;
                
            }
        }

        public void DeleteKämpfer()
        {
            if (Kämpfer != null && ViewHelper.Confirm("Kämpfer entfernen", String.Format("Soll der Kämpfer {0} entfernt werden?", Kämpfer.Name)))
            {                
                if (Global.CurrentKampf.BodenplanViewModel != null)
                    Global.CurrentKampf.BodenplanViewModel.RemoveCreature(Kämpfer);
                Global.CurrentKampf.Kampf.Kämpfer.Remove(Kämpfer);
            }
        }

        private CommandBase iniNeu;
        public CommandBase INIneu
        {
            get
            {
                if (iniNeu == null)
                    iniNeu = new CommandBase((o) => INIneuWürfeln(), null);
                return iniNeu;                
            }
        }

        public void INIneuWürfeln()
        {
            Initiative = Kämpfer.Initiative();
        }

        private CommandBase passiv;
        public CommandBase Passiv
        {
            get
            {
                if (passiv == null)
                    passiv = new CommandBase((o) => PassivKämpfer(), null);
                return passiv;                
            }
        }

        public void PassivKämpfer()
        {
            Abwehraktionen = Aktionen;
            OnChanged(nameof(Angriffsaktionen));
            IstPassiv = true;
        }

        private CommandBase unsichtbar;
        public CommandBase Unsichtbar
        {
            get
            {
                if (unsichtbar == null)
                    unsichtbar = new CommandBase((o) => UnsichtbarKämpfer(), null);
                return unsichtbar;                
            }
        }

        public void UnsichtbarKämpfer()
        {
            IstUnsichtbar = !IstUnsichtbar;
        }

        private CommandBase anführer;
        public CommandBase Anführer
        {
            get
            {
                if (anführer == null)
                    anführer = new CommandBase((o) => AnführerKämpfer(), null); 

                return anführer;                
            }
        }

        public void AnführerKämpfer()
        {
            IstAnführer = !IstAnführer;
        }

        #endregion

        private IKämpfer _kämpfer;

        public IKämpfer Kämpfer
        {
            get { return _kämpfer; }
            private set
            {
                //Auf Änderung von InitiativeBasis und InitiativeWurf hören und den Aktuellen INI-Wert anpassen. Dafür muss die Veränderung zum Basiswert gespeichert werden.
                if (_kämpfer == value)
                    return;
                if (_kämpfer != null)
                    _kämpfer.PropertyChanged -= Kämpfer_PropertyChanged;
                Set(ref _kämpfer, value);
                if (_kämpfer != null)
                    _kämpfer.PropertyChanged += Kämpfer_PropertyChanged;
            }
        }

        private btnHotkeyVM _speedbtnAudio = new btnHotkeyVM();
        public btnHotkeyVM SpeedbtnAudio
        {
            get { return _speedbtnAudio; }
            set { Set(ref _speedbtnAudio, value); }
        }

        private ICollection<IWesenPlaylist> _wesenPlaylist;
        public ICollection<IWesenPlaylist> WesenPlaylist
        {
            get { return _wesenPlaylist; }
            set { Set(ref _wesenPlaylist, value); }
        }

        private Dictionary<string, ManöverModifikator<INahkampfwaffe>> _preAngriffsMods = null;
        public Dictionary<string, ManöverModifikator<INahkampfwaffe>> PreAngriffsMods
        {
            get { return _preAngriffsMods; }
            set { Set(ref _preAngriffsMods, value); }
        }

        private IFernkampfwaffe _preFernkampfWaffe = null;
        public IFernkampfwaffe PreFernkampfWaffe
        {
            get { return _preFernkampfWaffe; }
            set { Set(ref _preFernkampfWaffe, value); }
        }
        private Dictionary<string, ManöverModifikator<IFernkampfwaffe>> _preFernkampfMods = null;
        public Dictionary<string, ManöverModifikator<IFernkampfwaffe>> PreFernkampfMods
        {
            get { return _preFernkampfMods; }
            set { Set(ref _preFernkampfMods, value); }
        }

        private Dictionary<string, ManöverModifikator<INahkampfwaffe>> _preAbwehrMods = null;
        public Dictionary<string, ManöverModifikator<INahkampfwaffe>> PreAbwehrMods
        {
            get { return _preAbwehrMods; }
            set { Set(ref _preAbwehrMods, value); }
        }

        private double _audioSpeedButtonVolume = MeisterGeister.Logic.Einstellung.Einstellungen.GeneralHotkeyVolume;
        public double AudioSpeedButtonVolume
        {
            get { return _audioSpeedButtonVolume; }
            set
            {
                Set(ref _audioSpeedButtonVolume, value);
                MeisterGeister.Logic.Einstellung.Einstellungen.GeneralHotkeyVolume = Convert.ToInt32(value);
            }
        }

        public bool LebensbalkenImmerAnzeigen
        { get { return MeisterGeister.Logic.Einstellung.Einstellungen.LebensbalkenImmerAnzeigen; } }

        #region Initiative

        //private const int RANDOM_SIZE = 100000;

        private decimal Kommastellen(int initiative)
        {
            decimal kommas = initiative;
            return kommas / 100;
            //decimal random = new Random(GetHashCode()).Next(0, RANDOM_SIZE);
            //return kommas / 100 + random / (RANDOM_SIZE * 100);
        }

        public decimal InitiativeMitKommas
        {
            get
            {
                return Initiative + Kommastellen(Kämpfer.InitiativeBasis);
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
                _initiative = Math.Max(0, value);
                try
                {
                    int bonus = Math.Max((int)Math.Floor((_initiative - 11) / 10.0), 0);
                    FreieAktionen = 2 + bonus;
                    Kämpfer.Modifikatoren.RemoveAll(m => m is Mod.PABonusDurchHoheIni);
                    if (bonus > 0)
                        Kämpfer.Modifikatoren.Add(new Mod.PABonusDurchHoheIni(bonus));
                    //Wenn INI < 0 -> Kämpfer verliert eine (Angriffs)Aktion
                    AktionenBerechnen();

                    //Initiative wird hier erst mit NotifyChanged bekanntgegeben da die Aktionen und Reaktionen davon abhängen
                    //Dazwischen sollen die Aktionen allerdings neu berechnet werden
                    OnChanged(nameof(Initiative));
                    // ????? notwendig                
                    if (Global.CurrentKampf.BodenplanViewModel != null && Global.CurrentKampf.BodenplanViewModel.IsShowIniKampf)
                        Global.CurrentKampf.BodenplanViewModel.SetIniWindowWidth(true);
                }
                catch (Exception ex)
                { ShowError("Beim Ändern der Initiative ist ein Fehler aufgetreten.", ex); }
            }
        }
        #endregion

        private int _team;
        public int Team
        {
            get { return _team; }
            set
            {
                Set(ref _team, value);

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

        public void NotifyIndexChanged()
        {
            OnChanged(nameof(Index));
        }

        private Kampf kampf;
        public Kampf Kampf
        {
            get { return kampf; }
            set { Set(ref kampf, value); }
        }

        private bool _istPassiv = false;
        public bool IstPassiv
        {
            get { return _istPassiv; }
            set { Set(ref _istPassiv, value);  }
        }


        private bool _istUnsichtbar = false;
        public bool IstUnsichtbar
        {
            get { return _istUnsichtbar; }
            set { Set(ref _istUnsichtbar, value); (Kämpfer as Wesen).Opacity = value ? .1 : 1;   }
        }

        private double _lichtquelleMeter = 0;
        public double LichtquelleMeter
        {
            get { return _lichtquelleMeter; }
            set
            {
                Set(ref _lichtquelleMeter, value);
                LichtquellePixel = value * 100;
            }
        }

        private double _lightCreatureX = 0;
        public double LightCreatureX
        {
            get { return _lightCreatureX; }
            set { Set(ref _lightCreatureX, value); }
        }
        private double _lightCreatureY = 0;
        public double LightCreatureY
        {
            get { return _lightCreatureY; }
            set { Set(ref _lightCreatureY, value); }
        }

        private double _lichtquellePixel = 0;
        public double LichtquellePixel
        {
            get { return _lichtquellePixel; }
            set
            {
                Set(ref _lichtquellePixel, value);

                Bodenplan.Logic.BattlegroundCreature bgC = (Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects
                    .Where(t => t is Bodenplan.Logic.BattlegroundCreature)
                    .FirstOrDefault(s => (s as Bodenplan.Logic.BattlegroundCreature).ki == this) as Bodenplan.Logic.BattlegroundCreature);

                Bodenplan.Logic.LichtquelleObject lichtObj =
                    Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects
                        .Where(t => t is Bodenplan.Logic.LichtquelleObject)
                        .Where(t => (t as Bodenplan.Logic.LichtquelleObject).iKämpfer == Global.CurrentKampf.BodenplanViewModel.SelectedObject as IKämpfer).FirstOrDefault() as Bodenplan.Logic.LichtquelleObject;

                if (value != 0)
                {
                    BattlegroundCreature bgCreature = Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects
                        .Where(t => t is BattlegroundCreature)
                        .FirstOrDefault(t => (t as BattlegroundCreature).ki == this) as BattlegroundCreature;


                    //Nur ein Lichquellenobject erzeugen, wenn auch ein Kämpfer existiert. 
                    //Ist dies nicht der Fall sind wir im Lade-Zyklus. Die Lichtquelle wird dann nach allen Kämpfern erstellt
                    if (bgCreature == null)
                        return;

                    bool istNeu = false;
                    if (lichtObj == null)
                    {
                        istNeu = true;
                        lichtObj = new Bodenplan.Logic.LichtquelleObject();
                    }
                    KämpferInfo kämpInfo = bgCreature.ki;
                    lichtObj.ki = kämpInfo;

                    lichtObj.LichtquellePixelRadius = 10 + value + value + bgC.CreatureWidth;
                    lichtObj.LightCreatureX = bgC.CreatureX - LichtquellePixel;
                    lichtObj.LightCreatureY = bgC.CreatureY - LichtquellePixel;

                    if (istNeu)
                    {
                        var firstCreature = Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects.FirstOrDefault(t => t is BattlegroundCreature);
                        int index = Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects.IndexOf(firstCreature);
                        Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects.Add(lichtObj);
                        Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects.Move(
                            Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects.Count - 1, index);
                    }
                }
                else
                {
                    if (lichtObj != null)
                        Global.CurrentKampf.BodenplanViewModel.BattlegroundObjects.Remove(lichtObj);
                }
                if (bgC != null)
                {
                    LichtquellePixelRadius = 10 + value + value + bgC.CreatureWidth;
                    LightCreatureX = bgC.CreatureX - LichtquellePixel;
                    LightCreatureY = bgC.CreatureY - LichtquellePixel;
                }

                if (LichtquelleMeter != value / 100) LichtquelleMeter = value / 100;                
            }
        }

        private double _lichtquellePixelRadius = 0;
        public double LichtquellePixelRadius
        {
            get { return _lichtquellePixelRadius; }
            set { Set(ref _lichtquellePixelRadius, value); }
        }

        private bool _istImKampf = true;
        public bool IstImKampf
        {
            get { return _istImKampf; }
            set 
            { 
                Set(ref _istImKampf, value);
                if (!value)
                {
                    Global.CurrentKampf.Kampf.Kämpfer.Remove(Kämpfer);
                }
                else
                    Global.CurrentKampf.Kampf.Kämpfer.Add(Kämpfer);
            }
        }

        private bool _istAnführer = false;
        public bool IstAnführer
        {
            get { return _istAnführer; }
            set { Set(ref _istAnführer, value); }
        }

        private Nullable<Position> positionSelbst;
        public Nullable<Position> PositionSelbst
        {
            get { return positionSelbst?? Position.Stehend; }
            set
            {
                if (value == null)
                {
                    value = positionSelbst?? Position.Stehend;
                }
                Set(ref positionSelbst, value);
                
                foreach (ManöverInfo mi in Kampf.InitiativListe)
                {
                    KampfManöver<IWaffe> manöver = mi.Manöver as KampfManöver<IWaffe>;
                    if (manöver != null)
                        ((ManöverModifikator<Position, IWaffe>)manöver.Mods["PositionSelbst"]).Value = value.Value; 
                }
            }
        }

        public KämpferInfo()
        { }

        public KämpferInfo(IKämpfer k, Kampf kampf)
        {             
            if (k == null)
                throw new ArgumentNullException("IKämpfer k darf nicht null sein.");
            if (kampf == null)
                throw new ArgumentNullException("Kampf kampf darf nicht null sein.");
            Kämpfer = k;
            Kampf = kampf;
            Team = 1;
            Initiative = k.Initiative();
            if (k is IGegnerBase)
                (k as MeisterGeister.Model.Gegner).KämpferTempName = "Gegner " + (kampf.Kämpfer.Where(t => t.Team == 2).ToList().Count + 1);

            WesenPlaylist = (k is MeisterGeister.Model.Held) ?
                new ObservableCollection<IWesenPlaylist>((k as MeisterGeister.Model.Held).Held_Audio_Playlist.AsEnumerable<IWesenPlaylist>()) :
                (k is MeisterGeister.Model.GegnerBase) ?
                    new ObservableCollection<IWesenPlaylist>((k as MeisterGeister.Model.GegnerBase).GegnerBase_Audio_Playlist.AsEnumerable<IWesenPlaylist>()) :
                    new ObservableCollection<IWesenPlaylist>((k as MeisterGeister.Model.Gegner).GegnerBase.GegnerBase_Audio_Playlist.AsEnumerable<IWesenPlaylist>());
            
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;
        }

        private void Kämpfer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "InitiativeBasis" || e.PropertyName == "InitiativeWurf")
            {
                Initiative = Kämpfer.InitiativeBasis + Kämpfer.InitiativeWurf;
            }
            if (e.PropertyName == "Position")
                PositionSelbst = Kämpfer.Position;
        }
        
        #region Aktionen
        private int _aktionen = 2;
        [DependentProperty("Initiative")]
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
                //hier kein OnChanged einbinden
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
                Set(ref _freieAktionen, value);
            }
        }

        [DependentProperty("Angriffsaktionen")]
        public int MaxAngriffsaktionen
        {
            get 
            { return Angriffsaktionen >= 2? Angriffsaktionen: 2; }
        }

        public bool IgnoreChgAktionen = false;

        private int _angriffsaktionen = 1;
        [DependentProperty("Initiative")]
        public int Angriffsaktionen
        {
            get { return _angriffsaktionen; }
            set
            {
                //Check Längerfristige Handlung ist aktiv und Umwandeln in Attacke-Aktion
                ManöverInfo mi = Global.CurrentKampf.Kampf.InitiativListe
                                .Where(z => z.AktKampfrunde == Global.CurrentKampf.Kampf.Kampfrunde)
                                .FirstOrDefault(t => t.Manöver.Ausführender.Kämpfer == this.Kämpfer);
                if (mi == null)
                {
                    mi = Global.CurrentKampf.Kampf.InitiativListe.LastOrDefault(t => t.Manöver.Ausführender.Kämpfer == this.Kämpfer);
                }
                if (mi != null && !IgnoreChgAktionen && mi.Manöver.GetType() != typeof(Manöver.Attacke))
                {
                    if (ViewHelper.ConfirmYesNoCancel("Längerfristige Handlung unterbrechen", "Durch die Änderung der Aktionen wird die längerfristige Handlung unterbrochen." + Environment.NewLine + Environment.NewLine +
                        this.Kämpfer.Name + " will ein " + (
                        mi.Manöver.GetType() == typeof(Manöver.FernkampfManöver) ? "Fernkampf '(" + 
                        ((mi.Manöver as Manöver.FernkampfManöver).FernkampfWaffeSelected != null?
                            (mi.Manöver as Manöver.FernkampfManöver).FernkampfWaffeSelected.Name:
                            "unbekannt" ) + ")":
                        mi.Manöver.GetType() == typeof(Manöver.Zauber) ? "Zauber: '" +
                            ((mi.Manöver as Manöver.Zauber).Held_Zauber != null? (mi.Manöver as Manöver.Zauber).Held_Zauber.Zauber.Name :
                            (mi.Manöver as Manöver.Zauber).GegnerBase_Zauber != null? (mi.Manöver as Manöver.Zauber).GegnerBase_Zauber.Zauber.Name : ""):
                        mi.Manöver.GetType() == typeof(Manöver.SonstigesManöver) ? "sonstiges Manöver: '" +
                        (mi.Manöver as Manöver.SonstigesManöver).Name :
                        " unbekanntes Manöver" ) + "' durchfürhen." + Environment.NewLine + Environment.NewLine +
                        "Diese Handlung würde noch " + mi.Manöver.VerbleibendeDauer + " Aktionen dauern." + Environment.NewLine + Environment.NewLine +
                        "Soll die Handlung unterbrochen werden?") != 2) return;

                    ((Wesen)mi.Manöver.Ausführender.Kämpfer).AktVerbleibendeDauer = null;
                    mi.Manöver.VerbleibendeDauer = 0;
                    Global.CurrentKampf.Kampf.SortedInitiativListe =
                        Global.CurrentKampf.Kampf.InitiativListe.Where(t => t.AktKampfrunde == Global.CurrentKampf.Kampf.Kampfrunde).OrderByDescending(t => t.Start.InitiativPhase);
                    mi.UmwandelnAttacke.Execute(null);
                }

                if (value < 0)
                    value = 0;
                if (value > Aktionen)
                    value = Aktionen;
                _angriffsaktionen = value;
                _abwehraktionen = Aktionen - _angriffsaktionen;
                AktionenNeuSetzen();
                Kampf.SortedInitiativListe = Kampf.InitiativListe != null ?
                    (this.Kampf.Kampfrunde == 0 ? //.AktuelleAktionszeit.Kampfrunde
                    Kampf.InitiativListe.OrderByDescending(t => t.Start.InitiativPhase) :
                    Kampf.InitiativListe.Where(t => t.Start.Kampfrunde == this.Kampf.Kampfrunde).OrderByDescending(t => t.Start.InitiativPhase)
                    )
                    : null;
                if (Global.CurrentKampf.BodenplanViewModel != null && Global.CurrentKampf.BodenplanViewModel.KampfWindow != null)                
                {
                    Global.CurrentKampf.BodenplanViewModel.SetIniWindowWidth(true);
                }
                OnChanged(nameof(AngriffsaktionenÜbrig));
                OnChanged(nameof(VerbrauchteAbwehraktionen));
                //.RaiseEvent(new RoutedEventArgs(si))
                //VM.KampfWindow.SizeChanged
            }
        }

        private int _abwehraktionen = 1;
        [DependentProperty("Initiative")]
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
                AktionenNeuSetzen();
                Kampf.SortedInitiativListe = Kampf.InitiativListe != null ?
                    (this.Kampf.Kampfrunde == 0?
                    Kampf.InitiativListe.OrderByDescending(t => t.Start.InitiativPhase):
                    Kampf.InitiativListe.Where(t => t.Start.Kampfrunde == this.Kampf.Kampfrunde).OrderByDescending(t => t.Start.InitiativPhase) 
                    )
                    : null;            // .Kampf.Kampfrunde
            }
        }

        private int _verbrauchteAngriffsaktionen = 0;
        public int VerbrauchteAngriffsaktionen
        {
            get { return _verbrauchteAngriffsaktionen; }
            set { Set(ref _verbrauchteAngriffsaktionen, value); }
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
            set { Set(ref _verbrauchteAbwehraktionen, value); }
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
            set { Set(ref _verbrauchteFreieAktionen, value); }
        }

        [DependentProperty("VerbrauchteFreieAktionen"), DependentProperty("FreieAktionen")]
        public int FreieAktionenÜbrig
        {
            get { return FreieAktionen - VerbrauchteFreieAktionen; }
        }

        private void AktionenBerechnen()
        {
            int aktionen = 2;

            //Wenn die Initiative auf 0 sinkt hat man nur noch eine Abwehraktion
            //Bei längerfristigen Handlungen hat man nur noch eine Aktion pro KR
            if (Initiative < 0)
            {
                aktionen = 1;
            }
            if (!IstImKampf)
            {
                Aktionen = 0;
                OnChanged(nameof(Abwehraktionen));
                OnChanged(nameof(Angriffsaktionen));
                OnChanged(nameof(Aktionen));
                return;
            }

            //wenn man eine LängerfristigeHandlung Dauer >= 2 ausführt, dann hat man maximal 2 Aktionen während die Abwehraktionen verfallen
            var längerfristig = AngriffsManöver.Where(mi => mi.AktKampfrunde == mi.Kampf.Kampfrunde)
                .Where(mi => mi.Manöver.VerbleibendeDauer >= 2).OrderBy(mi => mi.Start).FirstOrDefault();
            if (längerfristig != null)
            {
                if (längerfristig.InitiativeModStart == 0)
                {
                    Aktionen = aktionen;
                    _angriffsaktionen = Math.Min(2, Aktionen);
                    _abwehraktionen = 0;
                    return;
                }
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

            OnChanged(nameof(Abwehraktionen));
            OnChanged(nameof(Angriffsaktionen));
            OnChanged(nameof(Aktionen));
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
                Set(ref _kampfstil, value);
                AktionenBerechnen();
            }
        }

        private WaffenloserKampfstil _waffenloserKampfstil;
        public WaffenloserKampfstil WaffenloserKampfstil
        {
            get { return _waffenloserKampfstil; }
            set { Set(ref _waffenloserKampfstil, value); }
        }

        public IEnumerable<ManöverInfo> AngriffsManöver
        {
            get
            {
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

            var ersterAngriff = AngriffsManöver.Where(mi => mi.Manöver is AngriffsManöver).FirstOrDefault();
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
                //alle anderen Manöver, die zuviel sind, löschen.
                Kampf.InitiativListe.Remove(manöver);
                geplanteAktionen.Remove(manöver);
            }
            //wenn die Liste ganz leer ist, füge KeineAktion hinzu
            //if (Angriffsaktionen == 0 && Kampf.InitiativListe.Where(mi => mi.KämpferInfo == this).Count() == 0)
            //    Kampf.InitiativListe.Add(this, new Manöver.KeineAktion(this), 0);

            AbwehrManöver.Clear();
        }

        private void AktionenNeuSetzen()
        {
            Kampf.InitiativListe.RemoveAll(mi => mi.Manöver.Ausführender == this && mi.Start.Kampfrunde == Kampf.Kampfrunde);
            Kampf.InitiativListe.AddRange(StandardAktionenSetzen(Kampf.Kampfrunde));
        }

        public IEnumerable<ManöverInfo> StandardAktionenSetzen(int kampfrunde)
        {
            if (kampfrunde == 0)
                yield break;

            AktionenBerechnen();

            var geplanteAktionen = AngriffsManöver.Where(mi => mi.Start.Kampfrunde <= kampfrunde && mi.End.Kampfrunde >= kampfrunde).OrderBy(mi => mi.Start);
            //löschen von Manövern, für die der falsche Kampfstil gewählt ist. oder für die zu wenig aktionen vorhanden sind.
            //DeleteManöver(ref geplanteAktionen);
            //var lfh = AngriffsManöver.Where(mi => mi.Manöver.VerbleibendeDauer >= 2).OrderBy(mi => mi.InitiativeStart).FirstOrDefault();
            //if (lfh != null)
            //{
            //    if (lfh.InitiativeModStart == 0 && Aktionen > 1)//zweite Aktion
            //        Kampf.InitiativListe.Add(lfh.Manöver, -8, kampfrunde);
            //    return;
            //}

            int zusatzAktionen = geplanteAktionen.Where(mi => mi.Manöver is ZusätzlicheAngriffsaktion).Count();

            //DateTime dtstart = DateTime.Now;
            //Debug.WriteLine(String.Format("KR {1}: Aktionen berechnen: {0}", DateTime.Now - dtstart, kampfrunde));
            for (int i = geplanteAktionen.SelectMany(ki => ki.Aktionszeiten).Where(zeit => zeit.Kampfrunde == kampfrunde).Count(); i < Angriffsaktionen; i++)
            {
                if (i == 0)
                {
                    yield return new ManöverInfo(new Attacke(this), 0, kampfrunde);
                }
                else // i>=1
                {
                    var ersterAngriff = AngriffsManöver.Where(mi => mi.Manöver is AngriffsManöver).FirstOrDefault();
                    if (Kampfstil == Kampfstil.BeidhändigerKampf)
                    {
                        //normale Aktionen und zusatzaktionen getrennt zählen, dann ist es einfach
                        //wenn noch keine Angriffsaktion da ist, dann Attacke bei i*-8
                        //wenn schon eine Angriffsaktion in der Liste ist, und i<Aktionen, dann zusatzaktion bei erste Angriffsaktion-4. sonst Attacke bei -8
                        if (ersterAngriff == null)
                        {
                            yield return new ManöverInfo(new Attacke(this), Math.Max(i * 4, 8), kampfrunde);
                        }
                        else
                        if (ersterAngriff != null)
                        {
                            if (zusatzAktionen > 0 && Abwehraktionen == 0 && i == Angriffsaktionen - 1) //es wurde umgewandelt.
                                yield return new ManöverInfo(new Attacke(this), 8, kampfrunde);
                            else
                            {
                                //4 ini-phasen nach dem ersten angriff
                                yield return new ManöverInfo(new ZusätzlicheAngriffsaktion(this), ersterAngriff.InitiativeModStart + (zusatzAktionen + 1) * 4, kampfrunde);
                                zusatzAktionen++;
                            }
                        }
                    }
                    else if (Kampfstil == Kampfstil.Parierwaffenstil)
                    {
                        if (Abwehraktionen == 1 && ersterAngriff != null &&
                            (Kämpfer is Model.Gegner || (Kämpfer as Model.Held).HatSonderfertigkeit("Tod von Links")) &&
                            geplanteAktionen.Where(mi => mi.Manöver is TodVonLinks).Count() == 0)
                            yield return new ManöverInfo(new TodVonLinks(this), Math.Max(i * 4, 8), kampfrunde);
                        else
                            yield return new ManöverInfo(new Attacke(this), Math.Max(i * 4, 8), kampfrunde);
                    }
                    else if (Kämpfer is Model.Gegner && Aktionen > 2)
                        yield return new ManöverInfo(new Attacke(this), i * 4, kampfrunde);
                    else
                        yield return new ManöverInfo(new Attacke(this), Math.Max(i * 4, 8), kampfrunde);
                }
            }
            bool skip1Abwehr = false;
            var längerfristig = AngriffsManöver.Where(
                mi => mi.Manöver.GetType() == typeof(Manöver.FernkampfManöver)
                || mi.Manöver.GetType() == typeof(Manöver.Zauber)
                || (mi.Manöver.GetType() == typeof(Manöver.SonstigesManöver) && mi.Manöver.Dauer >= 2)
                ).OrderBy(mi => mi.Start).FirstOrDefault(); 
            if (längerfristig != null)
            {
                if (längerfristig.End.Kampfrunde == kampfrunde)
                {
                    ManöverInfo mi = new ManöverInfo(new Attacke(this), 0, kampfrunde);

                    if (längerfristig.Manöver.GetType() == typeof(Manöver.FernkampfManöver))
                    {
                        mi.Manöver = new Manöver.FernkampfManöver(this, 
                            (IFernkampfwaffe)(längerfristig.Manöver as Manöver.FernkampfManöver).FernkampfWaffeSelected);            
                    }
                    else
                    if (längerfristig.Manöver.GetType() == typeof(Manöver.Zauber))
                    {
                        mi.Manöver = new Manöver.Zauber(this,
                            (Held_Zauber)(längerfristig.Manöver as Manöver.Zauber).Held_Zauber);
                    }
                    //Nur 1 Aktion für das Würfeln der längerfriatige Handlung
                    mi.Start = längerfristig.End;
                    mi.End = längerfristig.End;
                    //In der 1.Akt muss die längerfristige Handlung aktiv werden, die 2. Akt wird ebenfalls als aktive Aktion freigeschaltet
                    if (längerfristig.Manöver.VerbleibendeDauer == 1 && Abwehraktionen > 0)
                    {
                        mi.Manöver.Dauer = 1;
                        mi.Manöver.VerbleibendeDauer = 1;
                        yield return new ManöverInfo(mi.Manöver, 0, kampfrunde);
                        yield return new ManöverInfo(new Attacke(this), 8, kampfrunde);
                    }
                    else
                    //Die Längerfristige Handlung Endet in der KR - daher muss die 2. Akt ein aktives Manöver werden
                    if (this.Aktionen >= 2 && längerfristig.Manöver.VerbleibendeDauer == 2)
                    {
                        mi.Manöver.Dauer = 1;
                        mi.Manöver.VerbleibendeDauer = 1;
                        yield return new ManöverInfo(mi.Manöver, 8, kampfrunde);
                    }
                    skip1Abwehr = true;
                    ((Wesen)mi.Kampf.AktIniKämpfer.Kämpfer).AktVerbleibendeDauer = längerfristig.Manöver.VerbleibendeDauer;
                }
            }
            //Debug.WriteLine(String.Format("KR {1}: Manöver erstellen: {0}", DateTime.Now - dtstart, kampfrunde));

            //Parade-Manöver setzen
            AbwehrManöver.Clear();
            for (int i = 0; i < Abwehraktionen -(skip1Abwehr ? 1 : 0); i++)
                AbwehrManöver.Add(new ManöverInfo(new Parade(this), 0, Kampf.Kampfrunde));            
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

    public class KämpferInfoListe : ExtendedObservableCollection<KämpferInfo>
    {
        private Dictionary<IKämpfer, KämpferInfo> _kämpfer_kämpferinfo;

        public KämpferInfoListe(Kampf kampf)
        {
            _kampf = kampf;
            _kämpfer_kämpferinfo = new Dictionary<IKämpfer, KämpferInfo>();
            lazySort = new LazyUpdate(() => Sort());
            CollectionChangedExtended += KämpferInfoListe_CollectionChangedExtended;
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
            private set { _kampf = value; }
        }

        #region Add and Remove


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

        public void RemoveAll(Predicate<KämpferInfo> match)
        {
            foreach (KämpferInfo k in this.Where(ki => match(ki)).ToList())
                Remove(k);
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

        private void KämpferInfoListe_CollectionChangedExtended(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (KämpferInfo k in e.NewItems)
                {
                    _kämpfer_kämpferinfo.Add(k.Kämpfer, k);
                    k.PropertyChanged += KämpferInfo_PropertyChanged;
                }
                lazySort.Do();
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (KämpferInfo k in e.OldItems)
                {
                    k.PropertyChanged -= KämpferInfo_PropertyChanged;
                    _kämpfer_kämpferinfo.Remove(k.Kämpfer);
                }
                lazySort.Do();
            }
        }

        private LazyUpdate lazySort;

        private void KämpferInfo_PropertyChanged(object o, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Initiative")
            {
                lazySort.Do();
            }
        }


        public void Sort()
        {
            base.Sort(ki => ki.InitiativeMitKommas);
            foreach (KämpferInfo ki in this)
                ki.NotifyIndexChanged();
        }
    }
}
