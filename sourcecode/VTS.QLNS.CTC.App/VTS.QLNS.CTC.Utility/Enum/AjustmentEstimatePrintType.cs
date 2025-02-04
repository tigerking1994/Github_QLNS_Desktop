using System.Collections.Generic;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public struct AjustmentEstimatePrintScreen
    {
        public const string ROOT_DIALOG = "RootDialog";

        public static Dictionary<int, string> AjustmentEstimatePrintTypeName = new Dictionary<int, string>
        {
            {(int) AjustmentEstimatePrintType.PHANBO_DUTOAN, "IN BÁO CÁO - ĐIỀU CHỈNH PHÂN BỔ DỰ TOÁN"},
            {(int) AjustmentEstimatePrintType.PHANBO_DUTOAN_THEOLAN, "IN BÁO CÁO - "},
            {(int) AjustmentEstimatePrintType.QUYET_TOAN_CHI_NSNN, "IN BÁO CÁO - "}
        };

        public static Dictionary<int, string> AjustmentEstimatePrintTypeTitle = new Dictionary<int, string>
        {
            {(int) AjustmentEstimatePrintType.PHANBO_DUTOAN, "In báo cáo - "},
            {(int) AjustmentEstimatePrintType.PHANBO_DUTOAN_THEOLAN, "In báo cáo - "},
            {(int) AjustmentEstimatePrintType.QUYET_TOAN_CHI_NSNN, "In nhận phân bổ - "}
        };

        public static Dictionary<int, string> AjustmentEstimatePrintTypeDescription = new Dictionary<int, string>
        {
            {(int) AjustmentEstimatePrintType.PHANBO_DUTOAN, "Chọn in chi tiết "},
            {(int) AjustmentEstimatePrintType.PHANBO_DUTOAN_THEOLAN, "Chọn in chi tiết "},
            {(int) AjustmentEstimatePrintType.QUYET_TOAN_CHI_NSNN, "Chọn "}
        };
    }

    public enum AjustmentEstimatePrintType
    {
        PHANBO_DUTOAN = 1,
        PHANBO_DUTOAN_THEOLAN = 2,
        QUYET_TOAN_CHI_NSNN = 3
    }
}
