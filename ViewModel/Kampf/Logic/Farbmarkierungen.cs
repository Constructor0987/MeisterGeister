using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xceed.Wpf.Toolkit;
using MeisterGeister.Logic.Settings;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public class Farbmarkierungen
    {
        private static ObservableCollection<ColorItem> standardColors = null;
        public static ObservableCollection<ColorItem> StandardColors
        {
            get
            {
                if (standardColors == null)
                {
                    standardColors = new ObservableCollection<ColorItem>()
                    {
                        new ColorItem(Colors.Aqua, "Aqua"),
                        new ColorItem(Colors.Azure, "Azure"),
                        new ColorItem(Colors.BlueViolet, "BlueViolet")
                    };
                }
                return standardColors;
            }
            set
            {
                standardColors = value;   
            }
        }

        private static ObservableCollection<ColorItem> recentColors = null;
        public static ObservableCollection<ColorItem> RecentColors
        {
            get
            {
                if (recentColors == null)
                {
                    recentColors = new ObservableCollection<ColorItem>();
                    if (!String.IsNullOrEmpty(Einstellungen.KampfRecentColors))
                    {
                        var colorStrings = Einstellungen.KampfRecentColors.Split(',');
                        foreach (var cs in colorStrings)
                        {
                            if (String.IsNullOrWhiteSpace(cs))
                                continue;
                            Color c = Colors.Transparent;
                            try
                            {
                                c = (Color)ColorConverter.ConvertFromString(cs);
                            }
                            catch(Exception e)
                            {
                                var m = e.Message;
                            }
                            if (c != Colors.Transparent)
                                recentColors.Add(new ColorItem(c, cs));
                        }
                    }
                }
                recentColors.CollectionChanged += recentColors_CollectionChanged;
                return recentColors;
            }
            set
            {
                recentColors.CollectionChanged -= recentColors_CollectionChanged;
                recentColors = value;
                recentColors.CollectionChanged += recentColors_CollectionChanged;
                SaveRecentColorsToEinstellungen();
            }
        }

        static void SaveRecentColorsToEinstellungen()
        {
            string colorString = "";
            foreach (var c in recentColors)
            {
                colorString += (colorString.Length > 0) ? "," : "" + c.Name;
            }
            Einstellungen.KampfRecentColors = colorString;
        }

        static void recentColors_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SaveRecentColorsToEinstellungen();
        }
    }
}
