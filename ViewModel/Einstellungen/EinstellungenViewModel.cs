using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ImpromptuInterface;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;

using Q42.HueApi;
using Q42.HueApi.Interfaces;

using Q42.HueApi.ColorConverters.Original;
using Q42.HueApi.ColorConverters.OriginalWithModel;
using Q42.HueApi.ColorConverters.HSB;
using Q42.HueApi.ColorConverters;
using Q42.HueApi.Models.Bridge;
using System.Windows.Media;
using System.Windows.Threading;
using MeisterGeister.View.Settings;
using System.Threading;
using System.Windows;
using MeisterGeister.View;

namespace MeisterGeister.ViewModel.Settings
{
    //TODO: Farben dürfen nicht statisch von Farbe 1 auf Farbe 2 wechseln, sondern müssen Übergänge bekommen (via Gradient)
    //public static class GradientStopCollectionExtensions
    //{
    //    public static Color GetRelativeColor(this GradientStopCollection gsc, double offset)
    //    {
    //        GradientStop before = gsc.Where(w => w.Offset == gsc.Min(m => m.Offset)).First();
    //        GradientStop after = gsc.Where(w => w.Offset == gsc.Max(m => m.Offset)).First();

    //        foreach (var gs in gsc)
    //        {
    //            if (gs.Offset < offset && gs.Offset > before.Offset)
    //            {
    //                before = gs;
    //            }
    //            if (gs.Offset > offset && gs.Offset < after.Offset)
    //            {
    //                after = gs;
    //            }
    //        }

    //        var color = new Color();

    //        color.ScA = (float)((offset - before.Offset) * (after.Color.ScA - before.Color.ScA) / (after.Offset - before.Offset) + before.Color.ScA);
    //        color.ScR = (float)((offset - before.Offset) * (after.Color.ScR - before.Color.ScR) / (after.Offset - before.Offset) + before.Color.ScR);
    //        color.ScG = (float)((offset - before.Offset) * (after.Color.ScG - before.Color.ScG) / (after.Offset - before.Offset) + before.Color.ScG);
    //        color.ScB = (float)((offset - before.Offset) * (after.Color.ScB - before.Color.ScB) / (after.Offset - before.Offset) + before.Color.ScB);

    //        return color;
    //    }
    //}

    public class EinstellungenViewModel : Base.ViewModelBase
    {
        public class LightColor
        {
            public Light light { get; set; }
            public Color color { get; set; }
        }
        public class HUESzene
        {
            public string Name { get; set; }
            private List<LightColor> _lstLightColor = new List<LightColor>();
            public List<LightColor> lstLightColor
            {
                get { return _lstLightColor; }
                set { _lstLightColor = value; }
            }
        }

        private LightColor _lightColorSelected = new LightColor();
        public LightColor LightColorSelected
        {
            get { return _lightColorSelected; }
            set { _lightColorSelected = value; }
        }

        private HUESzene _selHUESzene = new HUESzene();
        public HUESzene SelHUESzene
        {
            get { return _selHUESzene; }
            set 
            {
                Set(ref _selHUESzene, value);
                if (value == null)
                    return;
                List<Light> LightsLeft = new List<Light>();
                if (value.lstLightColor.Count == 0)
                    LightsLeft.AddRange(lstHUELights);
                else
                    LightsLeft.AddRange(lstHUELights.Where(t => !value.lstLightColor.Select(z => z.light).ToList().Contains(t)).ToList());
                lstHUELightsLeft = LightsLeft;
            }
        }

        private List<HUESzene> _lstHUESzenen = Global.MainVM.lstHUESzenen;
        public List<HUESzene> lstHUESzenen
        { 
            get { return _lstHUESzenen; }
            set
            {
                Set(ref _lstHUESzenen, value);
                Global.MainVM.lstHUESzenen = value;
            }
        }

        private string _hUESzeneName = null;
        public string HUESzeneName
        {
            get { return _hUESzeneName; }
            set { Set(ref _hUESzeneName, value); }
        }

        public Color GetRelativeColor(GradientStopCollection gsc, double offset)
        {
            GradientStop before = gsc.Where(w => w.Offset == gsc.Min(m => m.Offset)).First();
            GradientStop after = gsc.Where(w => w.Offset == gsc.Max(m => m.Offset)).First();

            foreach (var gs in gsc)
            {
                if (gs.Offset < offset && gs.Offset > before.Offset)
                {
                    before = gs;
                }
                if (gs.Offset > offset && gs.Offset < after.Offset)
                {
                    after = gs;
                }
            }

            var color = new Color();

            color.ScA = (float)((offset - before.Offset) * (after.Color.ScA - before.Color.ScA) / (after.Offset - before.Offset) + before.Color.ScA);
            color.ScR = (float)((offset - before.Offset) * (after.Color.ScR - before.Color.ScR) / (after.Offset - before.Offset) + before.Color.ScR);
            color.ScG = (float)((offset - before.Offset) * (after.Color.ScG - before.Color.ScG) / (after.Offset - before.Offset) + before.Color.ScG);
            color.ScB = (float)((offset - before.Offset) * (after.Color.ScB - before.Color.ScB) / (after.Offset - before.Offset) + before.Color.ScB);

            return color;
        }

        public List<string> lstDeviceID
        {
            get { return _lstDeviceID; }
            set { Set(ref _lstDeviceID, value); }
        }

        private List<string> _lstDeviceID = new List<string>();

        #region Property

        public Base.CommandBase onBtnSelectHUESzeneColor
        {
            get
            {
                if (_onBtnSelectHUESzeneColor == null)
                {
                    _onBtnSelectHUESzeneColor = new Base.CommandBase(SelectHUESzeneColor, null);
                }

                return _onBtnSelectHUESzeneColor;
            }
        }
        public Base.CommandBase onBtnSelectHUEColor
        {
            get
            {
                if (_onBtnSelectHUEColor == null)
                {
                    _onBtnSelectHUEColor = new Base.CommandBase(SelectHUEColor, null);
                }

                return _onBtnSelectHUEColor;
            }
        }

