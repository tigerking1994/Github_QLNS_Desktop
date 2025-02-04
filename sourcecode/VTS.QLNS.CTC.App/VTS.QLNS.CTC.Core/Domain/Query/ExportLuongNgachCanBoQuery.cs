using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ExportLuongNgachCanBoQuery
    {
        public string Tencb { get; set; }
        public string Parent { get; set; }
    }
}
