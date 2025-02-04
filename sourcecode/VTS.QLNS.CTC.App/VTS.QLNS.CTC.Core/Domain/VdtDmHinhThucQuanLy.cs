using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDmHinhThucQuanLy
    {
        public Guid IIdHinhThucQuanLyId { get; set; }
        public string SMaHinhThucQuanLy { get; set; }
        public string STenVietTat { get; set; }
        public string STenHinhThucQuanLy { get; set; }
        public string SMoTa { get; set; }
        public int? IThuTu { get; set; }
    }
}
