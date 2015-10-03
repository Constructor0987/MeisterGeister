using MeisterGeister.ViewModel.AudioPlayer.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MeisterGeister.Model
{
    public partial class Held_Audio_Playlist : IWesenPlaylist
    {
        public bool HatGegner
        {
            get { return false; }
        }

        public bool HatHeld
        {
            get { return true; }
        }

        public GegnerBase GegnerBase
        {
            get { return null; }
        }
    }
}
