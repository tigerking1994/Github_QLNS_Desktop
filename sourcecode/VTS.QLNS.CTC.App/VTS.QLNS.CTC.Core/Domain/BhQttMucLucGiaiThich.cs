using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhQttMucLucGiaiThich
    {
        [Column("iID_MLGT")]
        [Key]
        public Guid IIDMLGT { get; set; }
        public int ISTT { get; set; }
        public string SNoiDung { get; set; }
        public int ILoai { get; set; }
        public string SMaGiaiThich { get; set; }
    }
}
