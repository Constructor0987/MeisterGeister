using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeisterGeister.ViewModel.Kampf.Logic.Modifikatoren;
using MeisterGeister.Logic.Extensions;

namespace MeisterGeister.ViewModel.Modifikatoren
{
    public class CustomModifikatorViewModel : Base.ViewModelBase
    {
        /// <summary>
        /// Factory zum Erstellen des Modifikators
        /// </summary>
        private CustomModifikatorFactory factory = new CustomModifikatorFactory();

        #region Basisdaten zum Nachschlagen
        /// <summary>
        /// Liste von möglichen Modifikatoren (gruppiert nach Typ)
        /// </summary>
        public IList<ModifikatorTyp> ModifikatorTypen
        {
            get { return ModifikatorTyp.Liste; }
        }
        ModifikatorTyp selectedModifikatorTyp;
        public ModifikatorTyp SelectedModifikatorTyp
        {
            get { return selectedModifikatorTyp; }
            set { Set(ref selectedModifikatorTyp, value); }
        }
        
        private static List<string> operatoren = new List<string>() { "+", "-", "*", "/" };
        /// <summary>
        /// Liste von Rechenfunktionen
        /// </summary>
        public static List<string> Operatoren
        {
            get { return operatoren; }
        }

        // Liste von Zaubernamen
        // Liste von Talentnamen
        #endregion

        #region Eigenschaften des Modifikators aus der Factory
        //Liste von eingebrachten Modifikatoren
        ExtendedObservableCollection<ModifikatorTypViewModel> modifikatorTypVMListe = new ExtendedObservableCollection<ModifikatorTypViewModel>();
        public ExtendedObservableCollection<ModifikatorTypViewModel> ModifikatorTypVMListe
        {
            get { return modifikatorTypVMListe; }
            set { Set(ref modifikatorTypVMListe, value); }
        }

        /// <summary>
        /// Fehler aufrund von fehlenden oder falschen Eingaben
        /// </summary>
        IReadOnlyDictionary<object, string> Errors
        {
            get { return factory.Errors; }
        }

        /// <summary>
        /// Die Auswirkungen als Text
        /// </summary>
        string Auswirkungen
        {
            get { return factory.Auswirkungen; }
        }

        /// <summary>
        /// Name des zu erstellenden Modifikators
        /// </summary>
        string Name
        {
            get
            {
                return factory.Name;
            }
            set
            {
                factory.Name = value;
                OnChanged("Name");
            }
        }

        /// <summary>
        /// Literaturangabe des zu erstellenden Modifikators (optional)
        /// </summary>
        string Literatur
        {
            get
            {
                return factory.Literatur;
            }
            set
            {
                factory.Literatur = value;
                OnChanged("Literatur");
            }
        }

        //aus dem childVMs bestimmen, ob die felder benötigt werden.
        //0-n Zaubername
        SortedSet<string> zaubername;
        //0-n Talentname
        SortedSet<string> talentname;
        #endregion

        #region Je ModifikatorTyp
        public class ModifikatorTypViewModel : Base.ViewModelBase
        {
            private CustomModifikatorFactory factory;
            /// <summary>
            /// Sobald die Werte in diesem Dictionary festgelegt werden, gelten sie auch automatisch für das Ergebnis.
            /// </summary>
            IDictionary<string, object> factoryProperties;
            
            public ModifikatorTypViewModel(ModifikatorTyp typ, CustomModifikatorFactory factory)
            { 
                this.factory = factory;
                this.factoryProperties = factory[typ.Typ];
                Typ = typ;
                ApplyExpression();
            }

            bool? needsMethod = null;
            /// <summary>
            /// Gibt an, ob eine Methode gebraucht wird.
            /// Wird keine gebraucht, muss man auch keine Eingabefelder dafür anzeigen.
            /// </summary>
            public bool NeedsMethod
            {
                get
                {
                    if (needsMethod == null)
                        needsMethod = (GetMethodName() != null);
                    return needsMethod.Value;
                }
            }

