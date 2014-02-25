using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Documents;

namespace MeisterGeister.ViewModel.Bodenplan.Logic
{
    public static class Ressources
    {
        public static String[] GetPictureUrls()
        {
            return GetPictureUrls("");
        }

        public static String[] GetPictureUrls(String customDirectoryPath)
        {
            String[] filenames = new string[0];
            try
            {
               filenames = System.IO.Directory.GetFiles("Daten\\Bodenplan");
            }
            catch (Exception e)
            {
                Console.WriteLine("[STATIC CLASS RESSOURCES]:"+e.Message);
            }
            
            return filenames;
        }

        public static string Decoder(string value)
        {
            Encoding enc = new UTF8Encoding();
            byte[] bytes = enc.GetBytes(value);
            return enc.GetString(bytes);
        }

        public static string GetFullApplicationPath()
        {
            return Environment.CurrentDirectory;
        }

        public static string GetFullApplicationPathForPictures()
        {
            return GetFullApplicationPath() + "\\Daten\\Bodenplan\\";
        }

        public static List<int> GetZLevelsFromInputString(String input)
        {
            try
            {
                //e.g. 1-3,5,8,3-12
                List<int> l = new List<int>();
                string[] comma = input.Trim().Replace(" ", "").Split(',');
                foreach (var s in comma)
                {
                    string[] temp = s.Split('-');
                    l.Add(Convert.ToInt32(temp[0]));
                    if (temp.Count() > 1) l.Add(Convert.ToInt32(temp[1]));
                }
                return l;
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR].[GetZLevelsFromInputString] "+e.Message);
                return new List<int>(10);
            }
        }

        public static List<string> GetPossibleZLevels(ObservableCollection<BattlegroundBaseObject> bol)
        {
            List<string> l = new List<string>();
            foreach (var bo in bol)
            {
                if(!l.Contains(bo.ZLevel.ToString())) l.Add(bo.ZLevel.ToString());
            }

            l.Sort(CompareByStringNumber);

            return l;
        }

        public static void SetVisibilityDependetOnZLevelSelection(ref ObservableCollection<BattlegroundBaseObject> bol, String visibleZLevels, bool ignorZLevels)
        {
            
            if (visibleZLevels.Length == 0 || ignorZLevels)
            {
                SetVisibilityForAll(ref bol,ignorZLevels);
                return;
            }

            String[] vZL = visibleZLevels.Split(',');

            foreach (var bo in bol)
            {
                bo.IsVisible = false;
                for (int i = 0; i < vZL.Length; i++)
                {
                    if (bo.ZLevel == Convert.ToInt32(vZL[i])) bo.IsVisible = true;
                    if (bo.ZLevel == Convert.ToInt32(vZL[i])) Console.WriteLine("VISIBLE: "+bo.ToString() + "  "+bo.ZLevel);
                }    
            }
        }

        public static void SetVisibilityForAll(ref ObservableCollection<BattlegroundBaseObject> bol, Boolean visi)
        {
            if (bol == null) return;
            foreach (var bo in bol)
            {
                bo.IsVisible = visi;
            }
        }

        private static int CompareByStringNumber(string x, string y)
        {
            if (x == null)
            {
                if (y == null)
                { 
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    return Convert.ToInt32(x).CompareTo(Convert.ToInt32(y));
                }
            }
        }
    }
}
