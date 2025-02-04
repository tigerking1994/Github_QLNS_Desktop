using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsQsMucLuc : EntityBase
    {
        [Column("iID_QSMucLuc")]
        public override Guid Id { get; set; }
        public Guid IIdMlns { get; set; }
        public Guid? IIdMlnsCha { get; set; }
        public string SM { get; set; }
        public string STm { get; set; }
        public string SKyHieu { get; set; }
        public string SMoTa { get; set; }
        public int IThuTu { get; set; }
        public string SHienThi { get; set; }
        public bool BHangCha { get; set; }
        public int ITrangThai { get; set; }
        public int INamLamViec { get; set; }
    }
}
