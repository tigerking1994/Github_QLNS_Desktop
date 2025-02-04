using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhlcNhaThauGoiThauDetailQuery
    {
        public bool IsChecked { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public int? IIdNguonVonId { get; set; }
        public Guid? IIdChiPhiGocId { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public Guid? IIdHangMucId { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool IsHangCha { get; set; }
        public string SNoiDung { get; set; }
        public string SMaOrder { get; set; }
        public double FGiaTriDuocDuyet { get; set; }
        public double FGiaTriGoiThau { get; set; }
    }
}
