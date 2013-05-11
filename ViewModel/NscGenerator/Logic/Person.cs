using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.NscGenerator.Logic
{
    class Person : PersonNurName
    {
        public Person(String name, String namensbedeutung, Geschlecht geschlecht, Model.Kultur kultur, Model.Rasse rasse) : base(name, namensbedeutung, geschlecht, kultur, rasse)
        {

        }
    }

    
}
