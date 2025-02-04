using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NSDonViThucHienDuAnExportQuery
    {
        public string Id_DonVi { get; set; }
        public string TenDonVi { get; set; }
        public string Id_DonVi_Parent { get; set; }
        public string SDiaChi { get; set; }
        public bool? BHangCha { get; set; }
        public int? CapDonVi { get; set; }
    }
}
