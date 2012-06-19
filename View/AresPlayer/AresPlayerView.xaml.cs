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
using MeisterGeister.View.Windows;

namespace MeisterGeister.View.AresPlayer
{
    /// <summary>
    /// Interaktionslogik für AresPlayerView.xaml
    /// </summary>
    public partial class AresPlayerView : UserControl
    {
        public AresPlayerView()
        {
            InitializeComponent();

            // PlugIn DLL laden
            try
            {
                var pluInControl = new Ares.MGPlugin.Controller();

                if (pluInControl != null)
                {
                    PlugInControl = pluInControl;
                    _windowsFormsHost1.Child = PlugInControl;
                }
            }
            catch (Exception ex)
            {
                MsgWindow errWin = new MsgWindow("PlugIn Fehler", "Beim Laden eines PlugIns ist ein Fehler aufgetreten.", ex);
                errWin.ShowDialog();
            }
        }

        private Ares.MGPlugin.Controller PlugInControl = null;
    }
}
