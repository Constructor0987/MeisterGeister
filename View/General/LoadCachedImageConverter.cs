using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MeisterGeister.View.General
{
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class LoadCachedImageConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(string) && targetType != typeof(ImageSource))
                throw new InvalidOperationException("The target must be a string or ImageSource");

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(value.ToString());
            try
            {
                image.EndInit();
            }
            catch (Exception)
            {
                return new BitmapImage();
            }

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(BitmapImage))
                throw new InvalidOperationException("The target must be a BitmapImage");
            return Convert((value as BitmapImage).UriSource.ToString(), targetType, parameter, culture);
        }

        #endregion
    }
}
