using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public enum RealRevenueExpenditureType
    {
        REAL_BUDGET_NOTICE = 1,
        REAL_BUDGET_RESULT = 2,
        REAL_BUDGET_NATIONAL_DEFENSE_RESULT=3,
        REAL_BUDGET_STATE_RESULT = 4
    }

    public enum RevenueExpenditureImportType
    {
        PLAN_BUDGET_IMPORT_EXPORT = 1,
        APPROVED_ESTIMATION_IMPORT_EXPORT = 2,
        REVENUE_EXPENDITURE_DIVISION_IMPORT_EXPORT = 3,
        REAL_REVENUE_EXPENDITURE_IMPORT_EXPORT = 4
    }

    public class RevenueExpenditureType
    {
        #region Revenue Expenditure
        public const string ROOT_DIALOG = "RootDialog";
        public const string DETAIL_DIALOG = "DetailWindow";
        public const int NSQP = 1;
        public const int NSNN = 2;
        public const string MLNS_NN = "802";
        public const string MLNS_QP = "801";
        public const string REVENUE_EXPENDITURE_REPORT_TEMPLATE = "rptThuNop_01DTN.xlsx";
        public const string PLAN_BUDGET_REPORT_TEMPLATE = "rptDuToan_ThuNop.xlsx";
        public const string REAL_REVENUE_EXPENDITURE_REPORT_TEMPLATE = "ThongTri_Thunop_NganSach";
        public const string RPT_DT_CHUNG_TU_TONG_HOP = "rpt_DT_ChungTu_TongHop.xlsx";
        public const string RPT_TN_DTDN_CHUNG_TU_TONG_HOP = "rpt_TN_DTDN_ChungTu.xlsx";
        public const string RPT_DT_PHAN_BO_CHUNG_TU_TONG_HOP = "rpt_TN_DT_ChungTu_TongHop.xlsx";
        public const string RPT_QT_CHUNG_TU_TONGHOP = "rptTnQt_ChungTu_TongHop.xlsx";
        public const string TEMPLATE_EXCEL = "xlsx";
        public const string TEMPALTE_PDF = "pdf";
        public const string RPT_QT_CHUNG_TU_SOLIEU_THUCTHU = "rpt_TN_QT_ChungTu_SoLieu_ThucThu.xlsx";
        public const string RPT_THUNOP_QUOCPHONG = "rpt_ThuNop_QuocPhong";
        public const string RPT_THUNOP_QUOCPHONG_TO2 = "rpt_ThuNop_QuocPhong_To2";
        public const string RPT_THUNOP_QUOCPHONG_ONEPAGE = "rpt_ThuNop_QuocPhong_OnePage";
        public const string RPT_THUNOP_NGANSACH_NHANUOC = "rpt_ThuNop_NganSach_NhaNuoc";
        public const string RPT_THUNOP_NGANSACH_NHANUOC_TO2 = "rpt_ThuNop_NganSach_NhaNuoc_To2";
        public const string RPT_THUNOP_NGANSACH_NHANUOC_TO3 = "rpt_ThuNop_NganSach_NhaNuoc_To3";
        public const string RPT_THUNOP_NGANSACH_NHANUOC_ONEPAGE = "rpt_ThuNop_NganSach_NhaNuoc_OnePage";
        public const string MLNS_BQP = "80102";
        public const string MLNS_DP = "80101";
        public const string DONVI_DUTOAN = "I. ĐƠN VỊ DỰ TOÁN";
        public const string DONVI_HACHTOAN = "II. ĐƠN VỊ HẠCH TOÁN";
        #endregion

        #region Plan budget report
        public const string PLAN_REPORT_SUM_TYPE_KEY = "0";
        public const string PLAN_REPORT_SUB_TYPE_KEY = "1";
        #endregion

        #region Real Revenue report
        public const string REAL_REVENUE_REPORT_QUATER_MONTH_KEY = "2";
        public const string REAL_REVENUE_REPORT_MONTH_KEY = "0";
        public const string REAL_REVENUE_REPORT_QUATER_KEY = "1";
        #endregion

        public struct RevenueAndExpenditureType
        {
            public const int ApprovedEstimation = 0;
            public const int DivisionEstimation = 1;
            public const int PlanEstimation = 2;
            public const int RealRevenueExpenditure = 3;
            public const int UnitType = 1;
        }
    }
}
