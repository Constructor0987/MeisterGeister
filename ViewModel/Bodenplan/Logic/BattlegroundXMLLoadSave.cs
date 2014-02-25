using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public class BattlegroundXMLLoadSave
    {
        public void SaveMapToXML(ObservableCollection<BattlegroundBaseObject> bbo, String filename)
        {
            try
            {
                //Run needed serializationfunktion - remove all BattlegroundCreature objects
                ObservableCollection<BattlegroundBaseObject> bboWithoutHeroes = new ObservableCollection<BattlegroundBaseObject>();
                foreach (var o in bbo)
                {
                    if (!(o is BattlegroundCreature))
                    {
                        o.RunBeforeXMLSerialization();
                        bboWithoutHeroes.Add(o);
                    }
                }

                //serialize observablecollection in class XmlCollectionHelper to XML File
                XmlSerializer xs = new XmlSerializer(typeof(XmlCollectionHelper));
                XmlCollectionHelper xmlH = new XmlCollectionHelper(bboWithoutHeroes);

                foreach (var p in bboWithoutHeroes)
                {
                    if (p is ImageObject)
                    {
                        var st = ((ImageObject)p).PictureUrl.Split('/');
                        if(!xmlH.PictureIsAlreadySerialized(st[st.Length-1])) {
                            xmlH.AddBase64PicToPictureBox(st[st.Length - 1], ((ImageObject)p).ImageToBase64());
                        }
                    }
                }

                using (StreamWriter wr = new StreamWriter(filename))
                {
                    xs.Serialize(wr, xmlH);
                    wr.Close();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("[IOEXCEPTION] [SAVE] " + e.Message);
            }
            catch (XmlException e)
            {
                Console.WriteLine("[XmlEXCEPTION] [SERIALIZE]" + e.Message);
            }
        }

        public ObservableCollection<BattlegroundBaseObject> LoadMapFromXML(String filename)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(XmlCollectionHelper));
                using (var stream = File.OpenRead(filename))
                {
                    var loadedFile = (XmlCollectionHelper)(serializer.Deserialize(stream));

                    foreach (var pic in loadedFile.BattlegroundXMLPictureBox)
                    {
                        var currentPics = Ressources.GetPictureUrls();
                        var found = currentPics.Where(x => x.EndsWith(pic.picName)).Any();
                        
                        if(!found) Base64ToImage(pic.picName,pic.picInBase64);
                    }

                    foreach (var element in loadedFile.ObsColl)
                    {
                        element.RunAfterXMLDeserialization();
                    }
                    stream.Close();
                    return loadedFile.ObsColl;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("[IOEXCEPTION] [LOAD]" + e.Message);
            }
            catch (XmlException e)
            {
                Console.WriteLine("[XmlEXCEPTION] [DESERIALIZE]" + e.Message);
            }
            Console.WriteLine("[INFO] NO DATA FOUND?");
            return new ObservableCollection<BattlegroundBaseObject>();
        }

        public void Base64ToImage(String PictureUrl, String base64String)
        {
            try
            {
                var picname = PictureUrl.Split('\\');
                // Convert Base64 String to byte[]
                byte[] imageBytes = Convert.FromBase64String(base64String);
                MemoryStream ms = new MemoryStream(imageBytes, 0,
                  imageBytes.Length);

                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);

                using (FileStream file = new FileStream(Ressources.GetFullApplicationPathForPictures() + picname[picname.Length - 1], FileMode.Create, System.IO.FileAccess.Write))
                {
                    file.Write(imageBytes, 0, imageBytes.Length);
                    ms.Close();
                    file.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[Base64ToFile] " + e.Message);
            }
        }
    }


    //XMLInclude every possible Battlegroundobject you want to serialize
    [System.Xml.Serialization.XmlInclude(typeof(BattlegroundBaseObject))]
    [System.Xml.Serialization.XmlInclude(typeof(PathLine))]
    [System.Xml.Serialization.XmlInclude(typeof(FilledPathLine))]
    [System.Xml.Serialization.XmlInclude(typeof(ImageObject))]    
    public class XmlCollectionHelper 
    {
        public ObservableCollection<BattlegroundBaseObject> ObsColl = new ObservableCollection<BattlegroundBaseObject>();
        public List<BattlegroundBase64Picture> BattlegroundXMLPictureBox = new List<BattlegroundBase64Picture>();

        public XmlCollectionHelper() { }

        public XmlCollectionHelper(ObservableCollection<BattlegroundBaseObject> obscoll)
        {
            ObsColl = obscoll;
        }
        
        public void AddBase64PicToPictureBox(String pictureName, String picInBase64)
        {
            BattlegroundXMLPictureBox.Add(new BattlegroundBase64Picture(pictureName, picInBase64));
        }

        public bool PictureIsAlreadySerialized(String pictureName)
        {
            foreach (var p in BattlegroundXMLPictureBox)
            {
                if (p.picName.Equals(pictureName)) return true;
            }
            return false; //no picture with name pictureName found...
        }
    }


    //PictureSerializationClass
    public class BattlegroundBase64Picture
    {
        public BattlegroundBase64Picture() { }

        public BattlegroundBase64Picture(String picname, String picinbase64)
        {
            picName = picname;
            picInBase64 = picinbase64;
        }

        public String picName = "";
        public String picInBase64 = "";
    }
}
