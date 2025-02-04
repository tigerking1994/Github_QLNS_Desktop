using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public struct KhcKcbBhxhMLNS
    {
        public const string QUAN_Y_DON_VI_KHOI_DU_TOAN = "9010004";
        public const string QUAN_Y_DON_VI_KHOI_HACH_TOAN = "9010005";
        public const string TRUONG_SA_DK_KHOI_DU_TOAN = "9010006";
        public const string TRUONG_SA_DK_KHOI_HACH_TOAN = "9010007";
    }
    public struct KhcKcbBhxhLoaiChungTu
    {
        public const int BhxhChungTu = 1;
        public const int BhxhChungTuTongHop = 2;
    }

    public struct ILoaiKCB
    {
        public const string LoaiKCBQYDV = "1";
        public const string LoaiKCBTSDK = "2";
    }

    public struct LoaiKeHoachChi
    {
        public const string QUAN_Y_DON_VI = "Chi kinh phí KCB tại quân y đơn vị";
        public const string TRUONG_SA_DK = "Chi kinh phí KCB tại Trường Sa - DK";
    }

    public enum ReportKhcKCBType
    {
        KHCKCBBHXHCT = 1,
        KHCKCBBHXHTH = 2,
        KHCKCBBHXHCTDT = 3,
    }
    public enum KhcKcbCheckPrintType
    {
        KHCKCBBHXHCT = 1,
        KHCKCBBHXHTH = 2,
        KHCKCBBHXHCTDT = 3,
    }
}
