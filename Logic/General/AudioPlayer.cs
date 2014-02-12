using System;
using System.Windows.Media;
using MeisterGeister.View.Windows;
using System.Reflection;
using System.IO;

namespace MeisterGeister.Logic.General
{
    public class AudioPlayer
    {
        private static MediaPlayer _player;

        public static void PlayFile(String url)
        {
            if (!IsMediaPlayerAvailable)
                return;

            try
            {
                if (_player == null)
                {
                    _player = new MediaPlayer();
                }
                _player.MediaFailed += new EventHandler<ExceptionEventArgs>(Player_MediaFailed);
                _player.MediaEnded += new EventHandler(Player_JingleEnded);

                _player.Open(new Uri(url));
                _player.Play();
            }
            catch (Exception ex)
            {
                string msg = "Der Audio Player hat einen Fehler verursacht.";

                if (ex is System.Windows.Media.InvalidWmpVersionException)
                {
                    IsMediaPlayerAvailable = false;
                    msg += "\nWindows Media Player ab Version 10 ist erforderlich, um die Audio-Funktionen von MeisterGeister zu nutzen.";
                    msg += "\nDie Audio-Funktionen werden für diese Sitzung deaktiviert.";
                }

                var errWin = new MsgWindow("Audio Fehler", msg, ex);
                errWin.ShowDialog();
                errWin.Close();
            }
        }

        static void Player_JingleEnded(object sender, EventArgs e)
        {
            App.CloseSplashScreen();
        }

        private static bool IsMediaPlayerAvailable = true;

        static void Player_MediaFailed(object sender, ExceptionEventArgs e)
        {
            if (!IsMediaPlayerAvailable)
                return;

            string msg = "Der Audio Player hat einen Fehler verursacht.";

            if (e.ErrorException is System.Windows.Media.InvalidWmpVersionException)
            {
                IsMediaPlayerAvailable = false;
                msg += "\nWindows Media Player ab Version 10 ist erforderlich, um die Audio-Funktionen von MeisterGeister zu nutzen.";
                msg += "\nDie Audio-Funktionen werden für diese Sitzung deaktiviert.";
            }

            var errWin = new MsgWindow("Audio Fehler", msg, e.ErrorException);
            errWin.ShowDialog();
            errWin.Close();
        }

        public static void PlayJingle()
        {
            PlayFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Audio\meistergeister.wav");
        }

        public static void PlayWürfel()
        {
            PlayFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Audio\WuerfelnSound0000.wav");
        }

    }
}
