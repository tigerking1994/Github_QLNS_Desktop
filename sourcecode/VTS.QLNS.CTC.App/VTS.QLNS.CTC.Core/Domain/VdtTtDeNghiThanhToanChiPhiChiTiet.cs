using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class VdtTtDeNghiThanhToanChiPhiChiTiet : EntityBase
    {
        public override Guid Id { get; set; }
        public Guid? IIdDeNghiThanhToanChiPhiId { get; set; }
        public Guid? IIdNoiDungChi { get; set; }
        public double? FGiaTriDeNghi { get; set; }
        public string SGhiChu { get; set; }
    }
}
