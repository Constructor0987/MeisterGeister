using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.Logic.Settings
{
    public static class Einstellungen
    {
        public static T GetOrCreateEinstellung<T>(string name, T defaultValue)
        {
            if (Global.IsInitialized)
            {
                Model.Einstellungen e = Global.ContextHeld.LoadEinstellungByName(name);
                if (e == null)
                {
                    e = Einstellungen.SetEinstellung<T>(name, defaultValue);
                }
                if (typeof(T) == typeof(Boolean) || typeof(T) == typeof(bool))
                    return (T)(object)e.WertBool;
                else if (typeof(T) == typeof(int) || typeof(T) == typeof(Int32) || typeof(T) == typeof(Int64))
                    return (T)(object)e.WertInt;
                else if (typeof(T) == typeof(string) || typeof(T) == typeof(String))
                {
                    if (e.WertText != null && e.WertText != String.Empty)
                        return (T)(object)e.WertText;
                    else
                        return (T)(object)e.WertString;
                }
            }
            return defaultValue;
        }

        public static Model.Einstellungen SetEinstellung<T>(string name, T value)
        {
            if (Global.IsInitialized)
            {
                Model.Einstellungen e = Global.ContextHeld.LoadEinstellungByName(name);
                if (e == null)
                {
                    e = Global.ContextHeld.New<Model.Einstellungen>();
                    e.Name = name;
                }
                if (typeof(T) == typeof(Boolean) || typeof(T) == typeof(bool))
                    e.WertBool = (bool)(object)value;
                else if (typeof(T) == typeof(int) || typeof(T) == typeof(Int32) || typeof(T) == typeof(Int64))
                    e.WertInt = (int)(object)value;
                else if (typeof(T) == typeof(string) || typeof(T) == typeof(String))
                {
                    if (((string)(object)value).Length > 300)
                    {
                        e.WertString = String.Empty;
                        e.WertText = (string)(object)value;
                    }
                    else
                    {
                        e.WertText = String.Empty;
                        e.WertString = (string)(object)value;
                    }
                }
                return e;
            }
            return null;
        }

        public static bool FrageNeueKampfrundeAbstellen
        {
            get
            {
                return GetOrCreateEinstellung<bool>("FrageNeueKampfrundeAbstellen", false);
            }
            set
            {
                SetEinstellung<bool>("FrageNeueKampfrundeAbstellen", value);
            }
        }

        public static bool JingleAbstellen
        {
            get
            {
                return GetOrCreateEinstellung<bool>("JingleAbstellen", false);
            }
            set
            {
                SetEinstellung<bool>("JingleAbstellen", value);
            }
        }

        public static int SelectedTab
        {
            get
            {
                return GetOrCreateEinstellung<int>("SelectedTab", 0);
            }
            set
            {
                SetEinstellung<int>("SelectedTab", 0);
            }
        }

        public static string StartTabs
        {
            get
            {
                return GetOrCreateEinstellung<string>("StartTabs", String.Empty);
            }
            set
            {
                SetEinstellung<string>("StartTabs", String.Empty);
            }
        }

        public static string KalenderExpandedSections
        {
            get
            {
                return GetOrCreateEinstellung<string>("KalenderExpandedSections", "111111");
            }
            set
            {
                SetEinstellung<string>("KalenderExpandedSections", "111111");
            }
        }

        public static string UmrechnerExpandedSections
        {
            get
            {
                return GetOrCreateEinstellung<string>("UmrechnerExpandedSections", "111111");
            }
            set
            {
                SetEinstellung<string>("UmrechnerExpandedSections", "111111");
            }
        }

        public static string Standort
        {
            get
            {
                return GetOrCreateEinstellung<string>("Standort", "Gareth#29.79180235685203#3.735098459067687");
            }
            set
            {
                SetEinstellung<string>("Standort", "Gareth#29.79180235685203#3.735098459067687");
            }
        }
    }
}
