using MeisterGeister.Daten;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister.ViewModel
{
    public class MainViewModel : Base.ViewModelBase
    {
        public MainViewModel()
        {
            App.Queue.ProgressChanged += Queue_ProgressChanged;
        }

        public string VersionInfo
        {
            get
            {
                string version = string.Format("V {0} / {1}", App.GetVersionString(App.GetVersionProgramm()), DatabaseUpdate.DatenbankVersionAktuell);
#if TEST
                version += " Test";
#endif
                if (Global.INTERN)
                    version += " Intern";
                return version;
            }
        }

        public string RegeleditionNummer
        {
            get
            {
                return Global.RegeleditionNummer;
            }
        }

        private int currentProgress;
        public int CurrentProgress
        {
            get
            {
                return this.currentProgress;
            }
            set
            {
                if (this.currentProgress != value)
                {
                    this.currentProgress = value;
                    OnChanged("CurrentProgress");
                }
            }
        }

        private string currentUserState;
        public string CurrentUserState
        {
            get
            {
                return this.currentUserState;
            }
            set
            {
                if (this.currentUserState != value)
                {
                    this.currentUserState = value;
                    OnChanged("CurrentUserState");
                }
            }
        }

        private Visibility progressBarVisibility;
        public Visibility ProgressBarVisibility
        {
            get { return progressBarVisibility; }
            set
            {
                if (value != progressBarVisibility)
                {
                    progressBarVisibility = value;
                    OnChanged("ProgressBarVisibility");
                }
            }
        }

        private void Queue_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 100)
            {
                Thread.Sleep(1000);
                ProgressBarVisibility = Visibility.Hidden;
            }
            else
                ProgressBarVisibility = Visibility.Visible;
            this.CurrentProgress = e.ProgressPercentage;
            this.CurrentUserState = e.UserState.ToString();
        }
    }
}
