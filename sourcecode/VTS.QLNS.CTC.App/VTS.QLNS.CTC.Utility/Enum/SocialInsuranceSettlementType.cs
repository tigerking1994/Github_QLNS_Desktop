using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class SocialInsuranceSettlementType
    {
    }

    public struct SettlementTypeLoaiChungTu
    {
        public const int ChungTu = 1;
        public const int ChungTuTongHop = 2;
    }

    public struct SettlementTypeSLNS
    {
        public const string SLNS = "9010003";
        public const string CKPTSDK = "9010006,9010007";
        public const string CKPTSDKDT = "9010006";
        public const string CKPTSDKHT = "9010007";
        public const string CKPQYDV = "9010004,9010005";
        public const string CKPQYDVDT = "9010004";
        public const string CKPQYDVHT = "9010005";
        public const string CKPHSSV = "9050002";
        public const string CKPNLD = "9050001";
        public const string CKPNLDHSSV = "9050001,9050002";
    }
    public struct SettlementTitle
    {
        // QTCQBHXH
        public const string Title1QTCQBHXHKeHoach = "BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BẢO HIỂM XÃ HỘI";

        public const string Title1QTCQBHXHTCOD = "Phụ lục I";
        public const string Title2QTCQBHXHTCOD = "GIẢI THÍCH CHI TRỢ CẤP ỐM ĐAU";

        public const string Title1QTCQBHXHTCTS = "Phụ lục II";
        public const string Title2QTCQBHXHTCTS = "GIẢI THÍCH CHI TRỢ CẤP THAI SẢN";

        public const string Title1QTCQBHXHTT = "THÔNG TRI";
        public const string Title2QTCQBHXHTT = "Quyết toán chi các chế độ bảo hiểm xã hội";

        public const string Title1DSNLDNV = "DANH SÁCH NGƯỜI LAO ĐỘNG NGHỈ VIỆC HƯỞNG CHẾ ĐỘ ỐM ĐAU KHÔNG ĐƯỢC TÍNH THỜI GIAN ĐÓNG BHXH, ĐÃ CHỐT SỔ BHXH";

        public const string Title1QTCQGTTCTN = "Phụ lục III";
        public const string Title2QTCQGTTCTN = "GIẢI THÍCH TRỢ CẤP TAI NẠN LAO ĐỘNG, BỆNH NGHỀ NGHIỆP";

        public const string Title1QTCQGTHTPVXNTVTT = "Phụ lục IV";
        public const string Title2QTCQGTHTPVXNTVTT = "GIẢI THÍCH CHI TRỢ CẤP HƯU TRÍ, PHỤC VIÊN, XUẤT NGŨ, THÔI VIỆC, TỬ TUẤT";

        // QTCQKPQL
        public const string Title1QTCQKPQLTT = "THÔNG TRI";
        public const string Title2QTCQKPQLTT = "Quyết toán chi kinh phí quản lý BHXH, BHYT";
        public const string TitleQTCQKPQLKeHoach = "BÁO CÁO";
        public const string Title2QTCQKPQLKeHoach = "Quyết toán chi kinh phí quản lý bảo hiểm xã hội";
        // QTCQKCB
        public const string Title1QTCQKCBKeHoach = "BÁO CÁO";
        public const string Title2QTCQKCBKeHoach = "QUYẾT TOÁN CHI KINH PHÍ KCB TẠI QUÂN Y ĐƠN VỊ";
        public const string Title1QTCQKCBTT = "THÔNG TRI";
        public const string Title2QTCQKCBTT = "Quyết toán chi kinh phí KCB tại quân y đơn vị";

        // QTCQK
        public const string Title1QTCQKKeHoach = "BÁO CÁO";
        public const string Title2QTCQKTSDKKeHoach = "QUYẾT TOÁN CHI KINH PHÍ KCB TẠI TRƯỜNG SA - DK ";
        public const string Title2QTCQKHSSVNLDKeHoach = "QUYẾT TOÁN CHI KINH PHÍ CHĂM SÓC SỨC KHỎE BAN ĐẦU NLĐ và HSSV ";
        public const string Title2QTCQKNLDKeHoach = "QUYẾT TOÁN CHI KINH PHÍ CHĂM SÓC SỨC KHỎE BAN ĐẦU CHO NLĐ";
        public const string Title2QTCQKHSSVKeHoach = "QUYẾT TOÁN CHI KINH PHÍ CHĂM SÓC SỨC KHỎE BAN ĐẦU CHO HSSV ";
        public const string Title2QTCQKKCBQNKeHoach = "QUYẾT TOÁN CHI TỪ NGUỒN KẾT DƯ QUỸ KCB BHYT QUÂN NHÂN ";
        public const string Title2QTCQKMSTTBYTKeHoach = "QUYẾT TOÁN CHI KINH PHÍ MUA SẮP TRANG THIẾT BỊ Y TẾ ";
        public const string Title2QTCQKHTBHTNKeHoach = "QUYẾT TOÁN CHI HỖ TRỢ NGƯỜI LAO ĐỘNG THAM GIA BHTN ";

        public const string Title1QTCQKTT = "THÔNG TRI";
        public const string Title2QTCQKTSTT = "Quyết toán chi kinh phí KCB tại Trường sa - DK";
        public const string Title2QTCQKNLDTT = "Quyết toán chi kinh phí chăm sóc sức khỏe ban đầu người lao động";
        public const string Title2QTCQKHSSVTT = "Quyết toán chi kinh phí chăm sóc sức khỏe ban đầu cho học sinh, sinh viên";
        public const string Title2QTCQKNKDQKCBBHYTQNTT = "Quyết toán chi từ nguồn kết dư quỹ KCB BHYT quân nhân";
        public const string Title2QTCQKMSTT = "Quyết toán chi kinh phí mua sắm trang thiết bị ý tế";
        public const string Title2QTCQKHTBHTNTT = "Quyết toán chi kinh phí hỗ trợ BHTN";
        // QTCNBHXH
        public const string Title1QTCNBHXHKeHoach = "BÁO CÁO QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BẢO HIỂM XÃ HỘI NĂM";
        public const string Title2QTCNBHXHKeHoach = "";
        public const string Title1QTCNBHXHPhuLuc = "QUYẾT TOÁN CHI CÁC CHẾ ĐỘ BẢO HIỂM XÃ HỘI NĂM....";
        public const string Title2QTCNBHXHPhuLuc = "(Kèm theo Quyết định số........../QĐ-BQP ngày...../...../........của Bộ trường Bộ Quốc Phòng)  ";

        // QTCNKPQL
        public const string Title1QTCNKPQLKeHoach = "BÁO CÁO QUYẾT TOÁN CHI KINH PHÍ QUẢN LÝ BẢO HIỂM XÃ HỘI, BẢO HIỂM Y TẾ ";
        public const string Title1QTCNKPQLPhuLuc = "CHỈ TIÊU VÀ QUYẾT TOÁN KINH PHÍ QUẢN LÝ BHXH, BHYT NĂM ";
        public const string Title2QTCNKPQLPhuLuc = "(Kèm theo Quyết định số........../QĐ-BQP ngày...../...../........của Bộ trường Bộ Quốc Phòng)  ";

        // QTCNKCB
        public const string Title1QTCNKCBKeHoach = "BÁO CÁO QUYẾT TOÁN KINH PHÍ KHÁM CHỮA BỆNH, CHỮA BỆNH CỦA QUÂN NHÂN TẠI QUÂN Y ĐƠN VỊ (KP 10%) ";
        public const string Title1QTCNKCBPhuLuc = "Phụ lục XII";
        public const string Title2QTCNKCBPhuLuc = "CHỈ TIÊU VÀ QUYẾT TOÁN CHI KHÁM CHỮA BỆNH TẠI QUÂN Y ĐƠN VỊ NĂM ";
        public const string Title3QTCNKCBPhuLuc = "(Kèm theo Quyết định số........../QĐ-BQP ngày...../...../........của Bộ trường Bộ Quốc Phòng)  ";

        // QTCNKCB
        public const string Title1QTCNTEMPLATEKeHoach = "BÁO CÁO QUYẾT TOÁN";
        public const string Title1QTCNTEMPLATEPhuLuc = "(Kèm theo Quyết định số........../QĐ-BQP ngày...../...../........của Bộ trường Bộ Quốc Phòng)  ";

        public const string Title2QTCNKPKTSDKKeHoach = "KINH PHÍ KHÁM CHỮA BỆNH BHYT CHO QUÂN NHÂN TẠI TRƯỜNG SA - DK ";
        public const string Title2QTCNKPKHSSVNLDKeHoach = "KINH CHI KINH PHÍ CHĂM SÓC SỨC KHỎE BAN ĐẦU NGƯỜI LAO ĐỘNG VÀ HỌC SINH, SINH VIÊN ";

        public const string Title2QTCNMSTTBYTKeHoach = "KINH PHÍ MUA SẮM TRANG THIẾT BỊ Y TẾ";
        public const string Title2QTCNKCBBHYTQNKeHoach = "KINH PHÍ CHI TỪ NGUỒN KẾT DƯ QUỸ KCB BHYT QUÂN NHÂN";

        public const string Title1QTCNCHTBHTNKeHoach = "BÁO CÁO QUYẾT TOÁN CHI HỖ TRỢ NGƯỜI LAO ĐỘNG ";
        public const string Title2QTCNCHTBHTNKeHoach = "THAM GIA BẢO HIỂM THẤT NGHIỆP NĂM";

        public const string Title1QTCNKPKTSDKPhuLuc = "Phụ lục XI";
        public const string Title2QTCNKPKTSDKPhuLuc = "CHỈ TIÊU VÀ QUYẾT TOÁN CHI KHÁM CHỮA BỆNH TẠI TS - DK NĂM ";

        public const string Title1QTCNKPKHSSVPhuLuc = "Phụ lục XIII";
        public const string Title2QTCNKPKHSSVPhuLuc = "CHỈ TIÊU VÀ QUYẾT TOÁN CHI KP CSSK BAN ĐẦU HỌC SINH, SINH VIÊN NĂM";

        public const string Title1QTCNKPKNLDPhuLuc = "XIV";
        public const string Title2QTCNKPKNLDPhuLuc = "CHỈ TIÊU VÀ QUYẾT TOÁN CHI KP CSSK BAN ĐẦU NGƯỜI LAO ĐỘNG NĂM ";

        public const string Title1QTCNKCBBHYTQNPhuLuc = "X";
        public const string Title2QTCNKCBBHYTQNPhuLuc = "CHỈ TIÊU VÀ QUYẾT TOÁN CHI TỪ NGUỒN KẾT DƯ QUỸ KCB BHYT QUÂN NHÂN";

        public const string Title1QTCNMSTTBYTPhuLuc = "X";
        public const string Title2QTCNMSTTBYTPhuLuc = "CHỈ TIÊU VÀ QUYẾT TOÁN CHI MUA SẮM TRANG THIẾT BỊ Y TẾ NĂM";

        public const string Title1QTCNCHTBHTNPhuLuc = "X";
        public const string Title2QTCNCHTBHTNPhuLuc = "CHỈ TIÊU VÀ QUYẾT TOÁN CHI HỖ TRỢ NGƯỜI LAO ĐÔNG THAM GIA BẢO HIỂM THẤT NGHIỆP NĂM";

        public const string Title3QTCNCHTBHTNKeHoach = "(Theo Quyết định số ...../....../QG-TTg)";

    }
    public struct SettlementTypeQuy
    {
        public const int Quy = 1;
        public const int Quy4 = 4;
    }

    public enum SettlementTypePrint
    {
        PRINT_SETTLEMENT_NOTICE = 1,
        PRINT_SETTLEMENT_PALN = 2,
        PRINT_SETTLEMENT_NOTICE_DETAIL = 3,
        PRINT_SETTLEMENT_PALN_DETAIL = 4,
        PRINT_SETTLEMENT_ADDENDUM = 4,
        PRINT_COMMUNICATE_SETTLEMENT_LNS = 5,
        PRINT_COMMUNICATE_SETTLEMENT_AGENCY = 6,
        PRINT_REGULARLY_SETTLEMENT = 7
    }

    public enum LoaiQTCThongTri
    {
        THONG_TRI_LOAI1 = 0,
        THONG_TRI_LOAI2 = 1
    }
    public enum LoaiChi
    {
        Loai_HSSV = 0,
        Loai_NLD = 1
    }

    public struct DotPhanBo
    {
        public const string Dau_Nam = "1";
        public const string Bo_Sung = "2";
        public const string Tat_Ca = "0";
    }
}
