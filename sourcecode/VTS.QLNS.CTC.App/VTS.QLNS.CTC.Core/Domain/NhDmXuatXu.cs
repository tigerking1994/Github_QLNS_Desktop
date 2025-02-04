using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDmXuatXu : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public string SMaXuatXu { get; set; }
        public string STenXuatXu { get; set; }
        public string SMoTa { get; set; }
        //Another properties 

    }
}
