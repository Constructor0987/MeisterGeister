using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                {
                    if (e.WertBool != null)
                        return (T)(object)e.WertBool;
                    return defaultValue;
                }
                else if (typeof(T) == typeof(int) || typeof(T) == typeof(Int32) || typeof(T) == typeof(Int64))
                {
                    if (e.WertInt != null)
                        return (T)(object)e.WertInt;
                    return defaultValue;
                }
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
                bool isnew = false;
                if (e == null)
                {
                    e = Global.ContextHeld.New<Model.Einstellungen>();
                    e.Name = name;
                    isnew = true;
                }
                if (typeof(T) == typeof(Boolean) || typeof(T) == typeof(bool))
                    e.WertBool = (bool)(object)value;
                else if (typeof(T) == typeof(int) || typeof(T) == typeof(Int32) || typeof(T) == typeof(Int64))
                    e.WertInt = (int)(object)value;
                else if (typeof(T) == typeof(string) || typeof(T) == typeof(String))
                {
                    if (value == null || ((string)(object)value).Length > 300)
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
                if(isnew)
                    Global.ContextHeld.Insert<Model.Einstellungen>(e);
                else
                    Global.ContextHeld.Update<Model.Einstellungen>(e);
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

        public static bool WuerfelSoundAbspielen
        {
            get
            {
                return GetOrCreateEinstellung<bool>("WuerfelSoundAbspielen", false);
            }
            set
            {
                SetEinstellung<bool>("WuerfelSoundAbspielen", value);
                if (WuerfelSoundAbspielenChanged != null)
                    WuerfelSoundAbspielenChanged(null, new EventArgs());
            }
        }

        public static EventHandler WuerfelSoundAbspielenChanged;

        public static int SelectedTab
        {
            get
            {
                return GetOrCreateEinstellung<int>("SelectedTab", 0);
            }
            set
            {
                SetEinstellung<int>("SelectedTab", value);
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
                SetEinstellung<string>("StartTabs", value);
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
                SetEinstellung<string>("KalenderExpandedSections", value);
            }
        }

        public static string ProbenAnzeigeModus
        {
            get
            {
                return GetOrCreateEinstellung<string>("ProbenAnzeigeModus", "Zeile");
            }
            set
            {
                SetEinstellung<string>("ProbenAnzeigeModus", value);
            }
        }

        public static string DatumAktuell
        {
            get
            {
                return GetOrCreateEinstellung<string>("DatumAktuell", "1|0|993|0");
            }
            set
            {
                SetEinstellung<string>("DatumAktuell", value);
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
                SetEinstellung<string>("UmrechnerExpandedSections", value);
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
                SetEinstellung<string>("Standort", value);
            }
        }

        public static string SelectedHeld
        {
            get
            {
                return GetOrCreateEinstellung<string>("SelectedHeld", null);
            }
            set
            {
                SetEinstellung<string>("SelectedHeld", value);
            }
        }

        public static int HeldenSelectedTab
        {
            get
            {
                return GetOrCreateEinstellung<int>("SelectedHeldenTab", 0);
            }
            set
            {
                SetEinstellung<int>("SelectedHeldenTab", value);
            }
        }

        public static string ProbenFavoriten
        {
            get
            {
                return GetOrCreateEinstellung<string>("ProbenFavoriten", null);
            }
            set
            {
                SetEinstellung<string>("ProbenFavoriten", value);
            }
        }
    }
}
