using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;
using System.Diagnostics;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.Model.Service {
    public class AudioHotkeyWesenService : ServiceBase {

        #region //----- EIGENSCHAFT ----

        public List<Model.Audio_HotkeyWesen> PlaylistHotkeyWesenListe
        {
            get { return Liste<Audio_HotkeyWesen>(); }
        }

        public List<Model.Audio_HotkeyWesen> PlaylistHotkeyHeldListe
        {
            get { return PlaylistHotkeyWesenListe.Where(t => t.Held != null).ToList(); }
        }

        public List<Model.Audio_HotkeyWesen> PlaylistHotkeyGegnerListe
        {
            get { return PlaylistHotkeyWesenListe.Where(t => t.GegnerBase != null).ToList(); }
        }
        

        #endregion

        #region //----- KONSTRUKTOR ----

        public AudioHotkeyWesenService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----


        public List<Audio_HotkeyWesen> GetHeldAktivHotkeyListe(Audio_Playlist aPlaylist)
        {
            return PlaylistHotkeyHeldListe.Where(tt => tt.Audio_PListGUID == aPlaylist.Audio_PlaylistGUID).ToList(); 
        }
                
        public List<Audio_HotkeyWesen> GetGegnerAktivHotkeyListe(Audio_Playlist aPlaylist)
        {
            return PlaylistHotkeyGegnerListe.Where(tt => tt.Audio_PListGUID == aPlaylist.Audio_PlaylistGUID).ToList(); 
        }

        public List<Audio_HotkeyWesen> GetWesenAktivHotkeyListe(Audio_Playlist aPlaylist)
        {
            return Liste<Audio_HotkeyWesen>().Where(t => t.Audio_PListGUID == aPlaylist.Audio_PlaylistGUID).ToList(); 
        }

       
        #endregion
        
    }
}