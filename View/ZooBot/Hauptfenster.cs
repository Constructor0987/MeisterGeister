using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
// Eigene Usings
using MeisterGeister.ViewModel.ZooBot.Logic.Landschaften;
using MeisterGeister.ViewModel.ZooBot.Logic.Pflanzen;
using MeisterGeister.ViewModel.ZooBot.Logic.Regionen;
using MeisterGeister.ViewModel.ZooBot.Logic.Tiere;

//TODO ??: Guraanstrauch bzw. Hesindigo nicht implementieren (Nur in DSA3 Regelwerk)
//TODO ??: Malomis nicht implementiert (Wächst nur in Gärten)
//TODO ??: Nemezijn nicht implementiert (Keine Spielwerte)

//TOOD ??: Karnickel als Tier "erfunden" (in vielen Regionen vorhanden)

namespace MeisterGeister.View.ZooBot
{
    public partial class Hauptfenster : UserControl
    {
        public ArrayList m_regionen = new ArrayList();
        public ArrayList m_landschaften = new ArrayList();
        public ArrayList m_pflanzen = new ArrayList();
        public ArrayList m_tiere = new ArrayList();

        public ArrayList Regionen
        {
            get { return this.m_regionen; }
        }

        public ArrayList Landschaften
        {
            get { return this.m_landschaften; }
        }

        public ArrayList Pflanzen
        {
            get { return this.m_pflanzen; }
        }

        public ArrayList Tiere
        {
            get { return this.m_tiere; }
        }

        public Hauptfenster()
        {
            InitializeRegionen();
            InitializeLandschaften();
            InitializePflanzen();
            InitializeTiere();
            InitializeComponent();

            //Sucht nach noch in keiner Region implementierten Pflanzen
            //SucheFehlendePflanzen();

            //Listet alle initialisierten Regionen in den zugehörigen Auswahlboxen auf
            foreach(BasisRegion r in this.Regionen)
            {   
                if(r.Pflanzen.Count > 0)
                    this.Kraeuter_BoxRegion.Items.Add(r.Name);
                if(r.EssbarePflanzen != (int)EVorkommen.KEINE)
                    this.Nahrung_BoxRegion.Items.Add(r.Name);
                if(r.Tiere.Count > 0)
                    this.Jagd_BoxRegion.Items.Add(r.Name);
                this.Fischen_BoxRegion.Items.Add(r.Name);
            }
        }

        #region MeisterGeisterInterface
        /// <summary>
        /// Nimmt alle Werte für Einsteins DSA Tool entgegen und setzt diese. Sollte einer der übergebenen Werte beim Setzen
        /// eine ArgumentOutOfRangeException provozieren, wird diese noch einmal geworfen.
        /// </summary>
        public void MeisterGeisterInterface(int MU, int IN, int KL, int FF, int GE, int KK,
            int Ackerbau, int Faehrtensuche, int Fallenstellen, int Fernkampfwaffe, int FischenAngeln,
            int Pflanzenkunde, int Schleichen, int SichVerstecken, int Sinnesschaerfe, int Tierkunde, int Wildnisleben,
            bool Scharfschuetze, bool Meisterschuetze, string Suchmonat, int AktiverReiter)
        {
            try
            {
                this.Kraeuter_MU.Value = MU;
                this.Kraeuter_IN.Value = IN;
                this.Fischen_KL.Value = KL;
                this.Kraeuter_FF.Value = FF;
                this.Jagd_GE.Value = GE;
                this.Fischen_KK.Value = KK;

                this.Nahrung_TAWAckerbau.Value = Ackerbau;
                this.Jagd_TAWFaehrtensuche.Value = Faehrtensuche;
                this.Fischen_TAWFallenstellen.Value = Fallenstellen;
                this.Jagd_TAWFernkampfwaffe.Value = Fernkampfwaffe;
                this.Fischen_TAWFischen.Value = FischenAngeln;
                this.Kraeuter_TAWPflanzen.Value = Pflanzenkunde;
                this.Jagd_TAWSchleichen.Value = Schleichen;
                this.Jagd_TAWSichVerstecken.Value = SichVerstecken;
                this.Kraeuter_TAWSinnes.Value = Sinnesschaerfe;
                this.Jagd_TAWTierkunde.Value = Tierkunde;
                this.Kraeuter_TAWWildnis.Value = Wildnisleben;

                this.Jagd_IstScharfschuetze.Checked = Scharfschuetze;
                this.Jagd_IstMeisterschuetze.Checked = Meisterschuetze;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }

            if (this.Kraeuter_BoxSuchmonat.Items.Contains(Suchmonat))
            {
                this.Kraeuter_BoxSuchmonat.Text = Suchmonat;
            }
            else
            {
                this.Kraeuter_BoxSuchmonat.Text = "Komplettes Jahr";
            }

            try
            {
                this.Registerkontrolle.SelectedIndex = AktiverReiter;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }

        }

        public int AktiverReiter
        {
            get { return this.Registerkontrolle.SelectedIndex; }
            set { this.Registerkontrolle.SelectedIndex = value; }
        }
        #endregion

        #region Methoden zum Testen
        /// <summary>
        /// Prüft ob eine Pflanze überhaupt in irgend einer Region vorkommt und gibt die nicht vorkommende auf der TextBox aus
        /// </summary>
        private void SucheFehlendePflanzen()
        {
            foreach (BasisRegion r in this.Regionen)
            {
                foreach (string s in r.Pflanzen)
                {
                    for(int k = 0; k < this.Pflanzen.Count; k++)
                    {
                        if (s.Equals((this.Pflanzen[k] as BasisPflanze).Name))
                        {
                            this.Pflanzen.RemoveAt(k);
                            break;
                        }
                    }
                }
            }

            foreach (BasisPflanze p in this.Pflanzen)
            {
                this.Kraeuter_TextfeldAusgabe.Text += p.Name + " wurde in keiner Region gefunden \r\n";
            }
        }

