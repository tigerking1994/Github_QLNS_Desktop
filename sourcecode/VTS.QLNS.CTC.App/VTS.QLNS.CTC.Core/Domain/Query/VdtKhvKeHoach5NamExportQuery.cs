using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvKeHoach5NamExportQuery
    {
        public Guid? Id { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string STenDuAn { get; set; }
        public string SMaDuAn { get; set; }
        public int IGiaiDoanTu { get; set; }
        public int IGiaiDoanDen { get; set; }
        public string SSoKeHoach { get; set; }
        public string SDiaDiem { get; set; }
        public string ThoiGianThucHien { get; set; }
        public string SGhiChu { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public int? IIdNguonVonId { get; set; }
        public string STenNguonVon { get; set; }
        public double? FHanMucDauTu { get; set; }
        public double? FTongSoNhuCauNSQP { get; set; }
        public double? FVonDaGiao { get; set; }
        public double? FVonBoTriTuNamDenNam { get; set; }
        public double? FGiaTriSau5Nam { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IdChungTu { get; set; }
        public Guid? IdChungTuParent { get; set; }
        public Guid? IIdDonViId { get; set; }
        public bool? BActive { get; set; }
        public bool? IsGoc { get; set; }
        public int? ILoaiChungTu { get; set; }
        public int? IGiaiDoanTuCT { get; set; }
        public int? IGiaiDoanDenCT { get; set; }
        public int INamLamViec { get; set; }
        public Guid? IIdDonViThucHienDuAn { get; set; }
        public string IIdMaDonViThucHienDuAn { get; set; }
    }
}
