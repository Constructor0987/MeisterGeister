using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MeisterGeister.Daten;
using MeisterGeister.Model;
using MeisterGeister.View.General;
//Eigene usings
using MeisterGeister.ViewModel.MeisterSpicker.Logic;
using Base = MeisterGeister.ViewModel.Base;

namespace MeisterGeister.ViewModel.Foundry
{
    public class FoundryViewModel : Base.ToolViewModelBase
    {

        public class MyTimer
        {
            static int start = 0;
            static int stop = 0;
            public static void start_timer()
            {
                start = Environment.TickCount;
            }
            public static void stop_timer()
            {
                stop_timer("");
            }
            public static void stop_timer(string msg)
            {
                stop = Environment.TickCount;
                print(msg);
            }
            private static void print(string msg)
            {
                string output = "MyTimer(" + msg + "): " + (stop - start) + " Millisekunden";
                System.Diagnostics.Debug.WriteLine(output);
            }
        }

        #region //---- Variablen ----
                
        List<HeldenArgument> lstHeldArgument = new List<HeldenArgument>();
        public class dbArgument
        {
            public string Argument
            { get; set; }
            public string Prefix
            { get; set; }
            public string Suffix
            { get; set; }
            public string ArgString
            { get; set; }
        }

        public class HeldenArgument
        {
            public Held h
            { get; set; }
            public List<dbArgument> lstArguments
            { get; set; }
            public string outcome
            { get; set; }
        }

        private Held _selectedDBHeld = null;
        public Held SelectedDBHeld
        {
            get { return _selectedDBHeld; }
            set { Set(ref _selectedDBHeld, value); }
        }

        private string _tokenName = null;
        public string TokenName
        {
            get { return _tokenName; }
            set { Set(ref _tokenName, value); }
        }

        private List<string> _lstdisplayName = new List<string>();
        public List<string> lstDisplayName
        {
            get { return _lstdisplayName; }
            set { Set(ref _lstdisplayName, value); }
        }

        private string _displayName = null;
        public string DisplayName
        {
            get { return _displayName; }
            set { Set(ref _displayName, value); }
        }

        private List<string> _lstRepresentedName = new List<string>();
        public List<string> lstRepresentedName
        { 
            get { return _lstRepresentedName; }
            set { Set(ref _lstRepresentedName, value); }
        }

        private string _representedName = null;
        public string RepresentedName
        {
            get { return _representedName; }
            set { Set(ref _representedName, value); }
        }

        private bool _linkActorData = false;
        public bool LinkActorData
        {
            get { return _linkActorData; }
            set { Set(ref _linkActorData, value); }
        }

        private List<string> _lstTokenDisposition = new List<string>();
        public List<string> lstTokenDisposition
        {
            get { return _lstTokenDisposition; }
            set { Set(ref _lstTokenDisposition, value); }
        }

        private string _tokenDisposition = null;
        public string TokenDisposition
        {
            get { return _tokenDisposition; }
            set { Set(ref _tokenDisposition, value); }
        }

        #endregion

        #region //---- FELDER ----


        #endregion

        #region //---- EIGENSCHAFTEN ----

        #endregion

        #region //---- LISTEN ----

        #endregion

        #region //---- KONSTRUKTOR ----

