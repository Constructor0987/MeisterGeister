using DgSuche;
using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace MeisterGeister.ViewModel.Karte
{
    public class KarteViewModel : Base.ViewModelBase
    {
        public KarteViewModel()
        {
            onHeldenPositionSetzen = new CommandBase(HeldenPositionSetzen, null);
            onDereGlobusÖffnen = new CommandBase(DereGlobusÖffnen, null);
            onCenterOnHelden = new CommandBase(CenterOnHelden, null);

            karten = new List<Logic.Karte>();
            var aventurien = new Logic.Karte("Aventurien", "pack://siteoforigin:,,,/Images/Karten/Aventurien.jpg", 7150, 11000);
            karten.Add(aventurien);
            SelectedKarte = aventurien;
        }

        bool firstLoad = true;
        public void Refresh(bool noCenter = false)
        {
            var p = (Point)dgConverter.Convert(new Point(Global.HeldenLon, Global.HeldenLat), typeof(Point), null, null);
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
            set { Set(ref selectedKarte, value); }
        }

        private Point heldenPosition = new Point();
        public Point HeldenPosition
        {
            get { return heldenPosition; }
            set { 
                Set(ref heldenPosition, value);
                SetGlobusPositionFromHeldenPosition();
                OnChanged("HeldenX");
                OnChanged("HeldenY");
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
            set { 
                heldenPosition.Y = value;
                HeldenPosition = heldenPosition;
                }
        }

        private MeisterGeister.Logic.Karte.DereGlobusToMapConverter dgConverter = new MeisterGeister.Logic.Karte.DereGlobusToMapConverter();

        private Point heldenGlobusPosition = new Point();
        public Point HeldenGlobusPosition
        {
            get { return heldenGlobusPosition; }
            set { 
                Set(ref heldenGlobusPosition, value);
                SetHeldenPositionFromGlobusPosition();
                OnChanged("HeldenLängengrad");
                OnChanged("HeldenBreitengrad");
            }
        }

        private void SetHeldenPositionFromGlobusPosition()
        {
            heldenPosition = (Point)dgConverter.Convert(heldenGlobusPosition, typeof(Point), null, null);
            OnChanged("HeldenPosition");
            OnChanged("HeldenX");
            OnChanged("HeldenY");
        }

        private void SetGlobusPositionFromHeldenPosition()
        {
            heldenGlobusPosition = (Point)dgConverter.ConvertBack(heldenPosition, typeof(Point), null, null);
            OnChanged("HeldenGlobusPosition");
            OnChanged("HeldenLängengrad");
            OnChanged("HeldenBreitengrad");
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

        private CommandBase onHeldenPositionSetzen;
        public CommandBase OnHeldenPositionSetzen
        {
            get { return onHeldenPositionSetzen; }
        }

        private void HeldenPositionSetzen(object args)
        {
            if(args is Point)
            {
                HeldenPosition = (Point)args;
                Global.Standort.Name = "Heldenposition";
                Global.HeldenLat = HeldenBreitengrad;
                Global.HeldenLon = HeldenLängengrad;
                //Zum Abgleich der Position wegen der Rundungsfehler
                Refresh(true);
            }
        }

        private CommandBase onDereGlobusÖffnen;
        public CommandBase OnDereGlobusÖffnen
        {
            get { return onDereGlobusÖffnen; }
        }

        private void DereGlobusÖffnen(object args)
        {
            if(args is Point)
            {
                Point p = (Point)dgConverter.ConvertBack(args, typeof(Point), null, null);
                Ortsmarke.StarteDereGlobus("Aus MeisterGeister", String.Format(CultureInfo.InvariantCulture, "{0:0.0#####}", p.X), String.Format(CultureInfo.InvariantCulture, "{0:0.0#####}", p.Y));
            }
        }

        private double zoom = 1;
        public double Zoom
        {
            get { return zoom; }
            set { Set(ref zoom, value); }
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


        private void CenterOnHelden(object obj)
        {
            Zoom = 1;
            TranslateX = -1 * (HeldenPosition.X - ZoomControlSize.Width / 2);
            TranslateY = -1 * (HeldenPosition.Y - ZoomControlSize.Height / 2);
        }

        private CommandBase onCenterOnHelden;
        public CommandBase OnCenterOnHelden
        {
            get { return onCenterOnHelden; }
        }
    }
}
