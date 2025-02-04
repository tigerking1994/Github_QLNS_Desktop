using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvKeHoach5NamDeXuatChiTietQuery
    {
        public string STT { get; set; }
        public Guid? Id { get; set; }
        public Guid? IIdKeHoach5NamId { get; set; }
        public string STen { get; set; }
        public string SDiaDiem { get; set; }
        public int? IGiaiDoanTu { get; set; }
        public int? IGiaiDoanDen { get; set; }
        public Guid? IIdDonViId { get; set; }
        public string STenDonVi { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public int? IIdNguonVonId { get; set; }
        public string STenNguonVon { get; set; }
        public double? FHanMucDauTu { get; set; }
        public double? FVonNSQPLuyKe { get; set; }
        public double? FVonNSQP { get; set; }
        public double? FGiaTriNamThuNhat { get; set; }
        public double? FGiaTriNamThuHai { get; set; }
        public double? FGiaTriNamThuBa { get; set; }
        public double? FGiaTriNamThuTu { get; set; }
        public double? FGiaTriNamThuNam { get; set; }
        public double? FGiaTriBoTri { get; set; }
        public double? FGiaTriKeHoach { get; set; }
        public double? FTongSoNhuCauNSQP { get; set; }
        public double? FGiaTriNamThuNhatOrigin { get; set; }
        public double? FGiaTriNamThuHaiOrigin { get; set; }
        public double? FGiaTriNamThuBaOrigin { get; set; }
        public double? FGiaTriNamThuTuOrigin { get; set; }
        public double? FGiaTriNamThuNamOrigin { get; set; }
        public double? FGiaTriBoTriOrigin { get; set; }
        public double? FGiaTriKeHoachOrigin { get; set; }
        public double? FTongSoNhuCauNSQPOrigin { get; set; }
        public string SGhiChu { get; set; }
        public bool? IsHangCha { get; set; } 
        public int? Level { get; set; }
        public int? IndexCode { get; set; }
        public string SMaOrder { get; set; }
        public Guid? IdReference { get; set; }
        public bool? IsParent { get; set; }
        public Guid? IdParent { get; set; }
        public Guid? IdParentModified { get; set; }
        public int? IsStatus { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string IIdMaDonVi { get; set; }
        [NotMapped]
        public Guid? IIdTongHop { get; set; }
    }
}
