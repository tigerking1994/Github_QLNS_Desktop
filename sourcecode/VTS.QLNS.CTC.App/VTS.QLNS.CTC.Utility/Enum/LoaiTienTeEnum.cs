using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class LoaiTienTeEnum
    {
        public enum Type
        {
            VND = 1,
            USD = 2,
            EUR = 3,
            NGOAI_TE_KHAC = 4
        }

        public struct TypeCode
        {
            public const string VND = "VND";
            public const string USD = "USD";
            public const string EUR = "EUR";
            public const string NGOAI_TE_KHAC = "-";
        }

        public struct TypeName
        {
            public const string VND = "Việt nam đồng";
            public const string USD = "Đô la Mỹ";
            public const string EUR = "Đồng tiền chung châu âu";
            public const string NGOAI_TE_KHAC = "Ngoại tệ khác";
        }

        public static string Get(int? type)
        {
            switch (type)
            {
                case (int)Type.VND:
                    return TypeName.VND;
                case (int)Type.USD:
                    return TypeName.USD;
                case (int)Type.NGOAI_TE_KHAC:
                    return TypeName.NGOAI_TE_KHAC;
            }
            return string.Empty;
        }

        public struct TypeBudgetSourceNH
        {
            public const int NSDB = 5;
        }
    }
}
