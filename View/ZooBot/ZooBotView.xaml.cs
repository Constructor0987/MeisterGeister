using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.Daten;
using MeisterGeister.View.Windows;
using MeisterGeister.Logic.Kalender;
using MeisterGeister.LogicAlt.General;

namespace MeisterGeister.View.ZooBot
{
    /// <summary>
    /// Interaktionslogik für ZooBotView.xaml
    /// </summary>
    public partial class ZooBotView : UserControl
    {
        public ZooBotView()
        {
            InitializeComponent();

            try
            {
                MeisterGeister.View.ZooBot.Hauptfenster pluInControl = new MeisterGeister.View.ZooBot.Hauptfenster();

                if (pluInControl != null)
                {
                    PlugInControl = pluInControl;
                    windowsFormsHost1.Child = PlugInControl;
                }
            }
            catch (Exception ex)
            {
                MsgWindow errWin = new MsgWindow("PlugIn Fehler", "Beim Laden eines PlugIns ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
            }

            List<string> fernkampfWaffen = new List<string>();
            fernkampfWaffen.Add("Bogen");
            fernkampfWaffen.Add("Armbrust");
            fernkampfWaffen.Add("Blasrohr");
            fernkampfWaffen.Add("Diskus");
            fernkampfWaffen.Add("Schleuder");
            fernkampfWaffen.Add("Wurfbeile");
            fernkampfWaffen.Add("Wurfmesser");
            fernkampfWaffen.Add("Wurfspeere");
            _comboBoxFernkampfwaffe.ItemsSource = fernkampfWaffen;
            _comboBoxFernkampfwaffe.SelectedIndex = 0;

            _comboBoxHeld.SelectedIndex = -1;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        public void Reload()
        {
            _comboBoxHeld.ItemsSource = Global.ContextHeld.HeldenGruppeListe;
        }

        private MeisterGeister.View.ZooBot.Hauptfenster PlugInControl = null;

        // TODO ??: Überführung ins neue Model
        public void SetHeldWerte(string talentname = "")
        {

            Model.Held h = null;
            if (_comboBoxHeld.SelectedItem != null)
                h = (Model.Held)_comboBoxHeld.SelectedItem;
            else
                h = new Model.Held();

            // Fernkampfwert
            string fernkampfwaffeString = _comboBoxFernkampfwaffe.SelectedValue.ToString();
            int fernkampfwaffe = h.Talentwert(fernkampfwaffeString);

            // Sonderfertigkeiten
            bool scharfschütze = h.HatSonderfertigkeit(string.Format("Scharfschütze ({0})", fernkampfwaffeString));
            bool meisterschütze = h.HatSonderfertigkeit(string.Format("Meisterschütze ({0})", fernkampfwaffeString));

            string suchmonat = (Datum.Aktuell.Monat == Monat.NamenloseTage ? "Namenlose Tage" : Datum.Aktuell.MonatString());
            int reiter = SelectTab(talentname);

            // Übergabe an Einsteins DSA-Tool
            if (PlugInControl != null)
            {
                try
                {
                    PlugInControl.MeisterGeisterInterface(
                            Math.Max(0, h.Mut), Math.Max(0, h.Intuition), Math.Max(0, h.Klugheit), 
                            Math.Max(0, h.Fingerfertigkeit), Math.Max(0, h.Gewandtheit), Math.Max(0, h.Körperkraft),
                            Math.Max(0, h.Talentwert("Ackerbau")), Math.Max(0, h.Talentwert("Fährtensuchen")),
                            Math.Max(0, h.Talentwert("Fallenstellen")), Math.Max(0, fernkampfwaffe),
                            Math.Max(0, h.Talentwert("Fischen/Angeln")), Math.Max(0, h.Talentwert("Pflanzenkunde")),
                            Math.Max(0, h.Talentwert("Schleichen")), Math.Max(0, h.Talentwert("Sich Verstecken")),
                            Math.Max(0, h.Talentwert("Sinnenschärfe")), Math.Max(0, h.Talentwert("Tierkunde")),
                            Math.Max(0, h.Talentwert("Wildnisleben")), scharfschütze, meisterschütze,
                            suchmonat, reiter);
                }
                catch (Exception ex)
                {
                    MsgWindow errWin = new MsgWindow("PlugIn Fehler", "Beim Laden einer PlugIn-Methode ist ein Fehler aufgetreten.", ex);
                    errWin.ShowDialog();
                }
            }
        }
        

        private string _lastTalent = string.Empty;

        private int SelectTab(string talent)
        {
            _lastTalent = talent;
            if (talent == string.Empty)
                return PlugInControl.AktiverReiter;
            if (talent == "Kräuter Suchen")
                return 0;
            else if (talent.StartsWith("Nahrung Sammeln"))
                return 1;
            else if (talent == "Fischen/Angeln" || talent == "Fallenstellen")
                return 3;
            else if (talent.StartsWith("Ansitzjagd") || talent.StartsWith("Pirschjagd"))
                return 2;

            return 0;
        }

        private void ComboBoxAuswahl_DropDownClosed(object sender, EventArgs e)
        {
            if (IsInitialized && _comboBoxHeld.SelectedItem != null)
            {
                SetHeldWerte(_lastTalent);
            }
        }

        private void ComboBoxAuswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized && _comboBoxHeld.SelectedItem != null)
            {
                SetHeldWerte(_lastTalent);
            }
        }

