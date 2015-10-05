using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;
using MeisterGeister.ViewModel.Helden.Logic;

namespace MeisterGeister.Model
{
    public partial class Held_Ausrüstung : IHatHeld
    {
        public Held_Ausrüstung()
        {
            //this.HeldAusrüstungGUID = Guid.NewGuid();
        }

        //public Ausrüstung Ausrüstung
        //{
        //    get
        //    {
        //        if (this.Held_Rüstung != null)
        //            return Held_Rüstung.Rüstung.Ausrüstung;
        //        if (this.Held_Fernkampfwaffe != null)
        //            return Held_Fernkampfwaffe.Fernkampfwaffe.Ausrüstung;
        //        if (this.Held_BFAusrüstung != null)
        //        { 
        //            if (this.Held_BFAusrüstung.Held_Waffe != null)
        //                return Held_BFAusrüstung.Held_Waffe.Waffe.Ausrüstung;
        //            if (this.Held_BFAusrüstung.Schild != null)
        //                return Held_BFAusrüstung.Schild.Ausrüstung;
        //        }
        //        return null;
        //}
    }
}
