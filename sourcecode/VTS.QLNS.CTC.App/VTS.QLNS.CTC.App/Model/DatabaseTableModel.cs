using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class DatabaseTableModel
    {
        public PackIconKind Icon { get; set; }
        public string TableName { get; set; }
        public bool IsFilter { get; set; }
    }
}
