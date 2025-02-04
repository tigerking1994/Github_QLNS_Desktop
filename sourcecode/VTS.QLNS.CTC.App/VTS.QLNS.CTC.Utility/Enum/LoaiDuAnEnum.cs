using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public static class LoaiDuAnEnum
    {
        public enum Type
        {
            TAT_CA = 0,
            KHOI_CONG_MOI = 1,
            CHUYEN_TIEP = 2
        }

        public struct TypeName
        {
            public const string TAT_CA = "Tất cả";
            public const string KHOI_CONG_MOI = "Khởi công mới";
            public const string CHUYEN_TIEP = "Chuyển tiếp";
        }

        public static string Get(int? type)
        {
            switch (type)
            {
                case (int)Type.TAT_CA:
                    return TypeName.TAT_CA;
                case (int)Type.KHOI_CONG_MOI:
                    return TypeName.KHOI_CONG_MOI;
                case (int)Type.CHUYEN_TIEP:
                    return TypeName.CHUYEN_TIEP;
            }
            return string.Empty;
        }
    }
}
