using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public class ExportPListTheme
    {
        public List<Audio_Playlist> aPlayList = new List<Audio_Playlist>();
        public List<Audio_Theme> aTheme = new List<Audio_Theme>();
    }

    public partial class Audio_Playlist
    {
        public Audio_Playlist()
        {
            Audio_PlaylistGUID = Guid.NewGuid();
        }

        #region Import Export
        //public static Audio_Playlist Import(string pfad, bool batch = false)
        //{
        //    return Import(pfad, Guid.Empty, batch);
        //}

        public static Audio_Playlist Import(string pfad, bool batch = false)
        {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            Guid audio_playlistGuid = serialization.ImportAudio(pfad);
            
            UpdateLists();
            return Global.ContextAudio.Liste<Audio_Playlist>().Where(a => a.Audio_PlaylistGUID == audio_playlistGuid).FirstOrDefault();
        }
        
        public void Export(string pfad, bool batch = true)
        {
            Audio_Theme aTheme_ALL = new Model.Audio_Theme();
            aTheme_ALL.Audio_Playlist = Global.ContextAudio.PlaylistListe;
            aTheme_ALL.Audio_Theme1 = Global.ContextAudio.ThemeListe;
            Global.ContextAudio.Update<Audio_Theme>(aTheme_ALL);
         /*   ExportPListTheme aPListTheme = new ExportPListTheme();
            aPListTheme.aPlayList = Global.ContextAudio.PlaylistListe;
            aPListTheme.aTheme = Global.ContextAudio.ThemeListe;
            */
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);

        //    serialization.ExportAudioTheme(aTheme_ALL.Audio_ThemeGUID, pfad);
            serialization.ExportAudioTheme(aTheme_ALL, pfad);

            Global.ContextAudio.Delete<Audio_Theme>(aTheme_ALL);
        /*    Global.ContextAudio.PlaylistListe.ForEach(delegate(Audio_Playlist aPlaylist)
            {
                serialization.ExportAudioPlaylist(aPlaylist._audio_PlaylistGUID, pfad);
            });

            Global.ContextAudio.ThemeListe.ForEach(delegate(Audio_Theme aTheme)
            {
                serialization.ExportAudioTheme(aTheme.Audio_ThemeGUID, pfad);
            });*/
            // Audio_PlaylistGUID
        }


        public static void UpdateLists()
        {
            Global.ContextAudio.UpdateList<Audio_Titel>();
            Global.ContextAudio.UpdateList<Audio_Playlist>();
            Global.ContextAudio.UpdateList<Audio_Playlist_Titel>();
            Global.ContextAudio.UpdateList<Audio_Theme>();
            Global.ContextAudio.UpdateList<Audio_Theme_Playlist>();
        }
        #endregion
    }
}
