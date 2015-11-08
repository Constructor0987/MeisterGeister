using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MeisterGeister.View.Karte
{
    public class PflanzeVerbreitungListViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<Pflanze_Verbreitung> pflanzen = (IEnumerable<Pflanze_Verbreitung>)value;
            ListCollectionView view = new ListCollectionView(pflanzen.ToList());
            view.SortDescriptions.Add(new SortDescription("Verbreitung", ListSortDirection.Ascending));
            view.GroupDescriptions.Add(new PropertyGroupDescription("Verbreitung"));
            return view;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
