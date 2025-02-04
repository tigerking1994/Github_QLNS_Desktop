using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhlcntChiPhiNguonVonCanCuQuery
    {
        public Guid IIdCanCuId { get; set; }
        public string SNoiDung { get; set; }
        public double FGiaTriPheDuyet { get; set; }
        public int ILoai { get; set; }
        public bool IsHangCha { get; set; }
        public Guid? Id { get; set; }
        public Guid? IIdParentId { get; set; }
    }
}
