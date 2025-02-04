using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaDuToanDmHangMuc: EntityBase
    {
        public Guid? IIdDuAnId { get; set; }
        public string SMaHangMuc { get; set; }
        public string STenHangMuc { get; set; }
        public double? FTienHangMuc { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public string SMoTa { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SQuyMo { get; set; }
        public string MaOrder { get; set; }
        //public Guid? DuToanId { get; set; }
        public Guid? IdLoaiCongTrinh { get; set; }
        public bool? BIsEdit { get; set; }
        public Guid? IIdHangMucPhanChia { get; set; }
    }
}
