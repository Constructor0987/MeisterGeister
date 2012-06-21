using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Weitere Usings
using System.ComponentModel;
//Eigene Usings
using M = MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Helden.Controls {
    public class TalentViewModel : Base.ViewModelBase {
        
        #region //FELDER
        //Listen
        private List<Model.Talent> talentListe = new List<Model.Talent>();
        private List<Model.Talent> heldTalentListe = new List<Model.Talent>();
        //Selection
        private Model.Held selectedHeld = new M.Held();
        #endregion
        
        #region //EIGENSCHAFTEN
        //Listen
        public List<Model.Talent> TalentListe { get { return talentListe; } set { talentListe = value; OnChanged("TalentListe"); } }
        public List<Model.Talent> HeldTalentListe { get { return heldTalentListe; } set { heldTalentListe = value; OnChanged("HeldTalentListe"); } }
        //Selection
        public Model.Held SelectedHeld { get { return selectedHeld; } set { selectedHeld = value; OnChanged("SelectedHeld"); } }        
        #endregion
        
        #region //KONSTRUKTOR
        public TalentViewModel() {
            //Auf Event HeldWechsel aus Global registrieren
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };
            SelectedHeld = Global.SelectedHeld;
        }
        #endregion
        
        #region //METHODEN
        public void Init() {
            //All-Add
            TalentListe = Global.ContextTalent.TalentListe
                .Where(item => item.TalentgruppeID == 1).ToList();

            //TODO DW: Wenn Heldenliste wieder bedienbar, entfernen
            //DemoType  
            if (SelectedHeld != null) {
                SelectedHeld = new M.Held() { Name = "Mein Held" };
                SelectedHeld.Held_Talent.Add(new M.Held_Talent() { Talentname = "Hiebwaffen", TaW = 4, HeldGUID = Global.SelectedHeldGUID, ZuteilungAT = 3, ZuteilungPA = 1 });
                SelectedHeld.Held_Talent.Add(new M.Held_Talent() { Talentname = "Raufen", TaW = 9, HeldGUID = Global.SelectedHeldGUID, ZuteilungAT = 5, ZuteilungPA = 4 });
                SelectedHeld.Held_Talent.Add(new M.Held_Talent() { Talentname = "Speere", TaW = 10, HeldGUID = Global.SelectedHeldGUID, ZuteilungAT = 6, ZuteilungPA = 4 });                
                
                //Held
                HeldTalentListe = Global.ContextTalent.TalentListe
                    .Where(item => SelectedHeld.Held_Talent
                        .Select(value => value.Talentname).Contains(item.Talentname)
                        && item.TalentgruppeID == 1).ToList();
            }
        }
        #endregion
        #region //EVENTS
        //Event
        void SelectedHeldChanged() {
            SelectedHeld = Global.SelectedHeld;
        }
        #endregion
    }
}
