using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.IO;
using MeisterGeister.View.Windows;
using System.Windows;

namespace MeisterGeister.ViewModel.Generator
{
    class GeneratorViewModel : Base.ViewModelBase
    {

        #region //---- FELDER ----

        //Intern
        bool IsLoaded = false;

        //UI
        private Model.Rasse _selectedRasse;
        private Model.Kultur _selectedKultur;
        private string _selectedProfession;
        private string _selectedAlter;
        private string _selectedGeschlecht;
        private bool _professionListeIsEnabled;
        private bool _kulturListeIsEnabled;
        private bool _rasseListeIsEnabled;
        private bool _geschlechtListeIsEnabled;
        private int _genNumber;
        private bool _cBKulturenChecked = true;
        private bool _cBNamenIsChecked;

        //Entitylisten
        private List<Model.Rasse> _rasseListe = new List<Model.Rasse>();
        private List<Model.Kultur> _kulturListe = new List<Model.Kultur>();
        private List<string> _professionListe = new List<string>();
        private List<string> _alterListe = new List<string>();
        private List<string> _geschlechtListe = new List<string>();
        private List<Person> _personenListe = new List<Person>();
        //Commands
        private Base.CommandBase _onReset;
        private Base.CommandBase _onGenerate;
        private Base.CommandBase _onAddAllNSC;

        #endregion

        #region //---- EIGENSCHAFTEN ----

        //Felder        

        //UI
        public IList<Person> SelectedPersonen
        {
            get;
            set;
        }

        private string _infoText = string.Empty;

        public string InfoText
        {
            get { return _infoText; }
            set
            {
                _infoText = value;
                OnChanged("InfoText");
            }
        }

        public Model.Rasse SelectedRasse
        {
            get { return _selectedRasse; }
            set
            {
                _selectedRasse = value;
                OnChanged("SelectedRasse");
                if (value != null && value.Name != "keine Rasse" )
                {
                    KulturListe = Global.ContextNsc.getKulturenByRasseName(value, CBKulturenIsChecked);
                    SelectedKultur = KulturListe[0];
                    RasseListeIsEnabled = false;
                    if(SelectedKultur.Name=="keine Kultur"){

                    }
                }
            }
        }

        public Model.Kultur SelectedKultur
        {
            get { return _selectedKultur; }
            set
            {
                _selectedKultur = value;
                OnChanged("SelectedKultur");
                if ( value != null && value.Name != "keine Kultur" )
                {
                    if(RasseListeIsEnabled){
                        RasseListe = Global.ContextNsc.getRasseByKulturName(value);
                        SelectedRasse = RasseListe[0];
                    }
                    if (ProfessionListeIsEnabled)
                    {
                        ProfessionListe = Global.ContextNsc.getProfessionenNamenByKultur(value);
                        SelectedProfession = "Keine Profession";
                    }
                    KulturListeIsEnabled = false;
                }
            }
        }

        public string SelectedProfession
        {
            get { return _selectedProfession; }
            set
            {
                _selectedProfession = value;
                OnChanged("SelectedProfession");
            }
        }

        public string SelectedAlter
        {
            get { return _selectedAlter; }
            set
            {
                _selectedAlter = value;
                OnChanged("SelectedAlter");
            }
        }

        public string SelectedGeschlecht
        {
            get { return _selectedGeschlecht; }
            set
            {
                _selectedGeschlecht = value;
                OnChanged("SelectedGeschlecht");
                //handle Gblins und Orks
               /* if (value == "m")
                {
                    List<Model.Kultur> tmp=Global.ContextNsc.getKulturenDistinct();
                    KulturListe = tmp.Where(k => !k.Variante.EndsWith("-Frau")).ToList();
                    SelectedKultur = KulturListe[0];
                }
                else if (value == "w")
                {
                    List<Model.Kultur> tmp = Global.ContextNsc.getKulturenDistinct();
                    KulturListe = tmp.Where(k => !k.Variante.EndsWith("-Mann")).ToList();
                    SelectedKultur = KulturListe[0];
                }
                GeschlechtListeIsEnabled = false;*/
            }
        }
        public bool GeschlechtListeIsEnabled
        {
            get { return _geschlechtListeIsEnabled; }
            set
            {
                _geschlechtListeIsEnabled = value;
                OnChanged("GeschlechtListeIsEnabled");
            }
        }
        //EntityListen
        public List<Model.Rasse> RasseListe
        {
            get { return _rasseListe; }
            set
            {
                _rasseListe = value;
                OnChanged("RasseListe");
            }
        }
        public bool RasseListeIsEnabled
        {
            get { return _rasseListeIsEnabled; }
            set
            {
                _rasseListeIsEnabled = value;
                OnChanged("RasseListeIsEnabled");
            }
        }

