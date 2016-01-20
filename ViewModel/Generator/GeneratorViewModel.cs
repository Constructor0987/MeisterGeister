using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.IO;
using MeisterGeister.View.Windows;
using System.Windows;
using MeisterGeister.ViewModel.Generator.Container;
using MeisterGeister.ViewModel.Generator.Factorys;
using MeisterGeister.Logic.General;
using MeisterGeister.Logic.Extensions;
using System.Windows.Data;

namespace MeisterGeister.ViewModel.Generator
{
    public class GeneratorViewModel : Base.ToolViewModelBase
    {
        #region //---- FELDER ----
        //Intern: Zeichenketten und weiteres
        const string NAMEN_RASSE_EGAL = "Beliebige Rasse";
        const string NAMEN_KULTUR_EGAL = "Beliebige Kultur";
        const string NAMEN_NAMENSGENERATOR_EGAL = "irgendein Name";
        const string NAMEN_STAND_EGAL = "irgendein Stand";
        const string GENERATOR_NAMEN = "Namen";
        const string GENERATOR_ORTSNAMEN = "Ortsnamen";
        const string GENERATOR_NSC = "NSC";
        const string GENERATOR_SCHATZ = "Schatz";
        const string GENERATOR_BIBLIOTHEK = "Bibliothek";
        const string GENERATOR_ALCHEMIELABOR = "Alchemielabor";
        readonly int STÄNDE_ANZAHL = Enum.GetNames(typeof(Stand)).Length;

        //UI
        private string _selectedRasse;
        private string _selectedKultur;
        private Stand _selectedStand;
        private bool _selectedStandZufällig;
        private string _selectedGenerator;
        private string _selectedNamensgenerator;
        private int _zuGenerierendeObjekte;
        private int _geschlechtWeiblichProzent;
        private bool _unüblicheKulturen = false;
        private string _infoText = string.Empty;
        private bool _zeigeZusatzinformationen;

        //Resource Manager für die Zusatzinformationen
        ResourceDictionary _zusatzinformationen;
        private FlowDocument _zusatzinformation;

        //Entitylisten
        private List<string> _rasseListe = new List<string>();
        private List<string> _kulturListe = new List<string>();
        private List<string> _standListe = new List<string>();
        private ExtendedObservableCollection<Object> _generierteObjekteListe = new ExtendedObservableCollection<Object>();
        private List<string> _generatorListe = new List<string>();
        private List<string> _namensgeneratorListe = new List<string>();
        //Commands
        private Base.CommandBase _onResetNamenOptionen;
        private Base.CommandBase _onGenerate;
        private Base.CommandBase _onClearGenerierteObjekteListe;
        private Base.CommandBase _onAddSelectedObject;
        private Base.CommandBase _onAddAllObjects;


        #endregion

        #region //---- EIGENSCHAFTEN ----

        public string InfoText
        {
            get { return _infoText; }
            set
            {
                _infoText = value;
                OnChanged("InfoText");
            }
        }

        public FlowDocument Zusatzinformation
        {
            get { return _zusatzinformation; }
            set
            {
                _zusatzinformation = value;
                OnChanged("Zusatzinformation");
            }
        }

        public bool ZeigeZusatzinformationen
        {
            get { return _zeigeZusatzinformationen; }
            set
            {
                _zeigeZusatzinformationen = value;
                OnChanged("ZeigeZusatzinformationen");
            }
        }

        public string SelectedRasse
        {
            get { return _selectedRasse; }
            set
            {
                if (_selectedRasse == value) return;
                _selectedRasse = value;
                OnChanged("SelectedRasse");
                RefreshKulturenListe();
            }
        }

        public string SelectedKultur
        {
            get { return _selectedKultur; }
            set
            {
                if (SelectedKultur == value) return;
                _selectedKultur = value;
                OnChanged("SelectedKultur");
                RefreshRassenListe();
            }
        }

