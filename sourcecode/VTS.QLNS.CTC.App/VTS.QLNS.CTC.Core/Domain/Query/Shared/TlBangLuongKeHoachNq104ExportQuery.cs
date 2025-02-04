using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query.Shared
{
    public class TlBangLuongKeHoachNq104ExportQuery
    {
        public Guid Id { get; set; }
        public Guid IIdParent { get; set; }
        public string SNoiDung { get; set; }
        public string SXauNoiMa { get; set; }
        public string STT { get; set; }
        public int ILevel { get; set; }
        public int IThuTu { get; set; }
        public double QSBQ { get; set; }
        public double LHT_TT { get; set; }
        public double PCCV_TT { get; set; }
        public double PCTN_TT { get; set; }
        public double PCTNVK_TT { get; set; }
        public double HSBL_TT { get; set; }
        public double FTongSo => LHT_TT + PCCV_TT + PCTN_TT + PCTNVK_TT + HSBL_TT;
    }
}
