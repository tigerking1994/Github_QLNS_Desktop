using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public struct PTDauThauType
    {
        public const string PT1 = "PT1";
        public const string PT2 = "PT2";
        public const string PT3 = "PT3";
        public const string PT4 = "PT4";
    }

    public struct PTDauThauTypeName
    {
        public const string PT_1 = "1 Giai đoạn 1 túi hồ sơ";
        public const string PT_2 = "1 Giai đoạn 2 túi hồ sơ";
        public const string PT_3 = "2 Giai đoạn 1 túi hồ sơ";
        public const string PT_4 = "2 Giai đoạn 2 túi hồ sơ";
    }

    public struct HTChonNhaThauType
    {
        public const string HT1 = "HT1";
        public const string HT2 = "HT2";
        public const string HT3 = "HT3";
        public const string HT4 = "HT4";
        public const string HT5 = "HT5";
        public const string HT6 = "HT6";
        public const string HT7 = "HT7";
        public const string HT8 = "HT8";
        public const string HT9 = "HT9";

    }

    public struct HTChonNhaThauTypeName
    {
        public const string HT_1 = "Đấu thầu rộng rãi";
        public const string HT_2 = "Đấu thầu hạn chế";
        public const string HT_3 = "Chỉ định thầu";
        public const string HT_4 = "Chào hàng cạnh tranh";
        public const string HT_5 = "Mua sắm trực tiếp";
        public const string HT_6 = "Tự thực hiện";
        public const string HT_7 = "Lựa chọn NT, NĐT trong trường hợp đặc biệt";
        public const string HT_8 = "Tham gia thực hiện của cộng đồng";
        public const string HT_9 = "Chỉ định thầu rút gọn";
    }

    public struct HTHopDongType
    {
        public const string HD1 = "HD1";
        public const string HD2 = "HD2";
        public const string HD3 = "HD3";
    }

    public struct HTHopDongTypeName
    {
        public const string HD_1 = "Hợp đồng trọn gói";
        public const string HD_2 = "Hợp đồng theo đơn giá cố định";
        public const string HD_3 = "Hợp đồng theo đơn giá điều chỉnh";
    }

    public struct LoaiGoiThauTypeName
    {
        public const string TU_VAN = "Gói thầu tư vấn";
        public const string XAY_LAP = "Gói thầu xây lắp";
    }

    public struct LoaiGoiThauType
    {
        public const string TUVAN = "TV";
        public const string XAYLAP = "XL";
    }

    public struct LoaiKHLCNTType
    {
        public const string DU_TOAN = "1";
        public const string QDDT = "2";
        public const string CHU_TRUONG_DAU_TU = "3";
    }

    public struct LoaiKHLCNTTypeName
    {
        public const string DU_TOAN = "TKTC và Tổng dự toán";
        public const string QDDT = "Phê duyệt dự án/Báo cáo KTKT";
        public const string CHU_TRUONG_DAU_TU = "Chủ trương đầu tư";
    }
}
