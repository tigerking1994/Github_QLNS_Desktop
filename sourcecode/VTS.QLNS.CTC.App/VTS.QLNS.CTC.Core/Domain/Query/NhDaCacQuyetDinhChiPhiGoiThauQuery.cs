using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaCacQuyetDinhChiPhiGoiThauQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdCacQuyetDinhChiPhiId { get; set; }
        public Guid? IIdQDDauTuChiPhiId { get; set; }
        public Guid? IIdDuToanChiPhiId { get; set; }
        public Guid? IIdGoiThauID { get; set; }
        public double? FTienGoiThauUsd { get; set; }
        public double? FTienGoiThauVnd { get; set; }
        public double? FTienGoiThauEur { get; set; }
        public double? FTienGoiThauNgoaiTeKhac { get; set; }
        public double? FGiaTriUSD { get; set; }
        public double? FGiaTriVND { get; set; }
        public double? FGiaTriEUR { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public string STenChiPhi { get; set; }
        public string SMaOrder { get; set; }
        public Guid? IIdParentId { get; set; }
    }
}
