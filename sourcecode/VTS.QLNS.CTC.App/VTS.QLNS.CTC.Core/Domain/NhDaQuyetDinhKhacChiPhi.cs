using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhDaQuyetDinhKhacChiPhi : EntityBase
    {
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public Guid? IIdDmChiPhiId { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdQuyetDinhKhacId { get; set; }
        public string SMaOrder { get; set; }
        public string STenChiPhi { get; set; }
        [NotMapped]
        public bool IsHasChildren { get; set; }
        [NotMapped]
        public bool IsEnableEdit => !this.Id.IsNullOrEmpty();
        [NotMapped]
        public bool IsHangCha => this.IIdParentId.IsNullOrEmpty();
    }
}
