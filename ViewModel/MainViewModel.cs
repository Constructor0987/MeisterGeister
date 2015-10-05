using MeisterGeister.Daten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.ViewModel
{
    public class MainViewModel : Base.ViewModelBase
    {
        public MainViewModel()
        {

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
    }
}
