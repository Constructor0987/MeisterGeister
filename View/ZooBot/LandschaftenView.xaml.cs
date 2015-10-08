using MeisterGeister.View.General;
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
using System.Windows.Shapes;
// Eigene Usings
using VM = MeisterGeister.ViewModel.ZooBot.Logic;

namespace MeisterGeister.View.ZooBot
{
    /// <summary>
    /// Interaktionslogik für LandschaftenView.xaml
    /// </summary>
    public partial class LandschaftenView : UserControl
    {
        public IList<Model.Landschaft> SelectedItems
        {
            get { return (IList<Model.Landschaft>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }
        public static DependencyProperty SelectedItemsProperty = DependencyProperty.Register("SelectedItems", typeof(IList<Model.Landschaft>), typeof(LandschaftenView),
                new PropertyMetadata(false));


        public VM.LandschaftenVM VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.LandschaftenVM))
                    return null;
                return DataContext as VM.LandschaftenVM;
            }
            set
            {
                DataContext = value;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public LandschaftenView()
        {
            InitializeComponent();
            VM = new VM.LandschaftenVM();
            VM.PropertyChanged += VM_PropertyChanged;
            
        }

        void VM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var vm = sender as VM.LandschaftenVM;
            if (sender != null && e.PropertyName == "SelectedItems")
                SelectedItems = vm.SelectedItems;
        }
    }
}