        public Base.CommandBase onBtnDoTheme
        {
            get
            {
                if (_onBtnDoTheme == null)
                {
                    _onBtnDoTheme = new Base.CommandBase(DoTheme, null);
                }

                return _onBtnDoTheme;
            }
        }

        public Base.CommandBase onbtnNeuesHUETheme
        {
            get
            {
                if (_onbtnNeuesHUETheme == null)
                {
                    _onbtnNeuesHUETheme = new Base.CommandBase(NeuesHUETheme, null);
                }

                return _onbtnNeuesHUETheme;
            }
        }

        public Base.CommandBase onTBtnThemeProcessColor
        {
            get
            {
                if (_onTBtnThemeProcessColor == null)
                {
                    _onTBtnThemeProcessColor = new Base.CommandBase(TBtnThemeProcessColor, null);
                }

                return _onTBtnThemeProcessColor;
            }
        }

        public Base.CommandBase onbtnAddHUEProcess
        {
            get
            {
                if (_onbtnAddHUEProcess == null)
                {
                    _onbtnAddHUEProcess = new Base.CommandBase(AddHUEProcess, null);
                }

                return _onbtnAddHUEProcess;
            }
        }

        public Base.CommandBase onBtnHUEGWsuchen
        {
            get
            {
                if (_onBtnHUEGWsuchen == null)
                {
                    _onBtnHUEGWsuchen = new Base.CommandBase(HUEGWsuchen, null);
                }

                return _onBtnHUEGWsuchen;
            }
        }

        private Base.CommandBase _nBtnAddLightToSzene = null;
        public Base.CommandBase OnBtnAddLightToSzene
        {
            get
            {
                if (_nBtnAddLightToSzene == null)
                {
                    _nBtnAddLightToSzene = new Base.CommandBase(AddLightToSzene, null);
                }
                return _nBtnAddLightToSzene;
            }
        }

        public void AddLightToSzene(object obj)
        {
            if (cmbxSelHUE == null)
                return;
            List<LightColor> lstLC = new List<LightColor>();
            lstLC.AddRange(SelHUESzene.lstLightColor);
            lstLC.Add(new LightColor() { light = cmbxSelHUE, color = Colors.White } );
            SelHUESzene.lstLightColor = lstLC;
            cmbxSelHUE = null;

            lstHUELightsLeft = lstHUELights.Where(t => !SelHUESzene.lstLightColor.Select(z => z.light).ToList().Contains(t)).ToList();
            OnChanged(nameof(SelHUESzene));
        }

        private Base.CommandBase _onBtnAddHUESzene = null;
        public Base.CommandBase OnBtnAddHUESzene
        {
            get
            {
                if (_onBtnAddHUESzene == null)
                {
                    _onBtnAddHUESzene = new Base.CommandBase(AddHUESzene, null);
                }

                return _onBtnAddHUESzene;
            }
        }

        public void AddHUESzene(object obj)
        {
            List<HUESzene> lstSzenen = new List<HUESzene>();
            lstSzenen.AddRange(lstHUESzenen);
            HUESzene newHUESzene = new HUESzene();
            newHUESzene.Name = HUESzeneName;
            lstSzenen.Add(newHUESzene);
            HUESzeneName = null;
            lstHUESzenen = lstSzenen;
            SelHUESzene = newHUESzene;
        }

        public Base.CommandBase onBtnActivateHUEGW
        {
            get
            {
                if (_onBtnActivateHUEGW == null)
                {
                    _onBtnActivateHUEGW = new Base.CommandBase(ActivateHUEGW, null);
                }

                return _onBtnActivateHUEGW;
            }
        }

        public string Regeledition
        {
            get { return Global.Regeledition; }
            set { Global.Regeledition = value; }
        }

        public Base.CommandBase OnSetRegeledition
        {
            get
            {
                if (_onSetRegeledition == null)
                {
                    _onSetRegeledition = new Base.CommandBase(SetRegeledition, null);
                }

                return _onSetRegeledition;
            }
        }

        public bool IsPflanzenwissen
        {
            get { return Logic.Einstellung.Einstellungen.PflanzenwissenIntegrieren; }

            set
            {
                Logic.Einstellung.Einstellungen.PflanzenwissenIntegrieren = value;
                OnChanged(nameof(IsPflanzenwissen));
            }
        }

        public bool IsAudioSpieldauerBerechnen
        {
            get { return Logic.Einstellung.Einstellungen.AudioSpieldauerBerechnen; }

            set
            {
                Logic.Einstellung.Einstellungen.AudioSpieldauerBerechnen = value;
                OnChanged(nameof(IsAudioSpieldauerBerechnen));
            }
        }

        public bool IsInAnderemPfadSuchen
        {
            get { return Logic.Einstellung.Einstellungen.AudioInAnderemPfadSuchen; }

            set
            {
                Logic.Einstellung.Einstellungen.AudioInAnderemPfadSuchen = value;
                OnChanged(nameof(IsInAnderemPfadSuchen));
            }
        }

        public bool IsShowPlaylistFavorite
        {
            get { return Logic.Einstellung.Einstellungen.ShowPlaylistFavorite; }

            set
            {
                Logic.Einstellung.Einstellungen.ShowPlaylistFavorite = value;
                OnChanged(nameof(IsShowPlaylistFavorite));
            }
        }

        public bool IsMitUeberlastung
        {
            get { return Logic.Einstellung.Einstellungen.MitUeberlastung; }

            set
            {
                Logic.Einstellung.Einstellungen.MitUeberlastung = value;
                OnChanged(nameof(IsMitUeberlastung));
            }
        }

