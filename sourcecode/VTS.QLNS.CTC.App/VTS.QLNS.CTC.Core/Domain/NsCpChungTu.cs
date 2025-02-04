using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsCpChungTu : EntityBase
    {

        [Column("iID_CTCapPhat")]
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public string SDsidMaDonVi { get; set; }
        public string SDslns { get; set; }
        public string NChiTietToi { get; set; }
        public string ITypeMoTa { get; set; }
        public int? ILoai { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public bool BKhoa { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public double? FTongTuChi { get; set; }
        public double? FTongHienVat { get; set; }
        public int? INamNganSach { get; set; }
        public string IIdMaDmcapPhat { get; set; }
        public string SDSSoChungTuTongHop { get; set; }
        public bool? BDaTongHop { get; set; }
        public double? SoCapPhat { get; set; }
        [NotMapped]
        public string SMoTaBaoCao => string.Format("{0}: {1}", SSoChungTu, SMoTa);
    }
}
