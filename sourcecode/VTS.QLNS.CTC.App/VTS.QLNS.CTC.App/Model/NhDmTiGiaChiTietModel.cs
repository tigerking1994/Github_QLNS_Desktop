using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmTiGiaChiTietModel : ModelBase
    {
        public Guid IIdTiGiaId { get; set; }
        public Guid IIdTienTeId { get; set; }
        public string SMaTienTeQuyDoi { get; set; }
        public double? FTiGia { get; set; }
    }
}
