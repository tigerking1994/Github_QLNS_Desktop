using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class SocialInsuranceSalaryEnum
    {
    }
    public struct DefaultReportTitle
    {
        public const string TROCAPOMDAU = "BẢNG THANH TOÁN TRỢ CẤP ỐM ĐAU";
        public const string TROCAPTHAISAN = "BẢNG THANH TOÁN TRỢ CẤP THAI SẢN";
        public const string TROCAPTNLD = "BẢNG THANH TOÁN TRỢ CẤP TAI NẠN LAO ĐỘNG, BỆNH NGHỀ NGHIỆP";
        public const string TROCAPHUUTRI = "BẢNG THANH TOÁN TRỢ CẤP HƯU TRÍ, PHỤC VIÊN, THÔI VIỆC, TỬ TUẤT";
        public const string TROCAPXUATNGU = "BẢNG THANH TOÁN TRỢ CẤP XUẤT NGŨ";
    }
    public struct DefaultQTTReportTitle
    {
        public const string QTT_QUY_ALL = "BÁO CÁO THU - NỘP BẢO HIỂM XÃ HỘI, BẢO HIỂM Y TẾ, BẢO HIỂM THẤT NGHIỆP";
        public const string QTT_QUY_BHXH = "BÁO CÁO THU - NỘP BẢO HIỂM XÃ HỘI";
        public const string QTT_QUY_BHYT = "BÁO CÁO THU - NỘP BẢO HIỂM Y TẾ";
        public const string QTT_QUY_BHTN = "BÁO CÁO THU - NỘP BẢO HIỂM THẤT NGHIỆP";
        public const string QTT_NAM = "BÁO CÁO THU, NỘP BẢO HIỂM XÃ HỘI, BẢO HIỂM THẤT NGHIỆP, BẢO HIỂM Y TẾ";
        public const string QTT_PHU_LUC_BHXH = "QUYẾT TOÁN THU BẢO HIỂM XÃ HỘI NĂM";
        public const string QTT_PHU_LUC_BHTN = "QUYẾT TOÁN THU BẢO HIỂM THẤT NGHIỆP NĂM";
        public const string QTT_PHU_LUC_BHYT_QN = "QUYẾT TOÁN THU BẢO HIỂM Y TẾ QUÂN NHÂN NĂM";
        public const string QTT_PHU_LUC_BHYT_NLD = "QUYẾT TOÁN THU BẢO HIỂM Y TẾ NGƯỜI LAO ĐỘNG";
        public const string QTT_NOP_BHXH_BHYT_BHTN_THEO_THOI_GIAN = "BÁO CÁO THU - PHẢI NỘP BẢO HIỂM XÃ HỘI, BẢO HIỂM Y TẾ, BẢO HIỂM THẤT NGHIỆP THEO THỜI GIAN";

        public const string QTTM_PHU_LUC_THU_BHYT_TN = "QUYẾT TOÁN THẺ BẢO HIỂM Y TẾ THÂN NHÂN QUÂN NHÂN; THÂN NHÂN CƠ YẾU; THÂN NHÂN CN&VCQP; HS; SV;HVQS XÃ, PHƯỜNG; HV ĐÀO TẠO SĨ QUAN DỰ BỊ; HV QUỐC TẾ NĂM";
        public const string QTTM_PHU_LUC_BHYT_TN = "QUYẾT TOÁN THU BẢO HIỂM Y TÉ THÂN NHÂN NĂM";
        public const string QTTM_PHU_LUC_BHYT_HSSV = "QUYẾT TOÁN THU BHYT HSSV, HVQS XÃ PHƯỜNG VÀ SQDB NĂM";
    }
    public struct DefaultDTTReportTitle
    {
        public const string DTT_TIEU_DE_1 = "Phụ lục";
        public const string DTT_PAG_TIEU_DE_2 = "PHƯƠNG ÁN PHÂN BỔ DỰ TOÁN THU BHXH, BHYT, BHTN NĂM";
        public const string DTT_PAG_TIEU_DE_3 = "(Kèm theo văn bản ngày ....../....../.......... của ..............)";
        public const string DTT_TIEU_DE_2 = "VỀ VIỆC GIAO DỰ TOÁN THU, CHI BHXH, BHYT, BHTN NĂM";
        public const string DTT_TH_TIEU_DE_2 = "TỔNG HỢP DỰ TOÁN THU, CHI BHXH, BHYT, BHTN NĂM";
        public const string DTT_THU_TIEU_DE_2 = "VỀ VIỆC GIAO DỰ TOÁN THU BHXH, BHYT, BHTN NĂM";
        public const string DTT_CHI_TIEU_DE_2 = "VỀ VIỆC GIAO DỰ TOÁN CHI BHXH, BHYT, BHTN NĂM";
        public const string DTT_TIEU_DE_3 = "(Kèm theo Quyết định số:....../QĐ-BQP ngày....../....../......)";

        public const string DTT_BHXH = "DỰ TOÁN THU BẢO HIỂM XÃ HỘI";
        public const string DTT_BHTN = "DỰ TOÁN THU BẢO HIỂM THẤT NGHIỆP";
        public const string DTT_BHYT_QN = "DỰ TOÁN THU BẢO HIỂM Y TẾ QUÂN NHÂN";
        public const string DTT_BHYT_NLD = "DỰ TOÁN THU BẢO HIỂM Y TẾ NGƯỜI LAO ĐỘNG";

        public const string BHYT_TN_TIEU_DE_1 = "DỰ TOÁN THU BẢO HIỂM Y TẾ THÂN NHÂN";
        public const string BHYT_TN_TIEU_DE_2 = "(Kèm theo Quyết định số: ... ... /QĐ-BQP ngày ...../...../... ...)";
        public const string BHYT_HSSV_TIEU_DE_1 = "DỰ TOÁN THU BẢO HIỂM Y TẾ HSSV, HVQS XÃ PHƯỜNG VÀ SĨ QUAN DỰ BỊ";
        public const string BHYT_HSSV_TIEU_DE_2 = "(Kèm theo Quyết định số: ... ... /QĐ-BQP ngày ...../...../... ...)";

        public const string DTT_BS_TIEU_DE_2 = "VỀ VIỆC ĐIỀU CHỈNH, BỔ SUNG DỰ TOÁN THU, CHI BHXH, BHYT, BHTN";
        public const string DTT_BS_THU_TIEU_DE_2 = "VỀ VIỆC ĐIỀU CHỈNH, BỔ SUNG DỰ TOÁN THU BHXH, BHYT, BHTN";
        public const string DTT_BS_CHI_TIEU_DE_2 = "VỀ VIỆC ĐIỀU CHỈNH, BỔ SUNG DỰ TOÁN CHI BHXH, BHYT, BHTN";
        public const string DTT_BS_TIEU_DE_3 = "(Kèm theo Quyết định số:....../QĐ-BQP ngày....../....../...... của Bộ Quốc phòng)";

        public const string GDTT_BS_BHXH_TITLE_1 = "ĐIỀU CHỈNH, BỔ SUNG DỰ TOÁN THU BẢO HIỂM XÃ HỘI";
        public const string GDTT_BS_BHTN_TITLE_1 = "ĐIỀU CHỈNH, BỔ SUNG DỰ TOÁN THU BẢO HIỂM THẤT NGHIỆP";
        public const string GDTT_BS_BHYT_QN_TITLE_1 = "ĐIỀU CHỈNH, BỔ SUNG DỰ TOÁN THU BẢO HIỂM Y TẾ QUÂN NHÂN";
        public const string GDTT_BS_BHYT_NLD_TITLE_1 = "ĐIỀU CHỈNH, BỔ SUNG DỰ TOÁN THU BẢO HIỂM Y TẾ NGƯỜI LAO ĐỘNG";
        public const string GDTT_BS_TITLE_2 = "(Kèm theo Quyết định số: ...../QĐ-BQP ngày.../.../.... của Bộ Quốc phòng)";
    }
    public struct DefaultKHTReportTitle
    {
        public const string KHT_DETAIL_TITLE_1 = "Dự toán thu BHXH, BHYT, BHTN";
    }
    public struct MonthOfQuarter
    {
        public const string QUY1 = "1,2,3";
        public const string QUY2 = "4,5,6";
        public const string QUY3 = "7,8,9";
        public const string QUY4 = "10,11,12";
    }

    public struct Quarter
    {
        public const string QUY1 = "1";
        public const string QUY2 = "2";
        public const string QUY3 = "3";
        public const string QUY4 = "4";
    }

    public struct DefaultKHTMReportTitle
    {
        public const string TITLE_1 = "KẾ HOẠCH MUA THẺ BẢO HIỂM Y TẾ THÂN NHÂN QUÂN NHÂN; THÂN NHÂN CƠ YẾU; THÂN NHÂN CN&VCQP; HS; SV;HVQS XÃ, PHƯỜNG; HV ĐÀO TẠO SĨ QUAN DỰ BỊ; HV QUỐC TẾ";
    }

    public struct DefaultCPBSReportTitle
    {
        public const string THONG_TRI_CHI_TIET_TITLE_1 = "THÔNG TRI";
        public const string THONG_TRI_CHI_TIET_QN_TITLE_2 = "Cấp bổ sung kinh phí KCB BHYT Quân nhân";
        public const string THONG_TRI_CHI_TIET_TNQN_TITLE_2 = "Cấp bổ sung kinh phí KCB BHYT TNQN và NLĐ";
        public const string THONG_TRI_CHI_TIET_QN_TITLE_3 = "";
        public const string THONG_TRI_CHI_TIET_TNQN_TITLE_3 = "";

        public const string THONG_TRI_TONG_HOP_TITLE_1 = "THÔNG TRI TỔNG HỢP";
        public const string THONG_TRI_TONG_HOP_QN_TITLE_2 = "Cấp bổ sung kinh phí KCB BHYT Quân nhân";
        public const string THONG_TRI_TONG_HOP_TNQN_TITLE_2 = "Cấp bổ sung kinh phí KCB BHYT TNQN và NLĐ";
        public const string THONG_TRI_TONG_HOP_QN_TITLE_3 = "";
        public const string THONG_TRI_TONG_HOP_TNQN_TITLE_3 = "";

        public const string KE_HOACH_CAP_BO_SUNG_TITLE_1 = "KẾ HOẠCH";
        public const string KE_HOACH_CAP_BO_SUNG_QN_TITLE_2 = "Cấp bổ sung kinh phí KCB BHYT Quân nhân";
        public const string KE_HOACH_CAP_BO_SUNG_TNQN_TITLE_2 = "Cấp bổ sung kinh phí KCB BHYT TNQN và NLĐ";
        public const string KE_HOACH_CAP_BO_SUNG_QN_TITLE_3 = "";
        public const string KE_HOACH_CAP_BO_SUNG_TNQN_TITLE_3 = "";
    }

    public struct DefaultThamDinhQuyetToanTitle
    {
        public const string KET_QUA_THAM_DINH_TiTLE1 = "Kết quả thẩm định quyết toán thu, chi BHXH, BHTN, BHYT năm ";
        public const string KET_QUA_THAM_DINH_TiTLE2 = "(Kèm theo Biên bản thẩm định quyết toán BHXH, BHYT năm ";
        public const string CAN_CU_TRICH_QUY_BHXH_BHYT_TITLE1 = "TỔNG HỢP";
        public const string CAN_CU_TRICH_QUY_BHXH_BHYT_TITLE2 = "CĂN CỨ TRÍCH QUỸ BHXH SANG ĐÓNG BHYT";
        public const string CAN_CU_TRICH_QUY_BHXH_BHYT_TITLE3 = "";
    }
}
