using System;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public struct DemandCheckType
    {
        public const int DEMAND = 0;
        public const int DEMAND3Y = 99;
        public const int CORPORATIZED_HOSPITAL = 2;
        public const int CHECK = 3;
        public const int DISTRIBUTION = 4;
    }

    public struct StatusType
    {
        public const int ACTIVE = 1;
    }

    public enum DeleteType
    {
        DELETE_ALL_DETAIL = 0,
        DELETE_ALL_DETAIL_DISPLAY = 1,
    }
    public struct DemandCheckSortTypeName
    {
        public const string THOI_GIAN_ASC = "Thời gian - đơn vị ↑";
        public const string THOI_GIAN_DESC = "Thời gian - đơn vị ↓";
        public const string SO_CHUNG_TU_ASC = "Số chứng từ ↑";
        public const string SO_CHUNG_TU_DESC = "Số chứng từ ↓";
        public const string NGAY_CHUNG_TU_ASC = "Ngày chứng từ ↑";
        public const string NGAY_CHUNG_TU_DESC = "Ngày chứng từ ↓";
        public const string SO_QUYET_DINH_ASC = "Số QĐ ↑";
        public const string SO_QUYET_DINH_DESC = "Số QĐ ↓";
        public const string NGAY_QUYET_DINH_ASC = "Ngày QĐ ↑";
        public const string NGAY_QUYET_DINH_DESC = "Ngày QĐ ↓";
    }

    public struct DemandCheckSortTypeValue
    {
        public const string SO_CHUNG_TU_ASC = "SoChungTu ASC";
        public const string SO_CHUNG_TU_DESC = "SoChungTu DESC";
        public const string NGAY_CHUNG_TU_ASC = "NgayChungTu ASC";
        public const string NGAY_CHUNG_TU_DESC = "NgayChungTu DESC";
        public const string SO_QUYET_DINH_ASC = "SoQuyetDinh ASC";
        public const string SO_QUYET_DINH_DESC = "SoQuyetDinh DESC";
        public const string NGAY_QUYET_DINH_ASC = "NgayQuyetDinh ASC";
        public const string NGAY_QUYET_DINH_DESC = "NgayQuyetDinh DESC";
    }

    public enum DemandCheckPrintType
    {
        THE_REPORT_RECEIVES_THE_CHECK_NUMBER = 1,
        SUMMARY_REPORT_OF_TEST_NUMBER_ALLOCATION = 2,
        REPORT_DEMAND_DETAIL_NUMBER = 3,
        REPORT_ORG_DEMAND_DETAIL_NUMBER = 4,
        REPORT_SYNTHESIS_ORG_DEMAND_DETAIL_NUMBER = 5,
        REPORT_SYNTHESIS_DEMAND_DETAIL_NUMBER = 6,
        REPORT_DEMAND_NUMBER_SUMMARY = 7,
        REPORT_PHAN_BO_SO_KIEM_TRA_THEO_DON_VI = 8,
        REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA = 9,
        REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH = 10,
        REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH_DONVI_DOC_ML_NGANG = 11,
        REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH = 12,
        REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH = 13,
        REPORT_ORG_DEMAND3Y_DETAIL_NUMBER = 14,
        REPORT_DEMAND3Y_NUMBER_SUMMARY = 15,
        REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA = 16,
        REPORT_SO_SANH_NHAN_SKT_NAM_TRUOC_NAM_NAY = 17,
        REPORT_SO_SANH_PHAN_BO_SKT_NAM_TRUOC_NAM_NAY = 18
    }

    public struct DemandCheckScreen
    {
        public const string ROOT_DIALOG = "RootDialog";
        public const string DETAIL_DIALOG = "DetailDialog";
    }

    public enum DemandCheckAction
    {
        ADD_DEMAND = 0,
        EDIT_DEMAND = 1
    }

    public struct DataStateValue
    {
        public const int HIEN_THI_TAT_CA = 0;
        public const int CO_SO_LIEU_DT_QT_SKT = 1;
        public const int DA_NHAP_SKT = 2;
        public const int DA_NHAN_SKT = 3;
        public const int CO_SO_LIEU = 4;
        public const int SO_CON_LAI_CHUA_PHAN_BO = 5;
    }

    public struct DataStateName
    {
        public const string HIEN_THI_TAT_CA = "Hiển thị tất cả";
        public const string CO_SO_LIEU_DT_QT_SKT = "Có số liệu (DT,QT,SKT)";
        public const string DA_NHAP_SKT = "Đã nhập SKT";
        public const string DA_NHAN_SKT = "Đã nhận SKT";
        public const string CO_SO_LIEU = "Có số liệu";
        public const string SO_CON_LAI_CHUA_PHAN_BO = "Số kiểm tra còn lại chưa phân bổ";
    }

    public enum TypeReport
    {
        NUMBER_CHECK = 0,
        SUMMARY = 1
    }

    public struct TypeModuleCanCu
    {
        public const string DEMAND = "BUDGET_DEMANDCHECK_DEMAND";
        public const string DISTRIBUTION = "BUDGET_DEMANDCHECK_DISTRIBUTION";
        public const string PLAN_BEGIN_YEAR = "BUDGET_DEMANDCHECK_PLAN";
        public const string PLAN_BEGIN_YEAR_DACTHU = "BUDGET_DEMANDCHECK_PLAN_DACTHU";
    }

    public struct TypeViewSummary
    {
        public const int Summary = 0;
        public const int Detail = 1;
    }

    public struct TypeViewSummaryName
    {
        public const string Detail = "Xem chi tiết";
        public const String Summary = "Xem tổng hợp";
    }
    public struct TypeKhoi
    {
        public const int TAT_CA = 0;
        public const int DOANH_NGHIEP = 1;
        public const int DU_TOAN = 2;
        public const int BENH_VIEN = 3;
    }

    public struct TypeLoaiNNS
    {
        public const int TAT_CA = 0;
        public const int DU_TOAN = 1;
        public const int BENH_VIEN = 2;
        public const int DOANH_NGHIEP = 3;
    }

    public struct TypeLoaiNS
    {
        public const int TAT_CA = 0;
        public const int NS_QUOC_PHONG = 1;
        public const int NS_NHA_NUOC = 2;
        public const int NS_KHAC = 3;
    }
}