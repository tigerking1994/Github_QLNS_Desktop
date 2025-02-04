using System;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TnDtdnChungTu : EntityBase
    {
        public bool? BDaTongHop { get; set; }
        public bool BKhoa { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public double? FTongThucThuNamTruoc { get; set; }
        public double? FTongDuToanNamNay { get; set; }
        public double? FTongUocThucHienNamNay { get; set; }
        public double? FTongDuToanNamKeHoach { get; set; }
        public string IIdMaDonVi { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public int? INamLamViec { get; set; }
        public int? INamNganSach { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public string SDSDonViTongHop { get; set; }
        public string SDSSoChungTuTongHop { get; set; }
        public string SDSLNS { get; set; }
        public string SMoTa { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string SSoChungTu { get; set; }
        public bool? BSent { get; set; }
    }
}
