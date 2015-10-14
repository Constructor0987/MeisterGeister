using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Controls;
//Eigene usings
using MeisterGeister.ViewModel.Base;
using MeisterGeister.Model;
using MeisterGeister.Model.Service;
using System.Collections.ObjectModel;
using MeisterGeister.ViewModel.ZooBot.Logic.Regionen;
using MeisterGeister.ViewModel.ZooBot.Logic.Landschaften;
using MeisterGeister.ViewModel.ZooBot.Logic.Pflanzen;
using MeisterGeister.ViewModel.ZooBot.Logic.Tiere;
using System.Collections;
using MeisterGeister.View.ZooBot;
using System.Windows.Forms;
using MeisterGeister.Logic.Kalender;
using MeisterGeister.Logic.General;
using System.Windows;
using MeisterGeister.Model.Extensions;

namespace MeisterGeister.ViewModel.ZooBot
{
    public class ZooBotViewModel : Base.ViewModelBase
    {
        public ArrayList m_regionen = new ArrayList();
        public ArrayList m_landschaften = new ArrayList();
        public ArrayList m_pflanzen = new ArrayList();
        public ArrayList m_tiere = new ArrayList();
        public List<string> m_geländekundig = new List<string>();
        
        public ArrayList Regionen{ get { return m_regionen; }}
        public ArrayList Landschaften{get { return m_landschaften; }}
        public ArrayList Pflanzen{get { return m_pflanzen; }}
        public ArrayList Tiere{get { return m_tiere; }}
        public List<string> Geländekundig{get {return m_geländekundig;}}

        private List<Gebiet> _gebietListe = new List<Gebiet>();
        public List<Gebiet> GebietListe
        {
            get { return _gebietListe; }
            set
            {
                _gebietListe = value;
                OnChanged();
            }
        }

        private List<Pflanze> _filterPflanzenListeNachGebietsauswahl = new List<Pflanze>();
        public List<Pflanze> FilterPflanzenListeNachGebietsauswahl
        {
            get { return _filterPflanzenListeNachGebietsauswahl; }
            set
            {
                _filterPflanzenListeNachGebietsauswahl = value;
                OnChanged();
            }
        }

        private List<Pflanze> _filterPflanzenListe = new List<Pflanze>();
        public List<Pflanze> FilterPflanzenListe
        {
            get { return _filterPflanzenListe; }
            set
            {
                _filterPflanzenListe = value;
                OnChanged();
            }
        }
        private List<Pflanze> _pflanzenListe = new List<Pflanze>();
        public List<Pflanze> PflanzenListe
        {
            get { return _pflanzenListe; }
            set
            {
                _pflanzenListe = value;
                OnChanged();
            }
        }

        private List<Landschaft> _landschaftenListe = new List<Landschaft>();
        public List<Landschaft> LandschaftenListe
        {
            get { return _landschaftenListe; }
            set
            {
                _landschaftenListe = value;
                OnChanged();
            }
        }

        private List<Landschaft> _landschaftenGebietListe = new List<Landschaft>();
        public List<Landschaft> LandschaftGebietListe
        {
            get { return _landschaftenGebietListe; }
            set
            {
                _landschaftenGebietListe = value;
                OnChanged();
            }
        }

        private List<Pflanze> _landschaftGebietPflanzenListe = new List<Pflanze>();
        public List<Pflanze> LandschaftGebietPflanzenListe
        {
            get { return _landschaftGebietPflanzenListe; }
            set
            {
                _landschaftGebietPflanzenListe = value;
                OnChanged();
            }
        }

        private Point _heldenPos = new Point();        
        public Point HeldenPos
        {
            get { return _heldenPos;}            
            set
            {
                _heldenPos = value;
                OnChanged();
            }
        }

        #region Initialisierung der implementierten Regionen, Landschaften, Pflanzen
        private void InitializeRegionen()
        {
            Regionen.Add(new RegionEwigesEis());
            Regionen.Add(new RegionNoerdlicheTundra());
            Regionen.Add(new RegionNoerdlicheGraslaenderUndSteppen());
            Regionen.Add(new RegionNoerdlichesHochland());
            Regionen.Add(new RegionKalkgebirge());
            Regionen.Add(new RegionAndereMittellaendischeGebirge());
            Regionen.Add(new RegionNoerdlicheWaelderWestkueste());
            Regionen.Add(new RegionNoerdlicheWaelderTaiga());
            Regionen.Add(new RegionNoerdlicheWaelderBornland());
            Regionen.Add(new RegionNoerdlicheSuempfeMarschenMoore());
            Regionen.Add(new RegionMittellaendischeGraslaenderHeideSteppe());
            Regionen.Add(new RegionMittellaendischeWaelderGemaesigt());
            Regionen.Add(new RegionMittellaendischeWaelderYaquir());
            Regionen.Add(new RegionMittellaendischeWaelderTobrisch());
            Regionen.Add(new RegionImmgergrueneWaelderSuedosten());
            Regionen.Add(new RegionSuedlaendischeGraslaenderSteppen());
            Regionen.Add(new RegionWuestenrandgebiete());
            Regionen.Add(new RegionWueste());
            Regionen.Add(new RegionSuedlaendischeGebirge());
            Regionen.Add(new RegionMaraskan());
            Regionen.Add(new RegionSuedlicheSuempfe());
            Regionen.Add(new RegionRegenwald());
            Regionen.Add(new RegionSuedlicheRegengebirge());
            Regionen.Add(new RegionIfirnsOzean());
            Regionen.Add(new RegionMeerSiebenWinde());
            Regionen.Add(new RegionSuedmeer());
            Regionen.Add(new RegionPerlenmeer());
        }

        private void InitializeLandschaften()
        {
            Landschaften.Add(new LandschaftEis());            
            Landschaften.Add(new LandschaftWuesteUndWuestenrand());
            Landschaften.Add(new LandschaftGebirge());
            Landschaften.Add(new LandschaftHochland());
            Landschaften.Add(new LandschaftSteppe());
            Landschaften.Add(new LandschaftGraslandWiesen());
            Landschaften.Add(new LandschaftFlussSeeuferTeiche());
            Landschaften.Add(new LandschaftKuesteStrand());
            Landschaften.Add(new LandschaftFlussauen());
            Landschaften.Add(new LandschaftSumpfMoor());
            Landschaften.Add(new LandschaftRegenwald());
            Landschaften.Add(new LandschaftWald());
            Landschaften.Add(new LandschaftWaldrand());
            Landschaften.Add(new LandschaftMeer());
            Landschaften.Add(new LandschaftHoehleFeucht());
            Landschaften.Add(new LandschaftHoehleTrocken());
        }

        private void InitializeGeländekundig()
        {
            Geländekundig.Add("Eiskundig");
            Geländekundig.Add("Wüstenkundig");
            Geländekundig.Add("Gebirgskundig");
            Geländekundig.Add("Steppenkundig");
            Geländekundig.Add("Sumpfkundig");
            Geländekundig.Add("Dschungelkundig");
            Geländekundig.Add("Waldkundig");
            Geländekundig.Add("Meereskundig");
            Geländekundig.Add("Höhlenkundig");
        }
        
        private void InitializePflanzen()
        {
            Pflanzen.Add(new PflanzeAlraune());
            Pflanzen.Add(new PflanzeAlveranie());
            Pflanzen.Add(new PflanzeArganstrauch());
            Pflanzen.Add(new PflanzeAtanKiefer());
            Pflanzen.Add(new PflanzeAtmon());
            Pflanzen.Add(new PflanzeAxordaBaum());
            Pflanzen.Add(new PflanzeBasilamine());
            Pflanzen.Add(new PflanzeBelmart());
            Pflanzen.Add(new PflanzeBlutblatt());
            Pflanzen.Add(new PflanzeBoronie());
            Pflanzen.Add(new PflanzeBoronsschlinge());
            Pflanzen.Add(new PflanzeCarlog());
            Pflanzen.Add(new PflanzeCheriaKaktus());
            Pflanzen.Add(new PflanzeChonchinis());
            Pflanzen.Add(new PflanzeDisdychonda());
            Pflanzen.Add(new PflanzeDonf());
            Pflanzen.Add(new PflanzeDornrose());
            Pflanzen.Add(new PflanzeEfeuer());
            Pflanzen.Add(new PflanzeEgelschreck());
            Pflanzen.Add(new PflanzeEitrigerKrötenschemel());
            Pflanzen.Add(new PflanzeFeuermoosEfferdmoos());
            Pflanzen.Add(new PflanzeFeuerschlick());
            Pflanzen.Add(new PflanzeFinage());
            Pflanzen.Add(new PflanzeGrüneSchleimschlange());
            Pflanzen.Add(new PflanzeGulmond());
            Pflanzen.Add(new PflanzeHiradwurz());
            Pflanzen.Add(new PflanzeHoellenkraut());
            Pflanzen.Add(new PflanzeHollbeere());
            Pflanzen.Add(new PflanzeHorusche());
            Pflanzen.Add(new PflanzeIlmenblatt());
            Pflanzen.Add(new PflanzeIribaarslilie());
            Pflanzen.Add(new PflanzeJagdgras());
            Pflanzen.Add(new PflanzeJoruga());
            Pflanzen.Add(new PflanzeKairan());
            Pflanzen.Add(new PflanzeKajubo());
            Pflanzen.Add(new PflanzeKhomMhanadiknolle());
            Pflanzen.Add(new PflanzeKlippenzahn());
            Pflanzen.Add(new PflanzeKukuka());
            Pflanzen.Add(new PflanzeLotusFaerber());
            Pflanzen.Add(new PflanzeLotusPurpurner());
            Pflanzen.Add(new PflanzeLotusSchwarzer());
            Pflanzen.Add(new PflanzeLotusGrauer());
            Pflanzen.Add(new PflanzeLotusWeisser());
            Pflanzen.Add(new PflanzeLotusWeissgelber());
            Pflanzen.Add(new PflanzeLulanie());
            Pflanzen.Add(new PflanzeMadabluete());
            Pflanzen.Add(new PflanzeMenchalKaktus());
            Pflanzen.Add(new PflanzeMerachStrauch());
            Pflanzen.Add(new PflanzeMessergras());
            Pflanzen.Add(new PflanzeMibelrohr());
            Pflanzen.Add(new PflanzeMirbelstein());
            Pflanzen.Add(new PflanzeMirhamerSeidenliane());
            Pflanzen.Add(new PflanzeMohnWeisser());
            Pflanzen.Add(new PflanzeMohnBunter());
            Pflanzen.Add(new PflanzeMohnGrauer());
            Pflanzen.Add(new PflanzeMohnPurpur());
            Pflanzen.Add(new PflanzeMohnSchwarzer());
            Pflanzen.Add(new PflanzeMohnTiger());
            Pflanzen.Add(new PflanzeMorgendornstrauch());
            Pflanzen.Add(new PflanzeNaftanstaude());
            Pflanzen.Add(new PflanzeNeckerkraut());
            Pflanzen.Add(new PflanzeNothilf());
            Pflanzen.Add(new PflanzeOlginwurz());
            Pflanzen.Add(new PflanzeOrazal());
            Pflanzen.Add(new PflanzeOrklandBovist());
            Pflanzen.Add(new PflanzePestsporenpilz());
            Pflanzen.Add(new PflanzePhosphorpilz());
            Pflanzen.Add(new PflanzeQuasselwurz());
            Pflanzen.Add(new PflanzeQuinja());
            Pflanzen.Add(new PflanzeRahjalieb());
            Pflanzen.Add(new PflanzeRattenpilz());
            Pflanzen.Add(new PflanzeRauschgurke());
            Pflanzen.Add(new PflanzeRotePfeilbluete());
            Pflanzen.Add(new PflanzeRoterDrachenschlund());
            Pflanzen.Add(new PflanzeSansaro());
            Pflanzen.Add(new PflanzeSatuariensbusch());
            Pflanzen.Add(new PflanzeSchlangenzuenglein());
            Pflanzen.Add(new PflanzeSchleichenderTod());
            Pflanzen.Add(new PflanzeSchleimigerSumpfknoeterich());
            Pflanzen.Add(new PflanzeSchlinggras());
            Pflanzen.Add(new PflanzeSchwarmschwamm());
            Pflanzen.Add(new PflanzeSchwarzerWein());
            Pflanzen.Add(new PflanzeShurinstrauch());
            Pflanzen.Add(new PflanzeTalaschin());
            Pflanzen.Add(new PflanzeTarnblatt());
            Pflanzen.Add(new PflanzeTarnele());
            Pflanzen.Add(new PflanzeThonnys());
            Pflanzen.Add(new PflanzeTraschbart());
            Pflanzen.Add(new PflanzeTrichterwurzel());
            Pflanzen.Add(new PflanzeTuurAmashKelch());
            Pflanzen.Add(new PflanzeUlmenwürger());
            Pflanzen.Add(new PflanzeVierblatt());
            Pflanzen.Add(new PflanzeVragieswurzel());
            Pflanzen.Add(new PflanzeWaldwebe());
            Pflanzen.Add(new PflanzeWasserrausch());
            Pflanzen.Add(new PflanzeWinselgras());
            Pflanzen.Add(new PflanzeWirselkraut());
            Pflanzen.Add(new PflanzeWuergedattel());
            Pflanzen.Add(new PflanzeYaganstrauch());
            Pflanzen.Add(new PflanzeZithabar());
            Pflanzen.Add(new PflanzeZunderschwamm());
            Pflanzen.Add(new PflanzeZwoelfblatt());
        }

