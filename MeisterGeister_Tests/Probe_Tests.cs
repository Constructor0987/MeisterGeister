﻿using MeisterGeister.Logic.General;
using MeisterGeister.Model;
using NUnit.Framework;

namespace MeisterGeister_Tests
{
    [TestFixture]
    internal class Probe_Tests
    {
        [TestFixtureSetUp]
        public void SetupMethods()
        {
        }

        [Test]
        public void TalentProbenTests()
        {
            var ht = new Held_Talent
            {
                Talent = new Talent()
            };
            ProbenErgebnis pe = null;

            // Ohne Modifikator ---------------------------------------

            // TaW 0 Mod 0 10 10 10 10 10 10 0 0 0 TaP* 1 Qualität 0
            ht.TaW = 0;
            ht.Werte[0] = 10;
            ht.Werte[1] = 10;
            ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10;
            ht.Ergebnis.Würfe[1] = 10;
            ht.Ergebnis.Würfe[2] = 10;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(1, pe.Übrig, "TaP*\n" + InfoText(ht));
            Assert.AreEqual(0, pe.Qualität, "Qualität\n" + InfoText(ht));
            Assert.AreEqual(ErgebnisTyp.GELUNGEN, pe.Ergebnis, InfoText(ht));

            // TaW 0 Mod 0 10 10 10 10 10 8 0 0 2 TaP* 1 Qualität 0
            ht.TaW = 0;
            ht.Werte[0] = 10;
            ht.Werte[1] = 10;
            ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10;
            ht.Ergebnis.Würfe[1] = 10;
            ht.Ergebnis.Würfe[2] = 8;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(1, pe.Übrig, "TaP*\n" + InfoText(ht));
            Assert.AreEqual(0, pe.Qualität, "Qualität\n" + InfoText(ht));
            Assert.AreEqual(ErgebnisTyp.GELUNGEN, pe.Ergebnis, InfoText(ht));

            // TaW 0 Mod 0 10 10 10 6 8 8 4 2 2 TaP* 1 Qualität 2
            ht.TaW = 0;
            ht.Werte[0] = 10;
            ht.Werte[1] = 10;
            ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 6;
            ht.Ergebnis.Würfe[1] = 8;
            ht.Ergebnis.Würfe[2] = 8;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(1, pe.Übrig, "TaP*\n" + InfoText(ht));
            Assert.AreEqual(2, pe.Qualität, "Qualität\n" + InfoText(ht));
            Assert.AreEqual(ErgebnisTyp.GELUNGEN, pe.Ergebnis, InfoText(ht));

            // TaW 4 Mod 0 10 10 10 4 12 8 6 -2 2 TaP* 2 Qualität 2
            ht.TaW = 4;
            ht.Werte[0] = 10;
            ht.Werte[1] = 10;
            ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 4;
            ht.Ergebnis.Würfe[1] = 12;
            ht.Ergebnis.Würfe[2] = 8;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(2, pe.Übrig, "TaP*\n" + InfoText(ht));
            Assert.AreEqual(2, pe.Qualität, "Qualität\n" + InfoText(ht));
            Assert.AreEqual(ErgebnisTyp.GELUNGEN, pe.Ergebnis, InfoText(ht));

            // TaW 4 Mod 0 10 10 10 4 10 8 6 0 2 TaP* 4 Qualität 4
            ht.TaW = 4;
            ht.Werte[0] = 10;
            ht.Werte[1] = 10;
            ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 4;
            ht.Ergebnis.Würfe[1] = 10;
            ht.Ergebnis.Würfe[2] = 8;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(4, pe.Übrig, "TaP*\n" + InfoText(ht));
            Assert.AreEqual(4, pe.Qualität, "Qualität\n" + InfoText(ht));
            Assert.AreEqual(ErgebnisTyp.GELUNGEN, pe.Ergebnis, InfoText(ht));

            // TaW -2 Mod 0 10(8) 10(8) 10(8) 10 10 10
            // -2 -2 -2 TaP* 0 Qualität -2
            ht.TaW = -2;
            ht.Werte[0] = 10;
            ht.Werte[1] = 10;
            ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10;
            ht.Ergebnis.Würfe[1] = 10;
            ht.Ergebnis.Würfe[2] = 10;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(0, pe.Übrig, "TaP*\n" + InfoText(ht));
            Assert.AreEqual(-2, pe.Qualität, "Qualität\n" + InfoText(ht));
            Assert.AreEqual(ErgebnisTyp.MISSLUNGEN, pe.Ergebnis, InfoText(ht));

            // TaW -2 Mod 0 10(8) 10(8) 10(8) 7 7 6 1 1 2 TaP* 1 Qualität 1
            ht.TaW = -2;
            ht.Werte[0] = 10;
            ht.Werte[1] = 10;
            ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 7;
            ht.Ergebnis.Würfe[1] = 7;
            ht.Ergebnis.Würfe[2] = 6;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(1, pe.Übrig, "TaP*\n" + InfoText(ht));
            Assert.AreEqual(1, pe.Qualität, "Qualität\n" + InfoText(ht));
            Assert.AreEqual(ErgebnisTyp.GELUNGEN, pe.Ergebnis, InfoText(ht));

            // Mit Erschwernis ---------------------------------------

            // TaW 0 Mod +2 10(8) 10(8) 10(8) 10 10 10
            // -2 -2 -2 TaP* 0 Qualität -2
            ht.TaW = 0;
            ht.Modifikator = 2;
            ht.Werte[0] = 10;
            ht.Werte[1] = 10;
            ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10;
            ht.Ergebnis.Würfe[1] = 10;
            ht.Ergebnis.Würfe[2] = 10;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(0, pe.Übrig, "TaP*\n" + InfoText(ht));
            Assert.AreEqual(-2, pe.Qualität, "Qualität\n" + InfoText(ht));
            Assert.AreEqual(ErgebnisTyp.MISSLUNGEN, pe.Ergebnis, InfoText(ht));

            // TaW 0 Mod +2 10(8) 10(8) 10(8) 10 10 8
            // -2 -2 0 TaP* 0 Qualität -2
            ht.TaW = 0;
            ht.Modifikator = 2;
            ht.Werte[0] = 10;
            ht.Werte[1] = 10;
            ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10;
            ht.Ergebnis.Würfe[1] = 10;
            ht.Ergebnis.Würfe[2] = 8;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(0, pe.Übrig, "TaP*\n" + InfoText(ht));
            Assert.AreEqual(-2, pe.Qualität, "Qualität\n" + InfoText(ht));
            Assert.AreEqual(ErgebnisTyp.MISSLUNGEN, pe.Ergebnis, InfoText(ht));

            // TaW 0 Mod +2 10(8) 10(8) 10(8) 6 8 8 2 0 0 TaP* 1 Qualität 0
            ht.TaW = 0;
            ht.Modifikator = 2;
            ht.Werte[0] = 10;
            ht.Werte[1] = 10;
            ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 6;
            ht.Ergebnis.Würfe[1] = 8;
            ht.Ergebnis.Würfe[2] = 8;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(1, pe.Übrig, "TaP*\n" + InfoText(ht));
            Assert.AreEqual(0, pe.Qualität, "Qualität\n" + InfoText(ht));
            Assert.AreEqual(ErgebnisTyp.GELUNGEN, pe.Ergebnis, InfoText(ht));

            // Mit Erleichterung ---------------------------------------

            // TaW 0 Mod -2 10 10 10 10 10 10 0 0 0 TaP* 1 Qualität 2
            ht.TaW = 0;
            ht.Modifikator = -2;
            ht.Werte[0] = 10;
            ht.Werte[1] = 10;
            ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10;
            ht.Ergebnis.Würfe[1] = 10;
            ht.Ergebnis.Würfe[2] = 10;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(1, pe.Übrig, "TaP*\n" + InfoText(ht));
            Assert.AreEqual(2, pe.Qualität, "Qualität\n" + InfoText(ht));
            Assert.AreEqual(ErgebnisTyp.GELUNGEN, pe.Ergebnis, InfoText(ht));

            // TaW 0 Mod -2 10 10 10 10 10 8 0 0 2 TaP* 1 Qualität 2
            ht.TaW = 0;
            ht.Modifikator = -2;
            ht.Werte[0] = 10;
            ht.Werte[1] = 10;
            ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 10;
            ht.Ergebnis.Würfe[1] = 10;
            ht.Ergebnis.Würfe[2] = 8;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(1, pe.Übrig, "TaP*\n" + InfoText(ht));
            Assert.AreEqual(2, pe.Qualität, "Qualität\n" + InfoText(ht));
            Assert.AreEqual(ErgebnisTyp.GELUNGEN, pe.Ergebnis, InfoText(ht));

            // TaW 0 Mod -2 10 10 10 6 8 8 4 2 2 TaP* 1 Qualität 4
            ht.TaW = 0;
            ht.Modifikator = -2;
            ht.Werte[0] = 10;
            ht.Werte[1] = 10;
            ht.Werte[2] = 10;
            ht.Ergebnis.Würfe[0] = 6;
            ht.Ergebnis.Würfe[1] = 8;
            ht.Ergebnis.Würfe[2] = 8;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(1, pe.Übrig, "TaP*\n" + InfoText(ht));
            Assert.AreEqual(4, pe.Qualität, "Qualität\n" + InfoText(ht));
            Assert.AreEqual(ErgebnisTyp.GELUNGEN, pe.Ergebnis, InfoText(ht));

            // TaW 7 Mod -56 16 12 11 19 1 1
            // -3 11 10 TaP* 7 Qualität 60
            ht.TaW = 7;
            ht.Modifikator = -56;
            ht.Werte[0] = 16;
            ht.Werte[1] = 12;
            ht.Werte[2] = 11;
            ht.Ergebnis.Würfe[0] = 19;
            ht.Ergebnis.Würfe[1] = 1;
            ht.Ergebnis.Würfe[2] = 1;
            pe = ht.ProbenErgebnisBerechnen(ht.Ergebnis);
            Assert.AreEqual(7, pe.Übrig, "TaP*\n" + InfoText(ht));
            Assert.AreEqual(60, pe.Qualität, "Qualität\n" + InfoText(ht));
            Assert.AreEqual(ErgebnisTyp.GLÜCKLICH, pe.Ergebnis, InfoText(ht));
        }

        private string InfoText(Held_Talent probe)
        {
            return string.Format("TaW {0}\tMod {1}\nWerte:\t{2}\t{3}\t{4}\nWürfe:\t{5}\t{6}\t{7}\nTaP* {8}\tQualität {9}", probe.TaW, probe.Modifikator,
                probe.Werte[0], probe.Werte[1], probe.Werte[2],
                probe.Ergebnis.Würfe[0], probe.Ergebnis.Würfe[1], probe.Ergebnis.Würfe[2],
                probe.Ergebnis.Übrig, probe.Ergebnis.Qualität);
        }
    }
}
