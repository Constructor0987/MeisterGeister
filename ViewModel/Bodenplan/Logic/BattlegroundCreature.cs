using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Runtime.Serialization;
using System.Windows.Media;
using System.Windows;
using System.IO;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    [DataContract(IsReference = true)]
    public class BattlegroundCreature:BattlegroundBaseObject
    {
        private string _portraitFilename;
        private double _creatureX = 1200;
        private double _creatureY = 600;
        private Random r;
        private double _imageOriginalWidth = 80;
        private double _imageOriginalHeigth = 80;
        private PathGeometry _sightAreaGeometryData = new PathGeometry();
        private double _sightAreaLength = 120;

        public static String ICON_DIR = "/Images/Icons/General/";

        public string _creaturePosition = Ressources.GetRelativeApplicationPathForImagesIcons() + "FloatingCreature.png";
        public string CreaturePosition 
        {
            get { return _creaturePosition; }
            set
            {
                _creaturePosition = value;
                OnChanged("CreaturePosition");
            }
        }

        public BattlegroundCreature()
        {
            r = new Random();
            CreatureX += r.Next(0, 500);
            r = new Random();
            CreatureY += r.Next(0, 500);
            MoveObject(0,0,false); //for initial position of ZLevel Display
            CreateSightArea();
        }

        public double SightAreaLength
        {
            get { return _sightAreaLength; }
            set
            {
                _sightAreaLength = value;
                OnChanged("SightAreaLength");
                CalculateSightArea();
                Console.WriteLine("Sight Length: " + value);
            }
        }

        public PathGeometry SightAreaGeometryData
        {
            get { return _sightAreaGeometryData; }
            set
            {
                _sightAreaGeometryData = value;
                OnChanged("SightAreaGeometryData");
            }
        }

        //Objectname
        public override string ObjectName
        {
            get { return "Held/Monster"; }
        }

        public double _objectSize = 1;
        public double ObjectSize
        {
            get { return _objectSize; }
            set
            {
                _objectSize = value;
                ScalePicture(value);
                OnChanged("ObjectSize");
            }
        }

        public double CreatureX
        {
            get { return _creatureX; }
            set
            {
                _creatureX = value;
                CalculateSightArea();
                OnChanged("CreatureX");
            }
        }

        public double CreatureY
        {
            get { return _creatureY; }
            set
            {
                _creatureY = value;
                //CalculateSightArea();  //TODO: only on CreatureX Move, cause of too much calculations per pixelmove.
                OnChanged("CreatureY");
            }
        }

        int _sightLineSektor = 4;
        public int SightLineSektor
        {
            get { return _sightLineSektor; }
            set
            {
                _sightLineSektor = value;
                OnChanged("SightLineSektor");
            }
        }

        private string _creaturePictureUrl;
        public string CreaturePictureUrl
        {
            get { return _creaturePictureUrl; }
            set
            {
                _creaturePictureUrl = value;
                OnChanged("CreaturePictureUrl");
            }
        }

        private double _creatureNameY = 90;
        public double CreatureNameY
        {
            get { return _creatureNameY; }
            set
            {
                _creatureNameY = value;
                OnChanged("CreatureNameY");
            }
        }

        private double _creatureNameX = 90;
        public double CreatureNameX
        {
            get { return _creatureNameX; }
            set
            {
                _creatureNameX = value;
                OnChanged("CreatureNameX");
            }
        }

        private double _creatureWidth = 80;
        public double CreatureWidth
        {
            get { return _creatureWidth; }
            set
            {
                _creatureWidth = value;
                OnChanged("CreatureWidth");
            }
        }

        private double _creatureHeight = 80;
        public double CreatureHeight
        {
            get { return _creatureHeight; }
            set
            {
                _creatureHeight = value;
                OnChanged("CreatureHeight");
            }
        }

        public String PortraitFileName
        {
            get { return _portraitFilename; }
            set { _portraitFilename = value; }
        }

        public void LoadBattlegroundPortrait(string portraitFilename, bool ishero)
        {
            CreaturePictureUrl = ICON_DIR + "fragezeichen.png";
            //if(!File.Exists(portraitFilename)) 
            if(portraitFilename != null ) if(portraitFilename.Length!=0) CreaturePictureUrl = ishero ? portraitFilename : @portraitFilename.Replace("/DSA MeisterGeister;component", string.Empty);
        }

        public void ScalePicture(double factor)
        {
            CreatureHeight = _imageOriginalHeigth * factor;
            CreatureWidth = _imageOriginalWidth * factor;
        }

        public void MoveObject(double deltaX, double deltaY,bool stickAtCursor)
        {
            if (stickAtCursor)
            {
                CreatureX = deltaX;
                CreatureY = deltaY;
            }
            else
            {
                CreatureX = CreatureX + deltaX;
                CreatureY = CreatureY + deltaY;
            }
            ZDisplayX = CreatureX - 10;
            ZDisplayY = CreatureY - 10;
            CreatureNameX = CreatureX - 40;
            CreatureNameY = CreatureY - 25;
        }

        public override void RunBeforeXMLSerialization()
        {
            //nothing special to take care of...
        }
        public override void RunAfterXMLDeserialization()
        {
            //nothing special to take care of...
        }

        private void CreateSightArea()
        {
            
            PathFigureCollection _pathFigureCollection = new PathFigureCollection();
            PathFigure _pathFigure = new PathFigure();
            PathSegmentCollection _pathSegmentCollection = new PathSegmentCollection();

            // 6 possible sightline positions (hexagon)
 
            //first: 
            _pathFigure.StartPoint = new Point(CreatureX + CreatureWidth / 2 - 120, CreatureY + CreatureHeight / 2);
            _pathFigure.Segments.Add(new ArcSegment(new Point(CreatureX + CreatureWidth / 2 + 120, CreatureY + CreatureHeight / 2),
                new Size(80, 80), 0, true, SweepDirection.Clockwise, true));
            _pathFigure.IsFilled = true;
            _pathFigureCollection.Add(_pathFigure);
            SightAreaGeometryData.Figures = _pathFigureCollection;
        }

        private void CalculateSightArea() 
        {
            if (SightAreaGeometryData.Figures.Count == 0) return;

            double b = Math.Cos(34*Math.PI/180) * _sightAreaLength;
            double a = Math.Sin(34*Math.PI/180) * _sightAreaLength;

            if (SightLineSektor == 0)
            {
                SightAreaGeometryData.Figures.First().StartPoint = new Point(CreatureX + CreatureWidth / 2 + a, CreatureY + CreatureHeight / 2 - b);
                SightAreaGeometryData.Figures.First().Segments[0] = new ArcSegment(new Point(CreatureX + CreatureWidth / 2 - a, CreatureY + CreatureHeight / 2 + b),
                    new Size(80, 80), 0, true, SweepDirection.Clockwise, true);
            }
            else if (SightLineSektor == 1)
            {
                SightAreaGeometryData.Figures.First().StartPoint = new Point(CreatureX + CreatureWidth / 2 + _sightAreaLength, CreatureY + CreatureHeight / 2);
                SightAreaGeometryData.Figures.First().Segments[0] = new ArcSegment(new Point(CreatureX + CreatureWidth / 2 - _sightAreaLength, CreatureY + CreatureHeight / 2),
                    new Size(80, 80), 0, true, SweepDirection.Clockwise, true);
            }
            else if (SightLineSektor == 2)
            {
                SightAreaGeometryData.Figures.First().StartPoint = new Point(CreatureX + CreatureWidth / 2 + a, CreatureY + CreatureHeight / 2 + b);
                SightAreaGeometryData.Figures.First().Segments[0] = new ArcSegment(new Point(CreatureX + CreatureWidth / 2 - a, CreatureY + CreatureHeight / 2 - b),
                    new Size(80, 80), 0, true, SweepDirection.Clockwise, true);
            }
            else if (SightLineSektor == 3)
            {
                SightAreaGeometryData.Figures.First().StartPoint = new Point(CreatureX + CreatureWidth / 2 + a, CreatureY + CreatureHeight / 2 - b);
                SightAreaGeometryData.Figures.First().Segments[0] = new ArcSegment(new Point(CreatureX + CreatureWidth / 2 - a, CreatureY + CreatureHeight / 2 + b),
                    new Size(80, 80), 0, true, SweepDirection.Counterclockwise, true);
            }
            else if (SightLineSektor == 4)
            {
                SightAreaGeometryData.Figures.First().StartPoint = new Point(CreatureX + CreatureWidth / 2 - _sightAreaLength, CreatureY + CreatureHeight / 2);
                SightAreaGeometryData.Figures.First().Segments[0] = new ArcSegment(new Point(CreatureX + CreatureWidth / 2 + _sightAreaLength, CreatureY + CreatureHeight / 2),
                    new Size(80, 80), 0, true, SweepDirection.Clockwise, true);
            }
            else if (SightLineSektor == 5)
            {
                SightAreaGeometryData.Figures.First().StartPoint = new Point(CreatureX + CreatureWidth / 2 - a, CreatureY + CreatureHeight / 2 - b);
                SightAreaGeometryData.Figures.First().Segments[0] = new ArcSegment(new Point(CreatureX + CreatureWidth / 2 + a, CreatureY + CreatureHeight / 2 + b),
                    new Size(80, 80), 0, true, SweepDirection.Clockwise, true);
            }
            

            
        }

        public void CalculateNewSightLineSektor(Point currentMousePos)
        {
            // How the calculations and Sektores are designed...

            /*    -146°   146°
             *        \ 4 /
             *       3 \ / 5
             * -90° ---------  90°
             *       2 / \ 0
             *        / 1 \
             *    -34°    34°
             */ 

            double _a = currentMousePos.X - (CreatureX + CreatureWidth / 2);
            double _b = currentMousePos.Y - (CreatureY + CreatureHeight / 2);
            double _alpha = Math.Atan2(_a, _b) * (180 / Math.PI);

            if (_alpha <= 146 && _alpha > 90) SightLineSektor = 5;
            else if (_alpha <= 90 && _alpha > 34) SightLineSektor = 0;
            else if (_alpha <= 34 && _alpha > -34) SightLineSektor = 1;
            else if (_alpha <= -34 && _alpha > -90) SightLineSektor = 2;
            else if (_alpha <= -90 && _alpha > -146) SightLineSektor = 3;
            else SightLineSektor = 4;
            
            CalculateSightArea(); //update sightarea
        }

        public void UpdateCreaturePosition()
        {
            if (((Wesen)this).Position == Position.liegend) CreaturePosition = Ressources.GetRelativeApplicationPathForImagesIcons() + "OnTheGroundCreature.png";
            else if (((Wesen)this).Position == Position.kniend) CreaturePosition = Ressources.GetRelativeApplicationPathForImagesIcons() + "KneelingCreature.png";
            else if (((Wesen)this).Position == Position.stehend) CreaturePosition = Ressources.GetRelativeApplicationPathForImagesIcons() + "StandingCreature.png";
            else if (((Wesen)this).Position == Position.schwebend) CreaturePosition = Ressources.GetRelativeApplicationPathForImagesIcons() + "FloatingCreature.png";
            else if (((Wesen)this).Position == Position.fliegend) CreaturePosition = Ressources.GetRelativeApplicationPathForImagesIcons() + "FlyingCreature.png";
            else if (((Wesen)this).Position == Position.reitend) CreaturePosition = Ressources.GetRelativeApplicationPathForImagesIcons() + "RidingCreature.png";    
        }
    }
}
