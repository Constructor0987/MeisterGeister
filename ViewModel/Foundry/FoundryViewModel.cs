using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MeisterGeister.Daten;
using MeisterGeister.Model;
using MeisterGeister.View.General;
//Eigene usings
using MeisterGeister.ViewModel.MeisterSpicker.Logic;
using Base = MeisterGeister.ViewModel.Base;
using System.Net;
using MeisterGeister.Logic.Einstellung;

namespace MeisterGeister.ViewModel.Foundry
{
    public class FoundryViewModel : Base.ToolViewModelBase
    {

        //TODO:  Helden: Bars -Always visible, Lep, AsP
        //TODO:  Foundry Pfad vom User definierbar, read Options, set Web-Connection (lokal oder Inet auswählbar)
        //TODO:  SpielerScreen: Zeige WebBrowser

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
            public static string stop_timer(string msg)
            {
                stop = Environment.TickCount;
                return print(msg);
            }
            private static string print(string msg)
            {
                string output = "MyTimer(" + msg + "): " + (stop - start) + " Millisekunden";
                System.Diagnostics.Debug.WriteLine(output);
                return output;
            }
        }

        public class folder
        {
            public string name { get; set; }
            public string typ { get; set; }
            public string sorting { get; set; }
            public string color { get; set; }
            public string _id { get; set; }
        }

        #region //---- Variablen ----

        List<GegnerArgument> lstGegnerArgument = new List<GegnerArgument>();
        List<HeldenArgument> lstHeldArgument = new List<HeldenArgument>();
        List<PlaylistArgument> lstPListArgument = new List<PlaylistArgument>();

        private void ChangePath(string vonS, string inS)
        {
            if (GegnerPortraitPfad == null) return;
            string neu = null;
            neu = GegnerPortraitPfad.Replace(vonS, inS);
            if (neu != GegnerPortraitPfad)
            {
                GegnerPortraitPfad = neu;
                Einstellungen.SetEinstellung<string>("FoundryGegnerPortraitPfad", GegnerPortraitPfad);
            }
            neu = HeldPortraitPfad.Replace(vonS, inS);
            if (neu != HeldPortraitPfad)
            {
                HeldPortraitPfad = neu;
                Einstellungen.SetEinstellung<string>("FoundryHeldPortraitPfad", HeldPortraitPfad);
            }
            neu = GegnerTokenPfad.Replace(vonS, inS);
            if (neu != GegnerTokenPfad)
            {
                GegnerTokenPfad = neu;
                Einstellungen.SetEinstellung<string>("FoundryGegnerTokenPfad", GegnerTokenPfad);
            }
            neu = HeldTokenPfad.Replace(vonS, inS);
            if (neu != HeldTokenPfad)
            {
                HeldTokenPfad = neu;
                Einstellungen.SetEinstellung<string>("FoundryHeldTokenPfad", HeldTokenPfad);
            }            
        }

