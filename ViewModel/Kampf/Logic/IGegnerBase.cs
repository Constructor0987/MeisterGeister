using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Kampf.Logic
{
    public interface IGegnerBase
    {
        Guid GegnerBaseGUID { get;  }
        string Name { get;  }
        string Bild { get;  }
        int INIBasis { get;  }
        string INIZufall { get;  }
        int Aktionen { get;  }
        int PA { get;  }
        int LE { get;  }
        int AU { get;  }
        int AE { get;  }
        int KE { get;  }
        int KO { get;  }
        int MRGeist { get;  }
        int? MRKörper { get;  }
        double GS { get;  }
        double? GS2 { get; }
        double? GS3 { get; }
        int RSKopf { get;  }
        int RSBrust { get;  }
        int RSRücken { get;  }
        int RSArmL { get;  }
        int RSArmR { get;  }
        int RSBauch { get;  }
        int RSBeinL { get;  }
        int RSBeinR { get;  }
        int? GW { get;  }
        int? Jagd { get;  }
        int? Beschwörung { get;  }
        int? Kontrolle { get;  }
        int? Beschwörungskosten { get;  }
        string Tags { get;  }
        string Bemerkung { get;  }
        string Literatur { get;  }
        string Setting { get;  }
    }
}
