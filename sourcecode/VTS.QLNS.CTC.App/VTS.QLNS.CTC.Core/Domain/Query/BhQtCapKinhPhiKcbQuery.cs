using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQtCapKinhPhiKcbQuery
    {
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public bool BKhoa { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public int? IQuy { get; set; }
        public int? ILoaiKinhPhi { get; set; }
        public string SCoSoYTe { get; set; }
        public string SDslns { get; set; }
        public string SMoTa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public double? FKeHoachCap { get; set; }
        public double? FDaQuyetToan { get; set; }
        public double? FConLai { get; set; }
        public double? FQuyetToanQuyNay { get; set; }
        public string SQuyNamMoTa { get; set; }
    }
}
