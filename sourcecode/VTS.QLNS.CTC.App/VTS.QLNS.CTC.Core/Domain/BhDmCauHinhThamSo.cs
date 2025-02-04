using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhDmCauHinhThamSo : EntityBase
    {
        [Column("iID")]
        public override Guid Id { get; set; }  
        public string SMa { get; set; }
        public string STen { get; set; }
        public double? fGiaTri { get; set; }        
        public bool BTrangThai { get; set; }
        public string SMoTa {  get; set; }
        public int? INamLamViec { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgaySua { get; set; }

    }
}
