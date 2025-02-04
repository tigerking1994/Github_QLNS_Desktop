using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public static class CoQuanThanhToanEnum
    {
        public enum Type
        {
            KHO_BAC = 1,
            CQTC = 2,
            TONKHOAN_DONVI = 3
        }

        public struct TypeName
        {
            public static string KHO_BAC = "Kho bạc";
            public static string CTC = "Cục tài chính";
            public static string CQTC = "Cơ quan Tài chính đơn vị được ủy quyền";
            public static string TONKHOAN_DONVI = "Tồn khoản tại đơn vị";
        }
    }
}
