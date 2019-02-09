using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MeisterGeister.View.General;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    [DataContract(IsReference = true)]
    public class BattlegroundCreature : BattlegroundBaseObject
    {
        public static string ICON_DIR = "/Images/Icons/General/";
        private Image _creatureImage = null;
        public Image CreatureImage
        {
            get { return _creatureImage; }
            set { Set(ref _creatureImage, value); }
        }

        public string _creaturePosition = Ressources.GetRelativeApplicationPathForImagesIcons() + "FloatingCreature.png";
        public double _objectSize = 1;
        private double _creatureAktionsbuttonsPos = 112;//140;
        private double _creatureHeight = 80;
        private double _creatureNameX = 90;
        private double _creatureNameY = 90;
        private string _creaturePictureUrl;
        private double _creatureWidth = 80;
        private double _creatureX = 5000;

        //1200;
        private double _creatureY = 5000;

        private readonly double _imageOriginalHeigth = 80;
        private readonly double _imageOriginalWidth = 80;
        private KämpferInfo _ki = null;
        private Thickness _marginCreatureAktionsbuttons = new Thickness() { Left = 80, Top = -31, Right = 0, Bottom = 0 };
        private Thickness _marginCreatureLangAkt = new Thickness() { Left = 80 - 40, Top = 80 - 20, Right = 6, Bottom = 0 };

        private Thickness _marginCreatureATPAaktionen = new Thickness() { Left = -15, Top = 80, Right = 16, Bottom = 0 };

        private double _midCreatureX = 0;

        private double _midCreatureY = 0;

        private string _portraitFilename;

        private ManöverInfo _selectedManöver = null;

        private bool _showLebensbalken = false;

        private PathGeometry _sightAreaGeometryData = new PathGeometry();

        private double _sightAreaLength = 120;
        private int _sightLineSektor = 4;

        //600;
        private Random r;

        public BattlegroundCreature()
        {
            r = new Random();
            CreatureX += r.Next(0, 500);
            r = new Random();
            CreatureY += r.Next(0, 500);
            MoveObject(0, 0, false); //for initial position of ZLevel Display
            CreateSightArea();
            ShowLebensbalken = MeisterGeister.Logic.Einstellung.Einstellungen.LebensbalkenImmerAnzeigen || (this as Wesen).IsHeld;


        }

        public double CreatureAktionsbuttonsPos
        {
            get { return _creatureAktionsbuttonsPos; }

            set
            {
                _creatureAktionsbuttonsPos = value;
                OnChanged("CreatureAktionsbuttonsPos");
            }
        }

        public double CreatureHeight
        {
            get { return _creatureHeight; }
            set { Set(ref _creatureHeight, value); }
        }

        public double CreatureNameX
        {
            get { return _creatureNameX; }

            set
            {
                _creatureNameX = value;
                OnChanged("CreatureNameX");
            }
        }

        public double CreatureNameY
        {
            get { return _creatureNameY; }

            set
            {
                _creatureNameY = value;
                OnChanged("CreatureNameY");
            }
        }

        public string CreaturePictureUrl
        {
            get { return _creaturePictureUrl; }
            set { Set(ref _creaturePictureUrl, value); }
        }

        public string CreaturePosition
        {
            get { return _creaturePosition; }
            set { Set(ref _creaturePosition, value); }
        }

        public double CreatureWidth
        {
            get { return _creatureWidth; }
            set { Set(ref _creatureWidth, value); }
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

        public KämpferInfo ki
        {
            get
            {
                if (_ki == null && Global.CurrentKampf != null && Global.CurrentKampf.Kampf != null && Global.CurrentKampf.Kampf.Kämpfer.Count > 0)
                {
                    _ki = Global.CurrentKampf.Kampf.Kämpfer.FirstOrDefault(t => t.Kämpfer == (this as Wesen));
                }
                
                return _ki;
            }

            set
            {
                Set(ref _ki, value);
            }
        }

        public Thickness MarginCreatureAktionsbuttons
        {
            get { return _marginCreatureAktionsbuttons; }

            set
            {
                _marginCreatureAktionsbuttons = value;
                OnChanged(nameof(MarginCreatureAktionsbuttons));
            }
        }
        public Thickness MarginCreatureLangAkt
        {
            get { return _marginCreatureLangAkt; }

            set
            {
                _marginCreatureLangAkt = value;
                OnChanged(nameof(MarginCreatureLangAkt));
            }
        }

        public Thickness MarginCreatureATPAaktionen
        {
            get { return _marginCreatureATPAaktionen; }

            set
            {
                _marginCreatureATPAaktionen = value;
                OnChanged(nameof(MarginCreatureATPAaktionen));
            }
        }

        public double MidCreatureX
        {
            get { return _midCreatureX; }

            set
            {
                Set(ref _midCreatureX, value);
                if (ki != null)
                {
                    ki.LightCreatureX = CreatureX - (ki != null ? ki.LichtquellePixel : 0);
                }
            }
        }

        public double MidCreatureY
        {
            get { return _midCreatureY; }

            set
            {
                Set(ref _midCreatureY, value);
                if (ki != null)
                {
                    ki.LightCreatureY = CreatureY - (ki != null ? ki.LichtquellePixel : 0);
                }
            }
        }

        //Objectname
        public override string ObjectName
        {
            get { return "Held/Monster"; }
        }

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

        public string PortraitFileName
        {
            get { return _portraitFilename; }
            set { _portraitFilename = value; }
        }

        public ManöverInfo SelectedManöver
        {
            get { return _selectedManöver; }
            set { Set(ref _selectedManöver, value); }
        }

        public bool ShowLebensbalken
        {
            get { return _showLebensbalken; }
            set { Set(ref _showLebensbalken, value); }
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

        public int SightLineSektor
        {
            get { return _sightLineSektor; }

            set
            {
                _sightLineSektor = value;
                OnChanged("SightLineSektor");
            }
        }

        public void CalculateNewSightLineSektor(Point currentMousePos, bool rectGrid = false)
        {
            // How the calculations and Sektores are designed...
            var _a = currentMousePos.X - (CreatureX + CreatureWidth / 2);
            var _b = currentMousePos.Y - (CreatureY + CreatureHeight / 2);
            var _alpha = Math.Atan2(_a, _b) * (180 / Math.PI);

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

                if (_alpha <= 146 && _alpha > 90)
                {
                    SightLineSektor = 5;
                }
                else if (_alpha <= 90 && _alpha > 34)
                {
                    SightLineSektor = 0;
                }
                else if (_alpha <= 34 && _alpha > -34)
                {
                    SightLineSektor = 1;
                }
                else if (_alpha <= -34 && _alpha > -90)
                {
                    SightLineSektor = 2;
                }
                else if (_alpha <= -90 && _alpha > -146)
                {
                    SightLineSektor = 3;
                }
                else
                {
                    SightLineSektor = 4;
                }
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

                if (_alpha <= 157.5 && _alpha > 112.5)
                {
                    SightLineSektor = 10;
                }
                else if (_alpha <= 112.5 && _alpha > 67.5)
                {
                    SightLineSektor = 6;
                }
                else if (_alpha <= 67.5 && _alpha > 22.5)
                {
                    SightLineSektor = 11;
                }
                else if (_alpha <= 22.5 && _alpha > -22.5)
                {
                    SightLineSektor = 1;
                }
                else if (_alpha <= -22.5 && _alpha > -67.5)
                {
                    SightLineSektor = 8;
                }
                else if (_alpha <= -67.5 && _alpha > -112.5)
                {
                    SightLineSektor = 7;
                }
                else if (_alpha <= -112.5 && _alpha > -157.5)
                {
                    SightLineSektor = 9;
                }
                else
                {
                    SightLineSektor = 4;
                }
            }
            CalculateSightArea(); //update sightarea
        }

        public void CalculateSightArea()
        {
            if (SightAreaGeometryData.Figures.Count == 0)
            {
                return;
            }

            var d = Math.Cos(45 * Math.PI / 180) * _sightAreaLength;
            var c = Math.Sin(45 * Math.PI / 180) * _sightAreaLength;

            var b = Math.Cos(34 * Math.PI / 180) * _sightAreaLength;
            var a = Math.Sin(34 * Math.PI / 180) * _sightAreaLength;

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

        public void LoadBattlegroundPortrait(string portraitFilename, bool ishero)
        {
            CreaturePictureUrl = ICON_DIR + "fragezeichen.png";
            //CreaturePictureUrl = portraitFilename;
            //if(!File.Exists(portraitFilename))
            if (portraitFilename != null)
            {
                if (portraitFilename.Length != 0)
                {
                    CreaturePictureUrl = ishero ? portraitFilename : @portraitFilename.Replace("/DSA MeisterGeister;component", string.Empty);
                }
            }
            var img = new Image
            {
                Width = CreatureWidth,
                Height = CreatureHeight,
                Stretch = Stretch.Fill
            };
            CreatureImage = SetCreatrueImage(img);

            //string datei;
            //try
            //{
            //    datei = LoadImage(new Uri(@portraitFilename.Replace("/DSA MeisterGeister;component", string.Empty), UriKind.RelativeOrAbsolute));
            //}
            //catch
            //{
            //    datei = LoadImage(new Uri(@ArenaWindow.ICON_DIR + "fragezeichen.png", UriKind.Relative));
            //}
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
            MidCreatureX = CreatureX;
            MidCreatureY = CreatureY;
        }

        public override void RunAfterXMLDeserialization()
        {
            //nothing special to take care of...
        }

        public override void RunBeforeXMLSerialization()
        {
            //nothing special to take care of...
        }

        public Image SetCreatrueImage(Image img)
        {
            var pic = ki.Kämpfer.Bild ?? "/DSA MeisterGeister;component/Images/Icons/General/fragezeichen.png";
            try
            {
                if (!pic.ToLower().StartsWith("http"))
                {
                    if (!pic.StartsWith("/") && !File.Exists(pic))
                    {
                        pic = "/DSA MeisterGeister;component/Images/Icons/General/fragezeichen.png";
                    }

                    var src = new ImagePathConverter().Convert(pic, typeof(Image), null, null);
                    img.Source = src.ToString().StartsWith("/DSA MeisterGeister;") ?
                        new BitmapImage(new Uri("pack://application:,,," + src.ToString())) : src as ImageSource;
                }
                else
                {
                    var bitmap = new BitmapImage();
                    if (pic.ToLower().StartsWith("http"))
                    {
                        var buffer = new WebClient().DownloadData(pic);

                        using (var stream = new MemoryStream(buffer))
                        {
                            bitmap.BeginInit();
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.StreamSource = stream;
                            bitmap.EndInit();
                        }
                    }
                    img.Source = bitmap;
                }
                img.Tag = pic;
                return img;
            }
            catch
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/fragezeichen.png"));
                img.Tag = "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/fragezeichen.png";
                return img;
            }
        }

        public void ScalePicture(double factor)
        {
            CreatureHeight = _imageOriginalHeigth * factor;
            CreatureWidth = _imageOriginalWidth * factor;
            MarginCreatureAktionsbuttons = new Thickness() { Left = CreatureWidth, Top = -31, Right = 0, Bottom = 0 };
            CreatureAktionsbuttonsPos = Math.Max(112, 30 + CreatureHeight + 2 * factor);//130, 30 + CreatureHeight + 30 + 2 * factor);
            MarginCreatureLangAkt = new Thickness() { Left = CreatureWidth-40, Top = CreatureHeight-20, Right = 6, Bottom = 0 };

            MarginCreatureATPAaktionen = new Thickness() { Left = -15, Top = CreatureWidth, Right = 16, Bottom = 0 };
            SightAreaLength = CreatureWidth + 40;
        }

        private void CreateSightArea()
        {
            var _pathFigureCollection = new PathFigureCollection();
            var _pathFigure = new PathFigure();
            var _pathSegmentCollection = new PathSegmentCollection();

            // 6 possible sightline positions (hexagon)

            //first:
            _pathFigure.StartPoint = new Point(CreatureX + (ki != null ? CreatureWidth : 80) / 2 - 120, CreatureY + CreatureHeight / 2);
            _pathFigure.Segments.Add(new ArcSegment(new Point(CreatureX + (ki != null ? CreatureWidth : 80) / 2 + 120, CreatureY + CreatureHeight / 2),
                new Size(80, 80), 0, true, SweepDirection.Clockwise, true));
            _pathFigure.IsFilled = true;
            _pathFigureCollection.Add(_pathFigure);
            SightAreaGeometryData.Figures = _pathFigureCollection;
        }
    }
}
