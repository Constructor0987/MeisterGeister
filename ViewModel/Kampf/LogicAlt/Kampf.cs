using System;
using System.Collections.Generic;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.LogicAlt.General;

namespace MeisterGeister.ViewModel.Kampf.LogicAlt
{
    public class Kampf
    {
        private KämpferList _kämpferListe = new KämpferList();

        private KampfAktionListe _aktionen = new KampfAktionListe();

        public KämpferList KämpferListe
        {
            get { return _kämpferListe; }
            set { _kämpferListe = value; }
        }

        public KampfAktionListe AktionenListe
        {
            get { return _aktionen; }
        }

        private int _kampfrunde = 0;

        public int Kampfrunde
        {
            get { return _kampfrunde; }
        }

        public TimeSpan KampfZeit
        {
            get
            {
                var interval = new TimeSpan(0, 0, 0, _kampfrunde * 3);
                return interval;
            }
        }

        public Held AddHeld(Daten.DatabaseDSADataSet.HeldRow heldRow)
        {
            var h = new Held(heldRow);
            h.InitiativeWürfeln();
            h.KampfPartei = 1;
            if (!KämpferListe.Contains(h))
            {
                KämpferListe.Add(h);
            }
            return h;
        }

        public void AddKämpfer(Gegner g, bool verbündeter = false)
        {
            g.InitiativeWürfeln();
            g.KampfPartei = verbündeter ? 1 : 2;
            KämpferListe.Add(g);
        }

        public void AddAktion(string aktionQuelle, string aktionName, int aktionDauer, IKämpfer kämpfer, Aktion ak, AktionDelegate del = null)
        {
            var a = new KampfAktion
                                {
                                    Aktionsname = aktionName,
                                    Aktionsquelle = aktionQuelle,
                                    Kämpfer = kämpfer,
                                    Kampf = this,
                                    AktionAufruf = del,
                                    AktionenDauer = aktionDauer
                                };


            if (ak == Aktion.Aktion1)
            {
                a.InKampfrunde = (int)(Kampfrunde + (aktionDauer - 1) / 2.0);
                a.Aktion = aktionDauer % 2.0 > 0 ? Aktion.Aktion1 : Aktion.Aktion2;
            }
            else if (ak == Aktion.Aktion2)
            {
                a.InKampfrunde = (int)(Kampfrunde + aktionDauer / 2.0);
                a.Aktion = aktionDauer % 2.0 > 0 ? Aktion.Aktion2 : Aktion.Aktion1;
            }

            // Kampf Liste
            AktionenListe.Add(a);
            AktionenListe.Sort();

            // Kämpfer Liste
            if (kämpfer != null)
            {
                kämpfer.AktionenLaufend.Add(a);
                kämpfer.AktionenLaufend.Sort();
            }
        }

        public event EventHandler NächsterKämpferRollover;

        public void NächsterKämpfer(IKämpfer aktuell = null)
        {
            foreach (IKämpfer kämpfer in KämpferListe)
                kämpfer.AktuellerKämpfer = false;

            if (aktuell == null)
            {
                if (_kämpferListe.Count >= 1)
                {
                    int index = _kämpferListe.IndexOf(_aktuellerKämpfer);
                    if (++index < _kämpferListe.Count)
                        _aktuellerKämpfer = _kämpferListe[index];
                    else
                    {
                        _aktuellerKämpfer = _kämpferListe[0];
                        if (NächsterKämpferRollover != null)
                            NächsterKämpferRollover(null, null);
                    }
                    _aktuellerKämpfer.AktuellerKämpfer = true;
                }
                else
                    _aktuellerKämpfer = null;
            }
            else
            {
                aktuell.AktuellerKämpfer = true;
                _aktuellerKämpfer = aktuell;
            }
        }

        public void VorherigerKämpfer()
        {
            foreach (IKämpfer kämpfer in KämpferListe)
                kämpfer.AktuellerKämpfer = false;

            if (_kämpferListe.Count >= 1)
            {
                int index = _kämpferListe.IndexOf(_aktuellerKämpfer);
                if (--index >= 0)
                    _aktuellerKämpfer = _kämpferListe[index];
                else
                    _aktuellerKämpfer = _kämpferListe[_kämpferListe.Count - 1];
                _aktuellerKämpfer.AktuellerKämpfer = true;
            }
            else
                _aktuellerKämpfer = null;
        }

        public void NeueKampfrunde()
        {
            _kampfrunde++;

            // Nächsten Kämpfer auf Anfang
            foreach (IKämpfer kämpfer in KämpferListe)
                kämpfer.AktuellerKämpfer = false;

            if (_kämpferListe.Count >= 1)
            {
                _aktuellerKämpfer = _kämpferListe[0];
                _aktuellerKämpfer.AktuellerKämpfer = true;
            }
            else
                _aktuellerKämpfer = null;
        }

        public void NeuerKampf()
        {
            _kampfrunde = 0;
            var neueListe = new KämpferList();
            foreach (IKämpfer kämpfer in KämpferListe)
            {
                kämpfer.AktionenLaufend.Clear();
                if (kämpfer is Held)
                {
                    kämpfer.AktuellerKämpfer = false;
                    (kämpfer as Held).InitiativeWürfeln(); // Initiative neu würfeln
                    neueListe.Add(kämpfer);
                }
            }
            _kämpferListe = neueListe;

            _aktionen = new KampfAktionListe();
        }

        public void RemoveAktionenAll()
        {
            _aktionen.Clear();
            foreach (IKämpfer k in _kämpferListe)
            {
                k.AktionenLaufend.Clear();
            }
        }

        private IKämpfer _aktuellerKämpfer;
        public IKämpfer AktuellerKämpfer
        {
            get { return _aktuellerKämpfer; }
        }
    }
}
