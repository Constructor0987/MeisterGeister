using System;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using System.Threading;
// Eigene Usings
using MeisterGeister.Model;
using MeisterGeister.Model.Extensions;
using MeisterGeister.View.AudioPlayer;
using MeisterGeister.ViewModel.AudioPlayer;
using MeisterGeister.ViewModel.AudioPlayer.Logic;
using MeisterGeister.View.General;
using MeisterGeister.Logic.Einstellung;
using System.Windows.Media;

namespace MeisterGeister.ViewModel.AudioPlayer.Logic
{
    
    public class MusikZeileVM : Base.ToolViewModelBase
    {
        #region //---- FELDER ----

        private bool InAnderemPfadSuchen = Einstellungen.AudioInAnderemPfadSuchen;
        private MusikZeileModel _model;
        private BackgroundWorker _worker;
        private int _iterations = 50;
        private string _output;
        private bool _startEnabled = true;
        private bool _cancelEnabled = false;

        private lbThemeItemVM _item = null;
        private string _suchtext = string.Empty;

        private bool _changed = false;
	    private double _totalTimePlylist = 0;    
	    private double _vol_PlaylistMod = 0;
	    private DateTime _lastVolUpdate = DateTime.Now;
	    private uint _sollBtnGedrueckt = 0;
	    private int _objGruppe;
	    private UInt16 _anzVolChange = 0;
	    private UInt16 _anzPauseChange;
	    private string _playlistName = "";
	    private bool _istMusik = true;
        private List<MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile> _listZeile = new List<MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile>();
        private Nullable<bool> _min1SongWirdGespielt = null;
	    private List<Guid> _nochZuSpielen = new List<Guid>();
	    private List<UInt16> _gespielt = new List<UInt16>();
	    private Nullable<double> _force_Volume = null;
	    private bool _visuell = true;

        public DispatcherTimer wartezeitTimer = new DispatcherTimer();
        public Nullable<int> posObjGruppe;

        private string _liste = null;
        public string Liste
        {
            get { return _liste; }
            set
            {
                _liste = value;
                OnChanged();
            }
        }

        private MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt _grpobj = null;
        public MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt grpobj
        {
            get { return _grpobj; }
            set
            {
                _grpobj = value;
                OnChanged();
            }
        }
        
        private MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel _aPlayerVM = null;
        public MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel aPlayerVM
        {
            get { return _aPlayerVM; }
            set
            {
                _aPlayerVM = value;
                OnChanged();
            }
        }

        //Commands
        private Base.CommandBase _onlbThemeItemAdd;        

        #endregion
                        
        #region //---- EIGENSCHAFTEN ----


        private bool _teilAbspielbar;
        public bool TeilAbspielbar
        {
            get { return _teilAbspielbar; }
            set { Set(ref _teilAbspielbar, value); }
        }

        private bool _teilAbspielbarGecheckt;
        public bool TeilAbspielbarGecheckt
        {
            get { return _teilAbspielbarGecheckt; }
            set { Set(ref _teilAbspielbarGecheckt, value); }
        }

        private bool _showLänge;
        public bool ShowLänge
        {
            get { return _showLänge; }
            set { Set(ref _showLänge, value); }
        }

        private bool _asGeräuscheMusikZeile = false;
        public bool asGeräuscheMusikZeile
        {
            get { return _asGeräuscheMusikZeile; }
            set
            {
                Set(ref _asGeräuscheMusikZeile, value);
                ShowLänge = !asGeräuscheMusikZeile || asGeräuscheMusikZeile && GroßeAnsicht;
            }
        }

        private bool _großeAnsicht = true;
        public bool GroßeAnsicht
        {
            get { return _großeAnsicht; }
            set
            {
                Set(ref _großeAnsicht, value);
                ShowLänge = !asGeräuscheMusikZeile || asGeräuscheMusikZeile && GroßeAnsicht;
            }
        }

        private Audio_Playlist _aPlaylist;
        public Audio_Playlist aPlaylist
        {
            get { return _aPlaylist; }
            set 
            {
                if (value != null && _aPlaylist == null)
                {
                    PlaylistDoForce = value.DoForce;
                    PlaylistForceVolume = value.ForceVolume;
                }

                Set(ref _aPlaylist, value);
                asGeräuscheMusikZeile = !_aPlaylist.Hintergrundmusik;
                OnChanged("asGeräuscheMusikZeile");

            }
        }
        
