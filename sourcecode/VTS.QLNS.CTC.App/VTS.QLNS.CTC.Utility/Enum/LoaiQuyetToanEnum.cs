using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public static class LoaiQuyetToanEnum
    {
        public enum Type
        {
            QUYET_TOAN_KHO_BAC = 1,
            QUYET_TOAN_CQTC = 2,
            QUYET_TOAN_VON_UNG = 3
        }

        public struct TypeName
        {
            public const string QUYET_TOAN_KHO_BAC = "Báo cáo quyết toán các nguồn vốn đầu tư thuộc NSNN theo kho bạc";
            public const string QUYET_TOAN_CQTC = "Báo cáo quyết toán các nguồn vốn đầu tư thuộc NSNN theo cơ quan tài chính";
            public const string QUYET_TOAN_VON_UNG = "Báo cáo kế hoạch và thanh toán vốn đầu tư - Ứng trước kế hoạch vốn năm sau";
        }
    }
}
