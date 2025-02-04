using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NguonVonQuyetToanKeHoachQuery
    {
        public int IIdNguonVonId { get; set; }
        public double GiaTriPheDuyet { get; set; }
        public string TenNguonVon { get; set; }
        public double TienDeNghi { get; set; }
        public double fCapPhatTaiKhoBac { get; set; }
        public double fCapPhatBangLenhChi { get; set; }
        public double DaThanhToan { get; set; }
    }
}
