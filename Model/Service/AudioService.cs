using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Eigene Usings
using Model = MeisterGeister.Model;
using System.Diagnostics;

namespace MeisterGeister.Model.Service {
    public class AudioService : ServiceBase {

        #region //----- EIGENSCHAFT ----

        public List<Model.Audio_Playlist> PlaylistListe
        {
            get { return Liste<Audio_Playlist>(); }
        }

        public List<Model.Audio_Titel> TitelListe
        {
            get { return Liste<Audio_Titel>(); }
        }

        public List<Model.Audio_Playlist_Titel> PlaylistTitelListe
        {
            get { return Liste<Audio_Playlist_Titel>(); }
        }

        #endregion

        #region //----- KONSTRUKTOR ----

        public AudioService()
        {
        }

        #endregion

        #region //----- DATENBANKABFRAGEN ----

        public List<Audio_Titel> LoadTitelByPlaylist(Audio_Playlist aPlaylist)
        {
            List<Audio_Titel> tmp = Context.Audio_Playlist_Titel
                .Where(pt => pt.Audio_PlaylistGUID == aPlaylist.Audio_PlaylistGUID)
                    .Select(pt => pt.Audio_Titel).ToList();
            return tmp;
        }
        
        public List<Audio_Titel> LoadTitelByGUID(object titelGUID)
        {
            List<Audio_Titel> tmp = Context.Audio_Playlist_Titel
                .Where(pt => pt.Audio_TitelGUID.Equals(titelGUID))
                    .Select(pt => pt.Audio_Titel).ToList();
            return tmp;
        }

        public List<Audio_Playlist_Titel> LoadPlaylist_TitelByPlaylist(Audio_Playlist aPlaylist, Audio_Titel aTitel)
        {
            List<Audio_Playlist_Titel> tmp = Context.Audio_Playlist_Titel
                .Where(pt => pt.Audio_PlaylistGUID == aPlaylist.Audio_PlaylistGUID)
                    .Where(pt => pt.Audio_TitelGUID == aTitel.Audio_TitelGUID)
                    .Select(pt => pt).ToList();
            return tmp;
        }

        public bool AddTitelToPlaylist(Audio_Playlist aPlaylist, Audio_Titel aTitel)
        {
            Audio_Playlist_Titel tmp;
            return AddTitelToPlaylist(aPlaylist, aTitel, out tmp);
        }


        public bool AddTitelToPlaylist(Audio_Playlist aPlaylist, Audio_Titel aTitel, out Audio_Playlist_Titel aPlaylistTitel)
        {
            aPlaylistTitel = New<Audio_Playlist_Titel>();
            aPlaylistTitel.Audio_Playlist = aPlaylist;
            aPlaylistTitel.Audio_PlaylistGUID = aPlaylist.Audio_PlaylistGUID;
            //insert, update renew

            aPlaylistTitel.Audio_Titel = aTitel;
            aPlaylistTitel.Audio_TitelGUID = aTitel.Audio_TitelGUID;

            aPlaylistTitel.Aktiv = true;

            return Insert<Audio_Playlist_Titel>(aPlaylistTitel);
        }

        public bool RemoveTitelFromPlaylist(Audio_Playlist aPlaylist, Audio_Titel aTitel)
        {
            return RemoveTitelFromPlaylist(aTitel.Audio_Playlist_Titel.Where(pt => pt.Audio_PlaylistGUID == aPlaylist.Audio_PlaylistGUID).First());
        }

        public bool RemoveTitelFromPlaylist(Audio_Playlist_Titel aPlaylistTitel)
        {
            Audio_Titel aTitel = aPlaylistTitel.Audio_Titel;
            bool ret = Delete<Audio_Playlist_Titel>(aPlaylistTitel);
            if (aTitel.Audio_Playlist_Titel.Count == 0)
            {
                ret = ret && RemoveTitel(aTitel);
            }
            return ret;
        }
        
        public bool RemoveTitel(Audio_Titel aTitel)
        {
            TitelListe.Remove(aTitel);
            return Delete<Audio_Titel>(aTitel);
        }
       
        #endregion
        
    }
}