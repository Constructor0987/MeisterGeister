using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace MeisterGeister.View.Settings
{
    public partial class HUEInitWindow : Window
    {

        public HUEInitWindow()
        {
            InitializeComponent();
        }
        public DispatcherTimer TimeLeftTimer = new DispatcherTimer();

        private int _timeLeft = 15;
        public int TimeLeft
        {
            get { return _timeLeft; }
            set
            {
                _timeLeft = value;
                lblCountdown.Content = value.ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TimeLeftTimer.Tick += new EventHandler(TimeLeftTimer_Tick);
            TimeLeftTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            TimeLeftTimer.IsEnabled = true;
            TimeLeftTimer.Start();
        }
        private void TimeLeftTimer_Tick(object sender, EventArgs e)
        {
            TimeLeft--;
        }
    }
}