        public List<Model.Kultur> KulturListe
        {
            get { return _kulturListe; }
            set
            {
                _kulturListe = value;
                OnChanged("KulturListe");
            }
        }
        public bool KulturListeIsEnabled
        {
            get { return _kulturListeIsEnabled; }
            set
            {
                _kulturListeIsEnabled = value;
                OnChanged("KulturListeIsEnabled");
            }
        }

        public List<string> ProfessionListe
        {
            get { return _professionListe; }
            set
            {
                _professionListe = value;
                OnChanged("ProfessionListe");
            }
        }

        public bool ProfessionListeIsEnabled
        {
            get { return _professionListeIsEnabled; }
            set
            {
                _professionListeIsEnabled = value;
                OnChanged("ProfessionListeIsEnabled");
            }
        }

        public List<string> AlterListe
        {
            get { return _alterListe; }
            set
            {
                _alterListe = value;
                OnChanged("AlterListe");
            }
        }

        public List<string> GeschlechtListe
        {
            get { return _geschlechtListe; }
            set
            {
                _geschlechtListe = value;
                OnChanged("GeschlechtListe");
            }
        }

        public List<Person> PersonenListe
        {
            get { return _personenListe; }
            set
            {
                _personenListe = value;
                OnChanged("PersonenListe");
            }
        }

        public int GenNumber
        {
            get { return _genNumber; }
            set
            {
                _genNumber = value;
                OnChanged("GenNumber");
            }
        }

        public bool CBKulturenIsChecked
        {
            get { return _cBKulturenChecked; }
            set
            {
                _cBKulturenChecked = value;
                OnChanged("CBKulturenIsChecked");
                IsLoaded = false;
                LoadDaten();
            }
        }

        public bool CBNamenIsChecked
        {
            get { return _cBNamenIsChecked; }
            set
            {
                _cBNamenIsChecked = value;
                OnChanged("CBNamenIsChecked");
            }
        }
        
        //Zuordnung


        //Commands
        public Base.CommandBase OnReset
        {
            get { return _onReset; }
        }

        public Base.CommandBase OnGenerate
        {
            get { return _onGenerate; }
        }

        public Base.CommandBase OnAddAllNSC
        {
            get { return _onAddAllNSC; }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public GeneratorViewModel()
        {
            _onReset = new Base.CommandBase(Reset, null);
            _onGenerate = new Base.CommandBase(Generate, null);
            _onAddAllNSC = new Base.CommandBase(AddAllNSC, null);
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void LoadDaten()
        {
            if (IsLoaded == false)
            {
                                
                RasseListe = Global.ContextNsc.getRassenNameDistinct();
                SelectedRasse = RasseListe[0];
                RasseListeIsEnabled = true;

                KulturListe = Global.ContextNsc.getKulturenDistinct();
                SelectedKultur = KulturListe[0];
                KulturListeIsEnabled = true;

                ProfessionListe = Global.ContextNsc.getProfessionenNamenDistinct();
                //OnChanged("ProfessionListe");
                SelectedProfession = "keine Profession";
                ProfessionListeIsEnabled = true;

                AlterListe = Global.ContextNsc.getAltersklassen();
                //OnChanged("AlterListe");
                SelectedAlter = "kein Alter";

                GeschlechtListe = (new string[] { "m/w", "m", "w" }).ToList();
                //OnChanged("GeschlechtListe");
                SelectedGeschlecht = "m/w";
                GeschlechtListeIsEnabled = true;

                GenNumber = 1;

                IsLoaded = true;
            }
        }

        #endregion

        #region //---- EVENTS ----

        void Reset(object sender)
        {
            IsLoaded = false;
            LoadDaten();
        }

        void Generate(object sender)
        {
            Global.SetIsBusy(true, "NSCs werden generiert...");

            PersonenListe.Clear();
            List<Person> personen = new List<Person>();
            for (int i = 0; i < GenNumber; i++)
            {
                Person person = new Person(SelectedGeschlecht, SelectedAlter, SelectedRasse, SelectedKultur, SelectedProfession, CBKulturenIsChecked, CBNamenIsChecked) { IsNurName = (CBNamenIsChecked) ? Visibility.Collapsed : Visibility.Visible };
                //handle Goblin & Orks
                if (!(person.Name == null))
                    personen.Add(person);
            }
            PersonenListe = personen;

            Global.SetIsBusy(false);
        }

        void AddAllNSC(object sender)
        {
            AddNscToNotiz(PersonenListe);
        }

        internal void AddNscToNotiz(System.Collections.IList personListe)
        {
            int count = 0;
            if (personListe != null && personListe.Count > 0)
            {
                count = personListe.Count;
                string nsc = string.Empty;
                foreach (Person person in personListe)
                {
                    nsc += "\n--------- " + MeisterGeister.Logic.Kalender.Datum.Aktuell.ToStringShort() + "---------\n";
                    nsc += person.toString();
                }
                if (nsc != string.Empty)
                {
                    Global.ContextNotizen.NotizAllgemein.AppendText(nsc);
                }
            }
            InfoText = string.Format("{0} NSC gespeichert.", count);
        }

        #endregion
    }
}


