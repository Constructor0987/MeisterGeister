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
    }
}