        public string SelectedStand
        {
            get
            {
                if (_selectedStandZufällig) return NAMEN_STAND_EGAL;
                return _selectedStand.ToString("f");
            }
            set
            {
                if (value == NAMEN_STAND_EGAL)
                {
                    _selectedStandZufällig = true;
                }
                else
                {
                    _selectedStandZufällig = false;
                    _selectedStand = (Stand)Enum.Parse(typeof(Stand), value);
                }

                OnChanged("SelectedStand");
            }
        }

        public string SelectedGenerator
        {
            get { return _selectedGenerator; }
            set
            {
                _selectedGenerator = value;
                OnChanged("SelectedGenerator");
            }
        }
        public string SelectedNamensgenerator
        {
            get { return _selectedNamensgenerator; }
            set
            {
                _selectedNamensgenerator = value;
                OnChanged("SelectedNamensgenerator");
                UpdateZusatzinformationen(value);
            }
        }
        public int GeschlechtWeiblichProzent
        {
            get { return _geschlechtWeiblichProzent; }
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > 100)
                    value = 100;
                if (value == _geschlechtWeiblichProzent) return;
                _geschlechtWeiblichProzent = value;
                OnChanged("GeschlechtWeiblichProzent");
            }
        }

        //EntityListen
        public List<string> RasseListe
        {
            get { return _rasseListe; }
            set
            {
                _rasseListe = value;
                OnChanged("RasseListe");
            }
        }

        public List<string> KulturListe
        {
            get { return _kulturListe; }
            set
            {
                _kulturListe = value;
                OnChanged("KulturListe");
            }
        }

        public List<string> StandListe
        {
            get { return _standListe; }
            set
            {
                _standListe = value;
                OnChanged("StandListe");
            }
        }

        public List<string> GeneratorListe
        {
            get { return _generatorListe; }
            set
            {
                _generatorListe = value;
                OnChanged("GeneratorListe");
            }
        }

        public List<string> NamensgeneratorListe
        {
            get { return _namensgeneratorListe; }
            set
            {
                _namensgeneratorListe = value;
                OnChanged("NamensgeneratorListe");
            }
        }

        public ExtendedObservableCollection<Object> GenerierteObjekteListe
        {
            get { return _generierteObjekteListe; }
            set
            {
                _generierteObjekteListe = value;
                OnChanged("GenerierteObjekteListe");
            }
        }

        public int ZuGenerierendeObjekte
        {
            get { return _zuGenerierendeObjekte; }
            set
            {
                if (value < 1)
                    value = 1;
                if (value == _zuGenerierendeObjekte) return;
                _zuGenerierendeObjekte = value;
                OnChanged("ZuGenerierendeObjekte");
            }
        }

        public bool UnüblicheKulturen
        {
            get { return _unüblicheKulturen; }
            set
            {
                _unüblicheKulturen = value;
                OnChanged("UnüblicheKulturen");
                RefreshKulturenListe();
                RefreshRassenListe();
            }
        }

        //Commands
        public Base.CommandBase OnResetNamenOptionen
        {
            get { return _onResetNamenOptionen; }
        }

        public Base.CommandBase OnClearGenerierteObjekteListe
        {
            get { return _onClearGenerierteObjekteListe; }
        }

        public Base.CommandBase OnGenerate
        {
            get { return _onGenerate; }
        }

        public Base.CommandBase OnAddAllObjects
        {
            get { return _onAddAllObjects; }
        }

        public Base.CommandBase OnAddSelectedObjects
        {
            get { return _onAddSelectedObject; }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public GeneratorViewModel()
        {
            //_zusatzinformationenManager = new System.Resources.ResourceManager("MeisterGeister.View.Generator.GeneratorNamenInfos", this.GetType().Assembly);
            Uri u = new Uri("ViewModel/Generator/Zusatzinformationen/Zusatzinformationen.xaml", UriKind.RelativeOrAbsolute);
            _zusatzinformationen = Application.LoadComponent(u) as ResourceDictionary;
            _onGenerate = new Base.CommandBase(Generate, null);
            _onResetNamenOptionen = new Base.CommandBase(ResetNamenOptionen, null);
            _onClearGenerierteObjekteListe = new Base.CommandBase(ClearGenerierteObjekte, null);
            _onAddSelectedObject = new Base.CommandBase(AddSelectedNSC, null);
            _onAddAllObjects = new Base.CommandBase(AddAllNSC, null);
            Init();
        }

        #endregion

        #region //---- INSTANZMETHODEN ----
        public void Refresh()
        {
            // derzeit nichts beim erneuten Anzeigen der Tabs erforderlich
        }

        private void Init()
        {
            RasseListe.Add(NAMEN_RASSE_EGAL);
            RasseListe.AddRange(Global.ContextHeld.getRasseByKulturName(null, UnüblicheKulturen));
            SelectedRasse = RasseListe[0];
            OnChanged("RasseListe");

            // Kulturen werden über SelectedRasse gesetzt

            GeneratorListe.Add(GENERATOR_NAMEN);
            GeneratorListe.Add(GENERATOR_ORTSNAMEN);
            GeneratorListe.Add(GENERATOR_NSC);
            GeneratorListe.Add(GENERATOR_ALCHEMIELABOR);
            GeneratorListe.Add(GENERATOR_BIBLIOTHEK);
            GeneratorListe.Add(GENERATOR_SCHATZ);
            SelectedGenerator = GENERATOR_NAMEN;
            OnChanged("GeneratorListe");

            NamensgeneratorListe.Add(NAMEN_NAMENSGENERATOR_EGAL);
            NamensgeneratorListe.AddRange(MeisterGeister.Logic.General.ReflectionHelper.GetConstantValueStringCollection(typeof(MeisterGeister.ViewModel.Generator.Factorys.NamenFactoryHelper), false, false).Cast<string>().ToList());
            SelectedNamensgenerator = NAMEN_NAMENSGENERATOR_EGAL;
            OnChanged("NamensgeneratorListe");

            StandListe.Add(NAMEN_STAND_EGAL);
            StandListe.AddRange(Enum.GetNames(typeof(Stand)));
            SelectedStand = NAMEN_STAND_EGAL;
            OnChanged("StandListe");

            UnüblicheKulturen = false;

            GeschlechtWeiblichProzent = 50; ;

            ZuGenerierendeObjekte = 1;
        }

        private void RefreshRassenListe()
        {
            List<string> neueRassenListe = new List<string>();
            neueRassenListe.Add(NAMEN_RASSE_EGAL);
            if (SelectedKultur == NAMEN_KULTUR_EGAL)
            {
                neueRassenListe.AddRange(Global.ContextHeld.getRasseByKulturName(null, UnüblicheKulturen));
            }
            else
            {
                neueRassenListe.AddRange(Global.ContextHeld.getRasseByKulturName(SelectedKultur, UnüblicheKulturen));
            }
            RasseListe = neueRassenListe;
            if (!RasseListe.Contains(SelectedRasse))
                SelectedRasse = RasseListe[0];
        }

        private void RefreshKulturenListe()
        {
            List<string> neueKulturenListe = new List<string>();
            neueKulturenListe.Add(NAMEN_KULTUR_EGAL);
            if (SelectedRasse == NAMEN_RASSE_EGAL)
            {
                neueKulturenListe.AddRange(Global.ContextHeld.getKulturByRasseName(null, UnüblicheKulturen));
            }
            else
            {
                neueKulturenListe.AddRange(Global.ContextHeld.getKulturByRasseName(SelectedRasse, UnüblicheKulturen));
            }
            KulturListe = neueKulturenListe;
            if (!KulturListe.Contains(SelectedKultur))
                SelectedKultur = KulturListe[0];
        }

        private IEnumerable<PersonNurName> GeneriereNamen()
        {
            if (SelectedNamensgenerator == NAMEN_NAMENSGENERATOR_EGAL)
            {
                return from x in Enumerable.Range(0, ZuGenerierendeObjekte)
                       select GeneriereName(NamensgeneratorListe[RandomNumberGenerator.Generator.Next(NamensgeneratorListe.Count() - 1) + 1]);
            }
            else
            {
                return from x in Enumerable.Range(0, ZuGenerierendeObjekte)
                       select GeneriereName(SelectedNamensgenerator);
            }

        }

        private PersonNurName GeneriereName(string namenfactory)
        {
            return NamenFactoryHelper.GetFactory(namenfactory).GeneratePersonNurName(
                RandomNumberGenerator.Generator.Next(100) < GeschlechtWeiblichProzent ? Geschlecht.männlich : Geschlecht.weiblich,
                _selectedStandZufällig ? (Stand)RandomNumberGenerator.Generator.Next(STÄNDE_ANZAHL) : _selectedStand);
        }



        #endregion

        #region //---- EVENTS ----

        void ClearGenerierteObjekte(object sender)
        {
            GenerierteObjekteListe.Clear();
            OnChanged("GenerierteObjekteListe");
        }

        void ResetNamenOptionen(object sender)
        {
            SelectedRasse = NAMEN_RASSE_EGAL;
            SelectedKultur = NAMEN_KULTUR_EGAL;
            SelectedNamensgenerator = NAMEN_NAMENSGENERATOR_EGAL;
            GeschlechtWeiblichProzent = 50;
            SelectedStand = NAMEN_STAND_EGAL;
            
        }

        void Generate(object sender)
        {
            Global.SetIsBusy(true, "Objekte werden generiert...");
            switch (SelectedGenerator)
            {
                case (GENERATOR_NAMEN): GenerierteObjekteListe.AddRange(GeneriereNamen()); break;
                case (GENERATOR_ALCHEMIELABOR):
                case (GENERATOR_BIBLIOTHEK):
                case (GENERATOR_NSC):
                case (GENERATOR_SCHATZ): break;
            }
            GenerierteObjekteListe = GenerierteObjekteListe;
            Global.SetIsBusy(false);
        }

        void AddSelectedNSC(object sender)
        {
            //TODO
            AddNscToNotiz(GenerierteObjekteListe);
        }


        void AddAllNSC(object sender)
        {
            AddNscToNotiz(GenerierteObjekteListe);
        }

        private void AddNscToNotiz(IList<Object> liste)
        {
            if (liste == null) return;
            if (liste.Count() == 0) return;
            StringBuilder ausgabe = new StringBuilder();
            foreach (Object o in liste)
                ausgabe.Append("\n\n--------- " + MeisterGeister.Logic.Kalender.Datum.Aktuell.ToStringShort() + "---------\n" + o.ToString());
            Global.ContextNotizen.NotizAllgemein.AppendText(ausgabe.ToString());
            InfoText = string.Format("{0} Objekte gespeichert.", liste.Count());
        }

        private void UpdateZusatzinformationen(string key)
        {
            if (key != NAMEN_NAMENSGENERATOR_EGAL && NamenFactoryHelper.GetFactory(key).InformationenNamenVerfügbar)
            {
                Zusatzinformation = _zusatzinformationen[key] as FlowDocument;
                ZeigeZusatzinformationen = true;
            } else {
                Zusatzinformation = _zusatzinformationen["keine_Infos"] as FlowDocument;
                ZeigeZusatzinformationen = false;
            }
        }

        #endregion
    }

    [ValueConversion(typeof(Geschlecht), typeof(string))]
    public class GeschlechtToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((Geschlecht)value)
            {
                case Geschlecht.männlich:
                    return "/Images/Icons/geschlecht_m.png";
                case Geschlecht.weiblich:
                    return "/Images/Icons/geschlecht_w.png";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}


