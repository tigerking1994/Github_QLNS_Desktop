using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class NhCoQuanThanhToan
    {
        public enum Type
        {
            CTC_CAP = 1,
            DON_VI_CAP = 2,
        }

        public struct TypeName
        {
            public const string CTC_CAP = "CTC cấp";
            public const string DON_VI_CAP = "Đơn vị cấp";
        }

        public static string Get(int? type)
        {
            switch (type)
            {
                case (int)Type.CTC_CAP:
                    return TypeName.CTC_CAP;
                case (int)Type.DON_VI_CAP:
                    return TypeName.DON_VI_CAP;
            }
            return string.Empty;
        }
    }
}
