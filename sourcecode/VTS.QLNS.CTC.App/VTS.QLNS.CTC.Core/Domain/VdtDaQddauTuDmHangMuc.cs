using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaQddauTuDmHangMuc : EntityBase
    {
        public override Guid Id { get; set; }
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
        public Guid? IdLoaiCongTrinh { get; set; }
        public bool? BIsEdit { get; set; }
    }
}
