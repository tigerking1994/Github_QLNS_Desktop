using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhDmChiPhi : EntityBase
    {
        [NotMapped]
        public override Guid Id { get; set; }
        public Guid IIdChiPhi { get; set; }
        public string SMaChiPhi { get; set; }
        public string STenVietTat { get; set; }
        public string STenChiPhi { get; set; }
        public string SMoTa { get; set; }
        public int IThuTu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SIdMaNguoiDungTao { get; set; }
        public bool? BHangCha { get; set; }
    }
}
