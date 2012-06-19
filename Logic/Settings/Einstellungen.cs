using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Settings
{
    public static class Einstellungen
    {
        public static bool FrageNeueKampfrundeAbstellen
        {
            get
            {
                if (App.DatenDataSet == null)
                    return false;
                var row = App.DatenDataSet.Einstellungen.FindByName("FrageNeueKampfrundeAbstellen");
                if (row == null || row.IsWertBoolNull())
                    return false;
                return row.WertBool;
            }
            set
            {
                var row = App.DatenDataSet.Einstellungen.FindByName("FrageNeueKampfrundeAbstellen");
                if (row != null)
                    row.WertBool = value;
            }
        }

        public static bool JingleAbstellen
        {
            get
            {
                if (App.DatenDataSet == null)
                    return false;
                var row = App.DatenDataSet.Einstellungen.FindByName("JingleAbstellen");
                if (row == null || row.IsWertBoolNull())
                    return false;
                return row.WertBool;
            }
            set
            {
                var row = App.DatenDataSet.Einstellungen.FindByName("JingleAbstellen");
                if (row != null)
                    row.WertBool = value;
            }
        }

        public static int SelectedTab
        {
            get
            {
                if (App.DatenDataSet == null)
                    return 0;
                var row = App.DatenDataSet.Einstellungen.FindByName("SelectedTab");
                if (row == null || row.IsWertIntNull())
                    return 0;
                return row.WertInt;
            }
            set
            {
                var row = App.DatenDataSet.Einstellungen.FindByName("SelectedTab");
                if (row != null)
                    row.WertInt = value;
            }
        }

        public static string StartTabs
        {
            get
            {
                if (App.DatenDataSet == null)
                    return string.Empty;
                var row = App.DatenDataSet.Einstellungen.FindByName("StartTabs");
                if (row == null || row.IsWertTextNull())
                    return string.Empty;
                return row.WertText;
            }
            set
            {
                var row = App.DatenDataSet.Einstellungen.FindByName("StartTabs");
                if (row != null)
                    row.WertText = value;
            }
        }

        public static string KalenderExpandedSections
        {
            get
            {
                if (App.DatenDataSet == null)
                    return "111111";
                var row = App.DatenDataSet.Einstellungen.FindByName("KalenderExpandedSections");
                if (row == null || row.IsWertStringNull())
                    return "111111";
                return row.WertString;
            }
            set
            {
                var row = App.DatenDataSet.Einstellungen.FindByName("KalenderExpandedSections");
                if (row != null)
                    row.WertString = value;
            }
        }

        public static string UmrechnerExpandedSections
        {
            get
            {
                if (App.DatenDataSet == null)
                    return "111111";
                var row = App.DatenDataSet.Einstellungen.FindByName("UmrechnerExpandedSections");
                if (row == null || row.IsWertStringNull())
                    return "111111";
                return row.WertString;
            }
            set
            {
                var row = App.DatenDataSet.Einstellungen.FindByName("UmrechnerExpandedSections");
                if (row != null)
                    row.WertString = value;
            }
        }

        public static string Standort
        {
            get
            {
                if (App.DatenDataSet == null)
                    return "Gareth#29.79180235685203#3.735098459067687";
                var row = App.DatenDataSet.Einstellungen.FindByName("Standort");
                if (row == null || row.IsWertTextNull())
                    return "Gareth#29.79180235685203#3.735098459067687";
                return row.WertText;
            }
            set
            {
                var row = App.DatenDataSet.Einstellungen.FindByName("Standort");
                if (row != null)
                    row.WertText = value;
            }
        }
    }
}
