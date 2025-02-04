using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDmLoaiHopDong : EntityBase
    {
        [NotMapped]
        public override Guid Id { get; set; }
        public Guid IIdLoaiHopDongId { get; set; }
        public string SMaLoaiHopDong { get; set; }
        public string STenVietTat { get; set; }
        public string STenLoaiHopDong { get; set; }
        public string SMoTa { get; set; }
        public int? IThuTu { get; set; }
    }
}