        public lbThemeItemVM Item
        {
            get { return _item; }
            set
            {
                Set(ref _item, value);
                _suchtext = Name.ToLower() + Kategorie.ToLower();
            }

        }
        
        private bool _playlistDoForce;
        public bool PlaylistDoForce
        {
            get { return _playlistDoForce; }
            set
            {
                Set(ref _playlistDoForce, value);
                if (aPlaylist != null)
                {
                    aPlaylist.DoForce = value;
                    grpobj.DoForceVolume = value;
                }
            }
        }

        private int _playlistForceVolume;
        public int PlaylistForceVolume
        {
            get { return _playlistForceVolume; }
            set
            {
                Set(ref _playlistForceVolume, value);
                if (aPlaylist != null)
                {
                    aPlaylist.ForceVolume = value;
                    grpobj.force_Volume = (double)value / 100;
                }
            }
        }
        
        public bool Changed
        {
            get { return _changed; }
            set { Set(ref _changed, value); }
        }
        
        public double TotalTimePlylist
        {
            get { return _totalTimePlylist; }
            set { Set(ref _totalTimePlylist, value); }
        }
        
        public double Vol_PlaylistMod
        {
            get { return _vol_PlaylistMod; }
            set { Set(ref _vol_PlaylistMod, value); }
        }

        public DateTime LastVolUpdate
        {
            get { return _lastVolUpdate; }
            set { Set(ref _lastVolUpdate, value); }
        }
        
        public uint SollBtnGedrueckt
        {
            get { return _sollBtnGedrueckt; }
            set { Set(ref _sollBtnGedrueckt, value); }
        }
        
        public int ObjGruppe
        {
            get { return _objGruppe; }
            set { Set(ref _objGruppe, value); }
        }

        public UInt16 AnzVolChange
        {
            get { return _anzVolChange; }
            set { Set(ref _anzVolChange, value); }
        }

        public UInt16 AnzPauseChange
        {
            get { return _anzPauseChange; }
            set { Set(ref _anzPauseChange, value); }
        }
                
        public string PPlaylistName
        {
            get { return _playlistName; }
            set { Set(ref _playlistName, value); }
        }

        public bool IstMusik
        {
            get { return _istMusik; }
            set { Set(ref _istMusik, value); }
        }

        public List<MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile> ListZeile
        {
            get { return _listZeile; }
            set { Set(ref _listZeile, value); }
        }

        public Nullable<bool> Min1SongWirdGespielt
        {
            get { return _min1SongWirdGespielt; }
            set { Set(ref _min1SongWirdGespielt, value); }
        }

        public List<Guid> NochZuSpielen
        {
            get { return _nochZuSpielen; }
            set { Set(ref _nochZuSpielen, value); }
        }
        
        public  List<UInt16> Gespielt
        {
            get { return _gespielt; }
            set { Set(ref _gespielt, value); }
        }

        public Nullable<double> Force_Volume
        {
            get { return _force_Volume; }
            set { Set(ref _force_Volume, value); }
        }

        public bool visuell
        {
            get { return _visuell; }
            set { Set(ref _visuell, value); }
        }

        public Type ItemType 
        {
            get { return aPlaylist == null ? null : aPlaylist.GetType(); }
        }

        public string Name
        {
            get { return aPlaylist == null || aPlaylist.Name == null ? string.Empty : aPlaylist.Name; }
        }

        public string Kategorie
        {
            get { return aPlaylist == null || aPlaylist.Kategorie == null ? string.Empty : aPlaylist.Kategorie; }
        }

        //Commands
        public Base.CommandBase OnlbThemeAdd
        {
            get { return _onlbThemeItemAdd; }
        }

        
        #endregion

        #region //---- KONSTRUKTOR ----

        public MusikZeileVM()
        {
            // Event-Handler zur DependentProperty-Notification
            PropertyChanged += DependentProperty.PropagateINotifyProperyChanged;

            _onlbThemeItemAdd = new Base.CommandBase(lbThemeItemAdd, null);
            Iterations = aPlaylist != null? aPlaylist.Audio_Playlist_Titel.Count: 0;
                       
            _worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _worker.DoWork += worker_DoWork;
            _worker.ProgressChanged += worker_ProgressChanged;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            //_timerReadAgain.Tick += new EventHandler(_timerReadAgain_Tick);
            //_timerReadAgain.Interval = new TimeSpan(0, 0, 0, 1, 0);
        }
        #endregion


