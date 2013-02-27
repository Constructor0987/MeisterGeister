﻿using System;
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
//Eigene Usings
using MeisterGeister.Model;
using VM = MeisterGeister.ViewModel.Helden;

namespace MeisterGeister.View.Helden.Controls
{
    /// <summary>
    /// Interaktionslogik für EigenschaftenView.xaml
    /// </summary>
    public partial class EigenschaftenView : UserControl
    {
        public EigenschaftenView()
        {
            InitializeComponent();

         
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.EigenschaftenViewModel VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.EigenschaftenViewModel))
                    return null;
                return DataContext as VM.EigenschaftenViewModel;
            }
            set { DataContext = value; }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.ListenToChangeEvents = IsVisible;
        }

        //LoadedEvent: Init VM hier um zur DesignTime die UI laden zu können
        private void EigenschaftenLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
        	   //VM an View Registrieren
            VM = new VM.EigenschaftenViewModel();
			
			  try
            {
                VM.Init();
            }
            catch (Exception)
            {
            }
            if (VM != null)
                VM.ListenToChangeEvents = IsVisible;
        }     
    }
}
