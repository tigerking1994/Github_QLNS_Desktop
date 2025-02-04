﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("HT_Nhom")]
    public partial class HtNhom : EntityBase
    {
        public HtNhom()
        {
            SysGroupAuthorities = new HashSet<HtNhomQuyen>();
            SysGroupUsers = new HashSet<HtNhomNguoiDung>();
            BKichHoat = true;
        }

        [Column("sTenNhom")]
        public string STenNhom { get; set; }
        [Column("bKichHoat")]
        public bool BKichHoat { get; set; }

        public virtual ICollection<HtNhomQuyen> SysGroupAuthorities { get; set; }
        public virtual ICollection<HtNhomNguoiDung> SysGroupUsers { get; set; }
    }
}
