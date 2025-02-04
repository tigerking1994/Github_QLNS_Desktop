using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class VdtKtLoaiChiTietThanhToan
    {
        public enum Type
        {
            HOP_DONG = 1,
            CHI_PHI = 2,
        }

        public struct TypeName
        {
            public const string HOP_DONG = "Hợp đồng";
            public const string CHI_PHI = "Chi phí";
        }

        public static string Get(int? type)
        {
            switch (type)
            {
                case (int)Type.HOP_DONG:
                    return TypeName.HOP_DONG;
                case (int)Type.CHI_PHI:
                    return TypeName.CHI_PHI;
            }
            return string.Empty;
        }
    }
}
