using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using System.Windows;

namespace MeisterGeister.ViewModel.NscGenerator
{
    class Person : Base.ViewModelBase
    {
        #region //SUBKLASSEN
        #endregion

        #region //FELDER
        //intern
        //wichtige Eigenschaften
        private string _name { get; set; }
        private string _nameBedeutung { get; set; }
        private string _geschlecht { get; set; }
        private Model.Rasse _rasse { get; set; }
        private Model.Kultur _kultur{ get; set; }//diarium
        private int _kampffähigkeit { get; set; }//1001NSC
        private int _kompetenz { get; set; }//1001NSC
        private int _offenheit { get; set; }//1001NSC
        private int _extraversion { get; set; }//1001NSC
        private int _gewissenhaftigkeit { get; set; }//1001NSC
        private int _verträglichkeit { get; set; }//1001NSC
        private int _neurotizismus { get; set; }//1001NSC
        //TODO ??: Nie verwendet: private string profession;//diarium  //1001NSC
        private string _stand { get; set; }//1001NSC
        private string _altersklasse { get; set; } // wege des meisters S173ff //1001NSC //evtl. Mod draus machen, bzw. altersgrenzen.. in % für rassenanpassung
        private int _alter { get; set; }//1001NSC //diarium
        private int _größe { get; set; }// in Zentimeter //1001NSC //diarium // wege des meisters S173ff
        private string _größeMod;
        private string _gewicht { get; set; }
        private string _augenfarbe { get; set; } //1001NSC //diarium 
        private string _haarfarbe { get; set; } //1001NSC //diarium 
        private string _aussehen { get; set; } //1001NSC // wege des meisters S173ff
        private string _charakter { get; set; } //diarium
        private string _verhaltenUndDarstellung { get; set; }//diarium//1001NSC
        private string _soziales { get; set; }
        private string _historie { get; set; }
        private string _besonderes { get; set; } //1001NSC // wege des meisters S173ff
        private string _vorlieben { get; set; }

        // wege des meisters S173ff
        //TODO ??: Nie verwendet: private string[] besEigenschaften;
        //TODO ??: Nie verwendet: private string[] schlAngewohnheiten;
        //TODO ??: Nie verwendet: private string[] launenUeigenschaften;
        //TODO ??: Nie verwendet: private string groessenMod;
        //diarium
        //TODO ??: Nie verwendet: private string aktVerfassung;
        //TODO ??: Nie verwendet: private string gespraechsthema;
        //1001NSC  
        //private Persoenlichkeitsdimension persoenlichkeitsdimension;
        //TODO ??: Nie verwendet: private bool magiebegabt;
        //TODO ??: Nie verwendet: private string[] spezialgebiete;
        //TODO ??: Nie verwendet: private string schwesternschaft;







        //UI
        //Entitylisten
        //Zuordnungen
        //Commands
        #endregion

