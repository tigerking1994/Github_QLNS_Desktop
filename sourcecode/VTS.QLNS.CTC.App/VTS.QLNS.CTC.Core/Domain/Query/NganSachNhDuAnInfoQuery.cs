using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NganSachNhDuAnInfoQuery
    {
        public decimal? SoTienUsd { get; set; }
        public decimal? SoTienEur { get; set; }
        public decimal? SoTienVnd { get; set; }
        public decimal? SoTienNgoaiTeKhac { get; set; }
        public int IdNguonVon { get; set; }
        public string Ten { get; set; }
    }
}
