using DgSuche;
using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Resources;
using System.Windows.Threading;
using MeisterGeister.Logic.Extensions;
using System.Net;
using MeisterGeister.View.General;
using MeisterGeister.Logic.Karte;
using MeisterGeister.Model.Service;
using MeisterGeister.Model;
using System.Collections.ObjectModel;
using MeisterGeister.ViewModel.Karte.Logic;
using System.Windows.Input;
using MeisterGeister.Logic.General.AStar;
using System.Diagnostics;
using WPFExtensions.Controls;

namespace MeisterGeister.ViewModel.Karte
{
    public class KarteViewModel : Base.ToolViewModelBase
    {
        #region Kartendownload
        public static void DownloadKarten()
        {
            var kartenUrl = "http://meistergeister.org/download/763/";
            Global.Downloader.AddDownload(kartenUrl, FileExtensions.ConvertRelativeToAbsolutePath("Kartenpaket.zip"), Karten_DownloadFileCompleted);
        }

        static void Karten_DownloadFileCompleted(string filePath, Exception e)
        {
            if (e != null)
            {
                ViewHelper.ShowError("Fehler beim herunterladen des Kartenpaketes.", e);
                return;
            }
            FileExtensions.UnZip(FileExtensions.ConvertRelativeToAbsolutePath("Kartenpaket.zip"), FileExtensions.ConvertRelativeToAbsolutePath("."));
            ViewHelper.Popup("Das Kartenpaket wurde heruntergeladen und installiert.");
        }

        public static bool KartenVorhanden(List<Logic.Karte> karten = null)
        {
            if (karten == null)
                karten = KartenListeErstellen();
            foreach(var k in karten)
            {
                var uri = new Uri(k.Bildpfad, UriKind.RelativeOrAbsolute);
                if(uri.Host.StartsWith("siteoforigin:") && !System.IO.File.Exists(FileExtensions.ConvertRelativeToAbsolutePath(uri.AbsolutePath.Substring(1))))
                    return false;
            }
            return true;
        }
        #endregion

        #region Konstruktor, Setup und Refresh
        public KarteViewModel()
            : base(ViewHelper.Popup, ViewHelper.Confirm, ViewHelper.ShowError)
        {
            karten = KartenListeErstellen();
            if (!KartenVorhanden(karten) && Confirm("Karten herunterladen", "Mindestens eine Karte ist nicht installiert.\nSollen die fehlenden Karten von der MeisterGeister-Seite heruntergeladen werden?"))
                DownloadKarten();
            //TODO Karte anhand HeldenLon und HeldenLat bestimmen
            SelectedKarte = karten[0];
            InitializeOrte();
            this.PropertyChanged += KarteViewModel_PropertyChanged;
        }

        public override void RegisterEvents()
        {
            base.RegisterEvents();
            Global.HeldSelectionChanged += Global_HeldSelectionChanged;
            Global.StandortChanged += Global_StandortChanged;
            OnChanged("SelectedHeld");
        }

        public override void UnregisterEvents()
        {
            Global.StandortChanged -= Global_StandortChanged;
            Global.HeldSelectionChanged -= Global_HeldSelectionChanged;
            this.PropertyChanged -= KarteViewModel_PropertyChanged;
            base.UnregisterEvents();
        }

        private void KarteViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Lines")
            {
                OnChanged("Points");
                OnChanged("WayPoints");
            }
        }

        private bool ignoreGlobalStandortChangedEvent = false;
        private void Global_StandortChanged(object sender, EventArgs e)
        {
            if (ignoreGlobalStandortChangedEvent)
                return;
            HeldenGlobusPosition = Global.HeldenPosition;
        }

        private void Global_HeldSelectionChanged(object sender, EventArgs e)
        {
            OnChanged("SelectedHeld");
        }

