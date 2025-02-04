using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhTtThucHienNganSachGiaiDoan : EntityBase
    {
        public Guid ID { get; set; }
        public string sGiaiDoan { get; set; }
        public double? valueUSD { get; set; }
        public double? valueVND { get; set; }
        public virtual int? iGiaiDoanTu { get; set; }
        public virtual int? iGiaiDoanDen { get; set; }
    }
}
