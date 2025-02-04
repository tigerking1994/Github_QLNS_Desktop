using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class MediumTermPlan
    {
        public struct MediumTermPlanType
        {
            public const int PlanManager = 0;
            public const int PlanManagerApproved = 1;
            public const string IMPORT_KE_HOACH_TRUNG_HAN_TEMPLATE = "rptKeHoachTrungHan.xlsx";
            public const string IMPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_TEMPLATE = "rptKeHoachTrungHan_DeXuat.xlsx";
            public const string REPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_TEMPLATE = "rptKeHoachTrungHan_DeXuat_Report.xlsx";
            public const string REPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_CHUYENTIEP_TEMPLATE = "rptKeHoachTrungHan_DeXuat_ChuyenTiep_Report.xlsx";
            public const string REPORT_KE_HOACH_TRUNG_HAN_DUOC_DUYET_TEMPLATE = "rptKeHoachTrungHanReport.xlsx";
            public const string REPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_DIEU_CHINH = "rpt_KHTH_DeXuat_Dieu_Chinh.xlsx";
            public const string REPORT_KE_HOACH_TRUNG_HAN_DUOC_DUYET_CHUYENTIEP = "rptKeHoachTrungHan_ChuyenTiep_Report.xlsx";
            public const string EXPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_TEMPLATE = "rptKeHoachTrungHan_DeXuat_KHTHDX.xlsx";
            public const string EXPORT_KE_HOACH_TRUNG_HAN_DUOC_DUYET_TEMPLATE = "rptKeHoachTrungHan_KHTHDD.xlsx";
            public const string OUTPUT_KE_HOACH_TRUNG_HAN_TEMPLATE = "KeHoachTrungHan_DuocDuyet";
            public const string OUTPUT_KE_HOACH_TRUNG_HAN_DE_XUAT_TEMPLATE = "KeHoachTrungHan_DeXuat";
            public const string EXPORT_TEMPLATE_IMPORT_THONG_TIN_GOI_THAU = "Template-Import-Goithau.xlsx";
            public const string EXPORT_TEMPLATE_IMPORT_THONG_TIN_HOP_DONG = "Template-Import-HopDong.xlsx";
            public const string EXPORT_TEMPLATE_IMPORT_THONGTINCHIPHI_PHEDUYETDA = "Template_import_thongtinchiphi_pheduyetda.xlsx";
            public const string EXPORT_TEMPLATE_IMPORT_THONGTINCHIPHI_TKTCTDT = "Template_import_thongtinchiphi_tktctdt.xlsx";

            public const string CHUYEN_TIEP = "Chuyển tiếp";
            public const string KHOI_CONG = "Khởi công mới";
            public const string KET_THUC = "Kết thúc";
            public const string CHUYEN_TIEP_VALUE = "2";
            public const string KHOI_CONG_VALUE = "1";
            public const string KET_THUC_VALUE = "3";
            public const int NGUON_NGAN_SACH_DEFAULT = 1;
            public const int KCONG = 1;
            public const int GROUP = 1;
            public const int PROJECT = 2;
            public const int DETAIL_PROJECT = 3;
            public const int HANG_MUC = 2;
            public const int DUAN = 1;
        }

        public enum MediumTermPlanDuAnType
        {
            PlanManager = 0,
            PlanManagerApproved = 1
        }

        public enum MediumTermType
        {
            Nsqp = 1,
            Nsnn = 2,
            Nsdp = 3,
            Nskhac = 9
        }
    }

    public enum MediumTermModifyType
    {
        NEW = 1,
        CLONE = 2,
        HANGMUC = 3,
        CHILD = 4
    }

    public enum ReportMediumType
    {
        SUGGESTION_CTMM = 1,
        APPROVED_CTMM = 2,
        SUGGESTION_CTCT = 3,
        APPROVED_CTCT = 4
    }

    public enum VoucherTabIndex
    {
        VOUCHER = 1,
        VOUCHER_AGREGATE = 2
    }
}
