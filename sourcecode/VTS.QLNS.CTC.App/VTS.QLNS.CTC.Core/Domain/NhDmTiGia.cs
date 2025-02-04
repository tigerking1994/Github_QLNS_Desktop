using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDmTiGia : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public string SMaTiGia { get; set; }
        public string STenTiGia { get; set; }
        public string SMoTaTiGia { get; set; }
        public Guid? IIdTienTeGocId { get; set; }
        public string SMaTienTeGoc { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string SSoThongBaoKBNN { get; set; }
        public int? IThangTiGia { get; set; }
        public int? INamTiGia { get; set; }
        public DateTime? DNgayBanHanhKBNN { get; set; }

    }
}
