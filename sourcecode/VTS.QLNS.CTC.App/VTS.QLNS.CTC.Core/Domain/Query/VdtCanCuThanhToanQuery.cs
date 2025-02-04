using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtCanCuThanhToanQuery
    {
        public Guid Id { get; set; }
        public string STenDuAn { get; set; }
        public string SSoHopDong { get; set; }
        public DateTime? DNgayPheDuyet { get; set; }
        public double FGiaTriThanhToan { get; set; }
        public double FGiaTriThuHoi { get; set; }
        public Guid? IIdThongTriThanhToanId { get; set; }
    }
}
