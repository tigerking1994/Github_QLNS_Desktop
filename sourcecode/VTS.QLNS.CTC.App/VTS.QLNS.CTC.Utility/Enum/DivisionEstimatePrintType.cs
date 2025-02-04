using System.Collections.Generic;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public struct DivisionEstimateScreen
    {
        public const string ROOT_DIALOG = "RootDialog";

        public static Dictionary<int, string> DivisionEstimatePrintTypeName = new Dictionary<int, string>
        {
            {(int) DivisionEstimatePrintType.COVER_SHEET, "IN CHỈ TIÊU - TỜ BÌA"},
            {(int) DivisionEstimatePrintType.TARGET_AGENCY, "IN CHỈ TIÊU - ĐƠN VỊ"},
            {(int) DivisionEstimatePrintType.TARGET_MAJORS, "IN CHỈ TIÊU - NGÀNH"},
            {(int) DivisionEstimatePrintType.SYNTHESIS_BUDGET_AGENCY, "DỰ TOÁN PHÂN CẤP - TỔNG HỢP ĐƠN VỊ"},
            {(int) DivisionEstimatePrintType.SYNTHESIS_BUDGET_DIVISION, "IN BÁO CÁO SỐ PHÂN BỔ THEO ĐỢT"},
            {(int) DivisionEstimatePrintType.TARGET_AGENCY_LNS, "IN CHỈ TIÊU: ĐƠN VỊ - LNS"},
            {(int) DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_AGENCY_LNS, "IN CHỈ TIÊU: TỔNG HỢP ĐƠN VỊ"},
            {(int) DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_LNS_AGENCY, "DỰ TOÁN PHÂN CẤP - TỔNG HỢP ĐƠN VỊ"},
            {(int) DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_MAJORS, "DỰ TOÁN PHÂN CẤP - TỔNG HỢP ĐƠN VỊ- NGÀNH"},
            {(int) DivisionEstimatePrintType.PUBLIC_FINANCE, "CÔNG KHAI TÀI CHÍNH"},
            {(int) DivisionEstimatePrintType.PUBLIC_DIVISION, "CÔNG KHAI TÀI CHÍNH"},
            {(int) DivisionEstimatePrintType.DIVISION_PLAN, "PHƯƠNG ÁN PHÂN BỔ DỰ TOÁN"},
        };

        public static Dictionary<int, string> DivisionEstimatePrintTypeTitle = new Dictionary<int, string>
        {
            {(int) DivisionEstimatePrintType.COVER_SHEET, "In chỉ tiêu - Tờ bìa"},
            {(int) DivisionEstimatePrintType.TARGET_AGENCY, "In chỉ tiêu - Đơn vị"},
            {(int) DivisionEstimatePrintType.TARGET_MAJORS, "In chỉ tiêu - Ngành"},
            {(int) DivisionEstimatePrintType.SYNTHESIS_BUDGET_AGENCY, "Dự toán phân cấp - Tổng hợp đơn vị"},
            {(int) DivisionEstimatePrintType.SYNTHESIS_BUDGET_DIVISION, "In báo cáo số phân bổ theo đợt"},
            {(int) DivisionEstimatePrintType.TARGET_AGENCY_LNS, "In chỉ tiêu: Đơn vị - LNS"},
            {(int) DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_AGENCY_LNS, "In chỉ tiêu: Tổng hợp đơn vị"},
            {(int) DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_LNS_AGENCY, "Dự toán phân cấp - Tổng hợp đơn vị"},
            {(int) DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_MAJORS, "Dự toán phân cấp - Tổng hợp đơn vị - Ngành"},
            {(int) DivisionEstimatePrintType.PUBLIC_FINANCE, "Báo cáo danh mục công khai tài chính"},
            {(int) DivisionEstimatePrintType.PUBLIC_DIVISION, "Báo cáo công khai dự toán thu, chi NSNN - TT57"},
            {(int) DivisionEstimatePrintType.DIVISION_PLAN, "Báo cáo phương án phân bổ dự toán - Theo Công văn 2344/QĐ-CTC"},
        };

        public static Dictionary<int, string> DivisionEstimatePrintTypeDescription = new Dictionary<int, string>
        {
            {(int) DivisionEstimatePrintType.COVER_SHEET, "Chọn đợt cần in tờ bìa chỉ tiêu dự toán"},
            {(int) DivisionEstimatePrintType.TARGET_AGENCY, "Chọn thông số in chỉ tiêu dự toán cho đơn vị"},
            {(int) DivisionEstimatePrintType.TARGET_MAJORS, "Chọn thông số in chỉ tiêu dự toán cho Ngành bảo đảm"},
            {(int) DivisionEstimatePrintType.SYNTHESIS_BUDGET_AGENCY, "Chọn đợt phân cấp chi tiêu in báo cáo (MLNS hàng dọc, đơn vị hàng ngang)"},
            {(int) DivisionEstimatePrintType.SYNTHESIS_BUDGET_DIVISION, "Chọn in tổng hợp hoặc chi tiết số phân bổ theo đợt"},
            {(int) DivisionEstimatePrintType.TARGET_AGENCY_LNS, "Chọn thông số in chỉ tiêu dự toán cho đơn vị theo LNS"},
            {(int) DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_AGENCY_LNS, "Chọn đợt phân bổ chỉ tiêu in báo cáo ( đơn vị  hàng dọc, LNS hàng ngang)"},
            {(int) DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_LNS_AGENCY, "Chọn đợt phân cấp chi tiêu in báo cáo ( MLBS hàng dọc, đơn vị hàng ngang)"},
            {(int) DivisionEstimatePrintType.DETAIL_SYNTHESIS_TARGET_MAJORS, "Chọn đợt phân cấp chi tiêu in báo cáo ( MLBS hàng dọc, đơn vị hàng ngang)"},
            {(int) DivisionEstimatePrintType.PUBLIC_FINANCE, "Báo cáo danh mục công khai tài chính"},
            {(int) DivisionEstimatePrintType.PUBLIC_DIVISION,"Báo cáo công khai dự toán thu, chi NSNN - TT57" },
            {(int) DivisionEstimatePrintType.DIVISION_PLAN,"Báo cáo phương án phân bổ dự toán - Theo Công văn 2344/QĐ-CTC" }
        };
    }

    public enum DivisionEstimatePrintType
    {
        COVER_SHEET = 1,
        TARGET_AGENCY = 2,
        TARGET_MAJORS = 3,
        SYNTHESIS_BUDGET_AGENCY = 4,
        SYNTHESIS_BUDGET_DIVISION = 5,
        TARGET_AGENCY_LNS = 6,
        DETAIL_SYNTHESIS_TARGET_AGENCY_LNS = 7,
        DETAIL_SYNTHESIS_TARGET_LNS_AGENCY = 8,
        DETAIL_SYNTHESIS_TARGET_MAJORS = 9,
        TARGET_AGENCY_SUMMARY = 10,
        TARGET_MAJORS_DAY = 11,
        TARGET_MAJORS_AGENCY = 12,
        PUBLIC_FINANCE = 13,
        PUBLIC_DIVISION = 14,
        DIVISION_ESTIMATE_BATCH = 15,
        DIVISION_PLAN = 16,
        TARGET_AGENCY_HD4554 = 17,

    }
}
