using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtDaDuToanQuery
    {
        public Guid Id { get; set; }
        public Guid IIdDuAnId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SNoiDung { get; set; }
        public double? FTongDuToanPheDuyet { get; set; }
        public bool? BIsGoc { get; set; }
        public bool BActive { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool BLaTongDuToan { get; set; }
        public Guid? IIdDuToanGocId { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string STenDuAn { get; set; }
        public string TenDonVi { get; set; }
        public Guid? Id_DonVi { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public string ThoiGianThucHien { get; set; }
        public double? FTongMucDauTuSauDieuChinh { get; set; }
        public int? SoLanDieuChinh { get; set; }
        public string DiaDiem { get; set; }
        public string TenDuToan { get; set; }
        public string LoaiDuToan { get; set; }
        public string Loai { get; set; }
        public int TotalFiles { get; set; }
        public string SMoTa { get; set; }
        public bool? BKhoa { get; set; }
        public Guid? IIdQddauTuId { get; set; }
    }
}
