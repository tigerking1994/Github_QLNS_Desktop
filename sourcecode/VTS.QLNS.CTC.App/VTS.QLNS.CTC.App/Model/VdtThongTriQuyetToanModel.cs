using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtThongTriQuyetToanModel : DetailModelBase
    {
        public int IStt { get; set; }
        public Guid IIdDuAnId { get; set; }
        public string STenDuAn { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public string SLns { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STm { get; set; }
        public string STtm { get; set; }
        public string SNg { get; set; }
        public Guid? IIdMucLucNganSach { get; set; }
        public double FSoTien { get; set; }
        public Guid? IIdMucId { get; set; }
        public Guid? IIdTieuMucId { get; set; }
        public Guid? IIdTietMucId { get; set; }
        public Guid? IIdNganhId { get; set; }
    }
}
