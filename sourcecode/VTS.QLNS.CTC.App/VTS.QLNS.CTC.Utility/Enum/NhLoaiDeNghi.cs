using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class NhLoaiDeNghi
    {
        public enum Type
        {
            CAP_KINH_PHI = 1,
            THANH_TOAN = 3,
            TAM_UNG_KINH_PHI = 2,
            TAM_UNG_THEO_CHE_DO = 4
        }

        public struct TypeName
        {
            public const string CAP_KINH_PHI = "Cấp kinh phí";
            public const string THANH_TOAN = "Thanh toán theo khối lượng";
            public const string TAM_UNG_KINH_PHI = "Tạm ứng kinh phí";
            public const string TAM_UNG_THEO_CHE_DO = "Tạm ứng theo chế độ";
        }

        public static string Get(int? type)
        {
            switch (type)
            {
                case (int)Type.CAP_KINH_PHI:
                    return TypeName.CAP_KINH_PHI;
                case (int)Type.THANH_TOAN:
                    return TypeName.THANH_TOAN;
                case (int)Type.TAM_UNG_KINH_PHI:
                    return TypeName.TAM_UNG_KINH_PHI;
                case (int)Type.TAM_UNG_THEO_CHE_DO:
                    return TypeName.TAM_UNG_THEO_CHE_DO;
            }
            return string.Empty;
        }
    }
}
