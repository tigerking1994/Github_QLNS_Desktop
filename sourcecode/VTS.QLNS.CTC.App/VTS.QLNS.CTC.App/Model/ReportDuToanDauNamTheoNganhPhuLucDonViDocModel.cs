using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class ReportDuToanDauNamTheoNganhPhuLucDonViDocModel
    {
        public int Stt { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double TongCong { get; set; }
        public List<NsDtdauNamChungTuChiTiet> LstGiaTri { get; set; }
        public List<NsDtdauNamChungTuChiTiet> LstGiaTriTotal { get; set; }
    }
}
