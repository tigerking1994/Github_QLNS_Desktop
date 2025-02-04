using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportPhanBoKiemTraPhuongAnPhanBoQuery
    {
        public Guid? IIdMlsktCha { get; set; }
        public Guid IIdMlskt { get; set; }
        public string IdDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string sL { get; set; }
        public string sK { get; set; }
        public string sM { get; set; }
        public string sNG { get; set; }
        public string sKyHieu { get; set; }
        public string STT { get; set; }
        public string SSTTBC { get; set; }
        public bool? BHangCha { get; set; }
        public string sMoTa { get; set; }
        public string sNgCha { get; set; }
        public double FSoKiemTraNS { get; set; }
        public double FDuToanDauNam { get; set; }
        public double FSoDuKienPB { get; set; }
        public double FTang { get; set; }
        public double FGiam { get; set; }
        public double TongCong { get; set; }
        public int Level { get; set; }
    }
}
