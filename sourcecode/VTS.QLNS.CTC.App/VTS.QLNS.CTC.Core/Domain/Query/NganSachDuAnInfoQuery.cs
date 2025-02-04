using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NganSachDuAnInfoQuery
    {
        public decimal? SoTien { get; set; }
        public int IdNguonVon { get; set; }
        public string Ten { get; set; }
    }
}
