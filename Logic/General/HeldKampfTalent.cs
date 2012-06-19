using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Daten;

namespace MeisterGeister.Logic.General
{
    public class HeldKampfTalent
    {
        public DatabaseDSADataSet.Held_TalentRow HeldTalentRow { get; set; }

        public Held Held { get; set; }

        public string Talentname
        {
            get { return HeldTalentRow.Talentname; }
            set { HeldTalentRow.Talentname = value; }
        }

        public int TaW
        {
            get { return HeldTalentRow.TaW; }
            set { HeldTalentRow.TaW = value; }
        }

        public int ZuteilungAT
        {
            get
            {
                if (NurAttacke || HeldTalentRow == null || HeldTalentRow.IsZuteilungATNull())
                    return TaW;
                else
                    return HeldTalentRow.ZuteilungAT;
            }
            set
            {
                if (value >= MinZuteilung)
                    HeldTalentRow.ZuteilungAT = value;
                else
                    HeldTalentRow.ZuteilungAT = MinZuteilung;
            }
        }

        public int ZuteilungPA
        {
            get
            {
                if (NurAttacke || HeldTalentRow == null || HeldTalentRow.IsZuteilungPANull())
                    return 0;
                else
                    return HeldTalentRow.ZuteilungPA;
            }
            set
            {
                if (value >= MinZuteilung)
                    HeldTalentRow.ZuteilungPA = value;
                else
                    HeldTalentRow.ZuteilungPA = MinZuteilung;
            }
        }

        public int MinZuteilung
        {
            get
            {
                if (TaW <= 0)
                    return HeldTalentRow.TaW;
                else
                    return 0;
            }
        }

        public string Untergruppe
        {
            get
            {
                return HeldTalentRow.TalentRow.Untergruppe;
            }
        }

        public bool NurAttacke
        {
            get { return Untergruppe == "Fernkampf" || Untergruppe == "Bewaffnete AT-Technik"; }
        }

        public int AttackeWert
        {
            get
            {
                if (Untergruppe == "Fernkampf")
                    return Held.FernkampfBasis + ZuteilungAT;
                else
                    return Held.AttackeBasis + ZuteilungAT;
            }
        }

        public int ParadeWert
        {
            get
            {
                if (NurAttacke)
                    return 0;
                else
                    return Held.ParadeBasis + ZuteilungPA;
            }
        }

        public string ZuteilungsHinweis
        {
            get
            {
                string hinweis = string.Empty;

                int zuteilDiff = TaW - ZuteilungAT - ZuteilungPA;

                if (zuteilDiff > 0)
                    hinweis = string.Format("Noch {0} TaP zuteilen!", zuteilDiff);
                else if (zuteilDiff < 0)
                    hinweis = string.Format("{0} TaP zu viel zugeteilt!", zuteilDiff * -1);

                int zuteilDiff2 = ZuteilungPA - ZuteilungAT;
                if (!NurAttacke && (zuteilDiff2 > 5 || zuteilDiff2 < -5))
                {
                    hinweis += string.IsNullOrEmpty(hinweis) ? string.Empty : Environment.NewLine;
                    hinweis += "Max. 5 Punkte AT/PA-Unterschied!";
                }

                return hinweis;
            }
        }
    }
}
