using MeisterGeister.Logic.Einstellung;
using MeisterGeister.Logic.HeldenImport;
using MeisterGeister.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister.ViewModel.Helden
{
    public class DownloadHeldenViewModel : Base.ViewModelBase
    {
        private string _token;

        private ObservableCollection<HeldenSoftwareOnlineHeldViewModel> _helden;
        public ObservableCollection<HeldenSoftwareOnlineHeldViewModel> Helden
        {
            get
            {
                if (_helden == null)
                    _helden = new ObservableCollection<HeldenSoftwareOnlineHeldViewModel>();
                return _helden;
            }
            set
            {
                if (_helden != value)
                    Set(ref _helden, value);
            }
        }

        public DownloadHeldenViewModel(string token)
        {
            this._token = token;
            GetHeldenListe();
        }

        public void GetHeldenListe()
        {
            var syncer = new HeldenSoftwareOnlineService(_token);

            HeldenListe heldenListe = null;
            try
            {
                heldenListe = syncer.GetHeldenListe();
            }
            catch (Exception ex)
            {
                string msg = "Beim Aufruf der HeldenSoftware-Online ist ein Fehler aufgetreten.";
                var errWin = new MsgWindow("Abruf Heldenliste", msg, ex);
                errWin.ShowDialog();
                errWin.Close();
            }

            if(heldenListe != null)
            {
                Helden.Clear();
                foreach (HeldElement held in heldenListe.Helden)
                {
                    Helden.Add(new HeldenSoftwareOnlineHeldViewModel(held));
                }
            }
        }

        private bool _canDownloadHelden = true;
        public bool CanDownloadHelden(object args)
        {
            return _canDownloadHelden;
        }

        private Base.CommandBase _downloadHeldenCommand;
        public Base.CommandBase DownloadHeldenCommand
        {
            get
            {
                if (_downloadHeldenCommand == null)
                    _downloadHeldenCommand = new Base.CommandBase(DownloadHelden, CanDownloadHelden);
                return _downloadHeldenCommand;
            }
        }

        public void DownloadHelden(object arg)
        {
            var syncer = new HeldenSoftwareOnlineService(_token);
            syncer.ProgressChanged += Syncer_ProgressChanged;
            try
            {
                _canDownloadHelden = false;
                ICollection<HeldenImportResult> result = syncer.DownloadHelden();
                _canFinish = true;
            }
            catch (Exception ex)
            {
                string msg = "Beim Aufruf der HeldenSoftware-Online ist ein Fehler aufgetreten.";
                var errWin = new MsgWindow("Download Helden", msg, ex);
                errWin.ShowDialog();
                errWin.Close();
            }

            // Asynchroner Download aktuell leider nicht möglich wegen Singleton-DataContext
            //var worker = syncer.DownloadHeldenAsync();
            //worker.ProgressChanged += DownloadProgressed;
            //worker.RunWorkerCompleted += DownloadCompleted;
        }

        private void Syncer_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            HeldElement held = e.UserState as HeldElement;
            if(held != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    HeldenSoftwareOnlineHeldViewModel heldenOnlineHeld = Helden.Single(h => h.HeldenKey == held.HeldenKey);
                    heldenOnlineHeld.IsLoaded = true;
                }, System.Windows.Threading.DispatcherPriority.Background);
            }
        }

        private bool _canFinish = true;
        public bool CanFinish(object args)
        {
            return _canFinish;
        }

        private Base.CommandBase _finishCommand;
        public Base.CommandBase FinishCommand
        {
            get
            {
                if (_finishCommand == null)
                    _finishCommand = new Base.CommandBase(Finish, CanFinish);
                return _finishCommand;
            }
        }

        private void Finish(object obj)
        {
            DialogResult = true;
        }

        private bool? _dialogResult;
        public bool? DialogResult
        {
            get
            {
                return _dialogResult;
            }
            set
            {
                if (_dialogResult != value)
                {
                    Set(ref _dialogResult, value);
                }
            }
        }
    }
}
