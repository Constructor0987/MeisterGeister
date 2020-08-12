using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class HUE_Szene
    {
        public HUE_Szene()
        {
            HUE_SzeneGUID = Guid.NewGuid();
        }

        public static void UpdateLists()
        {
            Global.ContextHUE.UpdateList<HUE_Szene>();
            Model.HUE_Szene.UpdateLists();
        }
    }
}
