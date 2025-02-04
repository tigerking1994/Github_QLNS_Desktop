using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlDsCanBoNghiHuuKeHoachQuery
    {
        public Guid Id { get; set; }
        public string MaCB { get; set; }
        public string TenCB { get; set; }
        public string TenDonVi { get; set; }
        public string MaCapBac { get; set; }
        public string MaChucVu { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool IsNam { get; set; }
        public int Nam { get; set; }
        public int Thang { get; set; }
        public string TenCapBac { get; set; }
        public int TuoiHuuNam { get; set; }
        public int TuoiHuuNu { get; set; }
        public string Parent { get; set; }

    }
}
