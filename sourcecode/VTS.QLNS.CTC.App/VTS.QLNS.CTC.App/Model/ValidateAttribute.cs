using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class ValidateAttribute : Attribute
    {
        public string DisplayName { get; set; }
        public DATA_TYPE DataType { get; set; }
        public bool IsRequired { get; set; }
        public int MaxLength { get; set; }
        public string DefaultValue { get; set; }
        public string RegularExpression { get; set; }

        public ValidateAttribute(string displayName, DATA_TYPE dataType = DATA_TYPE.String, bool isRequired = false, string regularExpression = "")
        {
            DisplayName = displayName;
            DataType = dataType;
            IsRequired = isRequired;
            RegularExpression = regularExpression;
        }

        public ValidateAttribute(string displayName, DATA_TYPE dataType, int maxLength, bool isRequired = false, string regularExpression = "")
        {
            DisplayName = displayName;
            DataType = dataType;
            MaxLength = maxLength;
            IsRequired = isRequired;
            RegularExpression = regularExpression;
        }

        public ValidateAttribute(string displayName, DATA_TYPE dataType, string defaultValue, int maxLength = 0, string regularExpression = "")
        {
            DisplayName = displayName;
            DataType = dataType;
            DefaultValue = defaultValue;
            MaxLength = maxLength;
            RegularExpression = regularExpression;
        }
    }
}
