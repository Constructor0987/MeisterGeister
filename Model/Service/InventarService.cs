using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Using
using VM = MeisterGeister.ViewModel.Inventar.Inventar;

namespace MeisterGeister.Model.Service {
    public class InventarService : ServiceBase {

        #region //EIGENSCHAFTEN

        public List<Model.Ausrüstung> AusruestungListe {
            get { return Liste<Ausrüstung>(); }
        }
        public List<Model.Held_Ausrüstung> HeldZuAusruestungListe {
            get { return Liste<Held_Ausrüstung>(); }
        }
        public List<Model.Waffe> WaffeListe {
            get { return Liste<Waffe>(); }
        }

        public List<Model.Schild> SchildListe {
            get { return Liste<Schild>(); }
        }

        public List<Model.Fernkampfwaffe> FernkampfwaffeListe {
            get { return Liste<Fernkampfwaffe>(); }
        }

        public List<Model.Rüstung> RuestungListe {
            get { return Liste<Rüstung>(); }
        }
        public List<Model.Trageort> TrageortListe {
            get { return Liste<Trageort>(); }
        }

        #endregion

        #region //KONSTRUKTOR

        public InventarService() {
        }

        #endregion

        #region //DATENBANKABFRAGEN

        #region //--VM.ITEM_LOADBYHELD

        public List<Held_Ausrüstung> LoadAusruestungByHeld(Held aHeld) {
            return Context.Held_Ausrüstung.Where(hw => hw.HeldGUID == aHeld.HeldGUID).ToList();
        }

        #endregion

        #region //--INSERT

        public bool InsertHeldAusruestung(Held_Ausrüstung aHA) {
            try {
                return base.Insert<Held_Ausrüstung>(aHA);
            } catch (Exception) {
                return false;
            }            
        }

        #endregion

        #region //--REMOVE

        public bool RemoveAusruestungVonHeld(Model.Held_Ausrüstung aHA) {
            try {
                return base.Delete<Held_Ausrüstung>(aHA);
            } catch (Exception) {
                return false;
            }    
        }

        #endregion

        #region //--UPDATE

        public bool UpdateHeldAusruestung(Model.Held_Ausrüstung aHA) {
            try {
                return base.Update<Held_Ausrüstung>(aHA);
            } catch (Exception) {
                return false;
            }    
        }

        #endregion

        #endregion

    }
}
