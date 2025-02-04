using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDmLoaiTienTe : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public string SMaTienTe { get; set; }
        public string STenTienTe { get; set; }
        public string SMoTaChiTiet { get; set; }
    }
}
