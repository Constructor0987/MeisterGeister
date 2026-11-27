using System.Windows;

namespace MeisterGeister.View.Windows
{
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();
        }

        public void SetStatus(string message)
        {
            _statusTextBlock.Text = message;
        }
    }
}
