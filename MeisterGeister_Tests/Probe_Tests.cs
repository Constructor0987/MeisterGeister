using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using NUnit.Framework;

namespace MeisterGeister_Tests
{
    [TestFixture]
    class Probe_Tests
    {
        [TestFixtureSetUp]
        public void SetupMethods()
        {

        }

        [Test]
        public void TalentProbenTests()
        {
            Held_Talent ht = new Held_Talent();
            ht.Talent = new Talent();
            ProbenErgebnis pe = null;
            string info = string.Empty;

            // Ohne Modifikator
            // ---------------------------------------

            info = "TaW 0 -- Eigenschaften alle 10 -- Würfe alle 10 -- kein Mod --> 1 TaP*";
            ht.TaW = 0;
            ht.Werte[0] = 10; ht.Werte[1] = 10; ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10; ht.Ergebnis.Würfe[1] = 10; ht.Ergebnis.Würfe[2] = 10;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(pe.Übrig, 1, info);

            info = "TaW 0 -- Eigenschaften alle 10 -- 2 Würfe 10, 1 Wurf 12 -- kein Mod --> -2 TaP*";
            ht.TaW = 0;
            ht.Werte[0] = 10; ht.Werte[1] = 10; ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10; ht.Ergebnis.Würfe[1] = 10; ht.Ergebnis.Würfe[2] = 12;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(pe.Übrig, -2, info);

            info = "TaW 0 -- Eigenschaften alle 10 -- 2 Würfe 10, 1 Wurf 8 -- kein Mod --> 1 TaP*";
            ht.TaW = 0;
            ht.Werte[0] = 10; ht.Werte[1] = 10; ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10; ht.Ergebnis.Würfe[1] = 10; ht.Ergebnis.Würfe[2] = 8;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(pe.Übrig, 1, info);

            info = "TaW 4 -- Eigenschaften alle 10 -- Würfe alle 10 -- kein Mod --> 4 TaP*";
            ht.TaW = 4;
            ht.Werte[0] = 10; ht.Werte[1] = 10; ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10; ht.Ergebnis.Würfe[1] = 10; ht.Ergebnis.Würfe[2] = 10;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(pe.Übrig, 4, info);

            info = "TaW 4 -- Eigenschaften alle 10 -- 2 Würfe 10, 1 Wurf 12 -- kein Mod --> 2 TaP*";
            ht.TaW = 4;
            ht.Werte[0] = 10; ht.Werte[1] = 10; ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10; ht.Ergebnis.Würfe[1] = 10; ht.Ergebnis.Würfe[2] = 12;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(pe.Übrig, 2, info);

            info = "TaW 4 -- Eigenschaften alle 10 -- 2 Würfe 10, 1 Wurf 8 -- kein Mod --> 4 TaP*";
            ht.TaW = 4;
            ht.Werte[0] = 10; ht.Werte[1] = 10; ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10; ht.Ergebnis.Würfe[1] = 10; ht.Ergebnis.Würfe[2] = 8;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(pe.Übrig, 4, info);

            info = "TaW -4 -- Eigenschaften alle 10 -- Würfe alle 10 -- kein Mod --> -4 TaP*";
            ht.TaW = -4;
            ht.Werte[0] = 10; ht.Werte[1] = 10; ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10; ht.Ergebnis.Würfe[1] = 10; ht.Ergebnis.Würfe[2] = 10;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(pe.Übrig, -4, info);

            info = "TaW -4 -- Eigenschaften alle 10 -- 2 Würfe 10, 1 Wurf 12 -- kein Mod --> -6 TaP*";
            ht.TaW = -4;
            ht.Werte[0] = 10; ht.Werte[1] = 10; ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10; ht.Ergebnis.Würfe[1] = 10; ht.Ergebnis.Würfe[2] = 12;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(pe.Übrig, -6, info);

            info = "TaW -4 -- Eigenschaften alle 10 -- 2 Würfe 10, 1 Wurf 8 -- kein Mod --> -4 TaP*";
            ht.TaW = -4;
            ht.Werte[0] = 10; ht.Werte[1] = 10; ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10; ht.Ergebnis.Würfe[1] = 10; ht.Ergebnis.Würfe[2] = 8;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(pe.Übrig, -4, info);
        }

    }
}