        public static List<Logic.Karte> KartenListeErstellen()
        {
            Point p1, p2, p3, l1, l2, l3;
            var l = new List<Logic.Karte>();
            // Dwurinsand
            p1 = new Point(279, 1958);
            l1 = new Point(-7.66832038, 38.96870934);
            // Neu-Bosparan
            p2 = new Point(6513, 10241);
            l2 = new Point(11.45865558, 16.02534243);
            // Gareth
            p3 = new Point(3996, 5271);
            l3 = new Point(3.735098459, 29.79180236);
            var aventurien = new Logic.Karte("Aventurien", "pack://siteoforigin:,,,/Images/Karten/Aventurien.jpg", 7150, 11000,
                p1, l1, p2, l2, p3, l3, false, false);
            l.Add(aventurien);

            //Referenzpunkte in Myranor
            // Shanali
            p1 = new Point(6636, 7234);
            l1 = new Point(-54.132385, -0.188961);
            // Haldingaford
            p2 = new Point(8032, 1626);
            l2 = new Point(-47.376805, 28.632769);
            // Südliches Ende der Orismanifälle
            p3 = new Point(4215, 1980);
            l3 = new Point(-68.1022, 26.0588);
            var myranor = new Logic.Karte("Myranor", "pack://siteoforigin:,,,/Images/Karten/Myranor.jpg", 11000, 8076,
                p1, l1, p2, l2, p3, l3, true, false);
            l.Add(myranor);

#if KOORDINATENTEST
            //Koordiantentestkarte
            // Punkt 1
            p1 = new Point(7959, 1506);
            l1 = new Point(-40.977896, 23.829324);
            // Punkt 2
            p2 = new Point(1497, 4479);
            l2 = new Point(-102.659126, -4.548357);
            // Punkt 3
            p3 = new Point(6170, 6391);
            l3 = new Point(-58.056499, -22.796352);
            //// Punkt 4
            //p4 = new Point(4945, 3125);
            //l4 = new Point(-69.746588, 8.374168);
            var koordinaten = new Logic.Karte("Koordinaten", "pack://siteoforigin:,,,/Images/Karten/koordinaten.png", 11000, 8076,
                p1, l1, p2, l2, p3, l3, true, false);
            l.Add(koordinaten);
#endif

            return l;
        }