        #region //EIGENSCHAFTEN
        //Intern
        public string Name { get { return _name; } set { _name = value; OnChanged("Name"); } }
        public string NameBedeutung { get { return _nameBedeutung; } set { _nameBedeutung = value; OnChanged("NameBedeutung"); } }
        public string Geschlecht { get { return _geschlecht; } set { _geschlecht = value; OnChanged("Geschlecht"); } }
        public string GeschlechtView { get { if (_geschlecht == "w") return "/Images/Icons/geschlecht_w.png"; else return "/Images/Icons/geschlecht_m.png"; } set { _geschlecht = value; OnChanged("Geschlecht"); } }
        public Model.Rasse Rasse { get { return _rasse; } set { _rasse = value; OnChanged("Rasse"); } }
        public Model.Kultur Kultur { get { return _kultur; } set { _kultur = value; OnChanged("Kultur"); } }
        public string Stand { get { return _stand; } set { _stand = value; OnChanged("Stand"); } }
        public string Altersklasse { get { return _altersklasse; } set { _altersklasse = value; OnChanged("Altersklasse"); } }
        public int Alter { get { return _alter; } set { _alter = value; OnChanged("Alter"); } }
        public string GrößeMod { get { return _größeMod; } set { _größeMod = value; OnChanged("GrößeMod"); } } //in Schritt
        public double Größe { get { return (double)_größe / 100; } set { _größe = (int)value; OnChanged("Größe"); } } //in Schritt
        public int GrößeFinger { get { return (int)_größe / 2; } set { _größe = value; OnChanged("Größe"); } }
        public string Gewicht { get { return _gewicht; } set { _gewicht = value; OnChanged("Gewicht"); } }
        public string Haarfarbe { get { return _haarfarbe; } set { _haarfarbe = value; OnChanged("Haarfarbe"); } }
        public string Augenfarbe { get { return _augenfarbe; } set { _augenfarbe = value; OnChanged("Augenfarbe"); } }
        public string Aussehen { get { return _aussehen; } set { _aussehen = value; OnChanged("Aussehen"); } }
        public string Charakter { get { return _charakter; } set { _charakter = value; OnChanged("Aussehen"); } }
        public string Soziales { get { return _soziales; } set { _soziales = value; OnChanged("Soziales"); } }
        public string Historie { get { return _historie; } set { _historie = value; OnChanged("Historie"); } }
        public string Vorlieben { get { return _vorlieben; } set { _vorlieben = value; OnChanged("Vorlieben"); } }
        public string Besonderes { get { return _besonderes; } set { _besonderes = value; OnChanged("Besonderes"); } }
        public string VerhaltenUndDarstellung { get { return _verhaltenUndDarstellung; } set { _verhaltenUndDarstellung = value; OnChanged("VerhaltenUndDarstellung"); } }
        public int Kompetenz { get { return _kompetenz; } set { _kompetenz = value; OnChanged("Kompetenz"); } }
        public int Kampffähigkeit { get { return _kampffähigkeit; } set { _kampffähigkeit = value; OnChanged("Kampffähigkeit"); } }
        public int Offenheit { get { return _offenheit; } set { _offenheit = value; OnChanged("Offenheit"); } }
        public int Gewissenhaftigkeit { get { return _gewissenhaftigkeit; } set { _gewissenhaftigkeit = value; OnChanged("Gewissenhaftigkeit"); } }
        public int Extraversion { get { return _extraversion; } set { _extraversion = value; OnChanged("Extraversion"); } }
        public int Verträglichkeit { get { return _verträglichkeit; } set { _verträglichkeit = value; OnChanged("Verträglichkeit"); } }
        public int Neurotizismus { get { return _neurotizismus; } set { _neurotizismus = value; OnChanged("Neurotizismus"); } }
        public Visibility IsNurName { get; set; }
      
        #endregion

        #region //KONSTRUKTOR

        public Person(string geschlecht, string alter, Model.Rasse rasse, Model.Kultur kultur, string profession, bool unüblicheKulturen, bool nurNamen)
        {
            setGeschlecht(geschlecht); 
            setAbstammung(Geschlecht, rasse, kultur, unüblicheKulturen);
            setName();
            setAltersklasse(alter);
            setAlter();
            if (!nurNamen)
            {
                setStand();
                setGröße();
                setGewicht();
                setHaarUndAugenfarbe(Rasse);
                //Aussehen
                setHaarUndBart();
                setGesicht();
                setKörperlicheEigenschaft();
                setBehinderung();
                setCharakter();
                setSoziales();
                setHistorie();
                setVorlieben();
                //Besonderes
                setBesonderes();
                setVerhaltenUndDarstellung();
            }
            setKompetenzUndKampffähigkeit();
            setPersönlichkeitsdimension();
            
        }

        private void setBesonderes()
        {
            //in 10% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 4) == 1)
            {
                Besonderes += Global.ContextNsc.getBesonderes();
            }
            else Besonderes = "-";
            
        }

        private void setVorlieben()
        {
            Vorlieben += Global.ContextNsc.getVorlieben();
        }

        private void setBehinderung()
        {
            //in 10% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 4) == 1)
            {
                Aussehen += Global.ContextNsc.getBehinderung() + ", ";
            }    
        }

        private void setKörperlicheEigenschaft()
        {
            //in 10% der Fälle
            if (RandomNumberGenerator.Generator.Next(1, 4) == 1)
            {
                Aussehen += "\n";
                Aussehen += Global.ContextNsc.getKörperlicheEigenschaft() + ", ";
            }        
        }

