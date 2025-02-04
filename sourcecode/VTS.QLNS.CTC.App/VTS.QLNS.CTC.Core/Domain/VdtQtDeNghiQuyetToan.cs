using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtQtDeNghiQuyetToan : EntityBase
    {
        [Column("iID_DeNghiQuyetToanID")]
        public override Guid Id { get; set; }
        public string SSoBaoCao { get; set; }
        public DateTime? DThoiGianLapBaoCao { get; set; }
        public string SNguoiLap { get; set; }
        public DateTime? DThoiGianNhanBaoCao { get; set; }
        public string SNguoiNhan { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public DateTime? DThoiGianKhoiCong { get; set; }
        public DateTime? DThoiGianHoanThanh { get; set; }
        public double? FGiaTriDeNghiQuyetToan { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public string SMoTa { get; set; }
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
        public Guid? ParentId { get; set; }
        public bool? BTongHop { get; set; }
        public Guid? iID_QuyetDinh { get; set; }
        public string iID_LoaiQuyetToan { get; set; }
        
    }
}
