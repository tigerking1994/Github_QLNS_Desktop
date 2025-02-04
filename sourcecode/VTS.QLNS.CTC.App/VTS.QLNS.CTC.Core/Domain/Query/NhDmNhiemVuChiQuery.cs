using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDmNhiemVuChiQuery
    {
        public int Level { get; set; }
        public Guid Id { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SMaNhiemVuChi { get; set; }
        public string SMaNhiemVuChiParent { get; set; }
        public string STenNhiemVuChi { get; set; }
        public string SMoTaChiTiet { get; set; }
        public int? ILoaiNhiemVuChi { get; set; }
        public Guid? IIdKHTTNhiemVuChiId { get; set; }
        public double? FKeHoachTtcpUsd { get; set; }
        public double? FKeHoachBqpUsd { get; set; }
        public double? FKeHoachBqpVnd { get; set; }

        [NotMapped]
        public int Index { get; set; }
        [NotMapped]
        public string SMaOrder { get; set; }
        public string STT { get; set; }
    }
}
