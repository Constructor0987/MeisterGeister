using System;
using System.Collections.Generic;
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
//Eigene Usings
using MeisterGeister.Model;
using VM = MeisterGeister.ViewModel.Kampf.Logic;
//using Logic = MeisterGeister.ViewModel.Kampf.Logic;
using MeisterGeister.View.General;
using Settings = MeisterGeister.Logic.Settings;

namespace MeisterGeister.View.Kampf.Controls
{
	/// <summary>
	/// Interaktionslogik für GegnerView.xaml
	/// </summary>
    public partial class ManöverView : System.Windows.Controls.UserControl
	{
        public ManöverView()
		{
			this.InitializeComponent();
            //VM an View Registrieren
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.ManöverInfo VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.ManöverInfo))
                    return null;
                return DataContext as VM.ManöverInfo;
            }
            set { DataContext = value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

	}
}