using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MeisterGeister.View.General
{
    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = (string)value;

            if (path == null)
                return null;

            try
            {
                //Packet-URI or Web
                if (path.StartsWith("/") || path.StartsWith("http"))
                    return path;

                //ABSOLUTE
                if (path.Length > 0 && path[0] == System.IO.Path.DirectorySeparatorChar
                    || path.Length > 1 && path[1] == System.IO.Path.VolumeSeparatorChar)
                    return new BitmapImage(new Uri(path));

                //RELATIVE
                return new BitmapImage(new Uri(MeisterGeister.Logic.Extensions.FileExtensions.GetHomeDirectory() + path));
            }
            catch (Exception)
            {
                return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
