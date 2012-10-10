using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace MeisterGeister.View.General
{
    public class WürfelValidationRule : ValidationRule
    {
        private string _errorMessage = "Kein gültiger Würfelausdruck.";
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public override ValidationResult Validate(object value,
            CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            string inputString = (value ?? string.Empty).ToString();
            if (!Logic.General.Würfel.Validate(inputString))
            {
                result = new ValidationResult(false, this.ErrorMessage);
            }
            return result;
        }
    }
}
