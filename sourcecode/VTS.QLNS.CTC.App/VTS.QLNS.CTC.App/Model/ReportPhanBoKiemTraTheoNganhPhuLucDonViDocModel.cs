using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel
    {
        public int Stt { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double TongCong { get; set; }
        public double TongCongChuyenNganh { get; set; }
        public List<NsSktChungTuChiTiet> LstGiaTri { get; set; }
        public List<NsSktChungTuChiTiet> LstGiaTriTotal { get; set; }
    }
}
