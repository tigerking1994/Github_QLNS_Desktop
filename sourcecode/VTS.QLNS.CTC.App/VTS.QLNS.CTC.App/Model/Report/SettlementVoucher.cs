using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class SettlementVoucher
    {

        [ColumnAttribute("L", 3, MLNSFiled.L)]
        public string L { get; set; }
        [ColumnAttribute("K", 4, MLNSFiled.K)]
        public string K { get; set; }
        [ColumnAttribute("m", 5, MLNSFiled.M)]
        public string M { get; set; }
        [ColumnAttribute("TM", 6, MLNSFiled.TM)]
        public string TM { get; set; }
        [ColumnAttribute("TTM", 7, MLNSFiled.TTM)]
        public string TTM { get; set; }
        [ColumnAttribute("NG", 8, MLNSFiled.NG)]
        public string NG { get; set; }
        [ColumnAttribute("TNG", 9, MLNSFiled.TNG)]
        public string TNG { get; set; }
        [ColumnAttribute("TNG1", 10, MLNSFiled.TNG1)]
        public string TNG1 { get; set; }
        [ColumnAttribute("TNG2", 11, MLNSFiled.TNG2)]
        public string TNG2 { get; set; }
        [ColumnAttribute("TNG3", 12, MLNSFiled.TNG3)]
        public string TNG3 { get; set; }
    }
}