        ///// <summary>
        ///// Wird derzeit nicht benutz, da DLL statisch verlinkt ist.
        ///// </summary>
        //private void LoadDllDynamic()
        //{
        //    try
        //    {
        //        PlugInAssembly = System.Reflection.Assembly.LoadFrom("PlugIns\\DSAToolPlugin.dll");
        //        object pluInControl = null;
        //        if (PlugInAssembly != null)
        //            pluInControl = PlugInAssembly.CreateInstance("DSATool.Hauptfenster");

        //        if (pluInControl != null && pluInControl is System.Windows.Forms.Control)
        //        {
        //            PlugInControl = (System.Windows.Forms.Control)pluInControl;
        //            windowsFormsHost1.Child = PlugInControl;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgWindow errWin = new MsgWindow("PlugIn Fehler", "Beim Laden eines PlugIns ist ein Fehler aufgetreten.", ex, true);
        //        errWin.ShowDialog();
        //    }
        //}

        ///// <summary>
        ///// Wird derzeit nicht benutz, da DLL statisch verlinkt ist.
        ///// </summary>
        //private void InterfaceDynamic(Held h, int fernkampfwaffe, bool scharfschütze, bool meisterschütze, string suchmonat, int reiter)
        //{
        //    if (PlugInControl != null)
        //    {
        //        try
        //        {
        //            Type t = PlugInControl.GetType();
        //            t.InvokeMember("MeisterGeisterInterface", System.Reflection.BindingFlags.InvokeMethod, null, PlugInControl,
        //                new object[] {
        //                    h.MU, h.IN, h.KL, h.FF, h.GE, h.KK,
        //                    h.Talentwert("Ackerbau").Wert, h.Talentwert("Fährtensuchen").Wert,
        //                    h.Talentwert("Fallenstellen").Wert, fernkampfwaffe,
        //                    h.Talentwert("Fischen/Angeln").Wert, h.Talentwert("Pflanzenkunde").Wert,
        //                    h.Talentwert("Schleichen").Wert, h.Talentwert("Sich Verstecken").Wert,
        //                    h.Talentwert("Sinnenschärfe").Wert, h.Talentwert("Tierkunde").Wert,
        //                    h.Talentwert("Wildnisleben").Wert, scharfschütze, meisterschütze,
        //                    suchmonat, reiter}
        //                    );
        //        }
        //        catch (Exception ex)
        //        {
        //            MsgWindow errWin = new MsgWindow("PlugIn Fehler", "Beim Laden einer PlugIn-Methode ist ein Fehler aufgetreten.", ex, true);
        //            errWin.ShowDialog();
        //        }
        //    }
        //}

        //private System.Reflection.Assembly PlugInAssembly = null;
        //private System.Windows.Forms.Control PlugInControl = null;
    }
}
