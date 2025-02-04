using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Enum
{
    public struct EstimationTypeValue
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

    public struct EstimationTypeName
    {
        public const string NGAY_CHUNG_TU_ASC = "Ngày chứng từ ↑";
        public const string NGAY_CHUNG_TU_DESC = "Ngày chứng từ ↓";
        public const string SO_CHUNG_TU_ASC = "Số chứng từ ↑";
        public const string SO_CHUNG_TU_DESC = "Số chứng từ ↓";
        public const string SO_QUYET_DINH_ASC = "Số QĐ ↑";
        public const string SO_QUYET_DINH_DESC = "Số QĐ ↓";
        public const string NGAY_QUYET_DINH_ASC = "Ngày QĐ ↑";
        public const string NGAY_QUYET_DINH_DESC = "Ngày QĐ ↓";
    }

    public struct SoChungTuType
    {
        public const int ReceiveEstimate = 0;
        public const int EstimateDivision = 1;
        public const int HospitalEstimate = 2;
    }

    public struct EstimationReport
    {
        #region Template Name
        public const string DU_TOAN_TONG_HOP_1_TEMPLATE = "rptDuToan_PhanBo_TongHop_To_TuChi_HienVat_To_1";
        public const string DU_TOAN_TONG_HOP_2_TEMPLATE = "rptDuToan_PhanBo_TongHop_To_TuChi_HienVat_To_2";
        public const string DU_TOAN_TONG_HOP_CHUNG_1_TEMPLATE = "rptDuToan_PhanBo_TongHopChung_To1_";
        public const string DU_TOAN_TONG_HOP_CHUNG_2_TEMPLATE = "rptDuToan_PhanBo_TongHopChung_To2_";
        public const string DU_TOAN_CHI_TIEU_LUY_KE_TONG_HOP = "rptDuToan_ChiTieu_LuyKe_TongHop";
        public const string DU_TOAN_CHI_TIEU_LUY_KE_DU_PHONG = "rptDuToan_ChiTieu_DuPhong";
        #endregion
        #region Template Type
        public const string DU_TOAN_EXCEL = "xlsx";
        public const string DU_TOAN_PDF = "pdf";
        #endregion
        public const string DU_TOAN_THEO_NGANH = "NS_Nganh_Nganh";
        public const string DU_TOAN_THEO_CHUYEN_NGANH = "NS_Nganh";
    }

    public struct NSDuToan
    {
        public const int IPHANCAP_NHAN_PHANBO = 0;
        public const int IPHANCAP_PHANBO_DUTOAN = 1;

        public const int ILOAI_NHAN_PHANBO = 0;
        public const int ILOAI_PHANBO_DUTOAN = 1;
    }
}
