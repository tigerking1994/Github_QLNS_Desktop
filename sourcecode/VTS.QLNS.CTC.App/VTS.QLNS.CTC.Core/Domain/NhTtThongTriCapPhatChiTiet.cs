using System;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhTtThongTriCapPhatChiTiet : EntityBase
    {
        public Guid? IIdThongTriCapPhatId { get; set; }
        public Guid? IIdPheDuyetThanhToanId { get; set; }
        public string SMaOrder { get; set; }
        public int? ITrangThai { get; set; }
    }
}