        bool firstLoad = true;
        public void Refresh(bool noCenter = false)
        {
            //TODO Bounds der Karten überprüfen und anhand dessen die Karte wählen
            var p = (Point)dgConverter.Convert(Global.HeldenPosition, typeof(Point), null, null);
            if (p != HeldenPosition && !noCenter)
                firstLoad = true;
            HeldenPosition = p;
            if (firstLoad)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action<object>(CenterOnHelden), null);
                firstLoad = false;
            }
        }

        public void LoadDaten()
        {

        }
        #endregion

        #region Properties

        #region Karten und Konverter
        private bool _notZooming = true;
        public bool notZooming
        {
            get { return _notZooming; }
            set { Set(ref _notZooming, value); }
        }

        private List<Logic.Karte> karten = null;
        public List<Logic.Karte> Karten
        {
            get { return karten; }
            set { Set(ref karten,  value); }
        }

        private Logic.Karte selectedKarte = null;
        public Logic.Karte SelectedKarte
        {
            get { return selectedKarte; }
            set {
                if (Set(ref selectedKarte, value))
                {
                    DereGlobusToMapConverter = selectedKarte.DereGlobusToMapConverter;
                    MapToDereGlobusConverter = selectedKarte.MapToDereGlobusConverter;
                    SetHeldenPositionFromGlobusPosition();
                }
            }
        }

        private MeisterGeister.Logic.Karte.DereGlobusToMapConverter dgConverter;
        public MeisterGeister.Logic.Karte.DereGlobusToMapConverter DereGlobusToMapConverter
        {
            get { return dgConverter; }
            set { Set(ref dgConverter, value); }
        }

        MeisterGeister.Logic.Karte.MapToDereGlobusConverter mapConverter;
        public MeisterGeister.Logic.Karte.MapToDereGlobusConverter MapToDereGlobusConverter
        {
            get { return mapConverter; }
            set { Set(ref mapConverter, value); }
        }

        private ZoomControl _zoomControl;
        public ZoomControl zoomControl
        {
            get { return _zoomControl; }
            set {_zoomControl = value; }
        }
        #endregion

        #region Heldenposition auf der Karte und in Dereglobus-Koordinaten
        private Point heldenPosition = new Point();
        public Point HeldenPosition
        {
            get { return heldenPosition; }
            set { 
                Set(ref heldenPosition, value);
                SetGlobusPositionFromHeldenPosition();
                OnChanged("HeldenX");
                OnChanged("HeldenY");
                OnChanged("HeldenYMinusPinHeight");
            }
        }

        public int HeldenX
        {
            get { return (int)Math.Round(HeldenPosition.X, MidpointRounding.AwayFromZero); }
            set {
                heldenPosition.X = value;
                HeldenPosition = heldenPosition;
            }
        }

        public int HeldenY
        {
            get { return (int)Math.Round(HeldenPosition.Y, MidpointRounding.AwayFromZero); }
            set
            {
                heldenPosition.Y = value;
                HeldenPosition = heldenPosition;
                OnChanged("HeldenYMinusPinHeight");
            }
        }
        public int HeldenYMinusPinHeight
        {
            get { return (int)Math.Round(HeldenPosition.Y- 25.0 /Zoom , MidpointRounding.AwayFromZero); }
            set
            {
                heldenPosition.Y = value + 25.0 / Zoom;
                HeldenPosition = heldenPosition;
            }
        }

        private Point heldenGlobusPosition = new Point();
        public Point HeldenGlobusPosition
        {
            get { return heldenGlobusPosition; }
            set { 
                Set(ref heldenGlobusPosition, value);
                SetHeldenPositionFromGlobusPosition();
                OnChanged("HeldenLängengrad");
                OnChanged("HeldenBreitengrad");
                UpdateGlobalStandort();
            }
        }

        public double HeldenLängengrad
        {
            get { return (double)Math.Round(HeldenGlobusPosition.X, 6, MidpointRounding.AwayFromZero); }
            set {
                heldenGlobusPosition.X = value;
                HeldenGlobusPosition = heldenGlobusPosition;
            }
        }

        public double HeldenBreitengrad
        {
            get { return (double)Math.Round(HeldenGlobusPosition.Y, 6, MidpointRounding.AwayFromZero); }
            set {
                heldenGlobusPosition.Y = value;
                HeldenGlobusPosition = heldenGlobusPosition;
            }
        }

        //Hilfsmethoden
        private void UpdateGlobalStandort()
        {
            ignoreGlobalStandortChangedEvent = true;
            Global.HeldenPosition = HeldenGlobusPosition;
            ignoreGlobalStandortChangedEvent = false;
        }

        private void SetHeldenPositionFromGlobusPosition()
        {
            heldenPosition = (Point)dgConverter.Convert(heldenGlobusPosition, typeof(Point), null, null);
            OnChanged("HeldenPosition");
            OnChanged("HeldenX");
            OnChanged("HeldenY"); 
            OnChanged("HeldenYMinusPinHeight"); 
        }

        private void SetGlobusPositionFromHeldenPosition()
        {
            heldenGlobusPosition = (Point)dgConverter.ConvertBack(heldenPosition, typeof(Point), null, null);
            OnChanged("HeldenGlobusPosition");
            OnChanged("HeldenLängengrad");
            OnChanged("HeldenBreitengrad");
            UpdateGlobalStandort();
        }
        #endregion

        #region Routenplaner

        public ObservableCollection<Node> _allNodes;
        public ObservableCollection<Node> AllNodes
        {
            get
            {
                return _allNodes;
            }
            set
            {
                if(value != _allNodes)
                {
                    Set(ref _allNodes, value);
                }
            }
        }

        private bool _isToleranceActive = false;
        public bool IsToleranceActive
        {
            get { return _isToleranceActive; }
            set
            {
                if (value != _isToleranceActive)
                {
                    Set(ref _isToleranceActive, value);
                }
            }
        }

        private bool _isShowStages = false;
        public bool IsShowStages
        {
            get { return _isShowStages; }
            set
            {
                if (value != _isShowStages)
                {
                    Set(ref _isShowStages, value);
                }
            }
        }

        private bool _isAvoidMountains = true;
        public bool IsAvoidMountains
        {
            get { return _isAvoidMountains; }
            set
            {
                if (value != _isAvoidMountains)
                {
                    Set(ref _isAvoidMountains, value);
                }
            }
        }
        private bool _isAvoidForrests = false;
        public bool IsAvoidForrests
        {
            get { return _isAvoidForrests; }
            set
            {
                if (value != _isAvoidForrests)
                {
                    Set(ref _isAvoidForrests, value);
                }
            }
        }
        private bool _isAvoidSeas = false;
        public bool IsAvoidSeas
        {
            get { return _isAvoidSeas; }
            set
            {
                if (value != _isAvoidSeas)
                {
                    Set(ref _isAvoidSeas, value);
                }
            }
        }
        private bool _isAvoidRivers = false;
        public bool IsAvoidRivers
        {
            get { return _isAvoidRivers; }
            set
            {
                if (value != _isAvoidRivers)
                {
                    Set(ref _isAvoidRivers, value);
                }
            }
        }

        public Fortbewegung SelectedFortbewegung
        {
            get
            {
                return Global.ContextGeo.Liste<Fortbewegung>().Single(f => f.ID == (int)SelectedTravelType);
            }
        }

        private RoutingSummary _routingSummary;
        public RoutingSummary RoutingSummary
        {
            get
            {
                return _routingSummary;
            }
            set
            {
                Set(ref _routingSummary, value);
            }
        }

        private TravelType _selectedTravelType = TravelType.Afoot;
        public TravelType SelectedTravelType
        {
            get { return _selectedTravelType; }
            set
            {
                if(value != _selectedTravelType)
                {
                    Set(ref _selectedTravelType, value);
                }
            }
        }

        private Ort routeStarting;
        public Ort RouteStarting
        {
            get
            {
                return routeStarting;
            }
            set
            {
                if(value != routeStarting)
                {
                    Set(ref routeStarting, value);
                    Refocus();
                }
            }
        }

        private Ort routeEnding;
        public Ort RouteEnding
        {
            get
            {
                return routeEnding;
            }
            set
            {
                if (value != routeEnding)
                {
                    Set(ref routeEnding, value);
                    Refocus();
                }
            }
        }

        public ObservableCollection<RoutingOrt> WayPoints
        {
            get
            {
                return GetRouteDescription();
            }
        }

        private RoutingOrt selectedRoutingPoint;
        public RoutingOrt SelectedRoutingPoint
        {
            get
            {
                return selectedRoutingPoint;
            }
            set
            {
                var obj = value as RoutingOrt;
                if (value != selectedRoutingPoint)
                {
                    if(selectedRoutingPoint != null)
                        selectedRoutingPoint.IsSelected = false;
                    if (obj != null)
                        value.IsSelected = true;
                    Set(ref selectedRoutingPoint, value);
                }
            }
        }

        private ObservableCollection<RoutingOrt> GetRouteDescription()
        {
            //var stopWatch = Stopwatch.StartNew();
            var routingPoints = Lines.OfType<RoutingOrt>();
            ObservableCollection<RoutingOrt> resultingPoints = null;

            if (routingPoints.Any())
            {
                var routeDescribingService = new RouteDescribingService();
                var routeDescribingResult = routeDescribingService.GetDescription(routingPoints, 
                    new RouteDescribingConditions(SelectedFortbewegung, RouteEnding, IsToleranceActive));
                resultingPoints = routeDescribingResult.RouteDescription;
                RoutingSummary = routeDescribingResult.RoutingSummary;
            }

            //stopWatch.Stop();
            //Debug.WriteLine("GetRouteDescription: " + stopWatch.Elapsed.TotalMilliseconds);
            return resultingPoints;
        }

        private ObservableCollection<ViewModelBase> _lines;
        public ObservableCollection<ViewModelBase> Lines
        {
            get
            {
                if (_lines == null)
                    Lines = new ObservableCollection<ViewModelBase>();
                return _lines;
            }
            set
            {
                if(value != _lines)
                {
                    Set(ref _lines, value);
                }
            }
        }

        public ObservableCollection<RoutingPoint> Points
        {
            get
            {
                return new ObservableCollection<RoutingPoint>(Lines.OfType<RoutingPoint>());
            }
        }

        private ObservableCollection<Ort> _orte;
        public ObservableCollection<Ort> Orte
        {
            get
            {
                if (_orte == null)
                    Orte = new ObservableCollection<Ort>();
                return _orte;
            }
            set
            {
                if (value != _orte)
                {
                    Set(ref _orte, value);
                }
            }
        }

        private void InitializeOrte()
        {
            Orte.Clear();
            var orte =  Global.ContextGeo.Liste<Ort>()
                .Where(o => !string.IsNullOrEmpty(o.Name))
                .OrderBy(o => o.Name);

            foreach (var ort in orte)
            {
                Orte.Add(ort);
            }
        }

        private void Refocus()
        {
            if (RouteStarting != null)
            {
                if (RouteEnding != null)
                    AdjustViewToRoute();
                else
                    CenterOn(RouteStarting.Location);
            }
            else if (RouteEnding != null)
                CenterOn(RouteEnding.Location);
        }

        private void FindRoute(object args)
        {
            if (RouteStarting != null && RouteEnding != null)
            {
                Global.SetIsBusy(true, "Berechne Route...");

                AdjustViewToRoute();
                IEnumerable<Ort> nodes = CalculateRoute();
                PrintRoute(nodes);

                Global.SetIsBusy(false);
            }
        }

        private void PrintRoute(IEnumerable<Ort> nodes)
        {
            RouteDescribingConditions conditions = GetRouteDescribingConditions();
            var routeDescribingService = new RouteDescribingService();
            ObservableCollection<ViewModelBase> route = routeDescribingService.DrawRoute(nodes, conditions);
            //if (IsShowStages)
            //{
            //    var stages = routeDescribingService.GetStages(nodes.Select(o => o.RoutingStrecke).Where(s => s != null), conditions);
            //    foreach (var stage in stages)
            //        route.Add(stage);
            //}
            this.Lines = route;
        }

        private RouteDescribingConditions GetRouteDescribingConditions()
        {
            return new RouteDescribingConditions(SelectedFortbewegung, RouteEnding, IsToleranceActive);
        }

        private IEnumerable<Ort> CalculateRoute()
        {
            SearchParametersRouting searchingParameters = CreateSearchingParameters();
            var service = new RoutingService();
            IEnumerable<Ort> result = service.GetShortestPath(searchingParameters);
#if DEBUG
            //this.AllNodes = new ObservableCollection<Node>(service.AllNodes);
#endif
            return result;
        }

        private SearchParametersRouting CreateSearchingParameters()
        {
            return new SearchParametersRouting(new Size(SelectedKarte.Breite, SelectedKarte.Höhe), RouteStarting,
                            RouteEnding, SelectedTravelType, !IsAvoidRivers, !IsAvoidSeas, !IsAvoidMountains, !IsAvoidForrests);
        }

        private void AdjustViewToRoute()
        {
            Point center = GetRouteCenter();
            var routingService = new RoutingService();
            CenterOn(center);
            double adjustmentFactor = routingService.GetZoomAdjustment(ZoomControlSize, Zoom, center, RouteStarting.Location);

            if (adjustmentFactor != default(double))
            {
                Zoom /= adjustmentFactor;
            }
        }

        private Point GetRouteCenter()
        {
            var startingPoint = RouteStarting;
            var endingPoint = RouteEnding;
            return new Point((startingPoint.X + endingPoint.X) / 2, (startingPoint.Y + endingPoint.Y) / 2);
        }

        #endregion

        #endregion

        #region Commands
        private CommandBase findRouteCommand;
        public CommandBase FindRouteCommand
        {
            get
            {
                if (findRouteCommand == null)
                    findRouteCommand = new CommandBase(FindRoute, null);
                return findRouteCommand;
            }
        }

        private CommandBase onHeldenPositionSetzen;
        public CommandBase OnHeldenPositionSetzen
        {
            get {
                if(onHeldenPositionSetzen == null)
                    onHeldenPositionSetzen = new CommandBase(HeldenPositionSetzen, null);
                return onHeldenPositionSetzen; 
            }
        }

        private void HeldenPositionSetzen(object args)
        {
            if(args is Point)
            {
                HeldenPosition = (Point)args;
            }
        }

        private CommandBase onDereGlobusÖffnen;
        public CommandBase OnDereGlobusÖffnen
        {
            get {
                if(onDereGlobusÖffnen == null)
                    onDereGlobusÖffnen = new CommandBase(DereGlobusÖffnen, null);
                return onDereGlobusÖffnen;
            }
        }

        private void DereGlobusÖffnen(object args)
        {
            if(args is Point)
            {
                Point p = (Point)dgConverter.ConvertBack(args, typeof(Point), null, null);
                Ortsmarke.StarteDereGlobus("Aus MeisterGeister", String.Format(CultureInfo.InvariantCulture, "{0:0.0#####}", p.X), String.Format(CultureInfo.InvariantCulture, "{0:0.0#####}", p.Y));
            }
        }

        private Base.CommandBase onShowSpielerInfo = null;
        public Base.CommandBase OnShowSpielerInfo
        {
            get
            {
                if (onShowSpielerInfo == null)
                    onShowSpielerInfo = new Base.CommandBase(ShowSpielerInfo, null);
                return onShowSpielerInfo;
            }
        }
        private void ShowSpielerInfo(object sender)
        {
            View.SpielerScreen.SpielerWindow.SetContent(View.General.ViewHelper.GetImageFromControl((FrameworkElement)sender));
        }

        public void CenterOn(Point p)
        {
            Zoom = 1;
            TranslateX = -1 * (p.X - ZoomControlSize.Width / 2);
            TranslateY = -1 * (p.Y - ZoomControlSize.Height / 2);
        }

        public void CenterOn(DgSuche.Ortsmarke ort)
        {
            Point p = ConvertOrtsmarkeToPoint(ort);
            CenterOn(p);
        }

        private Point ConvertOrtsmarkeToPoint(Ortsmarke ort)
        {
            var dgp = new Point(ort.Longitude, ort.Latitude);
            Point p = (Point)dgConverter.Convert(dgp, typeof(Point), null, null);
            return p;
        }

        private void CenterOnHelden(object obj)
        {
            CenterOn(HeldenPosition);
        }

        private CommandBase onCenterOnHelden;
        public CommandBase OnCenterOnHelden
        {
            get {
                if(onCenterOnHelden == null)
                    onCenterOnHelden = new CommandBase(CenterOnHelden, null);
                return onCenterOnHelden;
            }
        }

        private CommandBase onShowGebiete;
        public CommandBase OnShowGebiete
        {
            get {
                if(onShowGebiete == null)
                    onShowGebiete = new CommandBase(ShowGebiete, null);
                return onShowGebiete;
            }
        }

        private void ShowGebiete(object args)
        {
            if (args is Point)
            {
                var p = (Point)dgConverter.ConvertBack(args, typeof(Point), null, null); ;

                var gebiete = Global.ContextZooBot.GetGebiete(p, 0.2);
                if (gebiete != null)
                {
                    var s = "Der Punkt liegt in den folgenden Gebieten:";
                    foreach (var g in gebiete)
                        s += "\n" + g.Name;
                    s += "\n";
                    s += "\n" + "Der Punkt liegt in den folgenden Regionen: \n" + string.Join(Environment.NewLine, Global.ContextZooBot.GetRegion(p, 0));
                    PopUp(s);
                }
                
            }
        }

        //private CommandBase onShowPflanzen;
        //public CommandBase OnShowPflanzen
        //{
        //    get
        //    {
        //        if (onShowPflanzen == null)
        //            onShowPflanzen = new CommandBase(ShowPflanzen, null);
        //        return onShowPflanzen;
        //    }
        //}

        //private void ShowPflanzen(object args)
        //{
        //    if (args is Point)
        //    {
        //        var p = (Point)dgConverter.ConvertBack(args, typeof(Point), null, null); ;
        //        var pvm = new PflanzenAnOrtViewModel(p, 0.2);
        //        var pwin = new MeisterGeister.View.Karte.PflanzenAnOrtWindow();
        //        pwin.VM = pvm;
        //        pwin.ShowDialog();
        //    }
        //}
        #endregion

        #region UI Bindings, wie Zoom und Translate
        private double zoom = 1;
        public double Zoom
        {
            get { return zoom; }
            set { Set(ref zoom, value);
                OnChanged("HeldenYMinusPinHeight");
            }
        }

        public Point Center
        {
            get
            {
                return new Point(translateX, translateY);
            }
        }

        private double translateX = 0;
        public double TranslateX
        {
            get { return translateX; }
            set { Set(ref translateX, value); }
        }

        private double translateY = 0;
        public double TranslateY
        {
            get { return translateY; }
            set { Set(ref translateY, value); }
        }

        private Size zoomControlSize;
        public Size ZoomControlSize
        {
            get { return zoomControlSize; }
            set { Set(ref zoomControlSize, value); }
        }
        #endregion


        public Model.Held SelectedHeld
        {
            get { return Global.SelectedHeld; }
        }

    }
}