        private void setGesicht()
        {
            //in 30% der Fälle 1-2 Merkmale
            if (RandomNumberGenerator.Generator.Next(1, 4) == 1)
            {
                int ran = RandomNumberGenerator.Generator.Next(1, 3);
                Aussehen += "\n";
                for (int i = 0; i < ran; i++)
                {
                    Aussehen += Global.ContextNsc.getRandomGesicht()+", ";
                }                    
            }        
        }

        private void setHaarUndAugenfarbe(Model.Rasse rasse)
        {
            Haarfarbe = Global.ContextNsc.getHaarfarbe(rasse);
            Augenfarbe = Global.ContextNsc.getAugenfarbe(rasse);
        }

        #endregion

        #region //INSTANZMETHODEN

        private void setPersönlichkeitsdimension() {
            Offenheit = RandomNumberGenerator.Generator.Next(1, 7) + RandomNumberGenerator.Generator.Next(1, 7) + RandomNumberGenerator.Generator.Next(1, 7) - 3;
            Gewissenhaftigkeit = RandomNumberGenerator.Generator.Next(1, 7) + RandomNumberGenerator.Generator.Next(1, 7) + RandomNumberGenerator.Generator.Next(1, 7) - 3;
            Extraversion = RandomNumberGenerator.Generator.Next(1, 7) + RandomNumberGenerator.Generator.Next(1, 7) + RandomNumberGenerator.Generator.Next(1, 7) - 3;
            Verträglichkeit = RandomNumberGenerator.Generator.Next(1, 7) + RandomNumberGenerator.Generator.Next(1, 7) + RandomNumberGenerator.Generator.Next(1, 7) - 3;
            Neurotizismus = RandomNumberGenerator.Generator.Next(1, 7) + RandomNumberGenerator.Generator.Next(1, 7) + RandomNumberGenerator.Generator.Next(1, 7) - 3;
        }

        private void setKompetenzUndKampffähigkeit()
        {
            //TODO MP: entfernen, bzw. nur für professionen
            Kompetenz = RandomNumberGenerator.Generator.Next(1, 7) + RandomNumberGenerator.Generator.Next(1, 7) + RandomNumberGenerator.Generator.Next(1, 7)-3;
            switch (RandomNumberGenerator.Generator.Next(1, 7)){
                case 1: Kampffähigkeit=((Kompetenz - 2)<0) ?  0 :  Kompetenz - 2; break;
                case 2: Kampffähigkeit = ((Kompetenz - 1) < 0) ?  0 :  Kompetenz - 1; break;
                case 3: Kampffähigkeit = Kompetenz; break;
                case 4: Kampffähigkeit = Kompetenz; break;
                case 5: Kampffähigkeit = Kompetenz+1; break;
                case 6: Kampffähigkeit = Kompetenz+2; break;
            }            
        }

        public string toString()
        {
            return Name + " (" + Geschlecht +") \n"
                + "Alter: "+Alter + " Jahre (" + Altersklasse + ") " + "Stand: " + Stand + "\n"
                + "Rasse: "+Rasse.Name+", Kultur: "+Kultur.Name+", Profession: "+"\n"
                + "Größe: "+Größe+" ("+GrößeFinger+")"+", Gewicht: "+Gewicht+", Haarfarbe: "+Haarfarbe+", Augenfarbe: "+Augenfarbe+"\n"                
                + "Aussehen: "+Aussehen+"\n"
                + "Charakter: "+Charakter+"\n"
                + "Soziales: "+Soziales+"\n"
                + "Historie: "+Historie+"\n"
                + "Vorlieben: " +Vorlieben+ "\n"
                + "Besonderes: " +Besonderes+ "\n"
                +  "Darstellung: "+VerhaltenUndDarstellung+"\n"; 
        }

        private void setName()
        {
            string name = Global.ContextNamen.createName(Geschlecht, Kultur);
            Name = (name.Split('|'))[0];
            if ((name.Split('|')).Length>1) NameBedeutung = (name.Split('|'))[1];
        }

