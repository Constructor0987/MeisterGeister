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

            _comboBoxHeld.ItemsSource = App.DatenDataSet.Held.Select("AktiveHeldengruppe = true", "Name ASC");

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

        private MeisterGeister.View.ZooBot.Hauptfenster PlugInControl = null;

        public void SetHeldWerte(string talentname = "")
        {

            DatabaseDSADataSet.HeldRow heldRow = null;
            if (_comboBoxHeld.SelectedItem != null)
                heldRow = (DatabaseDSADataSet.HeldRow)_comboBoxHeld.SelectedItem;

            Held h = new Held(heldRow);

            // Fernkampfwert
            string fernkampfwaffeString = _comboBoxFernkampfwaffe.SelectedValue.ToString();
            int fernkampfwaffe = h.Talentwert(fernkampfwaffeString).Wert;

            // Sonderfertigkeiten
            bool scharfschütze = (h.SonderfertigkeitRow(string.Format("Scharfschütze ({0})", fernkampfwaffeString)) != null);
            bool meisterschütze = (h.SonderfertigkeitRow(string.Format("Meisterschütze ({0})", fernkampfwaffeString)) != null);

            string suchmonat = (Datum.Aktuell.Monat == Monat.NamenloseTage ? "Namenlose Tage" : Datum.Aktuell.MonatString());
            int reiter = SelectTab(talentname);

            // Übergabe an Einsteins DSA-Tool
            if (PlugInControl != null)
            {
                try
                {
                    PlugInControl.MeisterGeisterInterface(
                            Math.Max(0, h.MU), Math.Max(0, h.IN), Math.Max(0, h.KL), Math.Max(0, h.FF), Math.Max(0, h.GE), Math.Max(0, h.KK),
                            Math.Max(0, h.Talentwert("Ackerbau").Wert), Math.Max(0, h.Talentwert("Fährtensuchen").Wert),
                            Math.Max(0, h.Talentwert("Fallenstellen").Wert), Math.Max(0, fernkampfwaffe),
                            Math.Max(0, h.Talentwert("Fischen/Angeln").Wert), Math.Max(0, h.Talentwert("Pflanzenkunde").Wert),
                            Math.Max(0, h.Talentwert("Schleichen").Wert), Math.Max(0, h.Talentwert("Sich Verstecken").Wert),
                            Math.Max(0, h.Talentwert("Sinnenschärfe").Wert), Math.Max(0, h.Talentwert("Tierkunde").Wert),
                            Math.Max(0, h.Talentwert("Wildnisleben").Wert), scharfschütze, meisterschütze,
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
