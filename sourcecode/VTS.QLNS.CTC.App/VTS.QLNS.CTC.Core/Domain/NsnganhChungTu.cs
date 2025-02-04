using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsNganhChungTu : EntityBase
    {
        [Column("iID_CTNganh")]
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoCongVan { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SDslns { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public bool BKhoa { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public decimal? FTongTuChi { get; set; }
        public decimal? FTongHienVat { get; set; }
        public int? ILoaiChungTu { get; set; }
        public int? INamNganSach { get; set; }
        public double? FTongHangNhap { get; set; }
        public double? FTongHangMua { get; set; }
        public double? FTongPhanCap { get; set; }
        public double? FDuphong { get; set; }
    }
}

