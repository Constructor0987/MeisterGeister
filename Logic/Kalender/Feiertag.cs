using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Kalender
{
    /// <summary>
    /// Beschreibt einen Feiertag.
    /// </summary>
    public class Feiertag
    {
        public string Name { get; set; }
        public string Art { get; set; }
        public string Verbreitung { get; set; }
        public string Stichwort { get; set; }
        private string _wikiLink = string.Empty;
        public string WikiLink
        {
            get
            {
                if (string.IsNullOrEmpty(_wikiLink))
                    return Name;
                else
                    return _wikiLink;
            }
            set { _wikiLink = value; }
        }

        public Feiertag(string name, string art, string stichwort, string verbreitung = "")
        {
            Name = name;
            Art = art;
            Stichwort = stichwort;
            Verbreitung = verbreitung;
        }

        public string Details
        {
            get
            {
                return string.Format("Art: {0}\nVerbreitung: {1}\n{2}", Art, Verbreitung, Stichwort);
            }
        }
    }
}