        private void InitializeTiere()
        {
            //Tiere.Add(new TierAffeBoronsaeffchen());
            //Tiere.Add(new TierAffeBruellaffe());
            Tiere.Add(new TierAffeLoewenaffe());
            Tiere.Add(new TierAffeMoosaffe());
            Tiere.Add(new TierAffeOtanOtan());
            Tiere.Add(new TierAffePurzelaffe());
            Tiere.Add(new TierAffeRiesenaffe());
            Tiere.Add(new TierAffeSumpfranze());
            //Tiere.Add(new TierAffeTotenkopfaeffchen());
            //Tiere.Add(new TierAffeZirkusaffe());
            //Tiere.Add(new TierAffeZuckeraffe());
            Tiere.Add(new TierAntilopeAlKebirAntilope());
            Tiere.Add(new TierAntilopeGabelantilope());
            Tiere.Add(new TierAntilopeHalmarAntilope());
            Tiere.Add(new TierAntilopeKaren());
            Tiere.Add(new TierAntilopeSpringbock());
            Tiere.Add(new TierAntilopeVytaggaAntilope());
            Tiere.Add(new TierBaerBaumbaer());
            Tiere.Add(new TierBaerBaumwuerger());
            Tiere.Add(new TierBaerBorkenbaer());
            Tiere.Add(new TierBaerFirunsbaer());
            //Tiere.Add(new TierBaerGrimmbaer());
            Tiere.Add(new TierBaerHoehlenbaer());
            Tiere.Add(new TierBaerOrklandbaer());
            Tiere.Add(new TierBaerSchwarzbaer());
            Tiere.Add(new TierDachsSchneedachs());
            Tiere.Add(new TierDachsStreifendachs());
            //Tiere.Add(new TierDachsSuessmaul());
            Tiere.Add(new TierElefantBrabakerWaldelefant());
            Tiere.Add(new TierElefantMammut());
            Tiere.Add(new TierElefantMastodon());
            Tiere.Add(new TierElefantZwergelefant());
            Tiere.Add(new TierFuchsGelbfuchs());
            Tiere.Add(new TierFuchsRotfuchs());
            //Tiere.Add(new TierFuchsSilberfuchs());
            Tiere.Add(new TierFuchsBlaufuchs());
            Tiere.Add(new TierGefluegelAuerhahn());
            //Tiere.Add(new TierGefluegelEnte());
            Tiere.Add(new TierGefluegelFasan());
            Tiere.Add(new TierGefluegelRegenbogenfasan());
            Tiere.Add(new TierGefluegelRebhuhn());
            Tiere.Add(new TierGefluegelTrappe());
            //Tiere.Add(new TierGefluegelWachtel());
            Tiere.Add(new TierHaseKarnickel());
            Tiere.Add(new TierHaseOrklandkarnickel());
            Tiere.Add(new TierHasePfeifhase());
            Tiere.Add(new TierHaseRiesenloeffler());
            Tiere.Add(new TierHaseRotpueschel());
            Tiere.Add(new TierHaseSilberbock());
            Tiere.Add(new TierHirschElch());
            Tiere.Add(new TierHirschFirunshirsch());
            Tiere.Add(new TierHirschKronenhirsch());
            Tiere.Add(new TierHirschRehwild());
            Tiere.Add(new TierHundSteppenhund());
            Tiere.Add(new TierKatzeWildkatze());
            Tiere.Add(new TierKlippechse());
            Tiere.Add(new TierLoeweBergloewe());
            Tiere.Add(new TierLoeweSandloewe());
            Tiere.Add(new TierLoeweSchattenloewe());
            Tiere.Add(new TierLoeweWaldloewe());
            Tiere.Add(new TierLuchsFirnluchs());
            Tiere.Add(new TierLuchsGaenseluchs());
            Tiere.Add(new TierLuchsRaschtulsluchs());
            Tiere.Add(new TierLuchsRotluchs());
            Tiere.Add(new TierLuchsSonnenluchs());
            Tiere.Add(new TierPantherFleckenpanther());
            //Tiere.Add(new TierPantherJaguar());
            Tiere.Add(new TierPantherKhomgepard());
            Tiere.Add(new TierRinderAuerochse());
            Tiere.Add(new TierRinderFirnyak());
            Tiere.Add(new TierRinderOngalobulle());
            Tiere.Add(new TierRinderSteppenrind());
            Tiere.Add(new TierRobbeFelsrobbe());
            Tiere.Add(new TierRobbeMeerkalb());
            Tiere.Add(new TierRobbeSeetiger());
            Tiere.Add(new TierSaebelzahntigerDschungeltiger());
            Tiere.Add(new TierSaebelzahntigerSilberloewe());
            Tiere.Add(new TierSaebelzahntigerSteppentiger());
            Tiere.Add(new TierSchreckkatze());
            Tiere.Add(new TierSchweinMaraskanischesStachelschwein());
            Tiere.Add(new TierSchweinWildschwein());
            Tiere.Add(new TierSchweinWarzenschwein());
            Tiere.Add(new TierStrauss());
            Tiere.Add(new TierVielfrass());
            Tiere.Add(new TierVielfrassBaumschleimer());
            Tiere.Add(new TierZiegeGebirgsbock());
        }

        private void InitializeGebiete()
        {
            GebietListe = Global.ContextZooBot == null ? new List<Model.Gebiet>() : Global.ContextZooBot.ZooBotGebieteListe;
        }

        private void InitializePflanzen2()
        {
            PflanzenListe = Global.ContextZooBot == null ? new List<Model.Pflanze>() : Global.ContextZooBot.ZooBotPflanzenListe;
        }

        private void InitializeLandschaften2()
        {
            LandschaftenListe = Global.ContextZooBot == null ? new List<Model.Landschaft>() : Global.ContextZooBot.ZooBotLandschaftenListe;
        }

        #endregion
         
       
        #region Methode zur Talentprobe
        /// <summary>
        /// Führt eine Talentprobe mit einem angegebenen Talentwert gegen eine definierte Erschwernis durch und gibt das Ergebnis in einem Textfeld aus.
        /// </summary>        
        /// <param name="tapstern">gibt TaP* zurück</param>
        /// <param name="held">Held, der im Proben-Fenster angezeigt werden soll</param>
        /// <param name="ausgabe">Textausgabe für eine Textbox</param>
        /// <returns>true, wenn Probe gelungen</returns>
        private bool Talentprobe(Held_Talent probenTalent, int probenModifikator, out int taPStern, out string ausgabe) 
        {
            if (probenTalent != null)
            {
                probenTalent.Modifikator = probenModifikator;
                if (SelectedHeld == null && probenTalent.Werte[0] == 0) //Wenn kein Held, dann die eigenschaften mit 10 vorbesetzen
                {
                    for (int i = 0; i < 3; i++)
                        probenTalent.Werte[i] = 10;
                }
                var ergebnis = ShowProbeDialog(probenTalent, SelectedHeld);

                ausgabe = "Bei einem Zuschlag von " +
                    ((probenModifikator < 0) ? probenModifikator.ToString() : ("+" + probenModifikator)) +
                    " und einem TaW von " + probenTalent.TaW + " wurde " +
                    probenTalent.Ergebnis.Würfe[0] + " / " +
                    probenTalent.Ergebnis.Würfe[1] + " / " +
                    probenTalent.Ergebnis.Würfe[2] + " gewürfelt.\r\n";

                if (probenTalent.Ergebnis.Ergebnis == ErgebnisTyp.GLÜCKLICH || probenTalent.Ergebnis.Ergebnis == ErgebnisTyp.MEISTERHAFT)
                {
                    ausgabe += "Spektakulärer Erfolg! (Spezielle Erfahrung)\r\n";
                }
                if (probenTalent.Ergebnis.Ergebnis == ErgebnisTyp.PATZER || probenTalent.Ergebnis.Ergebnis == ErgebnisTyp.FATALER_PATZER)
                {
                    ausgabe += "Katastrophaler Misserfolg!\r\n";
                }
                taPStern = ergebnis.Übrig;
                return ergebnis.Gelungen;
            }
            else
            {
                taPStern = -1;
                ausgabe = "";
                return false;
            }
        }     
        
        #endregion

        private int _wertTaWTalent;
        public int WertTaWTalent
        {
            get { return _wertTaWTalent; }
            set
            {
                _wertTaWTalent = value;
                OnChanged("WertTaWTalent");
            }
        }

        #region Talente
        private Talent _talentKräuterSuchen;
        public Talent TalentKräuterSuchen
        {
            get { return _talentKräuterSuchen; }
            set
            {
                if (value == null)
                    value = Global.ContextHeld.LoadTalentByName("Kräuter Suchen");

                _talentKräuterSuchen = value;
                OnChanged("TalentKräuterSuchen");
                WertTaWTalent = value == null ? 0 : SelectedHeld == null ? 0 : SelectedHeld.Talentwert(value);
            }
        }
        
        private Talent _talentNahrungSuchen;
        public Talent TalentNahrungSuchen
        {
            get { return _talentNahrungSuchen; }
            set
            {
                if (value == null)
                    value = Nahrung_NutzeAckerbau?
                        Global.ContextHeld.LoadTalentByName("Nahrung Sammeln (Agrarland)") :
                        Global.ContextHeld.LoadTalentByName("Nahrung Sammeln (Wildnis)");

                _talentNahrungSuchen = value;
                OnChanged("TalentNahrungSuchen");
                WertTaWTalent = value == null ? 0 : SelectedHeld == null ? 0 : SelectedHeld.Talentwert(value);
            }
        }
        
        private Talent _talentPirschjagdSuchen;
        public Talent TalentPirschjagdSuchen
        {
            get { return _talentPirschjagdSuchen; }
            set
            {
                if (value == null)
                    value = Global.ContextHeld.LoadTalentByName("Pirschjagd (" + Zoo_Fernkampfwaffe + ")");

                _talentPirschjagdSuchen = value;
                OnChanged("TalentPirschjagdSuchen");
                WertTaWTalent = value == null ? 0 : SelectedHeld == null ? 0 : SelectedHeld.Talentwert(value);
            }
        }

        private Talent _talentAnsitzjagdSuchen;
        public Talent TalentAnsitzjagdSuchen
        {
            get { return _talentAnsitzjagdSuchen; }
            set
            {
                if (value == null)
                    value = Global.ContextHeld.LoadTalentByName("Ansitzjagd (" + Zoo_Fernkampfwaffe + ")");

                _talentAnsitzjagdSuchen = value;
                OnChanged("TalentAnsitzjagdSuchen");
                WertTaWTalent = value == null ? 0 : SelectedHeld == null ? 0 : SelectedHeld.Talentwert(value);
            }
        }
        

        #endregion 


        #region //---- FELDER ----
        

        #region //-Allgemein-
        //Booleans
        private bool IsLoaded;
        //Listen
        
        //Selections
        private Model.Held _selectedHeld;
        //Werte


        private ObservableCollection<string> _ListSpeziell = 
            new ObservableCollection<string>(){ "Keine Besonderheiten",
                                                "Astral durchzogener Ort",
                                                "Palakar (Schwarze Stadt)",
                                                "Ruine",
                                                "Stätte Namenloser Macht",
                                                "Nacht",
                                                "Vollmondnacht (+/- 1 Tag)"};
        public ObservableCollection<string> ListSpeziell
        {
            get { return _ListSpeziell; }
        }

        private ObservableCollection<string> _fernkampfWaffen =
            new ObservableCollection<string>(){ "Bogen",
                                                "Armbrust",
                                                "Blasrohr",
                                                "Diskus",
                                                "Schleuder",
                                                "Wurfbeile",
                                                "Wurfmesser",
                                                "Wurfspeere"};
        public ObservableCollection<string> fernkampfWaffen
        {
            get { return _fernkampfWaffen; }
        }

        private ObservableCollection<object> _monate = new ObservableCollection<object>();
        public ObservableCollection<object> monate
        {
            get { return _monate; }
            set { _monate = value; OnChanged(); }
        }                

        private ObservableCollection<string> _regionenNamen = new ObservableCollection<string>();
        public ObservableCollection<string> RegionenNamen
        { 
            get { return _regionenNamen; }
            set 
            {
                _regionenNamen = value;
                OnChanged();
            }
        }

        private ObservableCollection<string> _landschaftenNamen = new ObservableCollection<string>();
        public ObservableCollection<string> LandschaftenNamen
        {
            get { return _landschaftenNamen; }
            set
            {
                _landschaftenNamen = value;
                OnChanged();
            }
        }

        private ObservableCollection<string> _jagd_landschaftenNamen = new ObservableCollection<string>();
        public ObservableCollection<string> Jagd_LandschaftenNamen
        {
            get { return _jagd_landschaftenNamen; }
            set
            {
                _jagd_landschaftenNamen = value;
                OnChanged();
            }
        }

        private ObservableCollection<string> _pflanzenNamen = new ObservableCollection<string>();
        public ObservableCollection<string> PflanzenNamen
        {
            get { return _pflanzenNamen; }
            set
            {
                _pflanzenNamen = value;
                OnChanged();
            }
        }

        private ObservableCollection<string> _jagd_tierNamen = new ObservableCollection<string>();
        public ObservableCollection<string> Jagd_TierNamen
        {
            get { return _jagd_tierNamen; }
            set
            {
                _jagd_tierNamen = value;
                OnChanged();
            }
        }

        #endregion

        #endregion

        #region //---- EIGENSCHAFTEN ----

        #region //-Allgemein-

        //Listen
        public List<Model.Held> HeldListe
        {
            get { return Global.ContextHeld.LoadHeldenGruppeWithZauberzeichen(); }
        }

        //Selections
        public Model.Held SelectedHeld
        {
            get { return _selectedHeld; }
            set
            {
                _selectedHeld = value;
                OnChanged("SelectedHeld");
              //  WertRitualkenntnis = Global.ContextHeld.LoadMaxRitualkenntnisWertByHeld(value);
              //  TalentListeKreise = Global.ContextHeld.LoadZauberzeichenTalenteByHeld(value);                
            }
        }

        #region //-Werte-

        #region Einstellungen

        private string _kraeuter_Verbreitung = "";
        public string Kraeuter_Verbreitung
        {
            get { return _kraeuter_Verbreitung; }
            set
            {
                _kraeuter_Verbreitung = value;
                OnChanged();
            }
        }

