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
                return ModifikatorenListe(typeof(Kampf.Logic.Modifikatoren.IModKL), SelectedHeld.KL ?? 8, SelectedHeld.Modifikatoren);
            }
        }
        public List<dynamic> ModifikatorenListeCH
        {
            get
            {
                return ModifikatorenListe(typeof(Kampf.Logic.Modifikatoren.IModCH), SelectedHeld.CH ?? 8, SelectedHeld.Modifikatoren);
            }
        }
        public List<dynamic> ModifikatorenListeIN
        {
            get
            {
                return ModifikatorenListe(typeof(Kampf.Logic.Modifikatoren.IModIN), SelectedHeld.IN ?? 8, SelectedHeld.Modifikatoren);
            }
        }
        public List<dynamic> ModifikatorenListeFF
        {
            get
            {
                return ModifikatorenListe(typeof(Kampf.Logic.Modifikatoren.IModFF), SelectedHeld.FF ?? 8, SelectedHeld.Modifikatoren);
            }
        }
        public List<dynamic> ModifikatorenListeGE
        {
            get
            {
                return ModifikatorenListe(typeof(Kampf.Logic.Modifikatoren.IModGE), SelectedHeld.GE ?? 8, SelectedHeld.Modifikatoren);
            }
        }
        public List<dynamic> ModifikatorenListeKO
        {
            get
            {
                return ModifikatorenListe(typeof(Kampf.Logic.Modifikatoren.IModKO), SelectedHeld.KO ?? 8, SelectedHeld.Modifikatoren);
            }
        }
        public List<dynamic> ModifikatorenListeKK
        {
            get
            {
                return ModifikatorenListe(typeof(Kampf.Logic.Modifikatoren.IModKK), SelectedHeld.KK ?? 8, SelectedHeld.Modifikatoren);
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