        public List<EinstellungItem> EinstellungListe
        {
            get { return einstellungListe; }

            set
            {
                einstellungListe = value;
                OnChanged(nameof(EinstellungListe));
            }
        }

        public ermittleRuestung BerechnungRuestung
        {
            get { return (ermittleRuestung)Logic.Einstellung.Einstellungen.RSBerechnung; }

            set
            {
                switch (value)
                {
                    case ermittleRuestung.AutomatischZonen:
                        Logic.Einstellung.Einstellungen.RSBerechnung = (int)value;
                        break;

                    case ermittleRuestung.Einfach:
                        Logic.Einstellung.Einstellungen.RSBerechnung = (int)value;
                        break;

                    case ermittleRuestung.Zonen:
                        Logic.Einstellung.Einstellungen.RSBerechnung = (int)value;
                        break;

                    case ermittleRuestung.AutomatischEinfach:
                        Logic.Einstellung.Einstellungen.RSBerechnung = (int)value;
                        break;

                    default:
                        return;
                }
                OnChanged(nameof(BerechnungRuestung));
            }
        }

        public ermittleBehinderung BerechnungBehinderung
        {
            get { return (ermittleBehinderung)Logic.Einstellung.Einstellungen.BEBerechnung; }

            set
            {
                switch (value)
                {
                    case ermittleBehinderung.Automatisch:
                        Logic.Einstellung.Einstellungen.BEBerechnung = (int)value;
                        break;

                    case ermittleBehinderung.Eingabe:
                        Logic.Einstellung.Einstellungen.BEBerechnung = (int)value;
                        break;

                    default:
                        return;
                }
                OnChanged(nameof(BerechnungBehinderung));
            }
        }

        public ermitteleUeberlastung BerechnungUeberlastung
        {
            get { return (ermitteleUeberlastung)Logic.Einstellung.Einstellungen.UeberlastungBerechnung; }

            set
            {
                switch (value)
                {
                    case ermitteleUeberlastung.Automatisch:
                        Logic.Einstellung.Einstellungen.UeberlastungBerechnung = (int)value;
                        break;

                    case ermitteleUeberlastung.Eingabe:
                        Logic.Einstellung.Einstellungen.UeberlastungBerechnung = (int)value;
                        break;

                    default:
                        return;
                }
                OnChanged(nameof(BerechnungUeberlastung));
            }
        }

        [DependentProperty(nameof(EinstellungListe))]
        public List<EinstellungItem> InventarListe
        {
            get
            {
                if (EinstellungListe == null)
                {
                    return null;
                }

                return EinstellungListe.Where(e => e.Kontext == "Inventar").ToList();
            }
        }

        [DependentProperty(nameof(EinstellungListe))]
        public List<string> KontextListe
        {
            get
            {
                if (EinstellungListe == null)
                {
                    return null;
                }

                return EinstellungListe.Select(e => e.Kontext).Distinct().ToList();
            }

            set { KontextListe = value; }
        }

        [DependentProperty(nameof(EinstellungListe))]
        public List<EinstellungItem> AllgemeinListe
        {
            get
            {
                if (EinstellungListe == null)
                {
                    return null;
                }

                return EinstellungListe.Where(e => e.Kontext == "Allgemein").ToList();
            }
        }

        [DependentProperty(nameof(EinstellungListe))]
        public List<EinstellungItem> ProbenListe
        {
            get
            {
                if (EinstellungListe == null)
                {
                    return null;
                }

                return EinstellungListe.Where(e => e.Kontext == "Proben").ToList();
            }
        }

        [DependentProperty(nameof(EinstellungListe))]
        public List<EinstellungItem> KampfListe
        {
            get
            {
                if (EinstellungListe == null)
                {
                    return null;
                }

                return EinstellungListe.Where(e => e.Kontext == "Kampf").ToList();
            }
        }

        [DependentProperty(nameof(EinstellungListe))]
        public List<EinstellungItem> AudioplayerListe
        {
            get
            {
                if (EinstellungListe == null)
                {
                    return null;
                }

                return EinstellungListe.Where(e => e.Kontext == "Audioplayer").ToList();
            }
        }

        [DependentProperty(nameof(EinstellungListe))]
        public List<EinstellungItem> AlmanachListe
        {
            get
            {
                if (EinstellungListe == null)
                {
                    return null;
                }

                return EinstellungListe.Where(e => e.Kontext == "Almanach").ToList();
            }
        }

        public List<string> PDFReaders
        {
            get
            {
                return Logic.General.Pdf.readers.Keys.ToList();
            }
        }

        public string SelectedPDFReader
        {
            get
            {
                return _selectedPDFReader;
            }

            set
            {
                _selectedPDFReader = value;
                Logic.Einstellung.Einstellungen.PdfReaderCommand = Logic.General.Pdf.readers[value][0];
                Logic.Einstellung.Einstellungen.PdfReaderArguments = Logic.General.Pdf.readers[value][1];
                Logic.General.Pdf.SetReader(value);
            }
        }

        [DependentProperty(nameof(EinstellungListe))]
        public List<Model.Setting> SettingListe
        {
            get
            {
                if (settingListe == null)
                {
                    return null;
                }

                return settingListe;
            }
        }

        public List<LiteraturItem> LiteraturListe
        {
            get;
            set;
        }

        private Base.CommandBase _onBtnSelectHUEColor = null;

        private Base.CommandBase _onBtnSelectHUESzeneColor = null;
        private Base.CommandBase _onBtnDoTheme = null;

        private Base.CommandBase _onbtnNeuesHUETheme = null;

        private Base.CommandBase _onTBtnThemeProcessColor = null;

        private Base.CommandBase _onbtnAddHUEProcess = null;

        private Base.CommandBase _onBtnHUEGWsuchen = null;

        private Base.CommandBase _onBtnActivateHUEGW = null;

