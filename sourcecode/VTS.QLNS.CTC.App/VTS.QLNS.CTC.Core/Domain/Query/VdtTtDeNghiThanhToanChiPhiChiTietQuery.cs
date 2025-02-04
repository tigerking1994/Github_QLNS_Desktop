using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtTtDeNghiThanhToanChiPhiChiTietQuery
    {
        public Guid? Id { get; set; }
        public Guid? IIdParent { get; set; }
        public Guid? IIdNoiDungChi { get; set; }
        public string SMaOrder { get; set; }
        public string SNoiDungChi { get; set; }
        public double FGiaTriPheDuyet { get; set; }
        public double FGiaTriDeNghi { get; set; }
        public string SGhiChu { get; set; }
    }
}
