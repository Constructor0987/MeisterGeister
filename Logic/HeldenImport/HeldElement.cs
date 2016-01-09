using System;
using System.Xml.Serialization;

namespace MeisterGeister.Logic.HeldenImport
{
    public class HeldElement
    {
        [XmlElement("heldenid")]
        public int HeldenId { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("heldenkey")]
        public string HeldenKey { get; set; }

        [XmlElement("heldlastchange")]
        public string HeldLastChange { get; set; }
    }
}