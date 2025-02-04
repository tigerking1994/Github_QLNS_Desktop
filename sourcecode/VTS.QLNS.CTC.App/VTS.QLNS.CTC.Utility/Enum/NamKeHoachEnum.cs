using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public static class NamKeHoachEnum
    {
        public enum Type
        {
            NAM_TRUOC = 1,
            NAM_NAY = 2,
            NAM_SAU = 3
        }

        public struct TypeName
        {
            public const string NAM_TRUOC = "Năm trước";
            public const string NAM_NAY = "Năm nay";
            public const string NAM_SAU = "Năm sau";
        }
    }
}
