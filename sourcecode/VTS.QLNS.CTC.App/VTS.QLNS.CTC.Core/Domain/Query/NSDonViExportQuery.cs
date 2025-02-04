using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NSDonViExportQuery
    {
        public string Id_DonVi { get; set; }
        public string TenDonVi { get; set; }
        public string Id_DonVi_Parent { get; set; }
        public string KyHieu { get; set; }
        public string MoTa { get; set; }
        public string Loai { get; set; }
        public bool? bQLNgToanQuan { get; set; }
        public int? Loai_don_vi { get; set; }
        public string Khoi { get; set; }
    }
}
