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

namespace MeisterGeister.View.AudioPlayer
{
    /// <summary>
    /// Interaktionslogik für grdThemeButton.xaml
    /// </summary>
    public partial class grdThemeButton : UserControl
    {
        private string _suchtext = string.Empty;
        
        
        /// <summary>
        /// Ruft das ViewModel des Views ab oder legt es fest und weist das ViewModel dem DataContext zu.
        /// </summary>
        public VM.grdThemeBtnVM VM
        {
            get
            {
                if (DataContext == null || !(DataContext is VM.grdThemeBtnVM))
                    return null;
                return DataContext as VM.grdThemeBtnVM;
            }
            set { DataContext = value; }
        }


        public grdThemeButton()
        {
            InitializeComponent();
            VM = new VM.grdThemeBtnVM();
        }



        #region //---- INSTANZMETHODEN ----

        /// <summary>
        /// Prüft, ob 'suchWort' im Namen, der Kategorie oder in den Tags vorkommt.
        /// </summary>
        /// <param name="suchWort"></param>
        /// <returns></returns>
        public bool Contains(string suchWort)
        {
            _suchtext = VM.Theme.Name.ToLower() + ((VM.Theme.Kategorie !=null)? VM.Theme.Kategorie.ToLower(): "");
            return _suchtext.Contains(suchWort.ToLower());
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
                if (!Contains(wort.ToLower()))
                    return false;
            }
            return true;
        }

        #endregion

        private void grd_MouseEnter(object sender, MouseEventArgs e)
        {
            VM.ForceVolumeVis = true;
        }

        private void grd_MouseLeave(object sender, MouseEventArgs e)
        {
            VM.ForceVolumeVis = VM.GroßeAnsicht;
        }
    }
}
