using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKhvKeHoach5NamDeXuatChiTiet : EntityBase
    {
        public Guid? IIdKeHoach5NamId { get; set; }
        public double FGiaTriKeHoach { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double FTiGia { get; set; }
        public string STrangThai { get; set; }
        public string SGhiChu { get; set; }
        public double? FGiaTriNamThuNhat { get; set; }
        public double? FGiaTriNamThuHai { get; set; }
        public double? FGiaTriNamThuBa { get; set; }
        public double? FGiaTriNamThuTu { get; set; }
        public double? FGiaTriNamThuNam { get; set; }
        public int? IIdNguonVonId { get; set; }
        public double? FGiaTriBoTri { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public double? FVonDaGiao { get; set; }
        public double? FVonBoTriTuNamDenNam { get; set; }
        public double? FHanMucDauTu { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string STen { get; set; }
        public Guid? IdReference { get; set; }
        public int? IGiaiDoanTu { get; set; }
        public int? IGiaiDoanDen { get; set; }
        public string SDiaDiem { get; set; }
        public Guid? IdParent { get; set; }
        public string SMaOrder { get; set; }
        public string STT { get; set; }
        public int? Level { get; set; }
        public int? IndexCode { get; set; }
        public bool IsParent { get; set; }
        public int IsStatus { get; set; }
        public double? FGiaTriNamThuNhatDc { get; set; }
        public double? FGiaTriNamThuHaiDc { get; set; }
        public double? FGiaTriNamThuBaDc { get; set; }
        public double? FGiaTriNamThuTuDc { get; set; }
        public double? FGiaTriNamThuNamDc { get; set; }
        public double? FGiaTriBoTriDc { get; set; } 
        public Guid? IdParentModified { get; set; }
        public bool? BActive { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string IIdMaDonVi { get; set; }
        public Guid? IIdTongHop { get; set; }

        [NotMapped]
        public Guid IdModified { get; set; }
        [NotMapped]
        public Guid? IdMParent { get; set; }
        [NotMapped]
        public Guid? IdReferenceModified { get; set; }
        //[NotMapped]
        //public Guid? IIdDuAn { get; set; }
    }
}
