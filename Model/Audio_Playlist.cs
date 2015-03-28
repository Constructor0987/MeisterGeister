using MeisterGeister.Model.Extensions;
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
                
        [DependentProperty("WarteZeit")]
        public string WarteZeitToolTip
        {
            get
            {
                long wert = WarteZeit;
                return "Minimale Wartezeit bei variabeler Pausenzeit\n"
                    + ((wert < 1000) ? wert + " ms" : (wert < 60000) ? 
                    Math.Round((double)(wert) / 1000, 2) + " sek." : 
                    Math.Round((double)(wert) / 60000,2) + " min.");
            }
        }


        [DependentProperty("WarteZeitMin")]
        public string WarteZeitMinToolTip
        {
            get
            {
                long wert = WarteZeitMin;
                return ("Minimale Wartezeit bei variabeler Pausenzeit\n"
                    + ((wert < 1000) ? wert + " ms" : (wert < 60000) ? 
                    Math.Round((double)(wert) / 1000, 2) + " sek." : 
                    Math.Round((double)(wert) / 60000, 2) + " min."));
            }
        }

        [DependentProperty("WarteZeitMax")]
        public string WarteZeitMaxToolTip
        {
            get
            {
                long wert = WarteZeitMax;
                return "Maximale Wartezeit bei variabeler Pausenzeit\n"
                    + ((wert < 1000) ? wert + " ms" : (wert < 60000) ? 
                    Math.Round((double)(wert) / 1000, 2) + " sek." : 
                    Math.Round((double)(wert) / 60000, 2) + " min.");
            }
        }
        #region Import Export

        public static Audio_Playlist Import(string pfad, string soll, bool batch = false)
        { 
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            
            Guid audio_playlistGuid = serialization.ImportAudio(pfad, soll);
            if (audio_playlistGuid == Guid.Empty)
                return null;
            UpdateLists();
            return Global.ContextAudio.Liste<Audio_Playlist>().Where(a => a.Audio_PlaylistGUID == audio_playlistGuid).FirstOrDefault();
        }

        public static void Delete(Audio_Playlist a, bool batch = false)
        {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            serialization.DeleteAudioData(a);
        }

        public static void Delete(Audio_Theme a, bool batch = false)
        {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            serialization.DeleteAudioData(a);
        }


        public void Export(string pfad, Guid g, bool batch = false)
        {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            serialization.ExportAudioPlaylist(g, pfad); //Audio_PlaylistGUID
        }


        
        public static void Export(List<Audio_Playlist> aPlaylistListe, string pfad, bool batch = false)
        {
            foreach (Audio_Playlist aPlaylist in aPlaylistListe)
            {
                //MUSS JEDES MAL AUSGEFÜHRT WERDEN, DA SONST DER EXPORT FEHLERHAFT IST
                Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
                //////////////////////////////////////////////////////////////////////

                Global.SetIsBusy(true, string.Format("Playliste '" + aPlaylist.Name.Replace("/", "_") + "' wird exportiert"));
                string datei = pfad + "\\Playlist_" + aPlaylist.Name.Replace("/", "_") + ".xml";

                datei = datei.Replace("--", "-");
                while (datei.EndsWith("-.xml") || datei.EndsWith(" .xml"))
                    datei = datei.Substring(0, datei.Length - 5) + ".xml";

                System.IO.File.Delete(datei);
                serialization.ExportAudioPlaylist(aPlaylist.Audio_PlaylistGUID, datei);
            }
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