        public FoundryViewModel()
        {
            string appFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = Path.Combine(appFolderPath, @"\Local\FoundryVTT\Data\");

            Init();
            lstRepresentedName.AddRange(lstHeldArgument.Select(t => t.h.Name));
            lstDisplayName.AddRange(new List<string>() {
                "Never Displayed", "When Controlled", "Hovered by Owner", "Hovered by Anyone", "Always for Owner", "Always for Everyone" });
            DisplayName = lstDisplayName.First();
            LinkActorData = false;
            lstTokenDisposition = new List<string>() { "Neutral", "Friendly", "Hostile" };
            TokenDisposition = lstTokenDisposition.Last();

            foreach (HeldenArgument harg in lstHeldArgument)
            {
                string charBild = harg.h.Bild;
                if (!File.Exists(path + System.IO.Path.GetFileName(charBild)))
                {
     //               File.Copy(charBild, path + System.IO.Path.GetFileName(charBild), true);
                }
            }
        }

        #endregion

        #region //---- INSTANZMETHODEN ----
            /*
             * 
             * 
             *              
            {"_id":"9yO8vvLKbGvn1Bge","name":"Gegner1","permission":{"default":0,"3WHJiGe2LNC2VNeR":3},"type":"character","data":{"biography":"","stats":{"health":{"value":0,"min":0,"max":0},"endurance":{"value":0,"min":0,"max":0},"astral":{"value":0,"min":0,"max":0},"karmal":{"value":0,"min":0,"max":0},"magic_resistance":{"value":0},"speed":{"value":8},"INI":{"value":0},"AT":{"value":0},"PA":{"value":0},"FK":{"value":0},"WS":{"value":0}},"attributes":{"MU":{"start":8,"mod":0,"current":8},"KL":{"start":8,"mod":0,"current":8},"IN":{"start":8,"mod":0,"current":8},"CH":{"start":8,"mod":0,"current":8},"FF":{"start":8,"mod":0,"current":8},"GE":{"start":8,"mod":0,"current":8},"KO":{"start":8,"mod":0,"current":8},"KK":{"start":8,"mod":0,"current":8}}},"sort":100001,"flags":{"core":{"sourceId":"Compendium.world.dsa-gegner.KBxxxx0000000001"}},"img":"icons/svg/mystery-man.svg","token":{"flags":{},"name":"Albion","displayName":0,"img":"icons/svg/mystery-man.svg","tint":null,"width":1,"height":1,"scale":1,"lockRotation":false,"rotation":0,"vision":false,"dimSight":0,"brightSight":0,"dimLight":0,"brightLight":0,"sightAngle":360,"lightAngle":360,"lightAlpha":1,"lightAnimation":{"speed":5,"intensity":5},"actorId":"9yO8vvLKbGvn1Bge","actorLink":false,"disposition":-1,"displayBars":0,"bar1":{},"bar2":{},"randomImg":false},"items":[],"effects":[]}
                     
            */
            //    "{{\"_id\":\"9yO8vvLKbGvn1Bge\",\"name\":\"Gegner1\",\"permission\":{\"default\":0,\"3WHJiGe2LNC2VNeR\":3},\"type\":\"character\",\"data\":{\"biography\":\"\",\"stats\":{\"health\":{\"value\":0,\"min\":0,\"max\":0},\"endurance\":{\"value\":0,\"min\":0,\"max\":0},\"astral\":{\"value\":0,\"min\":0,\"max\":0},\"karmal\":{\"value\":0,\"min\":0,\"max\":0},\"magic_resistance\":{\"value\":0},\"speed\":{\"value\":8},\"INI\":{\"value\":0},\"AT\":{\"value\":0},\"PA\":{\"value\":0},\"FK\":{\"value\":0},\"WS\":{\"value\":0}},\"attributes\":{\"MU\":{\"start\":8,\"mod\":0,\"current\":8},\"KL\":{\"start\":8,\"mod\":0,\"current\":8},\"IN\":{\"start\":8,\"mod\":0,\"current\":8},\"CH\":{\"start\":8,\"mod\":0,\"current\":8},\"FF\":{\"start\":8,\"mod\":0,\"current\":8},\"GE\":{\"start\":8,\"mod\":0,\"current\":8},\"KO\":{\"start\":8,\"mod\":0,\"current\":8},\"KK\":{\"start\":8,\"mod\":0,\"current\":8}}},\"sort\":100001,\"flags\":{\"core\":{\"sourceId\":\"Compendium.world.dsa - gegner.KBxxxx0000000001\"}},\"img\":\"icons / svg / mystery - man.svg\",\"token\":{\"flags\":{},\"name\":\"Albion\",\"displayName\":0,\"img\":\"icons / svg / mystery - man.svg\",\"tint\":null,\"width\":1,\"height\":1,\"scale\":1,\"lockRotation\":false,\"rotation\":0,\"vision\":false,\"dimSight\":0,\"brightSight\":0,\"dimLight\":0,\"brightLight\":0,\"sightAngle\":360,\"lightAngle\":360,\"lightAlpha\":1,\"lightAnimation\":{\"speed\":5,\"intensity\":5},\"actorId\":\"9yO8vvLKbGvn1Bge\",\"actorLink\":false,\"disposition\":-1,\"displayBars\":0,\"bar1\":{},\"bar2\":{},\"randomImg\":false},\"items\":[],\"effects\":[]}}");
                
        public void Init()
        {
            lstHeldArgument.Clear();
            MyTimer.start_timer();
            foreach (Held h in Global.ContextHeld.HeldenGruppeListe)
            {
                HeldenArgument hArg = new HeldenArgument();
                hArg.h = h;
                hArg.lstArguments = new List<dbArgument>();
                hArg.lstArguments.Add(new dbArgument { Prefix = "{{\"_id\":\"", ArgString = h.HeldGUID.ToString(), Suffix = "\"," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"name\":\"", ArgString = h.Name, Suffix = "\"," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"permission\":{{\"default\":", ArgString = "0,\"3WHJiGe2LNC2VNeR\":", Suffix = "3}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"type\":\"", ArgString = "character", Suffix = "\"," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "{{\"data\":{{\"biography\":\"", ArgString = "", Suffix = "\"," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"stats\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"health\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"value\":\"", ArgString = h.LebensenergieAktuell.ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"min\":\"", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"max\":\"", ArgString = h.LebensenergieMax.ToString(), Suffix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"endurance\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"value\":\"", ArgString = h.AusdauerAktuell.ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"min\":\"", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"max\":\"", ArgString = h.AusdauerMax.ToString(), Suffix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"astral\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"value\":\"", ArgString = h.AstralenergieAktuell.ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"min\":\"", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"max\":\"", ArgString = h.AstralenergieMax.ToString(), Suffix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"karmal\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"value\":\"", ArgString = h.KarmaenergieAktuell.ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"min\":\"", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"max\":\"", ArgString = h.KarmaenergieMax.ToString(), Suffix = "}}," });

                hArg.lstArguments.Add(new dbArgument { Prefix = "\"magic_restistance\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"value\":\"", ArgString = h.Magieresistenz.ToString(), Suffix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"speed\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"value\":\"", ArgString = h.Geschwindigkeit.ToString(), Suffix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"INI\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"value\":\"", ArgString = h.Initiative(false).ToString(), Suffix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"AT\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"value\":\"", ArgString = (h.AT ?? 0).ToString(), Suffix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"PA\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"value\":\"", ArgString = (h.PA ?? 0).ToString(), Suffix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"FK\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"value\":\"", ArgString = h.FernkampfBasis.ToString(), Suffix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"WS\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"value\":\"", ArgString = h.Wundschwelle.ToString(), Suffix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"attributes\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"MU\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"start\":\"", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"mod\":\"", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"current\":\"", ArgString = (h.MU ?? 0).ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"KL\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"start\":\"", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"mod\":\"", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"current\":\"", ArgString = (h.KL ?? 0).ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"IN\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"start\":\"", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"mod\":\"", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"current\":\"", ArgString = (h.IN ?? 0).ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"CH\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"start\":\"", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"mod\":\"", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"current\":\"", ArgString = (h.CH ?? 0).ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"FF\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"start\":\"", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"mod\":\"", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"current\":\"", ArgString = (h.FF ?? 0).ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"GE\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"start\":\"", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"mod\":\"", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"current\":\"", ArgString = (h.GE ?? 0).ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"KO\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"start\":\"", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"mod\":\"", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"current\":\"", ArgString = (h.KO ?? 0).ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"KK\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"start\":\"", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"mod\":\"", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"current\":\"", ArgString = (h.KK ?? 0).ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}}}}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"sort\":", ArgString = "100001", Suffix = "," });

