using MeisterGeister.Logic.Einstellung;
using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Bodenplan;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using MeisterGeister.View.General;

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
