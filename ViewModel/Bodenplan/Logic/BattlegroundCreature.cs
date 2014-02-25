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
        private double _imageOriginalWidth = 80;
        private double _imageOriginalHeigth = 80;

        public static String ICON_DIR = "/Images/Icons/General/";

        public BattlegroundCreature()
        {
            r = new Random();
            CreatureX += r.Next(0, 500);
            r = new Random();
            CreatureY += r.Next(0, 500);
            MoveObject(0,0); //for initial position of ZLevel Display
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
    }
}
