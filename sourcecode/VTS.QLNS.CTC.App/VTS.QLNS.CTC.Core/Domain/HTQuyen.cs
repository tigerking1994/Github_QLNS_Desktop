﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("HT_Quyen")]
    public partial class HtQuyen : EntityBase
    {
        [NotMapped]
        public override Guid Id { get; set; }
        [Column("iID_Quyen")]
        public Guid IIdQuyen { get; set; }
        [Column("iID_MaQuyen")]
        public string IIDMaQuyen { get; set; }
        [Column("sTenQuyen")]
        public string STenQuyen { get; set; }
        [Column("iID_LoaiQuyen")]
        public Guid IIDLoaiQuyen { get; set; }
        public IList<HtQuyenChucNang> SysFunctionAuthorities { get; set; }

        public HtQuyen()
        {
            SysGroupAuthorities = new HashSet<HtNhomQuyen>();
            SysFunctionAuthorities = new List<HtQuyenChucNang>();
        }

        public virtual HtLoaiQuyen SysAuthorityType { get; set; }
        public virtual ICollection<HtNhomQuyen> SysGroupAuthorities { get; set; }
    }
}
