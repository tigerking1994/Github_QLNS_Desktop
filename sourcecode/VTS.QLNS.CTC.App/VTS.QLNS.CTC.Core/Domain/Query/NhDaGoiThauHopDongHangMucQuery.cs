using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaGoiThauHopDongHangMucQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdGoiThauChiPhiId { get; set; }
        public double? FTienGoiThauUsd { get; set; }
        public double? FTienGoiThauVnd { get; set; }
        public double? FTienGoiThauEur { get; set; }
        public double? FTienGoiThauNgoaiTeKhac { get; set; }
        public string SMaHangMuc { get; set; }
        public string STenHangMuc { get; set; }
        public string STenChiPhi { get; set; }
        public string SMaOrder { get; set; }
        public Guid? IIdParentId { get; set; }
    }
}
