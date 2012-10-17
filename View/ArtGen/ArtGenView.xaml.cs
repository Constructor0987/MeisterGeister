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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        public void Reload()
        {
            _comboBoxHeld.ItemsSource = Global.ContextHeld.HeldenGruppeListe;
        }

        public void SetHeldWerte()
        {
            Model.Held h = null;
            if (_comboBoxHeld.SelectedItem != null)
                h = (Model.Held)_comboBoxHeld.SelectedItem;
            else
                h = new Model.Held();

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

            uint tawArcanovi = Convert.ToUInt32(h.Zauberfertigkeitswert("Arcanovi Artefakt (Spruchspeicher)"));
            uint tawArcanoviMatrix = Convert.ToUInt32(h.Zauberfertigkeitswert("Arcanovi Artefakt (Matrixgeber)"));
            uint tawArcanoviSemi = Convert.ToUInt32(h.Zauberfertigkeitswert("Arcanovi Artefakt (Semipermanenz)"));
            uint tawOdem = Convert.ToUInt32(h.Zauberfertigkeitswert("Odem Arcanum"));
            uint tawAnalys = Convert.ToUInt32(h.Zauberfertigkeitswert("Analys Arcanstruktur"));
            uint tawDestructibo = Convert.ToUInt32(h.Zauberfertigkeitswert("Destructibo Arcanitas"));
            uint tawMagiekunde = Convert.ToUInt32(h.Talentwert("Magiekunde"));
            
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
