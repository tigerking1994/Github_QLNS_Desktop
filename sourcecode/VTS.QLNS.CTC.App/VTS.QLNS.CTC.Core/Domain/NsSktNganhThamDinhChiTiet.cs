using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsSktNganhThamDinhChiTiet : EntityBase
    {
        [Column("iID_CTNganhThamDinhChiTiet")]
        public override Guid Id { get; set; }
        public Guid IIdCtnganhThamDinh { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public Guid IIdMucLuc { get; set; }
        public string SM { get; set; }
        public string SMoTa { get; set; }
        public double FTuChi{ get; set; }
        public string SGhiChu { get; set; }
        public int INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public double FSuDungTonKho { get; set; }
        public double FChiDacThuNganhPhanCap { get; set; }
    }
}
