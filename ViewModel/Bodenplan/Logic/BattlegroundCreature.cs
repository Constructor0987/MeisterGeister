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
    public class BattlegroundCreature : BattlegroundBaseObject
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
            set { Set(ref _creaturePosition, value); }
        }

        public BattlegroundCreature()
        {
            r = new Random();
            CreatureX += r.Next(0, 500);
            r = new Random();
            CreatureY += r.Next(0, 500);
            MoveObject(0,0,false); //for initial position of ZLevel Display
            CreateSightArea();
            ((Wesen)this).Position = Position.Stehend;
            UpdateCreaturePosition();
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
                Set(ref _creaturePictureUrl, value);
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
            //CreaturePictureUrl = portraitFilename;
            //if(!File.Exists(portraitFilename)) 
            if(portraitFilename != null ) if(portraitFilename.Length!=0) CreaturePictureUrl = ishero ? portraitFilename : @portraitFilename.Replace("/DSA MeisterGeister;component", string.Empty);
        }

        public void ScalePicture(double factor)
        {
            CreatureHeight = _imageOriginalHeigth * factor;
            CreatureWidth = _imageOriginalWidth * factor;
        }

        public override void MoveObject(double deltaX, double deltaY, bool stickAtCursor)
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


            double d = Math.Cos(45 * Math.PI / 180) * _sightAreaLength;
            double c = Math.Sin(45 * Math.PI / 180) * _sightAreaLength;

            double b = Math.Cos(34 * Math.PI / 180) * _sightAreaLength;
            double a = Math.Sin(34 * Math.PI / 180) * _sightAreaLength;

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
            else if (SightLineSektor == 6)
            {
                SightAreaGeometryData.Figures.First().StartPoint = new Point(CreatureX + CreatureWidth / 2, CreatureY + CreatureHeight / 2 - _sightAreaLength);
                SightAreaGeometryData.Figures.First().Segments[0] = new ArcSegment(new Point(CreatureX + CreatureWidth / 2, CreatureY + CreatureHeight / 2 + _sightAreaLength),
                    new Size(80, 80), 0, true, SweepDirection.Clockwise, true);
            }
            else if (SightLineSektor == 7)
            {
                SightAreaGeometryData.Figures.First().StartPoint = new Point(CreatureX + CreatureWidth / 2, CreatureY + CreatureHeight / 2 - _sightAreaLength);
                SightAreaGeometryData.Figures.First().Segments[0] = new ArcSegment(new Point(CreatureX + CreatureWidth / 2, CreatureY + CreatureHeight / 2 + _sightAreaLength),
                    new Size(80, 80), 0, true, SweepDirection.Counterclockwise, true);
            }
            else if (SightLineSektor == 8)
            {
                SightAreaGeometryData.Figures.First().StartPoint = new Point(CreatureX + CreatureWidth / 2 - c, CreatureY + CreatureHeight / 2 - d);
                SightAreaGeometryData.Figures.First().Segments[0] = new ArcSegment(new Point(CreatureX + CreatureWidth / 2 + c, CreatureY + CreatureHeight / 2 + d),
                    new Size(80, 80), 0, true, SweepDirection.Counterclockwise, true);
            }
            else if (SightLineSektor == 9)
            {
                SightAreaGeometryData.Figures.First().StartPoint = new Point(CreatureX + CreatureWidth / 2 - c, CreatureY + CreatureHeight / 2 + d);
                SightAreaGeometryData.Figures.First().Segments[0] = new ArcSegment(new Point(CreatureX + CreatureWidth / 2 + c, CreatureY + CreatureHeight / 2 - d),
                    new Size(80, 80), 0, true, SweepDirection.Clockwise, true);
            }
            else if (SightLineSektor == 10)
            {
                SightAreaGeometryData.Figures.First().StartPoint = new Point(CreatureX + CreatureWidth / 2 - c, CreatureY + CreatureHeight / 2 - d);
                SightAreaGeometryData.Figures.First().Segments[0] = new ArcSegment(new Point(CreatureX + CreatureWidth / 2 + c, CreatureY + CreatureHeight / 2 + d),
                    new Size(80, 80), 0, true, SweepDirection.Clockwise, true);
            }
            if (SightLineSektor == 11)
            {
                SightAreaGeometryData.Figures.First().StartPoint = new Point(CreatureX + CreatureWidth / 2 - c, CreatureY + CreatureHeight / 2 + d);
                SightAreaGeometryData.Figures.First().Segments[0] = new ArcSegment(new Point(CreatureX + CreatureWidth / 2 + c, CreatureY + CreatureHeight / 2 - d),
                    new Size(80, 80), 0, true, SweepDirection.Counterclockwise, true);
            }
        }

        public void CalculateNewSightLineSektor(Point currentMousePos, bool rectGrid = false)
        {
            // How the calculations and Sektores are designed...
            double _a = currentMousePos.X - (CreatureX + CreatureWidth / 2);
            double _b = currentMousePos.Y - (CreatureY + CreatureHeight / 2);
            double _alpha = Math.Atan2(_a, _b) * (180 / Math.PI);

            if (!rectGrid)
            {
                /*    -146°   146°
                 *        \ 4 /
                 *       3 \ / 5
                 * -90° ---------  90°
                 *       2 / \ 0
                 *        / 1 \
                 *    -34°    34°
                 */

                if (_alpha <= 146 && _alpha > 90) SightLineSektor = 5;
                else if (_alpha <= 90 && _alpha > 34) SightLineSektor = 0;
                else if (_alpha <= 34 && _alpha > -34) SightLineSektor = 1;
                else if (_alpha <= -34 && _alpha > -90) SightLineSektor = 2;
                else if (_alpha <= -90 && _alpha > -146) SightLineSektor = 3;
                else SightLineSektor = 4;
            }
            else
            {

                /*         -157.5   157.5
                 *       ------|-----|------
                 *       |     |     |     |
                 *       |  9  |  4  |  10 |
                 *       |     |     |     |
                 *-112.5 ------------------- 112.5
                 *       |     |     |     |
                 *       |  7  |  X  |  6  |
                 *       |     |     |     |
                 * -67.5 ------------------- 67.5
                 *       |     |     |     |
                 *       |  8  |  1  |  11 |
                 *       |     |     |     |
                 *       ------|-----|------
                 *          -22.5   22.5 
                 */

                if (_alpha <= 157.5 && _alpha > 112.5) SightLineSektor = 10;
                else if (_alpha <= 112.5 && _alpha > 67.5) SightLineSektor = 6;
                else if (_alpha <= 67.5 && _alpha > 22.5) SightLineSektor = 11;
                else if (_alpha <= 22.5 && _alpha > -22.5) SightLineSektor = 1;
                else if (_alpha <= -22.5 && _alpha > -67.5) SightLineSektor = 8;
                else if (_alpha <= -67.5 && _alpha > -112.5) SightLineSektor = 7;
                else if (_alpha <= -112.5 && _alpha > -157.5) SightLineSektor = 9;
                else SightLineSektor = 4;

            }
            CalculateSightArea(); //update sightarea
        }

        public void UpdateCreaturePosition()
        {
            if (this as Wesen == null) return;
            if (((Wesen)this).Position == Position.Liegend) CreaturePosition = Ressources.GetRelativeApplicationPathForImagesIcons() + "OnTheGroundCreature.png";
            else if (((Wesen)this).Position == Position.Kniend) CreaturePosition = Ressources.GetRelativeApplicationPathForImagesIcons() + "KneelingCreature.png";
            else if (((Wesen)this).Position == Position.Stehend) CreaturePosition = Ressources.GetRelativeApplicationPathForImagesIcons() + "StandingCreature.png";
            else if (((Wesen)this).Position == Position.Schwebend) CreaturePosition = Ressources.GetRelativeApplicationPathForImagesIcons() + "FloatingCreature.png";
            else if (((Wesen)this).Position == Position.Fliegend) CreaturePosition = Ressources.GetRelativeApplicationPathForImagesIcons() + "FlyingCreature.png";
            else if (((Wesen)this).Position == Position.Reitend) CreaturePosition = Ressources.GetRelativeApplicationPathForImagesIcons() + "RidingCreature.png";
        }
    }
}
