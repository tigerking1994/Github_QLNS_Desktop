using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsPhongBanDonVi
    {
        public int Id { get; set; }
        public string IdPhongBan { get; set; }
        public string IdDonVi { get; set; }
        public int? NamLamViec { get; set; }
        public int ITrangThai { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
    }
}