        private string _kraeuter_Zuschlag = "";
        public string Kraeuter_Zuschlag
        {
            get { return _kraeuter_Zuschlag; }
            set
            {
                _kraeuter_Zuschlag = value;
                OnChanged();
            }
        }

        private string _nahrung_Zuschlag = "";
        public string Nahrung_Zuschlag
        {
            get { return _nahrung_Zuschlag; }
            set
            {
                _nahrung_Zuschlag = value;
                OnChanged();
            }
        }
        
        private string _jagd_Zuschlag = "";
        public string Jagd_Zuschlag
        {
            get { return _jagd_Zuschlag; }
            set
            {
                _jagd_Zuschlag = value;
                OnChanged();
            }
        }

        private string _fischen_Zuschlag = "";
        public string Fischen_Zuschlag
        {
            get { return _fischen_Zuschlag; }
            set
            {
                _fischen_Zuschlag = value;
                OnChanged();
            }
        }

        private string _kräuter_Ausgabe = "";
        public string Kräuter_Ausgabe
        {
            get { return _kräuter_Ausgabe; }
            set
            {
                _kräuter_Ausgabe = value;
                OnChanged();
            }
        }

        private string _nahrung_Ausgabe = "";
        public string Nahrung_Ausgabe
        {
            get { return _nahrung_Ausgabe; }
            set
            {
                _nahrung_Ausgabe = value;
                OnChanged();
            }
        }
        
        private string _jagd_Ausgabe = "";
        public string Jagd_Ausgabe
        {
            get { return _jagd_Ausgabe; }
            set
            {
                _jagd_Ausgabe = value;
                OnChanged();
            }
        }
        
        private string _fischen_Ausgabe = "";
        public string Fischen_Ausgabe
        {
            get { return _fischen_Ausgabe; }
            set
            {
                _fischen_Ausgabe = value;
                OnChanged();
            }
        }
        
        private string _speziellSelected = "Keine Besonderheiten";
        public string SpeziellSelected
        {
            get { return _speziellSelected; } 
            set
            {
                _speziellSelected = value;
                OnChanged();

                if (GebieteSelected != null)
                    GebieteSelected = GebieteSelected;
                if (Kräuter_LandschaftGebietSelected != null)
                    Kräuter_LandschaftGebietSelected = Kräuter_LandschaftGebietSelected;

                //Falls Landschaft bereits gewählt wurde, wird in Abhängigkeit davon die Pflanzenliste neu berechnet, andernfalls 
                //in Abhängigkeit der Region, so diese bereits gewählt wurde.
                if (Kräuter_LandschaftSelected != null)
                    Kräuter_Landschaft_SelectedIndexChanged();
                else if (RegionSelected != null)
                    Region_SelectedIndexChanged();

                Kraeuter_Zuschlag = "";

                //Astral durchzogener Ort und Palakar erfordern weiter Pflanzen in der Liste, was in der Neuberechnung der
                //Pflanzenliste Beachtung findet. Alle anderen Optionen haben nur Einfluss auf Bestimmung und/oder Grundmenge
            }
        }

        private int _monatAuswahlWert = 0;
        public int MonatAuswahlWert
        {
            get { return _monatAuswahlWert; }
            set
            {
                _monatAuswahlWert = value;
                OnChanged();
            }
        }

        private object _monatAuswahl = null;
        public object MonatAuswahl
        {
            get { return _monatAuswahl; }
            set
            {
                _monatAuswahl = value;
                if (SuchMonatSelected != value.ToString())
                    SuchMonatSelected = value.ToString();

                if (GebieteSelected != null)
                    GebieteSelected = GebieteSelected;
                
                OnChanged();
            }
        }

        private string _suchMonatSelected = "Komplettes Jahr";
        public string SuchMonatSelected
        { 
            get { return _suchMonatSelected; } 
            set
            {
                _suchMonatSelected = value;
                
                OnChanged();
                for (int i = 0; i < monate.Count; i++)
                {
                    if (monate[i].ToString() == value)
                    {
                        MonatAuswahl = monate[i];
                        MonatAuswahlWert = i;
                        break;
                    }
                }

                //Falls Landschaft bereits gewählt wurde, wird in Abhängigkeit davon die Pflanzenliste neu berechnet, andernfalls 
                //in Abhängigkeit der Region, so diese bereits gewählt wurde.
                if (Kräuter_LandschaftSelected != null)
                    Kräuter_Landschaft_SelectedIndexChanged();
                else if (RegionSelected != null)
                    Region_SelectedIndexChanged();

                Kraeuter_Zuschlag = "";
            }
        }

        #endregion

        #region // Heldwerte

        private bool _kräuter_HatGeländekunde = false;
        public bool Kräuter_HatGeländekunde
        {
            get { return _kräuter_HatGeländekunde; }
            set
            {
                _kräuter_HatGeländekunde = value;
                OnChanged();
                Pflanze_SelectedIndexChanged();
            }
        }

        private bool _nahrung_HatGeländekunde = false;
        public bool Nahrung_HatGeländekunde
        {
            get { return _nahrung_HatGeländekunde; }
            set
            {
                _nahrung_HatGeländekunde = value;
                OnChanged();
                Nahrung_SelectedIndexChanged();
            }
        }

        private bool _jagd_HatGeländekunde = false;
        public bool Jagd_HatGeländekunde
        {
            get { return _jagd_HatGeländekunde; }
            set
            {
                _jagd_HatGeländekunde = value;
                OnChanged();
                Tier_SelectedIndexChanged();
            }
        }

        private bool _fischen_HatGeländekunde = false;
        public bool Fischen_HatGeländekunde
        {
            get { return _fischen_HatGeländekunde; }
            set
            {
                _fischen_HatGeländekunde = value;
                OnChanged();
                Fischen_Region_SelectedIndexChanged();
            }
        }

        private bool _kräuter_HatOrtskenntnis = false;
        public bool Kräuter_HatOrtskenntnis
        {
            get { return _kräuter_HatOrtskenntnis; }
            set
            {
                _kräuter_HatOrtskenntnis = value;
                OnChanged();
                Pflanze_SelectedIndexChanged();
            }
        }

        private bool _nahrung_HatOrtskenntnis = false;
        public bool Nahrung_HatOrtskenntnis
        {
            get { return _nahrung_HatOrtskenntnis; }
            set
            {
                _nahrung_HatOrtskenntnis = value;
                OnChanged();
                Nahrung_SelectedIndexChanged();
            }
        }

        private bool _jagd_HatOrtskenntnis = false;
        public bool Jagd_HatOrtskenntnis
        {
            get { return _jagd_HatOrtskenntnis; }
            set
            {
                _jagd_HatOrtskenntnis = value;
                OnChanged();
                Tier_SelectedIndexChanged();
            }
        }

        private bool _fischen_HatOrtskenntnis = false;
        public bool Fischen_HatOrtskenntnis
        {
            get { return _fischen_HatOrtskenntnis; }
            set
            {
                _fischen_HatOrtskenntnis = value;
                OnChanged();
                Fischen_Region_SelectedIndexChanged();
            }
        }

        private bool _istScharfschütze = false;
        public bool IstScharfschütze
        {
            get { return _istScharfschütze; }
            set
            {
                _istScharfschütze = value;
                Tier_SelectedIndexChanged();
                OnChanged();
            }
        }

        private bool _istMeisterschütze = false;
        public bool IstMeisterschütze
        {
            get { return _istMeisterschütze; }
            set
            {
                _istMeisterschütze = value;
                Tier_SelectedIndexChanged();
                OnChanged();
            }
        }

        private int _zoo_MU = 10;
        public int Zoo_MU
        {
            get { return _zoo_MU; }
            set
            {
                _zoo_MU = value;
                OnChanged();
            }
        }
        private int _zoo_IN = 10;
        public int Zoo_IN
        {
            get { return _zoo_IN; }
            set
            {
                _zoo_IN = value;
                OnChanged();
            }
        }

        private int _zoo_KL = 10;
        public int Zoo_KL
        {
            get { return _zoo_KL; }
            set
            {
                _zoo_KL = value;
                OnChanged();
            }
        }

        private int _zoo_FF = 10;
        public int Zoo_FF
        {
            get { return _zoo_FF; }
            set
            {
                _zoo_FF = value;
                OnChanged();
            }
        }

        private int _zoo_GE = 10;
        public int Zoo_GE
        {
            get { return _zoo_GE; }
            set
            {
                _zoo_GE = value;
                OnChanged();
            }
        }

        private int _zoo_KK = 10;
        public int Zoo_KK
        {
            get { return _zoo_KK; }
            set
            {
                _zoo_KK = value;
                OnChanged();
            }
        }

        private decimal _zoo_TaWSinnesS = 0;
        public decimal Zoo_TaWSinnesS
        {
            get { return _zoo_TaWSinnesS; }
            set
            {
                _zoo_TaWSinnesS = value;
                BerechneTAWKraeutersuche();
                BerechneTAWNahrungsSammeln();
                OnChanged();
            }
        }

        private decimal _zoo_TaWWildnis = 0;
        public decimal Zoo_TaWWildnis
        {
            get { return _zoo_TaWWildnis; }
            set
            {
                _zoo_TaWWildnis = value;
                BerechneTAWKraeutersuche();
                BerechneTAWNahrungsSammeln();
                BerechneTAWJagd();
                OnChanged();
            }
        }

        private decimal _zoo_TaWTierkunde = 0;
        public decimal Zoo_TaWTierkunde
        {
            get { return _zoo_TaWTierkunde; }
            set
            {
                _zoo_TaWTierkunde = value; 
                BerechneTAWJagd();
                OnChanged();
            }
        }

        private decimal _zoo_TaWPflanzen = 0;
        public decimal Zoo_TaWPflanzen
        {
            get { return _zoo_TaWPflanzen; }
            set
            {
                _zoo_TaWPflanzen = value;
                BerechneTAWKraeutersuche();
                BerechneTAWNahrungsSammeln();
                OnChanged();
            }
        }

        private decimal _zoo_TaWAckerbau = 0;
        public decimal Zoo_TaWAckerbau
        {
            get { return _zoo_TaWAckerbau; }
            set
            {
                _zoo_TaWAckerbau = value;
                BerechneTAWNahrungsSammeln();
                OnChanged();
            }
        }

        private decimal _zoo_TaWFährtensuchen = 0;
        public decimal Zoo_TaWFährtensuchen
        {
            get { return _zoo_TaWFährtensuchen; }
            set
            {
                _zoo_TaWFährtensuchen = value;
                BerechneTAWJagd();
                OnChanged();
            }
        }

        private decimal _zoo_TaWFallenstellen = 0;
        public decimal Zoo_TaWFallenstellen
        {
            get { return _zoo_TaWFallenstellen; }
            set
            {
                _zoo_TaWFallenstellen = value;
                OnChanged();
            }
        }
        
        private decimal _zoo_TaWFischenAngeln = 0;
        public decimal Zoo_TaWFischenAngeln
        {
            get { return _zoo_TaWFischenAngeln; }
            set
            {
                _zoo_TaWFischenAngeln = value;
                OnChanged();
            }
        }
        
        private decimal _zoo_TaWSchleichen = 0;
        public decimal Zoo_TaWSchleichen
        {
            get { return _zoo_TaWSchleichen; }
            set
            {
                _zoo_TaWSchleichen = value; 
                BerechneTAWJagd();
                OnChanged();
            }
        }
        
        private decimal _zoo_TaWSichVerstecken = 0;
        public decimal Zoo_TaWSichVerstecken
        {
            get { return _zoo_TaWSichVerstecken; }
            set
            {
                _zoo_TaWSichVerstecken = value;
                BerechneTAWJagd();
                OnChanged();
            }
        }

        private decimal _zoo_TaWFernkampfwaffe = 0;
        public decimal Zoo_TaWFernkampfwaffe
        {
            get { return _zoo_TaWFernkampfwaffe; }
            set
            {
                _zoo_TaWFernkampfwaffe = value;
                BerechneTAWJagd();
                OnChanged();
            }
        }

        private decimal _zoo_TaWNahrung = 0;
        public decimal Zoo_TAWNahrung
        {
            get { return _zoo_TaWNahrung; }
            set
            {
                _zoo_TaWNahrung = value;
                OnChanged();
            }
        }

        private decimal _zoo_TaWKräuter = 0;
        public decimal Zoo_TaWKräuter
        {
            get { return _zoo_TaWKräuter; }
            set
            {
                _zoo_TaWKräuter = value;
                OnChanged();
            }
        }

        private decimal _zoo_TaWPirschjagd = 0;
        public decimal Zoo_TaWPirschjagd
        {
            get { return _zoo_TaWPirschjagd; }
            set
            {
                _zoo_TaWPirschjagd = value;
                OnChanged();
            }
        }

        private decimal _zoo_TaWAnsitzjagd = 0;
        public decimal Zoo_TaWAnsitzjagd
        {
            get { return _zoo_TaWAnsitzjagd; }
            set
            {
                _zoo_TaWAnsitzjagd = value;
                OnChanged();
            }
        }

        #endregion

        #region Berechnungen

        private void BerechneTAWKraeutersuche()
        {
            Zoo_TaWKräuter = Math.Round((Zoo_TaWSinnesS + Zoo_TaWWildnis + Zoo_TaWPflanzen) / 3, MidpointRounding.AwayFromZero);

            if (Zoo_TaWKräuter > 2 * Zoo_TaWSinnesS)
                Zoo_TaWKräuter = 2 * Zoo_TaWSinnesS;
            if (Zoo_TaWKräuter > 2 * Zoo_TaWWildnis)
                Zoo_TaWKräuter = 2 * Zoo_TaWWildnis;
            if (Zoo_TaWKräuter > 2 * Zoo_TaWPflanzen)
                Zoo_TaWKräuter = 2 * Zoo_TaWPflanzen;
        }
        
