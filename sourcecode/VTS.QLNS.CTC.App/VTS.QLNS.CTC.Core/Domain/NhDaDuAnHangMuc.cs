using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDaDuAnHangMuc : EntityBase
    {
        public Guid? IIdDuAnId { get; set; }
        public string SMaHangMuc { get; set; }
        public string STenHangMuc { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SMaOrder { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
    }
}
