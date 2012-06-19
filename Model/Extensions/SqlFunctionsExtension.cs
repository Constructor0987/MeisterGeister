using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace MeisterGeister.Model.Extensions
{
    static class SqlFunctionsExtension
    {
        [EdmFunction("DatabaseDSAModel.Store", "upper")]
        public static string StringConvert(this Guid guid)
        {
            return guid.ToString().ToUpperInvariant();
        }
    }
}
