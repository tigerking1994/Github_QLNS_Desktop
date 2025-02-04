using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQttQuarterQuery
    {
        public BhQttQuarterQuery(int IQuy, int ILoai, string MoTa)
        {
            IQuyNam = IQuy;
            IQuyNamLoai = ILoai;
            SQuyNamMoTa = MoTa;
        }

        [Column("iQuyNam")]
        public int IQuyNam { get; set; }
        [Column("iQuyNamLoai")]
        public int IQuyNamLoai { get; set; }
        [Column("sQuyNamMoTa")]
        public string SQuyNamMoTa { get; set; }
    }
}
