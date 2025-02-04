using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmChucVuModel : BindableBase
    {
        public Guid Id { get; set; }
        public string MaCv { get; set; }
        public string TenCv { get; set; }
        public decimal? HeSoCv { get; set; }
        public decimal? ThanhTienCv { get; set; }
        public string ChucVuDisplay => string.Format("{0} - {1} - {2}", HeSoCv, TenCv, MaCv);
    }
}
