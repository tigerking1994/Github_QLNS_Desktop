using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhKhTongThe : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdParentAdjustId { get; set; }
        public Guid? IIdGocId { get; set; }

        public int? IGiaiDoanTu { get; set; }
        public int? IGiaiDoanDen { get; set; }
        public int? IGiaiDoanTu_TTCP { get; set; }
        public int? IGiaiDoanDen_TTCP { get; set; }
        public int? IGiaiDoanTu_BQP { get; set; }
        public int? IGiaiDoanDen_BQP { get; set; }
        public int? INamKeHoach { get; set; }

        public string SSoKeHoachTtcp { get; set; }
        public DateTime? DNgayKeHoachTtcp { get; set; }
        public string SMoTaChiTietKhttcp { get; set; }
        public double FTongGiaTriKhttcp { get; set; }

        public string SSoKeHoachBqp { get; set; }
        public DateTime? DNgayKeHoachBqp { get; set; }
        public string SMoTaChiTietKhbqp { get; set; }
        public double FTongGiaTriKhbqp { get; set; }
        public double FTongGiaTriKhbqpVnd { get; set; }

        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }

        public bool BIsActive { get; set; }
        public bool BIsGoc { get; set; }
        public bool BIsKhoa { get; set; }
        public int ILanDieuChinh { get; set; }
        public int? ILoai { get; set; }
    }
}