using System.Windows;
using System.Windows.Controls;
// Eigene Usings
using VM = MeisterGeister.ViewModel.Helden;

namespace MeisterGeister.View.Helden.Controls
{
    /// <summary>
    /// Interaktionslogik für BekanntePflanzenView.xaml
    /// </summary>
    public partial class BekanntePflanzenView : UserControl
    {
                
        public BekanntePflanzenView()
        {
            InitializeComponent();
            VM = new VM.BekanntePflanzenVM();
            Width = 550;
        }

        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.BekanntePflanzenVM VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.BekanntePflanzenVM))
                    return null;
                return DataContext as VM.BekanntePflanzenVM;
            }
            set { DataContext = value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (VM != null)
                VM.NotifyRefresh();
            VM = new VM.BekanntePflanzenVM();
        }
    }
}
