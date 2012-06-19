using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeisterGeister.ViewModel.Abenteuer
{
    public class AbenteuerViewModel
    {
        private int m_Nummer;
        private string m_Name;
        //private IEnumerable<Szene> m_Szenen;

        public AbenteuerViewModel(int nummer, string name)
        {
            m_Nummer = nummer;
            m_Name = name;
        }

        public int Nummer 
        {
            get { return m_Nummer; }
        }

        public string Name 
        {
            get { return m_Name; }
        }
    }
}
