using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public static class PaymentTypeEnum
    {
        public enum Type
        {
            THANH_TOAN = 1,
            TAM_UNG = 2,
            THU_HOI = 3,
            THU_HOI_NAM_TRUOC = 4,
            THU_HOI_NAM_NAY = 5,
            THU_HOI_UNG_TRUOC_NAM_TRUOC = 6,
            THU_HOI_UNG_TRUOC_NAM_NAY = 7
        }

        public struct TypeName
        {
            public const string THANH_TOAN = "Thanh toán";
            public const string TAM_UNG = "Tạm ứng";
            public const string THU_HOI = "Thu hồi ứng";
            public const string THU_HOI_UNG_TRUOC_NAM_TRUOC = "Thu hồi ứng trước năm trước";
            public const string THU_HOI_UNG_TRUOC_NAM_NAY = "Thu hồi ứng trước năm nay";
            public const string THU_HOI_NAM_TRUOC = "Thu hồi ứng chế độ năm trước";
            public const string THU_HOI_NAM_NAY = "Thu hồi ứng chế độ năm nay";
            public const string THANH_TOAN_KLHT = "Thanh toán KLHT";
        }

        public struct ThongTriStatus
        {
            public const string DaTao = "Đã có thông tri";
            public const string ChuaTao = "Chưa có thông tri";
            public const string All = "Tất cả";
        }

        public static string Get(int type)
        {
            switch (type)
            {
                case (int)Type.THANH_TOAN:
                    return TypeName.THANH_TOAN;
                case (int)Type.TAM_UNG:
                    return TypeName.TAM_UNG;
                case (int)Type.THU_HOI:
                    return TypeName.THU_HOI;
                case (int)Type.THU_HOI_NAM_TRUOC:
                    return TypeName.THU_HOI_NAM_TRUOC;
                case (int)Type.THU_HOI_NAM_NAY:
                    return TypeName.THU_HOI_NAM_NAY;
                case (int)Type.THU_HOI_UNG_TRUOC_NAM_TRUOC:
                    return TypeName.THU_HOI_UNG_TRUOC_NAM_TRUOC;
                case (int)Type.THU_HOI_UNG_TRUOC_NAM_NAY:
                    return TypeName.THU_HOI_UNG_TRUOC_NAM_NAY;
            }
            return string.Empty;
        }

        public struct CapBacLuong
        {
            public const string SyQuan_HamCoYeu = "1,5";
            public const string QNCN_ChuyenMonKyThuatCoYeu = "2,6";
        }

        public struct LoaiCapBac
        {
            public const string SyQuan_HamCoYeu = "5";
            public const string QNCN_ChuyenMonKyThuatCoYeu = "6";
        }
    }
}
