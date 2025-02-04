using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhThongTinNGuonVonQuery
    {
        public Guid Id { get; set; }
        public Guid IIdCacQuyetDinhId { get; set; }
        public int IIdNguonVonId { get; set; }
        public double FGiaTriNgoaiTeKhac { get; set; }
        public double FGiaTriUSD { get; set; }
        public double FGiaTriVND { get; set; }
        public double FGiaTriEUR { get; set; }
        public string STenNguonVon { get; set; }
    }
}
