using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeisterGeister.ViewModel.AudioPlayer.Logic
{
    public interface IWesenPlaylist
    {
        bool HatGegner { get; }
        bool HatHeld { get; }
        Audio_Playlist Audio_Playlist { get; }
        GegnerBase GegnerBase { get; }
        Held Held { get; }

    }
}
