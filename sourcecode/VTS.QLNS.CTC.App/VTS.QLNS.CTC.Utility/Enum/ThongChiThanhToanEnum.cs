using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public enum ThongTriTabIndex
    {
        CAP_THANH_TOAN_KLHT = 1,
        CAP_TAM_UNG = 2,
        CAP_KINH_PHI = 3,
        CAP_HOP_THUC = 4,
        CAP_THANH_TOAN_THU_HOI_UNG = 5
    }

    public static class LoaiThongTriEnum
    {
        public enum Type
        {
            CAP_THANH_TOAN = 1,
            CAP_TAM_UNG = 2,
            CAP_KINH_PHI = 3,
            CAP_HOP_THUC = 4
        }

        public struct Name
        {
            public static string CAP_THANH_TOAN = "Cấp thanh toán";
            public static string CAP_TAM_UNG = "Cấp tạm ứng";
            public static string CAP_KINH_PHI = "Cấp kinh phí";
            public static string CAP_HOP_THUC = "Cấp hợp thức";
        }
    }

    public struct KieuThongTri
    {
        public static string TT_Thu_UngKhac = "TT_Thu_UngKhac";
        public static string TT_KPQP = "TT_CTT_KPQP";
        public static string TT_TamUng_KPQP = "TT_TamUng_KPQP";
        public static string QT_KPQP_CS = "QTKPQP_CS";
        public static string C_KPQP_CS = "CKPQP_CS";
        public static string TN_NSQP = "TNNSQP";
        public static string TT_ThuUng_KPQP = "TT_ThuUng_KPQP";
        public static string TT_TamUng_KPNN = "TT_TamUng_KPNN";
        public static string TT_Cap_KPNN = "TT_Cap_KPNN";
        public static string TT_TamUng_KPK = "TT_TamUng_KPK";
        public static string TT_ThuUng_KPNN = "TT_ThuUng_KPNN";
        public static string QT_KPQP = "QTKPQP";
        public static string TT_Cap_UngKhac = "TT_Cap_UngKhac";
        public static string TT_Cap_KPK = "TT_Cap_KPK";
        public static string TKP_QP_CS = "TKPQP_CS";
        public static string QT_KPK = "QTKPK";
        public static string CTK = "CTK";
        public static string TTK = "TTK";
        public static string QT_NSNN = "QTNSNN";
        public static string TT_ThuUng_KPK = "TT_ThuUng_KPK";
    }

    public static class NamNganSachThongTri
    {
        public enum Type
        {
            NAM_TRUOC_CHUYEN_SANG = 1,
            NAM_NAY = 2
        }

        public struct Name
        {
            public static string NAM_TRUOC_CHUYEN_SANG = "Năm trước chuyển sang";
            public static string NAM_NAY = "Năm nay";
        }
    }
}
