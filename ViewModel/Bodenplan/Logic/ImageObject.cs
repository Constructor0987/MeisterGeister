using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public class ImageObject : BattlegroundBaseObject
    {
        private string _pictureUrl;
        private double _imageWidth = 100;
        private double _imageHeight = 100;
        private double _imagePositionX = 50;
        private double _imagePositionY = 50;
        private double _imageOriginalWidth = 100;
        private double _imageOriginalHeigth = 100;
        private double _objectSize = 1;


        public ImageObject()
        {}

        public ImageObject(String urlpath, double x, double y)
        {
            ImagePositionX = x;
            ImagePositionY = y;

            ImageWidth = _imageOriginalWidth;
            ImageHeight = _imageOriginalHeigth;

            PictureUrl = urlpath;

            ZDisplayX = x - 10;
            ZDisplayY = y - 10;
        }

        //Objectname
        public override string ObjectName
        {
            get { return "Bildobjekt"; }
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

        public double ImagePositionX
        {
            get { return _imagePositionX; }
            set
            {
                _imagePositionX = value;
                OnChanged("ImagePositionX");
            }
        }

        public double ImagePositionY
        {
            get { return _imagePositionY; }
            set
            {
                _imagePositionY = value;
                OnChanged("ImagePositionY");
            }
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

        public void MoveObject(double deltaX, double deltaY)
        {
            ImagePositionX = ImagePositionX + deltaX;
            ImagePositionY = ImagePositionY + deltaY;
            ZDisplayX = ImagePositionX - 10;
            ZDisplayY = ImagePositionY - 10;
              
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

        public String ImageToBase64()
        {
            Image image = Image.FromFile(PictureUrl);
            ImageFormat imgformat;
            if (PictureUrl.EndsWith(".png")) imgformat = ImageFormat.Png;
            else if (PictureUrl.EndsWith(".jpg") || PictureUrl.EndsWith(".jpeg")) imgformat = ImageFormat.Jpeg;
            else if (PictureUrl.EndsWith(".bmp")) imgformat = ImageFormat.Bmp;
            else return ""; //return empty string if not a known pictureformat is found...

            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, imgformat);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                return Convert.ToBase64String(imageBytes);
            }
        }

        public Image Base64ToImage(String base64String)
        {
            var picname = PictureUrl.Split('/');
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
    }
}
