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
    public class TextLabel : BattlegroundBaseObject
    {

        private string _textInLabel;
        private double _labelWidth = 100;
        private double _labelHeight = 100;
        private double _labelPositionX = 50;
        private double _labelPositionY = 50;
        public double _labelOriginalWidth = 100;
        public double _labelOriginalHeigth = 100;
        private double _labelSize = 1;
        private double _rotateAngle = 0;
        
        public TextLabel()
        {}

        public TextLabel(String text, double x, double y)
        {
            LabelPositionX = x;
            LabelPositionY = y;

            LabelWidth = _labelOriginalWidth;
            LabelHeight = _labelOriginalHeigth;

            TextInLabel = text;

            ZDisplayX = x - 10;
            ZDisplayY = y - 10;
        }

        //Objectname
        public override string ObjectName
        {
            get { return "Label as Text"; }
        }

        public double LabelSize
        {
            get { return _labelSize; }
            set
            {
                _labelSize = value;
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

        public double LabelPositionX
        {
            get { return _labelPositionX; }
            set
            {
                _labelPositionX = value;
                OnChanged("LabelPositionX");
            }
        }

        public double LabelPositionY
        {
            get { return _labelPositionY; }
            set
            {
                _labelPositionY = value;
                OnChanged("LabelPositionY");
            }
        }

        public double LabelWidth
        {
            get { return _labelWidth; }
            set
            {
                _labelWidth = value;
                OnChanged("LabelWidth");
            }
        }
        public double LabelHeight
        {
            get { return _labelHeight; }
            set
            {
                _labelHeight = value;
                OnChanged("LabelHeight");
            }
        }

        public String TextInLabel
        {
            get { return _textInLabel; }
            set
            {
                _textInLabel = value;
                OnChanged("TextInLabel");
            }
        }

        public override void MoveObject(double deltaX, double deltaY, bool stickAtCursor)
        {
            LabelPositionX = LabelPositionX + deltaX;
            LabelPositionY = LabelPositionY + deltaY;
            ZDisplayX = LabelPositionX - 10;
            ZDisplayY = LabelPositionY - 10;
              
        }

        public void ScalePicture(double factor)
        {
            LabelHeight = _labelOriginalHeigth * factor;
            LabelWidth = _labelOriginalWidth * factor;
        }


        public void CalculateNewDirection(System.Windows.Point currentMousePos)
        {
            //rotates images per leftclick for 45°
            RotateAngle += 45;
        }


        public override void RunBeforeXMLSerialization()
        {
            //nothing special to take care of...
        }

        public override void RunAfterXMLDeserialization()
        {
        }
    }
}
