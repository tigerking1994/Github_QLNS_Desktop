using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public struct KhcBhxhMLNS
    {
        public const string KHOI_DU_TOAN = "9010001";
        public const string KHOI_HACH_TOAN = "9010002";
    }
    public struct KhcBhxhLoaiChungTu
    {
        public const int BhxhChungTu = 1;
        public const int BhxhChungTuTongHop = 2;
    }

    public struct KhcStatusType
    {
        public const int ACTIVE = 1;
    }
    public enum ReportKhcType
    {
        KHCBHXHCT = 1,
        KHCBHXHTH = 2,
        KHCBHXHCTDT = 3,
    }
    public enum KhcCheckPrintType
    {
        KHCBHXHCT = 1,
        KHCBHXHTH = 2,
        KHCBHXHCTDT = 3,
    }

    public enum NdtcheckPrintType
    {
        NDTCCTNS = 1,
    }

    public struct ImportKhcBhxhDetail
    {
        public const string NoiDung = "NỘI DUNG";
    }

    public struct KhcCheckHachToanAnDuToan
    {
        public const string KhoiHoachToan = "Khối hạch toán";
        public const string KhoiDuToan = "Khối dự toán";
        public const string TroCapOmDau = "0001";
        public const string TroCapThaiSan = "0002";
        public const string TroCapTLLĐNN = "0003";
        public const string TroCapHuuTri = "0004";
        public const string TroCapPhucVien = "0005";
        public const string TroCapXuatNgu = "0006";
        public const string TroCapThoiViec = "0007";
        public const string TroCapTuTuat = "0008";
        public const string SLNSKhoiDuToan = "9010001";
        public const string SLNSKhoiHoachToan = "9010002";
    }
    public struct PlanKHCTitle
    {
        public const string Title3BaoCaoDuToan = "(Kèm theo Quyết định số: ....../QD-BQP ngày.... /...../.......)";

        // KHC BHXH
        public const string Title1BaoCaoKHC = "KẾ HOẠCH CHI CÁC CHẾ ĐỘ BẢO HIỂN XÃ HỘI";
        public const string Title1BaoCaoDuToan = "Phụ lục VII";
        public const string Title2BaoCaoDuToan = "DỰ TOÁN CHI CÁC CHẾ ĐỘ BẢO HIỂM XÃ HỘI NĂM";

        // KHC KPQL
        public const string Title1BaoCaoKHCKPQL = "KẾ HOẠCH CHI KINH PHÍ QUẢN LÝ BHXH, BHYT,BHTN";
        public const string Title1BaoCaoDuToanKHCKPQL = "Phụ lục VIII";
        public const string Title2BaoCaoDuToanKHCKPQL = "DỰ TOÁN CHI KINH PHÍ QUẢN LÝ BHXH, BHYT NĂM";

        // KHC  KCBQY
        public const string Title1BaoCaoKHCKCBQY = "KẾ HOẠCH CHI KINH KCB TẠI QUÂN Y ĐƠN VỊ";
        public const string Title1BaoCaoDuToanKHCKCBQY = "Phụ lục IX";
        public const string Title2BaoCaoDuToanKHCKCBQY = "DỰ TOÁN CHI KCB TẠI QUÂN Y ĐƠN VỊ NĂM";

        // KHC TNKKCHBHYTQN
        public const string Title1BaoCaoKHCTNKCBBHYTQN = "KẾ HOẠCH CHI TỪ NGUỒN KẾT DƯ QUỸ KCB BHYT QUÂN NHÂN";

        // KHC KPMSTTBYT
        public const string Title1BaoCaoKHCKPMSTTBYT = "KẾ HOẠCH CHI KINH PHÍ MUA SẮM TRANG THIẾT BỊ Y TẾ";

        // KHC HOTRO BHTN
        public const string Title1BaoCaoKHCHTBHTN = "KẾ HOẠCH CHI HỖ TRỢ BHTN";

        // KHC KHAC

        public const string Title1BaoCaoKHCKTS = "KẾ HOẠCH CHI KINH KCB CHO QUÂN NHÂN TẠI TRƯỜNG SA - DK ";
        public const string Title1BaoCaoDuToanKHCKTS = "Phụ lục X";
        public const string Title2BaoCaoDuToanKHCKTS = "DỰ TOÁN CHI KINH PHÍ KCB CHO QUÂN NHÂN TẠI TRƯỜNG SA - DK NĂM";

        public const string Title1BaoCaoKHCKHSSVNLD = "KẾ HOẠCH CHI KINH PHÍ CSSK CHO HỌC SINH, SINH VIÊN VÀ NGƯỜI LAO ĐỘNG";
        public const string Title1BaoCaoDuToanKHCKHSSVLNLD = "Phụ lục XI";
        public const string Title2BaoCaoDuToanKHCKHSSVNLD = "DỰ TOÁN CHI KINH PHÍ CSSK CHO HỌC SINH, SINH VIÊN VÀ NGƯỜI LAO ĐỘNG NĂM";


        // Bổ sung 
        public const string Title2BaoCaoBSDuToan = "ĐIỀU CHỈNH, BỔ SUNG DỰ TOÁN CHI CÁC CHẾ ĐỘ BẢO HIỂM XÃ HỘI NĂM";
        public const string Title2BaoCaoBSDuToanKHCKPQL = "ĐIỀU CHỈNH, BỔ SUNG DỰ TOÁN CHI KINH PHÍ QUẢN LÝ BHXH, BHYT NĂM";
        public const string Title2BaoCaoBSDuToanKHCKCBQY = "ĐIỀU CHỈNH, BỔ SUNG DỰ TOÁN CHI KCB TẠI QUÂN Y ĐƠN VỊ NĂM";
        public const string Title2BaoCaoBSDuToanKHCKTS = "ĐIỀU CHỈNH, BỔ SUNG DỰ TOÁN CHI KINH PHÍ KCB CHO QUÂN NHÂN TẠI TRƯỜNG SA - DK NĂM";
        public const string Title2BaoCaoBSDuToanKHCKHSSVNLD = "ĐIỀU CHỈNH, BỔ SUNG DỰ TOÁN CHI KINH PHÍ CSSK CHO HỌC SINH, SINH VIÊN VÀ NGƯỜI LAO ĐỘNG NĂM";
    }
}
