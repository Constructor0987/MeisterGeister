using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    class BattlegroundHero:BattlegroundCreature
    {
        private Held _hero;
        public Held Hero
        {
            get { return _hero; }
            set
            {
                _hero = value;
                OnPropertyChanged("Hero");
            }
        }

        public String AddHeldPortrait(Guid heldId, string portraitFilename)
        {
            try
            {
               return portraitFilename;
            }
            catch
            {
               return ICON_DIR + "fragezeichen.png";
            }
        }
    }
}
