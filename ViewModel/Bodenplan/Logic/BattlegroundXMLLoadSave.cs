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
using System.Data;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public class BattlegroundXMLLoadSave
    {
        public void SaveMapToXML(ObservableCollection<BattlegroundBaseObject> bbo, String filename, bool SaveWithoutPictures, List<double> fogSettings)
        {
            try
            {
                //Run needed serializationfunktion - remove all BattlegroundCreature objects
                ObservableCollection<BattlegroundBaseObject> bboWithoutHeroes = new ObservableCollection<BattlegroundBaseObject>();
                foreach (var o in bbo)
                {
                    if (!(o is BattlegroundCreature) && ((o is ImageObject && !SaveWithoutPictures) || (!(o is ImageObject))))
                    {
                        o.RunBeforeXMLSerialization();
                        //Console.WriteLine("SAVE TO XML: " + o.ToString());

                        bboWithoutHeroes.Add(o as BattlegroundBaseObject);//BattlegroundCreature);
                    }
                }

                ObservableCollection<DataTable> bboHeroes = new ObservableCollection<DataTable>();
                foreach (var o in bbo)
                {
                    if (o is BattlegroundCreature)
                    {
                        DataTable dtCreature = CreateStructurCreatureTbl(o as BattlegroundCreature);
                        dtCreature = AddCreatureInfo(dtCreature, o as BattlegroundCreature);
                        //o.RunBeforeXMLSerialization();
                        //Console.WriteLine("SAVE TO XML: " + o.ToString());

                        bboHeroes.Add(dtCreature);
                    }
                }


                //serialize observablecollection in class XmlCollectionHelper to XML File
                XmlSerializer xs = new XmlSerializer(typeof(XmlCollectionHelper));
                XmlCollectionHelper xmlH = new XmlCollectionHelper(bboWithoutHeroes);

                foreach (DataTable dt in bboHeroes)
                {
                    xmlH.AddObsDT(dt);
                }

                foreach (var p in bboWithoutHeroes)
                {
                    if (p is ImageObject)
                    {
                        var st = ((ImageObject)p).PictureUrl.Split('/');
                        if (!xmlH.PictureIsAlreadySerialized(st[st.Length - 1]))
                        {
                            xmlH.AddBase64PicToPictureBox(st[st.Length - 1], ((ImageObject)p).ImageToBase64());
                        }
                    }
                }

                if (fogSettings.Count != 0)
                {
                    DataTable dtSettings = new DataTable();
                    dtSettings.TableName = "Settings";
                    dtSettings.Columns.Add("BackgroundOffsetSize");
                    dtSettings.Columns.Add("BackgroundOffsetX");
                    dtSettings.Columns.Add("InvBackgroundOffsetY");

                    dtSettings.Columns.Add("GridOffsetX");
                    dtSettings.Columns.Add("GridOffsetY");
                    dtSettings.Columns.Add("ScaleSpielerGrid");
                    dtSettings.Columns.Add("ScaleKampfGrid");
                    dtSettings.Columns.Add("IsRechteckRaster");

                    dtSettings.Rows.Add();
                    dtSettings.Rows[0]["BackgroundOffsetSize"] = fogSettings[0];
                    dtSettings.Rows[0]["BackgroundOffsetX"] = fogSettings[1];
                    dtSettings.Rows[0]["InvBackgroundOffsetY"] = fogSettings[2];

                    dtSettings.Rows[0]["GridOffsetX"] = fogSettings[3];
                    dtSettings.Rows[0]["GridOffsetY"] = fogSettings[4];
                    dtSettings.Rows[0]["ScaleSpielerGrid"] = fogSettings[5];
                    dtSettings.Rows[0]["ScaleKampfGrid"] = fogSettings[6];
                    dtSettings.Rows[0]["IsRechteckRaster"] = fogSettings[7];
                    xmlH.AddObsDT(dtSettings);
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

        private DataTable AddCreatureInfo(DataTable dt, BattlegroundCreature o)
        {
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1]["CreatureHeight"] = o.CreatureHeight;
            dt.Rows[dt.Rows.Count - 1]["CreatureNameX"] = o.CreatureNameX;
            dt.Rows[dt.Rows.Count - 1]["CreatureNameY"] = o.CreatureNameY;
            dt.Rows[dt.Rows.Count - 1]["CreaturePictureUrl"] = o.CreaturePictureUrl;
            dt.Rows[dt.Rows.Count - 1]["CreaturePosition"] = o.CreaturePosition;
            dt.Rows[dt.Rows.Count - 1]["CreatureWidth"] = o.CreatureWidth;
            dt.Rows[dt.Rows.Count - 1]["CreatureX"] = o.CreatureX;
            dt.Rows[dt.Rows.Count - 1]["CreatureY"] = o.CreatureY;
            dt.Rows[dt.Rows.Count - 1]["PortraitFileName"] = o.PortraitFileName != null ? o.PortraitFileName : "";
            dt.Rows[dt.Rows.Count - 1]["ZDisplayX"] = o.ZDisplayX;
            dt.Rows[dt.Rows.Count - 1]["ZDisplayY"] = o.ZDisplayY;
            dt.Rows[dt.Rows.Count - 1]["ZLevel"] = o.ZLevel;
            return dt;
        }

        private DataTable CreateStructurCreatureTbl(BattlegroundCreature o)
        {
            DataTable dt = new DataTable();
            dt.TableName = o.ToString();
            dt.Columns.Add("CreatureHeight");
            dt.Columns.Add("CreatureNameX");
            dt.Columns.Add("CreatureNameY");
            dt.Columns.Add("CreaturePictureUrl");
            dt.Columns.Add("CreaturePosition");
            dt.Columns.Add("CreatureWidth");
            dt.Columns.Add("CreatureX");
            dt.Columns.Add("CreatureY");
            dt.Columns.Add("PortraitFileName");
            dt.Columns.Add("ZDisplayX");
            dt.Columns.Add("ZDisplayY");
            dt.Columns.Add("ZLevel");
            return dt;
        }


        public ObservableCollection<double> LoadSettingsFromXML(String filename)
        {
            ObservableCollection<double> back = new ObservableCollection<double>();
            try
            {
                var serializer = new XmlSerializer(typeof(XmlCollectionHelper));
                using (var stream = File.OpenRead(filename))
                {
                    var loadedFile = (XmlCollectionHelper)(serializer.Deserialize(stream));

                    foreach (var dt in loadedFile.ObsDT)
                    {
                        if (dt.TableName == "Settings")
                        {
                            DataRow drow = dt.Rows[0];
                            back.Add(Convert.ToDouble(drow["BackgroundOffsetSize"]));
                            back.Add(Convert.ToDouble(drow["BackgroundOffsetX"]));
                            back.Add(Convert.ToDouble(drow["InvBackgroundOffsetY"]));

                            back.Add(Convert.ToDouble(drow["GridOffsetX"]));
                            back.Add(Convert.ToDouble(drow["GridOffsetY"]));
                            back.Add(Convert.ToDouble(drow["ScaleSpielerGrid"]));
                            back.Add(Convert.ToDouble(drow["ScaleKampfGrid"]));
                            back.Add(Convert.ToDouble(drow["IsRechteckRaster"]));
                        }
                    }
                    stream.Close();
                    return back;
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("[InvalidOperationException]" + e.Message);
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
            return new ObservableCollection<double>();
        }


        public ObservableCollection<BattlegroundBaseObject> LoadMapFromXML(String filename, bool LoadWithoutPictures)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(XmlCollectionHelper));
                using (var stream = File.OpenRead(filename))
                {
                    var loadedFile = (XmlCollectionHelper)(serializer.Deserialize(stream));

                    if (!LoadWithoutPictures)
                    {
                        foreach (var pic in loadedFile.BattlegroundXMLPictureBox)
                        {
                            var currentPics = Ressources.GetPictureUrls();
                            var found = currentPics.Where(x => x.EndsWith(pic.picName)).Any();

                            if (!found) Base64ToImage(pic.picName, pic.picInBase64);
                        }
                    }

                    foreach (var element in loadedFile.ObsColl)
                    {
                        element.RunAfterXMLDeserialization();
                    }

                    foreach (var dt in loadedFile.ObsDT)
                    {
                        if (dt.TableName == "Settings") continue;

                        BattlegroundBaseObject bObj = Global.ContextHeld.HeldenGruppeListe.FirstOrDefault(t => t.ToString() == dt.TableName);
                        if (bObj != null)
                        {
                            DataRow drow = dt.Rows[0];
                            (bObj as BattlegroundCreature).CreatureHeight = Convert.ToDouble(drow["CreatureHeight"]);
                            (bObj as BattlegroundCreature).CreatureNameX = Convert.ToDouble(drow["CreatureNameX"]);
                            (bObj as BattlegroundCreature).CreatureNameY = Convert.ToDouble(drow["CreatureNameY"]);
                            (bObj as BattlegroundCreature).CreaturePictureUrl = drow["CreaturePictureUrl"].ToString();
                            (bObj as BattlegroundCreature).CreaturePosition = drow["CreaturePosition"].ToString();
                            (bObj as BattlegroundCreature).CreatureWidth = Convert.ToDouble(drow["CreatureWidth"]);
                            (bObj as BattlegroundCreature).CreatureX = Convert.ToDouble(drow["CreatureX"]);
                            (bObj as BattlegroundCreature).CreatureY = Convert.ToDouble(drow["CreatureY"]);

                            (bObj as BattlegroundCreature).PortraitFileName = drow["PortraitFileName"].ToString() != "" ? drow["PortraitFileName"].ToString() : null;
                            (bObj as BattlegroundCreature).ZDisplayX = Convert.ToDouble(drow["ZDisplayX"]);
                            (bObj as BattlegroundCreature).ZDisplayY = Convert.ToDouble(drow["ZDisplayY"]);
                            (bObj as BattlegroundCreature).ZLevel = Convert.ToDouble(drow["ZLevel"]);
                        }
                    }
                    stream.Close();
                    return loadedFile.ObsColl;
                }
            }
            catch (InvalidOperationException  e)
            {
                Console.WriteLine("[InvalidOperationException]" + e.Message);
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
        public ObservableCollection<DataTable> ObsDT = new ObservableCollection<DataTable>();
        public List<BattlegroundBase64Picture> BattlegroundXMLPictureBox = new List<BattlegroundBase64Picture>();

        public XmlCollectionHelper() { }

        public XmlCollectionHelper(ObservableCollection<BattlegroundBaseObject> obscoll)
        {
            ObsColl = obscoll;
        }

        public void AddObsDT(DataTable dt)
        {
            ObsDT.Add(dt);
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
