using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDuToanTongHopThuChiQuery
    {
        [Column("rowNum")]
        public int IRowNum { get; set; }
        [Column("stt")]
        public string STT { get; set; }
        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("fDuToan")]
        public double? FDuToan { get; set; }
        [Column("fHachToan")]
        public double? FHachToan { get; set; }
        [Column("fTongSoChi")]
        public double? FTongSoChi { get; set; }
        [Column("fNLD")]
        public double? FNLD { get; set; }
        [Column("fNSD")]
        public double? FNSD { get; set; }
        [Column("fTongSo")]
        public double? FTongSo { get; set; }
    }
}
