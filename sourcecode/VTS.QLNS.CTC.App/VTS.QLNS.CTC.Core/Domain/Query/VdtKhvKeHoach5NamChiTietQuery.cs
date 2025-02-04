using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvKeHoach5NamChiTietQuery
    {
        public Guid? Id { get; set; }
        public Guid? IIdKeHoach5NamId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string STT { get; set; }
        public string SMaDuAn { get; set; }
        public string STenDuAn { get; set; }
        public int IGiaiDoanTu { get; set; }
        public int IGiaiDoanDen { get; set; }
        public string SDiaDiem { get; set; }
        public string ThoiGianThucHien { get; set; }
        public string SGhiChu { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public string IIdMaDonVi { get; set; }
        public Guid? IIdDonViId { get; set; }
        public string STenDonVi { get; set; }
        public int? IIdNguonVonId { get; set; }
        public string STenNguonVon { get; set; }
        public double? FHanMucDauTu { get; set; }
        public double? FVonDaGiao { get; set; }
        public double? FVonDaGiaoOrigin { get; set; }
        public double? FVonBoTriTuNamDenNam { get; set; }
        public double? FVonBoTriTuNamDenNamOrigin { get; set; }
        public double? FGiaTriSau5Nam { get; set; }
        public double? FGiaTriSau5NamOrigin { get; set; }
        public double? FVonBoTriNamTruoc { get; set; }
        public double? FVonDaBoTriNamNay { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool? BActive { get; set; }
    }
}
