using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Audio_Theme
    {
        public Audio_Theme()
        {
            Audio_ThemeGUID = Guid.NewGuid();
        }

        /// <summary>
        /// Audio_Theme1
        /// </summary>
        public ICollection<Audio_Theme> Parents
        {
            get { return Audio_Theme1; }
            set { Audio_Theme1 = value; OnChanged("Parents"); }
        }

        /// <summary>
        /// Audio_Theme2
        /// </summary>
        public ICollection<Audio_Theme> Children
        {
            get { return Audio_Theme2; }
            set { Audio_Theme2 = value; OnChanged("Children"); }
        }

        //gehört eigentlich ins ViewModel vom AudioPlayer
        /// <summary>
        /// Exportiert alle Audiodaten.
        /// </summary>
        /// <param name="pfad">Pfad wo die XML-Datei abgespeichert werden soll</param>
        /// <param name="batch"></param>
        public static void ExportAll(string pfad)
        {
            Audio_Theme aTheme_ALL = Global.ContextAudio.GetThemeAll();
            Service.SerializationService serialization = Service.SerializationService.GetInstance(true);
            serialization.ExportAudioTheme(aTheme_ALL, pfad);
        }
        
        public static void UpdateLists()
        {
            Global.ContextAudio.UpdateList<Audio_Theme>();
            Model.Audio_Playlist.UpdateLists();
        }

        public void Export(string pfad, Guid g,bool batch = true)
        {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            if (g == Guid.Empty)
                serialization.ExportAudioTheme(Audio_ThemeGUID, pfad);
            else
                serialization.ExportAudioTheme(g, pfad);
        }
    }
}
