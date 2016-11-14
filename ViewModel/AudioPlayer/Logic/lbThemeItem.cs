using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using MeisterGeister.Model.Extensions;
using System.Windows.Threading;
using System.Windows.Controls;
// Eigene Usings
using MeisterGeister.View.AudioPlayer;
using MeisterGeister.ViewModel.AudioPlayer;
using MeisterGeister.Model;
using MeisterGeister.ViewModel.AudioPlayer.Logic;
using System.Windows.Media.Imaging;
using MeisterGeister.View.General;
using System.IO;

namespace MeisterGeister.ViewModel.AudioPlayer.Logic
{
    public class lbThemeItem : Base.ToolViewModelBase
    {
        #region //---- FELDER ----

        //private BitmapImage _listBoxItemIcon;
        private lbThemeItem _item = null;
        private string _suchtext = string.Empty;

        private bool _changed = false;
	    private double _totalTimePlylist = 0;    
	    private double _vol_PlaylistMod = 0;
	    private DateTime _lastVolUpdate = DateTime.Now;
	    private uint _sollBtnGedrueckt = 0;
	    private Audio_Playlist _aPlaylist = null;
	    private List<Audio_Playlist_Titel> _aPlaylistTitel;
	    private int _objGruppe;
	    private UInt16 _anzVolChange = 0;
	    private UInt16 _anzPauseChange;
	    private string _playlistName = "";
	    private bool _istMusik = true;
        private List<MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile> _listZeile = new List<MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile>();
	    private bool _wirdAbgespielt = false;
	    private List<Guid> _nochZuSpielen = new List<Guid>();
	    private List<UInt16> _gespielt = new List<UInt16>();
	    private Nullable<double> _force_Volume = null;

	    private bool _visuell = true;
        public DispatcherTimer wartezeitTimer = new DispatcherTimer();


        //Commands
        private Base.CommandBase _onlbThemeItemAdd;        

        #endregion

        #region //---- EIGENSCHAFTEN ----

        [DependentProperty("APlaylist")]
        public string ListBoxItemIconBild
        {
            get 
            {
                return (//new BitmapImage(new Uri(
                       APlaylist.Hintergrundmusik ? "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/copy.png" :
                                                   "pack://application:,,,/DSA MeisterGeister;component/Images/Icons/General/audio.png");
            }
        }
        

