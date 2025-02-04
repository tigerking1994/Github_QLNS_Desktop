using System.Globalization;
using System.Windows.Controls;

namespace VTS.QLNS.CTC.App.Helper
{
    public class CharacterLimitRule : ValidationRule
    {

        public int MiniumCharacter { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var charstring = string.Empty;
            if (value != null) charstring = value as string;

            return (charstring.Length < MiniumCharacter && charstring.Length != 0) ? new ValidationResult(false, ErrorMessage) : ValidationResult.ValidResult;
        }
    }
}