        private void BerechneTAWNahrungsSammeln()
        {
            if (Nahrung_NutzeAckerbau)
            {
                Zoo_TAWNahrung = Math.Round((Zoo_TaWSinnesS + Zoo_TaWAckerbau + Zoo_TaWPflanzen) / 3, MidpointRounding.AwayFromZero);

                if (Zoo_TAWNahrung > 2 * Zoo_TaWSinnesS)
                    Zoo_TAWNahrung = 2 * Zoo_TaWSinnesS;
                if (Zoo_TAWNahrung > 2 * Zoo_TaWAckerbau)
                    Zoo_TAWNahrung = 2 * Zoo_TaWAckerbau;
                if (Zoo_TAWNahrung > 2 * Zoo_TaWPflanzen)
                    Zoo_TAWNahrung = 2 * Zoo_TaWPflanzen;
            }
            else
            {
                Zoo_TAWNahrung = Math.Round((Zoo_TaWSinnesS + Zoo_TaWWildnis + Zoo_TaWPflanzen) / 3, MidpointRounding.AwayFromZero);

                if (Zoo_TAWNahrung > 2 * Zoo_TaWSinnesS)
                    Zoo_TAWNahrung = 2 * Zoo_TaWSinnesS;
                if (Zoo_TAWNahrung > 2 * Zoo_TaWWildnis)
                    Zoo_TAWNahrung = 2 * Zoo_TaWWildnis;
                if (Zoo_TAWNahrung > 2 * Zoo_TaWPflanzen)
                    Zoo_TAWNahrung = 2 * Zoo_TaWPflanzen;
            }
        }
        
        private void BerechneTAWJagd()
        {
            Zoo_TaWPirschjagd = Math.Round((Zoo_TaWWildnis + Zoo_TaWTierkunde + Zoo_TaWFährtensuchen + Zoo_TaWSchleichen + Zoo_TaWFernkampfwaffe) / 5, MidpointRounding.AwayFromZero);
            Zoo_TaWAnsitzjagd = Math.Round((Zoo_TaWWildnis + Zoo_TaWTierkunde + Zoo_TaWFährtensuchen + Zoo_TaWSichVerstecken + Zoo_TaWFernkampfwaffe) / 5, MidpointRounding.AwayFromZero);

            if (Zoo_TaWPirschjagd > 2 * Zoo_TaWWildnis)
                Zoo_TaWPirschjagd = 2 * Zoo_TaWWildnis;
            if (Zoo_TaWPirschjagd > 2 * Zoo_TaWTierkunde)
                Zoo_TaWPirschjagd = 2 * Zoo_TaWTierkunde;
            if (Zoo_TaWPirschjagd > 2 * Zoo_TaWFährtensuchen)
                Zoo_TaWPirschjagd = 2 * Zoo_TaWFährtensuchen;
            if (Zoo_TaWPirschjagd > 2 * Zoo_TaWSchleichen)
                Zoo_TaWPirschjagd = 2 * Zoo_TaWSchleichen;
            if (Zoo_TaWPirschjagd > 2 * Zoo_TaWFernkampfwaffe)
                Zoo_TaWPirschjagd = 2 * Zoo_TaWFernkampfwaffe;

            if (Zoo_TaWAnsitzjagd > 2 * Zoo_TaWWildnis)
                Zoo_TaWAnsitzjagd = 2 * Zoo_TaWWildnis;
            if (Zoo_TaWAnsitzjagd > 2 * Zoo_TaWTierkunde)
                Zoo_TaWAnsitzjagd = 2 * Zoo_TaWTierkunde;
            if (Zoo_TaWAnsitzjagd > 2 * Zoo_TaWFährtensuchen)
                Zoo_TaWAnsitzjagd = 2 * Zoo_TaWFährtensuchen;
            if (Zoo_TaWAnsitzjagd > 2 * Zoo_TaWSichVerstecken)
                Zoo_TaWAnsitzjagd = 2 * Zoo_TaWSichVerstecken;
            if (Zoo_TaWAnsitzjagd > 2 * Zoo_TaWFernkampfwaffe)
                Zoo_TaWAnsitzjagd = 2 * Zoo_TaWFernkampfwaffe;
        }

        #endregion

        #endregion





        //public TabItem TabItem { get { return _tabValue; } set { _tabValue = value; OnChanged("TabValue"); } }
        //public int WertRitualkenntnis
        //{
        //    get { return _wertRitualkenntnis; }
        //    set
        //    {
        //        _wertRitualkenntnis = value; OnChanged("WertRitualkenntnis");
        //        WertGrößeKreisKreise = Math.Round(((double)value / 2), MidpointRounding.AwayFromZero) + " Schritt";
        //        setWirkungsradiusZauberzeichen();
        //        setWirkungsradiusRunen();
        //        setWirkungsradiusKreise();
        //    }
        //}

        #endregion

        #endregion

        #region //---- KONSTRUKTOR ----

