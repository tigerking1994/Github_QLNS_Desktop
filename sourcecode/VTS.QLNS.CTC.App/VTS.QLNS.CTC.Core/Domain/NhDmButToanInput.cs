using System;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhDmButToanInput : EntityBase
    {
        public override Guid Id { get; set; }
        public string SSuKien { get; set; }
        public string SMaButToanInput { get; set; }
        public string STenButToanInput { get; set; }
        public string SCongThucBangLoi { get; set; }
        public string SCongThucTheoBangTongHop { get; set; }
        public int? INamNganSach { get; set; }
    }
}