        private Base.CommandBase _onSetRegeledition = null;

        private List<EinstellungItem> einstellungListe;

        private string _selectedPDFReader;

        private List<Model.Setting> settingListe;

        private void SelectHUESzeneColor(object obj)
        {
            HUESzene hSzene = new HUESzene();
            hSzene = SelHUESzene;

            HUEColorDialog colorDialog = new HUEColorDialog();
            colorDialog.TestLight = LightColorSelected.light;
            colorDialog.SelectedColor = LightColorSelected.color;
            colorDialog.Owner = obj as EinstellungenWindow;

            if (colorDialog.ShowDialog().Value)
            {
                LightColorSelected.color = colorDialog.SelectedColor;
                SelHUESzene = null;
                SelHUESzene = hSzene;
            }
        }
        private void SelectHUEColor(object obj)
        {
            if (HUEThemeSelected == null)
                HUEThemeSelected = lstHUEThemes[0];

            HUEColorDialog colorDialog = new HUEColorDialog();

            //   colorDialog.colorPicker .Client = Client;
            colorDialog.SelectedColor = HUEThemeSelected.LightProcessSelected.Color;
            //   colorDialog.SelectedColor = (SolidColorBrush)HUEThemeSelected.LightProcessSelected.Color);//, HUEThemeSelected.LightProcessSelected.Color.G, HUEThemeSelected.LightProcessSelected.Color.B);//((SolidColorBrush))// HUEThemeSelected.lstLightProcess[0].Color.ToString();// this.RectColorPicked.Fill).Color;
            colorDialog.Owner = obj as EinstellungenWindow;

            if (colorDialog.ShowDialog().Value)
            {
                HUETheme aktTheme = new Settings.EinstellungenViewModel.HUETheme();
                aktTheme = HUEThemeSelected;
                if (aktTheme.vm == null)
                    aktTheme.vm = this;
                HUEThemeSelected.LightProcessSelected.Color = colorDialog.SelectedColor;

                List<HUETheme> lst = new List<Settings.EinstellungenViewModel.HUETheme>();
                lst.AddRange(lstHUEThemes);
                lstHUEThemes = lst;
                //RectColorPicked.Fill = new SolidColorBrush(colorDialog.SelectedColor);
                //SendColorToLamps(colorDialog.SelectedColor);
            }
        }


        //private GradientStopCollection _gCol = new GradientStopCollection();
        //public GradientStopCollection gCol
        //{
        //    get { return _gCol; }
        //    set
        //    {
        //        _gCol = value;
        //        OnChanged("gCol");
        //        //Set(ref _gCol, return);
        //    }
        //}

        List<HUETheme> RunningThemes = new List<HUETheme>();

        private void DoTheme(object obj)
        {
            HUETheme ThemeToDo = obj as HUETheme;
            if (obj == null || HUEThemeSelected.lstLights == null)
                return;

            if (!ThemeToDo.isRunning)
            {
                ThemeToDo.vm = this;
                ThemeToDo.actLightProcess = null;
                ThemeToDo.StartTime = Environment.TickCount;
                //ThemeToDo.actLightProcess = 0;
                ThemeToDo.isRunning = true;
                ThemeToDo._timer.Start();
            }
            else
            {
                ThemeToDo._timer.Stop();
                ThemeToDo.isRunning = false;
            }
        }

        private void NeuesHUETheme(object obj)
        {
            List<HUETheme> lst = new List<HUETheme>();
            lst.AddRange(lstHUEThemes);
            lst.Add(new HUETheme()
            {
                Name = "Neues_HUE",
                KomplettDauer = 8000,
                doLoop = true,
                lstLightProcess = new List<LightProcess>() {
                    new LightProcess() { Brightness= 100, Color = Colors.Red, DauerProzent= .25, Phase=0 },
                    new LightProcess() { Brightness= 100, Color = Colors.Yellow, DauerProzent= .5, Phase=1 },
                    new LightProcess() { Brightness= 100, Color = Colors.Blue, DauerProzent= .75, Phase=2 } }
            });
            lstHUEThemes = lst;
        }

        private void TBtnThemeProcessColor(object obj)
        {
            if (((LightProcess)obj).IsSelected)
            {
                HUEThemeSelected.LightProcessSelected = (LightProcess)obj;
                //lstHUEThemes.ForEach(delegate(HUETheme ht) { ht.lstLightProcess.ForEach(delegate (LightProcess lp) { if (lp != obj) lp.IsSelected = false; }); });
            }
        }

        private void AddHUEProcess(object obj)
        {
            List<HUETheme> lstThemes = new List<HUETheme>();
            lstThemes.AddRange(lstHUEThemes);

            List<LightProcess> lst = new List<LightProcess>();
            HUEThemeSelected = lstHUEThemes[lstHUEThemes.Count - 1];
            if (HUEThemeSelected.lstLightProcess != null)
                lst.AddRange(HUEThemeSelected.lstLightProcess);
            lst.Add(new LightProcess() { Phase = lst.Count,
                DauerProzent = lst.Count == 0 ? .5 : (lst[lst.Count - 1].DauerProzent + 1) / 2,
                Dauer = HUEThemeSelected.KomplettDauer * (lst.Count == 0 ? .5 : (lst[lst.Count - 1].DauerProzent + 1) / 2),
                KomplettDauer = HUEThemeSelected.KomplettDauer,
                Color = Colors.AliceBlue,
                Brightness = 255 });

            //((HUETheme)obj).lstLightProcess = lst;
            HUEThemeSelected.lstLightProcess = lst;
            lstHUEThemes = lstThemes;
        }

        private void HUEGWsuchen(object obj)
        {
            InitHUEGateway();
        }

        private void ActivateHUEGW(object obj)
        {
            _ActivateHUE();
        }

