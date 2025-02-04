using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhSalaryDataQuery
    {
        public string SMaHieuCanBo { get; set; }
        public string SXauNoiMa { get; set; }
        public string SMaCanBo { get; set; }
        public string STenCanBo { get; set; }
        public string SMaCapBac { get; set; }
        public string STenCapBac { get; set; }
        public string ID_MaPhanHo { get; set; }
        public string STenPhanHo { get; set; }
        public int? ISoNgayHuong { get; set; }
        public int? ISoNgayTruyLinh { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public double? FSoTien { get; set; }
        public double? FTienTruyLinh { get; set; }
        public string ID_MaDonVi { get; set; }
        public DateTime? DTuNgay { get; set; }
        public DateTime? DDenNgay { get; set; }
        public int IThang { get; set; }

    }
}
