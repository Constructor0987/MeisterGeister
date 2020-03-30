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
    public class MP4Object : BattlegroundBaseObject
    {
        private string _videoUrl;
        private double _videoWidth = 1000;
        private double _videoHeight = 1000;
        private double _videoPositionX = 50;
        private double _videoPositionY = 50;
        public double _videoOriginalWidth = 10000;
        public double _videoOriginalHeigth = 10000;
        private double _objectSize = 1;
        private double _rotateAngle = 0;
        private double _minPosition;
        private double _maxPosition;
        private double _videoLength;
        private double _videoSpeedRatio;
        private bool _ismute;

        private bool isBackgroundPicture = false;
        private bool isFogPicture = false;

        public MP4Object()
        {}

        public MP4Object(String urlpath, double x, double y)
        {
            VideoPositionX = x;
            VideoPositionY = y;

            VideoWidth = _videoOriginalWidth;
            VideoHeight = _videoOriginalHeigth;

            VideoUrl = urlpath;

            ZDisplayX = x - 10;
            ZDisplayY = y - 10;
        }

        //Objectname
        public override string ObjectName
        {
            get { return "Videoobjekt"; }
        }

        public bool IsBackgroundPicture
        {
            get { return isBackgroundPicture; }
            set
            {
                isBackgroundPicture = value;
                OnChanged(nameof(IsBackgroundPicture));
            }
        }

        public bool IsFogPicture
        {
            get { return isFogPicture; }
            set
            {
                isFogPicture = value;
                OnChanged(nameof(IsFogPicture));
            }
        }

        public double ObjectSize
        {
            get { return _objectSize; }
            set
            {
                _objectSize = value;
                ScaleVideo(value);
                OnChanged(nameof(ObjectSize));
            }
        }

        public double RotateAngle
        {
            get { return _rotateAngle; }
            set
            {
                _rotateAngle = value;
                OnChanged(nameof(RotateAngle));
            }
        }

        public double VideoPositionX
        {
            get { return _videoPositionX; }
            set
            {
                _videoPositionX = value;
                OnChanged(nameof(VideoPositionX));
            }
        }

        public double VideoPositionY
        {
            get { return _videoPositionY; }
            set
            {
                _videoPositionY = value;
                OnChanged(nameof(VideoPositionY));
            }
        }

        public double VideoWidth
        {
            get { return _videoWidth; }
            set
            {
                _videoWidth = value;
                OnChanged(nameof(VideoWidth));
            }
        }
        public double VideoHeight
        {
            get { return _videoHeight; }
            set
            {
                _videoHeight = value;
                OnChanged(nameof(VideoHeight));
            }
        }

        public bool IsMute
        {
            get { return _ismute; }
            set { Set(ref _ismute, value); }
        }


        public double VideoSpeedRatio
        {
            get { return _videoSpeedRatio; }
            set { Set(ref _videoSpeedRatio, value); }
        }

        public double MaxPosition
        {
            get { return _maxPosition; }
            set { Set(ref _maxPosition, value); }
        }

        public double VideoLength
        {
            get { return _videoLength; }
            set { Set(ref _videoLength, value); }
        }

        public double MinPosition
        {
            get { return _minPosition; }
            set { Set(ref _minPosition, value); }
        }

        public String VideoUrl
        {
            get { return _videoUrl; }
            set { Set(ref _videoUrl, value); }
        }

        public override void MoveObject(double deltaX, double deltaY, bool stickAtCursor)
        {
            VideoPositionX = VideoPositionX + deltaX;
            VideoPositionY = VideoPositionY + deltaY;
            ZDisplayX = VideoPositionX - 10;
            ZDisplayY = VideoPositionY - 10;
              
        }

        public void ScaleVideo(double factor)
        {
            VideoHeight = _videoOriginalHeigth * factor/15000;
            VideoWidth = _videoOriginalWidth * factor/15000;
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
            //rotates Videos per leftclick for 45°
            RotateAngle += 45;
        }
    }
}
