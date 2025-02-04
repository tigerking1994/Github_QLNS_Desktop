using System.Collections.Generic;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class VoucherType
    {
        public const string NSSD_Key = "1";
        public const string NSBD_Key = "2";
        public const string NSSD_Value = "Ngân sách sử dụng";
        public const string NSBD_Value = "Ngân sách đặc thù của ngành";
        public const string VOCHER_TYPE = "NS_Nganh_Nganh";
        public const string DM_Nganh = "NS_Nganh";

        public static Dictionary<int?, string> VoucherTypeDict = new Dictionary<int?, string>
        {
            {1, "Ngân sách sử dụng"},
            {2, "Ngân sách đặc thù của ngành"}
        };

        public static Dictionary<int?, string> BudgetTypeDict = new Dictionary<int?, string>
        {
            {(int)BudgetType.YEAR, "Đầu năm"},
            {(int)BudgetType.LAST_YEAR, "Năm trước chuyển sang"},
            {(int)BudgetType.ADDITIONAL, "Bổ sung trước 30/09"},
            {(int)BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR, "Bổ sung sau 30/09"},
            {(int)BudgetType.ADJUSTED, "Điều chỉnh"}
        };

        public static Dictionary<BudgetType, string> BudgetTypeName = new Dictionary<BudgetType, string>
        {
            {BudgetType.YEAR, "Đầu năm"},
            {BudgetType.LAST_YEAR, "Năm trước chuyển sang"},
            {BudgetType.ADDITIONAL, "Bổ sung trước 30/09"},
            {BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR, "Bổ sung sau 30/09"},
            {BudgetType.ADJUSTED, "Điều chỉnh"}
        };

        public static Dictionary<string, string> TypeCanCuDict = new Dictionary<string, string>
        {
            {TypeCanCu.ESTIMATE, "Dự toán"},
            {TypeCanCu.SETTLEMENT, "Quyết toán"},
            {TypeCanCu.ALLOCATION, "Cấp phát"},
            {TypeCanCu.DEMAND, "Số nhu cầu"},
            {TypeCanCu.CHECK_NUMBER, "Số kiểm tra"}
        };

        public static Dictionary<int, string> ThietLapCanCuDict = new Dictionary<int, string>
        {
            {1, "Theo chứng từ"},
            {2, "Nhiều chứng từ"},
            {3, "Lũy kế đến 1 chứng từ"},
        };

        public static Dictionary<EstimateSettlementType, string> EstimateSettlementTypeName = new Dictionary<EstimateSettlementType, string>
        {
            {EstimateSettlementType.SIX_MONTH, "Lần 1"},
            {EstimateSettlementType.NINE_MONTH, "Lần 2"}
        };
    }

    public enum BudgetType
    {
        YEAR = 1,
        LAST_YEAR = 2,
        ADDITIONAL = 3,
        ADJUSTED = 4,
        ADDITIONAL_TRANSFER_LAST_YEAR = 5,
    }

    public enum EstimateSettlementType
    {
        SIX_MONTH = 1,
        NINE_MONTH = 2
    }

    public struct TypeCanCu
    {
        public const string ESTIMATE = "BUDGET_ESTIMATE";
        public const string SETTLEMENT = "BUDGET_SETTLEMENT";
        public const string ALLOCATION = "BUDGET_ALLOCATION";
        public const string DEMAND = "BUDGET_DEMANDCHECK_DEMAND";
        public const string CHECK_NUMBER = "BUDGET_DEMANDCHECK_CHECK";
    }

    public struct ThietLapCanCu
    {
        public const int MOT_CHUNG_TU = 1;
        public const int NHIEU_CHUNG_TU = 2;
        public const int LUY_KE = 3;
    }

    public struct TypeDanhMuc
    {
        public const string DM_CAUHINH = "DM_CauHinh";
        public const string DM_DONVITINH = "DM_DonViTinh";
        public const string NS_NamNganSach = nameof(NS_NamNganSach);

    }

    public struct MaDanhMuc
    {
        public const string DV_QUANLY = "DV_QUANLY";
        public const string DIADIEM = "DIADIEM";
        public const string MLNS_CHITIET_TOI = "MLNS_CHITIET_TOI";
        public const string NAM_LUY_KE = "NAM_LUY_KE";
        public const string CAP_PHAT_TOAN_DON_VI = "CAP_PHAT_TOAN_DON_VI";
        public const string DV_THONGTRI_BANHANH = "DV_THONGTRI_BANHANH";
    }
}

