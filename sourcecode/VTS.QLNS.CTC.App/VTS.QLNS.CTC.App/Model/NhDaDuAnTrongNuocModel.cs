using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaDuAnTrongNuocModel : ModelBase
    {
        public string STenDuAn { get; set; }
        public Guid? IIdChuDauTu { get; set; }
        public string STenChuDauTu { get; set; }
        public string SMaChudauTu { get; set; }
        public double? TongMucDauTuUsd { get; set; }
        public double? TongMucDauTuVnd { get; set; }
        public double? TongMucDauTuEur { get; set; }
        public double? TongMucDauTuNgoaiTeKhac { get; set; }
        public double? DTNgoaiTeKhac { get; set; }
        public string SDiaDiem { get; set; }
        public string TenChuDauTuDisplay { get; set; }
        public Guid? IIdKHTTNhiemVuChiId { get; set; }
    }
}
