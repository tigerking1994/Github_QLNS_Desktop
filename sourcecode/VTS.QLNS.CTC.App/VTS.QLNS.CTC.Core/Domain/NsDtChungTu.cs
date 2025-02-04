using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VTS.QLNS.CTC.Utility;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{

    [Table("NS_DT_ChungTu")]
    public partial class NsDtChungTu : EntityBase
    {
        [Column("iID_DTChungTu")]
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public string SDsidMaDonVi { get; set; }
        public string SDslns { get; set; }
        public int ILoai { get; set; }
        public int? ILoaiChungTu { get; set; }
        public int? ILoaiDuToan { get; set; }
        public int? INamNganSach { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public int? INamLamViec { get; set; }
        public string IIdDotNhan { get; set; }
        public double FTongTonKho { get; set; }
        public double FTongTuChi { get; set; }
        public double FTongRutKBNN { get; set; }
        public double FTongHienVat { get; set; }
        public double FTongHangMua { get; set; }
        public double FTongHangNhap { get; set; }
        public double FTongPhanCap { get; set; }
        public double FTongDuPhong { get; set; }
        public bool BKhoa { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public bool? BLuongNhanDuLieu { get; set; }
        public string SDonViNhanDuLieu { get; set; }
        public string IIDChungTuDieuChinh { get; set; }
        public string SSoChungTuDieuChinh { get; set; }
        public ICollection<NsDtChungTuChiTiet> ChungTuChiTiets { get; set; }
        public ICollection<NsDtNhanPhanBoMap> NsDtMapNhan { get; set; }
        public ICollection<NsDtNhanPhanBoMap> NsDtMapPhanBo { get; set; }
        [NotMapped]
        public bool ImportStatus { get; set; }
        [NotMapped]
        [CustomNameJsonAttribute("DuToanAnhXa")]
        public List<NsDtNhanPhanBoMap> ListVoucherMap { get; set; }
        [NotMapped]
        [CustomNameJsonAttribute("NhanDuToanChungTu")]
        public List<NsDtChungTu> ListVoucher { get; set; }
        [NotMapped]
        [CustomNameJsonAttribute("PhanBoDuToanChiTiet")]
        public List<NsDtChungTuChiTiet> ListDetailChiTiet { get; set; }
        [NotMapped]
        [CustomNameJsonAttribute("NhanDuToanChiTiet")]
        public List<NsDtChungTuChiTiet> ListDetailVoucher { get; set; }
        [NotMapped]
        [CustomNameJsonAttribute("CanCuChiTiet")]
        public List<NsDtChungTuCanCu> ListDetailCanCu { get; set; }
    }
}
