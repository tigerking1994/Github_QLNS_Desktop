using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("HT_LoaiQuyen")]
    public partial class HtLoaiQuyen : EntityBase
    {
        [Column("sTenLoaiQuyen")]
        public string STenLoaiQuyen { get; set; }

        public HtLoaiQuyen()
        {
            HtQuyens = new HashSet<HtQuyen>();
        }

        public virtual ICollection<HtQuyen> HtQuyens { get; set; }
    }
}
