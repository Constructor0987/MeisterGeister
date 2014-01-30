using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ImpromptuInterface;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.Settings
{

    public class EinstellungenViewModel : Base.ViewModelBase
    {

        #region Property

        public Boolean IsMitUeberlastung
        {
            get { return MeisterGeister.Logic.Einstellung.Einstellungen.IsMitUeberlastung; }
            set
            {
                MeisterGeister.Logic.Einstellung.Einstellungen.IsMitUeberlastung = value;
                OnChanged("IsMitUeberlastung");
            }
        }

        private List<EinstellungItem> einstellungListe;
        public List<EinstellungItem> EinstellungListe
        {
            get { return einstellungListe; }
            set
            {
                einstellungListe = value;
                OnChanged("EinstellungListe");
            }
        }

        public ermittleRuestung BerechnungRuestung
        {
            get { return (ermittleRuestung)MeisterGeister.Logic.Einstellung.Einstellungen.RSBerechnung; }
            set
            {
                switch (value)
                {
                    case ermittleRuestung.Automatisch:
                        MeisterGeister.Logic.Einstellung.Einstellungen.RSBerechnung = (int)value;
                        break;
                    case ermittleRuestung.Einfach:
                        MeisterGeister.Logic.Einstellung.Einstellungen.RSBerechnung = (int)value;
                        break;
                    case ermittleRuestung.Zonen:
                        MeisterGeister.Logic.Einstellung.Einstellungen.RSBerechnung = (int)value;
                        break;
                    default:
                        return;
                }
                OnChanged("BerechnungRuestung");
            }
        }

        public ermittleBehinderung BerechnungBehinderung
        {
            get { return (ermittleBehinderung)MeisterGeister.Logic.Einstellung.Einstellungen.BEBerechnung; }
            set
            {
                switch (value)
                {
                    case ermittleBehinderung.Automatisch:
                        MeisterGeister.Logic.Einstellung.Einstellungen.BEBerechnung = (int)value;
                        break;
                    case ermittleBehinderung.Eingabe:
                        MeisterGeister.Logic.Einstellung.Einstellungen.BEBerechnung = (int)value;
                        break;
                    default:
                        return;
                }
                OnChanged("BerechnungBehinderung");
            }
        }

        public ermitteleUeberlastung BerechnungUeberlastung
        {
            get { return (ermitteleUeberlastung)MeisterGeister.Logic.Einstellung.Einstellungen.UeberlastungBerechnung; }
            set
            {
                switch (value)
                {
                    case ermitteleUeberlastung.Automatisch:
                        MeisterGeister.Logic.Einstellung.Einstellungen.UeberlastungBerechnung = (int)value;
                        break;
                    case ermitteleUeberlastung.Eingabe:
                        MeisterGeister.Logic.Einstellung.Einstellungen.UeberlastungBerechnung = (int)value;
                        break;
                    default:
                        return;
                }
                OnChanged("BerechnungUeberlastung");
            }
        }        


        [DependentProperty("EinstellungListe")]
        public List<EinstellungItem> InventarListe
        {
            get
            {
                if (EinstellungListe == null)
                    return null;
                return EinstellungListe.Where(e => e.Kontext == "Inventar").ToList();
            }
        }

        [DependentProperty("EinstellungListe")]
        public List<String> KontextListe
        {
            get
            {
                if (EinstellungListe == null)
                    return null;
                return EinstellungListe.Select(e => e.Kontext).Distinct().ToList();
            }
            set { KontextListe = value; }
        }

        [DependentProperty("EinstellungListe")]
        public List<EinstellungItem> AllgemeinListe
        {
            get
            {
                if (EinstellungListe == null)
                    return null;
                return EinstellungListe.Where(e => e.Kontext == "Allgemein").ToList();
            }
        }

        [DependentProperty("EinstellungListe")]
        public List<EinstellungItem> ProbenListe
        {
            get
            {
                if (EinstellungListe == null)
                    return null;
                return EinstellungListe.Where(e => e.Kontext == "Proben").ToList();
            }
        }

        [DependentProperty("EinstellungListe")]
        public List<EinstellungItem> KampfListe
        {
            get
            {
                if (EinstellungListe == null)
                    return null;
                return EinstellungListe.Where(e => e.Kontext == "Kampf").ToList();
            }
        }

        [DependentProperty("EinstellungListe")]
        public List<EinstellungItem> AudioplayerListe
        {
            get
            {
                if (EinstellungListe == null)
                    return null;
                return EinstellungListe.Where(e => e.Kontext == "Audioplayer").ToList();
            }
        }

        [DependentProperty("EinstellungListe")]
        public List<EinstellungItem> AlmanachListe
        {
            get
            {
                if (EinstellungListe == null)
                    return null;
                return EinstellungListe.Where(e => e.Kontext == "Almanach").ToList();
            }
        }

        public List<string> PDFReaders
        {
            get 
            { 
                return Logic.General.Pdf.readers.Keys.ToList<string>(); 
            }
        }

        private string _selectedPDFReader;
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

        private List<Model.Setting> settingListe;
        [DependentProperty("EinstellungListe")]
        public List<Model.Setting> SettingListe
        {
            get
            {
                if (settingListe == null)
                    return null;
                return settingListe;
            }
        }

        public List<LiteraturItem> LiteraturListe
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public EinstellungenViewModel()
        {
            LoadDaten();
        }
        #endregion

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
        #endregion
    }

    #region LiteraturItem

    public class LiteraturItem : INotifyPropertyChanged
    {
        protected Model.Literatur _literatur = null;
        public LiteraturItem(Model.Literatur l)
        {
            _literatur = l;
            _literatur.PropertyChanged += Literatur_PropertyChanged;

            onOpenFileDialog = new Base.CommandBase(OpenFileDialog, null);
            onOpenUrlPdf = new Base.CommandBase(OpenUrlPdf, null);
            onOpenUrlPrint = new Base.CommandBase(OpenUrlPrint, null);
        }

        private void OpenFileDialog(object obj)
        {
            string file = View.General.ViewHelper.ChooseFile(string.Format("Zu '{0}' ein PDF auswählen", Name), string.Format("{0}.pdf", Name), false, "pdf");
            if (string.IsNullOrEmpty(file))
                return;
            Pfad = file;
        }

        private void OpenUrlPdf(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(UrlPdf))
                    System.Diagnostics.Process.Start(UrlPdf);
            }
            catch (Exception) { }
        }

        private void OpenUrlPrint(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(UrlPrint))
                    System.Diagnostics.Process.Start(UrlPrint);
            }
            catch (Exception) { }
        }

        public String UrlPdf
        {
            get { return _literatur.UrlPdf; }
            set { _literatur.UrlPdf = value; }
        }

        public String UrlPrint
        {
            get { return _literatur.UrlPrint; }
            set { _literatur.UrlPrint = value; }
        }

        public String Abkürzung
        {
            get { return _literatur.Abkürzung; }
            set { _literatur.Abkürzung = value; }
        }

        public String Name
        {
            get { return _literatur.Name; }
            set { _literatur.Name = value; }
        }

        public String Pfad
        {
            get { return _literatur.Pfad; }
            set { _literatur.Pfad = value; }
        }

        public int Seitenoffset
        {
            get { return _literatur.Seitenoffset; }
            set { _literatur.Seitenoffset = value; }
        }

        public Nullable<bool> IsOriginal
        {
            get { return _literatur.IsOriginal; }
        }

        private void Literatur_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnChanged(e.PropertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Base.CommandBase onOpenFileDialog;
        public Base.CommandBase OnOpenFileDialog
        {
            get { return onOpenFileDialog; }
        }

        private Base.CommandBase onOpenUrlPdf;
        public Base.CommandBase OnOpenUrlPdf
        {
            get { return onOpenUrlPdf; }
        }

        private Base.CommandBase onOpenUrlPrint;
        public Base.CommandBase OnOpenUrlPrint
        {
            get { return onOpenUrlPrint; }
        }
    }

    #endregion

    #region EinstellungItem
    //Falls typsensitive Hilfsklassen gebraucht werden.
    public class EinstellungItemString : EinstellungItemGeneric<String>
    {
        public EinstellungItemString(Model.Einstellung e) : base(e) { }
    }

    public class EinstellungItemBoolean : EinstellungItemGeneric<Boolean>
    {
        public EinstellungItemBoolean(Model.Einstellung e) : base(e) { }
    }

    public class EinstellungItemInteger : EinstellungItemGeneric<int>
    {
        public EinstellungItemInteger(Model.Einstellung e) : base(e) { }
    }

    public class EinstellungItemDouble : EinstellungItemGeneric<double>
    {
        public EinstellungItemDouble(Model.Einstellung e) : base(e) { }
    }

    public class EinstellungItemGeneric<T> : EinstellungItem
    {
        public EinstellungItemGeneric(Model.Einstellung e) : base(e) { }

        public T Wert
        {
            get { return einstellung.Get<T>(); }
            set { einstellung.Set<T>(value); }
        }
    }

    public class EinstellungItem : INotifyPropertyChanged
    {
        protected Model.Einstellung einstellung = null;
        public EinstellungItem(Model.Einstellung e)
        {
            einstellung = e;
            einstellung.PropertyChanged += einstellung_PropertyChanged;
        }

        public String Kontext
        {
            get { return einstellung.Kontext; }
            set { einstellung.Kontext = value; }
        }

        public String Kategorie
        {
            get { return einstellung.Kategorie; }
            set { einstellung.Kategorie = value; }
        }

        public String Name
        {
            get { return einstellung.Name; }
            set { einstellung.Name = value; }
        }

        public String Beschreibung
        {
            get { return einstellung.Beschreibung; }
            set { einstellung.Beschreibung = value; }
        }

        public String Typ
        {
            get { return einstellung.Typ; }
            set { einstellung.Typ = value; }
        }

        public Type Type
        {
            get { return einstellung.Type; }
        }

        private void einstellung_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
                OnChanged("Wert");
            else if (e.PropertyName == "Wert")
            { }
            else
                OnChanged(e.PropertyName);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static EinstellungItem GetTypedEinstellungItem(Model.Einstellung e)
        {
            if (e.Type == typeof(Boolean))
                return new EinstellungItemBoolean(e);
            if (e.Type == typeof(String))
                return new EinstellungItemString(e);
            if (e.Type == typeof(int))
                return new EinstellungItemInteger(e);
            if (e.Type == typeof(double))
                return new EinstellungItemDouble(e);
            return Impromptu.InvokeConstructor(typeof(EinstellungItemGeneric<>).MakeGenericType(e.Type), e);
        }
    }
    #endregion

    public enum ermittleRuestung
    {
        Automatisch,
        Einfach,
        Zonen
    }
    public enum ermittleBehinderung
    {
        Automatisch,
        Eingabe
    }
    public enum ermitteleUeberlastung
    {
        Automatisch,
        Eingabe
    }
}
