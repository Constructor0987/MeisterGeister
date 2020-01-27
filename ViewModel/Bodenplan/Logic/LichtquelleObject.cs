using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using MeisterGeister.ViewModel.Kampf.Logic;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public class LichtquelleObject : BattlegroundBaseObject
    {
        private double _lichtquelleWidth = 100;
        private double _lichtquelleHeight = 100;
        private double _lichtquellePositionX = 50;
        private double _lichtquellePositionY = 50;
        public double _lichtquelleOriginalWidth = 100;
        public double _lichtquelleOriginalHeigth = 100;
        private double _objectSize = 1;
        private double _rotateAngle = 0;
        private IKämpfer _ikämpfer = null;

        private bool isBackgroundPicture = false;
        private bool isFogPicture = false;

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

        public LichtquelleObject()
        {}

        public KämpferInfo _ki;
        public KämpferInfo ki
        {
            get { return _ki; }
            set { Set(ref _ki, value); }
        }
        public IKämpfer iKämpfer
        {
            get { return _ikämpfer; }
            set { Set(ref _ikämpfer, value); }
        }

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
            get { return "Bildobjekt"; }
        }

        public bool IsBackgroundPicture
        {
            get { return isBackgroundPicture; }
            set
            {
                isBackgroundPicture = value;
                OnChanged("IsBackgroundPicture");
            }
        }

        public bool IsFogPicture
        {
            get { return isFogPicture; }
            set
            {
                isFogPicture = value;
                OnChanged("IsFogPicture");
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

        public double RotateAngle
        {
            get { return _rotateAngle; }
            set
            {
                _rotateAngle = value;
                OnChanged("RotateAngle");
            }
        }

        public double LichquellePositionX
        {
            get { return _lichtquellePositionX; }
            set
            {
                _lichtquellePositionX = value;
                OnChanged("ImagePositionX");
            }
        }

        public double LichquellePositionY
        {
            get { return _lichtquellePositionY; }
            set
            {
                _lichtquellePositionY = value;
                OnChanged("ImagePositionY");
            }
        }

        public double LichquelleWidth
        {
            get { return _lichtquelleWidth; }
            set
            {
                _lichtquelleWidth = value;
                OnChanged("ImageWidth");
            }
        }
        public double LichquelleHeight
        {
            get { return _lichtquelleHeight; }
            set
            {
                _lichtquelleHeight = value;
                OnChanged("ImageHeight");
            }
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