        public ZooBotViewModel()
            : base(View.General.ViewHelper.ShowProbeDialog)
        {
            //_onBauZauberzeichen = new Base.CommandBase(BauZauberzeichen, null);
            
            Init();
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Init()
        {
            if (IsLoaded == false)
            {
                SelectedHeld = null;
               
                InitializeGebiete();
                InitializePflanzen2();
                InitializeLandschaften2();

                InitializeRegionen();
                InitializeLandschaften();
                InitializePflanzen();
                InitializeTiere();

                //Listet alle initialisierten Regionen in den zugehörigen Auswahlboxen auf
                foreach (BasisRegion r in Regionen)
                {
                    if (r.Pflanzen.Count > 0)
                        RegionenNamen.Add(r.Name);
                }
                
                monate.Add("Komplettes Jahr");
                foreach (Monat item in Enum.GetValues(typeof(Monat)))
                    monate.Add(item);

                string suchmonat = (Datum.Aktuell.Monat == Monat.NamenloseTage ? "Namenlose Tage" : Datum.Aktuell.MonatString());
                SuchMonatSelected = suchmonat;

                HeldenPos = new Point(Global.HeldenLon, Global.HeldenLat); 
                IsLoaded = true;
            }
        }

        public void Refresh()
        {
            //Nur Helden mit entsprechender Sonderfertigkeit
            OnChanged("HeldListe");
            SetHeldWerte();
        }        

        #endregion

        #region //---- EVENTS ----

        private Held _zoo_HeldSelected = null;
        public Held Zoo_HeldSelected
        {
            get { return _zoo_HeldSelected; }
            set
            {
                _zoo_HeldSelected = value;
                OnChanged();
                SetHeldWerte();
                SelectedHeld = value;
                Kräuter_LandschaftSelected = Kräuter_LandschaftSelected;
                Nahrung_LandschaftSelected = Nahrung_LandschaftSelected;
                Fischen_LandschaftSelected = Fischen_LandschaftSelected;
            }
        }


        private List<Gebiet> _gebieteSelected = null;
        public List<Gebiet> GebieteSelected
        {
            get { return _gebieteSelected; }
            set
            {
                _gebieteSelected = value;

                PflanzeSelected = null;
                Kräuter_LandschaftGebietSelected = null;

                List<Pflanze> pList = new List<Pflanze>();
                List<Landschaft> pLandschaft = new List<Landschaft>();

                if (value == null) return;

                foreach (Gebiet g in value)
                {
                    if (g.Name == "ganz Aventurien")
                    {
                        pLandschaft = LandschaftenListe.OrderBy(t => t.Name).ToList();
                        pList = PflanzenListe.OrderBy(t => t.Name).ToList();
                        break;
                    }
                    else
                    {
                        foreach (var pGebiet in g.Pflanze)
                        {
                            if (!pList.Contains(pGebiet))
                            {
                                foreach (Pflanze_Ernte pErnte in pGebiet.Pflanze_Ernte)
                                {
                                    if (pGebiet.GetInErnte(MonatAuswahlWert)
                                    
                                          ||
                                          MonatAuswahlWert == 0
                                         && 
                                         !pList.Contains(pGebiet))
                                        pList.Add(pGebiet);
                                }
                            }
                            pGebiet.Landschaften.ForEach(delegate(Landschaft l)
                            {
                                if (!pLandschaft.Contains(l))
                                    pLandschaft.Add(l);
                            });
                        }
                    }
                }

                //Ausklammern von Blutblatt und Karain, falls kein astraler Ort gewählt
                //Ausklammern von Schwarzer Mohn, falls nicht Palakar gewählt
                if (!SpeziellSelected.Equals("Astral durchzogener Ort"))
                {
                    pList.Remove(pList.FirstOrDefault(t => t.Name.Equals("Kairan")));
                    pList.Remove(pList.FirstOrDefault(t => t.Name.Equals("Blutblatt")));
                }
                if (!SpeziellSelected.Equals("Palakar (Schwarze Stadt)"))
                    pList.Remove(pList.FirstOrDefault(t => t.Name.Equals("Schwarzer Mohn")));

                FilterPflanzenListe = pList;
                FilterPflanzenListeNachGebietsauswahl = pList;
                LandschaftGebietListe = pLandschaft.OrderBy(t => t.Name).ToList();
                OnChanged();
            }
        }
                
        private Landschaft _kräuter_LandschaftGebietSelected = null;
        public Landschaft Kräuter_LandschaftGebietSelected
        {
            get { return _kräuter_LandschaftGebietSelected; }
            set
            {                
                _kräuter_LandschaftGebietSelected = value;
                List<Pflanze> pList = new List<Pflanze>();// FilterPflanzenListeNachGebietsauswahl.ToList();
                FilterPflanzenListe = FilterPflanzenListeNachGebietsauswahl.ToList();

                if (value == null || !value.Name.Equals("überall"))
                {
                    FilterPflanzenListe.ForEach(delegate(Pflanze p)
                    {
                        if (p.Landschaften.Contains(value) && !pList.Contains(p))
                            pList.Add(p);
                        //if (!p.Landschaften.Contains(value))
                        //    pList.Remove(p);
                    });
                    
                }

                FilterPflanzenListe = pList.OrderBy(t => t.Name).ToList();
                OnChanged();
            }
        }
        

        private string _regionSelected = "";
        public string RegionSelected
        {
            get { return _regionSelected; }
            set
            {
                _regionSelected = value;
                OnChanged();                
                Region_SelectedIndexChanged();
                Nahrung_SelectedIndexChanged();
                Jagd_RegionSelectedIndexChanged();
                Fischen_Region_SelectedIndexChanged();
            }
        }

        private string _zoo_Fernkampfwaffe = "Bogen";
        public string Zoo_Fernkampfwaffe
        {
            get { return _zoo_Fernkampfwaffe; }
            set
            {
                _zoo_Fernkampfwaffe = value;
                SetHeldWerte();
                BerechneTAWJagd();
                OnChanged();            
            }
        }
                
        private bool _nahrung_NutzeAckerbau = false;
        public bool Nahrung_NutzeAckerbau
        {
            get { return _nahrung_NutzeAckerbau; }
            set
            {
                _nahrung_NutzeAckerbau = value;
                OnChanged();
            }
        }

        private bool _kräuter_HatSuchdauerVerdoppelt = false;
        public bool Kräuter_HatSuchdauerVerdoppelt
        {
            get { return _kräuter_HatSuchdauerVerdoppelt; }
            set
            {
                _kräuter_HatSuchdauerVerdoppelt = value;
                OnChanged();
            }
        }
        private bool _nahrung_HatSuchdauerVerdoppelt = false;
        public bool Nahrung_HatSuchdauerVerdoppelt
        {
            get { return _nahrung_HatSuchdauerVerdoppelt; }
            set
            {
                _nahrung_HatSuchdauerVerdoppelt = value;
                OnChanged();
            }
        }

        private bool _fischen_FallenInRuheAufstellen = false;
        public bool Fischen_FallenInRuheAufstellen
        {
            get { return _fischen_FallenInRuheAufstellen; }
            set
            {
                _fischen_FallenInRuheAufstellen = value;
                OnChanged();
            }
        }
        

        private string _kräuter_landschaftSelected = "";
        public string Kräuter_LandschaftSelected
        {
            get { return _kräuter_landschaftSelected; }
            set
            {
                _kräuter_landschaftSelected = value;
                OnChanged();
                Kräuter_Landschaft_SelectedIndexChanged();

                // Fügt die Geländekunde hinzu nach der ausgewählten Region
                Kräuter_HatGeländekunde = (value != null && value != "") ? SetGeländekunde(value) : false;
            }
        }

        private string _nahrung_landschaftSelected = "";
        public string Nahrung_LandschaftSelected
        {
            get { return _nahrung_landschaftSelected; }
            set
            {
                _nahrung_landschaftSelected = value;
                OnChanged();

                // Fügt die Geländekunde hinzu nach der ausgewählten Region
                Nahrung_HatGeländekunde = (value != null && value != "") ? SetGeländekunde(value) : false;
            }
        }

        private string _fischen_landschaftSelected = "";
        public string Fischen_LandschaftSelected
        {
            get { return _fischen_landschaftSelected; }
            set
            {
                _fischen_landschaftSelected = value;
                OnChanged();

                // Fügt die Geländekunde hinzu nach der ausgewählten Region
                Fischen_HatGeländekunde = (value != null && value != "") ? SetGeländekunde(value) : false;
            }
        }

        private string _jagd_landschaftSelected = "";
        public string Jagd_LandschaftSelected
        {
            get { return _jagd_landschaftSelected; }
            set
            {
                _jagd_landschaftSelected = value;
                OnChanged();
                Jagd_Landschaft_SelectedIndexChanged();
                // Fügr die Geländekunde hinzu nach der ausgewählten Region
                Jagd_HatGeländekunde = (value != null && value != "") ? SetGeländekunde(value) : false;
            }
        }
        //private string _pflanzeSelected = "";
        //public string PflanzeSelected
        //{
        //    get { return _pflanzeSelected; }
        //    set
        //    {
        //        _pflanzeSelected = value;
        //        OnChanged();
        //        Pflanze_SelectedIndexChanged();
        //    }
        //}

        private Pflanze _pflanzeSelected = null;
        public Pflanze PflanzeSelected
        {
            get { return _pflanzeSelected; }
            set
            {
                _pflanzeSelected = value;
                OnChanged();
                Pflanze_SelectedIndexChanged();
            }
        }
        
        private string _jagd_TierSelected = "";
        public string Jagd_TierSelected
        {
            get { return _jagd_TierSelected; }
            set
            {
                _jagd_TierSelected = value;
                OnChanged();
                Tier_SelectedIndexChanged();
            }
        }
        
        private void Region_SelectedIndexChanged()
        {
            //Setzt leere Strings in allen Boxen, da sonst NullPointExceptions
            Kräuter_LandschaftSelected = "";
            Nahrung_LandschaftSelected = "";
            Fischen_LandschaftSelected = "";
            LandschaftenNamen.Clear();
            PflanzeSelected = null;
            PflanzenNamen.Clear();
            
            //Sucht alle in der Region vorkommenden Landschaften und Pflanzen
            foreach (BasisRegion r in this.Regionen)
            {
                if (RegionSelected.Equals(r.Name))
                {
                    //Fügt alle in der Region gespeicherten Pflanzen hinzu
                    foreach (string s in r.Pflanzen)
                        PflanzenNamen.Add(s);
                    
                    //Fügte alle in der Region gespeicherten Landschaften hinzu
                    foreach (string s in r.Landschaften)
                        LandschaftenNamen.Add(s);
                                        
                    // alphabetische Sortierung der Landschaften                    
                    object[] SortedItemsL = new object[LandschaftenNamen.Count];
                    for (int i = 0; i < SortedItemsL.Length; i++)
                    {
                        SortedItemsL[i] = Kräuter_LandschaftSelected;
                    }
                    Array.Sort(SortedItemsL);
                    //this.Kraeuter_BoxLandschaft.Items.Clear();
                    //this.Kraeuter_BoxLandschaft.Items.AddRange(SortedItemsL);

                    break;
                }
            }

            //Entfernt Blutblatt und Karain, falls kein astraler Ort gewählt
            //Entfernt Schwarzer Mohn, falls nicht Palakar gewählt
            for (int i = PflanzenNamen.Count - 1; i >= 0; i--)
            {
                object o = PflanzenNamen[i];
                bool delete = true;

                if (o.ToString().Equals("Kairan") || o.ToString().Equals("Blutblatt"))
                {
                    if (SpeziellSelected.Equals("Astral durchzogener Ort"))
                        delete = false;
                    if (delete)
                        PflanzenNamen.RemoveAt(i);
                }

                if (o.ToString().Equals("Schwarzer Mohn"))
                {
                    if (SpeziellSelected.Equals("Palakar (Schwarze Stadt)"))
                        delete = false;
                    if (delete)
                        PflanzenNamen.RemoveAt(i);
                }
            }

            //Entfernt die nicht im Suchmonat erntebaren Pflanzen
            for (int i = PflanzenNamen.Count - 1; i >= 0; i--)
            {
                object o = PflanzenNamen[i];
                bool delete = true;

                foreach (BasisPflanze p in this.Pflanzen)
                {
                    if (o.ToString().Equals(p.Name))
                    {
                        if (SuchMonatSelected.Equals("Komplettes Jahr"))
                        {
                            delete = false;
                        }
                        else
                        {
                            foreach (string s in p.GetErntezeit(RegionSelected))
                            {
                                if (s.Equals(SuchMonatSelected))
                                    delete = false;
                            }
                            if (delete)
                                PflanzenNamen.RemoveAt(i);
                            break;
                        }
                    }
                }
            }

            Kraeuter_Zuschlag = "";

            //Sucht nach den in der aktuellen Region nicht findbaren Pflanzen
            //SucheRegionalNichtFindbarePflanzen();
            //SucheStrandPflanzenInRegion();
        }


        private bool SetGeländekunde(string landschaft)
        {
            if (SelectedHeld != null)
            {
                BasisLandschaft bl = (BasisLandschaft)Landschaften.ToArray().FirstOrDefault(t => ((BasisLandschaft)t).Name == landschaft);
                return SelectedHeld.HatSonderfertigkeit("Geländekunde (" + bl.Geländekundig + ")");
            }
            else
                return false;
        }
            
        #region Nahrungssuche - Auswahlboxen: Region
        private void Nahrung_SelectedIndexChanged()
        {
            //Bestimmung Boni
            int bonus = 0;
            if (Nahrung_HatGeländekunde)
                bonus += 3;
            if (Nahrung_HatOrtskenntnis)
                bonus += 7;

            //Sucht den in der Region vorhandenen Zuschlag auf das Sammeln von Nahrung
            int suchschwierigkeit = 0;
            foreach (BasisRegion r in Regionen)
            {
                if (RegionSelected.Equals(r.Name))
                {
                    suchschwierigkeit = r.EssbarePflanzen;
                    break;
                }
            }

            //Einrechnung von Boni
            int erschw = 0;
            if (suchschwierigkeit < 0)
                erschw = suchschwierigkeit;
            else
            {
                erschw = suchschwierigkeit - bonus;
                if (erschw < 0)
                    erschw = 0;
            }

            if (suchschwierigkeit < 100)
                Nahrung_Zuschlag = "+" + erschw.ToString();
            else
                Nahrung_Zuschlag = "---";

        }
        #endregion 


        private void Kräuter_Landschaft_SelectedIndexChanged()
        {
            PflanzeSelected = null;
            PflanzenNamen.Clear();

            //Sucht die in der Region vorkommenden Pflanzen
            foreach (BasisRegion r in this.Regionen)
            {
                if (RegionSelected.Equals(r.Name))
                {
                    foreach (string s in r.Pflanzen)
                    {
                        PflanzenNamen.Add(s);
                    }
                    break;
                }
            }

            //Entfernt die nicht in der gewählten Landschaft vorkommenden Pflanzen
            for (int i = PflanzenNamen.Count - 1; i >= 0; i--)
            {
                object o = PflanzenNamen[i];
                bool delete = true;

                foreach (BasisPflanze p in this.Pflanzen)
                {
                    if (o.ToString().Equals(p.Name))
                    {
                        foreach (VerbreitungsElementPflanzen v in p.GetVerbreitung(SuchMonatSelected))
                        {
                            if (v.Landschaft.Equals(Kräuter_LandschaftSelected))
                                delete = false;
                        }
                        if (delete)
                            PflanzenNamen.RemoveAt(i);
                        break;
                    }
                }
            }

            //Entfernt Blutblatt und Karain, falls kein astraler Ort gewählt
            //Entfernt Schwarzer Mohn, falls nicht Palakar gewählt
            for (int i = PflanzenNamen.Count - 1; i >= 0; i--)
            {
                object o = PflanzenNamen[i];
                bool delete = true;

                if (o.ToString().Equals("Kairan") || o.ToString().Equals("Blutblatt"))
                {
                    if (SpeziellSelected.Equals("Astral durchzogener Ort"))
                        delete = false;
                    if (delete)
                        PflanzenNamen.RemoveAt(i);
                }

                if (o.ToString().Equals("Schwarzer Mohn"))
                {
                    if (SpeziellSelected.Equals("Palakar (Schwarze Stadt)"))
                        delete = false;
                    if (delete)
                        PflanzenNamen.RemoveAt(i);
                }
            }

            //Entfernt die nicht im Suchmonat erntebaren Pflanzen
            for (int i = PflanzenNamen.Count - 1; i >= 0; i--)
            {
                object o = PflanzenNamen[i];
                bool delete = true;

                foreach (BasisPflanze p in this.Pflanzen)
                {
                    if (o.ToString().Equals(p.Name))
                    {
                        if (SuchMonatSelected.Equals("Komplettes Jahr"))
                        {
                            delete = false;
                        }
                        else
                        {
                            foreach (string s in p.GetErntezeit(RegionSelected))
                            {
                                if (s.Equals(SuchMonatSelected))
                                    delete = false;
                            }
                            if (delete)
                                PflanzenNamen.RemoveAt(i);
                            break;
                        }
                    }
                }
            }

            Kraeuter_Zuschlag = "";
        }

        private void Pflanze_SelectedIndexChanged()
        {
            bool doProbe = true;

            //Prüfung ob Bedingungen für Berechnung des Zuschlag gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (PflanzeSelected == null || Kräuter_LandschaftGebietSelected == null) //Kräuter_LandschaftSelected == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Boni
                int bonus = 0;
                if (Kräuter_HatGeländekunde)
                    bonus += 3;
                if (Kräuter_HatOrtskenntnis)
                    bonus += 7;

                //Ermittelung der Suchschwierigkeit
                string verbreitung = "unbekannt";

                int suchschwierigkeit = 
                    PflanzeSelected.Bestimmung +
                    (!Kräuter_LandschaftGebietSelected.Name.Equals("überall")?
                    PflanzeSelected.Pflanze_Verbreitung.FirstOrDefault(t => t.LandschaftGUID == Kräuter_LandschaftGebietSelected.LandschaftGUID).Verbreitung:
                    0);
                
                //Einrechnung von Boni
                int erschw = 0;
                if (suchschwierigkeit < 0)
                    erschw = suchschwierigkeit;
                else
                {
                    erschw = suchschwierigkeit - bonus;
                    if (erschw < 0)
                        erschw = 0;
                }

                string zuschlag = "";
                if (erschw < 0)
                    zuschlag = erschw.ToString();
                else
                    zuschlag = "+" + erschw.ToString();

                Kraeuter_Zuschlag = zuschlag;
                Kraeuter_Verbreitung = verbreitung;
            }
        }
        
        private void Speziell_SelectedIndexChanged()
        {
            //Falls Landschaft bereits gewählt wurde, wird in Abhängigkeit davon die Pflanzenliste neu berechnet, andernfalls 
            //in Abhängigkeit der Region, so diese bereits gewählt wurde.
            if (Kräuter_LandschaftSelected != null)
                Kräuter_Landschaft_SelectedIndexChanged();
            else if (RegionSelected != null)
                Region_SelectedIndexChanged();

            Kraeuter_Zuschlag = "";

            //Astral durchzogener Ort und Palakar erfordern weiter Pflanzen in der Liste, was in der Neuberechnung der
            //Pflanzenliste Beachtung findet. Alle anderen Optionen haben nur Einfluss auf Bestimmung und/oder Grundmenge
        }

