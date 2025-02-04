using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhKhChiTiet :EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }

        public Guid? IIdParentId { get; set; }
        public Guid? IIdParentAdjustId { get; set; }
        public Guid? IIdGocId { get; set; }
        //public Guid IIdKhTongTheNhiemVuChiId { get; set; }
        public string SSoKeHoach { get; set; }
        public DateTime? DNgayKeHoach { get; set; }
        public string SMoTaChiTiet { get; set; }
        public Double? FGiaTriNgoaiTeKhac { get; set; }
        public Double? FGiaTriUsd { get; set; }
        public Double? FGiaTriVnd { get; set; }
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
    }
}
