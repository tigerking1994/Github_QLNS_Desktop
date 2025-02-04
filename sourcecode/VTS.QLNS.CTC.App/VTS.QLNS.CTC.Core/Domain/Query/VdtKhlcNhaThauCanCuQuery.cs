using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhlcNhaThauCanCuQuery
    {
        public Guid Id { get; set; }
        public int ILoaiCanCu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string STenDonVi { get; set; }
        public int ITepDinhKem { get; set; }
        public double FTongGiaTriPheDuyet { get; set; }
    }
}
