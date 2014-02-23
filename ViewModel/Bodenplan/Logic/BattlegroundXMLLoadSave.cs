using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public class BattlegroundXMLLoadSave
    {
        public void SaveMapToXML(ObservableCollection<BattlegroundBaseObject> bol)
        {
            SerializieToXML(bol);
        }

        private void SerializieToXML(ObservableCollection<BattlegroundBaseObject> bol)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(ObservableCollection<BattlegroundBaseObject>));
            var subReq = bol;
            StringWriter sww = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);
            xsSubmit.Serialize(writer, subReq);
            var xml = sww.ToString(); // Your xml
        }
    }
}
