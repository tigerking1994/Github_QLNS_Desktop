using System;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TnDtdnChungTuChiTiet : EntityBase
    {
        public Guid? IdChungTu { get; set; }
        public string XauNoiMa { get; set; }
        public string Lns { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }
        public string Tng { get; set; }
        public string Tng1 { get; set; }
        public string Tng2 { get; set; }
        public string Tng3 { get; set; }
        public double FThucThuNamTruoc { get; set; }
        public double FDuToanNamNay { get; set; }
        public double FUocThucHienNamNay { get; set; }
        public double FDuToanNamKeHoach { get; set; }
        public bool BHangCha { get; set; }
        public bool BKhoa { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public int INamNganSach { get; set; }
        public int INamLamViec { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
    }
}
