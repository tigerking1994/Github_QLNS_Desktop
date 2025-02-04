using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class DtNhanPhanBoMap : BindableBase
    {
        public Guid Id { get; set; }
        public Guid IIdCtduToanNhan { get; set; }
        public Guid IIdCtduToanPhanBo { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNgaySua { get; set; }

    }
}
