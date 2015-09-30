using MeisterGeister.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Model
{
    public partial class Audio_HotkeyWesen
    {
        public Audio_Playlist BezugsPlaylist = null;

        public Audio_HotkeyWesen()
        {
            Audio_HotkeyWesenGUID = Guid.NewGuid();
        }
            
        public string Name
        {
            get { return Held != null ? Held.Name : GegnerBase.Name; }
        }

        private bool _hatPlaylist = false;
        public bool HatPlaylist
        {
            get { return _hatPlaylist; }
            set { Set(ref _hatPlaylist, value); }
        }
    }
}
