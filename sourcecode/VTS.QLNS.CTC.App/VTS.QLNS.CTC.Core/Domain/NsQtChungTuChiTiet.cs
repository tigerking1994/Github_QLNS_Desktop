using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsQtChungTuChiTiet : EntityBase
    {
        [Column("iID_QTCTChiTiet")]
        public override Guid Id { get; set; }
        public Guid? IIdQtchungTu { get; set; }
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
        public string STng1 { get; set; }
        public string STng2 { get; set; }
        public string STng3 { get; set; }
        public string SMoTa { get; set; }
        public bool BHangCha { get; set; }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public int? INamLamViec { get; set; }
        public int? IThangQuyLoai { get; set; }
        public int? IThangQuy { get; set; }
        public string IIdMaDonVi { get; set; }
        public double FTuChiDeNghi { get; set; }
        public double FTuChiPheDuyet { get; set; }
        public double FSoNguoi { get; set; }
        public double FSoNgay { get; set; }
        public double FSoLuot { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public double? FChuyenNamSauDaCap { get; set; }
        //public double? FChuyenNamSauChuaCap { get; set; }
        public double? FDeNghiChuyenNamSau { get; set; }
        [NotMapped]
        public bool ImportStatus { get; set; }
    }
}
