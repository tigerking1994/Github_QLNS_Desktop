using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhHdnkCacQuyetDinhChiPhiDmChiPhiQuery
    {
        public Guid? Id { get; set; }
        public Guid? IIdCacQuyetDinhId { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriEur { get; set; }
        public double? FGiaTriVnd { get; set; }
        public string STenChiPhi { get; set; }
        public string SMaOrder { get; set; }
        public string STenDanhMucChiPhi { get; set; }
        public Guid? IIdParentId { get; set; }
    }
}
