using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public class YearPlanManagerType
    {
        public const string OUTPUT_KH_NAM_DONVI = "KHNam_DonVi";

        public const string RPT_KH_NAM_CHUYEN_TIEP = "rpt_KHNam_ChuyenTiep";
        public const string RPT_KH_NAM_KHOI_CONG_MOI = "rpt_KHNam_MoMoi";
        public const string RPT_KH_NAM_DUOC_DUYET= "rpt_KHNam_DuocDuyet.xlsx";
        public const string RPT_KH_NAM_DONVI = "rpt_KHNam_DonVi.xlsx";
        public const string RPT_BAOCAO_KH_NAM_DONVI = "rpt_BaoCao_KHNam_DonVi.xlsx";
        public const string RPT_BAOCAO_KH_NAM_DONVI_DIEUCHINH = "rpt_BaoCao_KHNam_DonVi_DieuChinh.xlsx";
        public const string RPT_BAOCAO_KH_NAM_DONVI_GOC = "rpt_BaoCao_KHNam_DonVi_Goc.xlsx";
        public const string RPT_BAOCAO_KH_NAM_DONVI_CTMM = "rpt_BaoCao_KHNam_DonVi_CTMM.xlsx";
        public const string RPT_BAOCAO_KH_NAM_DONVI_CTCT = "rpt_BaoCao_KHNam_DonVi_CTCT.xlsx";
        public const string RPT_BAOCAO_KH_NAM_DONVI_PHEDUYET_DIEUCHINH = "rpt_BaoCao_KHNam_DonVi_PheDuyet_DieuChinh.xlsx"; 
        public const string RPT_BAOCAO_KH_NAM_DONVI_PHEDUYET = "rpt_BaoCao_KHNam_DonVi_PheDuyet.xlsx";
        public const string RPT_KH_NAM_DEXUAT_DIEUCHINH_NGUON_VON_KHAC = "rpt_BaoCao_KHNam_DeXuat_DieuChinh_NguonVonKhac.xlsx";
        public const string RPT_KH_NAM_DEXUAT_GOC_NGUON_VON_KHAC = "rpt_BaoCao_KHNam_DeXuat_Goc_NguonVonKhac.xlsx";
        public const string RPT_KH_NAM_DONVI_PHEDUYET_NGUON_VON_KHAC = "rpt_BaoCao_KHNam_DonVi_PheDuyet_NguonVonKhac.xlsx";
        public const string RPT_KH_NAM_DONVI_PHEDUYET_DIEU_CHINH_NGUON_VON_KHAC = "rpt_BaoCao_KHNam_DonVi_PheDuyet_DieuChinh_NguonVonKhac.xlsx"; 
        public const int DU_AN_CHUYEN_TIEP = 2;
        public const string CHUYEN_TIEP = "chuyển tiếp";
        public const string KHOI_CONG_MOI = "mở mới";
        public const string RPT_KE_HOACH_VON_NAM_DUOC_DUYET = "rptKeHoachVonNamDuocDuyet.xlsx";
        public const string RPT_KE_HOACH_VON_NAM_DUOC_DUYET_BY_LOAICONGTRINH = "rptKeHoachVonNamDuocDuyet_LoaiCongTrinh.xlsx";
        public const string RPT_BAOCAO_KH_NAM_DONVI_PHEDUYET_CTMM = "rpt_BaoCao_KHNam_DonVi_PheDuyet_CTMM.xlsx";
        public const string RPT_BAOCAO_KH_NAM_DONVI_PHEDUYET_CTCT = "rpt_BaoCao_KHNam_DonVi_PheDuyet_CTCT.xlsx";
        public const string RPT_KH_VONUNG_DONVI = "rpt_KHVonUng_DonVi.xlsx";
        public const string RPT_KH_VONUNG_DUOCDUYET = "rpt_KHVonUng_DuocDuyet.xlsx";
        public const string OUTPUT_KH_VONUNG_DONVI = "KHVonUng_DonVi";
        public const string OUTPUT_KH_VONUNG_DUOCDUYET = "KHVonUng_DuocDuyet";
    }

    public class LoaiDuToan
    {
        public enum Type
        {
            DAU_NAM = 0,
            BO_XUNG = 1,
            NAM_TRUOC_CHUYEN_SANG =2
        }

        public struct Name
        {
            public static string DAU_NAM = "Đầu năm";
            public static string BO_XUNG = "Bổ sung";
            public static string NAM_TRUOC_CHUYEN_SANG = "Năm trước chuyển sang";
        }
    }
}
