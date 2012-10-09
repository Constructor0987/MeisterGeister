using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Eigene Usings
using BasarLogic = MeisterGeister.ViewModel.Basar.Logic;

namespace MeisterGeister.Model
{
    public partial class Handelsgut : BasarLogic.IHandelsgut
    {
        public bool Usergenerated
        {
            get { return !HandelsgutGUID.ToString().StartsWith("00000000-0000-0000-00"); }
        }
    }
}
