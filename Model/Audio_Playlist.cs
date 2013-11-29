using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Audio_Playlist
    {
        public Audio_Playlist()
        {
            Audio_PlaylistGUID = Guid.NewGuid();
        }

        #region Import Export
        public static Audio_Playlist Import(string pfad, bool batch = false)
        {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            Guid audio_playlistGuid = serialization.ImportAudio(pfad);
            
            UpdateLists();
            return Global.ContextAudio.Liste<Audio_Playlist>().Where(a => a.Audio_PlaylistGUID == audio_playlistGuid).FirstOrDefault();
        }

        
        public void Export(string pfad, Guid g, bool batch = true)
        {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            serialization.ExportAudioPlaylist(g, pfad); //Audio_PlaylistGUID
        }

        public static void UpdateLists()
        {
            Global.ContextAudio.UpdateList<Audio_Titel>();
            Global.ContextAudio.UpdateList<Audio_Playlist>();
            Global.ContextAudio.UpdateList<Audio_Playlist_Titel>();
        }
        #endregion
    }
}