        /// <summary>
        /// Prüft welche Pflanzen in einer Region bei den möglichen Landschaften findbar sind und gibt die nicht findbaren auf der TextBox aus
        /// </summary>
        private void SucheRegionalNichtFindbarePflanzen()
        {
            this.Kraeuter_TextfeldAusgabe.Text = "";
            for(int a = this.Kraeuter_BoxPflanze.Items.Count-1; a >= 0; a--)
            {
                bool findbar = false;
                string s = this.Kraeuter_BoxPflanze.Items[a].ToString();
                foreach (BasisPflanze p in this.Pflanzen)
                {
                    if (s.Equals(p.Name))
                    {
                        foreach (BasisRegion r in this.Regionen)
                        {
                            if (this.Kraeuter_BoxRegion.SelectedItem.ToString().Equals(r.Name))
                            {
                                foreach (string st in r.Landschaften)
                                {
                                    foreach (VerbreitungsElementPflanzen v in p.GetVerbreitung(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString()))
                                    {
                                        if (st.Equals(v.Landschaft))
                                        {
                                            findbar = true;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }                    
                }
                if (!findbar)
                {
                    this.Kraeuter_TextfeldAusgabe.Text += this.Kraeuter_BoxPflanze.Items[a].ToString() + " Fund in den möglichen Landschaften nicht möglich! \r\n";
                    this.Kraeuter_TextfeldAusgabe.Text += "     Mögliche Landschaften wären ";
                    foreach (BasisPflanze p in this.Pflanzen)
                    {
                        if (p.Name.Equals(this.Kraeuter_BoxPflanze.Items[a].ToString()))
                        {
                            foreach (VerbreitungsElementPflanzen v in p.GetVerbreitung(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString()))
                            {
                                this.Kraeuter_TextfeldAusgabe.Text += v.Landschaft + ", ";
                            }
                            break;
                        }
                    }
                    this.Kraeuter_TextfeldAusgabe.Text += "\r\n";
                }
            }
        }

        /// <summary>
        /// Sucht Pflanzen in der Region, die die Landschaft Strand, Küste beinhalten
        /// </summary>
        private void SucheStrandPflanzenInRegion()
        {
            this.Kraeuter_TextfeldAusgabe.Text = "";
            for (int a = this.Kraeuter_BoxPflanze.Items.Count - 1; a >= 0; a--)
            {
                bool strand = false;
                string s = this.Kraeuter_BoxPflanze.Items[a].ToString();
                foreach (BasisPflanze p in this.Pflanzen)
                {
                    if (s.Equals(p.Name))
                    {
                        foreach (VerbreitungsElementPflanzen v in p.GetVerbreitung(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString()))
                        {
                            if (v.Landschaft.ToString().Equals("Küste, Strand"))
                                strand = true;
                        }
                        break;
                    }
                }
                    if (strand)
                    {
                        this.Kraeuter_TextfeldAusgabe.Text += this.Kraeuter_BoxPflanze.Items[a].ToString() + " hat Strand als Fundlandschaft! \r\n";
                        this.Kraeuter_TextfeldAusgabe.Text += "     Mögliche Landschaften wären ";
                        foreach (BasisPflanze p in this.Pflanzen)
                        {
                            if (p.Name.Equals(this.Kraeuter_BoxPflanze.Items[a].ToString()))
                            {
                                foreach (VerbreitungsElementPflanzen v in p.GetVerbreitung(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString()))
                                {
                                    this.Kraeuter_TextfeldAusgabe.Text += v.Landschaft + ", ";
                                }
                                break;
                            }
                        }
                        this.Kraeuter_TextfeldAusgabe.Text += "\r\n";
                    }
                
            }
        }

        /// <summary>
        /// Suche Tiere, die in keiner Region aufgeführt sind
        /// </summary>
        private void SucheTiereInKeinerRegion()
        {
            foreach (BasisRegion r in this.Regionen)
            {
                foreach (VerbreitungsElementTiere v in r.Tiere)
                {
                    for (int k = 0; k < this.Tiere.Count; k++)
                    {
                        if (v.Tier.Equals((this.Tiere[k] as BasisTier).Name))
                        {
                            this.Tiere.RemoveAt(k);
                            break;
                        }
                    }
                }
            }

            foreach (BasisTier t in this.Tiere)
            {
                this.Jagd_TextfeldAusgabe.Text += t.Name + " wurde in keiner Region gefunden \r\n";
            }
        }

        /// <summary>
        /// Suche Regionen, die Tiere beinhalten, die nicht existieren
        /// </summary>
        private void SucheNichtExistierendeTiere()
        {
            foreach (BasisRegion r in this.Regionen)
            {
                foreach (VerbreitungsElementTiere v in r.Tiere)
                {
                    bool found = false;
                    foreach (BasisTier t in this.Tiere)
                    {
                        if (t.Name.Equals(v.Tier))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        this.Jagd_TextfeldAusgabe.Text += v.Tier + " in Region " + r.Name + " ist nicht implementiert!\r\n";
                }
            }            

            object[] SortedItems = new object[this.Jagd_TextfeldAusgabe.Lines.Length];
            for (int i = 0; i < SortedItems.Length; i++)
            {
                SortedItems[i] = this.Jagd_TextfeldAusgabe.Lines[i];
            }
            Array.Sort(SortedItems);
            this.Jagd_TextfeldAusgabe.Clear();
            for (int i = 0; i < SortedItems.Length; i++)
            {
                this.Jagd_TextfeldAusgabe.Text += (SortedItems[i] as string)+"\r\n";
            }
        }
        #endregion

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
        #endregion

        #region Methode zur Talentprobe
        /// <summary>
        /// Führt eine Talentprobe mit einem angegebenen Talentwert gegen eine definierte Erschwernis durch und gibt das Ergebnis in einem Textfeld aus.
        /// </summary>
        /// <param name="eig1">Erste Eigenschaft</param>
        /// <param name="eig2">Zweite Eigenschaft</param>
        /// <param name="eig3">Dritte Eigenschaft</param>
        /// <param name="taw">Talentwert</param>
        /// <param name="erschw">Probenerschwernis</param>
        /// <param name="tapstern">gibt TaP* zurück</param>
        /// <param name="box">TextBox in welcher Ausgabe erfolgen soll</param>
        /// <returns>true, wenn Probe gelungen</returns>
        private bool Talentprobe(int eig1, int eig2, int eig3, int taw, int erschw, out int tapstern, TextBox box)
        {
            Random rand = new Random();
            int[] wuerfel = new int[] { rand.Next(1, 21), rand.Next(1, 21), rand.Next(1, 21) };
            if (About_Manuell.Checked)
            {
                Wuerfeleingabe dialog = new Wuerfeleingabe();
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    wuerfel[0] = dialog.Eigenschaft1;
                    wuerfel[1] = dialog.Eigenschaft2;
                    wuerfel[2] = dialog.Eigenschaft3;
                }
            }            

            string zuschlag = "";
            if (erschw < 0)
                zuschlag = erschw.ToString();
            else
                zuschlag = "+" + erschw.ToString();

            box.Text = "Bei einem Zuschlag von " + zuschlag + " und einem TaW von " + taw + " wurde " + wuerfel[0] + " / " + wuerfel[1] + " / " + wuerfel[2] + " gewürfelt.\r\n";

            taw = taw - erschw;

            if ((wuerfel[0] == 1 && wuerfel[1] == 1) || (wuerfel[1] == 1 && wuerfel[2] == 1) || (wuerfel[0] == 1 && wuerfel[2] == 1))
            {
                if (taw <= 0)
                    tapstern = 1;
                else
                    tapstern = taw;

                box.Text += "Spektakulärer Erfolg! (Spezielle Erfahrung)\r\n";

                return true;
            }
            if ((wuerfel[0] == 20 && wuerfel[1] == 20) || (wuerfel[1] == 20 && wuerfel[2] == 20) || (wuerfel[0] == 20 && wuerfel[2] == 20))
            {
                tapstern = -1;

                box.Text += "Katastrophaler Misserfolg! (Spezielle Erfahrung)\r\n";

                return false;
            }

            if (taw >= 0)
            {
                if (wuerfel[0] > eig1)
                    taw = taw - (wuerfel[0] - eig1);
                if (wuerfel[1] > eig2)
                    taw = taw - (wuerfel[1] - eig2);
                if (wuerfel[2] > eig3)
                    taw = taw - (wuerfel[2] - eig3);
            }
            else
            {
                if (wuerfel[0] <= eig1 + taw)
                    if (wuerfel[1] <= eig2 + taw)
                        if (wuerfel[2] <= eig3 + taw)
                            taw = 0;
            }

            if (taw >= 0)
            {
                if (taw == 0)
                    tapstern = 1;
                else
                    tapstern = taw;

                return true;
            }
            else
            {
                tapstern = -1;
                return false;
            }
        }
        #endregion

        #region Kräutersuche - Eigenschaften, Talente, Checkboxen
        private void Kraeuter_MU_ValueChanged(object sender, EventArgs e)
        {
            if (this.About_Kopplung.Checked)
            {
                this.Nahrung_MU.Value = this.Kraeuter_MU.Value;
                this.Jagd_MU.Value = this.Kraeuter_MU.Value;
            }
        }

        private void Kraeuter_IN_ValueChanged(object sender, EventArgs e)
        {
            if (this.About_Kopplung.Checked)
            {
                this.Nahrung_IN.Value = this.Kraeuter_IN.Value;
                this.Jagd_IN.Value = this.Kraeuter_IN.Value;
                this.Fischen_IN.Value = this.Kraeuter_IN.Value;
            }
        }

        private void Kraeuter_FF_ValueChanged(object sender, EventArgs e)
        {
            if (this.About_Kopplung.Checked)
            {
                this.Nahrung_FF.Value = this.Kraeuter_FF.Value;
                this.Fischen_FF.Value = this.Kraeuter_FF.Value;
            }
        }

        private void Kraeuter_TAWKraeuter_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Kraeuter_TAWSinnes_ValueChanged(object sender, EventArgs e)
        {
            this.BerechneTAWKraeutersuche();
            if (this.About_Kopplung.Checked)
                this.Nahrung_TAWSinnes.Value = this.Kraeuter_TAWSinnes.Value;
        }

        private void Kraeuter_TAWWildnis_ValueChanged(object sender, EventArgs e)
        {
            this.BerechneTAWKraeutersuche();
            if (this.About_Kopplung.Checked)
            {
                this.Nahrung_TAWWildnis.Value = this.Kraeuter_TAWWildnis.Value;
                this.Jagd_TAWWildnisleben.Value = this.Kraeuter_TAWWildnis.Value;
            }
        }

        private void Kraeuter_TAWPflanzen_ValueChanged(object sender, EventArgs e)
        {
            this.BerechneTAWKraeutersuche();
            if (this.About_Kopplung.Checked)
                this.Nahrung_TAWPflanzen.Value = this.Kraeuter_TAWPflanzen.Value;
        }

        private void BerechneTAWKraeutersuche()
        {
            this.Kraeuter_TAWKraeuter.Value = Math.Round((this.Kraeuter_TAWSinnes.Value + this.Kraeuter_TAWWildnis.Value + this.Kraeuter_TAWPflanzen.Value) / 3, MidpointRounding.AwayFromZero);

            if (this.Kraeuter_TAWKraeuter.Value > 2 * this.Kraeuter_TAWSinnes.Value)
                this.Kraeuter_TAWKraeuter.Value = 2 * this.Kraeuter_TAWSinnes.Value;
            if (this.Kraeuter_TAWKraeuter.Value > 2 * this.Kraeuter_TAWWildnis.Value)
                this.Kraeuter_TAWKraeuter.Value = 2 * this.Kraeuter_TAWWildnis.Value;
            if (this.Kraeuter_TAWKraeuter.Value > 2 * this.Kraeuter_TAWPflanzen.Value)
                this.Kraeuter_TAWKraeuter.Value = 2 * this.Kraeuter_TAWPflanzen.Value;
        }

        private void Kraeuter_HatSuchdauerVerdoppelt_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Kraeuter_HatOrtskenntnis_CheckedChanged(object sender, EventArgs e)
        {
            this.Kraeuter_BoxPflanze_SelectedIndexChanged(null, null);
        }

        private void Kraeuter_HatGelaendekunde_CheckedChanged(object sender, EventArgs e)
        {
            this.Kraeuter_BoxPflanze_SelectedIndexChanged(null, null);
        }
        #endregion

        #region Kräutersuche - Auswahlboxen: Suchmonat, Speziell, Region, Landschaft, Pflanzen
        private void Kraeuter_BoxRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Setzt leere Strings in allen Boxen, da sonst NullPointExceptions
            this.Kraeuter_BoxLandschaft.Text = "";
            this.Kraeuter_BoxLandschaft.Items.Clear();
            this.Kraeuter_BoxPflanze.Text = "";
            this.Kraeuter_BoxPflanze.Items.Clear();

            //Sucht alle in der Region vorkommenden Landschaften und Pflanzen
            foreach(BasisRegion r in this.Regionen)
            {
                if (this.Kraeuter_BoxRegion.SelectedItem.ToString().Equals(r.Name))
                {
                    //Fügt alle in der Region gespeicherten Pflanzen hinzu
                    foreach (string s in r.Pflanzen)
                    {
                        this.Kraeuter_BoxPflanze.Items.Add(s);
                    }

                    //Fügte alle in der Region gespeicherten Landschaften hinzu
                    foreach (string s in r.Landschaften)
                    {
                        this.Kraeuter_BoxLandschaft.Items.Add(s);
                    }

                    // alphabetische Sortierung der Landschaften                    
                    object[] SortedItemsL = new object[this.Kraeuter_BoxLandschaft.Items.Count];
                    for (int i = 0; i < SortedItemsL.Length; i++)
                    {
                        SortedItemsL[i] = this.Kraeuter_BoxLandschaft.Items[i];
                    }
                    Array.Sort(SortedItemsL);
                    this.Kraeuter_BoxLandschaft.Items.Clear();
                    this.Kraeuter_BoxLandschaft.Items.AddRange(SortedItemsL);

                    break;
                }
            }

            //Entfernt Blutblatt und Karain, falls kein astraler Ort gewählt
            //Entfernt Schwarzer Mohn, falls nicht Palakar gewählt
            for (int i = this.Kraeuter_BoxPflanze.Items.Count - 1; i >= 0; i--)
            {
                object o = this.Kraeuter_BoxPflanze.Items[i];
                bool delete = true;

                if (o.ToString().Equals("Kairan") || o.ToString().Equals("Blutblatt"))
                {
                    if (this.Kraeuter_BoxBesonderheiten.SelectedItem.ToString().Equals("Astral durchzogener Ort"))
                        delete = false;
                    if (delete)
                        this.Kraeuter_BoxPflanze.Items.RemoveAt(i);
                }

                if (o.ToString().Equals("Schwarzer Mohn"))
                {
                    if (this.Kraeuter_BoxBesonderheiten.SelectedItem.ToString().Equals("Palakar (Schwarze Stadt)"))
                        delete = false;
                    if (delete)
                        this.Kraeuter_BoxPflanze.Items.RemoveAt(i);
                }
            }

            //Entfernt die nicht im Suchmonat erntebaren Pflanzen
            for (int i = this.Kraeuter_BoxPflanze.Items.Count - 1; i >= 0; i--)
            {
                object o = this.Kraeuter_BoxPflanze.Items[i];
                bool delete = true;

                foreach (BasisPflanze p in this.Pflanzen)
                {
                    if (o.ToString().Equals(p.Name))
                    {
                        if (this.Kraeuter_BoxSuchmonat.SelectedItem.ToString().Equals("Komplettes Jahr"))
                        {
                            delete = false;
                        }
                        else
                        {
                            foreach (string s in p.GetErntezeit(this.Kraeuter_BoxRegion.SelectedItem.ToString()))
                            {
                                if (s.Equals(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString()))
                                    delete = false;
                            }
                            if (delete)
                                this.Kraeuter_BoxPflanze.Items.RemoveAt(i);
                            break;
                        }
                    }
                }
            }

            this.Kraeuter_Zuschlag.Text = "";

            //Sucht nach den in der aktuellen Region nicht findbaren Pflanzen
            //SucheRegionalNichtFindbarePflanzen();
            //SucheStrandPflanzenInRegion();
        }

        private void Kraeuter_BoxLandschaft_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Kraeuter_BoxPflanze.Text = "";
            this.Kraeuter_BoxPflanze.Items.Clear();

            //Sucht die in der Region vorkommenden Pflanzen
            foreach (BasisRegion r in this.Regionen)
            {
                if (this.Kraeuter_BoxRegion.SelectedItem.ToString().Equals(r.Name))
                {
                    foreach (string s in r.Pflanzen)
                    {
                        this.Kraeuter_BoxPflanze.Items.Add(s);
                    }
                    break;
                }
            }

            //Entfernt die nicht in der gewählten Landschaft vorkommenden Pflanzen
            for(int i = this.Kraeuter_BoxPflanze.Items.Count - 1; i >= 0; i--)
            {
                object o = this.Kraeuter_BoxPflanze.Items[i];
                bool delete = true;

                foreach (BasisPflanze p in this.Pflanzen)
                {
                    if (o.ToString().Equals(p.Name))
                    {
                        foreach (VerbreitungsElementPflanzen v in p.GetVerbreitung(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString()))
                        {
                            if (v.Landschaft.Equals(this.Kraeuter_BoxLandschaft.SelectedItem.ToString()))
                                delete = false;
                        }
                        if (delete)
                            this.Kraeuter_BoxPflanze.Items.RemoveAt(i);
                        break;
                    }
                }
            }

            //Entfernt Blutblatt und Karain, falls kein astraler Ort gewählt
            //Entfernt Schwarzer Mohn, falls nicht Palakar gewählt
            for (int i = this.Kraeuter_BoxPflanze.Items.Count - 1; i >= 0; i--)
            {
                object o = this.Kraeuter_BoxPflanze.Items[i];
                bool delete = true;

                if (o.ToString().Equals("Kairan") || o.ToString().Equals("Blutblatt"))
                {
                    if (this.Kraeuter_BoxBesonderheiten.SelectedItem.ToString().Equals("Astral durchzogener Ort"))
                        delete = false;
                    if (delete)
                        this.Kraeuter_BoxPflanze.Items.RemoveAt(i);
                }

                if (o.ToString().Equals("Schwarzer Mohn"))
                {
                    if (this.Kraeuter_BoxBesonderheiten.SelectedItem.ToString().Equals("Palakar (Schwarze Stadt)"))
                        delete = false;
                    if (delete)
                        this.Kraeuter_BoxPflanze.Items.RemoveAt(i);
                }
            }

            //Entfernt die nicht im Suchmonat erntebaren Pflanzen
            for (int i = this.Kraeuter_BoxPflanze.Items.Count - 1; i >= 0; i--)
            {
                object o = this.Kraeuter_BoxPflanze.Items[i];
                bool delete = true;

                foreach (BasisPflanze p in this.Pflanzen)
                {
                    if (o.ToString().Equals(p.Name))
                    {
                        if(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString().Equals("Komplettes Jahr"))
                        {
                            delete = false;
                        }
                        else
                        {
                            foreach (string s in p.GetErntezeit(this.Kraeuter_BoxRegion.SelectedItem.ToString()))
                            {
                                if (s.Equals(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString()))
                                    delete = false;
                            }
                            if (delete)
                                this.Kraeuter_BoxPflanze.Items.RemoveAt(i);
                            break;
                        }
                    }
                }
            }

            this.Kraeuter_Zuschlag.Text = "";
        }

        private void Kraeuter_BoxPflanze_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool doProbe = true;
            
            //Prüfung ob Bedingungen für Berechnung des Zuschlag gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (this.Kraeuter_BoxPflanze.SelectedItem == null || this.Kraeuter_BoxLandschaft.SelectedItem == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Boni
                int bonus = 0;
                if (this.Kraeuter_HatGelaendekunde.Checked)
                    bonus += 3;
                if (this.Kraeuter_HatOrtskenntnis.Checked)
                    bonus += 7;

                //Ermittelung der Suchschwierigkeit
                int suchschwierigkeit = 0;
                foreach (BasisPflanze p in this.Pflanzen)
                {
                    if (this.Kraeuter_BoxPflanze.SelectedItem.ToString().Equals(p.Name))
                    {
                        suchschwierigkeit += p.GetBestimmung(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString(), this.Kraeuter_BoxBesonderheiten.SelectedItem.ToString(), this.Kraeuter_BoxLandschaft.SelectedItem.ToString());
                        foreach (VerbreitungsElementPflanzen v in p.GetVerbreitung(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString()))
                        {
                            if (this.Kraeuter_BoxLandschaft.SelectedItem.ToString().Equals(v.Landschaft))
                            {
                                suchschwierigkeit += v.Vorkommen;
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

                this.Kraeuter_Zuschlag.Text = zuschlag;
            }
        }

        private void Kraeuter_BoxSuchmonat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Falls Landschaft bereits gewählt wurde, wird in Abhängigkeit davon die Pflanzenliste neu berechnet, andernfalls 
            //in Abhängigkeit der Region, so diese bereits gewählt wurde.
            if (this.Kraeuter_BoxLandschaft.SelectedItem != null)
                this.Kraeuter_BoxLandschaft_SelectedIndexChanged(null, null);
            else if (this.Kraeuter_BoxRegion.SelectedItem != null)
                this.Kraeuter_BoxRegion_SelectedIndexChanged(null, null);

            this.Kraeuter_Zuschlag.Text = "";
        }

        private void Kraeuter_BoxBesonderheiten_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Falls Landschaft bereits gewählt wurde, wird in Abhängigkeit davon die Pflanzenliste neu berechnet, andernfalls 
            //in Abhängigkeit der Region, so diese bereits gewählt wurde.
            if (this.Kraeuter_BoxLandschaft.SelectedItem != null)
                this.Kraeuter_BoxLandschaft_SelectedIndexChanged(null, null);
            else if (this.Kraeuter_BoxRegion.SelectedItem != null)
                this.Kraeuter_BoxRegion_SelectedIndexChanged(null, null);

            this.Kraeuter_Zuschlag.Text = "";

            //Astral durchzogener Ort und Palakar erfordern weiter Pflanzen in der Liste, was in der Neuberechnung der
            //Pflanzenliste Beachtung findet. Alle anderen Optionen haben nur Einfluss auf Bestimmung und/oder Grundmenge
        }
        #endregion

        #region Kräutersuche - Buttons: Gezielte Suche, Allgemeine Suche
        /// <summary>
        /// Button für gezielte Suche nach einer Pflanze
        /// </summary>
        private void ButtonSuchePflanzeGezielt_Click(object sender, EventArgs e)
        {
            bool doProbe = true;

            //Prüfung ob Bedingungen für Suche gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (this.Kraeuter_BoxPflanze.SelectedItem == null || this.Kraeuter_BoxLandschaft.SelectedItem == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Talentwert und Boni
                int taw = (int)this.Kraeuter_TAWKraeuter.Value;
                if (this.Kraeuter_HatSuchdauerVerdoppelt.Checked)
                    taw = (int)this.Kraeuter_TAWKraeuter.Value + (int)Math.Round((double)this.Kraeuter_TAWKraeuter.Value / 2.0, MidpointRounding.AwayFromZero);

                int bonus = 0;
                if (this.Kraeuter_HatGelaendekunde.Checked)
                    bonus += 3;
                if (this.Kraeuter_HatOrtskenntnis.Checked)
                    bonus += 7;

                //Ermittelung der Suchschwierigkeit
                int suchschwierigkeit = 0;
                foreach (BasisPflanze p in this.Pflanzen)
                {
                    if (this.Kraeuter_BoxPflanze.SelectedItem.ToString().Equals(p.Name))
                    {
                        suchschwierigkeit += p.GetBestimmung(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString(),this.Kraeuter_BoxBesonderheiten.SelectedItem.ToString(),this.Kraeuter_BoxLandschaft.SelectedItem.ToString());
                        foreach (VerbreitungsElementPflanzen v in p.GetVerbreitung(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString()))
                        {
                            if (this.Kraeuter_BoxLandschaft.SelectedItem.ToString().Equals(v.Landschaft))
                            {
                                suchschwierigkeit += v.Vorkommen;
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

                //Talentprobe
                int tapstern = 0;
                bool result = this.Talentprobe((int)this.Kraeuter_MU.Value, (int)this.Kraeuter_IN.Value, (int)this.Kraeuter_FF.Value, taw, erschw, out tapstern, this.Kraeuter_TextfeldAusgabe);

                //this.Kraeuter_TextfeldAusgabe.Text = this.Kraeuter_TextfeldAusgabe.Text + "Suchschwierigkeit vor Einrechnung von Ortskenntnis und Geländekunde betrug: " + suchschwierigkeit + " \r\n";
                //this.Kraeuter_TextfeldAusgabe.Text = this.Kraeuter_TextfeldAusgabe.Text + "Probenerschwernis nach Einrechnung von Ortskenntnis und Geländekunde betrug: " + erschw + " \r\n";

                //Ausgabe Ergebnis
                if (result)
                {
                    string grundmenge = "";
                    string referenz = "";
                    string gefahr = "";
                    foreach (BasisPflanze p in this.Pflanzen)
                    {
                        if (p.Name.Equals(this.Kraeuter_BoxPflanze.SelectedItem.ToString()))
                        {
                            gefahr = p.GetGefahr();
                            grundmenge = p.GetGrundmenge(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString(), tapstern);
                            referenz = p.SeiteZBA.ToString();
                            break;
                        }
                    }

                    if (this.Kraeuter_HatSuchdauerVerdoppelt.Checked)
                        this.Kraeuter_TextfeldAusgabe.Text += "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 2 Stunden gedauert. \r\n\r\n";
                    else
                        this.Kraeuter_TextfeldAusgabe.Text += "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1 Stunde gedauert. \r\n\r\n";

                    if (suchschwierigkeit <= 0)
                        suchschwierigkeit = 2;

                    int menge = 1;
                    while (tapstern >= Math.Round(suchschwierigkeit / 2.0, MidpointRounding.AwayFromZero))
                    {
                        menge++;
                        tapstern -= (int)Math.Round(suchschwierigkeit / 2.0, MidpointRounding.AwayFromZero);
                    }

                    if (!grundmenge.Equals(""))
                        this.Kraeuter_TextfeldAusgabe.Text += "Von der Pflanze " + this.Kraeuter_BoxPflanze.SelectedItem.ToString() + " wurde insgesamt " + menge + " mal die Grundmenge (" + grundmenge + ") gefunden. ";
                    else
                        this.Kraeuter_TextfeldAusgabe.Text += "Die Pflanze " + this.Kraeuter_BoxPflanze.SelectedItem.ToString() + " hat keine bekannten verwertbaren Pflanzenteile. ";

                    this.Kraeuter_TextfeldAusgabe.Text += "\r\n\r\nFür detailliertere Informationen siehe \"Zoo-Botanica Aventurica\" Seite " + referenz + ".";

                    if (!gefahr.Equals(""))
                    {
                        this.Kraeuter_TextfeldAusgabe.Text += "\r\n\r\nHinweis: ";
                        this.Kraeuter_TextfeldAusgabe.Text += gefahr;
                    }
                }
                else
                {
                    this.Kraeuter_TextfeldAusgabe.Text += "Die Probe ist leider misslungen.";
                }
            }
            else
            {
                this.Kraeuter_TextfeldAusgabe.Text = "Auswahl wurde nicht korrekt durchgeführt und eine Suche ist daher nicht möglich. Wurden Region, Landschaft und zu suchende Pflanze gewählt?";
            }
        }

        /// <summary>
        /// Button für allgemeine Suche nach Pflanzen
        /// </summary>
        private void ButtonSuchePflanzen_Click(object sender, EventArgs e)
        {
            bool doProbe = true;

            //Prüfung ob Bedingungen für Suche gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (this.Kraeuter_BoxLandschaft.SelectedItem == null)
                doProbe = false;

            if (doProbe)
            {
                //Ermittelung des Talentwertes
                int taw = (int)this.Kraeuter_TAWKraeuter.Value;
                if (this.Kraeuter_HatSuchdauerVerdoppelt.Checked)
                    taw = (int)this.Kraeuter_TAWKraeuter.Value + (int)Math.Round((double)this.Kraeuter_TAWKraeuter.Value / 2.0, MidpointRounding.AwayFromZero);
                if (this.Kraeuter_HatGelaendekunde.Checked)
                    taw += 3;
                if (this.Kraeuter_HatOrtskenntnis.Checked)
                    taw += 7;

                //Talentprobe
                int tapstern = 0;
                bool result = this.Talentprobe((int)this.Kraeuter_MU.Value, (int)this.Kraeuter_IN.Value, (int)this.Kraeuter_FF.Value, taw, 0, out tapstern, this.Kraeuter_TextfeldAusgabe);

                if (result)
                {
                    if (this.Kraeuter_HatSuchdauerVerdoppelt.Checked)
                        this.Kraeuter_TextfeldAusgabe.Text += "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 2 Stunden gedauert. \r\n\r\n";
                    else
                        this.Kraeuter_TextfeldAusgabe.Text += "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1 Stunde gedauert. \r\n\r\n";
                    
                    //Bestimmung Suchschwierigkeit für jede grundsätzlich findbare Pflanze und Vergleich mit TaP* ob Fund möglich
                    ArrayList optionen = new ArrayList();
                    foreach (object o in this.Kraeuter_BoxPflanze.Items)
                    {
                        int suchschwierigkeit = 0;
                        foreach (BasisPflanze p in this.Pflanzen)
                        {
                            if (o.ToString().Equals(p.Name))
                            {
                                suchschwierigkeit += p.GetBestimmung(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString(), this.Kraeuter_BoxBesonderheiten.SelectedItem.ToString(), this.Kraeuter_BoxLandschaft.SelectedItem.ToString());
                                foreach (VerbreitungsElementPflanzen v in p.GetVerbreitung(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString()))
                                {
                                    if (this.Kraeuter_BoxLandschaft.SelectedItem.ToString().Equals(v.Landschaft))
                                    {
                                        suchschwierigkeit += v.Vorkommen;
                                    }
                                }
                                break;
                            }
                        }

                        if (Math.Round(tapstern / 2.0, MidpointRounding.AwayFromZero) >= suchschwierigkeit)
                        {
                            optionen.Add(o);
                        }
                    }

                    //Falls mögliche Pflanzenfunde existieren, zufällige Auswahl und Ausgabe Ergebnis
                    if (optionen.Count > 0)
                    {
                        Random rand = new Random();
                        int fund = rand.Next(0, optionen.Count);                       

                        int suchschwierigkeit = 0;
                        foreach (BasisPflanze p in this.Pflanzen)
                        {
                            if (optionen[fund].ToString().Equals(p.Name))
                            {
                                suchschwierigkeit += p.GetBestimmung(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString(), this.Kraeuter_BoxBesonderheiten.SelectedItem.ToString(), this.Kraeuter_BoxLandschaft.SelectedItem.ToString());
                                foreach (VerbreitungsElementPflanzen v in p.GetVerbreitung(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString()))
                                {
                                    if (this.Kraeuter_BoxLandschaft.SelectedItem.ToString().Equals(v.Landschaft))
                                    {
                                        suchschwierigkeit += v.Vorkommen;
                                    }
                                }
                                break;
                            }
                        }

                        if (suchschwierigkeit <= 0)
                            suchschwierigkeit = 2;

                        string grundmenge = "";
                        string referenz = "";
                        string gefahr = "";
                        foreach (BasisPflanze p in this.Pflanzen)
                        {
                            if (p.Name.Equals(optionen[fund].ToString()))
                            {
                                gefahr = p.GetGefahr();
                                grundmenge = p.GetGrundmenge(this.Kraeuter_BoxSuchmonat.SelectedItem.ToString(), tapstern);
                                referenz = p.SeiteZBA.ToString();
                                break;
                            }
                        }

                        int menge = 1;
                        tapstern -= suchschwierigkeit;
                        while(tapstern >= suchschwierigkeit)
                        {
                            menge++;
                            tapstern -= suchschwierigkeit;
                        }

                        if (!grundmenge.Equals(""))
                            this.Kraeuter_TextfeldAusgabe.Text += "Von der Pflanze " + optionen[fund].ToString() + " wurde insgesamt " + menge + " mal die Grundmenge (" + grundmenge + ") gefunden. ";
                        else
                            this.Kraeuter_TextfeldAusgabe.Text += "Die Pflanze " + optionen[fund].ToString() + " hat keine bekannten verwertbaren Pflanzenteile. ";
                        this.Kraeuter_TextfeldAusgabe.Text += "\r\n\r\nFür detailliertere Informationen siehe \"Zoo-Botanica Aventurica\" Seite " + referenz + ".";

                        if (!gefahr.Equals(""))
                        {
                            this.Kraeuter_TextfeldAusgabe.Text += "\r\n\r\nHinweis: ";
                            this.Kraeuter_TextfeldAusgabe.Text += gefahr;
                        }
                    }
                    else
                    {
                        this.Kraeuter_TextfeldAusgabe.Text += "Obwohl die Probe gelungen ist, reicht es leider nicht aus um etwas brauchbares zu finden.";
                    }

                }
                else
                {
                    this.Kraeuter_TextfeldAusgabe.Text += "Die Probe ist leider misslungen.";
                }
            }
            else
            {
                this.Kraeuter_TextfeldAusgabe.Text = "Auswahl wurde nicht korrekt durchgeführt und eine Suche ist daher nicht möglich. Wurde Region, Landschaft und zu suchende Pflanze gewählt?";
            }
        }
        #endregion

        #region Nahrungssuche - Eigenschaften, Talente, Checkboxen
        private void Nahrung_MU_ValueChanged(object sender, EventArgs e)
        {
            if (this.About_Kopplung.Checked)
            {
                this.Kraeuter_MU.Value = this.Nahrung_MU.Value;
                this.Jagd_MU.Value = this.Nahrung_MU.Value;
            }
        }

        private void Nahrung_IN_ValueChanged(object sender, EventArgs e)
        {
            if (this.About_Kopplung.Checked)
            {
                this.Kraeuter_IN.Value = this.Nahrung_IN.Value;
                this.Jagd_IN.Value = this.Nahrung_IN.Value;
                this.Fischen_IN.Value = this.Nahrung_IN.Value;
            }
        }

        private void Nahrung_FF_ValueChanged(object sender, EventArgs e)
        {
            if (this.About_Kopplung.Checked)
            {
                this.Kraeuter_FF.Value = this.Nahrung_FF.Value;
                this.Fischen_FF.Value = this.Nahrung_FF.Value;
            }
        }

        private void Nahrung_TAWSinnes_ValueChanged(object sender, EventArgs e)
        {
            this.BerechneTAWNahrungssuche();
            if (this.About_Kopplung.Checked)
                this.Kraeuter_TAWSinnes.Value = this.Nahrung_TAWSinnes.Value;
        }

        private void Nahrung_TAWWildnis_ValueChanged(object sender, EventArgs e)
        {
            this.BerechneTAWNahrungssuche();
            if (this.About_Kopplung.Checked)
            {
                this.Kraeuter_TAWWildnis.Value = this.Nahrung_TAWWildnis.Value;
                this.Jagd_TAWWildnisleben.Value = this.Nahrung_TAWWildnis.Value;
            }
        }

        private void Nahrung_TAWPflanzen_ValueChanged(object sender, EventArgs e)
        {
            this.BerechneTAWNahrungssuche();
            if (this.About_Kopplung.Checked)
                this.Kraeuter_TAWPflanzen.Value = this.Nahrung_TAWPflanzen.Value;
        }

        private void Nahrung_TAWAckerbau_ValueChanged(object sender, EventArgs e)
        {
            this.BerechneTAWNahrungssuche();
        }

        private void BerechneTAWNahrungssuche()
        {
            if (this.Nahrung_NutzeAckerbau.Checked)
            {
                this.Nahrung_TAWNahrung.Value = Math.Round((this.Nahrung_TAWSinnes.Value + this.Nahrung_TAWAckerbau.Value + this.Nahrung_TAWPflanzen.Value) / 3, MidpointRounding.AwayFromZero);

                if (this.Nahrung_TAWNahrung.Value > 2 * this.Nahrung_TAWSinnes.Value)
                    this.Nahrung_TAWNahrung.Value = 2 * this.Nahrung_TAWSinnes.Value;
                if (this.Nahrung_TAWNahrung.Value > 2 * this.Nahrung_TAWAckerbau.Value)
                    this.Nahrung_TAWNahrung.Value = 2 * this.Nahrung_TAWAckerbau.Value;
                if (this.Nahrung_TAWNahrung.Value > 2 * this.Nahrung_TAWPflanzen.Value)
                    this.Nahrung_TAWNahrung.Value = 2 * this.Nahrung_TAWPflanzen.Value;
            }
            else
            {
                this.Nahrung_TAWNahrung.Value = Math.Round((this.Nahrung_TAWSinnes.Value + this.Nahrung_TAWWildnis.Value + this.Nahrung_TAWPflanzen.Value) / 3, MidpointRounding.AwayFromZero);

                if (this.Nahrung_TAWNahrung.Value > 2 * this.Nahrung_TAWSinnes.Value)
                    this.Nahrung_TAWNahrung.Value = 2 * this.Nahrung_TAWSinnes.Value;
                if (this.Nahrung_TAWNahrung.Value > 2 * this.Nahrung_TAWWildnis.Value)
                    this.Nahrung_TAWNahrung.Value = 2 * this.Nahrung_TAWWildnis.Value;
                if (this.Nahrung_TAWNahrung.Value > 2 * this.Nahrung_TAWPflanzen.Value)
                    this.Nahrung_TAWNahrung.Value = 2 * this.Nahrung_TAWPflanzen.Value;
            }
        }

        private void Nahrung_TAWNahrung_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Nahrung_NutzeAckerbau_CheckedChanged(object sender, EventArgs e)
        {
            this.BerechneTAWNahrungssuche();
        }

        private void Nahrung_HatGelaendekunde_CheckedChanged(object sender, EventArgs e)
        {
            if(this.Nahrung_BoxRegion.SelectedItem != null)
                this.Nahrung_BoxRegion_SelectedIndexChanged(null, null);
        }

        private void Nahrung_HatOrtskenntnis_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Nahrung_BoxRegion.SelectedItem != null)
                this.Nahrung_BoxRegion_SelectedIndexChanged(null, null);
        }

        private void Nahrung_HatSuchdauerVerdoppelt_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Nahrungssuche - Auswahlboxen: Region
        private void Nahrung_BoxRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Bestimmung Boni
            int bonus = 0;
            if (this.Nahrung_HatGelaendekunde.Checked)
                bonus += 3;
            if (this.Nahrung_HatOrtskenntnis.Checked)
                bonus += 7;

            //Sucht den in der Region vorhandenen Zuschlag auf das Sammeln von Nahrung
            int suchschwierigkeit = 0;
            foreach (BasisRegion r in this.Regionen)
            {
                if (this.Nahrung_BoxRegion.SelectedItem.ToString().Equals(r.Name))
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
                this.Nahrung_Zuschlag.Text = "+" + erschw.ToString();
            else
                this.Nahrung_Zuschlag.Text = "---";

        }
        #endregion 

        #region Nahrungssuche - Buttons: Nahrung sammeln
        /// <summary>
        /// Implementiert die komplette Nahrungssuche
        /// </summary>
        private void Nahrung_ButtonSuche_Click(object sender, EventArgs e)
        {
            bool doProbe = true;

            //Prüfung ob Bedingungen für Suche gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (this.Nahrung_BoxRegion.SelectedItem == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Talentwert und Boni
                int taw = (int)this.Nahrung_TAWNahrung.Value;
                if (this.Nahrung_HatSuchdauerVerdoppelt.Checked)
                    taw = (int)this.Nahrung_TAWNahrung.Value + (int)Math.Round((double)this.Nahrung_TAWNahrung.Value / 2.0, MidpointRounding.AwayFromZero);

                int bonus = 0;
                if (this.Nahrung_HatGelaendekunde.Checked)
                    bonus += 3;
                if (this.Nahrung_HatOrtskenntnis.Checked)
                    bonus += 7;

                //Ermittelung der Suchschwierigkeit
                int suchschwierigkeit = 0;
                foreach (BasisRegion r in this.Regionen)
                {
                    if (this.Nahrung_BoxRegion.SelectedItem.ToString().Equals(r.Name))
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
                    if (this.Nahrung_HatOrtskenntnis.Checked)
                    {
                        result = true;
                        tapstern = taw - erschw;
                        if (tapstern < 1)
                            tapstern = 1;
                        this.Nahrung_TextfeldAusgabe.Text = "Es wurde keine Probe gewürfelt, da der Suchende über Ortskenntnis im betreffenden Gebiet verfügt.\r\n";
                    }
                    else
                    {
                        result = this.Talentprobe((int)this.Nahrung_MU.Value, (int)this.Nahrung_IN.Value, (int)this.Nahrung_FF.Value, taw, erschw, out tapstern, this.Nahrung_TextfeldAusgabe);
                    }
                    //this.Kraeuter_TextfeldAusgabe.Text = this.Kraeuter_TextfeldAusgabe.Text + "Suchschwierigkeit vor Einrechnung von Ortskenntnis und Geländekunde betrug: " + suchschwierigkeit + " \r\n";
                    //this.Kraeuter_TextfeldAusgabe.Text = this.Kraeuter_TextfeldAusgabe.Text + "Probenerschwernis nach Einrechnung von Ortskenntnis und Geländekunde betrug: " + erschw + " \r\n";

                    //Ausgabe Ergebnis
                    if (result)
                    {
                        if (this.Nahrung_HatSuchdauerVerdoppelt.Checked)
                            this.Nahrung_TextfeldAusgabe.Text += "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 2 Stunden gedauert. \r\n\r\n";
                        else
                            this.Nahrung_TextfeldAusgabe.Text += "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1 Stunde gedauert. \r\n\r\n";

                        int menge = 1;

                        while (tapstern >= 3)
                        {
                            menge++;
                            tapstern -= 3;
                        }

                        if(menge > 1)
                            this.Nahrung_TextfeldAusgabe.Text += "Es wurden insgesamt " + menge + " Tagesrationen an essbaren Pflanzen gefunden. ";
                        else
                            this.Nahrung_TextfeldAusgabe.Text += "Es wurde eine Tagesration an essbaren Pflanzen gefunden. ";

                        this.Nahrung_TextfeldAusgabe.Text += "\r\n\r\nFür detailliertere Informationen siehe \"Zoo-Botanica Aventurica\" Seite 224.";
                    }
                    else
                    {
                        this.Nahrung_TextfeldAusgabe.Text += "Die Probe ist leider misslungen.";
                    }
                }
                else
                {
                    this.Nahrung_TextfeldAusgabe.Text = "In dieser Region gibt es keine essbaren Pflanzen!";
                }
            }
            else
            {
                this.Nahrung_TextfeldAusgabe.Text = "Auswahl wurde nicht korrekt durchgeführt und eine Suche ist daher nicht möglich. Wurde die Region gewählt?";
            }
        }
        #endregion

        #region Jagd - Eigenschaften, Talente, Checkboxen
        private void Jagd_MU_ValueChanged(object sender, EventArgs e)
        {
            if (this.About_Kopplung.Checked)
            {
                this.Kraeuter_MU.Value = this.Jagd_MU.Value;
                this.Nahrung_MU.Value = this.Jagd_MU.Value;
            }
        }

        private void Jagd_IN_ValueChanged(object sender, EventArgs e)
        {
            if (this.About_Kopplung.Checked)
            {
                this.Kraeuter_IN.Value = this.Jagd_IN.Value;
                this.Nahrung_IN.Value = this.Jagd_IN.Value;
                this.Fischen_IN.Value = this.Jagd_IN.Value;
            }
        }

        private void Jagd_GE_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Jagd_TAWFaehrtensuche_ValueChanged(object sender, EventArgs e)
        {
            this.BerechneTAWJagd();
        }

        private void Jagd_TAWWildnisleben_ValueChanged(object sender, EventArgs e)
        {
            this.BerechneTAWJagd();
            if (this.About_Kopplung.Checked)
            {
                this.Kraeuter_TAWWildnis.Value = this.Jagd_TAWWildnisleben.Value;
                this.Nahrung_TAWWildnis.Value = this.Jagd_TAWWildnisleben.Value;
            }
        }

        private void Jagd_TAWTierkunde_ValueChanged(object sender, EventArgs e)
        {
            this.BerechneTAWJagd();
        }

        private void Jagd_TAWSchleichen_ValueChanged(object sender, EventArgs e)
        {
            this.BerechneTAWJagd();
        }

        private void Jagd_TAWSichVerstecken_ValueChanged(object sender, EventArgs e)
        {
            this.BerechneTAWJagd();
        }

        private void Jagd_TAWFernkampfwaffe_ValueChanged(object sender, EventArgs e)
        {
            this.BerechneTAWJagd();
        }

        private void BerechneTAWJagd()
        {
            this.Jagd_TAWPirschjagd.Value = Math.Round((this.Jagd_TAWWildnisleben.Value + this.Jagd_TAWTierkunde.Value + this.Jagd_TAWFaehrtensuche.Value + this.Jagd_TAWSchleichen.Value + this.Jagd_TAWFernkampfwaffe.Value) / 5, MidpointRounding.AwayFromZero);
            this.Jagd_TAWAnsitzjagd.Value = Math.Round((this.Jagd_TAWWildnisleben.Value + this.Jagd_TAWTierkunde.Value + this.Jagd_TAWFaehrtensuche.Value + this.Jagd_TAWSichVerstecken.Value + this.Jagd_TAWFernkampfwaffe.Value) / 5, MidpointRounding.AwayFromZero);

            if (this.Jagd_TAWPirschjagd.Value > 2 * this.Jagd_TAWWildnisleben.Value)
                this.Jagd_TAWPirschjagd.Value = 2 * this.Jagd_TAWWildnisleben.Value;
            if (this.Jagd_TAWPirschjagd.Value > 2 * this.Jagd_TAWTierkunde.Value)
                this.Jagd_TAWPirschjagd.Value = 2 * this.Jagd_TAWTierkunde.Value;
            if (this.Jagd_TAWPirschjagd.Value > 2 * this.Jagd_TAWFaehrtensuche.Value)
                this.Jagd_TAWPirschjagd.Value = 2 * this.Jagd_TAWFaehrtensuche.Value;
            if (this.Jagd_TAWPirschjagd.Value > 2 * this.Jagd_TAWSchleichen.Value)
                this.Jagd_TAWPirschjagd.Value = 2 * this.Jagd_TAWSchleichen.Value;
            if (this.Jagd_TAWPirschjagd.Value > 2 * this.Jagd_TAWFernkampfwaffe.Value)
                this.Jagd_TAWPirschjagd.Value = 2 * this.Jagd_TAWFernkampfwaffe.Value;

            if (this.Jagd_TAWAnsitzjagd.Value > 2 * this.Jagd_TAWWildnisleben.Value)
                this.Jagd_TAWAnsitzjagd.Value = 2 * this.Jagd_TAWWildnisleben.Value;
            if (this.Jagd_TAWAnsitzjagd.Value > 2 * this.Jagd_TAWTierkunde.Value)
                this.Jagd_TAWAnsitzjagd.Value = 2 * this.Jagd_TAWTierkunde.Value;
            if (this.Jagd_TAWAnsitzjagd.Value > 2 * this.Jagd_TAWFaehrtensuche.Value)
                this.Jagd_TAWAnsitzjagd.Value = 2 * this.Jagd_TAWFaehrtensuche.Value;
            if (this.Jagd_TAWAnsitzjagd.Value > 2 * this.Jagd_TAWSichVerstecken.Value)
                this.Jagd_TAWAnsitzjagd.Value = 2 * this.Jagd_TAWSichVerstecken.Value;
            if (this.Jagd_TAWAnsitzjagd.Value > 2 * this.Jagd_TAWFernkampfwaffe.Value)
                this.Jagd_TAWAnsitzjagd.Value = 2 * this.Jagd_TAWFernkampfwaffe.Value;
        }

        private void Jagd_HatOrtskenntnis_CheckedChanged(object sender, EventArgs e)
        {
            this.Jagd_BoxTier_SelectedIndexChanged(null, null);
        }

        private void Jagd_HatGelaendekunde_CheckedChanged(object sender, EventArgs e)
        {
            this.Jagd_BoxTier_SelectedIndexChanged(null, null);
        }

        private void Jagd_IstScharfschuetze_CheckedChanged(object sender, EventArgs e)
        {
            this.Jagd_BoxTier_SelectedIndexChanged(null, null);
        }

        private void Jagd_IstMeisterschuetze_CheckedChanged(object sender, EventArgs e)
        {
            this.Jagd_BoxTier_SelectedIndexChanged(null, null);
        }
        #endregion

        #region Jagd - Auswahlboxen: Region, Landschaft, Tiere
        private void Jagd_BoxRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Setzt leere Strings in allen Boxen, da sonst NullPointExceptions
            this.Jagd_BoxLandschaft.Text = "";
            this.Jagd_BoxLandschaft.Items.Clear();
            this.Jagd_BoxTier.Text = "";
            this.Jagd_BoxTier.Items.Clear();

            //Sucht alle in der Region vorkommenden Landschaften und Tiere
            foreach (BasisRegion r in this.Regionen)
            {
                if (this.Jagd_BoxRegion.SelectedItem.ToString().Equals(r.Name))
                {
                    //Fügt alle in der Region gespeicherten Tiere hinzu
                    foreach (VerbreitungsElementTiere v in r.Tiere)
                    {
                        this.Jagd_BoxTier.Items.Add(v.Tier);
                    }

                    // alphabetische Sortierung der Tiere
                    object[] SortedItems = new object[this.Jagd_BoxTier.Items.Count];
                    for (int i = 0; i < SortedItems.Length; i++)
                    {
                        SortedItems[i] = this.Jagd_BoxTier.Items[i];
                    }
                    Array.Sort(SortedItems);
                    this.Jagd_BoxTier.Items.Clear();
                    this.Jagd_BoxTier.Items.AddRange(SortedItems);

                    //Fügt alle in den Tieren dieser Region gespeicherten Landschaften hinzu
                    foreach (VerbreitungsElementTiere v in r.Tiere)
                    {
                        foreach (BasisTier t in this.Tiere)
                        {
                            if (v.Tier.Equals(t.Name))
                            {
                                foreach(string l in t.GetVerbreitung())
                                {
                                    if(!this.Jagd_BoxLandschaft.Items.Contains(l))
                                        this.Jagd_BoxLandschaft.Items.Add(l);
                                }                                
                            }
                        }
                    }

                    // alphabetische Sortierung der Landschaften                    
                    object[] SortedItemsL = new object[this.Jagd_BoxLandschaft.Items.Count];
                    for (int i = 0; i < SortedItemsL.Length; i++)
                    {
                        SortedItemsL[i] = this.Jagd_BoxLandschaft.Items[i];
                    }
                    Array.Sort(SortedItemsL);
                    this.Jagd_BoxLandschaft.Items.Clear();
                    this.Jagd_BoxLandschaft.Items.AddRange(SortedItemsL);
                }
            }

            this.Jagd_Zuschlag.Text = "";
        }

        private void Jagd_BoxLandschaft_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Jagd_BoxTier.Text = "";
            this.Jagd_BoxTier.Items.Clear();

            // Sucht die in der Region vorkommenden Tiere
            foreach (BasisRegion r in this.Regionen)
            {
                if (this.Jagd_BoxRegion.SelectedItem.ToString().Equals(r.Name))
                {
                    foreach (VerbreitungsElementTiere v in r.Tiere)
                    {
                        this.Jagd_BoxTier.Items.Add(v.Tier);
                    }
                    break;
                }
            }

            //Entfernt die nicht in der gewählten Landschaft vorkommenden Tiere
            for (int i = this.Jagd_BoxTier.Items.Count - 1; i >= 0; i--)
            {
                object o = this.Jagd_BoxTier.Items[i];
                bool delete = true;

                foreach (BasisTier t in this.Tiere)
                {
                    if (o.ToString().Equals(t.Name))
                    {
                        foreach (string l in t.GetVerbreitung())
                        {
                            if (l.Equals(this.Jagd_BoxLandschaft.SelectedItem.ToString()))
                                delete = false;
                        }
                        if (delete)
                            this.Jagd_BoxTier.Items.RemoveAt(i);
                        break;
                    }
                }
            }

            // alphabetische Sortierung der Tiere
            object[] SortedItems = new object[this.Jagd_BoxTier.Items.Count];
            for (int i = 0; i < SortedItems.Length; i++)
            {
                SortedItems[i] = this.Jagd_BoxTier.Items[i];
            }
            Array.Sort(SortedItems);
            this.Jagd_BoxTier.Items.Clear();
            this.Jagd_BoxTier.Items.AddRange(SortedItems);

            this.Jagd_Zuschlag.Text = "";
        }

        private void Jagd_BoxTier_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool doProbe = true;

            //Prüfung ob Bedingungen für Berechnung des Zuschlag gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (this.Jagd_BoxTier.SelectedItem == null || this.Jagd_BoxLandschaft.SelectedItem == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Boni
                int bonus = 0;
                if (this.Jagd_HatGelaendekunde.Checked)
                    bonus += 3;
                if (this.Jagd_HatOrtskenntnis.Checked)
                    bonus += 7;
                if (this.Jagd_IstScharfschuetze.Checked || this.Jagd_IstMeisterschuetze.Checked)
                {
                    if (this.Jagd_IstMeisterschuetze.Checked)
                        bonus += 7;
                    else
                        bonus += 3;
                }

                //Ermittelung der Suchschwierigkeit
                int suchschwierigkeit = 0;
                foreach (BasisTier t in this.Tiere)
                {
                    if (this.Jagd_BoxTier.SelectedItem.ToString().Equals(t.Name))
                    {
                        suchschwierigkeit += t.GetJagdschwierigkeit();
                        foreach (BasisRegion r in this.Regionen)
                        {
                            if (this.Jagd_BoxRegion.SelectedItem.ToString().Equals(r.Name))
                            {
                                foreach(VerbreitungsElementTiere v in r.Tiere)
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

                this.Jagd_Zuschlag.Text = zuschlag;
            }
        }
        #endregion

        #region Jagd - Buttons: Pirschjagd und Ansitzjagd
        private void Jagd_ButtonPirschjagd_Click(object sender, EventArgs e)
        {
            JagdAbwicklung((int)this.Jagd_TAWPirschjagd.Value, 1);
        }

        private void Jagd_ButtonAnsitzjagd_Click(object sender, EventArgs e)
        {
            JagdAbwicklung((int)this.Jagd_TAWAnsitzjagd.Value, 1.5);
        }

        private void JagdAbwicklung(int Talentwert, double dauer)
        {
            bool doProbe = true;

            //Prüfung ob Bedingungen für Jagd gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (this.Jagd_BoxTier.SelectedItem == null || this.Jagd_BoxLandschaft.SelectedItem == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Talentwert und Boni
                int taw = Talentwert;

                //Bestimmung Boni
                int bonus = 0;
                if (this.Jagd_HatGelaendekunde.Checked)
                    bonus += 3;
                if (this.Jagd_HatOrtskenntnis.Checked)
                    bonus += 7;
                if (this.Jagd_IstScharfschuetze.Checked || this.Jagd_IstMeisterschuetze.Checked)
                {
                    if (this.Jagd_IstMeisterschuetze.Checked)
                        bonus += 7;
                    else
                        bonus += 3;
                }

                //Ermittelung der Jagdschwierigkeit
                int jagdschwierigkeit = 0;
                foreach (BasisTier t in this.Tiere)
                {
                    if (this.Jagd_BoxTier.SelectedItem.ToString().Equals(t.Name))
                    {
                        jagdschwierigkeit += t.GetJagdschwierigkeit();
                        foreach (BasisRegion r in this.Regionen)
                        {
                            if (this.Jagd_BoxRegion.SelectedItem.ToString().Equals(r.Name))
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
                bool result = this.Talentprobe((int)this.Jagd_MU.Value, (int)this.Jagd_IN.Value, (int)this.Jagd_GE.Value, taw, erschw, out tapstern, this.Jagd_TextfeldAusgabe);

                //this.Jagd_TextfeldAusgabe.Text = this.Jagd_TextfeldAusgabe.Text + "Suchschwierigkeit vor Einrechnung von Ortskenntnis und Geländekunde betrug: " + jagdschwierigkeit + " \r\n";
                //this.Jagd_TextfeldAusgabe.Text = this.Jagd_TextfeldAusgabe.Text + "Probenerschwernis nach Einrechnung von Ortskenntnis und Geländekunde betrug: " + erschw + " \r\n";

                //Ausgabe Ergebnis
                if (result)
                {
                    string beute = "";
                    string referenz = "";
                    string gefahr = "";
                    foreach (BasisTier t in this.Tiere)
                    {
                        if (t.Name.Equals(this.Jagd_BoxTier.SelectedItem.ToString()))
                        {
                            gefahr = t.GetGefahr();
                            beute = t.GetBeute();
                            referenz = t.SeiteZBA.ToString();
                            break;
                        }
                    }

                    if(dauer == 1)
                        this.Jagd_TextfeldAusgabe.Text += "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1 Stunde gedauert. \r\n\r\n";
                    else
                        this.Jagd_TextfeldAusgabe.Text += "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1.5 Stunden gedauert. \r\n\r\n";

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
                        if (menge == 1)
                            this.Jagd_TextfeldAusgabe.Text += "Es wurde 1 Exemplar der Gattung " + this.Jagd_BoxTier.SelectedItem.ToString() + " erlegt. ";
                        else
                            this.Jagd_TextfeldAusgabe.Text += "Es wurden " + menge + " Exemplare der Gattung " + this.Jagd_BoxTier.SelectedItem.ToString() + " erlegt. ";

                        this.Jagd_TextfeldAusgabe.Text += "Jedes Exemplar liefert folgende Beuteteile: " + beute;
                    }
                    else
                        this.Jagd_TextfeldAusgabe.Text += "Das Tier " + this.Jagd_BoxTier.SelectedItem.ToString() + " hat keine bekannten verwertbaren Beuteteile. ";

                    this.Jagd_TextfeldAusgabe.Text += "\r\n\r\nFür detailliertere Informationen siehe \"Zoo-Botanica Aventurica\" Seite " + referenz + ".";

                    if (!gefahr.Equals(""))
                    {
                        this.Jagd_TextfeldAusgabe.Text += "\r\n\r\nHinweis: ";
                        this.Jagd_TextfeldAusgabe.Text += gefahr;
                    }
                }
                else
                {
                    this.Jagd_TextfeldAusgabe.Text += "Die Probe ist leider misslungen.";
                }
            }
            else
            {
                this.Jagd_TextfeldAusgabe.Text = "Auswahl wurde nicht korrekt durchgeführt und eine Jagd ist daher nicht möglich. Wurden Region, Landschaft und zu jagendes Tier gewählt?";
            }
        }
        #endregion

        #region Fischen - Eigenschaften, Talente, Checkboxen
        private void Fischen_IN_ValueChanged(object sender, EventArgs e)
        {
            if (this.About_Kopplung.Checked)
            {
                this.Kraeuter_IN.Value = this.Fischen_IN.Value;
                this.Nahrung_IN.Value = this.Fischen_IN.Value;
                this.Jagd_IN.Value = this.Fischen_IN.Value;
            }
        }

        private void Fischen_KL_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Fischen_FF_ValueChanged(object sender, EventArgs e)
        {
            if (this.About_Kopplung.Checked)
            {
                this.Kraeuter_FF.Value = this.Fischen_FF.Value;
                this.Nahrung_FF.Value = this.Fischen_FF.Value;
            }
        }

        private void Fischen_KK_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Fischen_TAWFischen_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Fischen_TAWFallenstellen_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Fischen_HatGelaendekunde_CheckedChanged(object sender, EventArgs e)
        {
            this.Fischen_BoxRegion_SelectedIndexChanged(null, null);
        }

        private void Fischen_HatOrtskenntnis_CheckedChanged(object sender, EventArgs e)
        {
            this.Fischen_BoxRegion_SelectedIndexChanged(null, null);
        }

        private void Fischen_RuheAufstellen_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Fischen - Auswahlboxen: Region
        private void Fischen_BoxRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Bestimmung Boni
            int bonus = 0;
            if (this.Fischen_HatGelaendekunde.Checked)
                bonus += 3;
            if (this.Fischen_HatOrtskenntnis.Checked)
                bonus += 7;

            //Sucht die selektierte Region
            int jagdschwierigkeit = 0;
            foreach (BasisRegion r in this.Regionen)
            {
                if (this.Fischen_BoxRegion.SelectedItem.ToString().Equals(r.Name))
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
                this.Fischen_Zuschlag.Text = "+" + erschw.ToString();
            else
                this.Fischen_Zuschlag.Text = "---";

        }
        #endregion

        #region Fischen - Buttons: Fischen und Fallenstellen
        private void Fischen_ButtonGewaesserEinschaetzen_Click(object sender, EventArgs e)
        {
            bool doProbe = true;

            //Prüfung ob Bedingungen für Suche gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (this.Fischen_BoxRegion.SelectedItem == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Talentwert und Boni
                int taw = (int)this.Fischen_TAWFischen.Value;

                int bonus = 0;
                if (this.Fischen_HatGelaendekunde.Checked)
                    bonus += 3;
                if (this.Fischen_HatOrtskenntnis.Checked)
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
                bool result = this.Talentprobe((int)this.Fischen_IN.Value, (int)this.Fischen_IN.Value, (int)this.Fischen_KL.Value, taw, erschw, out tapstern, this.Fischen_TextfeldAusgabe);
                if (result)
                    this.Fischen_TextfeldAusgabe.Text += "Die Probe um einzuschätzen ob es sich überhaupt lohnt hier die Angel auszuwerfen ist mit " + tapstern + " TaP* gelungen.\r\n\r\n";
                else
                    this.Fischen_TextfeldAusgabe.Text += "Die Probe um einzuschätzen ob es sich überhaupt lohnt hier die Angel auszuwerfen ist leider misslungen.\r\n\r\n";

                this.Fischen_TextfeldAusgabe.Text += "Für detailliertere Informationen siehe \"Zoo-Botanica Aventurica\" Seite 60"; // und \"Wege des Schwertes\" Seite 26";
            }
        }

        private void Fischen_ButtonAngeln_Click(object sender, EventArgs e)
        {
            bool doProbe = true;

            //Prüfung ob Bedingungen für Suche gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (this.Fischen_BoxRegion.SelectedItem == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Talentwert und Boni
                int taw = (int)this.Fischen_TAWFischen.Value;                

                int bonus = 0;
                if (this.Fischen_HatGelaendekunde.Checked)
                    bonus += 3;
                if (this.Fischen_HatOrtskenntnis.Checked)
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
                    bool result = this.Talentprobe((int)this.Fischen_IN.Value, (int)this.Fischen_FF.Value, (int)this.Fischen_KK.Value, taw, erschw, out tapstern, this.Fischen_TextfeldAusgabe);

                    //this.Kraeuter_TextfeldAusgabe.Text = this.Kraeuter_TextfeldAusgabe.Text + "Suchschwierigkeit vor Einrechnung von Ortskenntnis und Geländekunde betrug: " + suchschwierigkeit + " \r\n";
                    //this.Kraeuter_TextfeldAusgabe.Text = this.Kraeuter_TextfeldAusgabe.Text + "Probenerschwernis nach Einrechnung von Ortskenntnis und Geländekunde betrug: " + erschw + " \r\n";

                    //Ausgabe Ergebnis
                    if (result)
                    {
                        this.Fischen_TextfeldAusgabe.Text += "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1 Stunde gedauert. \r\n\r\n";

                        int menge = 1;

                        while (tapstern >= 3)
                        {
                            menge++;
                            tapstern -= 3;
                        }

                        if (menge > 1)
                            this.Fischen_TextfeldAusgabe.Text += "Es wurden insgesamt " + menge + " halbe Tagesrationen Fisch gefangen. ";
                        else
                            this.Fischen_TextfeldAusgabe.Text += "Es wurde eine halbe Tagesration Fisch gefangen. ";

                        this.Fischen_TextfeldAusgabe.Text += "\r\n\r\nFür detailliertere Informationen siehe \"Zoo-Botanica Aventurica\" Seite 60"; // und \"Wege des Schwertes\" Seite 26";
                    }
                    else
                    {
                        this.Fischen_TextfeldAusgabe.Text += "Die Probe ist leider misslungen.";
                    }
                }
                else
                {
                    this.Fischen_TextfeldAusgabe.Text = "In dieser Region gibt es keinen Fisch!";
                }
            }
            else
            {
                this.Fischen_TextfeldAusgabe.Text = "Auswahl wurde nicht korrekt durchgeführt und Fischen ist daher nicht möglich. Wurde die Region gewählt?";
            }
        }

        private void Fischen_ButtonFallenstellen_Click(object sender, EventArgs e)
        {
            bool doProbe = true;

            //Prüfung ob Bedingungen für Suche gegeben sind und alle Felder die dazu nötig ausgefüllt sind
            if (this.Fischen_BoxRegion.SelectedItem == null)
                doProbe = false;

            if (doProbe)
            {
                //Bestimmung Talentwert und Boni
                int taw = (int)this.Fischen_TAWFallenstellen.Value;
                if (this.Fischen_RuheAufstellen.Checked)
                    taw = (int)this.Fischen_TAWFallenstellen.Value + (int)Math.Round((double)this.Fischen_TAWFallenstellen.Value / 2.0, MidpointRounding.AwayFromZero);

                int bonus = 0;
                if (this.Fischen_HatGelaendekunde.Checked)
                    bonus += 3;
                if (this.Fischen_HatOrtskenntnis.Checked)
                    bonus += 7;

                //Ermittelung der Suchschwierigkeit
                int jagdschwierigkeit = 0;
                foreach (BasisRegion r in this.Regionen)
                {
                    if (this.Fischen_BoxRegion.SelectedItem.ToString().Equals(r.Name))
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
                    bool result = this.Talentprobe((int)this.Fischen_IN.Value, (int)this.Fischen_FF.Value, (int)this.Fischen_KK.Value, taw, erschw, out tapstern, this.Fischen_TextfeldAusgabe);

                    //this.Kraeuter_TextfeldAusgabe.Text = this.Kraeuter_TextfeldAusgabe.Text + "Suchschwierigkeit vor Einrechnung von Ortskenntnis und Geländekunde betrug: " + suchschwierigkeit + " \r\n";
                    //this.Kraeuter_TextfeldAusgabe.Text = this.Kraeuter_TextfeldAusgabe.Text + "Probenerschwernis nach Einrechnung von Ortskenntnis und Geländekunde betrug: " + erschw + " \r\n";

                    //Ausgabe Ergebnis
                    if (result)
                    {
                        if (this.Fischen_RuheAufstellen.Checked)
                            this.Fischen_TextfeldAusgabe.Text += "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1.5 Stunden gedauert. \r\n\r\n";
                        else
                            this.Fischen_TextfeldAusgabe.Text += "Die Probe ist mit " + tapstern + " TaP* gelungen und hat 1 Stunde gedauert. \r\n\r\n";

                        int menge = 1;
                        while (tapstern >= Math.Round(jagdschwierigkeit / 2.0, MidpointRounding.AwayFromZero))
                        {
                            menge++;
                            tapstern -= (int)Math.Round(jagdschwierigkeit / 2.0, MidpointRounding.AwayFromZero);
                        }

                        if (menge > 1)
                            this.Fischen_TextfeldAusgabe.Text += "Bei der Kontrolle der Fallen zeigt sich, dass insgesamt " + menge + " Tagesrationen Fleisch gefangen wurden. ";
                        else
                            this.Fischen_TextfeldAusgabe.Text += "Bei der Kontrolle der Fallen zeigt sich, dass eine Tagesration Fleisch gefangen wurde. ";

                        this.Fischen_TextfeldAusgabe.Text += "\r\n\r\nFür detailliertere Informationen siehe \"Zoo-Botanica Aventurica\" Seite 60.";
                    }
                    else
                    {
                        this.Fischen_TextfeldAusgabe.Text += "Die Probe ist leider misslungen.";
                    }
                }
                else
                {
                    this.Fischen_TextfeldAusgabe.Text = "In dieser Region gibt es keine Tiere!";
                }
            }
            else
            {
                this.Fischen_TextfeldAusgabe.Text = "Auswahl wurde nicht korrekt durchgeführt und Fallenstellen ist daher nicht möglich. Wurde die Region gewählt?";
            }
        }
        #endregion

        #region About this Tool - Disclaimer und Kontakt
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = "www.ulisses-spiele.de";

            if (null != target && target.StartsWith("www"))
            {
                System.Diagnostics.Process.Start(target);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = "www.mprim.homeip.net/einsteins-dsa-tool/";

            if (null != target && target.StartsWith("www"))
            {
                System.Diagnostics.Process.Start(target);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = "mailto:dsatool@gmx.de";

            if (null != target && target.StartsWith("mailto:"))
            {
                System.Diagnostics.Process.Start(target);
            }
        }
        #endregion
    }
}