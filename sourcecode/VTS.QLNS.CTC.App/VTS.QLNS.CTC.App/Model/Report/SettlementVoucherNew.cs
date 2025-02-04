using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class SettlementVoucherNew
    {

        [ColumnAttribute("L", 4, MLNSFiled.L)]
        public string L { get; set; }
        [ColumnAttribute("K", 5, MLNSFiled.K)]
        public string K { get; set; }
        [ColumnAttribute("m", 6, MLNSFiled.M)]
        public string M { get; set; }
        [ColumnAttribute("TM", 7, MLNSFiled.TM)]
        public string TM { get; set; }
        [ColumnAttribute("TTM", 8, MLNSFiled.TTM)]
        public string TTM { get; set; }
        [ColumnAttribute("NG", 9, MLNSFiled.NG)]
        public string NG { get; set; }
        [ColumnAttribute("TNG", 10, MLNSFiled.TNG)]
        public string TNG { get; set; }
        [ColumnAttribute("TNG1", 11, MLNSFiled.TNG1)]
        public string TNG1 { get; set; }
        [ColumnAttribute("TNG2", 12, MLNSFiled.TNG2)]
        public string TNG2 { get; set; }
        [ColumnAttribute("TNG3", 13, MLNSFiled.TNG3)]
        public string TNG3 { get; set; }
    }
}
