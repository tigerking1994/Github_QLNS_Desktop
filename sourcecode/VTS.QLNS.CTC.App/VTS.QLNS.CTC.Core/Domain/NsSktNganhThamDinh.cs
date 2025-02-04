using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsSktNganhThamDinh : EntityBase
    {
        [Column("iID_CTNganhThamDinh")]
        public override Guid Id { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public int ILoai { get; set; }
        public bool BKhoa { get; set; }
        public int INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public int? ILoaiChungTu { get; set; }
        public string ITongHop { get; set; }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public double? FTongTuChiNganh { get; set; }
        public double? FTongHienVatNganh { get; set; }
        public double? FTongTuChiCtc { get; set; }
        public double? FTongHienVatCtc { get; set; }
    }
}
