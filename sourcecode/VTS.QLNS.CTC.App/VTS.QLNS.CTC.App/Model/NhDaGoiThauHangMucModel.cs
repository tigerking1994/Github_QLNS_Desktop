using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaGoiThauHangMucModel : CurrencyDetailModelBase
    {
        public Guid? IIdGoiThauChiPhiId { get; set; }
        public Guid? IIdParentId { get; set; }
        public double? FTienGoiThauUsd { get; set; }
        public double? FTienGoiThauVnd { get; set; }
        public double? FTienGoiThauEur { get; set; }
        public double? FTienGoiThauNgoaiTeKhac { get; set; }
        public string SMaHangMuc { get; set; }
        public string STenHangMuc { get; set; }
        public string SMaOrder { get; set; }
        public Guid? IdChiPhi { get; set; }
        public Guid? IIdDuToanHangMucId { get; set; }
        public Guid? IIdQdDauTuHangMucId { get; set; }
        public Guid? IIdCacQuyetDinhHangMucId { get; set; }
        public bool IsSuggestion { get; set; }
    }
}
