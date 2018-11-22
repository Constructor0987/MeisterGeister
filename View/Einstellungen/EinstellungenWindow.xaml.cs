using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MeisterGeister.Logic.Einstellung;
using MeisterGeister.View.AudioPlayer;
using MeisterGeister.View.General;
using MeisterGeister.View.Windows;
using MeisterGeister.ViewModel.Settings;

// Eigene Usings
using GeneralLogic = MeisterGeister.Logic.General;

namespace MeisterGeister.View.Settings
{
    /// <summary>
    /// Interaktionslogik für EinstellungenWindow.xaml
    /// </summary>
    public partial class EinstellungenWindow : Window
    {
        public List<string> stdPfad = new List<string>();

        public EinstellungenWindow()
        {
            InitializeComponent();

            // DataContext setzen
            VM = new EinstellungenViewModel();

            _listBoxSettings.ItemsSource = Global.ContextHeld.Liste<Model.Setting>();

            // AudioPlayer Standard-Pfade auflisten
            setStdPfad();
            CreateStandardPfadItems();
            // CheckBox & Slider aktualisieren
            _checkboxSpieldauerBerechnen.IsChecked = Einstellungen.AudioSpieldauerBerechnen;
            _checkboxInAnderemPfadSuchen.IsChecked = Einstellungen.AudioInAnderemPfadSuchen;
            _sldFading.Value = Einstellungen.Fading;
        }

        public EinstellungenViewModel VM
        {
            get { return DataContext as EinstellungenViewModel; }
            set { DataContext = value; }
        }

        public void _sldFading_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsInitialized)
            {
                _sldFading.ToolTip = Math.Round(e.NewValue / 100, 1) + " Sekunden In-/Out-Fading";
                Einstellungen.Fading = (int)e.NewValue;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonPlayJingle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Jingle abspielen
                GeneralLogic.AudioPlayer.PlayJingle();
            }
            catch (Exception ex)
            {
                var errWin = new MsgWindow("Audio Fehler", ex.Message);
                errWin.ShowDialog();
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));

            e.Handled = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Global.ContextHeld.Save();
        }

        private void setStdPfad()
        {
            if (stdPfad.Any())
            {
                stdPfad.RemoveRange(0, stdPfad.Count);
            }

            stdPfad.AddRange(Einstellungen.AudioVerzeichnis.Split(new char[] { '|' }));
        }

        private void CreateStandardPfadItems()
        {
            lbStandardPfade.Items.Clear();
            for (var i = 0; i <= stdPfad.Count; i++)
            {
                var lbItemBtn = new ListboxItemBtn();
                lbItemBtn.lblStdPfad.Content = i < stdPfad.Count ? stdPfad[i] : string.Empty;
                SetStandardPfadItemEvents(lbItemBtn);
                lbStandardPfade.Items.Add(lbItemBtn);
            }
        }

        private void SetStandardPfadItemEvents(ListboxItemBtn listboxItemBtn)
        {
            listboxItemBtn.btnStdPfad.Click += btnStdPfad_Click;
            listboxItemBtn.imgIcon.MouseUp += imgStdIconDelete_MouseUp;
        }

        private void btnStdPfad_Click(object sender, RoutedEventArgs e)
        {
            var lbiBtn = (ListboxItemBtn)((StackPanel)((Grid)((Button)sender).Parent).Parent).Parent;
            if (((Button)sender).Tag != null)
            {
                var s = "";
                if ((string)lbiBtn.lblStdPfad.Content == "")
                {
                    stdPfad.Add("");
                }

                for (var i = 0; i < stdPfad.Count; i++)
                {
                    if ((string)lbiBtn.lblStdPfad.Content != stdPfad[i])
                    {
                        s += stdPfad[i] + "|";
                    }
                    else
                    {
                        s += lbiBtn.btnStdPfad.Tag.ToString() + "|";
                    }
                }

                if (s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                }
                else
                {
                    s = "C:";
                }

                lbiBtn.lblStdPfad.Content = ((Button)sender).Tag.ToString();

                Einstellungen.AudioVerzeichnis = s;
                Einstellungen.UpdateEinstellungen();
                ((Button)sender).Tag = null;
                setStdPfad();

                var lastButton = lbStandardPfade.Items[lbStandardPfade.Items.Count - 1] as ListboxItemBtn;
                SetStandardPfadItemEvents(lastButton);

                ViewHelper.Popup("Die Änderungen können erst beim nächten Starten des Audio-Tools übernommen werden." + Environment.NewLine +
                    "Falls das Audio-Tool geöffnet ist, schließen Sie es und öffnen es erneut, um die Standard-Pfad entsprechend zu setzen.");
            }
        }

        private void imgStdIconDelete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var lbiBtn = (ListboxItemBtn)((StackPanel)((Grid)((Image)sender).Parent).Parent).Parent;
            var s = "";
            for (var i = 0; i < stdPfad.Count; i++)
            {
                if (stdPfad[i] != lbiBtn.lblStdPfad.Content.ToString())
                {
                    s += stdPfad[i] + "|";
                }
            }

            if (s.Length > 0)
            {
                s = s.Substring(0, s.Length - 1);
            }
            else
            {
                s = "C:";
            }

            Einstellungen.AudioVerzeichnis = s;
            Einstellungen.UpdateEinstellungen();

            ((ListBox)lbiBtn.Parent).Items.Remove(lbiBtn);
            setStdPfad();
            ViewHelper.Popup("Die Änderungen können erst beim nächten Starten des Audio-Tools übernommen werden." + Environment.NewLine +
                "Falls das Audio-Tool geöffnet ist, schließen Sie es und öffnen es erneut, um die Standard-Pfad entsprechend zu setzen.");
        }

        private void CheckBoxEinstellungItem_CheckedChanged(object sender, RoutedEventArgs e)
        {
            var tag = (sender as CheckBox).Tag;
            if (tag != null)
            {
                // Notification Event feuern Bei Bedarf können hier weitere Events ausgelöst werden...
                switch (tag.ToString())
                {
                    case "ToolTitelAusblenden":
                        Einstellungen.RaiseToolTitelAusblendenChanged();
                        break;

                    case "WuerfelSoundAbspielen":
                        Einstellungen.RaiseWuerfelSoundAbspielenChanged(this);
                        break;

                    default:
                        break;
                }
            }
        }

        private void _rbtnSpieldauerBerechnen_Checked(object sender, RoutedEventArgs e)
        {
        }
    }
}
