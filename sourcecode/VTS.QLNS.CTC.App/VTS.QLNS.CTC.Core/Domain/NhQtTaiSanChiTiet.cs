using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhQtTaiSanChiTiet : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public Guid? IIdTaiSanId { get; set; }
        public string SHangMuc { get; set; }
        public int? ISoLuong { get; set; }
        public Guid? IIdDonViTinhId { get; set; }
        public Guid? IIdXuatXuId { get; set; }
        public Guid? IIdHopDongId { get; set; }
        //Another properties 

    }
}
