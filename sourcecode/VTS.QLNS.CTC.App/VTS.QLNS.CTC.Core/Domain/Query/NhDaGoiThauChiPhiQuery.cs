using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaGoiThauChiPhiQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public Guid? IIdQddauTuChiPhiId { get; set; }
        public Guid? IIdDuToanChiPhiId { get; set; }
        public double? FTienGoiThauUsd { get; set; }
        public double? FTienGoiThauVnd { get; set; }
        public double? FTienGoiThauEur { get; set; }
        public double? FTienGoiThauNgoaiTeKhac { get; set; }
        public string STenChiPhiQDDT { get; set; }
        public string STenChiPhiDT { get; set; }
    }
}
