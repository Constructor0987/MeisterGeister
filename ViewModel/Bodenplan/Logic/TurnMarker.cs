using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public class TurnMarker : BattlegroundBaseObject
    {
        private string _pictureUrl;
        private double _imageWidth = 200;
        private double _imageHeight = 200;
        private double _markerPositionX = 0;
        private double _markerPositionY = 0;
        private Thickness _markerMargin = new Thickness();
        public double _imageOriginalWidth = 200;
        public double _imageOriginalHeigth = 200;
        private double _objectSize = 1;
        private double _rotateAngle = 0;
        private Visibility _visibility = Visibility.Visible;

        private System.Windows.Thickness _turnMarkerMargin = new Thickness();

        private double _turnMarkerDurchmesser = 200;

        private bool isBackgroundPicture = false;
        private bool isFogPicture = false;

        public TurnMarker()
        { }

        public TurnMarker(double x, double y)
        {
            MarkerPositionX = x;
            MarkerPositionY = y;

            ImageWidth = _imageOriginalWidth;
            ImageHeight = _imageOriginalHeigth;

            PictureUrl = "/DSA MeisterGeister;component/Images/Bodenplan/Turnmarker.png";

            ZDisplayX = x - 10;
            ZDisplayY = y - 10;
        }

        //Objectname
        public override string ObjectName
        {
            get { return "TurnMarker"; }
        }

        public bool IsBackgroundPicture
        {
            get { return isBackgroundPicture; }
            set { Set(ref isBackgroundPicture, value); }
        }

        public bool IsFogPicture
        {
            get { return isFogPicture; }
            set { Set(ref isFogPicture, value); }
        }

        public Visibility Visible
        {
            get { return _visibility; }
            set { Set(ref _visibility, value); }
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


        public double TurnMarkerDurchmesser
        {
            get { return _turnMarkerDurchmesser; }
            set { Set(ref _turnMarkerDurchmesser, value); }
        }

        public System.Windows.Thickness TurnMarkerMargin
        {
            get { return _turnMarkerMargin; }
            set { Set(ref _turnMarkerMargin, value); }
        }

        public double RotateAngle
        {
            get { return _rotateAngle; }
            set { Set(ref _rotateAngle, value); }
        }

        public double MarkerPositionX
        {
            get { return _markerPositionX; }
            set
            {
                Set(ref _markerPositionX, value);
                MarkerMargin = new Thickness(MarkerPositionX, MarkerPositionY, 0, 0);
            }
        }

        public double MarkerPositionY
        {
            get { return _markerPositionY; }
            set
            {
                Set(ref _markerPositionY, value);
                MarkerMargin = new Thickness(MarkerPositionX, MarkerPositionY, 0, 0);
            }
        }

        public Thickness MarkerMargin
        {
            get { return _markerMargin; }
            set { Set(ref _markerMargin, value); }
        }

        public double ImageWidth
        {
            get { return _imageWidth; }
            set
            {
                _imageWidth = value;
                OnChanged("ImageWidth");
            }
        }
        public double ImageHeight
        {
            get { return _imageHeight; }
            set
            {
                _imageHeight = value;
                OnChanged("ImageHeight");
            }
        }

        public String PictureUrl
        {
            get { return _pictureUrl; }
            set
            {
                _pictureUrl = value;
                OnChanged("PictureUrl");
            }
        }

        public override void MoveObject(double deltaX, double deltaY, bool stickAtCursor)
        {
            MarkerPositionX = MarkerPositionX + deltaX;
            MarkerPositionY = MarkerPositionY + deltaY;
            ZDisplayX = MarkerPositionX - 10;
            ZDisplayY = MarkerPositionY - 10;

        }

        public void ScalePicture(double factor)
        {
            ImageHeight = _imageOriginalHeigth * factor;
            ImageWidth = _imageOriginalWidth * factor;
        }

        public override void RunBeforeXMLSerialization()
        {
            //nothing special to take care of...
        }

        public override void RunAfterXMLDeserialization()
        {
            var picname = PictureUrl.Split('\\');
            PictureUrl = Ressources.GetFullApplicationPathForPictures() + picname[picname.Length - 1];
        }
    }
}
