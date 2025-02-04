using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsNganhChungTuChiTiet : EntityBase
    {
        [Column("iID_CTNganhChiTiet")]
        public override Guid Id { get; set; }
        public Guid? IIdCtnganh { get; set; }
        public Guid IIdMlns { get; set; }
        public Guid? IIdParentMlns { get; set; }
        public string SXauNoiMa { get; set; }
        public string SLns { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STm { get; set; }
        public string STtm { get; set; }
        public string SNg { get; set; }
        public string STng { get; set; }
        public string SMoTa { get; set; }
        public string SChuong { get; set; }
        public int? INamLamViec { get; set; }
        public bool BHangCha { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public double FTuChi { get; set; }
        public double FPhanCap { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public double FChuaPhanCap { get; set; }
        public double FHangNhap { get; set; }
        public double FHangMua { get; set; }
        public int? INamNganSach { get; set; }
    }
}

