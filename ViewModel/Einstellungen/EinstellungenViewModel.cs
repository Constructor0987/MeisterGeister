using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ImpromptuInterface;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.Settings
{
    public class EinstellungenViewModel : Base.ViewModelBase
    {
        public List<string> lstDeviceID
        {
            get { return _lstDeviceID; }
            set { Set(ref _lstDeviceID, value); }
        }

        private List<string> _lstDeviceID = new List<string>();

        #region Property

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

        private void SelectHUEColor(object obj)
        {
        }

        private void DoTheme(object obj)
        {
        }

        private void NeuesHUETheme(object obj)
        {
        }

        private void TBtnThemeProcessColor(object obj)
        {
        }

        private void AddHUEProcess(object obj)
        {
        }

        private void HUEGWsuchen(object obj)
        {
        }

        private void ActivateHUEGW(object obj)
        {
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
            }
        }

        #endregion Public Methods
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
