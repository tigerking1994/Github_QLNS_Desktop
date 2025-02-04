using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportNhQtQuyetToanNienDoQuery
    {
        public Guid Id { get; set; }
        public int ILoai { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public Guid IIdNhiemVuChiId { get; set; }
        public string STenNhiemVuChi { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string STenDuAn { get; set; }
        public string NoiDung { get; set; }

        [NotMapped]
        public Guid? IIdParentId { get; set; }
        [NotMapped]
        public bool IsParent { get; set; }
        [NotMapped]
        public int Level { get; set; }
        [NotMapped]
        public int Index { get; set; }
        [NotMapped]
        public int DetailType { get; set; }
        [NotMapped]
        public string SubTitle { get; set; }
    }
}
