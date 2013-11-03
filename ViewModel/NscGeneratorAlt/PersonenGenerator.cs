using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;

namespace MeisterGeister.ViewModel.PersonenGenerator
{
    class PersonenGenerator
    {
        #region //SUBKLASSEN

        public class Person
        {
            //wichtige Eigenschaften
            
            private string geschlecht;
            private Model.Rasse rasse;//diarium
            private Model.Kultur kultur;//diarium
            //private string rassenVariante;
            //private string kulturVariante;
            private string profession;//diarium  //1001NSC
            private string altersklasse; // wege des meisters S173ff //1001NSC //evtl. Mod draus machen, bzw. altersgrenzen.. in % für rassenanpassung
            private int alter;//1001NSC //diarium
            private int groesse; // in Zentimeter //1001NSC //diarium // wege des meisters S173ff
            private int gewicht;// in Stein // wege des meisters S173ff //diarium
            private string augenfarbe;//1001NSC //diarium 
            private string haarfarbe;//1001NSC //diarium 
            private string haartracht;//1001NSC
            private string bart;//1001NSC

          
            // wege des meisters S173ff
            private string[] besEigenschaften;
            private string[] besAussehen;
            private string[] behinderungen;
            private string[] schlAngewohnheiten;
            private string[] launenUeigenschaften;
            private string groessenMod;
            private string gewichtMod;

            private string[] sonstigeBesonderheiten;

            //diarium
            private string[] auessereErscheinungen;
            private string[] charaktereigenschaften;
            private string aktVerfassung;
            private string gespraechsthema;
            //1001NSC
            private string[] koerperbau;//wie auessereErscheinungen

            private string gesichtsmerkmale;
            private string stand;
            private string kompetenz;
            private string besonderheiten;
       
            private bool magiebegabt;
            private string[] spezialgebiete;
            private string schwesternschaft;
            // handwerker können weiter aufgeteilt werden S.64
            //Magiebegabte bekommen spezialgebiet(e) S76
            // Hexen bekommen eine Schwesternschaft (&Vertrauten) S80
            // evtl. aufgliederung nach Kathegorien
            private W20 w20 = new W20();
            private W6 w6 = new W6();
            private static readonly Random ZahlenGenerator = new Random();

            public Person(string geschlecht, string alter, string rasse, string kultur, string profession)
            {
    //            setGeschlecht(geschlecht);
     //           this.rasse = new NscService().getRassenVarianteRandom(rasse);
                genName();
//                genAltersklasse();
                genAlter();
                genGroesseUndGewicht();
                //Haarfarbe nach Alter (&Rasse) grau meliert...
            }

 

            private void genName()
            {
            }