        #region //---- INSTANZMETHODEN ----

        //Commands
        private Base.CommandBase _ontbtnCheckChecked;
        public Base.CommandBase OntbtnCheckChecked
        {
            get
            {
                if (_ontbtnCheckChecked == null)
                    _ontbtnCheckChecked = new Base.CommandBase(tbtnCheckChecked, null);
                return _ontbtnCheckChecked;
            }
        }
        public void tbtnCheckChecked(object obj)
        {
           // _timerReadAgain.Interval = new TimeSpan(0, 0, 0, 1, 0);
            //_anzGefunden = 0;
            //_anzLastScan = 0;           
            MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.GruppenObjekt inGrpObject = 
                aPlayerVM._GrpObjecte.Where(t => !t.visuell).FirstOrDefault(t => t.aPlaylist.Audio_PlaylistGUID == aPlaylist.Audio_PlaylistGUID);

            if (grpobj.aPlaylist != null &&
                (inGrpObject == null || 
                posObjGruppe == null))
            {
                aPlayerVM.tiErstellt++;
                grpobj.objGruppe = Convert.ToInt16(aPlayerVM.tiErstellt);
                
                grpobj.visuell = false;
                grpobj.force_Volume = (double)grpobj.aPlaylist.ForceVolume / 100;
                grpobj.DoForceVolume = grpobj.aPlaylist.DoForce;

                grpobj.NochZuSpielen.Clear();
                grpobj.Gespielt.Clear();
                bool neueKlangZeilen = grpobj._listZeile == null || grpobj._listZeile.Count == 0;

                foreach (Audio_Playlist_Titel aPlaylistTitel in grpobj.aPlaylist.Audio_Playlist_Titel)
                {
                    if (aPlaylistTitel.Audio_Titel != null)
                    {
                        if (neueKlangZeilen) 
                            aPlayerVM.KlangNewRow(grpobj, aPlaylistTitel);

                        if (aPlaylistTitel.Aktiv &&
                            !grpobj.NochZuSpielen.Contains(aPlaylistTitel.Audio_TitelGUID))
                        {
                            for (int i = 0; i <= aPlaylistTitel.Rating; i++)
                                grpobj.NochZuSpielen.Add(aPlaylistTitel.Audio_TitelGUID);
                        }
                    }
                }
                grpobj.mZeileVM = this;
                aPlayerVM._GrpObjecte.Add(grpobj);
                posObjGruppe = aPlayerVM._GrpObjecte.Count - 1;
            }
            else
            {
                grpobj._listZeile.FindAll(t => !t.playable).ForEach(delegate(
                    MeisterGeister.ViewModel.AudioPlayer.AudioPlayerViewModel.KlangZeile kZeile) 
                    { 
                        kZeile.playable = true;
                        kZeile.playMediaFailed = 0;
                    });
            }

            if (grpobj.aPlaylist.DoForce)
                grpobj.force_Volume = (double)grpobj.aPlaylist.ForceVolume / 100;
            else
                grpobj.force_Volume = null;

            if (!grpobj.wirdAbgespielt)
            {
                grpobj.wirdAbgespielt = true;
                //WARTEZEIT DER PLAYLISTE EINBAUEN
                grpobj.wartezeitTimer.Tag = grpobj;
                if (!grpobj.aPlaylist.WarteZeitAktiv)
                    wartezeitTimer_Tick(grpobj.wartezeitTimer, new EventArgs());
                else
                    if (grpobj.aPlaylist.WarteZeitAktiv)
                    {
                        if (grpobj.wartezeitTimer.IsEnabled)
                            grpobj.wartezeitTimer.Stop();

                        grpobj.wartezeitTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)grpobj.aPlaylist.WarteZeit);
                        grpobj.wartezeitTimer.Start();
                        grpobj.mZeileVM.Min1SongWirdGespielt = false;

                        //RANDOM neue Wartezeit bevor Playlist spielt
                        if (grpobj.aPlaylist.WarteZeitChange)
                        {
                            grpobj.aPlaylist.WarteZeit = (new Random()).Next((int)grpobj.aPlaylist.WarteZeitMin, (int)grpobj.aPlaylist.WarteZeitMax);
                            Global.ContextAudio.Update<Audio_Playlist>(grpobj.aPlaylist);
                        }
                    }
            }
            aPlayerVM.ErwPlayerGeräuscheAktiv = true;
        }

        public void wartezeitTimer_Tick(object sender, EventArgs e)
        {
            if (grpobj == null)
                return;

            for (int i = 0; i < grpobj._listZeile.Count; i++)
            {
                grpobj._listZeile[i].istPause = false;

                if (!grpobj._listZeile[i].istLaufend && grpobj._listZeile[i].aPlaylistTitel.Aktiv)
                    grpobj._listZeile[i].istStandby =
                        (!grpobj._listZeile[i].istPause && !grpobj._listZeile[i].istLaufend && grpobj._listZeile[i].aPlaylistTitel.Aktiv) ? true : false;
            }
            aPlayerVM.CheckPlayStandbySongs(grpobj);

            if (!grpobj.aPlaylist.Hintergrundmusik && grpobj.aPlaylist.Fading)
                aPlayerVM.FadingInGeräusch(grpobj);
            ((DispatcherTimer)sender).Stop();
        }

        private Base.CommandBase _ontbtnCheckUnChecked;
        public Base.CommandBase OntbtnCheckUnChecked
        {
            get
            {
                if (_ontbtnCheckUnChecked == null)
                    _ontbtnCheckUnChecked = new Base.CommandBase(tbtnCheckUnChecked, null);
                return _ontbtnCheckUnChecked;
            }
        }
        public void tbtnCheckUnChecked(object obj)
        {
            if (grpobj.wartezeitTimer.IsEnabled)
                grpobj.wartezeitTimer.Stop();
            grpobj.wartezeitTimer.Tag = null;
            System.Diagnostics.Debug.WriteLine("1");
            if (grpobj != null)// && grpobj.wirdAbgespielt)
            {
                if (grpobj == null)
                    return;
                System.Diagnostics.Debug.WriteLine("2");
                grpobj.wirdAbgespielt = false;

                if (!grpobj.aPlaylist.Hintergrundmusik && grpobj.aPlaylist.Fading)
                {
                    System.Diagnostics.Debug.WriteLine("3");
                    aPlayerVM.FadingOutGeräusch(true, true, grpobj);

                    System.Diagnostics.Debug.WriteLine("4");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("5");
                    if (!grpobj.aPlaylist.Hintergrundmusik && !grpobj.aPlaylist.Fading)
                    {
                        System.Diagnostics.Debug.WriteLine("6");
                        for (int i = 0; i < grpobj.mZeileVM.grpobj._listZeile.Count; i++)
                        {
                            grpobj.mZeileVM.grpobj._listZeile[i].istPause = true;
                            grpobj.mZeileVM.grpobj._listZeile[i].aData.Pause();
                            grpobj.mZeileVM.grpobj._listZeile[i].istStandby = false;
                            grpobj.mZeileVM.grpobj._listZeile[i].istLaufend = false;
                            grpobj.mZeileVM.grpobj._listZeile[i].aData.Stop();
                            grpobj.mZeileVM.grpobj._listZeile[i].aData.Close();
                        }
                        //grpobj.mZeileVM.grpobj._listZeile.All(t => t.istPause = true);
                        //grpobj.mZeileVM.grpobj._listZeile.All(t => t.aData.Pause());
                        //grpobj.mZeileVM.grpobj._listZeile.All(t => t.istStandby = true); //true ?
                        //grpobj.mZeileVM.grpobj._listZeile.All(t => t.istLaufend = false);
                        //aPlayerVM.CheckPlayStandbySongs(grpobj);
                        //grpobj.mZeileVM.grpobj._listZeile.All(t => t.aData.Stop());
                        //grpobj.mZeileVM.grpobj._listZeile.All(t => t.aData.Close());
                        grpobj.mZeileVM.Min1SongWirdGespielt = null;
                        aPlayerVM._GrpObjecte.Remove(grpobj);
                        System.Diagnostics.Debug.WriteLine("7");
                        aPlayerVM.CheckPlayStandbySongs(grpobj);
                        System.Diagnostics.Debug.WriteLine("7.1");
                        return;
                    }
                    System.Diagnostics.Debug.WriteLine("8");
                }
                for (int i = 0; i < grpobj._listZeile.Count; i++)
                {
                    if (!grpobj.aPlaylist.Hintergrundmusik)
                    {
                        System.Diagnostics.Debug.WriteLine("9_"+ i.ToString());
                        if (grpobj._listZeile[i].aData != null && !grpobj.aPlaylist.Fading)
                        {
                            System.Diagnostics.Debug.WriteLine("10_" + i.ToString());
                            grpobj._listZeile[i].istPause = true;
                            grpobj._listZeile[i].aData.Pause();
                            grpobj._listZeile[i].istStandby = false;
                            grpobj._listZeile[i].istLaufend = false;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("11_" + i.ToString());
                        grpobj._listZeile[i].istPause = true;
                        if (!grpobj._listZeile[i].FadingOutStarted && grpobj._listZeile[i].istLaufend)
                        {
                            System.Diagnostics.Debug.WriteLine("12_" + i.ToString());
                            grpobj._listZeile[i].FadingOutStarted = true;
                            aPlayerVM.FadingOut(grpobj._listZeile[i], grpobj, true, true);

                            grpobj._listZeile[i].istLaufend = false;
                            if (grpobj._listZeile[i].audioZeileVM != null) grpobj._listZeile[i].audioZeileVM.Progress = 0;
                            grpobj._listZeile[i].istStandby = true;
                            System.Diagnostics.Debug.WriteLine("13_" + i.ToString());
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine("14");
                aPlayerVM.CheckPlayStandbySongs(grpobj);
                grpobj.totalTimePlylist = -1;
            }
            System.Diagnostics.Debug.WriteLine("15");
            aPlayerVM.ErwPlayerGeräuscheAktiv = aPlayerVM.ErwPlayerGeräuscheAktiv;
        }



        #region Bindable Properties

        private List<string> _stdPfad = new List<string>();
        public List<string> StdPfad
        {
            get { return _stdPfad; }
            set { Set(ref _stdPfad, value); }
        }

        public int Iterations
        {
            get { return _iterations; }
            set { Set(ref _iterations, value); }
        }

        private double _fadingPercentage = 0;
        public double FadingPercentage
        {
            get { return _fadingPercentage; }
            set
            {
                if (_fadingPercentage != value * .60)
                {
                    _fadingPercentage = value * .60;
                    OnChanged();
                }
            }
        }
        private double _readPercentage = 0;
        public double ReadPercentage
        {
            get { return _readPercentage; }
            set
            {
                if (_readPercentage != value * .60)
                {
                    _readPercentage = value * .60;
                    OnChanged();
                }
            }
        }
        
        public string Output
        {
            get { return _output; }
            set { Set(ref _output, value); }
        }
        public bool StartEnabled
        {
            get { return _startEnabled; }
            set { Set(ref _startEnabled, value); }
        }

        public bool CancelEnabled
        {
            get { return _cancelEnabled; }
            set { Set(ref _cancelEnabled, value); }
        }

        #endregion


        #region Public Methods

        public void StartProcess()
        {
            Iterations = aPlaylist != null ? aPlaylist.Audio_Playlist_Titel.Count : 0;
            Output = "";
            if (!_worker.IsBusy)
            {
                if (_model == null)
                    _model = new MusikZeileModel(Iterations);
                else
                    _model.Iterations = Iterations;
                _worker.RunWorkerAsync(_model);
            }
            StartEnabled = !_worker.IsBusy;
            CancelEnabled = _worker.IsBusy;
        }

        public void CancelProcess()
        {
            _worker.CancelAsync();
        }

        #endregion

        #region BackgroundWorker Events

        // Note: This event fires on the background thread.
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            var model = (MusikZeileModel)e.Argument;
            Output = "";
            
            int result = 0;
                        
            foreach (var val in model)
            {
                model.APlaylistTitel = aPlaylist.Audio_Playlist_Titel.ElementAt(val-1);
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                if (worker.WorkerReportsProgress)
                {
                    MyTimer.start_timer();
                    SpinWait.SpinUntil(() => { return model.APlaylistTitel.Audio_Titel != null; }, 1000);
                    MyTimer.stop_timer("Worker_DoWork");
                    if (model.APlaylistTitel.Audio_Titel != null)
                    {
                        if (!File.Exists(model.APlaylistTitel.Audio_Titel.Pfad + "\\" + model.APlaylistTitel.Audio_Titel.Datei))
                        {
                            Audio_Titel titel = setTitelStdPfad(model.APlaylistTitel.Audio_Titel);
                            if (File.Exists(titel.Pfad + "\\" + titel.Datei))
                            {
                                Global.ContextAudio.Update<Audio_Titel>(titel);
                                model.APlaylistTitel.Audio_Titel = titel;
                                result++;
                            }
                        }
                        else
                            result++;
                    }
                    int percentComplete = (int)((float)val / (float)model.Iterations * 100);
                    string updateMessage =
                        string.Format("Titel {0} von {1} überprüft", val, model.Iterations);
                    worker.ReportProgress(percentComplete, updateMessage);
                }
                //result = val;
            }
            e.Result = result;
        }

        // Note: This event fires on the UI thread.
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Output = e.Error.Message;
            }
            else if (e.Cancelled)
            {
                Output = "Abgebrochen";
            }
            else
            {
                if ((int)e.Result != Iterations || (int)e.Result == 0)
                {
                    if (Output == "" || Output.StartsWith("Titel"))
                        Output = e.Result.ToString() + " von " + Iterations + " gefunden";   
                    TeilAbspielbar = true;

                    //Bis 5 mal überprüfen wenn gleiches Scan-Resultat
                    //if (_anzLastScan == (int)e.Result) 
                    //    _anzGefunden++;
                    //if (_anzGefunden < 5)
                    //    _timerReadAgain.Start();
                    //_anzLastScan = (int)e.Result;
                }
                else
                {
                    if (Output == "" || Output.StartsWith("Titel"))
                        Output = "Alle " + e.Result.ToString() + " von " + Iterations + " gefunden";
                    TeilAbspielbar = false;
                    TeilAbspielbarGecheckt = true;
                }
                ReadPercentage = 0;
            }
            StartEnabled = !_worker.IsBusy;
            CancelEnabled = _worker.IsBusy;
            OnChanged("TeilAbspielbar");
            _worker.Dispose();
        }

        // Note: This event fires on the UI thread.
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ReadPercentage = e.ProgressPercentage;
            Output = (string)e.UserState;
        }

        #endregion



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
                string datei = ViewHelper.ChooseFile("Playliste exportieren", "Playlist_" + this.aPlaylist.Name.Replace("/", "_") + ".xml", true, "xml");
                if (string.IsNullOrEmpty(datei)) return;

                Global.SetIsBusy(true, string.Format("Die Playlist wird exportiert ..."));

                datei = datei.Replace("--", "-");
                while (datei.EndsWith("-.xml") || datei.EndsWith(" .xml"))
                    datei = datei.Substring(0, datei.Length - 5) + ".xml";

                File.Delete(datei);
                this.aPlaylist.Export(datei, this.aPlaylist.Audio_PlaylistGUID);

                Global.SetIsBusy(false);
                ViewHelper.Popup("Die Playlist-Daten wurden erfolgreich gesichert.");
            }
            catch (Exception ex)
            {
                Global.SetIsBusy(false);
                ViewHelper.ShowError("Allgmeiner Fehler" + Environment.NewLine + "Beim Exportieren der Playlist ist ein Fehler aufgetreten.", ex);
            }
        }

        
        private void lbThemeItemAdd(object sender)
        {
            if (lbThemeItemAddEvent != null)
                lbThemeItemAddEvent(this, new EventArgs());
        }

        public event EventHandler lbThemeItemAddEvent;

        #endregion

        public Audio_Titel setTitelStdPfad(Audio_Titel aTitel)
        {
            char[] charsToTrim = { '\\' };
            //Check Titel -> Pfad vorhanden ansonsten Standard-Pfad hinzufügen
            if (//System.IO.Path.IsPathRooted(System.IO.Path.GetDirectoryName(aTitel.Pfad + "\\" + aTitel.Datei)) &&
                File.Exists(aTitel.Pfad + "\\" + aTitel.Datei))
            {                
                foreach (string pfad in _stdPfad)
                {
                    if (pfad == aTitel.Pfad)
                        return aTitel;

                    if (aTitel.Pfad != null && (aTitel.Pfad + "\\" + aTitel.Datei).Contains(pfad))
                    {
                        aTitel.Datei = (aTitel.Pfad.EndsWith("\\") ? aTitel.Pfad + aTitel.Datei : aTitel.Pfad + "\\" + aTitel.Datei).
                            Substring(pfad.EndsWith("\\") ? pfad.Length : pfad.Length + 1);
                        aTitel.Pfad = pfad.TrimEnd(charsToTrim);
                        return aTitel;
                    }
                }
                // Pfad noch kein Standard-Pfad
                if (ViewHelper.Confirm("Audio-Pfad ist kein Standard-Pfad", "Der Pfad der Audio-Datei konnte nicht unter den Standard-Pfaden gefunden werden." +
                    Environment.NewLine + "In dieser Konstellation ist es nicht zulässig, den Titel abzuspielen." + Environment.NewLine +
                    "Soll der Pfad mit in die Standard-Pfade integriert werden?" + Environment.NewLine + Environment.NewLine + "Neuer Pfad:     " + aTitel.Pfad))
                {
                    MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis =
                        MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis + "|" + aTitel.Pfad;
                    setStdPfad();
                }
                return aTitel;
            }

            //Pfad+Titel nicht gefunden -> Check Titel in einem anderen Standard-Pfad
            foreach (string pfad in StdPfad)
            {
                if (aTitel.Datei == null && aTitel.Pfad != null)
                {
                    aTitel.Datei = aTitel.Pfad;
                    aTitel.Pfad = "";
                }
                if (File.Exists(pfad.TrimEnd(charsToTrim) + "\\" + aTitel.Datei))
                {
                    aTitel.Pfad = pfad.TrimEnd(charsToTrim);
                    return aTitel;
                }

                if (File.Exists(pfad.TrimEnd(charsToTrim) + "\\" + System.IO.Path.GetFileName(aTitel.Datei)))
                {
                    aTitel.Pfad = pfad.TrimEnd(charsToTrim);
                    aTitel.Datei = System.IO.Path.GetFileName(aTitel.Datei);
                    return aTitel;
                }
            }

            if (InAnderemPfadSuchen &&
                StdPfad.Count > 0)
            {
                //ab hier: kein Std.-Pfad ist gültig -> Check in jedem Std.-Pfad mit Suche incl. Unterverzeichnisse nach dem Dateinamen                
                string gesuchteDatei = System.IO.Path.GetFileName(StdPfad[0].TrimEnd(charsToTrim) + "\\" + aTitel.Datei);
                foreach (string pfad in StdPfad)
                {
                    if (pfad != "C:\\" && Directory.Exists(pfad))
                    {
                        string[] pfad_datei = Directory.GetFiles(pfad.TrimEnd(charsToTrim), gesuchteDatei, SearchOption.AllDirectories);
                        if (pfad_datei.Length > 0)
                        {
                            aTitel.Pfad = System.IO.Path.GetDirectoryName(pfad_datei[0]);
                            aTitel.Datei = System.IO.Path.GetFileName(pfad_datei[0]);
                            aTitel = setTitelStdPfad(aTitel);
                            return aTitel;
                        }
                    }
                }
            }

            if (aTitel.Pfad == null) aTitel.Pfad = "";
            if (aTitel.Pfad == "" || aTitel.Datei == null)
            {
                string pfadDatei = aTitel.Pfad != null || aTitel.Pfad != "" ? aTitel.Pfad : "";
                if (pfadDatei != "" && !pfadDatei.EndsWith("\\"))
                    pfadDatei = pfadDatei + "\\";
                if (aTitel.Datei != null)
                    pfadDatei = pfadDatei + aTitel.Datei;

                aTitel.Pfad = System.IO.Path.GetDirectoryName(pfadDatei);
                aTitel.Datei = System.IO.Path.GetFileName(pfadDatei);
            }

            return aTitel;
        }

        public void setStdPfad()
        {
            char[] charsToTrim = { '\\' };
            if (StdPfad.Count > 0) StdPfad.RemoveRange(0, StdPfad.Count);
            StdPfad.AddRange(MeisterGeister.Logic.Einstellung.Einstellungen.AudioVerzeichnis.Split(new Char[] { '|' }));
        }
    }

}
