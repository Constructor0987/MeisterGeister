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
using System.Windows.Threading;

namespace MeisterGeister.View.Windows
{
    /// <summary>
    /// Interaktionslogik für StartupWindow.xaml
    /// Dient als Wartefenster während MeisterGeister startet und den Jingle abspielt.
    /// </summary>
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();

            _closeTimer.Interval = new TimeSpan(0, 0, 15); // automatisch nach 15 sec schließen
            _closeTimer.Tick += new EventHandler(CloseTimer_Tick);
            _closeTimer.Start();
        }

        void CloseTimer_Tick(object sender, EventArgs e)
        {
            KillMe();
        }

        public void KillMe()
        {
            _closeTimer.Stop();
            this.Close();
            Application.Current.MainWindow.Opacity = 1;
        }

        private DispatcherTimer _closeTimer = new DispatcherTimer();

        private void _jingleStoppTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();

            EinstellungenWindow gui = new EinstellungenWindow();
            gui.ShowDialog();
        }
    }
}
