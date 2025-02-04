using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class LoaiNhaThau
    {
        public enum Type
        {
            NHA_THAU = 1,
            DON_VI_UY_THAC = 2,
        }

        public struct TypeName
        {
            public const string NHA_THAU = "Nhà thầu";
            public const string DON_VI_UY_THAC = "Đơn vị ủy thác";
        }

        public static string Get(int? type)
        {
            switch (type)
            {
                case (int)Type.NHA_THAU:
                    return TypeName.NHA_THAU;
                case (int)Type.DON_VI_UY_THAC:
                    return TypeName.DON_VI_UY_THAC;
            }
            return string.Empty;
        }
    }
}