                hArg.lstArguments.Add(new dbArgument { Prefix = "\"flags\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"core\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"sourceId\":\"", ArgString = "Compendium.world.dsa-gegner.KBxxxx0000000001", Suffix = "\"}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}," });

                hArg.lstArguments.Add(new dbArgument { Prefix = "\"img\":\"", ArgString = "icons/svg/mystery-man.svg", Suffix = "\"," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"token\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"flags\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"name\":\"", ArgString = h.Name, Suffix = "\"," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"displayName\":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"img\":\"", ArgString = "icons/svg/mystery-man.svg", Suffix = "\"," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"tint\":", ArgString = "null", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"width\":", ArgString = "1", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"height\":", ArgString = "1", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"scale\":", ArgString = "1", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"lockRotation\":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"vision\":", ArgString = "false", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"dimSight\":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"brightSight\":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"dimLight\":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"brightLight\":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"sightAngle\":", ArgString = "360", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"lightAngle\":", ArgString = "360", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"lightAnimation\":{{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"speed\":", ArgString = "5", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"intensity\":", ArgString = "5" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"actorId\":\"", ArgString = h.HeldGUID.ToString(), Suffix = "\"," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"actorLink\":", ArgString = "false", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"disposition\":", ArgString = "-1", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"displayBars\":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"bar1\":{{", ArgString = "", Suffix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"bar2\":{{", ArgString = "", Suffix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"randomImg\":", ArgString = "false", Suffix = "" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"items\":[", ArgString = "", Suffix = "]," });
                hArg.lstArguments.Add(new dbArgument { Prefix = "\"effects\":[", ArgString = "", Suffix = "]" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}" });

                hArg.outcome = "";
                foreach (dbArgument arg in hArg.lstArguments)
                { hArg.outcome += arg.Prefix + arg.ArgString + arg.Suffix; }

                lstHeldArgument.Add(hArg);
            }
            MyTimer.stop_timer("Helden-DB-Argument");
        }


        #endregion

        #region //---- EVENTS ----


        private Base.CommandBase _onBtnExportHelden = null;
        public Base.CommandBase OnBtnExportHelden
        {
            get
            {
                if (_onBtnExportHelden == null)
                    _onBtnExportHelden = new Base.CommandBase(ExportHelden, null);
                return _onBtnExportHelden;
            }
        }
        private void ExportHelden(object sender)
        {


            //string exdata = string.Format(
            //    "{{\"_id\":\"{0}\"," +
            //    "\"name\":\"{1}\"," +
            //    "\"permission\":{{\"default\":{2}," +
            //    "\"{3}\":{4}}}," +
            //    "\"type\":\"{5}\"," +
            //    "\"data\":{{\"biography\":\"{6}\"," +
            //    "\"stats\":{{\"health\":{{\"value\":{7}," +
            //    "\"min\":{8}," +
            //    "\"max\":{9}}}," +
            //    "\"endurance\":{{\"value\":{10}," +
            //    "\"min\":{11}," +
            //    "\"max\":{12}}}," +
            //    "\"astral\":{{\"value\":{13}," +
            //    "\"min\":{14}," +
            //    "\"max\":{15}}}," +
            //    "\"karmal\":{{\"value\":{16}," +
            //    "\"min\":{17}," +
            //    "\"max\":{18}}}," +
            //    "\"magic_resistance\":{{\"value\":{19}}}," +
            //    "\"speed\":{{\"value\":{20}}}," +
            //    "\"INI\":{{\"value\":{21}}}," +
            //    "\"AT\":{{\"value\":{22}}}," +
            //    "\"PA\":{{\"value\":{23}}}," +
            //    "\"FK\":{{\"value\":{24}}}," +
            //    "\"WS\":{{\"value\":{25}}}," +
            //    "\"attributes\":{{\"MU\":{{\"start\":{26}," +
            //    "\"mod\":{27}," +
            //    "\"current\":{28}}}," +
            //    "\"KL\":{{\"start\":{29}," +
            //    "\"mod\":{30}," +
            //    "\"current\":{31}}}," +
            //    "\"IN\":{{\"start\":{32}," +
            //    "\"mod\":{33}," +
            //    "\"current\":{34}}}," +
            //    "\"CH\":{{\"start\":{35}," +
            //    "\"mod\":{36}," +
            //    "\"current\":{37}}}," +
            //    "\"FF\":{{\"start\":{38}," +
            //    "\"mod\":{39}," +
            //    "\"current\":{40}}}," +
            //    "\"GE\":{{\"start\":{41}," +
            //    "\"mod\":{42}," +
            //    "\"current\":{43}}}," +
            //    "\"KO\":{{\"start\":{44}," +
            //    "\"mod\":{45}," +
            //    "\"current\":{46}}}," +
            //    "\"KK\":{{\"start\":{47}," +
            //    "\"mod\":{48}," +
            //    "\"current\":{49}}}}}," +
            //    "\"sort\":{50}," +

            //    //"\"flags\":{{{51}}}," +
            //    //"\"name\":\"{52}\"," +
            //    //"\"displayName\":{53}," +
            //    //"\"img\":\"{54}\"," +
            //    ////"\"token\":{{\"flags\":{{{53}}}," +
            //    ////"\"img\":\"{56}\"," +
            //    //"\"tint\":{55}," +
            //    //"\"width\":{56}," +
            //    //"\"height\":{57}," +
            //    //"\"scale\":{60}," +
            //    //"\"lockRotation\":{61}," +
            //    //"\"rotation\":{62}," +
            //    //"\"vision\":{63}," +
            //    //"\"dimSight\":{64}," +
            //    //"\"brightSight\":{65}," +
            //    //"\"dimLight\":{66}," +
            //    //"\"brightLight\":{67}," +
            //    //"\"sightAngle\":{68}," +
            //    //"\"lightAngle\":{69}," +
            //    //"\"lightAlpha\":{70}," +
            //    //"\"lightAnimation\":{{\"speed\":{71}," +
            //    //"\"intensity\":{72}}}," +
            //    //"\"actorId\":\"{73}\"," +
            //    //"\"actorLink\":{74}," +
            //    //"\"disposition\":{75}," +
            //    //"\"displayBars\":{76}," +
            //    //"\"bar1\":{{{77}}}," +
            //    //"\"bar2\":{{{78}}}," +
            //    //"\"randomImg\":{79}}}," +
            //    //"\"items\":[{80}]," +
            //    //"\"effects\":[{81}]}}}}",

            //    h.HeldGUID.ToString(),
            //    h.Name,
            //    0,
            //    "3WHJiGe2LNC2VNeR",
            //    3,
            //    "character",
            //    "",
            //    h.LebensenergieAktuell,
            //    0,
            //    h.LebensenergieMax,
            //    h.AusdauerAktuell,  //10
            //    0,
            //    h.AusdauerMax,
            //    h.AstralenergieAktuell,
            //    0,
            //    h.AstralenergieMax,
            //    h.KarmaenergieAktuell,
            //    0,
            //    h.KarmaenergieMax,
            //    h.Magieresistenz,
            //    h.Geschwindigkeit,//20
            //    h.Initiative(false).ToString(),
            //    h.AT??0,
            //    h.PA??0,  
            //    h.Fernkampf,
            //    h.Wundschwelle, //25
            //    8,
            //    0,
            //    h.MU,
            //    8,
            //    0,  //30
            //    h.KL,
            //    8,
            //    0,
            //    h.IN,
            //    8,
            //    0,
            //    h.CH,
            //    8,
            //    0,
            //    h.FF, //40
            //    8,
            //    0,
            //    h.GE,
            //    8,
            //    0,
            //    h.KO,
            //    8,
            //    0,
            //    h.KK,
            //    100001 //50
            //    //"",
            //    //"{{\"core\":{{\"sourceId\":\"Compendium.world.dsa - gegner.KBxxxx0000000001\"}}}}",
            //    //"icons/svg/mystery-man.svg\"",
            //    //"null",
            //    //1,
            //    //1,
            //    //1,
            //    //"false",
            //    //0,
            //    //"false",//60
            //    //0,
            //    //0,
            //    //0,
            //    //0,
            //    //360,
            //    //360,
            //    //1,
            //    //5,
            //    //5,
            //    //h.HeldGUID.ToString(),//70
            //    //"false",
            //    //-1,
            //    //0,
            //    //"",
            //    //"",
            //    //"false",
            //    //"",
            //    //""
            //    );

            ////"{{\"_id\":\"{0}\","+
            ////", h.HeldGUID.ToString());



        }

        #endregion
    }
    }
