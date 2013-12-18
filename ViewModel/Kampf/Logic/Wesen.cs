using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

using KampfLogic = MeisterGeister.ViewModel.Kampf.Logic;
using Mod = MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using MeisterGeister.Logic.Extensions;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MeisterGeister.Logic.General;
using MeisterGeister.ViewModel.Helden.Logic;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    [DataContract(IsReference=true)]
    public class Wesen : MeisterGeister.ViewModel.Bodenplan.Logic.BattlegroundCreature
    {
        public Wesen() : base()
        {
            Modifikatoren.CollectionChanged += OnModifikatorenChanged;
            ModifikatorenChanged += DependsOnModifikator.OnModifikatorenChanged;
            if (this is INotifyPropertyChanged)
            {
                (this as INotifyPropertyChanged).PropertyChanged += OnPropertyChanged;
            }
        }

        #region Modifikatoren
        #region ModifikatorChanged Event
        private event NotifyCollectionChangedEventHandler ModifikatorenChanged;
        private void OnModifikatorenChanged(object o, NotifyCollectionChangedEventArgs args)
        {
            if (ModifikatorenChanged != null)
                ModifikatorenChanged(this, args);
        }
        #endregion

        public int GetModifikatorProben(Probe probe)
        {
            if (probe == null)
                return 0;
            int mod = 0;
            if (Modifikatoren != null && probe is Eigenschaft)
                Modifikatoren.Where(m => m is Mod.IModAlleEigenschaftsProben).Select(m => (Mod.IModAlleEigenschaftsProben)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => mod = m.ApplyAlleEigenschaftsProbenMod(mod));
            else if (Modifikatoren != null)
            {
                Modifikatoren.Where(m => m is Mod.IModAlleProben).Select(m => (Mod.IModAlleProben)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => mod = m.ApplyAlleProbenMod(mod));
                if (probe is Model.Zauber || probe is Model.Held_Zauber)
                    Modifikatoren.Where(m => m is Mod.IModZauberprobe).Select(m => (Mod.IModZauberprobe)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => mod = m.ApplyZauberprobeMod(mod));
                else if (probe is Model.Talent || probe is Model.Held_Talent || probe is MetaTalent)
                    Modifikatoren.Where(m => m is Mod.IModTalentprobe).Select(m => (Mod.IModTalentprobe)m).OrderBy(m => m.Erstellt).ToList().ForEach(m => mod = m.ApplyTalentprobeMod(mod));
            }
            return mod;
        }

        public List<dynamic> GetModifikatorenListe(Probe probe)
        {
            if (Modifikatoren == null || probe == null)
                return new List<dynamic>();
            if (probe is Eigenschaft)
                return ModifikatorenListe(typeof(Mod.IModAlleEigenschaftsProben), 0);
            else
            {
                List<dynamic> list = ModifikatorenListe(typeof(Mod.IModAlleProben), 0);
                if (probe is Model.Zauber || probe is Model.Held_Zauber)
                    list.AddRange(ModifikatorenListe(typeof(Mod.IModZauberprobe), list.Count() == 0 ? 0 : list.LastOrDefault().Wert));
                else if (probe is Model.Talent || probe is Model.Held_Talent || probe is MetaTalent)
                    list.AddRange(ModifikatorenListe(typeof(Mod.IModTalentprobe), list.Count() == 0 ? 0 : list.LastOrDefault().Wert));
                return list;
            }
        }

        private int Apply(Mod.IModifikator mod, Type typ, int? wert)
        {
            if (!typeof(Mod.IModifikator).IsAssignableFrom(typ) || !typ.IsInstanceOfType(mod))
                return wert ?? 0;
            foreach (System.Reflection.MethodInfo mi in typ.GetMethods())
                if (mi.Name.StartsWith("Apply"))
                    return ImpromptuInterface.Impromptu.InvokeMember(mod, mi.Name, wert);
            return wert ?? 0;
        }

        public List<dynamic> ModifikatorenListe(Type modTyp, int? startWert, ICollection<Mod.IModifikator> mods = null)
        {
            if (mods == null)
                mods = Modifikatoren;
            if (!typeof(Mod.IModifikator).IsAssignableFrom(modTyp))
                throw new ArgumentException("modTyp muss von IModifikator erben.");
            List<dynamic> li = new List<dynamic>();
            int? wertTmp = startWert;
            foreach (var item in mods.Where(m => modTyp.IsInstanceOfType(m)).OrderBy(m => m.Erstellt).ToList())
            {
                startWert = Apply(item, modTyp, startWert);
                li.Add(new
                {
                    Mod = item,
                    Wert = startWert,
                    IsWertGesenkt = startWert < wertTmp,
                    IsWertGesteigert = startWert > wertTmp
                });
                wertTmp = startWert;
            }
            return li;
        }

        Mod.ModifikatorenListe _modifikatoren = new Mod.ModifikatorenListe();
        public Mod.ModifikatorenListe Modifikatoren
        {
            get { return _modifikatoren; }
        }

        public int GetModifikatorCount<T>()
            where T : Mod.IModifikator, new()
        {
            return Modifikatoren.Where(m => m is T).Count();
        }

        /// <returns>Veränderung der Modifikatorenanzahl</returns>
        public int SetModifikatorCount(Type t, int targetCount)
        {
            var name = ImpromptuInterface.InvokeMemberName.Create;
            return ImpromptuInterface.Impromptu.InvokeMember(this, name("SetModifikatorCount", new[] { t }), targetCount);
        }

        /// <returns>Veränderung der Modifikatorenanzahl</returns>
        public int SetModifikatorCount<T>(int targetCount)
            where T : Mod.IModifikator, new()
        {
            IList<Mod.IModifikator> mods = Modifikatoren.Where(m => m is T).ToList();
            int change = 0;
            if (targetCount < mods.Count)
            {
                for (int i = 0; mods.Count > 0 && i < mods.Count - targetCount; i++)
                {
                    change--;
                    Modifikatoren.Remove(mods[i]);
                }
            }
            else if (targetCount > mods.Count)
            {
                for (int i = mods.Count; i < targetCount; i++)
                {
                    change++;
                    Modifikatoren.Add(new T());
                }
            }
            return change;
        }
        #endregion

        #region Energie Stati

        public string GetLebensenergieStatus()
        {
                IKämpfer k = this as IKämpfer;
                if (k==null || k.LebensenergieMax == 0)
                    return string.Empty;
                int leModCount = 0;
                if (k.LebensenergieAktuell < k.Konstitution * -1)
                    return "Tot";
                else if (k.LebensenergieAktuell <= 0)
                    return "Bewusstlos";
                else if (Modifikatoren.Where(m => m is Mod.LebensenergieKampfunfähigModifikator).Count() > 0)
                    return "Kampfunfähig";
                else if ((leModCount = Modifikatoren.Where(m => m is Mod.NiedrigeLebensenergieModifikator).Count()) > 0)
                    return "< 1/" + (leModCount + 1);
                return string.Empty;
        }

        public string LebensenergieStatusDetails
        {
            get
            {
                IKämpfer k = this as IKämpfer;
                if (k == null || k.LebensenergieMax == 0)
                    return string.Empty;
                string info = string.Empty; int leModCount = 0;
                if (k.LebensenergieAktuell < k.Konstitution * -1)
                    info = "Tot";
                else if (k.LebensenergieAktuell <= 0)
                {
                    info = "Bewusstlos";
                    info += Environment.NewLine + string.Format("tot in W6 x KO ({0}) Kampfrunden ({0} bis {1} KR = {2} bis {3} sec)",
                        k.Konstitution, 6 * k.Konstitution, 3 * k.Konstitution, 18 * k.Konstitution);
                }
                else if (Modifikatoren.Where(m => m is Mod.LebensenergieKampfunfähigModifikator).Count() > 0)
                {
                    info = "Kampfunfähig";
                    info += Environment.NewLine + "keine Aktionen möglich, außer mit GS 1 bewegen";
                }
                else if ((leModCount = Modifikatoren.Where(m => m is Mod.NiedrigeLebensenergieModifikator).Count()) > 0)
                {
                    info = "< 1/" + (leModCount + 1);
                    info += Environment.NewLine + string.Format("Optional: Eigenschaftsproben +{0}; Talent-/Zauberproben +{1}; GS -{0}", leModCount, leModCount * 3);
                }
                info += Environment.NewLine + "(\"Auswirkungen niedriger Lebensenergie\" siehe WdS 56f.)";
                return info;
            }
        }

        protected string GetAusdauerStatus()
        {
            int auModCount = 0;
            if ((this as IKämpfer).AusdauerMax == 0)
                return string.Empty;
            if (Modifikatoren.Where(m => m is Mod.AusdauerKampfunfähigModifikator).Count() > 0)
                return "Kampfunfähig";
            else if ((auModCount = Modifikatoren.Where(m => m is Mod.NiedrigeAusdauerModifikator).Count()) > 0)
                return "< 1/" + (auModCount + 2);
            return string.Empty;
        }

        public string AusdauerStatusDetails
        {
            get
            {
                string info = string.Empty; int auModCount = 0;
                if ((this as IKämpfer).AusdauerMax == 0)
                    return info;
                if (Modifikatoren.Where(m => m is Mod.AusdauerKampfunfähigModifikator).Count() > 0)
                {
                    info = "Kampfunfähig";
                    info += Environment.NewLine + "keine Aktionen möglich, außer Atem Holen";
                }
                else if ((auModCount = Modifikatoren.Where(m => m is Mod.NiedrigeAusdauerModifikator).Count()) > 0)
                {
                    info = "< 1/" + (auModCount + 2);
                    info += Environment.NewLine + string.Format("Optional: Eigenschaftsproben +{0}; Talent-/Zauberproben +{1}", auModCount, auModCount * 3);
                }
                info += Environment.NewLine + "(\"Auswirkungen niedriger Ausdauer\" siehe WdS 83)";
                return info;
            }
        }

        [DependsOnModifikator(typeof(Mod.AusdauerKampfunfähigModifikator))]
        [DependsOnModifikator(typeof(Mod.LebensenergieKampfunfähigModifikator))]
        public bool Kampfunfähig
        {
            get
            {
                return Modifikatoren.Where(m => m is Mod.AusdauerKampfunfähigModifikator).Count() > 0
                    || Modifikatoren.Where(m => m is Mod.LebensenergieKampfunfähigModifikator).Count() > 0;
            }
        }

        #endregion

        public bool IsGegner
        {
            get { return this is Model.Gegner; }
        }

        public bool IsHeld
        {
            get { return this is Model.Held; }
        }

        #region Events und resultierende Modifikatoren

        //Events auf setter, die im DB-Model stehen.
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "LebensenergieAktuell")
            {
                CheckLePModifikatoren();
            }
            else if (args.PropertyName == "AusdauerAktuell")
            {
                CheckAuModifikatoren();
            }
        }
        
        protected void CheckAuModifikatoren()
        {
            IKämpfer k = this as IKämpfer;
            if (k == null || k.AusdauerMax == 0)
                return;
            double percent = (double)k.AusdauerAktuell / (double)k.AusdauerMax;
            int targetModCount = 0;
            if (percent >= 1.0 / 3.0)
                targetModCount = 0;
            else if (percent >= 1.0 / 4.0)
                targetModCount = 1;
            else
                targetModCount = 2;
            int change = 0;

            // nur anwenden, wenn Regel-Option aktiv und Wesen AU hat
            if (MeisterGeister.Logic.Settings.Regeln.NiedrigeAU && k.AusdauerMax != 0)
                change = SetModifikatorCount<Mod.NiedrigeAusdauerModifikator>(targetModCount);

            if (targetModCount == 1 && change >= 1)
            {
                // TODO ??: + 1 Erschöpfung
            }

            if (k.AusdauerAktuell <= 0)
            {
                if (!(Modifikatoren.Where(m => m is Mod.AusdauerKampfunfähigModifikator).Count() > 0))
                {
                    Modifikatoren.Add(new Mod.AusdauerKampfunfähigModifikator());
                    // TODO ??: + 1W6 Erschöpfung
                }
            }
            else
            {
                Modifikatoren.RemoveAll(m => m is Mod.AusdauerKampfunfähigModifikator);
            }
        }

        protected void CheckLePModifikatoren()
        {
            IKämpfer k = this as IKämpfer;
            if (k == null || k.LebensenergieMax == 0)
                return;
            double percent = (double)k.LebensenergieAktuell / (double)k.LebensenergieMax;
            int targetModCount = 0;
            if (percent >= 0.5)
                targetModCount = 0;
            else if (percent >= 1.0 / 3.0)
                targetModCount = 1;
            else if (percent >= 1.0 / 4.0)
                targetModCount = 2;
            else
                targetModCount = 3;

            // nur anwenden, wenn Regel-Option aktiv und Wesen LE hat
            if (MeisterGeister.Logic.Settings.Regeln.NiedrigeLE && k.LebensenergieMax != 0)
                SetModifikatorCount<Mod.NiedrigeLebensenergieModifikator>(targetModCount);

            if (k is Model.Held && !(k as Model.Held).HatVorNachteil("Eisern") && !(k as Model.Held).HatVorNachteil("Zäher Hund")
                 && k.LebensenergieAktuell <= 5 || k.LebensenergieAktuell <= 0)
            {
                if (!(Modifikatoren.Where(m => m is Mod.LebensenergieKampfunfähigModifikator).Count() > 0))
                    Modifikatoren.Add(new Mod.LebensenergieKampfunfähigModifikator());
            }
            else
            {
                Modifikatoren.RemoveAll(m => m is Mod.LebensenergieKampfunfähigModifikator);
            }
        }
        #endregion
    }
}
