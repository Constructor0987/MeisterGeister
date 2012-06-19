using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.Model
{
    public partial class Gegner_Angriff : IWaffe, IFernkampfwaffe, INahkampfwaffe
    {
        #region IWaffe
        public int TPKKBonus
        {
            get { return 0; }
        }
        #endregion

        #region IFernkampfwaffe
        public int? RWSehrNah
        {
            get { return Reichweite; }
        }

        public int? RWNah
        {
            get { return Reichweite; }
        }

        public int? RWMittel
        {
            get { return Reichweite; }
        }

        public int? RWWeit
        {
            get { return Reichweite; }
        }

        public int? RWSehrWeit
        {
            get { return Reichweite; }
        }

        public int? TPSehrNah
        {
            get { return 0; }
        }

        public int? TPNah
        {
            get { return 0; }
        }

        public int? TPMittel
        {
            get { return 0; }
        }

        public int? TPWeit
        {
            get { return 0; }
        }

        public int? TPSehrWeit
        {
            get { return 0; }
        }
        #endregion

        #region INahkampfwaffe
        public Distanzklasse Distanzklasse
        {
            get { return Waffe.ParseDistanzklasse(DK); }
        }
        #endregion
    }
}
