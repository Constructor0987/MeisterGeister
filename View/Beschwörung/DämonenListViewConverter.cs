using MeisterGeister.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MeisterGeister.View.Beschwörung
{
    public class DämonenListViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<GegnerBase> dämonen = (List<GegnerBase>)value;
            List<Dämon_Domäne> domänen = new List<Dämon_Domäne>();
            foreach(GegnerBase dämon in dämonen)
            {
                domänen.AddRange(dämon.Beschwörbares.Dämon.Dämon_Domäne);
            }
            ListCollectionView view = new ListCollectionView(domänen);
            view.GroupDescriptions.Add(new PropertyGroupDescription("Domäne"));
            //view.SortDescriptions.Add(new SortDescription("Dämon.Hörner", ListSortDirection.Descending));
            return view;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class DämonDomäneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            GegnerBase g = (GegnerBase)value;
            return g.Beschwörbares.Dämon.Dämon_Domäne.First();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return ((Dämon_Domäne)value).Dämon.Beschwörbares.GegnerBase;
        }
    }
}
