using System;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhQtQuyetToanNienDo : EntityBase
    {
        public Guid? IIdParentId { get; set; }
        public Guid? IIdGocId { get; set; }
        public string SSoDeNghi { get; set; }
        public DateTime? DNgayDeNghi { get; set; }
        public int? INamKeHoach { get; set; }
        public Guid? IIdDonViId { get; set; }
        public string IIdMaDonVi { get; set; }
        public int? IIdNguonVonId { get; set; }
        public int? ILoaiThanhToan { get; set; }
        public int? ILoaiQuyetToan { get; set; }
        public int? ICoQuanThanhToan { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public string SMoTa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool BIsActive { get; set; }
        public bool BIsGoc { get; set; }
        public bool BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public bool BIsXoa { get; set; }
        public virtual Guid? IID_TongHopID { get; set; }
        public virtual string STongHopChildID { get; set; }
        
    }
}
