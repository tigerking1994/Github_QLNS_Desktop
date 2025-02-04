using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class MucLucSoKiemTraTheoNganhQuery
    {
        public Guid? IIdMlsktCha { get; set; }
        public Guid IIdMlskt { get; set; }
        public bool? BHangCha { get; set; }
        public string SM { get; set; }
        public string SKyHieu { get; set; }
        public string SStt { get; set; }
        public string SMoTa { get; set; }
        public string SNG { get; set; }
        public string SNgCha { get; set; }
    }
}