        private void setCharakter()
        {   
            Charakter = Global.ContextNsc.getCharakter();
            //1-3 weitere 
            int ran = RandomNumberGenerator.Generator.Next(1, 4);
            List<string> par = new List<string>();
            for (int i = 0; i < ran; i++)
            {
                string parr=Global.ContextNsc.getCharakter();
                if(!par.Contains(parr)){
                    par.Add(parr);
                }
            }
            for (int i = 0; i < par.Count; i++)
            {
                Charakter = Charakter +", "+ par[i];
            }
        }

        private void setHistorie(){
            if (Altersklasse != "Kind")
            {
                Historie = Global.ContextNsc.getHistorie();
                //0-1 weitere 
                int ran = RandomNumberGenerator.Generator.Next(0, 2);
                for (int i = 0; i < ran; i++)
                {
                    Historie = Historie + ", " + Global.ContextNsc.getHistorie();
                }
            }
            else Historie = "-";
        }

        private void setSoziales(){
            Soziales = Global.ContextNsc.getSoziales();
            //0-1 weitere 
            int ran = RandomNumberGenerator.Generator.Next(0, 2);
            List<string> par = new List<string>();
            for (int i = 0; i < ran; i++)
            {
                string parr = Global.ContextNsc.getCharakter();
                if (!par.Contains(parr))
                {
                    par.Add(parr);
                }
            }
            for (int i = 0; i < ran; i++)
            {
                Soziales = Soziales + ", " + par[i];
            }
        }

        private void setVerhaltenUndDarstellung(){
            VerhaltenUndDarstellung = Global.ContextNsc.getVerhaltenUndDarstellung();
        }

        private void setStand(){
            Stand = Global.ContextNsc.getStand();
        }

        private void setHaarUndBart()
        {
            if (!(Rasse.Name == "Achaz"))
            {
                Aussehen = Global.ContextNsc.getHaar(Geschlecht, Rasse);
                if (Altersklasse != "Kind") Aussehen = Aussehen + "; " + Global.ContextNsc.getBart(Geschlecht, Rasse);
                Aussehen = Aussehen + ", ";
            }
        }

        private void setGewicht()
        {
            int gewicht;
            if (Altersklasse == "Kind")
            {
                gewicht = 10 + ((int)(Größe * 100) - 10) / 11 * Alter;
            }
            else
            {
                if (Rasse.Name == "Grolm") gewicht = (int)(Größe * 100) /3;
                else gewicht = (int)(Größe * 100) + Rasse.Gewicht;
            }
            //Änderung an der Statur nach WdM
            int ran = RandomNumberGenerator.Generator.Next(1, 21);
            if (ran <= 1) Gewicht = (gewicht - (int)(gewicht * 0.3)).ToString() + " Stein ("+"mager"+")";
            else if (ran <= 3) Gewicht = (gewicht - (int)(gewicht * 0.2)).ToString() + " Stein (" + "sehr dünn" + ")";
            else if (ran <= 7) Gewicht = (gewicht - (int)(gewicht * 0.1)).ToString() + " Stein (" + "eher dünn" + ")";
            else if (ran <= 13) Gewicht = gewicht.ToString() + " Stein";
            else if (ran <= 17) Gewicht = (gewicht + (int)(gewicht * 0.1)).ToString() + " Stein (" + "dicklich" + ")";
            else if (ran <= 19) Gewicht = (gewicht + (int)(gewicht * 0.2)).ToString() + " Stein (" + "dick" + ")";
            else if (ran <= 20) Gewicht = (gewicht + (int)(gewicht * 0.3)).ToString() + " Stein (" + "fett" + ")";
        }

