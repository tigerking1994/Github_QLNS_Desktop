using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportDuToanThuBhxhTheoDonViPhuLucQuery
    {
        public Guid IIDMlnsCha { get; set; }
        public Guid IIDMlns { get; set; }
        public string IdDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string STT { get; set; }
        public bool? BHangCha { get; set; }
        public string sNG { get; set; }
        public string sMoTa { get; set; }
        public Int32 QuyetToan { get; set; }
        public Int32 DuToan { get; set; }
        public double ThuBHXHNldDong { get; set; }
        //public double ThuBHXHNldDong { get; set; }
        public double PhanCap { get; set; }
        public double MuaHangHienVat { get; set; }
        public double DacThu { get; set; }
        public double TongCong { get; set; }
        public List<NsSktChungTuChiTiet> LstGiaTri { get; set; }
        public List<NsSktChungTuChiTiet> LstGiaTriTotal { get; set; }
    }
}
