using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public class VdtDmDonViThucHienDuAn : EntityBase
    {
        [NotMapped]
        public override Guid Id { get; set; }
        public Guid IIdDonVi { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public Guid? IIdDonViCha { get; set; }
        public string SDiaChi { get; set; }
        public bool BHangCha { get; set; }
        [NotMapped]
        public string TenDonViCha { get; set; }
        public int? ICapDonVi { get; set; }
        public string IIDMaDonViNS { get; set; }
        [NotMapped]
        public string TenDonViNS { get; set; }
        public string SKyHieu { get; set; }
    }
}
