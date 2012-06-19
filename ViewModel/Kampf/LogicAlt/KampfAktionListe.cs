using System.Collections.Generic;

namespace MeisterGeister.ViewModel.Kampf.LogicAlt
{
    public class KampfAktionListe : List<KampfAktion>
    {
        public new void Sort()
        {
            Sort(Compare);
        }

        public void RemoveAlteAktionen(int kr)
        {
            _kr = kr;
            RemoveAll(KleinerAlsKampfrunde);
        }

        public static int Compare(KampfAktion x, KampfAktion y)
        {
            // prüfen auf null-Übergabe
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1;
            // Vergleich
            if (x.InKampfrunde < y.InKampfrunde)
                return -1;
            if (x.InKampfrunde > y.InKampfrunde)
                return 1;
            if (x.InPhase > y.InPhase)
                return -1;
            if (x.InPhase < y.InPhase)
                return 1;
            return 0;
        }

        private static int _kr;

        public static bool KleinerAlsKampfrunde(KampfAktion a)
        {
            return a.InKampfrunde < _kr;
        }
    }
}