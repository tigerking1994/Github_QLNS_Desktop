using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmThemCachTinhLuongModel : BindableBase
    {
        public Guid Id { get; set; }
        public string MaThemCachTl { get; set; }
        public string TenThemCachTl { get; set; }
    }
}
