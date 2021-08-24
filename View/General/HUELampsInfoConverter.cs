using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using Q42.HueApi;
using Q42.HueApi.Models;
using MeisterGeister.ViewModel;
using MeisterGeister.ViewModel.Basar.Logic;


namespace MeisterGeister.View.General
{
    [ValueConversion(typeof(bool), typeof(bool))]

    public class HUELampsInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            MainViewModel MainVM = MainViewModel.Instance;
            if (MainVM == null)
                return null;

            Scene sc = MainVM.lstHUEScenes.FirstOrDefault(t => t.Name == value as string);
            if (sc == null)
                return null;
            return "Angesteuerte Lampen:\n-------------------------\n" + string.Join("\n",
                sc.Lights.Select(t => MainVM.lstHUELights.FirstOrDefault(l => l.Id == t).Id + " : " +
                MainVM.lstHUELights.FirstOrDefault(l => l.Id == t).Name));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // keine Rückkonvertierung vorgesehen
            return null;
        }
    }

    //public class InverseBooleanConverter : IValueConverter
    //{
    //    #region IValueConverter Members

    //    public object Convert(object value, Type targetType, object parameter,
    //        System.Globalization.CultureInfo culture)
    //    {
    //        if (targetType != typeof(bool) && targetType != typeof(bool?))
    //            throw new InvalidOperationException("The target must be a boolean");

    //        return !(bool)value;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter,
    //        System.Globalization.CultureInfo culture)
    //    {
    //        return Convert(value, targetType, parameter, culture);
    //    }

    //    #endregion
    //}
}
