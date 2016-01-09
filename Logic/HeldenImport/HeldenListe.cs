using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MeisterGeister.Logic.HeldenImport
{
    [XmlRoot("helden")]
    public class HeldenListe
    {
        public HeldenListe()
        {
            Helden = new List<HeldElement>();
        }

        [XmlElement("held", typeof(HeldElement))]
        public List<HeldElement> Helden { get; set; }
    }
}
