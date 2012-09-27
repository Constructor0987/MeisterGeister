using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeisterGeister.Model.Extensions;

using System.Reflection;
using ImpromptuInterface;

namespace MeisterGeister.ViewModel.Helden
{
    public class EigenschaftenViewModel : Base.ViewModelBase, Logic.IChangeListener
    {
        #region //---- COMMANDS ----

        private Base.CommandBase onMaxEnergie;
        public Base.CommandBase OnMaxEnergie
        {
            get { return onMaxEnergie; }
        }

        #endregion

        #region //---- EIGENSCHAFTEN & FELDER ----

        // Selection
        public Model.Held SelectedHeld
        {
            get { return Global.SelectedHeld; }
            set
            {
                Global.SelectedHeld = value;
                OnChanged("SelectedHeld");
            }
        }

        public System.Windows.Visibility HinweisMagieKarmaVisibility
        {
            get 
            {
                if (SelectedHeld == null || !(SelectedHeld.Magiebegabt || SelectedHeld.Geweiht))
                    return System.Windows.Visibility.Visible;
                return System.Windows.Visibility.Collapsed;
            }
        }

        private int Apply(Kampf.Logic.Modifikatoren.IModifikator mod, Type typ, int wert)
        {
            if (!typeof(Kampf.Logic.Modifikatoren.IModifikator).IsAssignableFrom(typ) || !typ.IsInstanceOfType(mod) )
                return wert;
            foreach (MethodInfo mi in typ.GetMethods())
                if (mi.Name.StartsWith("Apply"))
                    return Impromptu.InvokeMember(mod, mi.Name, wert);
            return wert;
        }

        public List<dynamic> ModifikatorenListe(Type modTyp, int startWert, ICollection<Kampf.Logic.Modifikatoren.IModifikator> mods)
        {
            if (!typeof(Kampf.Logic.Modifikatoren.IModifikator).IsAssignableFrom(modTyp))
                throw new ArgumentException("modTyp muss von IModifikator erben.");
            List<dynamic> li = new List<dynamic>();
            foreach (var item in mods.Where(m => modTyp.IsInstanceOfType(m)).OrderBy(m => m.Erstellt).ToList())
            {
                startWert =  Apply(item, modTyp, startWert);
                li.Add(new
                {
                    Mod = item,
                    Wert = startWert
                });
            }
            return li;
        }

