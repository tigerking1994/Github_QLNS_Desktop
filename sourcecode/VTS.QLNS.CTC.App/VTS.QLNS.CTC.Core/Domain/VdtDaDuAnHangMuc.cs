using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaDuAnHangMuc:EntityBase
    {
        [Column("iID_DuAn_HangMucID")]
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
        public string MaOrDer { get; set; }
        public Guid? IdChuTruong { get; set; }
        public int? iID_NguonVonID { get; set; }
        public int? indexMaHangMuc { get; set; }
        public double? fHanMucDauTu { get; set; }
        public Guid? IdLoaiCongTrinh { get; set; }
        [NotMapped]
        public string SMaDuAn { get; set; }
    }
}
