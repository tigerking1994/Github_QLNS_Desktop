using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDmLoaiTaiSan : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public string SMaLoaiTaiSan { get; set; }
        public string STenLoaiTaiSan { get; set; }
        public string SMoTa { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgayTao { get; set; }
        //Another properties 

    }
}
