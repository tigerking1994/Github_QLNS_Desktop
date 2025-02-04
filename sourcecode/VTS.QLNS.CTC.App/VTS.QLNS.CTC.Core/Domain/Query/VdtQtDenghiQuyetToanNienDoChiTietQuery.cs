using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtQtDenghiQuyetToanNienDoChiTietQuery
    {
        public Guid? iId_DeNghiQuyetToanNienDoId { get; set; }
        public Guid? iId_DuAnId { get; set; }
        public Guid? iId_MucId { get; set; }
        public Guid? iId_TieuMucId { get; set; }
        public Guid? iId_TietMucId { get; set; }
        public Guid? iId_NganhId { get; set; }
        public double? fGiaTriCapPhatNamTruoc { get; set; }
        public double? fGiaTriQuyetToanNamTruocDonVi { get; set; }
        public double? fGiaTriQuyetToanNamNayDonVi { get; set; }
        public double? fGiaTriCapPhatNamNay { get; set; }
        public double? fGiaTriQuyetToanNamTruoc { get; set; }
        public double? fGiaTriQuyetToanNamNay { get; set; }
        public Guid? iId_DonViTienTeId { get; set; }
        public Guid? iId_TienTeId { get; set; }
        public double mTiGia { get; set; }
        public double mTiGiaDonVi { get; set; }
        public string sTenDuAn { get; set; }
        public double? fGiaTriDauTu { get; set; }
        public double? fHanMucDauTu { get; set; }
        public string sXauNoiMa { get; set; }
        public double? fThuThanhKhoan { get; set; }
        public double? fBuTruThuaThieu { get; set; }
        public double? fThuLaiKeHoachNamNay { get; set; }
        public double? fThuLaiKeHoachNamTruoc { get; set; }
        public double? fThuThanhKhoanNamTruoc { get; set; }
        public double? fThuUng { get; set; }
        public double? fGiaTriLuyKe { get; set; }
        public double? fTongMucDauTu
        {
            get
            {
                return (!this.fGiaTriDauTu.HasValue || this.fHanMucDauTu == 0) ? this.fHanMucDauTu : this.fGiaTriDauTu;
            }
        }
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
    }
}
