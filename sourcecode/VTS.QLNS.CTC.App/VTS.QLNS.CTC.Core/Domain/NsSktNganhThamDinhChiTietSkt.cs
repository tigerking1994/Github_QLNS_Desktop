using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsSktNganhThamDinhChiTietSkt : EntityBase
    {
        [Column("iID_CTNganhThamDinhChiTiet_SKT")]
        public override Guid Id { get; set; }
        public Guid IIdCtsoKiemTra { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public Guid IIdMucLuc { get; set; }
        public string SM { get; set; }
        public string SMoTa { get; set; }
        public string SGhiChu { get; set; }
        public int INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public double? FTuChi { get; set; }
        public double? FSuDungTonKho { get; set; }
        public double? FChiDacThuNganhPhanCap { get; set; }
    }
}
