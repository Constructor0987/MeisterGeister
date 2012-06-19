namespace MeisterGeister.ViewModel.Kampf.LogicAlt
{
    /// <summary>
    /// Beschreibt eine Aktion in einem Kampf.
    /// </summary>
    public class KampfAktion
    {
        /// <summary>
        /// Beschreibt die Quelle der Aktion.
        /// </summary>
        public string Aktionsquelle { get; set; }

        public string Aktionsname { get; set; }

        public int InKampfrunde { get; set; }

        public int AktionenDauer { get; set; }

        public Kampf Kampf { get; set; }

        public Aktion Aktion { get; set; }

        /// <summary>
        /// Beschreibt in welcher Initiative-Phase die Aktion eintritt.
        /// </summary>
        public int InPhase
        {
            get
            {
                if (Aktion == Aktion.Aktion1)
                {
                    return Kämpfer != null ? Kämpfer.Initiative : 8;
                }
                if (Aktion == Aktion.Aktion2)
                {
                    return Kämpfer != null ? Kämpfer.Initiative - 8 : 0;
                }
                return 8;
            }
        }

        public int RestAktionenDauer
        {
            get
            {
                if (Aktuell)
                {
                    if (Aktion == Aktion.Aktion1)
                    {
                        return 1;
                    }
                    if (Aktion == Aktion.Aktion2)
                    {
                        return System.Math.Min(2, AktionenDauer);
                    }
                }
                int rest = (Kampf.Kampfrunde - InKampfrunde) * 2;
                if (Aktion == Aktion.Aktion1)
                {
                    rest--;
                }
                if (Aktion == Aktion.Aktion2)
                {
                    
                }
                return rest;
            }
        }

        public bool Aktuell
        {
            get
            {
                return InKampfrunde == Kampf.Kampfrunde;
            }
        }

        public bool Vorbei
        {
            get
            {
                return InKampfrunde < Kampf.Kampfrunde;
            }
        }

        public System.Windows.Visibility AktuellUi
        {
            get
            {
                if (Aktuell)
                {
                    return System.Windows.Visibility.Visible;
                }
                return System.Windows.Visibility.Hidden;
            }
        }

        public IKämpfer Kämpfer { get; set; }

        public string InfoText
        {
            get
            {
                return string.Format("KR: {0}, Phase {1}: {2}{3}", InKampfrunde, InPhase,
                    string.IsNullOrEmpty(Aktionsquelle) ? string.Empty : "[" + Aktionsquelle + "] ", Aktionsname);
            }
        }

        public AktionDelegate AktionAufruf { get; set; }

        public void AktionAusführen()
        {
            if (AktionAufruf != null)
            {
                // Ereignis-Delegaten ausführen
                AktionAufruf();
            }
        }
    }

    public delegate void AktionDelegate();
}