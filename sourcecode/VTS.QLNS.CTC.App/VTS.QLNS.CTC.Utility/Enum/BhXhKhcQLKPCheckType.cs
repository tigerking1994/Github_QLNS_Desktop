using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public struct KhcQlBhxhLoaiChungTu
    {
        public const int BhxhChungTu = 1;
        public const int BhxhChungTuTongHop = 2;
    }
    public struct KhcQlBhxhMLNS
    {
        public const string KHOI = "9010003";
    }

    public struct ImportKhcQLKinhPhi
    {
        public const string NoiDung = "NỘI DUNG";
    }
    public enum ReportKhcQLKP
    {
        KHCQLKPCT = 1,
        KHCQLKPTH = 2,
        KHCQLKPCTDT = 3
    }
    public enum KhcQLKPCheckPrintType
    {
        KHCQLKPCT = 1,
        KHCQLKPTH = 2,
        KHCQLKPCTDT = 3
    }
}
