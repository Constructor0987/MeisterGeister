using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Using
using VM = MeisterGeister.ViewModel.Inventar.InventarViewModel;

namespace MeisterGeister.Model.Service
{
    public class InventarService : ServiceBase
    {

        #region //EIGENSCHAFTEN

        public List<Model.Ausrüstung> AusruestungListe
        {
            get { return Context.Ausrüstung.Where(s => s.Ausrüstung_Setting.Any(a_s => a_s.Setting.Aktiv)).ToList(); }
        }
        public List<Model.Held_Ausrüstung> HeldZuAusruestungListe
        {
            get { return Liste<Held_Ausrüstung>(); }
        }
        public List<Model.Waffe> WaffeListe
        {
            get { return Context.Waffe.Where(s => s.Ausrüstung.Ausrüstung_Setting.Any(a_s => a_s.Setting.Aktiv)).ToList(); }
        }

        public List<Model.Schild> SchildListe
        {
            get
            {
                return Context.Schild.Where(s => s.Ausrüstung.Ausrüstung_Setting.Any(a_s => a_s.Setting.Aktiv)).ToList();
            }
        }

        public List<Model.Fernkampfwaffe> FernkampfwaffeListe
        {
            get { return Context.Fernkampfwaffe.Where(s => s.Ausrüstung.Ausrüstung_Setting.Any(a_s => a_s.Setting.Aktiv)).ToList(); }
        }

        public List<Model.Rüstung> RuestungListe
        {
            get { return Context.Rüstung.Where(s => s.Ausrüstung.Ausrüstung_Setting.Any(a_s => a_s.Setting.Aktiv)).ToList(); }
        }

        public List<Model.Munition> MunitionListe
        {
            get { return Context.Munition.Where(t => Setting.AktiveSettings.Any(s => (t.Setting ?? "Aventurien").Contains(s.Name))).ToList(); }
        }

        public List<Model.Trageort> TrageortListe
        {
            get { return Liste<Trageort>(); }
        }

        public List<Model.Held_Inventar> HeldZuInventarListe
        {
            get { return Liste<Held_Inventar>(); }
        }

        #endregion

        #region //KONSTRUKTOR

        public InventarService()
        {
        }

        #endregion

        #region //DATENBANKABFRAGEN

        #region //--VM.ITEM_LOADBYHELD

        public List<Held_Ausrüstung> LoadAusruestungByHeld(Held aHeld)
        {
            return Context.Held_Ausrüstung.Where(hw => hw.HeldGUID == aHeld.HeldGUID).ToList();
        }

        #endregion

        #region //--INSERT

        public bool InsertHeldAusruestung(Held_Ausrüstung aHA)
        {
            try
            {
                return base.Insert<Held_Ausrüstung>(aHA);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region //--REMOVE

        public bool RemoveAusruestungVonHeld(Model.Held_Ausrüstung aHA)
        {
            try
            {
                return base.Delete<Held_Ausrüstung>(aHA);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveInventarVonHeld(Model.Held_Inventar aHI)
        {
            try
            {
                return base.Delete<Held_Inventar>(aHI);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region //--UPDATE

        public bool UpdateHeldAusruestung(Model.Held_Ausrüstung aHA)
        {
            try
            {
                return base.Update<Held_Ausrüstung>(aHA);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateHeldInventar(Model.Held_Inventar aHI)
        {
            try
            {
                return base.Update<Held_Inventar>(aHI);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #endregion

    }
}
