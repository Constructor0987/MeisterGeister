using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class GegnerBase
    {
        public GegnerBase()
        {
            GegnerBaseGUID = Guid.NewGuid();
        }

        public bool Usergenerated
        {
            get { return !GegnerBaseGUID.ToString().StartsWith("00000000-0000-0000-000"); }
        }

        #region Import Export
        public static GegnerBase Import(string pfad, bool batch = false)
        {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            Guid gegnerGuid = serialization.ImportGegner(pfad);
            if (gegnerGuid == Guid.Empty)
                return null;
            Global.ContextKampf.UpdateList<GegnerBase>();
            return Global.ContextKampf.Liste<GegnerBase>().Where(g => g.GegnerBaseGUID == gegnerGuid).First();
        }

        public void Export(string pfad, bool batch = false)
        {
            Service.SerializationService serialization = Service.SerializationService.GetInstance(!batch);
            serialization.ExportGegner(GegnerBaseGUID, pfad);
        }
        #endregion
    }
}
