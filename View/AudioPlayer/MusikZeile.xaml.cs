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
//eigene 
using MeisterGeister.Model;
using VM = MeisterGeister.ViewModel.AudioPlayer.Logic;
using System.Globalization;

namespace MeisterGeister.View.AudioPlayer
{

    /// <summary>
    /// Interaktionslogik für MusikZeile.xaml
    /// </summary>
    public partial class MusikZeile : ListBoxItem
    {
        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.MusikZeileVM VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.MusikZeileVM))
                    return null;
                return DataContext as VM.MusikZeileVM;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Eine Zusammenführung aller durchsuchbaren Felder.
        /// </summary>
        private string _suchtext = string.Empty;

        public MusikZeile()
        {
            InitializeComponent();
            VM = new VM.MusikZeileVM();

            _suchtext = tblkTitel.Text.ToLower() + tboxKategorie.Text.ToLower();

            VM._timerCheckLaufend.Tick += new EventHandler(_timerCheckLaufend_Tick);
            VM._timerCheckLaufend.Interval = new TimeSpan(0, 0, 0, 0, 500);
        }

        public void _timerCheckLaufend_Tick(object sender, EventArgs e)
        {
            if (!(VM.aPlayerVM != null && VM.grpobj != null && VM.aPlayerVM._GrpObjecte.Contains(VM.grpobj)))
                VM.WirdAbgespielt  = false;
            else
            {
                for (int i = 0; i < VM.grpobj._listZeile.Count; i++)
                {
                    if (VM.grpobj._listZeile[i]._mplayer != null && 
                        VM.grpobj._listZeile[i]._mplayer.HasAudio)
                    {
                        VM.WirdAbgespielt = true;
                        return;
                    }
                }
                VM.WirdAbgespielt = false;
            }
               // (VM.grpobj._listZeile.Count(t => t.istLaufend) > 0)) ? true : false;
          //  if (!VM.WirdAbgespielt)
          //      VM._timerCheckLaufend.Stop();
        }

        #region //---- INSTANZMETHODEN ----

        /// <summary>
        /// Prüft, ob 'suchWort' im Namen, der Kategorie oder in den Tags vorkommt.
        /// </summary>
        /// <param name="suchWort"></param>
        /// <returns></returns>
        public bool Contains(string suchWort)
        {
            _suchtext = tblkTitel.Text.ToLower() + tboxKategorie.Text.ToLower();
            return _suchtext.Contains(suchWort);
        }

        /// <summary>
        /// Prüft, ob die 'suchWorte' im Namen, der Kategorie oder in den Tags vorkommt.
        /// Es wird dabei eine UND-Prüfung durchgeführt.
        /// </summary>
        /// <param name="suchWorte"></param>
        /// <returns></returns>
        public bool Contains(string[] suchWorte)
        {
            foreach (string wort in suchWorte)
            {
                if (!Contains(wort))
                    return false;
            }
            return true;
        }

        private void tboxTextChanged(object sender, TextChangedEventArgs e)
        {
            _suchtext = tblkTitel.Text.ToLower() + tboxKategorie.Text.ToLower();
        }

        private void OnTitelNameUpdated(object sender, DataTransferEventArgs e)
        {
            //tblkTitel.Text = VM.TitelName;
            _suchtext = tblkTitel.Text.ToLower() + tboxKategorie.Text.ToLower();
        }

        #endregion
    }
}
