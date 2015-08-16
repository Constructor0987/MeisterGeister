using MeisterGeister.ViewModel.Settings;

// ReSharper disable once CheckNamespace

namespace MeisterGeister.View.Settings
{
    /// <summary>
    ///     Interaction logic for NetzwerkeinstellungenView.xaml
    /// </summary>
    public partial class NetzwerkeinstellungenView
    {
        public NetzwerkeinstellungenView()
        {
            InitializeComponent();
            Vm = new NetzwerkeinstellungenViewModel();
        }

        public NetzwerkeinstellungenViewModel Vm
        {
            get { return DataContext as NetzwerkeinstellungenViewModel; }
            set { DataContext = value; }
        }
    }
}