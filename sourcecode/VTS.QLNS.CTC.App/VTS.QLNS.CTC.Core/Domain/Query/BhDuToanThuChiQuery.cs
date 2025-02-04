using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDuToanThuChiQuery
    {
        [Column("iID_DTChungTu")]
        public Guid? Id { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }
        [Column("dNgayQuyetDinh")]
        public DateTime? DNgayQuyetDinh { get; set; }
        [Column("sNgayQuyetDinh")]
        public string SNgayQuyetDinh { get; set; }
        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }
        [Column("sNgayQuyetDinhDMY")]
        public string SNgayQuyetDinhDMY { get; set; }
        [Column("iLoaiDuToan")]
        public int ILoaiDuToan { get; set; }
    }
}
