using System.Collections.Generic;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public struct DivisionScreen
    {
        public const string ROOT_DIALOG = "RootDialog";

        public static Dictionary<int, string> DivisionPrintTypeName = new Dictionary<int, string>
        {
            {(int) DivisionPrintType.BUDGET_PERIOD, "IN BÁO CÁO - PHÂN BỔ THEO ĐỢT"},
            {(int) DivisionPrintType.BUDGET_ACCUMULATION, "IN BÁO CÁO - PHÂN BỔ THEO ĐỢT"},
            {(int) DivisionPrintType.BUDGET_SPECIALIZED, "IN BÁO CÁO - PHÂN BỔ THEO NGÀNH"},
            {(int) DivisionPrintType.SYNTHESIS_BUDGET_SELF, "IN BÁO CÁO TỔNG HỢP SỐ PHÂN BỔ"},
            {(int) DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS, "IN BÁO CÁO TỔNG HỢP SỐ PHÂN BỔ"},
            {(int) DivisionPrintType.SYNTHESIS_BUDGET_COMMON, "IN BÁO CÁO TỔNG HỢP SỐ PHÂN BỔ"},
        };

        public static Dictionary<int, string> DivisionPrintTypeTitle = new Dictionary<int, string>
        {
            {(int) DivisionPrintType.BUDGET_PERIOD, "In báo cáo - Phân bổ theo đợt"},
            {(int) DivisionPrintType.BUDGET_ACCUMULATION, "In báo cáo - Phân bổ theo đợt"},
            {(int) DivisionPrintType.BUDGET_SPECIALIZED, "In nhận phân bổ - Theo ngành"},
            {(int) DivisionPrintType.SYNTHESIS_BUDGET_SELF, "In báo cáo tổng hợp số phân bổ"},
            {(int) DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS, "In báo cáo tổng hợp số phân bổ"},
            {(int) DivisionPrintType.SYNTHESIS_BUDGET_COMMON, "In báo cáo tổng hợp số phân bổ"},
        };

        public static Dictionary<int, string> DivisionPrintTypeDescription = new Dictionary<int, string>
        {
            {(int) DivisionPrintType.BUDGET_PERIOD, "Chọn in chi tiết một đợt hoặc luỹ kế tới đợt trên phân bổ"},
            {(int) DivisionPrintType.BUDGET_ACCUMULATION, "Chọn in chi tiết một đợt hoặc luỹ kế tới đợt trên phân bổ"},
            {(int) DivisionPrintType.BUDGET_SPECIALIZED, "Chọn thông số in số đã nhận phân bổ dự toán cho các Ngành, chuyên ngành"},
            {(int) DivisionPrintType.SYNTHESIS_BUDGET_SELF, "Chọn in chi tiết một đợt hoặc luỹ kế tới đợt trên phân bổ"},
            {(int) DivisionPrintType.SYNTHESIS_BUDGET_ARTIFACTS, "Chọn in chi tiết một đợt hoặc luỹ kế tới đợt trên phân bổ"},
            {(int) DivisionPrintType.SYNTHESIS_BUDGET_COMMON, "Chọn in chi tiết một đợt hoặc luỹ kế tới đợt trên phân bổ"},
        };
    }

    public enum DivisionPrintType
    {
        BUDGET_PERIOD = 1,
        BUDGET_ACCUMULATION = 2,
        BUDGET_SPECIALIZED = 3,
        SYNTHESIS_BUDGET_SELF = 4,
        SYNTHESIS_BUDGET_ARTIFACTS = 5,
        SYNTHESIS_BUDGET_COMMON = 6,
        ESTIMATE_BY_RECEIVE_DIVISION = 7,
    }
}
