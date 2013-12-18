using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Runtime.Serialization;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    [DataContract(IsReference = true)]
    public class BattlegroundCreature:BattlegroundBaseObject
    {
        private string _portraitFilename;
        private double _creatureX = 100;
        private double _creatureY = 100;
        private Random r;
        private double _objectSize = 1;
        private double _imageOriginalWidth = 90;
        private double _imageOriginalHeigth = 90;

        public static String ICON_DIR = "/Images/Icons/General/";

        public BattlegroundCreature()
        {
            r = new Random();
            CreatureX += r.Next(0, 500);
            r = new Random();
            CreatureY += r.Next(0, 500);
        }

        public double CreatureX
        {
            get { return _creatureX; }
            set
            {
                _creatureX = value;
                OnChanged("CreatureX");
            }
        }

        public double CreatureY
        {
            get { return _creatureY; }
            set
            {
                _creatureY = value;
                OnChanged("CreatureY");
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

        private double _creatureWidth = 90;
        public double CreatureWidth
        {
            get { return _creatureWidth; }
            set
            {
                _creatureWidth = value;
                OnChanged("CreatureWidth");
            }
        }

        private double _creatureHeight = 90;
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
            if(portraitFilename != null) CreaturePictureUrl = ishero ? portraitFilename : @portraitFilename.Replace("/DSA MeisterGeister;component", string.Empty);
        }

        public void ScalePicture(double factor)
        {
            CreatureHeight = _imageOriginalHeigth * factor;
            CreatureWidth = _imageOriginalWidth * factor;
        }

        public void MoveObject(double deltaX, double deltaY)
        {
            CreatureX = CreatureX + deltaX;
            CreatureY = CreatureY + deltaY;
            ZDisplayX = CreatureX - 10;
            ZDisplayY = CreatureY - 10;

            //Console.WriteLine(CreatureX + "  "+CreatureY);

        }

        //public Image LoadImage(Uri uri)
        //{
        //    Image portrait = new Image();
        //    //portrait.Width = Width - 2 * frameWidth;
        //    //portrait.Height = Height - 2 * frameWidth;

        //    BitmapImage myBitmapImage = new BitmapImage();

        //    try
        //    {

        //        myBitmapImage.BeginInit();
        //        //myBitmapImage.UriSource = new Uri(@ArenaWindow.PORTRAIT_DIR + filename, UriKind.Relative);
        //        myBitmapImage.UriSource = uri;
        //        myBitmapImage.DecodePixelWidth = 256;// (int)portrait.Width;
        //        myBitmapImage.DecodePixelHeight = 256;// (int)portrait.Height;
        //        myBitmapImage.EndInit();
        //        portrait.Source = myBitmapImage;

        //    }
        //    catch
        //    {
        //        portrait = null;
        //    }

        //    return portrait;
        //}
    }
}
