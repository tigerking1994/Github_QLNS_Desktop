using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDmDonViTinh : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public string SMaDonViTinh { get; set; }
        public string STenDonViTinh { get; set; }
        public string SMoTa { get; set; }
        //Another properties 

    }
}
