using System;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public struct ZeitImKampf : IComparable, IComparable<ZeitImKampf>
    {
        public ZeitImKampf(int kampfrunde, decimal initiativPhase)
        {
            Kampfrunde = kampfrunde;
            InitiativPhase = initiativPhase;
        }

        public int Kampfrunde { get; set; }
        public decimal InitiativPhase { get; set; }


        public static bool operator <(ZeitImKampf left, ZeitImKampf right)
        {
            return left.Kampfrunde < right.Kampfrunde || (left.Kampfrunde == right.Kampfrunde && left.InitiativPhase > right.InitiativPhase);
        }
        public static bool operator >(ZeitImKampf left, ZeitImKampf right)
        {
            return left.Kampfrunde > right.Kampfrunde || (left.Kampfrunde == right.Kampfrunde && left.InitiativPhase < right.InitiativPhase);
        }
        public static bool operator >=(ZeitImKampf left, ZeitImKampf right)
        {
            return !(right < left);
        }
        public static bool operator <=(ZeitImKampf left, ZeitImKampf right)
        {
            return !(right > left);
        }
        public static bool operator ==(ZeitImKampf left, ZeitImKampf right)
        {
            return left.Kampfrunde == right.Kampfrunde && left.InitiativPhase == right.InitiativPhase;
        }
        public static bool operator !=(ZeitImKampf left, ZeitImKampf right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is ZeitImKampf)
                return this == (ZeitImKampf)obj;
            else return false;
        }

        public override int GetHashCode()
        {
            return Kampfrunde.GetHashCode() ^ InitiativPhase.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            return CompareTo((ZeitImKampf)obj);
        }

        public int CompareTo(ZeitImKampf other)
        {
            int diffKR = Kampfrunde - other.Kampfrunde;
            if (diffKR == 0)
            {
                if (InitiativPhase == other.InitiativPhase)
                    return 0;
                else
                    return Math.Sign(InitiativPhase - other.InitiativPhase);
            }
            else return diffKR;
        }

        public override string ToString()
        {
            return String.Format("Kampfrunde: {0} Iniphase: {1}", Kampfrunde, InitiativPhase);
        }
    }
}
