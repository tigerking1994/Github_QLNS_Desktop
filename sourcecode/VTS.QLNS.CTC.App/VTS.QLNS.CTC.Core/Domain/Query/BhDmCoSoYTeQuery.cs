using System;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDmCoSoYTeQuery
    {
        public Guid Id { get; set; }
        public string IIDMaCoSoYTe { get; set; }
        public string STenCoSoYTe { get; set; }
        public int? INamLamViec { get; set; }
        public int? ITrangThai { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
    }
}
