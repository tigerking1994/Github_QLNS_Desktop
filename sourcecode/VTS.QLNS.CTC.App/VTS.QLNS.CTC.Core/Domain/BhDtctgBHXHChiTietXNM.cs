using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DTC_DuToanChiTrenGiao_ChiTiet_XNM")]
    public partial class BhDtctgBHXHChiTietXNM : EntityBase
    {
        [Column("iID")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IID_DTC_DuToanChiTrenGiao { get; set; }
        public string SXauNoiMa { get; set; }
        public double? FTuChi { get; set; }  
    }
}