        private bool _isLokalInstalliert = false;
        public bool IsLocalInstalliert
        {
            get { return _isLokalInstalliert; }
            set 
            { 
                Set(ref _isLokalInstalliert, value);
                if (!value)
                {
                    FoundryPfad = FTPAdresse + "/Data/";
                    ReadFoundryOptions(string.Format("{0}/config/options.json", FTPAdresse));

                    ChangePath(@"\", "/");
                }
                else
                {
                    FoundryPfad = localFoundryPfad;
                    string appFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string path = appFolderPath + @"\FoundryVTT\Config\";
                    if (Directory.Exists(path))
                        ReadFoundryOptions(path + @"\options.json");

                    ChangePath("/", @"\");
                }
            }
        }

        #region //---- FTP ----

        private string _ftpAdresse = "ftp://195.114.11.154:21";
        public string FTPAdresse
        {
            get { return _ftpAdresse; }
            set 
            { 
                Set(ref _ftpAdresse, value);
                Einstellungen.SetEinstellung("FoundryFTPAdresse", value);
            }
        }

        private string _ftpUser = "juergen";
        public string FTPUser
        {
            get { return _ftpUser; }
            set 
            { 
                Set(ref _ftpUser, value);
                Einstellungen.SetEinstellung("FoundryFTPUser", value);
            }
        }

        private string _ftpPasswort= "FoundryVTT2021";
        public string FTPPasswort
        {
            get { return _ftpPasswort; }
            set 
            { 
                Set(ref _ftpPasswort, value);
                Einstellungen.SetEinstellung("FoundryFTPPasswort", value);
            }
        }

        private string _testDatei = @"C:\temp\Test.txt";
        public string TestDatei
        {
            get { return _testDatei; }
            set { Set(ref _testDatei, value); }
        }


        private Base.CommandBase _onBtnFTPConfig = null;
        public Base.CommandBase OnBtnFTPConfig
        {
            get
            {
                if (_onBtnFTPConfig == null)
                    _onBtnFTPConfig = new Base.CommandBase(FTPConfig, null);
                return _onBtnFTPConfig;
            }
        }
        private void FTPConfig(object sender)
        {
            string back = ViewHelper.InputDialog("FTP-Adresse", "Gebe die FTP-Adresse zu dem Server ein", FTPAdresse);
            if (!string.IsNullOrEmpty(back))
                FTPAdresse = back;
            back = ViewHelper.InputDialog("FTP-User", "Gebe den FTP-Usernamen zu dem Server ein", FTPUser);
            if (!string.IsNullOrEmpty(back))
                FTPUser = back;
            back = ViewHelper.InputDialog("FTP-Passwort", "Gebe das FTP-Passwort zu dem Server ein", "");
            if (!string.IsNullOrEmpty(back))
                FTPPasswort = back;
        }


        private Base.CommandBase _onBtnConnectFTP = null;
        public Base.CommandBase OnBtnConnectFTP
        {
            get
            {
                if (_onBtnConnectFTP == null)
                    _onBtnConnectFTP = new Base.CommandBase(ConnectFTP, null);
                return _onBtnConnectFTP;
            }
        }
        private void ConnectFTP(object sender)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPAdresse + "/Data/worlds/test.txt");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            request.Credentials = new NetworkCredential(FTPUser,FTPPasswort, "");


            // Copy the contents of the file to the request stream.
            byte[] fileContents;
            using (StreamReader sourceStream = new StreamReader(TestDatei))
            {
                fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            }

            request.ContentLength = fileContents.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            }
        }


        #endregion

        #region //---- Classes ----

        public class PlaylistArgument
        {
            public string GMid { get; set; }
            public string _id { get; set; }
            public string name { get; set; }
            public string permission { get; set; }  // default = 0 user:Gamemaster = 3
            public int sort { get; set; }
            public string flags { get; set; }

            public string preArg
            {
                get 
                {
                    char A = (char)34;
                    string arg = "{" + A + "_id"+A+":" + A + _id + A + "," +
                        A + "name" + A + ":" + A + name + A + "," +
                        A + "permission" + A + ":{" + A + "default" + A + ":0," + A + GMid + A + ":3}," +
                        A + "sort" + A + ":" + sort.ToString() + "," +
                        A + "flags" + A + ":{}";
                    return arg; 
                }
                //{
                  //  "_id":"DUBBfsfZPdvWN8Mn",
        //"name":"neu",
        //"permission":{"default":0,"gNZOk6idrMy6uSkk":3},
        //"sort":100001,
        //"flags":{},
            }
            public class SoundArg
            {
                public List<string> lstArg {get; set;} 
                public string lstTitel { get; set; }
            }

            /*
             * 
             {
            "_id":"hMKrznmD7vhNLDNq",
            "flags":{},
            "path":"Musik/_Stra%C3%9Fenmusik/104-miguel_angel_tallante--cruzada-oma.mp3",
            "repeat":false,
            "volume":0.35355339059327373,
            "name":"104-miguel_angel_tallante--cruzada-oma",
            "playing":false,
            "streaming":false
        }
             * 
             * 
             * 
             */
            public List<SoundArg> lstSounds { get; set; }

            public string postArg
            {
                get
                {
                    char A = (char)34;
                    string arg = A + "mode" + A + ":" + mode.ToString() + "," +
                        A + "playing" + A + ":" + playing + "}";
                    return arg; }
            }
            public int mode { get; set; }
            public string playing { get; set; }

            public string outtext
            {
                get {
                    char A = (char)34;
                    return preArg + "," + A + "sounds" + A + ":[" + string.Join(",", lstSounds.Select(t => t.lstTitel)) + "]," + postArg;
                }
            }
            
        // {"_id":"DUBBfsfZPdvWN8Mn",
        //"name":"neu",
        //"permission":{"default":0,"gNZOk6idrMy6uSkk":3},
        //"sort":100001,
        //"flags":{},
        //"sounds":[
        //        ]
        
        //"mode":1,
        //"playing":false}
    }
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

        public class GegnerArgument
        {
            public GegnerBase g
            { get; set; }
            public List<dbArgument> lstArguments
            { get; set; }
            public string outcome
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

        #endregion


        private string _gegnerPortraitPfad = null;
        public string GegnerPortraitPfad
        {
            get { return _gegnerPortraitPfad; }
            set 
            {
                string prevalue = value;
                if (prevalue != null && !prevalue.EndsWith("/") && !prevalue.EndsWith(@"\"))
                    prevalue = prevalue + (IsLocalInstalliert ? @"\" : "/");
                Set(ref _gegnerPortraitPfad, prevalue);
                Einstellungen.SetEinstellung<string>("FoundryGegnerPortraitPfad", GegnerPortraitPfad);
            }
        }


        private string _heldPortraitPfad = null;
        public string HeldPortraitPfad
        {
            get { return _heldPortraitPfad; }
            set
            {
                string prevalue = value;
                if (!prevalue.EndsWith("/") && !prevalue.EndsWith(@"\"))
                    prevalue = prevalue + (IsLocalInstalliert ? @"\" : "/");
                Set(ref _heldPortraitPfad, prevalue);
                Einstellungen.SetEinstellung<string>("FoundryHeldPortraitPfad", HeldPortraitPfad);
            }
        }

        private string _gegnerTokenPfad = null;
        public string GegnerTokenPfad
        {
            get { return _gegnerTokenPfad; }
            set
            {
                string prevalue = value;
                if (!prevalue.EndsWith("/") && !prevalue.EndsWith(@"\"))
                    prevalue = prevalue + (IsLocalInstalliert ? @"\" : "/");
                Set(ref _gegnerTokenPfad, prevalue);
                Einstellungen.SetEinstellung<string>("FoundryGegnerTokenPfad", GegnerTokenPfad);
            }
        }

        private string _heldTokenPfad = null;
        public string HeldTokenPfad
        {
            get { return _heldTokenPfad; }
            set
            {
                string prevalue = value;
                if (!prevalue.EndsWith("/") && !prevalue.EndsWith(@"\"))
                    prevalue = prevalue + (IsLocalInstalliert ? @"\" : "/");
                Set(ref _heldTokenPfad, prevalue);
                Einstellungen.SetEinstellung<string>("FoundryHeldTokenPfad", HeldTokenPfad);
            }
        }

        private string _musikPfad = null;
        public string MusikPfad
        {
            get { return _musikPfad; }
            set
            {
                string prevalue = value;
                if (!prevalue.EndsWith("/") && !prevalue.EndsWith(@"\"))
                    prevalue = prevalue + (IsLocalInstalliert ? @"\" : "/");
                Set(ref _musikPfad, prevalue);
                Einstellungen.SetEinstellung<string>("FoundryMusikPfad", MusikPfad);
            }
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

        private string _localFoundryPfad = null;
        public string localFoundryPfad
        {
            get { return _localFoundryPfad; }
            set { Set(ref _localFoundryPfad, value); }
        }


        private string _foundryPfad = null;
        public string FoundryPfad
        {
            get { return _foundryPfad; }
            set { Set(ref _foundryPfad, value); }
        }

        private string _playlistStatus = null;
        public string PlaylistStatus
        {
            get { return _playlistStatus; }
            set { Set(ref _playlistStatus, value); }
        }

        private bool _overwritePictureFile = false;
        public bool OverwritePictureFile
        {
            get { return _overwritePictureFile; }
            set { Set(ref _overwritePictureFile, value); }
        }
        private bool _overwritePlaylistFile = false;
        public bool OverwritePlaylistFile
        {
            get { return _overwritePlaylistFile; }
            set { Set(ref _overwritePlaylistFile, value); }
        }

        private bool _copyTitelFile = false;
        public bool CopyTitelFile
        {
            get { return _copyTitelFile; }
            set { Set(ref _copyTitelFile, value); }
        }

        private folder _selectedHeldenFolder= null;
        public folder SelectedHeldenFolder
        {
            get { return _selectedHeldenFolder; }
            set { Set(ref _selectedHeldenFolder, value); }
        }
        private folder _selectedGegnerFolder= null;
        public folder SelectedGegnerFolder
        {
            get { return _selectedGegnerFolder; }
            set { Set(ref _selectedGegnerFolder, value); }
        }

        private string _selectedWorld = null;
        public string SelectedWorld
        {
            get { return _selectedWorld; }
            set 
            { 
                Set(ref _selectedWorld, value);

                if (value != null)
                {
                    if (IsLocalInstalliert)
                        GetActorFolders(string.Format(@"{0}worlds\{1}\data\folders.db", FoundryPfad, value));
                    else
                        GetActorFolders(string.Format("{0}worlds/{1}/data/folders.db", FoundryPfad, value));
                }
            }
        }
        #endregion

        #region //---- FELDER ----


        #endregion

        #region //---- EIGENSCHAFTEN ----

        #endregion

        #region //---- LISTEN ----

        public List<GegnerBase> lstGegnerBase
        {
            get { return Global.ContextHeld.Liste<GegnerBase>().OrderBy(h => h.Name).ToList(); }
        }

        public List<Audio_Playlist> lstPlaylists
        {
            get { return Global.ContextAudio.PlaylistListe.OrderBy(h => h.Name).ToList(); }
        }

        private List<folder> _lstFolders = new List<folder>();
        public List<folder> lstFolders
        {
            get { return _lstFolders; }
            set { Set(ref _lstFolders, value); }
        }

        private List<string> _lstWorlds = new List<string>();
        public List<string> lstWorlds
        {
            get { return _lstWorlds; }
            set { Set(ref _lstWorlds, value); }
        }
        private int _portNo = 0;
        public int PortNo
        {
            get { return _portNo; }
            set { Set(ref _portNo, value); }
        }

        private CefSharp.Wpf.ChromiumWebBrowser _cWebBrowser = new CefSharp.Wpf.ChromiumWebBrowser();
        public CefSharp.Wpf.ChromiumWebBrowser cWebBrowser
        {
            get { return _cWebBrowser; }
            set 
            { 
                Set(ref _cWebBrowser, value);
                cWebBrowser.Address = LocalUri;
            }
        }

        private string _localUri = "http://192.168.178.181:30000/";
        public string LocalUri
        {
            get { return _localUri; }
            set { Set(ref _localUri, value); }
        }
        private string _inetUri = "http://1.2.3.4:30000/";
        public string InetUri
        {
            get { return _inetUri; }
            set { Set(ref _inetUri, value); }
        }
        #endregion

        #region //---- KONSTRUKTOR ----

        public FoundryViewModel()
        {
            FTPAdresse = Einstellungen.GetEinstellung<string>("FoundryFTPAdresse");
            FTPUser = Einstellungen.GetEinstellung<string>("FoundryFTPUser");
            FTPPasswort = Einstellungen.GetEinstellung<string>("FoundryFTPPasswort");

            IsLocalInstalliert = Einstellungen.GetEinstellung<bool>("IsLocalInstalliert");
            if (IsLocalInstalliert)
                FoundryPfad = localFoundryPfad;
            else
                FoundryPfad = FTPAdresse + "/data/";

            if (IsLocalInstalliert)
            {
                string appFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string path = appFolderPath + @"\FoundryVTT\Config\";

                if (Directory.Exists(path))
                    ReadFoundryOptions(path + @"\options.json");
            }
            else
            {
                ReadFoundryOptions(FTPAdresse + "/config/options.json");
            }

            GegnerPortraitPfad = Einstellungen.GetEinstellung<string>("FoundryGegnerPortraitPfad");
            HeldPortraitPfad = Einstellungen.GetEinstellung<string>("FoundryHeldPortraitPfad");
            GegnerTokenPfad = Einstellungen.GetEinstellung<string>("FoundryGegnerTokenPfad");
            HeldTokenPfad = Einstellungen.GetEinstellung<string>("FoundryHeldTokenPfad");
            MusikPfad = Einstellungen.GetEinstellung<string>("FoundryMusikPfad");

            Init();
            lstRepresentedName.AddRange(lstHeldArgument.Select(t => t.h.Name));
            lstDisplayName.AddRange(new List<string>() {
                "Never Displayed", "When Controlled", "Hovered by Owner", "Hovered by Anyone", "Always for Owner", "Always for Everyone" });
            DisplayName = lstDisplayName.First();
            LinkActorData = false;
            lstTokenDisposition = new List<string>() { "Neutral", "Friendly", "Hostile" };
            TokenDisposition = lstTokenDisposition.Last();

            stdPfad.AddRange(MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis.Split(new Char[] { '|' }));
        }

        private List<string> _stdPfad = new List<string>();
        public List<string> stdPfad
        {
            get { return _stdPfad; }
            set { Set(ref _stdPfad, value); }
        }

        private FtpWebRequest _listRequest = null;
        public FtpWebRequest listRequest
        {
            get { return _listRequest; }
            set { Set(ref _listRequest, value); }
        }

        #endregion

        #region //---- Funktionen ----
        void ListFtpDirectory(string url, string rootPath, bool onlyDir, NetworkCredential credentials, List<string> list)
        {
            StreamReader listReader = null;
            var listRequest = (FtpWebRequest)WebRequest.Create(url + rootPath);
            listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            listRequest.Credentials = credentials;
            listRequest.KeepAlive = false;
            listRequest.UsePassive = true;
            listRequest.Timeout= 4000;

            var lines = new List<string>();
            try
            {
                using (var listResponse = (FtpWebResponse)listRequest.GetResponse())
                using (var listStream = listResponse.GetResponseStream())
                using (listReader = new StreamReader(listStream))
                {
                    while (!listReader.EndOfStream)
                    {
                        lines.Add(listReader.ReadLine());
                        if (onlyDir)
                        {
                            string[] tokens =
                                lines.Last().Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                            string permissions = tokens[0];
                            string name = tokens[8];
                            if (permissions[0] == 'd')
                                list.Add(name);
                        }
                    }
                }

                if (!onlyDir)
                {
                    foreach (string line in lines)
                    {
                        string[] tokens =
                            line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                        string name = tokens[8];
                        string permissions = tokens[0];

                        string filePath = rootPath + name;

                        if (permissions[0] == 'd')
                        {
                            ListFtpDirectory(url, filePath + "/", onlyDir, credentials, list);
                        }
                        else
                        {
                            list.Add(filePath);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ViewHelper.ShowError(string.Format("Beim Lesen der FTP Seite {0} ist ein Fehler aufgetreten", url + rootPath), ex);
            }
            finally
            {
                if (listReader != null)
                    listReader.Close();
            }
        }
        private void LoadWorldsFolder()
        {
            if (IsLocalInstalliert)
            {
                lstWorlds.Clear();
                string worldPfad = FoundryPfad + @"worlds";
                List<string> lstFullDir = Directory.GetDirectories(worldPfad).ToList();
                lstFullDir.ForEach(delegate (string s)
                { lstWorlds.Add(new Uri(s).Segments.Last().ToUpper()); });
            }
            else
            {
                List<string> list = new List<string>();
                NetworkCredential credentials = new NetworkCredential(FTPUser, FTPPasswort);
                ListFtpDirectory(FTPAdresse + "/data/worlds/", "", true, credentials, list);
                lstWorlds = list;
            }
        }

        private void GetActorFolders(string filepath)
        {
            string FileData = GetFileData(filepath);

            if (FileData != null)
            { 
                List<string> lstFileData = FileData.Split(new Char[] { '\n' }).ToList();
                List<folder> lst = new List<folder>();

                lst.Add(new folder() { name = "" });
                foreach (string data in lstFileData)
                {
                    if (string.IsNullOrEmpty(data))
                        continue;
                    string d = data.Substring(1, data.Length - 3);
                    List<string> line = d.Split(new Char[] { ',' }).ToList();
                    string nameF = line.FirstOrDefault(t => t.StartsWith("\"name\":"));
                    string typF = line.FirstOrDefault(t => t.StartsWith("\"type\":"));
                    string sortingF = line.FirstOrDefault(t => t.StartsWith("\"sorting\":"));
                    string colorF = line.FirstOrDefault(t => t.StartsWith("\"color\":"));
                    string _idF = line.FirstOrDefault(t => t.StartsWith("\"_id\":"));

                    lst.Add(new folder()
                    {
                        name = nameF.Split(new Char[] { ':' }).Last().Replace("\"", ""),
                        typ = typF.Split(new Char[] { ':' }).Last().Replace("\"", ""),
                        sorting = sortingF.Split(new Char[] { ':' }).Last().Replace("\"", ""),
                        color = colorF?.Split(new Char[] { ':' }).Last().Replace("\"", ""),
                        _id = _idF.Split(new Char[] { ':' }).Last().Replace("\"", "")
                    });
                }
                lstFolders = lst;
            }
        }

        private void SetFileData(string file, string daten, bool datenInFile = false)
        {
            if (IsLocalInstalliert)
                File.WriteAllText(file, daten);
            else
            {
                string tempDatei = null;
                if (!datenInFile)
                {
                    tempDatei = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\tempData.txt";
                    File.WriteAllText(tempDatei, daten);
                }
                else
                    tempDatei = daten;

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(file);
                request.Credentials = new NetworkCredential(FTPUser, FTPPasswort);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                using (Stream fileStream = File.OpenRead(tempDatei))
                using (Stream ftpStream = request.GetRequestStream())
                {
                    fileStream.CopyTo(ftpStream);
                }

                if (!datenInFile)
                    File.Delete(tempDatei);
            }
        }
        private string GetFileData(string filepath)
        {
            string FileData = null;
            if (IsLocalInstalliert)
            {
                if (File.Exists(filepath))
                    FileData = File.ReadAllText(filepath).Trim();
            }
            else
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(filepath);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(FTPUser, FTPPasswort, "");
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                FileData = reader.ReadToEnd().Trim();
                Console.WriteLine($"Download Complete, status {response.StatusDescription}");
                reader.Close();
                response.Close();
            }
            return FileData;
        }
        private string GetUserID(string filepath, string username)
        {
            char A = (char)34;
            string id = null;
            string FileData = GetFileData(filepath);

            if (FileData != null)
            { 
                List<string> lstFileData = FileData.Split(new Char[] { '\n' }).ToList();
                string uLine = lstFileData.FirstOrDefault(t => t.Contains(A + "name" + A + ":" + A + username + A));
                string d = uLine.Substring(1, uLine.Length - 3);
                List<string> line = d.Split(new Char[] { ',' }).ToList();
                string _idF = line.FirstOrDefault(t => t.StartsWith(A + "_id" + A + ":"));
                id = _idF.Substring(7, _idF.Length - 8);
            }
            return id;
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        private void ReadFoundryOptions(string filepath)
        {
            string FileData = GetFileData(filepath);
            if (FileData != null)
            { 
                List<string> lstFileData = FileData.Split(new Char[] { '\n' }).ToList();

                string portLine = lstFileData.FirstOrDefault(t => t.StartsWith("  \"port\":"));
                portLine = portLine.Trim().TrimEnd(new Char[] { ',' });
                int portNo = 0;
                int.TryParse(portLine.Substring(portLine.IndexOf(":") + 1), out portNo);
                PortNo = portNo;

                if (IsLocalInstalliert)
                {
                    string dataPathLine = lstFileData.FirstOrDefault(t => t.StartsWith("  \"dataPath\":"));
                    dataPathLine = dataPathLine.Trim().TrimEnd(new Char[] { ',' }).Trim();
                    string dataPath = dataPathLine.Substring(dataPathLine.IndexOf(":") + 1).Trim().Trim(new Char[] { '"' });
                    dataPath = dataPath.Replace("/", @"\");
                    dataPath += @"\data\";
                    FoundryPfad = dataPath;
                    localFoundryPfad = FoundryPfad;
                }
            }
            else
                FoundryPfad = null;
        }
        private void BildSpeichern(string bildDateiname, string WesenPfad, string MGPfad, List<string> lstFTPGegnerPics, List<string> lstDateienKopiert)
        {
            if (!string.IsNullOrEmpty(bildDateiname) && !lstDateienKopiert.Contains(bildDateiname))
            {
                string charBild = bildDateiname;
                string srcCharFilename = System.IO.Path.GetFileName(charBild);
                if (bildDateiname.StartsWith("/"))
                {
                    BitmapImage bmpi1 = new BitmapImage(new Uri("pack://application:,,," + bildDateiname));
                    using (MemoryStream outStream = new MemoryStream())
                    {
                        PngBitmapEncoder enc = new PngBitmapEncoder();
                        enc.Frames.Add(BitmapFrame.Create(bmpi1));

                        FileStream fs = null;
                        if (IsLocalInstalliert)
                        {
                            if (!File.Exists(FoundryPfad + srcCharFilename) || OverwritePictureFile)
                                fs = File.Open(FoundryPfad + srcCharFilename, FileMode.Create);
                        }
                        else
                        {
                            //Speichern temporär ins MG-Verzeichnis 
                            if (!lstFTPGegnerPics.Contains(srcCharFilename) || OverwritePictureFile)
                                fs = File.Open(MGPfad + srcCharFilename, FileMode.Create);
                        }
                        if (fs != null)
                        {
                            enc.Save(fs);
                            fs.Close();
                        }
                        if (!IsLocalInstalliert)
                        {
                            if (!lstFTPGegnerPics.Contains(srcCharFilename) || OverwritePictureFile)
                                SetFileData(FoundryPfad + WesenPfad + System.IO.Path.GetFileName(charBild), MGPfad + srcCharFilename, true);
                            File.Delete(MGPfad + srcCharFilename);
                        }
                    }
                }
                else
                if (File.Exists(charBild))
                {
                    if (IsLocalInstalliert)
                    {
                        if (!File.Exists(FoundryPfad + srcCharFilename) || OverwritePictureFile)
                        {
                            File.Copy(charBild, FoundryPfad + srcCharFilename, OverwritePictureFile);
                        }
                    }
                    else
                    {
                        if (!lstFTPGegnerPics.Contains(srcCharFilename) || OverwritePictureFile)
                        {
                            SetFileData(FoundryPfad + WesenPfad + System.IO.Path.GetFileName(charBild), charBild, true);
                        }
                    }
                }
                lstDateienKopiert.Add(charBild);
            }
        }
        public void GetGegnerData()
        {
            lstGegnerArgument.Clear();
            MyTimer.start_timer();
            List<string> lstPicKopiert = new List<string>();
            List<string> lstTokenKopiert = new List<string>();
            string MGPfad = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\";

            List<string> lstFTPGegnerPics = new List<string>();
            if (!IsLocalInstalliert)
            {
                List<string> list = new List<string>();
                NetworkCredential credentials = new NetworkCredential(FTPUser, FTPPasswort);
                ListFtpDirectory(FoundryPfad + GegnerPortraitPfad, "", false, credentials, list);
                lstFTPGegnerPics = list;
            }

            foreach (GegnerBase g in lstGegnerBase)
            {
                BildSpeichern(g.Bild, GegnerPortraitPfad, MGPfad, lstFTPGegnerPics, lstPicKopiert);
                BildSpeichern(g.Bild, GegnerTokenPfad, MGPfad, lstFTPGegnerPics, lstTokenKopiert);

                string GetFilenamePortrait = string.IsNullOrEmpty(g.Bild) ? null: (GegnerPortraitPfad + System.IO.Path.GetFileName(g.Bild));
                //Todo: muss auf g.Token abgeändert werden, sobald Token DB vorhanden & GegnerTokenPfad
                string GetFilenameToken = string.IsNullOrEmpty(g.Bild) ? GetFilenamePortrait : (GegnerPortraitPfad + System.IO.Path.GetFileName(g.Bild));

                char A = (char)34; // (new Char[] { '"' });
                                       //4e1d3250-f700-3000-0001-387712958942  => 0001387712958942
                    string id = g.GegnerBaseGUID.ToString().Substring(19, 17).Replace("-", "");

                    GegnerArgument gArg = new GegnerArgument();
                    gArg.g = g;
                    gArg.lstArguments = new List<dbArgument>();

                    gArg.lstArguments = new List<dbArgument>();
                    gArg.lstArguments.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = id, Suffix = A + "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = g.Name, Suffix = A + "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "permission" + A + ":{\"default" + A + ":", ArgString = "0,\"3WHJiGe2LNC2VNeR" + A + ":", Suffix = "3}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "character", Suffix = A + "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "data" + A + ":{\"biography" + A + ":\"", ArgString = "", Suffix = A + "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "stats" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "health" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.LE.ToString(), Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = g.LE.ToString(), Suffix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "endurance" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.AU.ToString(), Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = g.AU.ToString(), Suffix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "astral" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.AE.ToString(), Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = g.AE.ToString(), Suffix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "karmal" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.KE.ToString(), Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = g.KE.ToString(), Suffix = "}," });

                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "magic_resistance" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = Math.Max(g.MRKörper.HasValue?g.MRKörper.Value:0, g.MRGeist).ToString(), Suffix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "speed" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = Math.Max(g.MRKörper.HasValue ? g.MRKörper.Value : 0, g.MRGeist).ToString(), Suffix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "INI" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.INIBasis.ToString(), Suffix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "AT" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.AT.ToString(), Suffix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "PA" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.PA.ToString(), Suffix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "FK" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = g.FK.ToString(), Suffix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "WS" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = ((int)g.KO/2).ToString(), Suffix = "}" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "attributes" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "MU" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (g.MU ?? 0).ToString() });
                    gArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "KL" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (g.KL ?? 0).ToString() });
                    gArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "IN" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (g.IN ?? 0).ToString() });
                    gArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "CH" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (g.CH ?? 0).ToString() });
                    gArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "FF" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (g.FF ?? 0).ToString() });
                    gArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "GE" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (g.GE ?? 0).ToString() });
                    gArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "KO" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = g.KO.ToString() });
                    gArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "KK" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (g.KK ?? 0).ToString() });
                    gArg.lstArguments.Add(new dbArgument { Prefix = "}}}," });

                    //Folder
                    if (SelectedGegnerFolder != null && !string.IsNullOrEmpty(SelectedGegnerFolder.name))
                    {
                        gArg.lstArguments.Add(new dbArgument { Prefix = A + "folder" + A + ":\"", ArgString = SelectedGegnerFolder._id, Suffix = "\"," });
                    }

                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "sort" + A + ":", ArgString = "100001", Suffix = "," });

                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "flags" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "core" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "sourceId" + A + ":\"", ArgString = "Compendium.world.dsa-gegner.KBxxxx0000000001", Suffix = A + "}" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = "}," });

                //Portrait Image
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "img" + A + ":\"", ArgString = 
                        (string.IsNullOrEmpty(GetFilenamePortrait) ? "icons/svg/mystery-man.svg" :
                         GetFilenamePortrait), Suffix = A + "," });
                    
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "token" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "flags" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = g.Name, Suffix = A + "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "displayName" + A + ":", ArgString = "0", Suffix = "," });
                    
                //Token Image
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "img" + A + ":\"", ArgString = 
                        (string.IsNullOrEmpty(GetFilenameToken) ? "icons/svg/mystery-man.svg" :
                        GetFilenameToken), Suffix = A + "," });
                    
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "tint" + A + ":", ArgString = "null", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "width" + A + ":", ArgString = "1", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "height" + A + ":", ArgString = "1", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "scale" + A + ":", ArgString = "1", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "lockRotation" + A + ":", ArgString = "false", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "vision" + A + ":", ArgString = "false", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "dimSight" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "brightSight" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "dimLight" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "brightLight" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "sightAngle" + A + ":", ArgString = "360", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "lightAngle" + A + ":", ArgString = "360", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "lightAlpha" + A + ":", ArgString = "1", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "lightAnimation" + A + ":{" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "speed" + A + ":", ArgString = "5", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "intensity" + A + ":", ArgString = "5" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "actorId" + A + ":\"", ArgString = id, Suffix = A + "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "actorLink" + A + ":", ArgString = "false", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "disposition" + A + ":", ArgString = "-1", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "displayBars" + A + ":", ArgString = "0", Suffix = "," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "bar1" + A + ":{", ArgString = "", Suffix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "bar2" + A + ":{", ArgString = "", Suffix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "randomImg" + A + ":", ArgString = "false", Suffix = "" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "items" + A + ":[", ArgString = "", Suffix = "]," });
                    gArg.lstArguments.Add(new dbArgument { Prefix = A + "effects" + A + ":[", ArgString = "", Suffix = "]" });
                    gArg.lstArguments.Add(new dbArgument { Prefix = "}" });

                    gArg.outcome = "";
                    foreach (dbArgument arg in gArg.lstArguments)
                    { gArg.outcome += arg.Prefix + arg.ArgString + arg.Suffix; }

                    lstGegnerArgument.Add(gArg);
                }
                MyTimer.stop_timer("Gegner-DB-Argument");
        }
        public void GetHeldenData()
        {
            lstHeldArgument.Clear();
            MyTimer.start_timer();

            List<string> lstPicKopiert = new List<string>();
            List<string> lstTokenKopiert = new List<string>();
            string MGPfad = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\";

            List<string> lstFTPHeldenPics = new List<string>();
            if (!IsLocalInstalliert)
            {
                List<string> list = new List<string>();
                NetworkCredential credentials = new NetworkCredential(FTPUser, FTPPasswort);
                ListFtpDirectory(FoundryPfad + HeldPortraitPfad, "", false, credentials, list);
                lstFTPHeldenPics = list;
            }

            foreach (Held h in Global.ContextHeld.HeldenGruppeListe.Where(t => t.AktiveHeldengruppe.Value))
            {
                BildSpeichern(h.Bild, HeldPortraitPfad, MGPfad, lstFTPHeldenPics, lstPicKopiert);
                BildSpeichern(h.Bild, HeldTokenPfad, MGPfad, lstFTPHeldenPics, lstTokenKopiert);

                string GetFilenamePortrait = string.IsNullOrEmpty(h.Bild) ? null : (HeldPortraitPfad + System.IO.Path.GetFileName(h.Bild));
                //Todo: muss auf g.Token abgeändert werden, sobald Token DB vorhanden & GegnerTokenPfad
                string GetFilenameToken = string.IsNullOrEmpty(h.Bild) ? GetFilenamePortrait : (HeldTokenPfad + System.IO.Path.GetFileName(h.Bild));

                char A = (char)34; // (new Char[] { '"' });
                //4e1d3250-f700-3000-0001-387712958942  => 0001387712958942
                string id = h.HeldGUID.ToString().Substring(19, 17).Replace("-", "");
                HeldenArgument hArg = new HeldenArgument();
                hArg.h = h;
                hArg.lstArguments = new List<dbArgument>();
                hArg.lstArguments.Add(new dbArgument { Prefix = "{" + A + "_id" + A + ":\"", ArgString = id, Suffix = A + "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = h.Name, Suffix = A + "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "permission" + A + ":{\"default" + A + ":", ArgString = "0,\"3WHJiGe2LNC2VNeR" + A + ":", Suffix = "3}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "type" + A + ":\"", ArgString = "character", Suffix = A + "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "data" + A + ":{\"biography" + A + ":\"", ArgString = "", Suffix = A + "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "stats" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "health" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.LebensenergieAktuell.ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = h.LebensenergieMax.ToString(), Suffix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "endurance" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.AusdauerAktuell.ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = h.AusdauerMax.ToString(), Suffix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "astral" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.AstralenergieAktuell.ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = h.AstralenergieMax.ToString(), Suffix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "karmal" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.KarmaenergieAktuell.ToString(), Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "min" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "max" + A + ":", ArgString = h.KarmaenergieMax.ToString(), Suffix = "}," });

                hArg.lstArguments.Add(new dbArgument { Prefix = A + "magic_resistance" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.Magieresistenz.ToString(), Suffix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "speed" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.Geschwindigkeit.ToString(), Suffix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "INI" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.Initiative(false).ToString(), Suffix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "AT" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = (h.AT ?? 0).ToString(), Suffix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "PA" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = (h.PA ?? 0).ToString(), Suffix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "FK" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.FernkampfBasis.ToString(), Suffix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "WS" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "value" + A + ":", ArgString = h.Wundschwelle.ToString(), Suffix = "}" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "attributes" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "MU" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (h.MU ?? 0).ToString() });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "KL" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (h.KL ?? 0).ToString() });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "IN" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (h.IN ?? 0).ToString() });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "CH" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (h.CH ?? 0).ToString() });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "FF" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (h.FF ?? 0).ToString() });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "GE" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (h.GE ?? 0).ToString() });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "KO" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (h.KO ?? 0).ToString() });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "KK" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "start" + A + ":", ArgString = "8", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "mod" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "current" + A + ":", ArgString = (h.KK ?? 0).ToString() });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}}}," });

                //Folder
                if (SelectedHeldenFolder != null && !string.IsNullOrEmpty(SelectedHeldenFolder.name))
                {
                    hArg.lstArguments.Add(new dbArgument { Prefix = A + "folder" + A + ":\"", ArgString = SelectedHeldenFolder._id, Suffix = "\"," });
                }
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "sort" + A + ":", ArgString = "100001", Suffix = "," });

                hArg.lstArguments.Add(new dbArgument { Prefix = A + "flags" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "core" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "sourceId" + A + ":\"", ArgString = "Compendium.world.dsa-gegner.KBxxxx0000000001", Suffix = A + "}" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}," });

                //Portrait Image
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "img" + A + ":\"", ArgString = 
                    (string.IsNullOrEmpty(GetFilenamePortrait) ? "icons/svg/mystery-man.svg" :
                     GetFilenamePortrait), Suffix = A + ","});

                hArg.lstArguments.Add(new dbArgument { Prefix = A + "token" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "flags" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "name" + A + ":\"", ArgString = h.Name, Suffix = A + "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "displayName" + A + ":", ArgString = "0", Suffix = "," });

                //Token Image
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "img" + A + ":\"", ArgString =
                    (string.IsNullOrEmpty(GetFilenameToken) ? "icons/svg/mystery-man.svg" :
                    GetFilenameToken),Suffix = A + "," });

                hArg.lstArguments.Add(new dbArgument { Prefix = A + "tint" + A + ":", ArgString = "null", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "width" + A + ":", ArgString = "1", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "height" + A + ":", ArgString = "1", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "scale" + A + ":", ArgString = "1", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "lockRotation" + A + ":", ArgString = "false", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "vision" + A + ":", ArgString = "true", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "dimSight" + A + ":", ArgString = "20", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "brightSight" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "dimLight" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "brightLight" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "sightAngle" + A + ":", ArgString = "180", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "lightAngle" + A + ":", ArgString = "180", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "lightAlpha" + A + ":", ArgString = "1", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "lightAnimation" + A + ":{" });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "speed" + A + ":", ArgString = "5", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "intensity" + A + ":", ArgString = "5" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "actorId" + A + ":\"", ArgString = id, Suffix = A + "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "actorLink" + A + ":", ArgString = "false", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "disposition" + A + ":", ArgString = "-1", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "displayBars" + A + ":", ArgString = "0", Suffix = "," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "bar1" + A + ":{", ArgString = "", Suffix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "bar2" + A + ":{", ArgString = "", Suffix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "randomImg" + A + ":", ArgString = "false", Suffix = "" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "items" + A + ":[", ArgString = "", Suffix = "]," });
                hArg.lstArguments.Add(new dbArgument { Prefix = A + "effects" + A + ":[", ArgString = "", Suffix = "]" });
                hArg.lstArguments.Add(new dbArgument { Prefix = "}" });

                hArg.outcome = "";
                foreach (dbArgument arg in hArg.lstArguments)
                { hArg.outcome += arg.Prefix + arg.ArgString + arg.Suffix; }

                lstHeldArgument.Add(hArg);
            }
            MyTimer.stop_timer("Helden-DB-Argument");
        }
        public void Init()
        {
            //Read Worlds
            if (string.IsNullOrEmpty(FoundryPfad) ||
                (IsLocalInstalliert && !Directory.Exists(FoundryPfad)))
                return;

            LoadWorldsFolder();
        }
        private void CreateFolder(object sender)
        {
            if (SelectedWorld == null)
            {
                ViewHelper.Popup("Wähle zuerst eine Welt");
                return;
            }
            string newFolder = ViewHelper.InputDialog("Erstelle Actor Verzeichnis", "Gebe den Namen des neuen Verzeichnisses\nfür die Actor ein", "");
            
            if (string.IsNullOrEmpty(newFolder))
                return;
            if (lstFolders.FirstOrDefault(t => t.name == newFolder) != null)
            {
                ViewHelper.Popup(string.Format("Das Verzeichnis {0} existiert bereits.\nFunktion abgebrochen", newFolder));
                return;
            }
            char A = (char)34; // (new Char[] { '"' });
            string _id = Guid.NewGuid().ToString().Substring(19, 17).Replace("-", "");
            
            string outdata = "{"+A+"name:"+A+ newFolder + A + "," + A + "type" + A + ":" + A + "Actor" + A + "," + A + "sort" + A + 
                ":null," + A + "flags" + A + ":{}," + A + "parent" + A + ":null," + A + "sorting" + A + ":" + A + "a" + A + 
                "," + A + "color" + A + ":" + A + A + "," + A + "_id" + A + ":" + A + _id + A + "}\n";

            if (IsLocalInstalliert)
            {
                string foldersFilePath = FoundryPfad + @"worlds\" + SelectedWorld + @"\data\folders.db";
                File.AppendAllText(foldersFilePath, outdata);
            }
            else
            {
                string foldersFilePath = FoundryPfad + @"worlds/" + SelectedWorld + "/data/folders.db";
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(foldersFilePath);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(FTPUser, FTPPasswort, "");
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                string FileData = reader.ReadToEnd();
                Console.WriteLine($"Download Complete, status {response.StatusDescription}");
                reader.Close();
                response.Close();

                FileData += outdata;
                FtpWebRequest request2 = (FtpWebRequest)WebRequest.Create(foldersFilePath);
                request2.Method = WebRequestMethods.Ftp.UploadFile;
                request2.Credentials = new NetworkCredential(FTPUser, FTPPasswort, "");

                // convert contents to byte.
                byte[] fileContents = Encoding.ASCII.GetBytes(FileData);
                request.ContentLength = fileContents.Length;

                using (Stream requestStream = request2.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }
                using (response = (FtpWebResponse)request2.GetResponse())
                {
                    Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                }
            }
            List<folder> lst = new List<folder>();
            lst.AddRange(lstFolders);
            lst.Add(new folder() { name = newFolder, color = "", sorting="a", typ="Actor", _id = _id });
            lstFolders = lst;

            ViewHelper.Popup("Verzeichnis erstellt");
        }
        private bool PathExists(string path)
        {
            if (IsLocalInstalliert)
            {
                return Directory.Exists(path);
            }
            else
            {
                // NACH DEM AUFRUF GIBR ES EINE EXCEPTION BEIM NÄCHSTEN ANFRAGEN DER FTP SEITE!!
                try
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
                    request.Method = WebRequestMethods.Ftp.ListDirectory;
                    request.Credentials = new NetworkCredential(FTPUser, FTPPasswort, "");
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    return true;
                }
                catch (WebException ex)
                {
                    return false;
                }
            }
        }
        #endregion

        #region //---- EVENTS ----
        private Base.CommandBase _onBtnCreateFolder = null;
        public Base.CommandBase OnBtnCreateFolder
        {
            get
            {
                if (_onBtnCreateFolder == null)
                    _onBtnCreateFolder = new Base.CommandBase(CreateFolder, null);
                return _onBtnCreateFolder;
            }
        }
        
        private Base.CommandBase _onBtnExportGegner = null;
        public Base.CommandBase OnBtnExportGegner
        {
            get
            {
                if (_onBtnExportGegner == null)
                    _onBtnExportGegner = new Base.CommandBase(ExportGegner, null);
                return _onBtnExportGegner;
            }
        }
        private void ExportGegner(object sender)
        {
            try
            {
                if (SelectedWorld == null)
                {
                    ViewHelper.Popup("Wähle zuerst eine Welt");
                    return;
                }
                if ((SelectedGegnerFolder == null || SelectedGegnerFolder.name == "") &&
                    !ViewHelper.Confirm("Export der Gegner", "Die Gegner-Datenbank wird ins das Hauptverzeichnis exportiert\n" +
                    "Wir empfehlen hier dringend zuvor ein Verzeichnis zu erstellen, um Gegner, NSC und Helden zu separieren\n\nSollen die Gegner in das " +
                    "Hauptverzeichnis exportiert werden?"))
                    return;

                //if (!PathExists(FoundryPfad + GegnerPortraitPfad) || !PathExists(FoundryPfad + GegnerTokenPfad))
                //{
                //    ViewHelper.Popup(string.Format("Nicht alle Pfade sind vorhanden.\nBitte überprüfe folgende Pfade:\n\n* {0}\n* {1}",
                //        FoundryPfad + GegnerPortraitPfad, FoundryPfad + GegnerTokenPfad));
                //    return;
                //}

                System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Wait); 
                MyTimer.start_timer();
                GetGegnerData();

                List<string> lstErsetzt = new List<string>();
                string A = (new Char[] { '"' }).ToString();

                //Open actors.db
                string actorsPath = IsLocalInstalliert ?
                    FoundryPfad + @"worlds\" + SelectedWorld + @"\data\actors.db" :
                    FoundryPfad + @"worlds/" + SelectedWorld + @"/data/actors.db";
                string FileData = GetFileData(actorsPath);
                if (FileData != null)
                {
                    List<string> lstFileData = FileData.Split(new Char[] { '\n' }).ToList(); //oldFileData
                    string newFileDataAdd = "";
                    //Check Gegner vorhanden -> Ja = Zeile löschen und ersetzen
                    foreach (GegnerArgument garg in lstGegnerArgument)
                    {
                        Nullable<int> pos = lstFileData.IndexOf(lstFileData.Where(t => t.Contains("name\":\"" + garg.g.Name + "\",")).FirstOrDefault());
                        if (pos != null && pos != -1)
                        {
                            lstFileData.RemoveAt(pos.Value);
                            lstErsetzt.Add(garg.g.Name);
                        }
                        newFileDataAdd += "\n" + garg.outcome;
                    }
                    string newFile = string.Join("\n", lstFileData);
                    newFile += newFileDataAdd;

                    //Gegner einfügen
                    SetFileData(actorsPath, newFile);
                    MyTimer.stop_timer("");
                    System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
                    ViewHelper.Popup(string.Format("Alle {0} Einträge wurden überprüft. Es wurden {1} aktualisiert und nach Foundry exportiert",
                        lstGegnerArgument.Count, lstErsetzt.Count));
                }
                else
                {

                    System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
                    ViewHelper.Popup("Die Foundry Datenbank 'actors.db' konnte nicht gefunden werden.\n\n Diese Datei sollte unter " +
                        "folgendem Pfad sein:\n" + actorsPath);
                }
            }
            finally
            {

                System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
            }
        }

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
            try
            { 
            if (SelectedWorld == null)
            {
                ViewHelper.Popup("Wähle zuerst eine Welt");
                return;
            }
            if ((SelectedHeldenFolder == null || SelectedHeldenFolder.name == "") && 
                !ViewHelper.Confirm("Export der Helden", "Die Helden werden ins das Hauptverzeichnis exportiert\n" +
                "Wir empfehlen hier zuvor ein Verzeichnis zu erstellen, um Helden, NSC und Gegner zu separieren\n\nSollen die Helden in das" +
                "Hauptverzeichnis exportiert werden?"))
                return;

            //if (!PathExists(FoundryPfad + HeldPortraitPfad) || !PathExists(FoundryPfad + HeldTokenPfad))
            //{ 
            //    ViewHelper.Popup(string.Format("Nicht alle Pfade sind vorhanden.\nBitte überprüfe folgende Pfade:\n\n* {0}\n* {1}",
            //        FoundryPfad + HeldPortraitPfad, FoundryPfad + HeldTokenPfad));
            //    return;
            //}

            System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
            MyTimer.start_timer();
            GetHeldenData();
            //Init();

            List<string> lstErsetzt = new List<string>();
            string A = (new Char[] { '"' }).ToString();

            //Open actors.db
            string actorsPath = IsLocalInstalliert ?
                FoundryPfad + @"worlds\" + SelectedWorld + @"\data\actors.db" :
                FoundryPfad + @"worlds/" + SelectedWorld + @"/data/actors.db";
            string FileData = GetFileData(actorsPath);
                if (FileData != null)
                {
                    List<string> lstFileData = FileData.Split(new Char[] { '\n' }).ToList();
                    string newFileDataAdd = "";
                    //Check Held vorhanden -> Ja = Zeile löschen und ersetzen
                    foreach (HeldenArgument harg in lstHeldArgument)
                    {
                        Nullable<int> pos = lstFileData.IndexOf(lstFileData.Where(t => t.Contains("name\":\"" + harg.h.Name + "\",")).FirstOrDefault());
                        if (pos != null && pos != -1)
                        {
                            lstFileData.RemoveAt(pos.Value);
                            lstErsetzt.Add(harg.h.Name);
                        }
                        newFileDataAdd += "\n" + harg.outcome;
                    }
                    string newFile = string.Join("\n", lstFileData);
                    newFile += newFileDataAdd;

                    //Held einfügen
                    SetFileData(actorsPath, newFile);
                    System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
                    ViewHelper.Popup(MyTimer.stop_timer("Refresh Helden-Daten aktualisiert in") + "\n\nAlle Helden wurden eingefügt.\n" +
                        "Folgende Helden wurden ersetzt:\n" +(lstErsetzt.Count > 0 ? string.Join("\n", lstErsetzt.Select(t => "  * "+t).ToList()): ""));
                }
                else
                {
                    System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
                    ViewHelper.Popup("Die Foundry Datenbank 'actors.db' konnte nicht gefunden werden.\n\n Diese Datei sollte unter " +
                    "folgendem Pfad sein:\n" + actorsPath);
                }
            }
            finally
            {
                System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Arrow);
            }
        }


        private Base.CommandBase _onBtnExportPlaylists = null;
        public Base.CommandBase OnBtnExportPlaylists
        {
            get
            {
                if (_onBtnExportPlaylists == null)
                    _onBtnExportPlaylists = new Base.CommandBase(ExportPlaylists, null);
                return _onBtnExportPlaylists;
            }
        }
        private void ExportPlaylists(object sender)
        {
            MyTimer.start_timer();
            System.Globalization.CultureInfo en = new System.Globalization.CultureInfo("en-US", false);
            int anzTotalTitel = 0;
            if (!Directory.Exists(string.Format(@"{0}{1}", FoundryPfad, MusikPfad)))
                Directory.CreateDirectory(string.Format(@"{0}{1}", FoundryPfad, MusikPfad));

            List<PlaylistArgument> lstPArg = new List<PlaylistArgument>();

            string GMid = GetUserID(string.Format(@"{0}worlds\{1}\data\users.db", FoundryPfad, SelectedWorld), "Gamemaster");

            PlaylistStatus = "Alle Playlisten werden exportiert ...";
            foreach (Audio_Playlist aPlaylist in lstPlaylists)
            {
                PlaylistStatus = string.Format("Export '{0}' ...", aPlaylist.Name);
                int anzTitel = aPlaylist.Audio_Playlist_Titel.Count;
                PlaylistArgument pArg = new PlaylistArgument();
                char A = (char)34; // (new Char[] { '"' });
                                    //4e1d3250-f700-3000-0001-387712958942  => 0001387712958942
                pArg.GMid = GMid;
                pArg._id = aPlaylist.Audio_PlaylistGUID.ToString().Substring(19, 17).Replace("-", "");
                pArg.name = aPlaylist.Name;
                pArg.permission = A + "default" + A + ":0," + A + GMid + A + ":3";
                pArg.sort = 100001;
                pArg.mode = 1;
                pArg.playing = "false";

                int current = 1;
                pArg.lstSounds = new List<PlaylistArgument.SoundArg>();
                //List<Audio_Playlist_Titel> lstTitel = new List<Audio_Playlist_Titel>();
                foreach (Audio_Playlist_Titel aTitel in aPlaylist.Audio_Playlist_Titel.OrderBy(t => t.Audio_Titel.Name))
                {
                    string MGpathFile = aTitel.Audio_Titel.Pfad + @"\" + aTitel.Audio_Titel.Datei;
                    if (!File.Exists(MGpathFile)) continue;

                    PlaylistStatus = string.Format("Export '{0}' {1} of {2} ...", aPlaylist.Name, current, anzTitel);
                    PlaylistArgument.SoundArg sArg = new PlaylistArgument.SoundArg();
                    sArg.lstArg = new List<string>();
                    string aTitelTeilGuid = aTitel.Audio_TitelGUID.ToString().Substring(19, 17).Replace("-", "");
                    sArg.lstArg.Add("{" + A + "_id" +A+":"+A+ aTitelTeilGuid+ A );
                    sArg.lstArg.Add( A + "flags" + A + ":{}");

                    stdPfad.ForEach((Action<string>)delegate (string s) {
                        if (MGpathFile.StartsWith(s))
                            MGpathFile = MGpathFile.Replace(s, (string)this.MusikPfad); });
                    string zielPfad = FoundryPfad + MGpathFile;
                    MGpathFile = MGpathFile.Replace(@"\","/");
                    if (!MGpathFile.StartsWith(MusikPfad))
                        continue;

                    sArg.lstArg.Add(A + "path" + A+":"+A+ MGpathFile+ A );
                    sArg.lstArg.Add(A + "repeat" + A + ":" + "false" );
                    sArg.lstArg.Add(A + "volume"+A+ ":" +((decimal)(aTitel.Volume/100)).ToString(en));
                    sArg.lstArg.Add(A + "name"+A + ":" + A+System.IO.Path.GetFileNameWithoutExtension(aTitel.Audio_Titel.Datei) +A );
                    sArg.lstArg.Add(A + "playing" + A + ":false" );
                    sArg.lstArg.Add(A + "streaming" + A + ":false}" );
                    sArg.lstTitel = string.Join(",", sArg.lstArg);
                    pArg.lstSounds.Add(sArg);

                    if (CopyTitelFile && !File.Exists(zielPfad))
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(zielPfad)))
                            Directory.CreateDirectory(Path.GetDirectoryName(zielPfad));
                        File.Copy(aTitel.Audio_Titel.Pfad + @"\" + aTitel.Audio_Titel.Datei, zielPfad, false);

                    }
                    current++;
                    anzTotalTitel++;
                }
                lstPArg.Add(pArg);

                if (anzTotalTitel ==100)
                { }
            }


            //Open playlists.db
            string worldPath = FoundryPfad + @"worlds\" + SelectedWorld + @"\data\";
            string playlistsFile = worldPath + "playlists.db";
            if (OverwritePlaylistFile)
                File.Delete(playlistsFile);
            List<string> lstFileData = new List<string>();
            string oldFileData = (File.Exists(playlistsFile))? File.ReadAllText(playlistsFile): "";

            string newFileData = oldFileData + string.Join("\n", lstPArg.Select(t => t.outtext));

            //Playlisten einfügen
            File.WriteAllText(playlistsFile, newFileData);

            PlaylistStatus = null;
            string stop = MyTimer.stop_timer("");
            PopUp(string.Format("Playlisten erfolgreich übertragen.\nEs wurden {0} in {1} übertragen.", anzTotalTitel, stop));
        }

        /*
         * 
         * 
         {"_id":"DUBBfsfZPdvWN8Mn",
        "name":"neu",
        "permission":{"default":0,"gNZOk6idrMy6uSkk":3},
        "sort":100001,
        "flags":{},
        "sounds":[
        {
            "_id":"BOTQlxGUhlZFk387",
            "flags":{},
            "path":"Musik/_Stra%C3%9Fenmusik/107-peter_balding--as_summer_closes-oma.mp3",
            "repeat":false,"volume":0.35355339059327373,
            "name":"107-peter_balding--as_summer_closes-oma",
            "playing":false,
            "streaming":false
        },
        {
            "_id":"9w7UzHLKZD0GE3Ao",
            "flags":{},
            "path":"Musik/_Stra%C3%9Fenmusik/107-peter_balding--as_summer_closes-oma.mp3",
            "repeat":false,"volume":0.35355339059327373,
            "name":"107-peter_balding--as_summer_closes-oma",
            "playing":false,
            "streaming":false
        },
        {
            "_id":"hMKrznmD7vhNLDNq",
            "flags":{},
            "path":"Musik/_Stra%C3%9Fenmusik/104-miguel_angel_tallante--cruzada-oma.mp3",
            "repeat":false,
            "volume":0.35355339059327373,
            "name":"104-miguel_angel_tallante--cruzada-oma",
            "playing":false,
            "streaming":false
        }],
        "mode":1,
        "playing":false}

         */
        #endregion
    }
}
