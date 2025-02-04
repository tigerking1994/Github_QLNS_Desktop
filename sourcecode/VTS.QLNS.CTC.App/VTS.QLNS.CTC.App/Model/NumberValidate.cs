using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Properties;

namespace VTS.QLNS.CTC.App.Model
{
    public class NumberValidate : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                double number = 0;
                bool check = string.IsNullOrEmpty(value?.ToString().Trim()) || double.TryParse(value.ToString(), out number);

                if (!check)
                {
                    return new ValidationResult(false, Resources.MesInvalidData);
                }
            }
            catch (Exception e)
            {
                return new ValidationResult(false, Resources.MesInvalidData);
            }
            return ValidationResult.ValidResult;
        }
    }
}
