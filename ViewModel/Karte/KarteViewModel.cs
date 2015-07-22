using MeisterGeister.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MeisterGeister.ViewModel.Karte
{
    public class KarteViewModel : Base.ViewModelBase
    {
        public KarteViewModel()
        {
            onHeldenPositionSetzen = new CommandBase(HeldenPositionSetzen, null);

            karten = new List<Logic.Karte>();
            var aventurien = new Logic.Karte("Aventurien", "pack://siteoforigin:,,,/Images/Karten/Aventurien.jpg", 7150, 11000);
            karten.Add(aventurien);
            SelectedKarte = aventurien;
        }

        public void Refresh()
        {
            // derzeit nichts beim erneuten anzeigen erforderlich
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
            }
        }
    }
}
