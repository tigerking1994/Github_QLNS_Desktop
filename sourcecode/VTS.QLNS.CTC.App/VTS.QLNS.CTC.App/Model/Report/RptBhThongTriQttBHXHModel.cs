using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class RptBhThongTriQttBHXHModel : ModelBase
    {
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string TNG { get; set; }
        public string MoTa { get; set; }
        public long NguoiLD { get; set; }
        public long NguoiSDLD { get; set; }
        public long TongCong => NguoiLD + NguoiSDLD;
        public string SMaDonVi { get; set; }
        public Guid IIDMLNS { get; internal set; }
        public Guid IIDMLNSCha { get; internal set; }
    }
}
