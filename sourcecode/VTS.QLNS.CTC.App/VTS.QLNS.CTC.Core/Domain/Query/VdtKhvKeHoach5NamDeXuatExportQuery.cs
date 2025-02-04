using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvKeHoach5NamDeXuatExportQuery
    {
        public string STT { get; set; }
        public Guid IdChiTiet { get; set; }
        public Guid? IdChungTu { get; set; }
        public int? NamLamViec { get; set; }
        public int? ILoai { get; set; }
        public Guid? IdParentVoucher { get; set; }
        public bool? BActive { get; set; }
        public bool? IsGoc { get; set; }
        public Guid? IIdDonViChungTu { get; set; }
        public string SSoKeHoach { get; set; }
        public int IGiaiDoanTu { get; set; }
        public int IGiaiDoanDen { get; set; }
        public Guid? IIdDuAn { get; set; }
        public string IdMaDonVi { get; set; }
        public Guid? IIdParentModified { get; set; }
        public string STen { get; set; }
        public string SDiaDiem { get; set; }
        public Guid? IIdDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SThoiGianThucHien { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public double? FHanMucDauTu { get; set; }
        public int? IIdNguonVon { get; set; }
        public string STenNguonVon { get; set; }
        public double? FGiaTriNamThuNhat { get; set; }
        public double? FGiaTriNamThuHai { get; set; }
        public double? FGiaTriNamThuBa { get; set; }
        public double? FGiaTriNamThuTu { get; set; }
        public double? FGiaTriNamThuNam { get; set; }
        public double? FGiaTriBoTri { get; set; }
        public string SGhiChu { get; set; }
        public string SMaLoaiCongTrinh { get; set; }
        public string SMaOrder { get; set; }
        public Guid? IdReference { get; set; }
        public double FTongSo
        {
            get
            {
                return (FGiaTriNamThuNhat ?? 0) + (FGiaTriNamThuHai ?? 0) + (FGiaTriNamThuBa ?? 0) + (FGiaTriNamThuTu ?? 0) + (FGiaTriNamThuNam ?? 0);
            }
        }
        public double FTongSoNhuCau
        {
            get
            {
                if (IIdNguonVon.HasValue &&
                    IIdNguonVon.Value.Equals((int)NSNguonNganSachEnum.Type.NGAN_SACH_QP))
                {
                    return (FGiaTriBoTri ?? 0) + FTongSo;
                }
                return 0;
            }
        }
        public Guid? IdParent { get; set; }
        public bool IsParent { get; set; }
        public int IsStatus { get; set; }
        public int? Level { get; set; }
        public int? IndexCode { get; set; }
        public string STongHop { get; set; }
        [NotMapped]
        public bool IsHangCha { get; set; }
    }
}
