using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;
using System.Diagnostics;
using Q42.HueApi;
using System.Windows.Media;

namespace MeisterGeister.Model.Service {
    public class HUEService : ServiceBase {

        public class LightColor
        {
            public Light light { get; set; }
            public Color color { get; set; }
        }
        public class HUESzene
        {
            public string Name { get; set; }
            private List<LightColor> _lstLightColor = new List<LightColor>();
            public List<LightColor> lstLightColor
            {
                get { return _lstLightColor; }
                set { _lstLightColor = value; }
            }
        }


        #region //----- EIGENSCHAFT ----

        public List<Model.HUE_Szene> SzenenListe
        {
            get { return Liste<HUE_Szene>(); }
        }

        public List<Model.HUE_LampeColor> LampeColorListe
        {
            get { return Liste<HUE_LampeColor>(); }
        }


        #endregion

        #region //----- KONSTRUKTOR ----

        public HUEService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        public HUE_Szene LoadSzeneByGUID(Guid HUE_SzeneGUID)
        {
            HUE_Szene tmp = Context.HUE_Szene
                .FirstOrDefault(pt => pt.HUE_SzeneGUID == HUE_SzeneGUID);
            return tmp;
        }

        public List<HUE_Szene> GetALLSzenen()
        {
            return SzenenListe;
        }

        public bool AddSzene(string Szenenname, out HUE_Szene aSzene)
        {
            aSzene = New<HUE_Szene>();
            aSzene.Name = Szenenname;
            //insert, update renew
                        
            return Insert<HUE_Szene>(aSzene);
        }

        public HUE_LampeColor AddLampenColorToSzene(string aName, string aColor)
        {
            HUE_LampeColor aLampecolor = new HUE_LampeColor();
     //       aLampecolor.HUE_LampeColorGUID = Guid.NewGuid();
            aLampecolor.Lampenname = aName;
            aLampecolor.Color = aColor;

            Insert<HUE_LampeColor>(aLampecolor);
            Update<HUE_LampeColor>(aLampecolor);
            return aLampecolor;
        }

        // Add & Remove

        //public List<HUE_Szene_LampeColor> LoadLampeColor_BySzene(HUE_Szene aSzene, Audio_Titel aTitel)
        //{
        //    var tmp = aPlaylist.Audio_Playlist_Titel
        //        .Where(pt => pt.Audio_TitelGUID == aTitel.Audio_TitelGUID)
        //            .ToList();
        //    return tmp;
        //}

        //public bool AddTitelToPlaylist(Audio_Playlist aPlaylist, Audio_Titel aTitel)
        //{
        //    Audio_Playlist_Titel tmp;
        //    return AddTitelToPlaylist(aPlaylist, aTitel, out tmp);
        //}


        #endregion

    }
}
