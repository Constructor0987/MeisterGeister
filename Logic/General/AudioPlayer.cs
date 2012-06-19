using System;
using System.Windows.Media;
using MeisterGeister.View.Windows;

namespace MeisterGeister.Logic.General
{
    public class AudioPlayer
    {
        private static MediaPlayer _player;

        public static void PlayFile(String url)
        {
            try
            {
                if (_player == null)
                    _player = new MediaPlayer();
                
                _player.MediaFailed += new EventHandler<ExceptionEventArgs>(Player_MediaFailed);

                _player.Open(new Uri(url));
                _player.Play();
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Audio Fehler", "Der Audio Player hat einen Fehler verursacht.", ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        static void Player_JingleEnded(object sender, EventArgs e)
        {
            App.CloseSplashScreen();
        }

        static void Player_MediaFailed(object sender, ExceptionEventArgs e)
        {
            var errWin = new MsgWindow("Audio Fehler", "Der Audio Player hat einen Fehler verursacht.", e.ErrorException);
            errWin.ShowDialog();
            errWin.Close();
        }

        public static void PlayJingle()
        {
            if (_player == null)
                _player = new MediaPlayer();
            _player.MediaEnded += new EventHandler(Player_JingleEnded);

            PlayFile(Environment.CurrentDirectory + @"\Audio\meistergeister.wav");
        }

        public static void PlayWürfel()
        {
            PlayFile(Environment.CurrentDirectory + @"\Audio\WuerfelnSound0000.wav");
        }

    }
}
