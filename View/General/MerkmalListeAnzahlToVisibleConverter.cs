using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace MeisterGeister.View.General
{
    [ValueConversion(typeof(bool), typeof(bool))]

    public class MerkmalListeAnzahlToVisibleConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new MerkmalListeAnzahlToVisibleConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int compareToValue = System.Convert.ToInt32(parameter);
            
            List<string> lstMerkmale = new List<string>();
            lstMerkmale = (value as string).Trim(' ').Split(',').ToList();

            return lstMerkmale.Count() >= compareToValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