            string methodName = "Nothing";
            private string GetMethodName()
            {
                if (methodName != "Nothing")
                    return methodName;
                foreach (var key in factoryProperties.Keys)
                {
                    if (key.StartsWith("Apply") && key.EndsWith("Mod"))
                        return methodName = key;
                }
                return methodName = null;
            }

            bool? needsZaubername = null;
            /// <summary>
            /// Gibt an, ob ein Zaubername gebraucht wird.
            /// Wird keiner gebraucht, muss man auch keine Eingabefelder dafür anzeigen.
            /// </summary>
            public bool NeedsZaubername
            {
                get
                {
                    if (needsZaubername == null)
                        needsZaubername = factoryProperties.ContainsKey("Zaubername");
                    return needsZaubername.Value;
                }
            }

            ISet<string> zaubername = null;
            public ISet<string> Zaubername
            {
                get { return zaubername; }
                set { 
                    if(Set(ref zaubername, value))
                    {
                        factoryProperties["Zaubername"] = value;
                        OnChanged("Auswirkungen");
                    }
                }
            }

            bool? needsTalentname = null;
            /// <summary>
            /// Gibt an, ob ein Talentname gebraucht wird.
            /// Wird keiner gebraucht, muss man auch keine Eingabefelder dafür anzeigen.
            /// </summary>
            public bool NeedsTalentname
            {
                get
                {
                    if (needsTalentname == null)
                        needsTalentname = factoryProperties.ContainsKey("Talentname");
                    return needsTalentname.Value;
                }
            }

            ISet<string> talentname = null;
            public ISet<string> Talentname
            {
                get { return talentname; }
                set
                {
                    if (Set(ref talentname, value))
                    {
                        factoryProperties["Talentname"] = value;
                        OnChanged("Auswirkungen");
                    }
                }
            }


            ModifikatorTyp typ;
            public ModifikatorTyp Typ
            {
                get { return typ; }
                set { Set(ref typ, value); }
            }

            /// <summary>
            /// Liste der Operatoren.
            /// Wrapper für einfachere Referenzierung.
            /// </summary>
            public IList<string> Operatoren
            {
                get { return CustomModifikatorViewModel.Operatoren; }
            }

            string selectedOperator = "+";
            /// <summary>
            /// Gewählter Operator
            /// </summary>
            public string SelectedOperator
            {
                get { return selectedOperator; }
                set { 
                    if(Set(ref selectedOperator, value))
                        ApplyExpression();
                }
            }

            int wert = 0;
            /// <summary>
            /// Operand zur Rechenfunktion
            /// </summary>
            public int Wert
            {
                get { return wert; }
                set { 
                    if(Set(ref wert, value))
                        ApplyExpression();
                }
            }
            
            /// <summary>
            /// Erstellt mit der Factory eine Expression und weist diese zu.
            /// </summary>
            void ApplyExpression()
            {
                if(!NeedsMethod)
                    return;
                factory.SetModifikator(GetMethodName(), SelectedOperator, Wert);
                OnChanged("Auswirkungen");
            }
            // Beim EndetMitZeitpunkt-Modifikator muss später noch ein DSA-Datum hinzugefügt werden. Das setzt aber einen überarbeiteten Kalender voraus.
        }
        #endregion

        #region Aktionen
        //Aktionen
        // Modifikator hinzufügen
        void AddModifikatorTyp(ModifikatorTyp typ)
        {
            if (factory.AddModifikator(typ.Typ))
            {
                ModifikatorTypViewModel vm = new ModifikatorTypViewModel(typ, factory);
                ModifikatorTypVMListe.Add(vm);
            }
        }

        // Modifikator löschen
        // Könnte man auch leicht mit dem ModifikatorTypViewModel als Parameter programmieren.
        void RemoveModifikatorTyp(ModifikatorTyp typ)
        {
            if (factory.RemoveModifikator(typ.Typ))
            {
                ModifikatorTypVMListe.RemoveAll(vm => vm.Typ == typ, false);
            }
        }

        // Talentname hinzufügen
        // Talentname löschen
        // Zaubername hinzufügen
        // Zaubername löschen
        // Modifikator löschen
        // Anwenden (ist danach nicht mehr editierbar)
        #endregion
    }
}
