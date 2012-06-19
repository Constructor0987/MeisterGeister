using System.Collections.Generic;

namespace MeisterGeister.ViewModel.Kampf.LogicAlt
{
    public class KämpferList : List<IKämpfer>
    {
        public new void Sort()
        {
            Sort(Compare);
            Reverse();
        }

        public new bool Contains(IKämpfer item)
        {
            foreach (IKämpfer kämpfer in this)
            {
                if (kämpfer.Name == item.Name)
                    return true;
            }
            return false;
        }

        public int ContainsName(IKämpfer item)
        {
            int max = 0, temp = 0;
            string tempStr = string.Empty;
            foreach (IKämpfer kämpfer in this)
            {
                string name = kämpfer.Name.Trim().TrimEnd(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }).Trim();
                if (name == item.Name)
                {
                    tempStr = kämpfer.Name.Trim().Replace(name, string.Empty);
                    if (tempStr == string.Empty)
                        tempStr = "1";
                    if (System.Int32.TryParse(tempStr, out temp))
                        max = System.Math.Max(max, temp);
                }
            }
            return max;
        }

        public static int Compare(IKämpfer x, IKämpfer y)
        {
            // prüfen auf null-Übergabe
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1;
            // Vergleich
            if (x.Initiative < y.Initiative)
                return -1;
            if (x.Initiative > y.Initiative)
                return 1;
            if (x.InitiativeBasis < y.InitiativeBasis)
                return -1;
            if (x.InitiativeBasis > y.InitiativeBasis)
                return 1;
            return y.Name.CompareTo(x.Name);
        }
    }
}