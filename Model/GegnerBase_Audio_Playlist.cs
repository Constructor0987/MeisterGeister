using MeisterGeister.ViewModel.AudioPlayer.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MeisterGeister.Model
{
    public partial class GegnerBase_Audio_Playlist : IWesenPlaylist
    {
        public bool HatGegner
        {
            get { return true; }
        }

        public bool HatHeld
        {
            get { return false; }
        }

        public Held Held
        {
            get { return null; }
        }
    }
}
