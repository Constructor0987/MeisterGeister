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
using Q42.HueApi.Models.Groups;

namespace MeisterGeister.View.General
{
    [ValueConversion(typeof(bool), typeof(bool))]

    public class HUEGroupNoToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            MainViewModel MainVM = MainViewModel.Instance;
            if (MainVM == null)
                return null;

            Group gr = MainVM.lstHUEGroups.FirstOrDefault(t => t.Id == value as string);
            if (gr == null)
                return null;
            else
                return gr.Name + ": ";
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
