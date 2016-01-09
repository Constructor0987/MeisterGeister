using MeisterGeister.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Logic.HeldenImport
{
    public class HeldenImportResult
    {
        public string ImportPfad { get; set; }

        public List<string> ImportLogs { get; set; }

        public Held Held { get; set; }
    }
}
