﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class KampfstilHelper
    {
        //public static List<Kampfstil> Kampfstile(IKämpfer k)
        //{

        //TODO JT: besser einfach als liste aufbauen und dann nach dem Paradewert sortieren
        //eine andere Methode sollte den Paradewert ausgeben können.

        //    List<Kampfstil> stile = new List<Kampfstil>();
        //    stile.Add(Kampfstil.Keiner);
        //    if (k is Held)
        //    {
        //        Held h = k as Held;
        //        //werden zwei Ausrüstungsgegenstände verwendet?
        //        IEnumerable<Held_Ausrüstung> hände = h.Held_Ausrüstung.Where(ha => (ha.Ausrüstung.Schild != null || ha.Ausrüstung.Fernkampfwaffe != null || ha.Ausrüstung.Waffe != null) && ha.Trageort.Name.EndsWith("Hand"));
        //        Held_Ausrüstung linkeHand = hände.Where(ha => ha.Trageort.Name.Equals("Linke Hand")).First();
        //        Held_Ausrüstung rechteHand = hände.Where(ha => ha.Trageort.Name.Equals("Rechte Hand")).First();
        //        Held_Ausrüstung hauptHand = rechteHand;
        //        Held_Ausrüstung nebenHand = linkeHand;
        //        if (h.HatVorNachteil("Linkshänder") || h.HatVorNachteil("Beidhändig"))
        //        {
        //            hauptHand = linkeHand;
        //            nebenHand = rechteHand;
        //        }
        //        if (nebenHand.Ausrüstung.Waffe != null)
        //        {
        //            Waffe w = nebenHand.Ausrüstung.Waffe;
        //        }
        //        else if (nebenHand.Ausrüstung.Schild != null)
        //        {
        //            Schild s = nebenHand.Ausrüstung.Schild;
        //            if (s.Typ == "S")
        //                stile.Insert(0, Kampfstil.Schildkampf);
        //            else if (s.Typ == "P")
        //                stile.Insert(0, Kampfstil.Parierwaffenstil);
        //            else
        //            {

        //                stile.Insert(0, Kampfstil.Parierwaffenstil);
        //            }
        //        }
                
        //    }
        //    return stile;
        //}

        public static List<WaffenloserKampfstil> WaffenloseKampfstile(IKämpfer k)
        {
            List<WaffenloserKampfstil> stile = new List<WaffenloserKampfstil>();
            stile.Add(WaffenloserKampfstil.Raufen);
            if (k is Held)
            {
                Held h = k as Held;
                int ringen = h.Talentwert("Ringen");
                int raufen = h.Talentwert("Raufen");
                if(raufen > ringen)
                    stile.Insert(stile.Count, WaffenloserKampfstil.Ringen);
                else
                    stile.Insert(0, WaffenloserKampfstil.Ringen);
                if (h.HatSonderfertigkeit("Bornländisch"))
                    stile.Insert(0, WaffenloserKampfstil.Bornländisch);
                if (h.HatSonderfertigkeit("Unauer Schule"))
                    stile.Insert(0, WaffenloserKampfstil.UnauerSchule);
                if (h.HatSonderfertigkeit("Hammerfaust"))
                    stile.Insert(0, WaffenloserKampfstil.Hammerfaust);
                if (h.HatSonderfertigkeit("Gladiatorenstil"))
                    stile.Insert(0, WaffenloserKampfstil.Gladiatorenstil);
                if (h.HatSonderfertigkeit("Mercanario"))
                    stile.Insert(0, WaffenloserKampfstil.Mercanario);
                if (h.HatSonderfertigkeit("Hruruzat"))
                    stile.Insert(0, WaffenloserKampfstil.Hruruzat);
            }
            return stile;
        }
    }

    public enum Kampfstil
    {
        Keiner,
        Schildkampf,
        Parierwaffenstil,
        BeidhändigerKampf,
        //MehrhändigerKampf, //Mehrhändiger Kampf kann mit den anderen Stilen kombiniert werden.
        Halbschwert
    }

    public enum WaffenloserKampfstil
    {
        Raufen,
        Ringen,
        Mercanario,
        Hammerfaust,
        Bornländisch,
        Hruruzat,
        UnauerSchule,
        Gladiatorenstil
    }
}
