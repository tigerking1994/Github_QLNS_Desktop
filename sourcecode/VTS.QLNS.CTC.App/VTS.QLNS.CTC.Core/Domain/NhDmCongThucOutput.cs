using System;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhDmCongThucOutput : EntityBase
    {
        public override Guid Id { get; set; }
        public string SMenuSuDung { get; set; }
        public string SMaOutput { get; set; }
        public string STenOutput { get; set; }
        public string SCongThucBangLoi { get; set; }
        public string SCongThucTheoBangTongHop { get; set; }
    }
}
