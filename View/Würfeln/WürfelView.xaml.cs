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
using System.Windows.Navigation;
using System.Windows.Shapes;
// Eigene Usings
using MeisterGeister.Logic.General;
using MeisterGeister.View.General;
using MeisterGeister.View.Windows;

namespace MeisterGeister.View.Würfeln
{
    /// <summary>
    /// Interaktionslogik für WürfelView.xaml
    /// </summary>
    public partial class WürfelView : UserControl
    {
        public WürfelView()
        {
            InitializeComponent();

            _checkBoxSoundAbspielen.Checked -= CheckBoxSoundAbspielen_Changed;
            _checkBoxSoundAbspielen.Unchecked -= CheckBoxSoundAbspielen_Changed;
            _checkBoxSoundAbspielen.IsChecked = MeisterGeister.Logic.Settings.Einstellungen.WuerfelSoundAbspielen;
            _checkBoxSoundAbspielen.Checked += CheckBoxSoundAbspielen_Changed;
            _checkBoxSoundAbspielen.Unchecked += CheckBoxSoundAbspielen_Changed;

            //Würfel.SoundAbspielenChanged += WürfelSoundAbspielen_Changed;
        }

        private void CheckBoxSoundAbspielen_Changed(object sender, RoutedEventArgs e)
        {
            if (IsInitialized)
            {
                //Würfel.SoundAbspielenChanged -= WürfelSoundAbspielen_Changed;
                MeisterGeister.Logic.Settings.Einstellungen.WuerfelSoundAbspielen = (bool)_checkBoxSoundAbspielen.IsChecked;
                //Würfel.SoundAbspielenChanged += WürfelSoundAbspielen_Changed;
            }
        }

        private void WürfelSoundAbspielen_Changed(object sender, EventArgs e)
        {
            _checkBoxSoundAbspielen.Checked -= CheckBoxSoundAbspielen_Changed;
            _checkBoxSoundAbspielen.Unchecked -= CheckBoxSoundAbspielen_Changed;
            _checkBoxSoundAbspielen.IsChecked = MeisterGeister.Logic.Settings.Einstellungen.WuerfelSoundAbspielen;
            _checkBoxSoundAbspielen.Checked += CheckBoxSoundAbspielen_Changed;
            _checkBoxSoundAbspielen.Unchecked += CheckBoxSoundAbspielen_Changed;
        }

        private void ButtonWürfel_Click(object sender, RoutedEventArgs e)
        {
            uint seiten = 0;
            if (((Button)sender).Tag == null)
                seiten = Convert.ToUInt32(_intBoxWx.Value);
            else
                seiten = Convert.ToUInt32(((Button)sender).Tag);
            
            Würfel w = new Würfel(seiten);
            w.Würfeln((uint)_intBoxAnzahl.Value, (uint)_intBoxWiederholungen.Value, _intBoxMod.Value);

            _labelEinzelwürfe.Text = string.Format("Einzelwürfe:\nW{0}", seiten);

            _textBlockEinzelwürfe.Text = w.ErgebnisDetails.Einzelwürfe;
            _textBlockErgebnisStaffel.Text = w.ErgebnisDetails.Staffel;
            _textBlockErgebnisSumme.Text = w.ErgebnisDetails.Summe.ToString();

            if (MeisterGeister.Logic.Settings.Einstellungen.WuerfelSoundAbspielen)
            {
                try
                {
                    Würfel.PlaySound();
                }
                catch (Exception ex)
                {
                    MsgWindow errWin = new MsgWindow("Audio Fehler", ex.Message);
                    errWin.ShowDialog();
                }
            }
        }

        private void ButtonWürfel_MouseEnter(object sender, MouseEventArgs e)
        {
            int seiten = 0;
            if (((Button)sender).Tag == null)
                seiten = Convert.ToInt32(_intBoxWx.Value);
            else
                seiten = Convert.ToInt32(((Button)sender).Tag);

            _textBlockErwartungswert.Text =
                Würfel.Erwartungswert(seiten, _intBoxAnzahl.Value, _intBoxWiederholungen.Value, _intBoxMod.Value).ToString();
            _labelErwartungswert.Text = string.Format("Erwartungswert:\n{0} x ({1}W{2}+{3})", _intBoxWiederholungen.Value, _intBoxAnzahl.Value, seiten, _intBoxMod.Value);
        }

        private void IntBoxWx_NumValueChanged(IntBox sender)
        {
            if (IsInitialized)
                ButtonWürfel_MouseEnter(_buttonWx, null);
        }
    }
}
