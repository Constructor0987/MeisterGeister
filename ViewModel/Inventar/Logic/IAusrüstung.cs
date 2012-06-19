﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Inventar.Logic
{
    public interface IAusrüstung
    {
        string Name
        {
            get;
            set;
        }

        double Preis
        {
            get;
            set;
        }

        int Gewicht
        {
            get;
            set;
        }

        string Verbreitung
        {
            get;
            set;
        }

        string Literatur
        {
            get;
            set;
        }

        string Setting
        {
            get;
            set;
        }

        string Bemerkung
        {
            get;
            set;
        }
    }
}
