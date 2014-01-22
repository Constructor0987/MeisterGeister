using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Data.Objects.DataClasses;
using System.Data.Entity;

namespace MeisterGeister.Model.Extensions
{
    static class SqlFunctionsExtension
    {
        [DbFunctionAttribute("DatabaseDSAModel.Store", "upper")]
        public static string StringConvert(this Guid guid)
        {
            return guid.ToString().ToUpperInvariant();
        }
    }
}