        private void SetRegeledition(object obj)
        {
            var regWin = new View.Windows.RegeleditionWindow
            {
                Owner = System.Windows.Application.Current.MainWindow
            };
            var dlgResult = regWin.ShowDialog();
            regWin = null;
            if (dlgResult == true)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        #endregion Property

        #region Constructor

        public EinstellungenViewModel()
        {
            LoadDaten();
        }

        #endregion Constructor

        #region Public Methods

        public void LoadDaten()
        {
            if (Global.ContextHeld != null)
            {
                EinstellungListe = Global.ContextHeld.Liste<Model.Einstellung>().Where(e => e.Kategorie != "Versteckt").OrderBy(h => h.Name).Select(e => EinstellungItem.GetTypedEinstellungItem(e)).ToList();
                settingListe = Global.ContextHeld.Liste<Model.Setting>().ToList();
                LiteraturListe = Global.ContextHeld.Liste<Model.Literatur>().OrderBy(h => h.Name).Select(e => new LiteraturItem(e)).ToList();

                lstHUEGateways = MainViewModel.Instance.lstHUEGateways;
                HUEGWSelected = MainViewModel.Instance.HUEGWSelected;
                lstHUELights = MainViewModel.Instance.lstHUELights;
                Client = MainViewModel.Instance.Client;
            }
        }

        #endregion Public Methods


        #region HUE Lampen

        public class LightProcess
        {
            private int _komplettDauer;
            public int KomplettDauer
            {
                get { return _komplettDauer; }
                set
                {
                    _komplettDauer = value;
                    Dauer = value * DauerProzent;
                }
            }

            public bool IsSelected { get; set; }
            public int Phase { get; set; }
            private double _dauerProzent;
            public double DauerProzent
            {
                get { return _dauerProzent; }
                set { _dauerProzent = value; }
            }
            private double _dauer;
            public double Dauer
            {
                get { return _dauer; }
                set
                {
                    _dauer = value;

                    DauerProzent = value / KomplettDauer;
                }
            }
            public Color Color { get; set; }
            public int? Brightness { get; set; }
        }

        public class HUETheme : Base.ToolViewModelBase
        {
            public override string Name { get; set; }

            private int _komplettDauer;
            public int KomplettDauer
            {
                get { return _komplettDauer; }
                set { Set(ref _komplettDauer, value);
                    if (lstLightProcess != null)
                        foreach (LightProcess lPro in lstLightProcess)
                        { lPro.KomplettDauer = value; }
                }
            }

            private LightProcess _lightProcessSelected;
            public LightProcess LightProcessSelected
            {
                get { return _lightProcessSelected; }
                set {
                    Set(ref _lightProcessSelected, value);
                }
            }

            private List<LightProcess> _lstLightProcess;
            public List<LightProcess> lstLightProcess
            {
                get { return _lstLightProcess; }
                set
                {
                    Set(ref _lstLightProcess, value);
                    if (value != null)
                        foreach (LightProcess lPro in value)
                        { lPro.KomplettDauer = KomplettDauer; }
                }
            }
            public List<Light> lstLights { get; set; }

            private bool _doLoop;
            public bool doLoop
            {
                get { return _doLoop; }
                set
                {
                    //if (value && !_doLoop && lstLightProcess != null)
                    //{
                    //    List<LightProcess> lstLL = new List<LightProcess>();
                    //    lstLL.AddRange(lstLightProcess);
                    //    lstLL.Add(
                    //        new LightProcess() {
                    //            Phase = lstLightProcess.Count,
                    //            Color = lstLightProcess[0].Color,
                    //            DauerProzent = 1,
                    //            Dauer = KomplettDauer,
                    //            KomplettDauer = KomplettDauer });
                    //    lstLightProcess = lstLL;
                    //}
                    //if (!value && _doLoop && lstLightProcess != null)
                    //{
                    //    List<LightProcess> lstLL = new List<LightProcess>();
                    //    lstLL.AddRange(lstLightProcess);
                    //    lstLL.RemoveAt(lstLL.Count - 1);
                    //    lstLightProcess = lstLL;
                    //}
                    Set(ref _doLoop, value);
                }
            }

            public bool doStrobe { get; set; }

            public bool isRunning { get; set; }
            public int? actLightProcess { get; set; }
            public int StartTime { get; set; }

            public DispatcherTimer _timer = new DispatcherTimer();
            public bool InitDone = false;

            public EinstellungenViewModel vm;
            //LightCommand command = new LightCommand();

            public LightCommand command { get; set; }

            private async void regHUE()
            {
                vm.appKey = await vm.Client.RegisterAsync("MGmeetsHUE", "PC");
            }

            private Light _addLightToTheme = null;
            public Light AddLightToTheme
            {
                get { return _addLightToTheme; }
                set
                {
                    Set(ref _addLightToTheme, value);
                    List<Light> lst = new List<Light>();
                    if (lstLights != null)
                        lst.AddRange(lstLights);
                    if (!lst.Contains(value))
                        lst.Add(value);
                    else
                        lst.Remove(value);
                    lstLights = lst;
                }
            }


            public void _timer_Tick(object sender, EventArgs e)
            {
                //Setze die Anfangsfarbe direkt
                if (actLightProcess == null)
                {
                    actLightProcess = 0;

                    //Control the lights
                    command = new LightCommand();

                    //if (doStrobe)
                    //command.Alert = Alert.Once;

                    //Turn the light on and set a Hex color for the command (see the section about Color Converters)
                    command.TurnOn().SetColor(new RGBColor(
                        lstLightProcess[actLightProcess.Value].Color.R,
                        lstLightProcess[actLightProcess.Value].Color.G,
                        lstLightProcess[actLightProcess.Value].Color.B));
                    //Helligkeit ?
                    command.TurnOn().Brightness = (byte)this.lstLightProcess[actLightProcess.Value].Brightness;
                    //Or start a colorloop
                    //command.Effect = Q42.HueApi.Effect.ColorLoop;
                    command.Effect = Q42.HueApi.Effect.None;

                    command.TransitionTime = new TimeSpan(0, 0, 0, 0,
                        (int)lstLightProcess[0].KomplettDauer / lstLightProcess.Count);
                    //Once you have composed your command, send it to one or more lights

                    vm.Client.SendCommandAsync(command, lstLights.Select(t => t.Id).ToList());
                    Console.WriteLine(StartTime + ":  START    " + lstLightProcess[actLightProcess.Value].Color.R + " " +
                        lstLightProcess[actLightProcess.Value].Color.G + " " +
                        lstLightProcess[actLightProcess.Value].Color.B + "    ");

                    //Abwarten bis Erste Farbe gesetzt ist
                    Thread.Sleep(lstLightProcess[0].KomplettDauer / lstLightProcess.Count);
                    StartTime = Environment.TickCount;

                    if (lstLightProcess.Count > 1)
                    {
                        Thread.Sleep(10);
                        command.SetColor(new RGBColor(
                            lstLightProcess[actLightProcess.Value + 1].Color.R,
                            lstLightProcess[actLightProcess.Value + 1].Color.G,
                            lstLightProcess[actLightProcess.Value + 1].Color.B));
                        command.TransitionTime = new TimeSpan(0, 0, 0, 0,
                            (int)lstLightProcess[actLightProcess.Value].Dauer);
                        vm.Client.SendCommandAsync(command, lstLights.Select(t => t.Id).ToList());
                    }
                    return;
                }

                int TimeGone = Environment.TickCount - StartTime;

                if (command.TransitionTime == null || actLightProcess == -1 ||
                    TimeGone > lstLightProcess[actLightProcess.Value].Dauer)
                {
                    actLightProcess++;
                    if (actLightProcess >= lstLightProcess.Count - 1)
                    {
                        if (!doLoop)
                        {
                            isRunning = false;
                            _timer.Stop();
                            return;
                        }
                        StartTime = Environment.TickCount;
                        command.SetColor(new RGBColor(
                            lstLightProcess[0].Color.R,
                            lstLightProcess[0].Color.G,
                            lstLightProcess[0].Color.B));

                        command.TransitionTime = new TimeSpan(0, 0, 0, 0,
                            lstLightProcess[0].KomplettDauer - (int)lstLightProcess[actLightProcess.Value - 1].Dauer);

                        vm.Client.SendCommandAsync(command, lstLights.Select(t => t.Id).ToList());

                        Console.WriteLine(TimeGone + ": CYCLE " + TimeGone + "   " +
                            lstLightProcess[0].Color.R + " " +
                            lstLightProcess[0].Color.G + " " +
                            lstLightProcess[0].Color.B + "    " +
                            lstLightProcess[0].Brightness);

                        //Abwarten bis End-Farbe gesetzt ist
                        Thread.Sleep(lstLightProcess[0].KomplettDauer - (int)lstLightProcess[actLightProcess.Value - 1].Dauer);
                        actLightProcess = -1;
                        return;
                    }

                    StartTime = Environment.TickCount;
                    command.SetColor(new RGBColor(
                        lstLightProcess[actLightProcess.Value + 1].Color.R,
                        lstLightProcess[actLightProcess.Value + 1].Color.G,
                        lstLightProcess[actLightProcess.Value + 1].Color.B));

                    command.TransitionTime = new TimeSpan(0, 0, 0, 0,
                        actLightProcess == 0 ? (int)lstLightProcess[actLightProcess.Value].Dauer :
                        (int)(lstLightProcess[actLightProcess.Value].Dauer - lstLightProcess[actLightProcess.Value - 1].Dauer));

                    vm.Client.SendCommandAsync(command, lstLights.Select(t => t.Id).ToList());

                    Console.WriteLine(TimeGone + ": CYCLE " + TimeGone + "   " +
                        lstLightProcess[actLightProcess.Value + 1].Color.R + " " +
                        lstLightProcess[actLightProcess.Value + 1].Color.G + " " +
                        lstLightProcess[actLightProcess.Value + 1].Color.B + "    " +
                        lstLightProcess[actLightProcess.Value + 1].Brightness);

                    if (actLightProcess >= lstLightProcess.Count - 1)
                        actLightProcess = 0;
                    return;
                }
            }

            private async void ToAll()
            {
                //Or send it to all lights
                var result = await vm.Client.SendCommandAsync(command);
            }

            public HUETheme()
            {
                _timer.Tick += new EventHandler(_timer_Tick);
                _timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            }
        }


        private HUETheme _HUEThemeSelected;
        public HUETheme HUEThemeSelected
        {
            get { return _HUEThemeSelected; }
            set { Set(ref _HUEThemeSelected, value);
                if (value != null && value.vm == null)
                    value.vm = this;
            }
        }

        private List<HUETheme> _lstHUEThemes = new List<HUETheme>();

        //  [DependentProperty("HUEThemeSelected")]
        public List<HUETheme> lstHUEThemes
        {
            get { return _lstHUEThemes; }
            set
            {
                Set(ref _lstHUEThemes, value);
                if (value != null)
                    HUEThemeSelected = value[0];
                MainViewModel.Instance.lstHUEThemes = value;
            }
        }

        private string _HUEProgress = "";
        public string HUEProgress
        {
            get { return _HUEProgress; }
            set { Set(ref _HUEProgress, value); }
        }

        private LocatedBridge _HUEGWSelected = null;
        public LocatedBridge HUEGWSelected
        {
            get { return _HUEGWSelected; }
            set
            {
                Set(ref _HUEGWSelected, value);
                if (value != null)
                    Logic.Einstellung.Einstellungen.SetEinstellung<string>("HUE_GatewayID", value.BridgeId);
                MainViewModel.Instance.HUEGWSelected = value;
            }
        }

        private List<LocatedBridge> _lstHUEGaterways = new List<LocatedBridge>();
        public List<LocatedBridge> lstHUEGateways
        {
            get { return _lstHUEGaterways; }
            set
            {
                Set(ref _lstHUEGaterways, value);
                MainViewModel.Instance.lstHUEGateways = value;
            }
        }
        private LocalHueClient _client = null;
        public LocalHueClient Client
        {
            get { return _client; }            
            set { Set(ref _client, value); }
        }
        string appKey = null;


        static EinstellungenViewModel instance;
        public static EinstellungenViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EinstellungenViewModel();
                }
                return EinstellungenViewModel.instance;
            }
            private set { EinstellungenViewModel.instance = value; }
        }


        public async void InitHUEGateway()
        {
            try
            {
                HUEProgress = "Q42Hue attempting to initialize ..";
                IBridgeLocator locator = new HttpBridgeLocator();

                var bridgeIPs = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));

                List<LocatedBridge> lstGW = new List<LocatedBridge>();
                foreach (var bridgeIp in bridgeIPs)
                {
                    lstGW.Add(bridgeIp);
                    HUEProgress = bridgeIp + ": " + bridgeIp.IpAddress;
                }
                lstHUEGateways = lstGW;
                if (bridgeIPs.Count() > 0)
                {
                    HUEProgress = bridgeIPs.Count() + " Gateway" + (bridgeIPs.Count() == 1 ? "s" : "") + " gefunden";
                    HUEGWSelected = lstHUEGateways[0];
                }
                else
                {
                    HUEProgress = "Q42Hue Error: Could not find bridge!";
                }
            }
            catch (Exception ex)
            {
                View.General.ViewHelper.ShowError(string.Format("Die Suche der HUE-Gateways über das Netzwerk konnte nicht durchgeführt werden"), ex);
            }
        }


        private List<LightCommand> _lstHUELightCmd = new List<LightCommand>();
        public List<LightCommand> lstHUELightCmd
        {
            get { return _lstHUELightCmd; }
            set { Set(ref _lstHUELightCmd, value); }
        }

        private List<Light> _lstHUELightsLeft = new List<Light>();
        public List<Light> lstHUELightsLeft
        {
            get { return _lstHUELightsLeft; }
            set { Set(ref _lstHUELightsLeft, value); }
        }

        private List<Light> _lstHUELights = new List<Light>();
        public List<Light> lstHUELights
        {
            get { return _lstHUELights; }
            set
            {
                Set(ref _lstHUELights, value);
                if (value.Count > 0)
                    SelectedHUELight = value[0];
                List<LightCommand> lstLightCmd = new List<LightCommand>();
                //lstHUELights.ForEach(q => lstLightCmd.Add(new LightCommand() { Light = q, LightCmd = new LightCommand() }));
                lstHUELightCmd = lstLightCmd;
                MainViewModel.Instance.lstHUELights = value;
            }
        }

        private Light _cmbxSelHUE = new Light();
        public Light cmbxSelHUE
        {
            get { return _cmbxSelHUE; }
            set { Set(ref _cmbxSelHUE, value); }
        }

        private Light _selectedHUELight = new Light();
        public Light SelectedHUELight
        {
            get { return _selectedHUELight; }
            set { Set(ref _selectedHUELight, value); }
        }

        private async void _ActivateHUE()
        {
            string ip = HUEGWSelected.IpAddress.ToString();

            Client = new LocalHueClient(ip);
            appKey = MeisterGeister.Logic.Einstellung.Einstellungen.GetEinstellung<string>("HUE_Registerkey");
            //4nuWDIXoZ0EMPxMBXhQFagf5bOsK-XcEc7DzS8G1

            if (string.IsNullOrEmpty(appKey))
            {
                MeisterGeister.View.General.ViewHelper.Popup("Das Gateway wurde mit der MeisterGeister Version noch nicht gekoppelt." + Environment.NewLine + Environment.NewLine +
                    "Bitte klicke nach dem bestätigen dieser Meldung innerhalb von 10 Sekunden den Button auf dem Gateway");
                //Register your application
                //Link button drücken zum Registrieren !!!
                //Make sure the user has pressed the button on the bridge before calling RegisterAsync
                //It will throw an LinkButtonNotPressedException if the user did not press the button
                bool pressed = false;
                int start = Environment.TickCount;
                while (!pressed && Environment.TickCount - start < 10000)
                {
                    try
                    {
                        appKey = await Client.RegisterAsync("MGmeetsHUE", "PC");
                        //Save the app key for next use
                        MeisterGeister.Logic.Einstellung.Einstellungen.SetEinstellung<string>("HUE_Registerkey", appKey);
                        pressed = true;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                if (string.IsNullOrEmpty(appKey))
                    return;
            }
            else
            {
                //If you already registered an appname, you can initialize the HueClient with the app's key:
                Client.Initialize(appKey);
            }
            //Search for new lights
            await Client.SearchNewLightsAsync(lstDeviceID);
            //Get all lights
            var resultLights = await Client.GetLightsAsync();

            lstHUELights = resultLights as List<Light>;
        }


        #endregion HUE Lampen
    }

    #region LiteraturItem

    public class LiteraturItem : INotifyPropertyChanged
    {
        public LiteraturItem() : this(new Model.Literatur())
        {
        }

        public LiteraturItem(Model.Literatur l)
        {
            Literatur = l;
            Literatur.PropertyChanged += Literatur_PropertyChanged;

            onOpenFileDialog = new Base.CommandBase(OpenFileDialog, null);
            onOpenUrlPdf = new Base.CommandBase(OpenUrlPdf, null);
            onOpenUrlPrint = new Base.CommandBase(OpenUrlPrint, null);
            onOpenPdf = new Base.CommandBase(OpenPdf, null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Model.Literatur Literatur { get; set; }

        public string UrlPdf
        {
            get { return Literatur.UrlPdf; }
            set { Literatur.UrlPdf = value; }
        }

        public string UrlPrint
        {
            get { return Literatur.UrlPrint; }
            set { Literatur.UrlPrint = value; }
        }

        public string Abkürzung
        {
            get { return Literatur.Abkürzung; }
            set { Literatur.Abkürzung = value; }
        }

        public string Name
        {
            get { return Literatur.Name; }
            set { Literatur.Name = value; }
        }

        public string Pfad
        {
            get { return Literatur.Pfad; }
            set { Literatur.Pfad = value; }
        }

        public int Seitenoffset
        {
            get { return Literatur.Seitenoffset; }
            set { Literatur.Seitenoffset = value; }
        }

        public bool? IsOriginal
        {
            get { return Literatur.IsOriginal; }
        }

        public Base.CommandBase OnOpenPdf
        {
            get { return onOpenPdf; }
        }

        public Base.CommandBase OnOpenFileDialog
        {
            get { return onOpenFileDialog; }
        }

        public Base.CommandBase OnOpenUrlPdf
        {
            get { return onOpenUrlPdf; }
        }

        public Base.CommandBase OnOpenUrlPrint
        {
            get { return onOpenUrlPrint; }
        }

        protected void OnChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private readonly Base.CommandBase onOpenPdf;

        private readonly Base.CommandBase onOpenFileDialog;

        private readonly Base.CommandBase onOpenUrlPdf;

        private readonly Base.CommandBase onOpenUrlPrint;

        private void OpenPdf(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(Pfad))
                {
                    Logic.General.Pdf.OpenFileInReader(Pfad);
                }
            }
            catch (Exception ex)
            {
                View.General.ViewHelper.ShowError(string.Format("Das PDF konnte nicht geöffnet werden.\nReader: {0}\nDatei: {1}\n", Logic.General.Pdf.OpenCommand, Pfad), ex);
            }
        }

        private void OpenFileDialog(object obj)
        {
            var file = View.General.ViewHelper.ChooseFile(string.Format("Zu '{0}' ein PDF auswählen", Name), string.Format("{0}.pdf", Name), false, true, "pdf");
            if (string.IsNullOrEmpty(file))
            {
                return;
            }

            Pfad = file;

            OnChanged(nameof(IsOriginal));
        }

        private void OpenUrlPdf(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(UrlPdf))
                {
                    System.Diagnostics.Process.Start(UrlPdf);
                }
            }
            catch (Exception) { }
        }

        private void OpenUrlPrint(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(UrlPrint))
                {
                    System.Diagnostics.Process.Start(UrlPrint);
                }
            }
            catch (Exception) { }
        }

        private void Literatur_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnChanged(e.PropertyName);
        }
    }

    #endregion LiteraturItem

    #region EinstellungItem

    /// <summary>
    /// Falls typsensitive Hilfsklassen gebraucht werden.
    /// </summary>
    public class EinstellungItemString : EinstellungItemGeneric<string>
    {
        public EinstellungItemString(Model.Einstellung e) : base(e)
        {
        }
    }

    public class EinstellungItemBoolean : EinstellungItemGeneric<bool>
    {
        public EinstellungItemBoolean(Model.Einstellung e) : base(e)
        {
        }
    }

    public class EinstellungItemInteger : EinstellungItemGeneric<int>
    {
        public EinstellungItemInteger(Model.Einstellung e) : base(e)
        {
        }
    }

    public class EinstellungItemDouble : EinstellungItemGeneric<double>
    {
        public EinstellungItemDouble(Model.Einstellung e) : base(e)
        {
        }
    }

    public class EinstellungItemGeneric<T> : EinstellungItem
    {
        public EinstellungItemGeneric(Model.Einstellung e) : base(e)
        {
        }

        public T Wert
        {
            get { return einstellung.Get<T>(); }
            set { einstellung.Set(value); }
        }
    }

    public class EinstellungItem : INotifyPropertyChanged
    {
        public EinstellungItem(Model.Einstellung e)
        {
            einstellung = e;
            einstellung.PropertyChanged += einstellung_PropertyChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Kontext
        {
            get { return einstellung.Kontext; }
            set { einstellung.Kontext = value; }
        }

        public string Kategorie
        {
            get { return einstellung.Kategorie; }
            set { einstellung.Kategorie = value; }
        }

        public string Name
        {
            get { return einstellung.Name; }
            set { einstellung.Name = value; }
        }

        public string Beschreibung
        {
            get { return einstellung.Beschreibung; }
            set { einstellung.Beschreibung = value; }
        }

        public string Typ
        {
            get { return einstellung.Typ; }
            set { einstellung.Typ = value; }
        }

        public Type Type
        {
            get { return einstellung.Type; }
        }

        public static EinstellungItem GetTypedEinstellungItem(Model.Einstellung e)
        {
            if (e.Type == typeof(bool))
            {
                return new EinstellungItemBoolean(e);
            }

            if (e.Type == typeof(string))
            {
                return new EinstellungItemString(e);
            }

            if (e.Type == typeof(int))
            {
                return new EinstellungItemInteger(e);
            }

            if (e.Type == typeof(double))
            {
                return new EinstellungItemDouble(e);
            }

            return Impromptu.InvokeConstructor(typeof(EinstellungItemGeneric<>).MakeGenericType(e.Type), e);
        }

        protected Model.Einstellung einstellung = null;

        protected void OnChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void einstellung_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Einstellung.Value))
            {
                OnChanged(nameof(Einstellung.Wert));
            }
            else if (e.PropertyName == nameof(einstellung.Wert))
            { }
            else
            {
                OnChanged(e.PropertyName);
            }
        }
    }

    #endregion EinstellungItem
}