        private void Tier_SelectedIndexChanged()
        {
            bool doProbe = true;

            //Prüfung ob Bedingungen für Berechnung des Zuschlag gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (Jagd_TierSelected == null || Jagd_LandschaftSelected == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Boni
                int bonus = 0;
                if (Jagd_HatGeländekunde)
                    bonus += 3;
                if (Jagd_HatOrtskenntnis)
                    bonus += 7;
                if (IstScharfschütze || IstMeisterschütze)
                    bonus += IstMeisterschütze? 7: 3;

                //Ermittelung der Suchschwierigkeit
                int suchschwierigkeit = 0;
                foreach (BasisTier t in this.Tiere)
                {
                    if (Jagd_TierSelected.Equals(t.Name))
                    {
                        suchschwierigkeit += t.GetJagdschwierigkeit();
                        foreach (BasisRegion r in this.Regionen)
                        {
                            if (RegionSelected.Equals(r.Name))
                            {
                                foreach (VerbreitungsElementTiere v in r.Tiere)
                                {
                                    if (v.Tier.Equals(t.Name))
                                    {
                                        suchschwierigkeit += v.Vorkommen;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }
                }

                //Einrechnung von Boni
                int erschw = 0;
                if (suchschwierigkeit < 0)
                    erschw = suchschwierigkeit;
                else
                {
                    erschw = suchschwierigkeit - bonus;
                    if (erschw < 0)
                        erschw = 0;
                }

                string zuschlag = "";
                if (erschw < 0)
                    zuschlag = erschw.ToString();
                else
                    zuschlag = "+" + erschw.ToString();

                Jagd_Zuschlag = zuschlag;
            }
        }

        #endregion

        #region Meta-Talente

        private Held_Talent ErstelleMetaTalent(string Talentname, int TaW)
        {
            Held_Talent hTalent = new Held_Talent();
            hTalent.TaW = TaW;
            hTalent.Talent = new Talent();
            hTalent.Talent.Probenname = Talentname;               
            hTalent.Talent.Eigenschaft1 = Global.ContextTalent.TalentListe.FirstOrDefault(t => t.Talentname == Talentname).Eigenschaft1;
            hTalent.Talent.Eigenschaft2 = Global.ContextTalent.TalentListe.FirstOrDefault(t => t.Talentname == Talentname).Eigenschaft2;
            hTalent.Talent.Eigenschaft3 = Global.ContextTalent.TalentListe.FirstOrDefault(t => t.Talentname == Talentname).Eigenschaft3;
            
            hTalent.Werte[0] = (hTalent.Talent.Eigenschaft1 == "MU") ? Zoo_MU :
                                (hTalent.Talent.Eigenschaft1 == "IN") ? Zoo_IN :
                                (hTalent.Talent.Eigenschaft1 == "GE") ? Zoo_GE :
                                (hTalent.Talent.Eigenschaft1 == "FF") ? Zoo_FF :
                                (hTalent.Talent.Eigenschaft1 == "KK") ? Zoo_KK :
                                (hTalent.Talent.Eigenschaft1 == "KL") ? Zoo_KL : 10;

            hTalent.Werte[1] = (hTalent.Talent.Eigenschaft2 == "MU") ? Zoo_MU :
                                (hTalent.Talent.Eigenschaft2 == "IN") ? Zoo_IN :
                                (hTalent.Talent.Eigenschaft2 == "GE") ? Zoo_GE :
                                (hTalent.Talent.Eigenschaft2 == "FF") ? Zoo_FF :
                                (hTalent.Talent.Eigenschaft2 == "KK") ? Zoo_KK :
                                (hTalent.Talent.Eigenschaft2 == "KL") ? Zoo_KL : 10;

            hTalent.Werte[2] = (hTalent.Talent.Eigenschaft3 == "MU") ? Zoo_MU :
                                (hTalent.Talent.Eigenschaft3 == "IN") ? Zoo_IN :
                                (hTalent.Talent.Eigenschaft3 == "GE") ? Zoo_GE :
                                (hTalent.Talent.Eigenschaft3 == "FF") ? Zoo_FF :
                                (hTalent.Talent.Eigenschaft3 == "KK") ? Zoo_KK :
                                (hTalent.Talent.Eigenschaft3 == "KL") ? Zoo_KL : 10;

            hTalent.Talent.Werte[0] = hTalent.Werte[0];
            hTalent.Talent.Werte[1] = hTalent.Werte[1];
            hTalent.Talent.Werte[2] = hTalent.Werte[2];

            return hTalent;
        }

        #endregion

        private Base.CommandBase _onGebieteVonPos = null;
        public Base.CommandBase OnGebieteVonPos
        {
            get
            {
                if (_onGebieteVonPos == null)
                    _onGebieteVonPos = new Base.CommandBase(GebieteVonPos, null);
                return _onGebieteVonPos;
            }
        }
        
        /// <summary>
        /// Button zum Bestimmen der Gebiete in der aktuellen Position
        /// </summary>
        void GebieteVonPos(object obj)
        {
            HeldenPos = new Point(Global.HeldenLon, Global.HeldenLat);

            List<Gebiet> gList = new List<Gebiet>();
            var gebiete = Global.ContextZooBot.GetGebiete(HeldenPos, 0.2);
            if (gebiete != null)
            {
                foreach (var g in gebiete)
                    gList.Add(g);
                GebieteSelected = gList;
            }
        }

        private Base.CommandBase _onBtnBekanntePflanzenForm = null;
        public Base.CommandBase OnBtnBekanntePflanzenForm
        {
            get
            {
                if (_onBtnBekanntePflanzenForm == null)
                    _onBtnBekanntePflanzenForm = new Base.CommandBase(BtnBekanntePflanzenForm, null);
                return _onBtnBekanntePflanzenForm;
            }
        }
        /// <summary>
        /// Button für gezielte Suche nach einer Pflanze
        /// </summary>
        void BtnBekanntePflanzenForm(object obj)
        {

            Zoo_HeldSelected = null;
        }

        #region Kräutersuche - Buttons: Gezielte Suche, Allgemeine Suche

        private Base.CommandBase _onBtnHeldAustragen = null;
        public Base.CommandBase OnBtnHeldAustragen
        {
            get
            {
                if (_onBtnHeldAustragen == null)
                    _onBtnHeldAustragen = new Base.CommandBase(BtnHeldAustragen, null);
                return _onBtnHeldAustragen;
            }
        }
        /// <summary>
        /// Button für gezielte Suche nach einer Pflanze
        /// </summary>
        void BtnHeldAustragen(object obj)
        {
            Zoo_HeldSelected = null;
        }

        private Base.CommandBase _onBtnGezielteSuche = null;
        public Base.CommandBase OnBtnGezielteSuche
        {
            get
            {
                if (_onBtnGezielteSuche == null)
                    _onBtnGezielteSuche = new Base.CommandBase(btnGezielteSuche, null);
                return _onBtnGezielteSuche;
            }
        }

        private int GetModBestimmung(Pflanze pflanze)
        {
            int mod = pflanze.Bestimmung;

            if (pflanze.Name.Equals("Efeuer") &&
                SpeziellSelected.Equals("Ruine"))
                return 0;
            else
            if (pflanze.Name.Equals("Feuerschlick") &&
                (MonatAuswahlWert == 2 ||       //Rondra
                 MonatAuswahlWert == 3) &&      //Efferd
                SpeziellSelected.Equals("Vollmondnacht (+/- 1 Tag)"))
                return -5;
            else
            if (pflanze.Name.Equals("Madablüte") &&
                SpeziellSelected.Equals("Vollmondnacht (+/- 1 Tag)"))
                return 5;
            else
            if (pflanze.Name.Equals("Rattenpilz") &&
                SpeziellSelected.Equals("Stätte Namenloser Macht"))
                return -7;
            else
            if (pflanze.Name.Equals("Winselgras") &&
                SpeziellSelected.Equals("Nacht") || SpeziellSelected.Equals("Vollmondnacht (+/- 1 Tag)"))
                return -2;
            else
            return mod;
        }

        /// <summary>
        /// Button für gezielte Suche nach einer Pflanze
        /// </summary>
        void btnGezielteSuche(object obj)
        {
            bool doProbe = true;
            string ausgabe = "";

            //Prüfung ob Bedingungen für Suche gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (PflanzeSelected == null || Kräuter_LandschaftGebietSelected == null) 
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Talentwert und Boni
                int taw = (int)Zoo_TaWKräuter;
                if (Kräuter_HatSuchdauerVerdoppelt)
                    taw = (int)Zoo_TaWKräuter + (int)Math.Round((double)Zoo_TaWKräuter / 2.0, MidpointRounding.AwayFromZero);

                int bonus = 0;
                if (Kräuter_HatGeländekunde)
                    bonus += 3;
                if (Kräuter_HatOrtskenntnis)
                    bonus += 7;

                // Modifikation bei speziellen Situationen
                int modBestimmung = GetModBestimmung(PflanzeSelected);

                //Ermittelung der Suchschwierigkeit

                int suchschwierigkeit = 
                    modBestimmung +
                    (!Kräuter_LandschaftGebietSelected.Name.Equals("überall") ?
                    PflanzeSelected.Pflanze_Verbreitung.FirstOrDefault(t => t.LandschaftGUID == Kräuter_LandschaftGebietSelected.LandschaftGUID).Verbreitung :
                    0);

                //Einrechnung von Boni
                int erschw = 0;
                if (suchschwierigkeit < 0)
                    erschw = suchschwierigkeit;
                else
                {
                    erschw = suchschwierigkeit - bonus;
                    if (erschw < 0)
                        erschw = 0;
                }

                //Talentprobe
                int tapstern = 0;
                bool result = Talentprobe(
                    ErstelleMetaTalent("Kräuter Suchen", taw), 
                    erschw, out tapstern, out ausgabe);

                //Ausgabe Ergebnis
                if (result)
                {
                    string grundmenge = "";
                    string referenz = "";
                    string gefahr = "";
                                        
                    Pflanze_Ernte pErnte = new Pflanze_Ernte();
                    pErnte = PflanzeSelected.Pflanze_Ernte.FirstOrDefault(t => t.PflanzeGUID == PflanzeSelected.PflanzeGUID);

                    grundmenge = pErnte.Grundmenge + " " + pErnte.Pflanzenteil;
                    referenz = PflanzeSelected.Literatur;
                    
                    ausgabe += (Kräuter_HatSuchdauerVerdoppelt) ?
                        "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 2 Stunden gedauert. \r\n\r\n":
                        "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1 Stunde gedauert. \r\n\r\n";

                    if (suchschwierigkeit <= 0)
                        suchschwierigkeit = 2;

                    int menge = 1;
                    while (tapstern >= Math.Round(suchschwierigkeit / 2.0, MidpointRounding.AwayFromZero))
                    {
                        menge++;
                        tapstern -= (int)Math.Round(suchschwierigkeit / 2.0, MidpointRounding.AwayFromZero);
                    }
                                        
                    ausgabe += !grundmenge.Equals("")?
                        "Von der Pflanze " + PflanzeSelected.Name + " wurde insgesamt " + menge + " mal die Grundmenge (" + grundmenge + ") gefunden. ":
                        "Die Pflanze " + PflanzeSelected.Name + " hat keine bekannten verwertbaren Pflanzenteile. ";

                    ausgabe += "\r\n\r\nFür detailliertere Informationen siehe \"Zoo-Botanica Aventurica\" Seite " + referenz + ".";

                    if (!gefahr.Equals(""))
                    {
                        ausgabe += "\r\n\r\nHinweis: ";
                        ausgabe += gefahr;
                    }
                }
                else
                {
                    ausgabe += "Die Probe ist leider misslungen.";
                }                
            }
            else
            {
                ausgabe = "Auswahl wurde nicht korrekt durchgeführt und eine Suche ist daher nicht möglich. Wurden Region, Landschaft und zu suchende Pflanze gewählt?";
            }
            Kräuter_Ausgabe = ausgabe;
        }

        private Base.CommandBase _onBtnAllgemeineSuche = null;
        public Base.CommandBase OnBtnAllgemeineSuche
        {
            get
            {
                if (_onBtnAllgemeineSuche == null)
                    _onBtnAllgemeineSuche = new Base.CommandBase(btnAllgemeineSuche, null);
                return _onBtnAllgemeineSuche;
            }
        }
        /// <summary>
        /// Button für gezielte Suche nach einer Pflanze
        /// </summary>
        void btnAllgemeineSuche(object obj)
        {
            bool doProbe = true;
            string ausgabe = "";

            //Prüfung ob Bedingungen für Suche gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (Kräuter_LandschaftGebietSelected == null) // Kräuter_LandschaftSelected == null)
                doProbe = false;

            if (doProbe)
            {
                //Ermittelung des Talentwertes
                int taw = (int)Zoo_TaWKräuter;
                if (Kräuter_HatSuchdauerVerdoppelt)
                    taw = (int)Zoo_TaWKräuter + (int)Math.Round((double)Zoo_TaWKräuter / 2.0, MidpointRounding.AwayFromZero);
                if (Kräuter_HatGeländekunde)
                    taw += 3;
                if (Kräuter_HatOrtskenntnis)
                    taw += 7;
                
                //Talentprobe
                int tapstern = 0;
                bool result = Talentprobe(ErstelleMetaTalent("Kräuter Suchen", taw), 0, out tapstern, out ausgabe);

                    
                if (result)
                {
                    ausgabe += (Kräuter_HatSuchdauerVerdoppelt) ?
                        "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 2 Stunden gedauert. \r\n\r\n":
                        "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1 Stunde gedauert. \r\n\r\n";

                    //Bestimmung Suchschwierigkeit für jede grundsätzlich findbare Pflanze und Vergleich mit TaP* ob Fund möglich
                    
                    ArrayList optionen = new ArrayList();  
                    foreach (Pflanze pflanze in FilterPflanzenListe) 
                    {                        
                        // Modifikation bei speziellen Situationen
                        int modBestimmung = GetModBestimmung(pflanze);

                        //Ermittelung der Suchschwierigkeit
                        int suchschwierigkeit = 
                            modBestimmung + 
                            (!Kräuter_LandschaftGebietSelected.Name.Equals("überall") ?
                            pflanze.Pflanze_Verbreitung.FirstOrDefault(t => t.LandschaftGUID == Kräuter_LandschaftGebietSelected.LandschaftGUID).Verbreitung :
                            0);
                        
                        if (Math.Round(tapstern / 2.0, MidpointRounding.AwayFromZero) >= suchschwierigkeit)
                            optionen.Add(pflanze);
                    }

                    //Falls mögliche Pflanzenfunde existieren, zufällige Auswahl und Ausgabe Ergebnis
                    if (optionen.Count > 0)
                    {
                        Random rand = new Random();
                        int fund = rand.Next(0, optionen.Count);
                                                
                        // Modifikation bei speziellen Situationen
                        int modBestimmung = GetModBestimmung(optionen[fund] as Pflanze);

                        //Ermittelung der Suchschwierigkeit
                        int suchschwierigkeit = 
                            modBestimmung + 
                            (!Kräuter_LandschaftGebietSelected.Name.Equals("überall") ?
                            (optionen[fund] as Pflanze).Pflanze_Verbreitung.FirstOrDefault(t => t.LandschaftGUID == Kräuter_LandschaftGebietSelected.LandschaftGUID).Verbreitung :
                            0);                        

                        if (suchschwierigkeit <= 0)
                            suchschwierigkeit = 2;

                        string grundmenge = "";
                        string referenz = "";
                        string gefahr = "";

                        Pflanze_Ernte pErnte = new Pflanze_Ernte();
                        pErnte = (optionen[fund] as Pflanze).Pflanze_Ernte.FirstOrDefault(t => t.PflanzeGUID == (optionen[fund] as Pflanze).PflanzeGUID);

                        int menge = 1;
                        if (pErnte != null)
                        {
                            grundmenge = pErnte.Grundmenge + " " + pErnte.Pflanzenteil;
                            referenz = (optionen[fund] as Pflanze).Literatur;

                            tapstern -= suchschwierigkeit;
                            while (tapstern >= suchschwierigkeit)
                            {
                                menge++;
                                tapstern -= suchschwierigkeit;
                            }
                        }

                        if (!grundmenge.Equals(""))
                            ausgabe += "Von der Pflanze " + (optionen[fund] as Pflanze).Name + " wurde insgesamt " + menge + " mal die Grundmenge (" + grundmenge + ") gefunden. ";
                        else
                            ausgabe += "Die Pflanze " + (optionen[fund] as Pflanze).Name + " hat keine bekannten verwertbaren Pflanzenteile. ";
                        ausgabe += "\r\n\r\nFür detailliertere Informationen siehe \"Zoo-Botanica Aventurica\" Seite " + referenz + ".";

                        if (!gefahr.Equals(""))
                        {
                            ausgabe += "\r\n\r\nHinweis: ";
                            ausgabe += gefahr;
                        }
                    }
                    else
                    {
                        ausgabe += "Obwohl die Probe gelungen ist, reicht es leider nicht aus um etwas brauchbares zu finden."; 
                    }
                }
                else
                {
                    ausgabe += "Die Probe ist leider misslungen.";                    
                }

                //Mögliche mit TaW angezeigen
                List<Pflanze> moeglich = new List<Pflanze>(); 
                foreach (Pflanze pflanze in FilterPflanzenListe)
                {
                    int modBestimmung = GetModBestimmung(pflanze);

                    int suchschwierigkeit =
                        modBestimmung +
                        (!Kräuter_LandschaftGebietSelected.Name.Equals("überall") ?
                        pflanze.Pflanze_Verbreitung.FirstOrDefault(t => t.LandschaftGUID == Kräuter_LandschaftGebietSelected.LandschaftGUID).Verbreitung :
                        0);

                    if (Math.Round((double)taw / 2.0, MidpointRounding.AwayFromZero) >= suchschwierigkeit)
                        moeglich.Add(pflanze);
                }
                if (moeglich.Count > 0)
                {
                    ausgabe += "\r\n\r\nMit dem TaW von " + taw + " und somit einen max Zuschlag der Pflanze von max. " + 
                        Math.Round((double)taw / 2.0, MidpointRounding.AwayFromZero) + 
                        ", können folgende Pflanzen gefunden werden: \r\n";
                    foreach (Pflanze p in moeglich.OrderBy(t => t.Name))
                        ausgabe += p.Name + " (" + p.Bestimmung + "), ";
                    ausgabe = ausgabe.Substring(0, ausgabe.Length - 2);
                }
            }
            else
            {
                ausgabe = "Auswahl wurde nicht korrekt durchgeführt und eine Suche ist daher nicht möglich. Wurde Region, Landschaft und zu suchende Pflanze gewählt?";
            }
            Kräuter_Ausgabe = ausgabe;
        }



        #region Nahrungssuche - Buttons: Nahrung sammeln
        private Base.CommandBase _onBtnNahrungssuche = null;
        public Base.CommandBase OnBtnNahrungssuche
        {
            get
            {
                if (_onBtnNahrungssuche == null)
                    _onBtnNahrungssuche = new Base.CommandBase(btnNahrungssuche, null);
                return _onBtnNahrungssuche;
            }
        }

        /// <summary>
        /// Implementiert die komplette Nahrungssuche
        /// </summary>
        void btnNahrungssuche(object obj)
        {
            bool doProbe = true;
            string ausgabe = "";
            //Prüfung ob Bedingungen für Suche gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (RegionSelected == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Talentwert und Boni
                int taw = (int)Zoo_TAWNahrung;
                if (Nahrung_HatSuchdauerVerdoppelt)
                    taw = (int)Zoo_TAWNahrung + (int)Math.Round((double)Zoo_TAWNahrung / 2.0, MidpointRounding.AwayFromZero);

                int bonus = 0;
                if (Nahrung_HatGeländekunde)
                    bonus += 3;
                if (Nahrung_HatOrtskenntnis)
                    bonus += 7;

                //Ermittelung der Suchschwierigkeit
                int suchschwierigkeit = 0;
                foreach (BasisRegion r in this.Regionen)
                {
                    if (RegionSelected.Equals(r.Name))
                    {
                        suchschwierigkeit = r.EssbarePflanzen;
                        break;
                    }
                }

                //Einrechnung von Boni
                int erschw = 0;
                if (suchschwierigkeit < 0)
                    erschw = suchschwierigkeit;
                else
                {
                    erschw = suchschwierigkeit - bonus;
                    if (erschw < 0)
                        erschw = 0;
                }

                if (suchschwierigkeit < 100)
                {
                    //Talentprobe
                    int tapstern = 0;
                    bool result = false;
                    if (Nahrung_HatOrtskenntnis)
                    {
                        result = true;
                        tapstern = taw - erschw;
                        if (tapstern < 1)
                            tapstern = 1;
                        ausgabe = "Es wurde keine Probe gewürfelt, da der Suchende über Ortskenntnis im betreffenden Gebiet verfügt.\r\n";
                    }
                    else
                    {
                        result = Talentprobe(
                            ErstelleMetaTalent(Nahrung_NutzeAckerbau ? "Nahrung Sammeln (Agrarland)" : "Nahrung Sammeln (Wildnis)", taw),
                            erschw, out tapstern, out ausgabe);
                    }
                    
                    //Ausgabe Ergebnis
                    if (result)
                    {
                        ausgabe += (Nahrung_HatSuchdauerVerdoppelt) ?
                            "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 2 Stunden gedauert. \r\n\r\n":
                            "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1 Stunde gedauert. \r\n\r\n";

                        int menge = 1;

                        while (tapstern >= 3)
                        {
                            menge++;
                            tapstern -= 3;
                        }

                        ausgabe += (menge > 1)?
                            "Es wurden insgesamt " + menge + " Tagesrationen an essbaren Pflanzen gefunden. ":
                            "Es wurde eine Tagesration an essbaren Pflanzen gefunden. ";

                        ausgabe += "\r\n\r\nFür detailliertere Informationen siehe \"Zoo-Botanica Aventurica\" Seite 224.";
                    }
                    else
                    {
                        ausgabe += "Die Probe ist leider misslungen.";
                    }
                }
                else
                {
                    ausgabe = "In dieser Region gibt es keine essbaren Pflanzen!";
                }
            }
            else
            {
                ausgabe = "Auswahl wurde nicht korrekt durchgeführt und eine Suche ist daher nicht möglich. Wurde die Region gewählt?";
            }
            Nahrung_Ausgabe = ausgabe;
        }
        #endregion

        #endregion
       
        #region Jagd - Auswahlboxen: Region, Landschaft, Tiere
        private void Jagd_RegionSelectedIndexChanged()
        {
            //Setzt leere Strings in allen Boxen, da sonst NullPointExceptions
            if (Jagd_LandschaftSelected != "")
                Jagd_LandschaftSelected = "";
            if (Jagd_LandschaftenNamen.Count != 0)
                Jagd_LandschaftenNamen.Clear();

            ObservableCollection<string> tiere = new ObservableCollection<string>();
            Jagd_TierSelected = "";
            
            //Sucht alle in der Region vorkommenden Landschaften und Tiere
            foreach (BasisRegion r in this.Regionen)
            {
                if (RegionSelected.Equals(r.Name))
                {
                    //Fügt alle in der Region gespeicherten Tiere hinzu
                    foreach (VerbreitungsElementTiere v in r.Tiere)
                    {
                        tiere.Add(v.Tier);
                    }

                    // alphabetische Sortierung der Tiere
                    object[] SortedItems = new object[tiere.Count];
                    for (int i = 0; i < SortedItems.Length; i++)
                    {
                        SortedItems[i] = tiere[i];
                    }
                    Array.Sort(SortedItems);
                    for (int i = 0; i < SortedItems.Length; i++)
                        tiere.Add(SortedItems[i].ToString());
                    Jagd_TierNamen = tiere;

                    //Fügt alle in den Tieren dieser Region gespeicherten Landschaften hinzu
                    foreach (VerbreitungsElementTiere v in r.Tiere)
                    {
                        foreach (BasisTier t in this.Tiere)
                        {
                            if (v.Tier.Equals(t.Name))
                            {
                                foreach (string l in t.GetVerbreitung())
                                {
                                    if (!Jagd_LandschaftenNamen.Contains(l))
                                        Jagd_LandschaftenNamen.Add(l);
                                }
                            }
                        }
                    }

                    // alphabetische Sortierung der Landschaften   
                    Jagd_LandschaftenNamen.ToList().Sort();
                    object[] SortedItemsL = new object[Jagd_LandschaftenNamen.Count];
                    for (int i = 0; i < SortedItemsL.Length; i++)
                    {
                        SortedItemsL[i] = Jagd_LandschaftenNamen[i];
                    }
                    Array.Sort(SortedItemsL);
                    Jagd_LandschaftenNamen.Clear();
                    for (int i = 0; i < SortedItemsL.Length; i++)
                        Jagd_LandschaftenNamen.Add(SortedItemsL[i].ToString());
                    //this.Jagd_BoxLandschaft.Items.AddRange(SortedItemsL);
                }
            }
            Jagd_Zuschlag = "";
        }

        private void Jagd_Landschaft_SelectedIndexChanged()
        {
            ObservableCollection<string> tiere = new ObservableCollection<string>();
            Jagd_TierSelected = "";

            // Sucht die in der Region vorkommenden Tiere
            foreach (BasisRegion r in this.Regionen)
            {
                if (RegionSelected.Equals(r.Name))
                {
                    foreach (VerbreitungsElementTiere v in r.Tiere)
                    {
                        tiere.Add(v.Tier);
                    }
                    break;
                }
            }

            //Entfernt die nicht in der gewählten Landschaft vorkommenden Tiere
            for (int i = tiere.Count - 1; i >= 0; i--)
            {
                object o = tiere[i];
                bool delete = true;

                foreach (BasisTier t in this.Tiere)
                {
                    if (o.ToString().Equals(t.Name))
                    {
                        foreach (string l in t.GetVerbreitung())
                        {
                            if (l.Equals(Jagd_LandschaftSelected))
                                delete = false;
                        }
                        if (delete)
                            tiere.RemoveAt(i);
                        break;
                    }
                }
            }

            // alphabetische Sortierung der Tiere
            object[] SortedItems = new object[tiere.Count];
            for (int i = 0; i < SortedItems.Length; i++)
            {
                SortedItems[i] = tiere[i];
            }
            Array.Sort(SortedItems);
            tiere.Clear();
            for (int i = 0; i < SortedItems.Length; i++)
                tiere.Add(SortedItems[i].ToString());
            Jagd_TierNamen = tiere;
            Jagd_Zuschlag = "";
        }

        private void Jagd_BoxTier_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool doProbe = true;

            //Prüfung ob Bedingungen für Berechnung des Zuschlag gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (Jagd_TierSelected == null || Jagd_LandschaftSelected == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Boni
                int bonus = 0;
                if (Jagd_HatGeländekunde)
                    bonus += 3;
                if (Jagd_HatOrtskenntnis)
                    bonus += 7;
                if (IstScharfschütze || IstMeisterschütze)
                    bonus += (IstMeisterschütze)? 7: 3;

                //Ermittelung der Suchschwierigkeit
                int suchschwierigkeit = 0;
                foreach (BasisTier t in this.Tiere)
                {
                    if (Jagd_TierSelected.Equals(t.Name))
                    {
                        suchschwierigkeit += t.GetJagdschwierigkeit();
                        foreach (BasisRegion r in this.Regionen)
                        {
                            if (RegionSelected.Equals(r.Name))
                            {
                                foreach (VerbreitungsElementTiere v in r.Tiere)
                                {
                                    if (v.Tier.Equals(t.Name))
                                    {
                                        suchschwierigkeit += v.Vorkommen;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }
                }

                //Einrechnung von Boni
                int erschw = 0;
                if (suchschwierigkeit < 0)
                    erschw = suchschwierigkeit;
                else
                {
                    erschw = suchschwierigkeit - bonus;
                    if (erschw < 0)
                        erschw = 0;
                }

                string zuschlag = "";
                if (erschw < 0)
                    zuschlag = erschw.ToString();
                else
                    zuschlag = "+" + erschw.ToString();

                Jagd_Zuschlag = zuschlag;
            }
        }
        #endregion

        #region Jagd - Buttons: Pirschjagd und Ansitzjagd

        private Base.CommandBase _onBtnPirschjagd = null;
        public Base.CommandBase OnBtnPirschjagd
        {
            get
            {
                if (_onBtnPirschjagd == null)
                    _onBtnPirschjagd = new Base.CommandBase(BtnPirschjagd, null);
                return _onBtnPirschjagd;
            }
        }
        /// <summary>
        /// Pirschjagd durchführen
        /// </summary>
        void BtnPirschjagd(object obj)
        {
            TalentPirschjagdSuchen = TalentPirschjagdSuchen;
            JagdAbwicklung(TalentPirschjagdSuchen, (int)Zoo_TaWPirschjagd, 1);
        }

        private Base.CommandBase _onBtnAnsitzjagd = null;
        public Base.CommandBase OnBtnAnsitzjagd
        {
            get
            {
                if (_onBtnAnsitzjagd == null)
                    _onBtnAnsitzjagd = new Base.CommandBase(BtnAnsitzjagd, null);
                return _onBtnAnsitzjagd;
            }
        }
        /// <summary>
        /// Ansitzjagd durchführen
        /// </summary>
        void BtnAnsitzjagd(object obj)
        {
            TalentAnsitzjagdSuchen = TalentAnsitzjagdSuchen;
            JagdAbwicklung(TalentAnsitzjagdSuchen, (int)Zoo_TaWAnsitzjagd, 1.5);
        }

        private void JagdAbwicklung(Talent JagdTalent, int Talentwert, double dauer)
        {
            bool doProbe = true;
            string ausgabe = "";

            //Prüfung ob Bedingungen für Jagd gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (Jagd_TierSelected == null || Jagd_LandschaftSelected == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Talentwert und Boni
                int taw = Talentwert;

                //Bestimmung Boni
                int bonus = 0;
                if (Jagd_HatGeländekunde)
                    bonus += 3;
                if (Jagd_HatOrtskenntnis)
                    bonus += 7;
                if (IstScharfschütze || IstMeisterschütze)
                    bonus += (IstMeisterschütze)? 7: 3;

                //Ermittelung der Jagdschwierigkeit
                int jagdschwierigkeit = 0;
                foreach (BasisTier t in this.Tiere)
                {
                    if (Jagd_TierSelected.Equals(t.Name))
                    {
                        jagdschwierigkeit += t.GetJagdschwierigkeit();
                        foreach (BasisRegion r in this.Regionen)
                        {
                            if (RegionSelected.Equals(r.Name))
                            {
                                foreach (VerbreitungsElementTiere v in r.Tiere)
                                {
                                    if (v.Tier.Equals(t.Name))
                                    {
                                        jagdschwierigkeit += v.Vorkommen;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }
                }

                //Einrechnung von Boni
                int erschw = 0;
                if (jagdschwierigkeit < 0)
                    erschw = jagdschwierigkeit;
                else
                {
                    erschw = jagdschwierigkeit - bonus;
                    if (erschw < 0)
                        erschw = 0;
                }
                
                //Talentprobe
                int tapstern = 0;
                bool result = Talentprobe(
                    ErstelleMetaTalent(JagdTalent.Name, taw), 
                    erschw, out tapstern, out ausgabe);

                //Ausgabe Ergebnis
                if (result)
                {
                    string beute = "";
                    string referenz = "";
                    string gefahr = "";
                    foreach (BasisTier t in this.Tiere)
                    {
                        if (t.Name.Equals(Jagd_TierSelected))
                        {
                            gefahr = t.GetGefahr();
                            beute = t.GetBeute();
                            referenz = t.SeiteZBA.ToString();
                            break;
                        }
                    }

                    ausgabe += (dauer == 1)?
                        "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1 Stunde gedauert. \r\n\r\n":
                        "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1.5 Stunden gedauert. \r\n\r\n";

                    if (jagdschwierigkeit <= 0)
                        jagdschwierigkeit = 2;

                    int menge = 1;
                    while (tapstern >= Math.Round(jagdschwierigkeit / 2.0, MidpointRounding.AwayFromZero))
                    {
                        menge++;
                        tapstern -= (int)Math.Round(jagdschwierigkeit / 2.0, MidpointRounding.AwayFromZero);
                    }

                    if (!beute.Equals(""))
                    {
                        ausgabe += (menge == 1)?
                            "Es wurde 1 Exemplar der Gattung " + Jagd_TierSelected + " erlegt. ":
                            "Es wurden " + menge + " Exemplare der Gattung " + Jagd_TierSelected + " erlegt. ";

                        ausgabe += "Jedes Exemplar liefert folgende Beuteteile: " + beute;
                    }
                    else
                        ausgabe += "Das Tier " + Jagd_TierSelected + " hat keine bekannten verwertbaren Beuteteile. ";

                    ausgabe += "\r\n\r\nFür detailliertere Informationen siehe \"Zoo-Botanica Aventurica\" Seite " + referenz + ".";

                    if (!gefahr.Equals(""))
                    {
                        ausgabe += "\r\n\r\nHinweis: ";
                        ausgabe += gefahr;
                    }
                }
                else
                {
                    ausgabe += "Die Probe ist leider misslungen.";
                }
            }
            else
            {
                ausgabe = "Auswahl wurde nicht korrekt durchgeführt und eine Jagd ist daher nicht möglich. Wurden Region, Landschaft und zu jagendes Tier gewählt?";
            }
            Jagd_Ausgabe = ausgabe;
        }
        
        #endregion
        
        #region Fischen - Auswahlboxen: Region
        private void Fischen_Region_SelectedIndexChanged()
        {
            //Bestimmung Boni
            int bonus = 0;
            if (Fischen_HatGeländekunde)
                bonus += 3;
            if (Fischen_HatOrtskenntnis)
                bonus += 7;

            //Sucht die selektierte Region
            int jagdschwierigkeit = 0;
            foreach (BasisRegion r in this.Regionen)
            {
                if (RegionSelected.Equals(r.Name))
                {
                    jagdschwierigkeit = r.Wildvorkommen + 5;
                    break;
                }
            }

            //Einrechnung von Boni
            int erschw = 0;
            if (jagdschwierigkeit < 0)
                erschw = jagdschwierigkeit;
            else
            {
                erschw = jagdschwierigkeit - bonus;
                if (erschw < 0)
                    erschw = 0;
            }

            if (jagdschwierigkeit < 100)
                Fischen_Zuschlag = "+" + erschw.ToString();
            else
                Fischen_Zuschlag = "---";

        }
        #endregion

        #region Fischen - Buttons: Fischen und Fallenstellen
        private Base.CommandBase _onBtnGewässerEinschätzen = null;
        public Base.CommandBase OnBtnGewässerEinschätzen
        {
            get
            {
                if (_onBtnGewässerEinschätzen == null)
                    _onBtnGewässerEinschätzen = new Base.CommandBase(btnGewässerEinschätzen, null);
                return _onBtnGewässerEinschätzen;
            }
        }
        /// <summary>
        /// Gewässer Einschätzen
        /// </summary>
        void btnGewässerEinschätzen(object obj)
        {
            bool doProbe = true;
            string ausgabe = "";

            //Prüfung ob Bedingungen für Suche gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (RegionSelected == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Talentwert und Boni
                int taw = (int)Zoo_TaWFischenAngeln;

                int bonus = 0;
                if (Fischen_HatGeländekunde)
                    bonus += 3;
                if (Fischen_HatOrtskenntnis)
                    bonus += 7;

                //Ermittelung der Suchschwierigkeit
                int suchschwierigkeit = 0;

                //Einrechnung von Boni
                int erschw = 0;
                if (suchschwierigkeit < 0)
                    erschw = suchschwierigkeit;
                else
                {
                    erschw = suchschwierigkeit - bonus;
                    if (erschw < 0)
                        erschw = 0;
                }
                
                // Gewaesser einschaetzen
                int tapstern = 0;
                bool result = Talentprobe(
                    ErstelleMetaTalent("Fischen/Angeln", taw), 
                    erschw, out tapstern, out ausgabe); 

                ausgabe += (result)?
                    "Die Probe um einzuschätzen ob es sich überhaupt lohnt hier die Angel auszuwerfen ist mit " + tapstern + " TaP* gelungen.\r\n\r\n":
                    "Die Probe um einzuschätzen ob es sich überhaupt lohnt hier die Angel auszuwerfen ist leider misslungen.\r\n\r\n";

                ausgabe += "Für detailliertere Informationen siehe \"Zoo-Botanica Aventurica\" Seite 60"; // und \"Wege des Schwertes\" Seite 26";
            }
            Fischen_Ausgabe = ausgabe;
        }

        private Base.CommandBase _onBtnAngelAuswerfen = null;
        public Base.CommandBase OnBtnAngelAuswerfen
        {
            get
            {
                if (_onBtnAngelAuswerfen == null)
                    _onBtnAngelAuswerfen = new Base.CommandBase(BtnAngelAuswerfen, null);
                return _onBtnAngelAuswerfen;
            }
        }
        /// <summary>
        /// Gewässer Angel Auswerfen
        /// </summary>
        void BtnAngelAuswerfen(object obj)
        {
            bool doProbe = true;
            string ausgabe = "";

            //Prüfung ob Bedingungen für Suche gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (RegionSelected == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Talentwert und Boni
                int taw = (int)Zoo_TaWFischenAngeln;

                int bonus = 0;
                if (Fischen_HatGeländekunde)
                    bonus += 3;
                if (Fischen_HatOrtskenntnis)
                    bonus += 7;

                //Ermittelung der Suchschwierigkeit
                int suchschwierigkeit = 0;

                //Einrechnung von Boni
                int erschw = 0;
                if (suchschwierigkeit < 0)
                    erschw = suchschwierigkeit;
                else
                {
                    erschw = suchschwierigkeit - bonus;
                    if (erschw < 0)
                        erschw = 0;
                }

                if (suchschwierigkeit < 100)
                {
                    //Talentprobe
                    int tapstern = 0;
                    bool result = Talentprobe(
                        ErstelleMetaTalent("Fischen/Angeln", taw), 
                        erschw, out tapstern, out ausgabe);

                    //Ausgabe Ergebnis
                    if (result)
                    {
                        ausgabe += "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1 Stunde gedauert. \r\n\r\n";

                        int menge = 1;

                        while (tapstern >= 3)
                        {
                            menge++;
                            tapstern -= 3;
                        }

                        ausgabe += (menge > 1)?
                            "Es wurden insgesamt " + menge + " halbe Tagesrationen Fisch gefangen. ":
                            "Es wurde eine halbe Tagesration Fisch gefangen. ";

                        ausgabe += "\r\n\r\nFür detailliertere Informationen siehe \"Zoo-Botanica Aventurica\" Seite 60"; // und \"Wege des Schwertes\" Seite 26";
                    }
                    else
                    {
                        ausgabe += "Die Probe ist leider misslungen.";
                    }
                }
                else
                {
                    ausgabe = "In dieser Region gibt es keinen Fisch!";
                }
            }
            else
            {
                ausgabe = "Auswahl wurde nicht korrekt durchgeführt und Fischen ist daher nicht möglich. Wurde die Region gewählt?";
            }
            Fischen_Ausgabe = ausgabe;
        }

        private Base.CommandBase _onBtnFallenAufstellen = null;
        public Base.CommandBase OnBtnFallenAufstellen
        {
            get
            {
                if (_onBtnFallenAufstellen == null)
                    _onBtnFallenAufstellen = new Base.CommandBase(BtnFallenAufstellen, null);
                return _onBtnFallenAufstellen;
            }
        }
        /// <summary>
        /// Gewässer Angel Auswerfen
        /// </summary>
        void BtnFallenAufstellen(object obj)
        {
            bool doProbe = true;
            string ausgabe = "";

            //Prüfung ob Bedingungen für Suche gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (RegionSelected == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Talentwert und Boni
                int taw = (int)Zoo_TaWFischenAngeln;
                if (Fischen_FallenInRuheAufstellen)
                    taw = (int)Zoo_TaWFischenAngeln + (int)Math.Round((double)Zoo_TaWFischenAngeln / 2.0, MidpointRounding.AwayFromZero);

                int bonus = 0;
                if (Fischen_HatGeländekunde)
                    bonus += 3;
                if (Fischen_HatOrtskenntnis)
                    bonus += 7;

                //Ermittelung der Suchschwierigkeit
                int jagdschwierigkeit = 0;
                foreach (BasisRegion r in this.Regionen)
                {
                    if (RegionSelected.Equals(r.Name))
                    {
                        jagdschwierigkeit = r.Wildvorkommen + 5;
                        break;
                    }
                }

                //Einrechnung von Boni
                int erschw = 0;
                if (jagdschwierigkeit < 0)
                    erschw = jagdschwierigkeit;
                else
                {
                    erschw = jagdschwierigkeit - bonus;
                    if (erschw < 0)
                        erschw = 0;
                }

                if (jagdschwierigkeit < 100)
                {
                    //Talentprobe
                    int tapstern = 0;
                    bool result = Talentprobe(
                        ErstelleMetaTalent("Fischen/Angeln", taw), 
                        erschw, out tapstern, out ausgabe);

                    //Ausgabe Ergebnis
                    if (result)
                    {
                        ausgabe += (Fischen_FallenInRuheAufstellen)?
                            "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1.5 Stunden gedauert. \r\n\r\n":
                            "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1 Stunde gedauert. \r\n\r\n";

                        int menge = 1;
                        while (tapstern >= Math.Round(jagdschwierigkeit / 2.0, MidpointRounding.AwayFromZero))
                        {
                            menge++;
                            tapstern -= (int)Math.Round(jagdschwierigkeit / 2.0, MidpointRounding.AwayFromZero);
                        }

                        ausgabe += (menge > 1)?
                            "Bei der Kontrolle der Fallen zeigt sich, dass insgesamt " + menge + " Tagesrationen Fleisch gefangen wurden. ":
                            "Bei der Kontrolle der Fallen zeigt sich, dass eine Tagesration Fleisch gefangen wurde. ";

                        ausgabe += "\r\n\r\nFür detailliertere Informationen siehe \"Zoo-Botanica Aventurica\" Seite 60.";
                    }
                    else
                        ausgabe += "Die Probe ist leider misslungen.";
                }
                else
                    ausgabe = "In dieser Region gibt es keine Tiere!";
            }
            else
                ausgabe = "Auswahl wurde nicht korrekt durchgeführt und Fallenstellen ist daher nicht möglich. Wurde die Region gewählt?";

            Fischen_Ausgabe = ausgabe;
        }
        #endregion


        public void SetHeldWerte(string talentname = "")
        {            
            Model.Held h = null;
            if (Zoo_HeldSelected != null)
                h = (Model.Held)Zoo_HeldSelected;
            else
                h = new Model.Held();

            string suchmonat = (Datum.Aktuell.Monat == Monat.NamenloseTage ? "Namenlose Tage" : Datum.Aktuell.MonatString());
            
            Zoo_MU = h.Mut;
            Zoo_IN = h.Intuition;
            Zoo_KL = h.Klugheit;
            Zoo_FF = h.Fingerfertigkeit;
            Zoo_GE = h.Gewandtheit;
            Zoo_KK = h.Körperkraft;
            Zoo_TaWAckerbau = h.Talentwert("Ackerbau");
            Zoo_TaWFährtensuchen = h.Talentwert("Fährtensuchen");
            Zoo_TaWFallenstellen = h.Talentwert("Fallenstellen");
            Zoo_TaWFischenAngeln = h.Talentwert("Fischen/Angeln");
            Zoo_TaWPflanzen = h.Talentwert("Pflanzenkunde");
            Zoo_TaWSchleichen = h.Talentwert("Schleichen");
            Zoo_TaWSichVerstecken = h.Talentwert("Sich Verstecken");
            Zoo_TaWSinnesS = h.Talentwert("Sinnenschärfe");
            Zoo_TaWTierkunde = h.Talentwert("Tierkunde");
            Zoo_TaWWildnis = h.Talentwert("Wildnisleben");
            Zoo_TaWFernkampfwaffe = h.Talentwert(Zoo_Fernkampfwaffe);
            IstScharfschütze = h.HatSonderfertigkeit(string.Format("Scharfschütze ({0})", Zoo_Fernkampfwaffe));
            IstMeisterschütze = h.HatSonderfertigkeit(string.Format("Meisterschütze ({0})", Zoo_Fernkampfwaffe));
        }
    }
}