        public List<dynamic> ModifikatorenListeMU
        {
            get
            {
                return ModifikatorenListe(typeof(Kampf.Logic.Modifikatoren.IModMU), SelectedHeld.MU ?? 8, SelectedHeld.Modifikatoren);
            }
        }
        public List<dynamic> ModifikatorenListeKL
        {
            get 
            {
                List<dynamic> li = new List<dynamic>();
                int eigenschaft = SelectedHeld.KL ?? 8;
                foreach (var item in SelectedHeld.Modifikatoren.Where(m => m is Kampf.Logic.Modifikatoren.IModKL).OrderBy(m => m.Erstellt).ToList())
                {
                    eigenschaft = ((Kampf.Logic.Modifikatoren.IModKL)item).ApplyKLMod(eigenschaft);
                    li.Add(new { 
                        Mod = item, 
                        Wert = eigenschaft
                    });
                }

                return li;
            }
        }
        public List<dynamic> ModifikatorenListeCH
        {
            get
            {
                List<dynamic> li = new List<dynamic>();
                int eigenschaft = SelectedHeld.CH ?? 8;
                foreach (var item in SelectedHeld.Modifikatoren.Where(m => m is Kampf.Logic.Modifikatoren.IModCH).OrderBy(m => m.Erstellt).ToList())
                {
                    eigenschaft = ((Kampf.Logic.Modifikatoren.IModCH)item).ApplyCHMod(eigenschaft);
                    li.Add(new
                    {
                        Mod = item,
                        Wert = eigenschaft
                    });
                }

                return li;
            }
        }
        public List<dynamic> ModifikatorenListeIN
        {
            get
            {
                List<dynamic> li = new List<dynamic>();
                int eigenschaft = SelectedHeld.IN ?? 8;
                foreach (var item in SelectedHeld.Modifikatoren.Where(m => m is Kampf.Logic.Modifikatoren.IModIN).OrderBy(m => m.Erstellt).ToList())
                {
                    eigenschaft = ((Kampf.Logic.Modifikatoren.IModIN)item).ApplyINMod(eigenschaft);
                    li.Add(new
                    {
                        Mod = item,
                        Wert = eigenschaft
                    });
                }

                return li;
            }
        }
        public List<dynamic> ModifikatorenListeFF
        {
            get
            {
                List<dynamic> li = new List<dynamic>();
                int eigenschaft = SelectedHeld.FF ?? 8;
                foreach (var item in SelectedHeld.Modifikatoren.Where(m => m is Kampf.Logic.Modifikatoren.IModFF).OrderBy(m => m.Erstellt).ToList())
                {
                    eigenschaft = ((Kampf.Logic.Modifikatoren.IModFF)item).ApplyFFMod(eigenschaft);
                    li.Add(new
                    {
                        Mod = item,
                        Wert = eigenschaft
                    });
                }

                return li;
            }
        }
        public List<dynamic> ModifikatorenListeGE
        {
            get
            {
                List<dynamic> li = new List<dynamic>();
                int eigenschaft = SelectedHeld.GE ?? 8;
                foreach (var item in SelectedHeld.Modifikatoren.Where(m => m is Kampf.Logic.Modifikatoren.IModGE).OrderBy(m => m.Erstellt).ToList())
                {
                    eigenschaft = ((Kampf.Logic.Modifikatoren.IModGE)item).ApplyGEMod(eigenschaft);
                    li.Add(new
                    {
                        Mod = item,
                        Wert = eigenschaft
                    });
                }

                return li;
            }
        }
        public List<dynamic> ModifikatorenListeKO
        {
            get
            {
                List<dynamic> li = new List<dynamic>();
                int eigenschaft = SelectedHeld.KO ?? 8;
                foreach (var item in SelectedHeld.Modifikatoren.Where(m => m is Kampf.Logic.Modifikatoren.IModKO).OrderBy(m => m.Erstellt).ToList())
                {
                    eigenschaft = ((Kampf.Logic.Modifikatoren.IModKO)item).ApplyKOMod(eigenschaft);
                    li.Add(new
                    {
                        Mod = item,
                        Wert = eigenschaft
                    });
                }

                return li;
            }
        }
        public List<dynamic> ModifikatorenListeKK
        {
            get
            {
                List<dynamic> li = new List<dynamic>();
                int eigenschaft = SelectedHeld.KK ?? 8;
                foreach (var item in SelectedHeld.Modifikatoren.Where(m => m is Kampf.Logic.Modifikatoren.IModKK).OrderBy(m => m.Erstellt).ToList())
                {
                    eigenschaft = ((Kampf.Logic.Modifikatoren.IModKK)item).ApplyKKMod(eigenschaft);
                    li.Add(new
                    {
                        Mod = item,
                        Wert = eigenschaft
                    });
                }

                return li;
            }
        }

        #endregion

        #region //---- KONSTRUKTOR ----

        public EigenschaftenViewModel()
        {
            // EventHandler für SelectedHeld registrieren
            Global.HeldSelectionChanged += (s, ev) => { SelectedHeldChanged(); };

            onMaxEnergie = new Base.CommandBase(SetEnergieMax, null);
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Init()
        {
            
        }

        public void NotifyRefresh()
        {
            OnChanged("SelectedHeld");
            OnChanged("HinweisMagieKarmaVisibility");
        }

        private void SetEnergieMax(object energieTyp)
        {
            if (energieTyp is MeisterGeister.View.General.EnergieEnum)
            {
                switch ((MeisterGeister.View.General.EnergieEnum)energieTyp)
                {
                    case MeisterGeister.View.General.EnergieEnum.Lebensenergie:
                        SelectedHeld.LebensenergieAktuell = SelectedHeld.LebensenergieMax;
                        break;
                    case MeisterGeister.View.General.EnergieEnum.Ausdauer:
                        SelectedHeld.AusdauerAktuell = SelectedHeld.AusdauerMax;
                        break;
                    case MeisterGeister.View.General.EnergieEnum.Astralenergie:
                        SelectedHeld.AstralenergieAktuell = SelectedHeld.AstralenergieMax;
                        break;
                    case MeisterGeister.View.General.EnergieEnum.Karmaenergie:
                        SelectedHeld.KarmaenergieAktuell = SelectedHeld.KarmaenergieMax;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region //---- EVENTS ----

        private void SelectedHeldChanged()
        {
            if (!ListenToChangeEvents)
                return;
            NotifyRefresh();
        }

        #endregion

        private bool listenToChangeEvents = true;

        public bool ListenToChangeEvents
        {
            get { return listenToChangeEvents; }
            set { listenToChangeEvents = value; SelectedHeldChanged(); }
        }
        
    }
    
}
