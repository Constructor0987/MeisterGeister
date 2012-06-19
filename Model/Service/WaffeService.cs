using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;
using System.Diagnostics;

namespace MeisterGeister.Model.Service {
    public class WaffeService : ServiceBase {

        #region //----- EIGENSCHAFT ----

        public List<Model.Waffe> WaffeListe {
            get { return Liste<Waffe>(); }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public WaffeService() {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        public List<Model.Waffe> LoadNahkampfwaffeByHeld(Held aHeld) {
            List<Waffe> tmp =
                Context.Waffe.SelectMany(w => 
                    w.Ausrüstung.Held_Ausrüstung).Where(hw => 
                        hw.HeldGUID == aHeld.HeldGUID).Select(hw => 
                            hw.Ausrüstung.Waffe).ToList();
            return tmp;
        }

        public List<Model.Fernkampfwaffe> LoadFernkampfwaffeByHeld(Held aHeld) {
            List<Fernkampfwaffe> tmp =
                Context.Fernkampfwaffe.SelectMany(w => 
                    w.Ausrüstung.Held_Ausrüstung).Where(hw => 
                        hw.HeldGUID == aHeld.HeldGUID).Select(hw => 
                            hw.Ausrüstung.Fernkampfwaffe).ToList();
            return tmp;
        }

        public bool InsertHeldWaffe(Model.Held aHeld, Model.Waffe aWaffe) {
            Held_Ausrüstung tmp = New<Held_Ausrüstung>();
            tmp.Held = aHeld;
            tmp.HeldGUID = aHeld.HeldGUID;

            tmp.Ausrüstung = aWaffe.Ausrüstung;
            tmp.AusrüstungGUID = aWaffe.Ausrüstung.AusrüstungGUID;

            tmp.Talentname = aWaffe.Talent.FirstOrDefault().Talentname;
            tmp.Angelegt = false;
            tmp.Trageort = Global.ContextInventar.TrageortListe.Where(item => item.Name == "Rucksack").FirstOrDefault();
            tmp.Anzahl = 1;
            

            return Insert<Held_Ausrüstung>(tmp);
        }        
       
        #endregion

    }
}