using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.NscGenerator.Logic
{
    class Person : PersonNurName
    {
        public Person(string name, string namensbedeutung, string namenstyp,  Geschlecht geschlecht, Stand stand) 
            : base(name, namensbedeutung, namenstyp, geschlecht, stand)
        {

        }
    }

    
}