            private void genGroesseUndGewicht()
            {
                this.groesse = (int)this.rasse.Größe;
                foreach (string wuerfel in this.rasse.GrößeMod.Split('+').ToList())
                {
                    // TODO remove as soon as DB is clean
                    int count;
                    if (wuerfel.Split('W').ToList()[0] == "") count = 1;
                    else count = Convert.ToInt16(wuerfel.Split('W').ToList()[0]);
                    //----
                    for (int i = 0; i < count; i++)
                    {
                        if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 3) this.groesse += (int)(new W3().Würfeln());
                        else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 6) this.groesse += (int)(new W6().Würfeln());
                        else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 10) this.groesse += (int)(new W10().Würfeln());
                        else if (Convert.ToInt16(wuerfel.Split('W').ToList()[1]) == 20) this.groesse += (int)(new W20().Würfeln());
                        else throw new NotImplementedException();
                    };
                };
                /* switch (this.rasse)
                 {
                     case "Mittelländer": this.groesse = 160 + (int)this.w20.Würfeln() + (int)this.w20.Würfeln();
                         this.gewicht = this.groesse - 100; break;
                     case "Norbarde": this.groesse = 158 + (int)this.w20.Würfeln() + (int)this.w20.Würfeln();
                         this.gewicht = this.groesse - 100; break;
                     case "Thorwaler": this.groesse = 168 + (int)this.w20.Würfeln() + (int)this.w20.Würfeln();
                         this.gewicht = this.groesse - 95; break;
                     case "Tulamide": this.groesse = 155 + (int)this.w20.Würfeln() + (int)this.w20.Würfeln();
                         this.gewicht = this.groesse - 105; break;
                     case "Halbelf": this.groesse = 158 + (int)this.w20.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln();
                         this.gewicht = this.groesse - 120; break;
                     case "Nivese": this.groesse = 155 + (int)this.w20.Würfeln() + (int)this.w20.Würfeln();
                         this.gewicht = this.groesse - 110; break;
                     case "Trollzacker": this.groesse = 195 + (int)this.w20.Würfeln();
                         this.gewicht = this.groesse - 100; break;
                     case "Waldmensch": this.groesse = 152 + (int)this.w6.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln();
                         this.gewicht = this.groesse - 110; break;
                     case "Utulu": this.groesse = 165 + (int)this.w20.Würfeln() + (int)this.w20.Würfeln();
                         this.gewicht = this.groesse - 110; break;
                     case "Zwerg": this.groesse = 128 + (int)this.w6.Würfeln() + (int)this.w6.Würfeln();
                         this.gewicht = this.groesse - 80; break;
                     case "Ork":
                     case "Halbork": this.groesse = 160 + (int)this.w6.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln();
                         this.gewicht = this.groesse - 100; break;
                     case "Goblin": this.groesse = 135 + (int)this.w6.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln() + (int)this.w6.Würfeln();
                         this.gewicht = this.groesse - 100; break;
                     case "Achaz":
                     case "Elf": break;
                     default: throw new NotImplementedException();
                 }*/
                if (this.altersklasse == "Kind")
                {
                    this.groesse = 50 + (this.groesse - 50) / 12 * this.alter;
                    this.gewicht = 4 + (this.groesse - 4) / 22 * this.alter;
                }
            }


            private void genAlter()
            {
                //TODO wahrscheinlichkeit für alter (weniger senioren, etc.)
                switch (this.rasse.Name)
                {
                    case "Mittelländer":
                    case "Norbarde":
                    case "Thorwaler":
                    case "Tulamide":
                    case "Halbelf": switch (this.altersklasse)
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
                    case "Utulu": switch (this.altersklasse)
                        {
                            case "Kind": this.gen(1, 10); break;
                            case "Jugendlich": this.gen(11, 15); break;
                            case "Erwachsen": this.gen(16, 50); break;
                            case "Senior": this.gen(51, 95); break;
                            default: throw new NotImplementedException();
                        } break;
                    case "Zwerg": switch (this.altersklasse)
                        {
                            case "Kind": this.gen(1, 20); break;
                            case "Jugendlich": this.gen(21, 35); break;
                            case "Erwachsen": this.gen(35, 300); break;
                            case "Senior": this.gen(301, 500); break;
                            default: throw new NotImplementedException();
                        } break;
                    case "Ork": switch (this.altersklasse)
                        {
                            case "Kind": this.gen(1, 10); break;
                            case "Jugendlich": this.gen(11, 16); break;
                            case "Erwachsen": this.gen(17, 35); break;
                            case "Senior": this.gen(36, 45); break;
                            default: throw new NotImplementedException();
                        } break;
                    case "Halbork": switch (this.altersklasse)
                        {
                            case "Kind": this.gen(1, 10); break;
                            case "Jugendlich": this.gen(11, 16); break;
                            case "Erwachsen": this.gen(17, 45); break;
                            case "Senior": this.gen(46, 60); break;
                            default: throw new NotImplementedException();
                        } break;
                    case "Goblin": switch (this.altersklasse)
                        {
                            case "Kind": this.gen(1, 8); break;
                            case "Jugendlich": this.gen(9, 12); break;
                            case "Erwachsen": this.gen(13, 28); break;
                            case "Senior": this.gen(29, 40); break;
                            default: throw new NotImplementedException();
                        } break;
                    case "Achaz": switch (this.altersklasse)
                        {
                            case "Kind": this.gen(1, 14); break;
                            case "Jugendlich": this.gen(15, 20); break;
                            case "Erwachsen": this.gen(21, 120); break;
                            case "Senior": this.gen(121, 180); break;
                            default: throw new NotImplementedException();
                        } break;
                    case "Elf": switch (this.altersklasse)
                        {
                            case "Kind": this.gen(1, 18); break;
                            case "Jugendlich": this.gen(19, 35); break;
                            case "Erwachsen": this.gen(36, 600); break;
                            case "Senior": this.gen(601, 1000); break;
                            default: throw new NotImplementedException();
                        } break;
                    default: throw new NotImplementedException();

                }

            }
            private void gen(int min, int max)
            {
                this.alter = ZahlenGenerator.Next(max - min) + min;
            }
  
 
        }


 

        #endregion

        #region //FELDER
        // intern
        //UI
        //Entitylisten
        //Zuordnungen
        //Commands
        #endregion

        #region //KONSTRUKTOR

        public PersonenGenerator(){
  
        }
        #endregion

        #region //INSTANZMETHODEN


        #endregion

        #region //EVENTS
        #endregion


    }
}
