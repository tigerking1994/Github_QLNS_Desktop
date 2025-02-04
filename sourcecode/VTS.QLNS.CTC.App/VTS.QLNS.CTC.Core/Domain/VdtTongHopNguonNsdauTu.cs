using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtTongHopNguonNsdauTu : EntityBase
    {
        public string IIdMaDonViQuanLy { get; set; }
        public int? IIdNguonVonId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public int? INamKeHoach { get; set; }
        public Guid? IIdChungTu { get; set; }
        public Guid? IIDMucID { get; set; }
        public Guid? IIDTieuMucID { get; set; }
        public Guid? IIDTietMucID { get; set; }
        public Guid? IIDNganhID { get; set; }
        public string Lns { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }
        public string SMaNguon { get; set; }
        public string SMaDich { get; set; }
        public string SMaNguonCha { get; set; }
        public double? FGiaTri { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public int IStatus { get; set; }
        public bool? BKeHoach { get; set; }
        public bool? BIsLog { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdMaNguonCha { get; set; }
        public string SMaTienTrinh { get; set; }
        public int? IThuHoiTUCheDo { get; set; }
        public int? ILoaiUng { get; set; }
    }
}
