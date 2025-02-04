using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class DataDieuChinhQuery
    {
        [Column("iID_MLNS")]
        public Guid IIdMlns { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid? IIdMlnsCha { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("DuToanNganSachNam")]
        public double? FDuToanNganSachNam { get; set; }
        [Column("DuKienQtDauNam")]
        public double? FDuKienQtDauNam { get; set; }
        [Column("DuKienQtCuoiNam")]
        public double? FDuKienQtCuoiNam { get; set; }
    }
}
