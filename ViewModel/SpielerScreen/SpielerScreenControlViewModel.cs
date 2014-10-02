using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Eigene usings
using MeisterGeister.ViewModel.Almanach.Logic;
using Base = MeisterGeister.ViewModel.Base;

namespace MeisterGeister.ViewModel.SpielerScreen
{
    public class SpielerScreenControlViewModel : Base.ViewModelBase
    {
        #region //---- FELDER ----

        // Felder
        private string _bildschirmInfo = "1 Bildschirm";

        // Listen
        private List<System.Windows.Forms.Screen> _screenList = System.Windows.Forms.Screen.AllScreens.ToList();

        //Commands

        #endregion

        #region //---- EIGENSCHAFTEN ----

        public string BildschirmInfo
        {
            get { return _bildschirmInfo; }
        }

        #region //---- LISTEN ----

        public List<System.Windows.Forms.Screen> ScreenList
        {
            get { return _screenList; }
        }

        #endregion

        //Commands

        #endregion

        #region //---- KONSTRUKTOR ----

        public SpielerScreenControlViewModel()
        {
            Init();
        }

        #endregion

        #region //---- INSTANZMETHODEN ----

        public void Refresh()
        {

        }

        public void Init()
        {
            // Bildschirminfos
            _bildschirmInfo = string.Format("{0} Bildschirm{1}", ScreenList.Count, ScreenList.Count == 1 ? string.Empty : "e");
        }


        #endregion

        #region //---- EVENTS ----

        #endregion
    }
}