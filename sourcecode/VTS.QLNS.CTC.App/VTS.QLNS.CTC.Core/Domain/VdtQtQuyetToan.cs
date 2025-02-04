using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtQtQuyetToan : EntityBase
    {
        public Guid Id { get; set; }
        public Guid IIdDuAnId { get; set; }
        public Guid IIdDenghiQuyetToanId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SCoQuanPheDuyet { get; set; }
        public string SNguoiKy { get; set; }
        public string SSoToTrinh { get; set; }
        public DateTime? DNgayToTrinh { get; set; }
        public string SSoThamDinh { get; set; }
        public DateTime? DNgayThamDinh { get; set; }
        public DateTime? DKhoiCongThucTe { get; set; }
        public DateTime? DKetThucThucTe { get; set; }
        public string SNoiDung { get; set; }
        public double? FTienQuyetToanToTrinh { get; set; }
        public double? FTienQuyetToanThamDinh { get; set; }
        public double? FTienQuyetToanPheDuyet { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGia { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public string SCoQuanThamDinh { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public Guid? IIDDonViID { get; set; }
        public string IIDMaDonVi { get; set; }
        public double? FChiPhiThietHai { get; set; }
        public double? FChiPhiKhongTaoNenTaiSan { get; set; }
        public double? FTaiSanDaiHanThuocCDTQuanLy { get; set; }
        public double? FTaiSanDaiHanDonViKhacQuanLy { get; set; }
        public double? FTaiSanNganHanThuocCDTQuanLy { get; set; }
        public double? FTaiSanNganHanDonViKhacQuanLy { get; set; }
        public bool BKhoa { get; set; }
    }
}
