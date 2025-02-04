using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaGoiThau : EntityBase
    {
        [Column("iID_GoiThauID")]
        public override Guid Id { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string SMaGoiThau { get; set; }
        public string STenGoiThau { get; set; }
        public string LoaiGoiThau { get; set; }
        public string SHinhThucChonNhaThau { get; set; }
        public string SPhuongThucDauThau { get; set; }
        public DateTime? DBatDauChonNhaThau { get; set; }
        public DateTime? DKetThucChonNhaThau { get; set; }
        public string SHinhThucHopDong { get; set; }
        public string SThoiGianThucHien { get; set; }
        public DateTime? DNgayLap { get; set; }
        public Guid? IIdGoiThauGocId { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool? BIsGoc { get; set; }
        public bool? BActive { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public double? FTienTrungThau { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGia { get; set; }
        public double? FGiaTri { get; set; }
        public bool? BIsKhoiTao { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public Guid? IIdKhlcnhaThau { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public bool? BKhoa { get; set; }
    }
}
