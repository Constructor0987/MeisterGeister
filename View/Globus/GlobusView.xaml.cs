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
using System.Net;
using System.Xml;
using MeisterGeister.View.Windows;

namespace MeisterGeister.View.Globus
{
    /// <summary>
    /// Interaktionslogik für GlobusControl.xaml
    /// </summary>
    public partial class GlobusView : UserControl
    {
        public GlobusView()
        {
            InitializeComponent();

            try
            {
                DgSuche.GlobusControl pluInControl = new DgSuche.GlobusControl(true);

                if (pluInControl != null)
                    this.Content = pluInControl;
            }
            catch (Exception ex)
            {
                MsgWindow errWin = new MsgWindow("PlugIn Fehler", "Beim Laden eines PlugIns ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
            }
        }

        /// <summary>
        /// Wird derzeit nicht benutz, da DLL statisch verlinkt ist.
        /// </summary>
        private void LoadDllDynamic()
        {
            // PlugIn DLL laden
            try
            {
                System.Reflection.Assembly plugInAssembly = System.Reflection.Assembly.LoadFrom("DG-Suche.dll");
                object pluInControl = plugInAssembly.CreateInstance("DgSuche.GlobusControl", false, System.Reflection.BindingFlags.CreateInstance, null, new object[] { true }, null, null);

                if (pluInControl != null)
                    this.Content = pluInControl;
            }
            catch (Exception ex)
            {
                MsgWindow errWin = new MsgWindow("PlugIn Fehler", "Beim Laden eines PlugIns ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
            }
        }
    }
}