using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Criteria
{
    public class TlQuyetToanChiTietTongHopCriteria
    {
        public string ListIdChungTuTongHop { get; set; }
        public string IdChungTu { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public int NamLamViec { get; set; }
        public int NamNganSach { get; set; }
        public int NguonNganSach { get; set; }
    }
}
