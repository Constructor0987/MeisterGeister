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
using System.Diagnostics;
// Eigene Usings
using GeneralLogic = MeisterGeister.Logic.General;
using MeisterGeister.Logic.Einstellung;
using MeisterGeister.View.AudioPlayer;
using MeisterGeister.View.Windows;
using MeisterGeister.ViewModel.Settings;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //_checkBoxFrageNeueKampfrundeAbstellen.IsChecked = Einstellungen.FrageNeueKampfrundeAbstellen;
            //_checkBoxJingleAbstellen.IsChecked = Einstellungen.JingleAbstellen;
            //_checkboxGleichSpielen.IsChecked = Einstellungen.AudioDirektAbspielen;
            //_sldFading.Value = Einstellungen.Fading;
            //tbStdPfad.Text = Einstellungen.GetEinstellung("AudioVerzeichnis", @"C:\");
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
                MsgWindow errWin = new MsgWindow("Audio Fehler", ex.Message);
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
            if (stdPfad.Count > 0) stdPfad.RemoveRange(0, stdPfad.Count);
            stdPfad.AddRange(MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis.Split(new Char[] { '|' }));
        }
        
        private void CreateStandardPfadItems()
        {
            lbStandardPfade.Items.Clear();
            for (int i = 0; i < stdPfad.Count; i++)
            {
                ListboxItemBtn lbItemBtn = new ListboxItemBtn();
                lbItemBtn.lblStdPfad.Content = stdPfad[i];
                lbItemBtn.btnStdPfad.Click += btnStdPfad_Click;
                lbItemBtn.imgIcon.MouseUp += imgStdIconDelete_MouseUp;
                lbStandardPfade.Items.Add(lbItemBtn);
            }
            ListboxItemBtn lbItemBtnLeer = new ListboxItemBtn();
            lbItemBtnLeer.lblStdPfad.Content = "";
            lbItemBtnLeer.btnStdPfad.Click += btnStdPfad_Click;
            lbItemBtnLeer.imgIcon.MouseUp += imgStdIconDelete_MouseUp;
            lbStandardPfade.Items.Add(lbItemBtnLeer);
        }

        private void btnStdPfad_Click(object sender, RoutedEventArgs e)
        {
            ListboxItemBtn lbiBtn = (ListboxItemBtn)((StackPanel)((Grid)((Button)sender).Parent).Parent).Parent;
            if (((Button)sender).Tag != null)
            {
                string s = "";
                if ((string)lbiBtn.lblStdPfad.Content == "")
                    stdPfad.Add("");
                for (int i = 0; i < stdPfad.Count; i++)
                {
                    if ((string)lbiBtn.lblStdPfad.Content != stdPfad[i])
                        s += stdPfad[i] + "|";
                    else
                        s += lbiBtn.btnStdPfad.Tag.ToString() + "|";
                }

                if (s.Length > 0)
                    s = s.Substring(0, s.Length - 1);
                else
                    s = "C:";

                lbiBtn.lblStdPfad.Content = ((Button)sender).Tag.ToString();

                Logic.Einstellung.Einstellungen.AudioVerzeichnis = s;
                Logic.Einstellung.Einstellungen.UpdateEinstellungen();
                ((Button)sender).Tag = null;
                setStdPfad();
            }
        }

        private void imgStdIconDelete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ListboxItemBtn lbiBtn = (ListboxItemBtn)((StackPanel)((Grid)((Image)sender).Parent).Parent).Parent;
            string s = "";
            for (int i = 0; i < stdPfad.Count; i++)
                if (stdPfad[i] != lbiBtn.lblStdPfad.Content.ToString())
                    s += stdPfad[i] + "|";
            if (s.Length > 0)
                s = s.Substring(0, s.Length - 1);
            else
                s = "C:";

            Logic.Einstellung.Einstellungen.AudioVerzeichnis = s;
            Logic.Einstellung.Einstellungen.UpdateEinstellungen();

            ((ListBox)lbiBtn.Parent).Items.Remove(lbiBtn);
            setStdPfad();
        }

        public void _sldFading_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsInitialized)
            {
                _sldFading.ToolTip = Math.Round(e.NewValue / 100, 1) + " Sekunden In-/Out-Fading";
                Einstellungen.Fading = (int)e.NewValue;
            }
        }

        private void CheckBoxEinstellungItem_CheckedChanged(object sender, RoutedEventArgs e)
        {
            object tag = (sender as CheckBox).Tag;
            if (tag != null)
            {
                // Notification Event feuern
                // Bei Bedarf können hier weitere Events ausgelöst werden...
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