        private void setGröße()
        {
            Größe = Rasse.Größe;
            //Änderung an der Statur nach WdM
            int ran = RandomNumberGenerator.Generator.Next(1, 21);
            if (ran <= 1)
            {
                foreach (string wuerfel in Rasse.GrößeMod.Split('+').ToList())
                {
                    for (int i = 0; i < Convert.ToInt16(wuerfel.Split('W').ToList()[0]); i++)
                    {
                        if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 3) Größe = Rasse.Größe + 1;
                        else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 6) Größe = Rasse.Größe + 1;
                        else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 10) Größe = Rasse.Größe + 1;
                        else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 20) Größe = Rasse.Größe + 1;
                        else throw new NotImplementedException();
                    }
                }
                GrößeMod =" zwergenwuchs ";
            }
            else if (ran <= 19)
            {
                foreach (string wuerfel in Rasse.GrößeMod.Split('+').ToList())
                {
                    for (int i = 0; i < Convert.ToInt16(wuerfel.Split('W').ToList()[0]); i++)
                    {
                        if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 3) Größe = Rasse.Größe + RandomNumberGenerator.Generator.Next(1, 4);
                        else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 6) Größe = Rasse.Größe + RandomNumberGenerator.Generator.Next(1, 7);
                        else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 10) Größe = Rasse.Größe + RandomNumberGenerator.Generator.Next(1, 11);
                        else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 20) Größe = Rasse.Größe + RandomNumberGenerator.Generator.Next(1, 21);
                        else throw new NotImplementedException();
                    }
                }
            }
            else if (ran <= 20)
            {
                foreach (string wuerfel in Rasse.GrößeMod.Split('+').ToList())
                {
                    for (int i = 0; i < Convert.ToInt16(wuerfel.Split('W').ToList()[0]); i++)
                    {
                        if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 3) Größe = Rasse.Größe + 3;
                        else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 6) Größe = Rasse.Größe + 6;
                        else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 10) Größe = Rasse.Größe + 10;
                        else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 20) Größe = Rasse.Größe + 20;
                        else throw new NotImplementedException();
                    }
                }
                GrößeMod = " hühnenhaft ";
            }
          
            if (Altersklasse == "Kind")
            {
                Größe = 75 + (Größe - 75) / 12 * Alter;
            }
        }

        private void setAlter()
        {
            switch (Rasse.Name)
            {
                case "Mittelländer":
                case "Norbarde":
                case "Thorwaler":
                case "Tulamide":
                case "Halbelf": switch (Altersklasse)
                    {
                        case "Kind": this.gen(1, 11); break;
                        case "Jugendlich": this.gen(12, 16); break;
                        case "Erwachsen": this.gen(17, 50); break;
                        case "Senior": this.gen(51, 85); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Nivese":
                case "Trollzacker":
                case "Waldmensch":
                case "Utulu": switch (Altersklasse)
                    {
                        case "Kind": this.gen(1, 11); break;
                        case "Jugendlich": this.gen(12, 15); break;
                        case "Erwachsen": this.gen(16, 50); break;
                        case "Senior": this.gen(51, 95); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Grolm": switch (Altersklasse)
                    {
                        case "Kind": this.gen(1, 3); break;
                        case "Jugendlich": this.gen(4, 10); break;
                        case "Erwachsen": this.gen(11, 25); break;
                        case "Senior": this.gen(26, 30); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Zwerg": switch (Altersklasse)
                    {
                        case "Kind": this.gen(1, 11); break;
                        case "Jugendlich": this.gen(12, 35); break;
                        case "Erwachsen": this.gen(35, 300); break;
                        case "Senior": this.gen(301, 500); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Ork": switch (Altersklasse)
                    {
                        case "Kind": this.gen(1, 11); break;
                        case "Jugendlich": this.gen(12, 16); break;
                        case "Erwachsen": this.gen(17, 35); break;
                        case "Senior": this.gen(36, 45); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Halbork": switch (Altersklasse)
                    {
                        case "Kind": this.gen(1, 11); break;
                        case "Jugendlich": this.gen(12, 16); break;
                        case "Erwachsen": this.gen(17, 45); break;
                        case "Senior": this.gen(46, 60); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Goblin": switch (Altersklasse)
                    {
                        case "Kind": this.gen(1, 11); break;
                        case "Jugendlich": this.gen(12, 13); break;
                        case "Erwachsen": this.gen(14, 28); break;
                        case "Senior": this.gen(29, 40); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Achaz": switch (Altersklasse)
                    {
                        case "Kind": this.gen(1, 11); break;
                        case "Jugendlich": this.gen(12, 20); break;
                        case "Erwachsen": this.gen(21, 120); break;
                        case "Senior": this.gen(121, 180); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Elf": switch (Altersklasse)
                    {
                        case "Kind": this.gen(1, 11); break;
                        case "Jugendlich": this.gen(12, 35); break;
                        case "Erwachsen": this.gen(36, 600); break;
                        case "Senior": this.gen(601, 1000); break;
                        default: throw new NotImplementedException();
                    } break;
                case "Troll": switch (Altersklasse)
                    {
                        case "Kind": this.gen(1, 11); break;
                        case "Jugendlich": this.gen(12, 35); break;
                        case "Erwachsen": this.gen(36, 350); break;
                        case "Senior": this.gen(351, 400); break;
                    } break;
                default: throw new NotImplementedException();

            }

        }

        private void gen(int min, int max)
        {
            Alter = RandomNumberGenerator.Generator.Next(max - min) + min;
        }

        private void setAltersklasse(string alter)
        {
            if (alter != "kein Alter") Altersklasse = alter;
            else
            {
                int tmp = RandomNumberGenerator.Generator.Next(1, 20);
                if (tmp <= 2) Altersklasse = "Kind";
                else if (tmp <= 6) Altersklasse = "Jugendlich";
                else if (tmp <= 18) Altersklasse = "Erwachsen";
                else if (tmp <= 20) Altersklasse = "Senior";
            }
        }

        private void setAbstammung(string geschlecht, Model.Rasse rasse, Model.Kultur kultur, bool unüblicheKulturen)
        {
            //if (!(Geschlecht == "m" && kultur.Variante.EndsWith("-Frau") || Geschlecht == "w" && kultur.Variante.EndsWith("-Mann")))
            
            if (rasse.Name == "keine Rasse")
            {
                if (kultur.Name == "keine Kultur") // gen Rasse() -> gen kultur(Rasse);
                {
                    List<Model.Rasse> rassen = Global.ContextNsc.getRasseVarianten(geschlecht);
                    Rasse = rassen[RandomNumberGenerator.Generator.Next(rassen.Count)];
                    List<Model.Kultur> kulturen = Global.ContextNsc.getKulturVariantenByRasseVariante(Rasse);
                    Kultur = kulturen[RandomNumberGenerator.Generator.Next(kulturen.Count)];
                }
                else // GenKultur() -> genRasse(kultur);
                {
                    List<Model.Kultur> kulturen = Global.ContextNsc.getKulturVariantenByKultur(kultur);
                    Kultur = kulturen[RandomNumberGenerator.Generator.Next(kulturen.Count)];
                    List<Model.Rasse> rassen = Global.ContextNsc.getRasseVariantenByKultur(geschlecht, kultur);
                    Rasse = rassen[RandomNumberGenerator.Generator.Next(rassen.Count)];
                }
            }
            else
            {
                if (kultur.Name == "keine Kultur") // gen Rasse(Rasse) -> gen kultur(Rasse);
                {
                    List<Model.Rasse> rassen = Global.ContextNsc.getRasseVariantenByRasse(rasse);
                    Rasse = rassen[RandomNumberGenerator.Generator.Next(rassen.Count)];
                    List<Model.Kultur> kulturen = Global.ContextNsc.getKulturVariantenByRasseVariante(Rasse);
                    Kultur = kulturen[RandomNumberGenerator.Generator.Next(kulturen.Count)];
                }
                else // Rasse = rasse -> genKultur(kultur);
                {
                    Rasse = rasse;
                    List<Model.Kultur> kulturen = Global.ContextNsc.getKulturVariantenByKultur(kultur);
                    Kultur = kulturen[RandomNumberGenerator.Generator.Next(kulturen.Count)];
                }
            }
            //TODO MP: remove Quickfix
           if ((Rasse.Name == "Goblin" || Rasse.Name=="Ork")&&Rasse.Variante.EndsWith("-Mann")) Geschlecht = "m";
           else if ((Rasse.Name == "Goblin" || Rasse.Name == "Ork") && Rasse.Variante.EndsWith("-Frau")) Geschlecht = "w";
        }

        private void setGeschlecht(string geschlecht)
        {
            if (geschlecht == "m/w")
            {
                if (RandomNumberGenerator.Generator.Next(1, 3) == 1) Geschlecht = "w";
                else Geschlecht = "m";
            }
            else Geschlecht = geschlecht;
        }

        #endregion

        #region //EVENTS

        #endregion




    }
}
