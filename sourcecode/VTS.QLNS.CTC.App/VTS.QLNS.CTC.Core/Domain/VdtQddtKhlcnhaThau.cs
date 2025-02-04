using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtQddtKhlcnhaThau : EntityBase
    {
        public override Guid Id { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string SMoTa { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public Guid? IIdDuToanId { get; set; }
        public Guid? IIdQDDauTuID { get; set; }
        public Guid? IIdChuTruongDauTuID { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool? BIsGoc { get; set; }
        public bool? BActive { get; set; }
        public Guid? IIdLCNhaThauGocID { get; set; }
        public bool? BKhoa { get; set; }
    }
}
