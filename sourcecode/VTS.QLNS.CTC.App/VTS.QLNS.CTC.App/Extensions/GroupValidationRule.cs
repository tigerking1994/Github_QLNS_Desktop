using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Properties;

namespace VTS.QLNS.CTC.App.Extensions
{
    public class GroupValidationRule : ValidationRule
    {
        public string ValidationTarget { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            switch (ValidationTarget)
            {
                case "Name":
                    string strValue = Convert.ToString(value);
                    if (String.IsNullOrEmpty(strValue))
                    {
                        return new ValidationResult(false, Resources.ErrorGroupNameEmpty);
                    }
                    return new ValidationResult(true, null);
                default:
                    throw new InvalidCastException($"Validation is not supported");
            }
        }
    }
}