        public lbThemeItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                _suchtext = Name.ToLower() + Kategorie.ToLower();
                OnChanged();
            }

        }

        
        public bool Changed
        {
            get { return _changed; }
            set { _changed = value; }
        }
        
        public double TotalTimePlylist
        {
            get { return _totalTimePlylist; }
            set { _totalTimePlylist = value; }
        }
        
        public double Vol_PlaylistMod
        {
            get { return _vol_PlaylistMod; }
            set { _vol_PlaylistMod = value; }
        }

        public DateTime LastVolUpdate
        {
            get { return _lastVolUpdate; }
            set { _lastVolUpdate = value; }
        }
        
        public uint SollBtnGedrueckt
        {
            get { return _sollBtnGedrueckt; }
            set { _sollBtnGedrueckt = value; }
        }

        public Audio_Playlist APlaylist
        {
            get { return _aPlaylist; }
            set { _aPlaylist = value; }
        }

        public List<Audio_Playlist_Titel> APlaylistTitel
        {
            get { return _aPlaylistTitel; }
            set { _aPlaylistTitel = value; }
        }
        
        public int ObjGruppe
        {
            get { return _objGruppe; }
            set { _objGruppe = value; }
        }

        public UInt16 AnzVolChange
        {
            get { return _anzVolChange; }
            set { _anzVolChange = value; }
        }

        public UInt16 AnzPauseChange
        {
            get { return _anzPauseChange; }
            set { _anzPauseChange = value; }
        }

        public string PPlaylistName
        {
            get { return _playlistName; }
            set { _playlistName = value; }
        }

        public bool IstMusik
        {
            get { return _istMusik; }
            set { _istMusik = value; }
        }

        public List<MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile> ListZeile
        {
            get { return _listZeile; }
            set { _listZeile = value; }
        }

        public bool WirdAbgespielt
        {
            get { return _wirdAbgespielt; }
            set { _wirdAbgespielt = value; }
        }

        public List<Guid> NochZuSpielen
        {
            get { return _nochZuSpielen; }
            set { _nochZuSpielen = value; }
        }
        
        public  List<UInt16> Gespielt
        {
            get { return _gespielt; }
            set { _gespielt = value; }
        }

        public Nullable<double> Force_Volume
        {
            get { return _force_Volume; }
            set { _force_Volume = value; }
        }

        public bool visuell
        {
            get { return _visuell; }
            set
            {
                _visuell = value;
            }
        }

        public Type ItemType 
        {
            get { return APlaylist == null ? null : APlaylist.GetType(); }
        }

        public override string Name
        {
            get { return APlaylist == null || APlaylist.Name == null ? string.Empty : APlaylist.Name; }
        }

        public string Kategorie
        {
            get { return APlaylist == null || APlaylist.Kategorie == null ? string.Empty : APlaylist.Kategorie; }
        }

        //Commands
        public Base.CommandBase OnlbThemeAdd
        {
            get { return _onlbThemeItemAdd; }
        }

        
        #endregion


        #region //---- KONSTRUKTOR ----

        public lbThemeItem()
        {
            // Event-Handler zur DependentProperty-Notification
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;

            _onlbThemeItemAdd = new Base.CommandBase(lbThemeItemAdd, null);
        }
        #endregion

        #region //---- INSTANZMETHODEN ----


        /// <summary>
        /// Prüft, ob 'suchWort' im Namen, der Kategorie oder in den Tags vorkommt.
        /// </summary>
        /// <param name="suchWort"></param>
        /// <returns></returns>
        public bool Contains(string suchWort)
        {
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


        private Base.CommandBase _onBtnExportLbEditor = null;
        public Base.CommandBase OnBtnExportLbEditor
        {
            get
            {
                if (_onBtnExportLbEditor == null)
                    _onBtnExportLbEditor = new Base.CommandBase(BtnExportLbEditor, null);
                return _onBtnExportLbEditor;
            }
        }
        void BtnExportLbEditor(object obj)
        {
            try
            {
                string datei = ViewHelper.ChooseFile("Playliste exportieren", "Playlist_" + this.APlaylist.Name.Replace("/", "_") + ".xml", true, "xml");
                if (datei != null)
                {
                    Global.SetIsBusy(true, string.Format("Die Playlist wird exportiert ..."));

                    datei = datei.Replace("--", "-");
                    while (datei.EndsWith("-.xml") || datei.EndsWith(" .xml"))
                        datei = datei.Substring(0, datei.Length - 5) + ".xml";

                    File.Delete(datei);
                    this.APlaylist.Export(datei, this.APlaylist.Audio_PlaylistGUID);

                    Global.SetIsBusy(false);
                    ViewHelper.Popup("Die Playlist-Daten wurden erfolgreich gesichert.");
                }
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Exportieren der Playlist ist ein Fehler aufgetreten.", ex);
            }
        }

        
        /*
        private void InventarAdd(object sender)
        {
            if (InventarAddEvent != null)
                InventarAddEvent(this, new EventArgs());
        }

        public event EventHandler InventarAddEvent;

        #endregion

        #region //---- INotifyPropertyChanged Member ----

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        */


        private void lbThemeItemAdd(object sender)
        {
            if (lbThemeItemAddEvent != null)
                lbThemeItemAddEvent(this, new EventArgs());
        }

        public event EventHandler lbThemeItemAddEvent;

        #endregion


        //#region //---- INotifyPropertyChanged Member ----

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void OnChanged(String info)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(info));
        //    }
        //}

        //#endregion
    }

}
