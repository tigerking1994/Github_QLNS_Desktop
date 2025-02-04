using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsSession
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int NguonNganSach { get; set; }
        public int NamNganSach { get; set; }
        public int NamLamViec { get; set; }
        public int? ThangLamViec { get; set; }
        public int ITrangThai { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
    }
}
