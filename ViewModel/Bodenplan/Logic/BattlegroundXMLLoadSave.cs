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
using MeisterGeister.Model;
using MeisterGeister.ViewModel.Kampf.Logic;
using MeisterGeister.View.General;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public class BattlegroundXMLLoadSave
    {
        public void SaveMapToXML(ObservableCollection<BattlegroundBaseObject> bbo, String filename, bool SaveWithoutPictures, List<double> Settings, bool GiveFeedback = true)
        {
            try
            {
                //Run needed serializationfunktion - remove all BattlegroundCreature objects
                ObservableCollection<BattlegroundBaseObject> bboWithoutHeroes = new ObservableCollection<BattlegroundBaseObject>();
                foreach (var o in bbo)
                {
                    if (!(o is BattlegroundCreature) && 
                        ((o is ImageObject && !SaveWithoutPictures) || 
                        (!(o is ImageObject)) ||
                        (!(o is MP4Object))) &&
                        (!(o is LichtquelleObject)))
                    {
                        o.RunBeforeXMLSerialization();
                        bboWithoutHeroes.Add(o as BattlegroundBaseObject);
                    }
                }


                ObservableCollection<DataTable> bboHeroes = new ObservableCollection<DataTable>();
                foreach (var o in bbo)
                {
                    if (o is BattlegroundCreature)
                    {
                        DataTable dtCreature = CreateStructurCreatureTbl(o as BattlegroundCreature);
                        dtCreature = AddCreatureInfo(dtCreature, o as BattlegroundCreature);
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

                if (Settings.Count != 0)
                {
                    DataTable dtSettings = new DataTable();
                    dtSettings.TableName = "Settings";
                    dtSettings.Columns.Add("BackgroundOffsetSize");
                    dtSettings.Columns.Add("BackgroundOffsetX");
                    dtSettings.Columns.Add("InvBackgroundOffsetY");

                    dtSettings.Columns.Add("PlayerGridOffsetX");
                    dtSettings.Columns.Add("PlayerGridOffsetY");
                    dtSettings.Columns.Add("ScaleSpielerGrid");
                    dtSettings.Columns.Add("ScaleKampfGrid");
                    dtSettings.Columns.Add("IsRechteckRaster");

                    //Zusätzliche Spalten 
                    dtSettings.Columns.Add("GridColorA");
                    dtSettings.Columns.Add("GridColorB");
                    dtSettings.Columns.Add("GridColorG");
                    dtSettings.Columns.Add("GridColorR");
                    dtSettings.Columns.Add("ShowSightArea");
                    dtSettings.Columns.Add("SightAreaLength");
                    dtSettings.Columns.Add("ShowCreatureName");
                    dtSettings.Columns.Add("UseFog");
                    dtSettings.Columns.Add("AktKampfrunde");
                    dtSettings.Columns.Add("IsEditorModeEnabled");

                    //Background Color
                    dtSettings.Columns.Add("BackgroundColorA");
                    dtSettings.Columns.Add("BackgroundColorB");
                    dtSettings.Columns.Add("BackgroundColorG");
                    dtSettings.Columns.Add("BackgroundColorR");

                    dtSettings.Rows.Add();
                    dtSettings.Rows[0]["BackgroundOffsetSize"] = Settings[0];
                    dtSettings.Rows[0]["BackgroundOffsetX"] = Settings[1];
                    dtSettings.Rows[0]["InvBackgroundOffsetY"] = Settings[2];

                    dtSettings.Rows[0]["PlayerGridOffsetX"] = Settings[3];
                    dtSettings.Rows[0]["PlayerGridOffsetY"] = Settings[4];
                    dtSettings.Rows[0]["ScaleSpielerGrid"] = Settings[5];
                    dtSettings.Rows[0]["ScaleKampfGrid"] = Settings[6];
                    dtSettings.Rows[0]["IsRechteckRaster"] = Settings[7];

                    //Zusätzliche Settings speichern
                    dtSettings.Rows[0]["GridColorA"] = Settings[8];
                    dtSettings.Rows[0]["GridColorB"] = Settings[9];
                    dtSettings.Rows[0]["GridColorG"] = Settings[10];
                    dtSettings.Rows[0]["GridColorR"] = Settings[11];

                    dtSettings.Rows[0]["ShowSightArea"] = Settings[12];
                    dtSettings.Rows[0]["SightAreaLength"] = Settings[13];
                    dtSettings.Rows[0]["ShowCreatureName"] = Settings[14];
                    dtSettings.Rows[0]["UseFog"] = Settings[15];
                    dtSettings.Rows[0]["AktKampfrunde"] = Settings[16];
                    dtSettings.Rows[0]["IsEditorModeEnabled"] = Settings[17];

                    //BackgroundColor
                    dtSettings.Rows[0]["BackgroundColorA"] = Settings[18];
                    dtSettings.Rows[0]["BackgroundColorB"] = Settings[19];
                    dtSettings.Rows[0]["BackgroundColorG"] = Settings[20];
                    dtSettings.Rows[0]["BackgroundColorR"] = Settings[21];

                    xmlH.AddObsDT(dtSettings);
                }
                using (StreamWriter wr = new StreamWriter(filename))
                {
                    xs.Serialize(wr, xmlH);
                    wr.Close();
                }
                if (GiveFeedback)
                    ViewHelper.Popup("Datei erfolgreich gespeichert");
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
            dt.Rows[dt.Rows.Count - 1]["CreaturePosition"] = (o as MeisterGeister.ViewModel.Kampf.Logic.IKämpfer).Position;// .CreaturePosition;
            dt.Rows[dt.Rows.Count - 1]["CreatureWidth"] = o.CreatureWidth;
            dt.Rows[dt.Rows.Count - 1]["CreatureX"] = o.CreatureX;
            dt.Rows[dt.Rows.Count - 1]["CreatureY"] = o.CreatureY;
            dt.Rows[dt.Rows.Count - 1]["PortraitFileName"] = o.PortraitFileName != null ? o.PortraitFileName : "";
            dt.Rows[dt.Rows.Count - 1]["ZDisplayX"] = o.ZDisplayX;
            dt.Rows[dt.Rows.Count - 1]["ZDisplayY"] = o.ZDisplayY;
            dt.Rows[dt.Rows.Count - 1]["ZLevel"] = o.ZLevel;
            dt.Rows[dt.Rows.Count - 1]["SightLineSektor"] = o.SightLineSektor;
            dt.Rows[dt.Rows.Count - 1]["HinweisText"] = (o as IKämpfer).HinweisText;
            dt.Rows[dt.Rows.Count - 1]["KämpferTempName"] = (o is Gegner) ? (o as Gegner).KämpferTempName: null;
            dt.Rows[dt.Rows.Count - 1]["ObjectSize"] = o.ObjectSize;

            dt.Rows[dt.Rows.Count - 1]["LebensenergieAktuell"] = (o is Gegner) ? (o as Gegner).LebensenergieAktuell : (o as Held).LebensenergieAktuell;
            dt.Rows[dt.Rows.Count - 1]["KarmaenergieAktuell"] = (o is Gegner) ? (o as Gegner).KarmaenergieAktuell : (o as Held).KarmaenergieAktuell;
            dt.Rows[dt.Rows.Count - 1]["AusdauerAktuell"] = (o is Gegner) ? (o as Gegner).AusdauerAktuell : (o as Held).AusdauerAktuell;
            dt.Rows[dt.Rows.Count - 1]["AstralenergieAktuell"] = (o is Gegner) ? (o as Gegner).AstralenergieAktuell : (o as Held).AstralenergieAktuell;
            dt.Rows[dt.Rows.Count - 1]["IstAnführer"] = (o as Wesen).ki.IstAnführer;

            dt.Rows[dt.Rows.Count - 1]["Wunden"] = (o is Gegner) ? (o as Gegner).Wunden : (o as Held).Wunden;
            dt.Rows[dt.Rows.Count - 1]["WundenByZoneUnlokalisiert"] = (o as IKämpfer).WundenByZone[Trefferzone.Unlokalisiert];
            dt.Rows[dt.Rows.Count - 1]["WundenByZoneArmL"] = (o as IKämpfer).WundenByZone[Trefferzone.ArmL];
            dt.Rows[dt.Rows.Count - 1]["WundenByZoneArmR"] = (o as IKämpfer).WundenByZone[Trefferzone.ArmR];
            dt.Rows[dt.Rows.Count - 1]["WundenByZoneBauch"] = (o as IKämpfer).WundenByZone[Trefferzone.Bauch];
            dt.Rows[dt.Rows.Count - 1]["WundenByZoneBeinL"] = (o as IKämpfer).WundenByZone[Trefferzone.BeinL];
            dt.Rows[dt.Rows.Count - 1]["WundenByZoneBeinR"] = (o as IKämpfer).WundenByZone[Trefferzone.BeinR];
            dt.Rows[dt.Rows.Count - 1]["WundenByZoneBrust"] = (o as IKämpfer).WundenByZone[Trefferzone.Brust];
            dt.Rows[dt.Rows.Count - 1]["WundenByZoneGesamt"] = (o as IKämpfer).WundenByZone[Trefferzone.Gesamt];
            dt.Rows[dt.Rows.Count - 1]["WundenByZoneKopf"] = (o as IKämpfer).WundenByZone[Trefferzone.Kopf];
            dt.Rows[dt.Rows.Count - 1]["WundenByZoneRücken"] = (o as IKämpfer).WundenByZone[Trefferzone.Rücken];

            dt.Rows[dt.Rows.Count - 1]["Initiative"] = (o as BattlegroundCreature).ki.Initiative;
            dt.Rows[dt.Rows.Count - 1]["GUID"] = (o is Gegner) ? (o as Gegner).GegnerBaseGUID : (o as Held).HeldGUID;
            dt.Rows[dt.Rows.Count - 1]["LichtquelleMeter"] = (o as BattlegroundCreature).ki.LichtquelleMeter;
            dt.Rows[dt.Rows.Count - 1]["IstUnsichtbar"] = (o as BattlegroundCreature).ki.IstUnsichtbar;
            dt.Rows[dt.Rows.Count - 1]["Angriffsaktionen"] = (o as BattlegroundCreature).ki.Angriffsaktionen;

            dt.Rows[dt.Rows.Count - 1]["IstImKampf"] = (o as BattlegroundCreature).ki.IstImKampf;
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
            dt.Columns.Add("SightLineSektor");
            dt.Columns.Add("HinweisText");
            dt.Columns.Add("KämpferTempName");
            dt.Columns.Add("ObjectSize");

            dt.Columns.Add("LebensenergieAktuell");
            dt.Columns.Add("KarmaenergieAktuell");
            dt.Columns.Add("AusdauerAktuell");
            dt.Columns.Add("AstralenergieAktuell");

            dt.Columns.Add("IstAnführer");
            dt.Columns.Add("Wunden");
            dt.Columns.Add("WundenByZoneUnlokalisiert");
            dt.Columns.Add("WundenByZoneArmL");
            dt.Columns.Add("WundenByZoneArmR");;
            dt.Columns.Add("WundenByZoneBauch");
            dt.Columns.Add("WundenByZoneBeinL");
            dt.Columns.Add("WundenByZoneBeinR");
            dt.Columns.Add("WundenByZoneBrust");
            dt.Columns.Add("WundenByZoneGesamt");
            dt.Columns.Add("WundenByZoneKopf");
            dt.Columns.Add("WundenByZoneRücken");

            dt.Columns.Add("Initiative");
            dt.Columns.Add("GUID");
            dt.Columns.Add("LichtquelleMeter");
            dt.Columns.Add("IstUnsichtbar");
            dt.Columns.Add("Angriffsaktionen");
            dt.Columns.Add("IstImKampf");
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

                            back.Add(Convert.ToDouble(drow["PlayerGridOffsetX"]));
                            back.Add(Convert.ToDouble(drow["PlayerGridOffsetY"]));
                            back.Add(Convert.ToDouble(drow["ScaleSpielerGrid"]));
                            back.Add(Convert.ToDouble(drow["ScaleKampfGrid"]));
                            back.Add(Convert.ToDouble(drow["IsRechteckRaster"]));

                            //Zusätzliche Daten laden
                            if (drow.ItemArray.Length > 8)
                            {
                                back.Add(Convert.ToDouble(drow["GridColorA"]));
                                back.Add(Convert.ToDouble(drow["GridColorB"]));
                                back.Add(Convert.ToDouble(drow["GridColorG"]));
                                back.Add(Convert.ToDouble(drow["GridColorR"]));

                                back.Add(Convert.ToDouble(drow["ShowSightArea"]));
                            }
                            if (drow.ItemArray.Length > 13)
                            {
                                back.Add(Convert.ToDouble(drow["SightAreaLength"]));
                                back.Add(Convert.ToDouble(drow["ShowCreatureName"]));
                                back.Add(Convert.ToDouble(drow["UseFog"]));
                                back.Add(Convert.ToInt32(drow["AktKampfrunde"]));
                                back.Add(Convert.ToDouble(drow["IsEditorModeEnabled"]));
                            }

                            if (drow.ItemArray.Length > 18)
                            {
                                back.Add(Convert.ToDouble(drow["BackgroundColorA"]));
                                back.Add(Convert.ToDouble(drow["BackgroundColorB"]));
                                back.Add(Convert.ToDouble(drow["BackgroundColorG"]));
                                back.Add(Convert.ToDouble(drow["BackgroundColorR"]));
                            }
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

                    Nullable<bool> HeldWerteAnpassen = null;
                    foreach (var dt in loadedFile.ObsDT)
                    {
                        if (dt.TableName == "Settings") continue;
                        DataRow drow = dt.Rows[0];

                        BattlegroundBaseObject bObj = Global.ContextHeld.HeldenGruppeListe.FirstOrDefault(t => t.ToString() == dt.TableName);
                        if (bObj != null && Global.CurrentKampf.Kampf.KämpferIList.FirstOrDefault(t => t.Kämpfer == bObj as IKämpfer) == null)
                        {
                            Held held = bObj as Held;
                            //KämpferInfo ki = null;
                            //if (!Global.CurrentKampf.Kampf.Kämpfer.Any(k => k.Kämpfer == held))
                            //{
                            //    ki = new KämpferInfo(held, Global.CurrentKampf.Kampf);
                            //    Global.CurrentKampf.Kampf.Kämpfer.Add(held);
                            //    if (Global.CurrentKampf.Kampf.Bodenplan.VM != null)
                            //        Global.CurrentKampf.Kampf.Bodenplan.VM.AddCreature(held);
                            //}
                            bObj = held;
                            //                   Global.ContextHeld.Insert<Model.Held>(bObj as Held);
                            //Global.CurrentKampf.Kampf.Kämpfer.Add(bObj as Held, 1);
                        }
                        if (bObj == null && drow.ItemArray.Length > 12)
                        {
                            GegnerBase gb = Global.ContextHeld.Liste<GegnerBase>().FirstOrDefault(t => t.GegnerBaseGUID.ToString() == drow["GUID"].ToString());
                            if (gb != null)
                            {
                                Gegner gegner = new Gegner(gb);
                                var gegner_name = gegner.Name;
                                int j = 1;
                                while (Global.CurrentKampf.Kampf.KämpferIList.Any(k => k.Kämpfer.Name == gegner_name))
                                    gegner_name = String.Format("{0} ({1})", gegner.Name, ++j);
                                gegner.Name = gegner_name;
                           
                                //GegnerToAdd = null;
                                bObj = gegner;


                        //        Gegner gegner = new Model.Gegner(gb);
                        //        bObj = gegner;
                        //        Global.ContextHeld.Insert<Model.Gegner>(gegner);
                        //        Global.CurrentKampf.Kampf.Kämpfer.Add(gegner, 2);
                            }                            
                        }
                        if (bObj != null)
                        {
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
                            if (drow.ItemArray.Length > 12)
                            {
                                (bObj as BattlegroundCreature).SightLineSektor = Convert.ToInt32(drow["SightLineSektor"]);

                                (bObj as IKämpfer).HinweisText = drow["HinweisText"].ToString();
                                (bObj as BattlegroundCreature).ObjectSize = Convert.ToDouble(drow["ObjectSize"]);

                                if (bObj is Gegner)
                                {
                                    (bObj as Gegner).LebensenergieAktuell = Convert.ToInt32(drow["LebensenergieAktuell"]);
                                    (bObj as Gegner).KarmaenergieAktuell = Convert.ToInt32(drow["KarmaenergieAktuell"]);
                                    (bObj as Gegner).AusdauerAktuell = Convert.ToInt32(drow["AusdauerAktuell"]);
                                    (bObj as Gegner).AstralenergieAktuell = Convert.ToInt32(drow["AstralenergieAktuell"]);

                                    if (drow.Table.Columns.Contains("KämpferTempName"))
                                        (bObj as Gegner).KämpferTempName = drow["KämpferTempName"].ToString();
                             //       (bObj as Wesen).ki.IstAnführer = Convert.ToBoolean(drow["IstAnführer"]);

                                    (bObj as IKämpfer).keineWeiterenAuswirkungenBeiWunden = true;
                                    (bObj as Gegner).Wunden = Convert.ToInt32(drow["Wunden"]);
                                    (bObj as IKämpfer).WundenByZone[Trefferzone.Unlokalisiert] = Convert.ToInt32(drow["WundenByZoneUnlokalisiert"]);
                                    (bObj as IKämpfer).WundenByZone[Trefferzone.ArmL] = Convert.ToInt32(drow["WundenByZoneArmL"]);
                                    (bObj as IKämpfer).WundenByZone[Trefferzone.ArmR] = Convert.ToInt32(drow["WundenByZoneArmR"]);
                                    (bObj as IKämpfer).WundenByZone[Trefferzone.Bauch] = Convert.ToInt32(drow["WundenByZoneBauch"]);
                                    (bObj as IKämpfer).WundenByZone[Trefferzone.BeinL] = Convert.ToInt32(drow["WundenByZoneBeinL"]);
                                    (bObj as IKämpfer).WundenByZone[Trefferzone.BeinR] = Convert.ToInt32(drow["WundenByZoneBeinR"]);
                                    (bObj as IKämpfer).WundenByZone[Trefferzone.Brust] = Convert.ToInt32(drow["WundenByZoneBrust"]);
                                    (bObj as IKämpfer).WundenByZone[Trefferzone.Gesamt] = Convert.ToInt32(drow["WundenByZoneGesamt"]);
                                    (bObj as IKämpfer).WundenByZone[Trefferzone.Kopf] = Convert.ToInt32(drow["WundenByZoneKopf"]);
                                    (bObj as IKämpfer).WundenByZone[Trefferzone.Rücken] = Convert.ToInt32(drow["WundenByZoneRücken"]);
                                    (bObj as IKämpfer).keineWeiterenAuswirkungenBeiWunden = false;
                              //      (bObj as Wesen).ki.Initiative = Convert.ToInt32(drow["Initiative"]);
                              //      if (drow.Table.Columns.Contains("Angriffsaktionen"))
                              //          (bObj as Wesen).ki.Angriffsaktionen = Convert.ToInt32(drow["Angriffsaktionen"]);
                                }
                                else
                                {
                                    if (!HeldWerteAnpassen.HasValue)
                                        HeldWerteAnpassen = ViewHelper.Confirm("Heldenwerte anpassen", "Die Battlemap-Datei enthält Werte der Helden." + Environment.NewLine +
                                            "Hierzu gehören aktuelle LeP, KaP, AuD, AsP, Anführer-Info und Wunden." + Environment.NewLine + Environment.NewLine +
                                            "Sollen die Werte der Battlemap-Datei benutzt werden?" + Environment.NewLine + Environment.NewLine + 
                                            "ACHTUNG!  Diese überschreiben die aktuellen Heldenwerte der enthaltenen Helden");
                                    if (HeldWerteAnpassen.Value)
                                    {
                                        (bObj as Held).LebensenergieAktuell = Convert.ToInt32(drow["LebensenergieAktuell"]);
                                        (bObj as Held).KarmaenergieAktuell = Convert.ToInt32(drow["KarmaenergieAktuell"]);
                                        (bObj as Held).AusdauerAktuell = Convert.ToInt32(drow["AusdauerAktuell"]);
                                        //(bObj as Held).AusdauerAktuell = Convert.ToInt32(drow["AusdauerMax"]);
                                        (bObj as Held).AstralenergieAktuell = Convert.ToInt32(drow["AstralenergieAktuell"]);

                                        (bObj as IKämpfer).keineWeiterenAuswirkungenBeiWunden = true;
                                        if (drow["Wunden"].ToString() != "")
                                            (bObj as Held).Wunden = Convert.ToInt32(drow["Wunden"]);
                                        (bObj as IKämpfer).WundenByZone[Trefferzone.Unlokalisiert] = Convert.ToInt32(drow["WundenByZoneUnlokalisiert"]);
                                        (bObj as IKämpfer).WundenByZone[Trefferzone.ArmL] = Convert.ToInt32(drow["WundenByZoneArmL"]);
                                        (bObj as IKämpfer).WundenByZone[Trefferzone.ArmR] = Convert.ToInt32(drow["WundenByZoneArmR"]);
                                        (bObj as IKämpfer).WundenByZone[Trefferzone.Bauch] = Convert.ToInt32(drow["WundenByZoneBauch"]);
                                        (bObj as IKämpfer).WundenByZone[Trefferzone.BeinL] = Convert.ToInt32(drow["WundenByZoneBeinL"]);
                                        (bObj as IKämpfer).WundenByZone[Trefferzone.BeinR] = Convert.ToInt32(drow["WundenByZoneBeinR"]);
                                        (bObj as IKämpfer).WundenByZone[Trefferzone.Brust] = Convert.ToInt32(drow["WundenByZoneBrust"]);
                                        (bObj as IKämpfer).WundenByZone[Trefferzone.Gesamt] = Convert.ToInt32(drow["WundenByZoneGesamt"]);
                                        (bObj as IKämpfer).WundenByZone[Trefferzone.Kopf] = Convert.ToInt32(drow["WundenByZoneKopf"]);
                                        (bObj as IKämpfer).WundenByZone[Trefferzone.Rücken] = Convert.ToInt32(drow["WundenByZoneRücken"]);
                                        (bObj as IKämpfer).keineWeiterenAuswirkungenBeiWunden = false;
                                        if (Global.CurrentKampf.Kampf.KämpferIList.FirstOrDefault(t => t.Kämpfer == bObj as IKämpfer) != null)
                                            Global.CurrentKampf.Kampf.KämpferIList.FirstOrDefault(t => t.Kämpfer == bObj as IKämpfer).Initiative = Convert.ToInt32(drow["Initiative"]);
                                    }
              
                                }

                            }
                        }

                        if (bObj is Gegner)
                        {
                            Global.ContextHeld.Insert<Gegner>(bObj as Gegner);
                            Global.CurrentKampf.Kampf.KämpferIList.Add(bObj as Gegner, 2);

                            (bObj as Wesen).ki.IstAnführer = Convert.ToBoolean(drow["IstAnführer"]);
                            (bObj as Wesen).ki.Initiative = Convert.ToInt32(drow["Initiative"]);
                            if (drow.Table.Columns.Contains("Angriffsaktionen"))
                                (bObj as Wesen).ki.Angriffsaktionen = Convert.ToInt32(drow["Angriffsaktionen"]);

                            try
                            {
                                (bObj as BattlegroundCreature).ki.LichtquelleMeter = Convert.ToDouble(drow["LichtquelleMeter"]);
                                (bObj as Wesen).ki.IstUnsichtbar = Convert.ToBoolean(drow["IstUnsichtbar"]);
                                if (drow.Table.Columns.Contains("IstImKampf") && Convert.ToBoolean(drow["IstImKampf"]) == false)
                                    (bObj as Wesen).ki.IstImKampf = Convert.ToBoolean(drow["IstImKampf"]);
                            }
                            catch { }
                            // zur Arena hinzufügen
                            //if (Global.CurrentKampf.Kampf.Bodenplan.VM != null)
                            //    Global.CurrentKampf.Kampf.Bodenplan.VM.AddCreature(bObj as Gegner);
                        }
                        else
                        if (bObj is Held)
                        {
                            KämpferInfo ki = null;
                            if (!Global.CurrentKampf.Kampf.KämpferIList.Any(k => k.Kämpfer == bObj))
                            {
                                ki = new KämpferInfo(bObj as Held, Global.CurrentKampf.Kampf);
                                Global.CurrentKampf.Kampf.KämpferIList.Add(bObj as Held);

                                (bObj as Wesen).ki.IstAnführer = Convert.ToBoolean(drow["IstAnführer"]);
                                (bObj as Held).ki.Initiative = Convert.ToInt32(drow["Initiative"]);
                                if (drow.Table.Columns.Contains("Angriffsaktionen"))
                                    (bObj as Held).ki.Angriffsaktionen = Convert.ToInt32(drow["Angriffsaktionen"]);

                                try
                                {
                                    (bObj as BattlegroundCreature).ki.LichtquelleMeter = Convert.ToDouble(drow["LichtquelleMeter"]);
                                    (bObj as Wesen).ki.IstUnsichtbar = Convert.ToBoolean(drow["IstUnsichtbar"]);
                                    if (drow.Table.Columns.Contains("IstImKampf") && Convert.ToBoolean(drow["IstImKampf"]) == false)
                                        (bObj as Wesen).ki.IstImKampf = Convert.ToBoolean(drow["IstImKampf"]);
                                }
                                catch { }
                                //if (Global.CurrentKampf.Kampf.Bodenplan.VM != null)
                                //    Global.CurrentKampf.Kampf.Bodenplan.VM.AddCreature(bObj as Held);
                            }
                        }
                    }
                    stream.Close();
                    Global.CurrentKampf.BodenplanViewModel.AddAllCreatures();

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
    [System.Xml.Serialization.XmlInclude(typeof(MP4Object))]
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
