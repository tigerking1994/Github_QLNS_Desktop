using System;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsBkChungTuChiTiet : EntityBase
    {
        [Column("iID_BKCTChiTiet")]
        public override Guid Id { get; set; }
        public Guid? IIdBkchungTu { get; set; }
        public Guid IIdMlns { get; set; }
        public Guid? IIdMlnsCha { get; set; }
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
        public bool BHangCha { get; set; }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public int? INamLamViec { get; set; }
        public string SLoai { get; set; }
        public int? ILoaiChi { get; set; }
        public int? IThangQuyLoai { get; set; }
        public int? IThangQuy { get; set; }
        public string IIdMaDonVi { get; set; }
        public double FTongTuChi { get; set; }
        public double FTongHienVat { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }

        public virtual NsBkChungTu IIdBkchungTuNavigation { get; set; }
    }
}
