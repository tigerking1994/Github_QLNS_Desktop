using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VTS.QLNS.CTC.Core.Domain.Query;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsDtdauNamChungTu : EntityBase
    {
        //public Guid IIdCtdtdauNam { get; set; }
        [Column("iID_CTDTDauNam")]
        public override Guid Id { get; set; }
        public string SMoTa { get; set; }
        public string IIdMaDonVi { get; set; }
        public int ISoChungTuIndex { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public bool BKhoa { get; set; }
        public int? ILoaiChungTu { get; set; }
        public int? INamNganSach { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public double? FTongTuChi { get; set; }
        public double? FTongHienVat { get; set; }
        public double? FTongHangNhap { get; set; }
        public double? FTongHangMua { get; set; }
        public double? FTongPhanCap { get; set; }
        public string SDSDonViTongHop { get; set; }
        public string SDSSoChungTuTongHop { get; set; }
        public int? ILoaiNguonNganSach { get; set; }

        public string SDslns { get; set; }
        public bool? BDaTongHop { get; set; }
        [NotMapped]
        public bool ImportStatus { get; set; }
        [NotMapped]
        public List<NsDtdauNamChungTuChiTiet> ListDetail { get; set; }
        [NotMapped]
        public List<JsonNsDtDauNamChungTuChiTietCanCuQuery> ListDtDauNamCanCu { get; set; }
        public bool? IsSent { get; set; }
    }
}
