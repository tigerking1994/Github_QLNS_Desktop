using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlPhuCapNq104Query
    {
        public Guid Id { get; set; }
        public string MaPhuCap { get; set; }
        public string TenPhuCap { get; set; }

        public decimal? GiaTri { get; set; }
        public bool? IsFormula { get; set; }
        public string Parent { get; set; }
        public string ParentName { get; set; }

        public decimal? HuongPCSN { get; set; }

        public DateTime? DateStart { get; set; }

        public int? ISoThangHuong { get; set; }

        public bool? BGiaTri { get; set; }
        public bool? BHuongPcSn { get; set; }

        public decimal FGiaTriNhoNhat { get; set; }
        public decimal FGiaTriLonNhat { get; set; }
        public Guid? IIdPhuCapKemTheo { get; set; }
        public string IIdMaPhuCapKemTheo { get; set; }
        public decimal FGiaTriPhuCapKemTheo { get; set; }
    }
}
