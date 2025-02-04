using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDmNhiemVuChi : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public string SMaNhiemVuChi { get; set; }
        public string STenNhiemVuChi { get; set; }
        public string SMoTaChiTiet { get; set; }
        public int? ILoaiNhiemVuChi { get; set; }
        public string SMaOrder { get; set; }
        public Guid? IIdParentId { get; set; }
        public ICollection<NhKhTongTheNhiemVuChi> NhKhTongTheNhiemVuChis { get; set; }
    }
}
