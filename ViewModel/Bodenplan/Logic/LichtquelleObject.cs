using System;
using System.Xml.Serialization;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    [Serializable]
    public class LichtquelleObject : BattlegroundBaseObject
    {
        private double _lichtquelleOriginalWidth = 100;
        private double _lichtquelleOriginalHeigth = 100;

        private double _lightCreatureX = 0;
        public double LightCreatureX
        {
            get { return _lightCreatureX; }
            set { Set(ref _lightCreatureX, value); }
        }

        private double _lightCreatureY = 0;
        public double LightCreatureY
        {
            get { return _lightCreatureY; }
            set { Set(ref _lightCreatureY, value); }
        }

        private double _lichtquellePixelRadius = 0;
        public double LichtquellePixelRadius
        {
            get { return _lichtquellePixelRadius; }
            set { Set(ref _lichtquellePixelRadius, value); }
        }

        [NonSerialized]
        private KämpferInfo _ki;
        [XmlIgnore]
        public KämpferInfo ki
        {
            get { return _ki; }
            set { Set(ref _ki, value); }
        }

        private IKämpfer _ikämpfer = null;
        public IKämpfer iKämpfer
        {
            get { return _ikämpfer; }
            set { Set(ref _ikämpfer, value); }
        }

        public LichtquelleObject()
        {}

        public LichtquelleObject(String urlpath, double x, double y)
        {
            LichquellePositionX = x;
            LichquellePositionY = y;

            LichquelleWidth = _lichtquelleOriginalWidth;
            LichquelleHeight = _lichtquelleOriginalHeigth;
            
            ZDisplayX = x - 10;
            ZDisplayY = y - 10;
        }

        //Objectname
        public override string ObjectName
        {
            get { return "Lichtquellenobjekt"; }
        }

        private bool isBackgroundPicture = false;
        public bool IsBackgroundPicture
        {
            get { return isBackgroundPicture; }
            set { Set(ref isBackgroundPicture, value); }
        }

        private bool isFogPicture = false;
        public bool IsFogPicture
        {
            get { return isFogPicture; }
            set { Set(ref isFogPicture, value); }
        }

        private double _objectSize = 1;
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

        private double _rotateAngle = 0;
        public double RotateAngle
        {
            get { return _rotateAngle; }
            set { Set(ref _rotateAngle, value); }
        }

        private double _lichtquellePositionX = 50;
        public double LichquellePositionX
        {
            get { return _lichtquellePositionX; }
            set { Set(ref _lichtquellePositionX, value); }
        }

        private double _lichtquellePositionY = 50;
        public double LichquellePositionY
        {
            get { return _lichtquellePositionY; }
            set { Set(ref _lichtquellePositionY, value); }
        }

        private double _lichtquelleWidth = 100;
        public double LichquelleWidth
        {
            get { return _lichtquelleWidth; }
            set { Set(ref _lichtquelleWidth, value); }
        }

        private double _lichtquelleHeight = 100;
        public double LichquelleHeight
        {
            get { return _lichtquelleHeight; }
            set { Set(ref _lichtquelleHeight, value); }
        }
       
        public override void MoveObject(double deltaX, double deltaY, bool stickAtCursor)
        {
            LichquellePositionX = LichquellePositionX + deltaX;
            LichquellePositionY = LichquellePositionY + deltaY;
            ZDisplayX = LichquellePositionX - 10;
            ZDisplayY = LichquellePositionY - 10;
              
        }

        public void ScalePicture(double factor)
        {
            LichquelleHeight = _lichtquelleOriginalHeigth * factor;
            LichquelleWidth = _lichtquelleOriginalWidth * factor;
        }

        public override void RunBeforeXMLSerialization()
        {
            //nothing special to take care of...
        }

        public override void RunAfterXMLDeserialization()
        {

        }

        public void CalculateNewDirection(System.Windows.Point currentMousePos)
        {
            //rotates images per leftclick for 45°
            RotateAngle += 45;
        }
    }
}
