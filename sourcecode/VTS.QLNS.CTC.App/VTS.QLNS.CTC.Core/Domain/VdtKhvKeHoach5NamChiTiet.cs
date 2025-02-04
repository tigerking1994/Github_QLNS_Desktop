using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKhvKeHoach5NamChiTiet : EntityBase
    {
        [Column("iID_KeHoach5Nam_ChiTietID")]
        public override Guid Id { get; set; }
        public Guid? IIdKeHoach5NamId { get; set; }
        public Guid IIdDuAnId { get; set; }
        //public double? FGiaTriNamThuNhat { get; set; }
        //public double? FGiaTriNamThuHai { get; set; }
        //public double? FGiaTriNamThuBa { get; set; }
        //public double? FGiaTriNamThuTu { get; set; }
        //public double? FGiaTriNamThuNam { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonVi { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public int? IIdNguonVonId { get; set; }
        //public Guid? IIdDonViTienTeId { get; set; }
        //public double? FTiGiaDonVi { get; set; }
        //public Guid? IIdTienTeId { get; set; }
        //public double FTiGia { get; set; }
        public string STrangThai { get; set; }
        public string STT { get; set; }
        public string SGhiChu { get; set; }
        public double? FGiaTriBoTri { get; set; }
        public double? FGiaTriBoTriDc { get; set; }
        //public Guid? IIDKeHoach5NamDuocDuyet { get; set; }
        public double? FVonDaGiao { get; set; }
        public double? FVonDaGiaoDc { get; set; }
        public double? FVonBoTriTuNamDenNam { get; set; }
        public double? FVonBoTriTuNamDenNamDc { get; set; }
        public double? FHanMucDauTu { get; set; }
        public double? FGiaTriKeHoach { get; set; }
        public double? FGiaTriDeXuat { get; set; }
        public string STen { get; set; }
        public Guid? IdParent { get; set; }
        public bool? BActive { get; set; }
    }
}
