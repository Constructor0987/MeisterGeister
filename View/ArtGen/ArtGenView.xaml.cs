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
using MeisterGeister.Daten;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.LogicAlt.General;
using MeisterGeister.View.Windows;

namespace MeisterGeister.View.ArtGen
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ArtGenView : UserControl
    {
        public ArtGenView()
        {
            InitializeComponent();

            _comboBoxHeld.ItemsSource = App.DatenDataSet.Held.Select("AktiveHeldengruppe = true", "Name ASC");

            // PlugIn DLL laden
            try
            {
                var pluInControl = new ArtefaktGenerator.ArtGenControl(true);

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
        }

        private ArtefaktGenerator.ArtGenControl PlugInControl = null;

        // TODO ??: Umstellen auf neues Model
        public void SetHeldWerte()
        {
            DatabaseDSADataSet.HeldRow heldRow = null;
            if (_comboBoxHeld.SelectedItem != null)
                heldRow = (DatabaseDSADataSet.HeldRow)_comboBoxHeld.SelectedItem;

            Held h = new Held(heldRow);

            //// wird benötigt, wenn DLL dynamisch geladen wird
            //object representation = 1;
            //try
            //{
            //    Type sfType = PlugInAssembly.GetType("ArtefaktGenerator.SF+SFType");
            //    representation = Enum.Parse(sfType, "1");
            //}
            //catch (Exception)
            //{
            //}

            ArtefaktGenerator.SF.SFType representation = ArtefaktGenerator.SF.SFType.OTHER;

            bool sfKraftkontrolle = h.HatSonderfertigkeit("Kraftkontrolle");
            bool sfVielfacheLadung = h.HatSonderfertigkeit("Vielfache Ladungen");
            bool sfStapeleffekt = h.HatSonderfertigkeit("Stapeleffekt");
            bool sfHypervehemenz = h.HatSonderfertigkeit("Hypervehemenz");
            bool sfMatrixgeber = h.HatSonderfertigkeit("Matrixgeber");
            bool sfSemipermI = h.HatSonderfertigkeit("Semipermanenz I");
            bool sfSemipermII = h.HatSonderfertigkeit("Semipermanenz II");
            bool sfRingkunde = false;
            bool sfAuxiliator = h.HatSonderfertigkeit("Auxiliator");
            uint tawArcanovi = 0;

            List<DreierProbenWert> liZauber = h.Zauberwert(new Zauber("Arcanovi Artefakt (Spruchspeicher)"));
            if (liZauber.Count > 0)
                tawArcanovi = Convert.ToUInt32(liZauber[0].Wert);
            uint tawArcanoviMatrix = 0;
            liZauber = h.Zauberwert(new Zauber("Arcanovi Artefakt (Matrixgeber)"));
            if (liZauber.Count > 0)
                tawArcanoviMatrix = Convert.ToUInt32(liZauber[0].Wert);
            uint tawArcanoviSemi = 0;
            liZauber = h.Zauberwert(new Zauber("Arcanovi Artefakt (Semipermanenz)"));
            if (liZauber.Count > 0)
                tawArcanoviSemi = Convert.ToUInt32(liZauber[0].Wert);
            uint tawOdem = 0;
            liZauber = h.Zauberwert(new Zauber("Odem Arcanum"));
            if (liZauber.Count > 0)
                tawOdem = Convert.ToUInt32(liZauber[0].Wert);
            uint tawAnalys = 0;
            liZauber = h.Zauberwert(new Zauber("Analys Arcanstruktur"));
            if (liZauber.Count > 0)
                tawAnalys = Convert.ToUInt32(liZauber[0].Wert);
            uint tawDestructibo = 0;
            liZauber = h.Zauberwert(new Zauber("Destructibo Arcanitas"));
            if (liZauber.Count > 0)
                tawDestructibo = Convert.ToUInt32(liZauber[0].Wert);
            uint tawMagiekunde = Convert.ToUInt32(h.Talentwert("Magiekunde").Wert);
            
            // Übergabe an ArtefaktGenerator
            PlugInControl.plugInHero(h.Name, representation, sfKraftkontrolle, sfVielfacheLadung, sfStapeleffekt, sfHypervehemenz, sfMatrixgeber, sfSemipermI, 
                sfSemipermII, sfRingkunde, sfAuxiliator, tawArcanovi, tawArcanoviMatrix, tawArcanoviSemi, tawOdem, tawAnalys, tawDestructibo, tawMagiekunde);
        }

        

        private void ComboBoxAuswahl_DropDownClosed(object sender, EventArgs e)
        {
            if (IsInitialized && _comboBoxHeld.SelectedItem != null)
            {
                SetHeldWerte();
            }
        }

        private void ComboBoxAuswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized && _comboBoxHeld.SelectedItem != null)
            {
                SetHeldWerte();
            }
        }

        ///// <summary>
        ///// Wird derzeit nicht benutz, da DLL statisch verlinkt ist.
        ///// </summary>
        //private void LoadDllDynamic()
        //{
        //    try
        //    {
        //        PlugInAssembly = System.Reflection.Assembly.LoadFrom("PlugIns\\ArtefaktGenerator.dll");
        //        object pluInControl = null;
        //        if (PlugInAssembly != null)
        //            pluInControl = PlugInAssembly.CreateInstance("ArtefaktGenerator.ArtGenControl", false, System.Reflection.BindingFlags.CreateInstance, null, new object[] { true }, null, null);

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

        //private System.Reflection.Assembly PlugInAssembly = null;
        //private System.Windows.Forms.Control PlugInControl = null;

        ///// <summary>
        ///// Wird derzeit nicht benutz, da DLL statisch verlinkt ist.
        ///// </summary>
        //private void InterfaceDynamic(Held h, object representation, bool sfKraftkontrolle, bool sfVielfacheLadung, bool sfStapeleffekt,
        //    bool sfHypervehemenz, bool sfMatrixgeber, bool sfSemipermI, bool sfSemipermII, bool sfRingkunde, bool sfAuxiliator, uint tawArcanovi,
        //    uint tawArcanoviMatrix, uint tawArcanoviSemi, uint tawOdem, uint tawAnalys, uint tawDestructibo, uint tawMagiekunde)
        //{
        //    if (PlugInControl != null)
        //    {
        //        try
        //        {
        //            Type t = PlugInControl.GetType();
        //            t.InvokeMember("plugInHero", System.Reflection.BindingFlags.InvokeMethod, null, PlugInControl,
        //                new object[] {
        //                    h.Name, representation, sfKraftkontrolle, sfVielfacheLadung, sfStapeleffekt, 
        //                    sfHypervehemenz, sfMatrixgeber, sfSemipermI, sfSemipermII, sfRingkunde,
        //                    sfAuxiliator, tawArcanovi, tawArcanoviMatrix, tawArcanoviSemi, tawOdem,
        //                    tawAnalys, tawDestructibo, tawMagiekunde }
        //                    );
        //        }
        //        catch (Exception ex)
        //        {
        //            MsgWindow errWin = new MsgWindow("PlugIn Fehler", "Beim Laden einer PlugIn-Methode ist ein Fehler aufgetreten.", ex, true);
        //            errWin.ShowDialog();
        //        }
        //    }
        //}
    }
}
