using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtDaGoiThauQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string STenDuAn { get; set; }
        public string SMaGoiThau { get; set; }
        public string STenGoiThau { get; set; }
        public string LoaiGoiThau { get; set; }
        public string SHinhThucChonNhaThau { get; set; }
        public string SPhuongThucDauThau { get; set; }
        public string SHinhThucHopDong { get; set; }
        public string SThoiGianThucHien { get; set; }
        public DateTime? DNgayLap { get; set; }
        public Guid? IIdGoiThauGocId { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool? BIsGoc { get; set; }
        public bool? BActive { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public string TenNhaThau { get; set; }
        public double? FTienTrungThau { get; set; }
        public string TenDonVi { get; set; }
        public string Id_DonVi { get; set; }
        public string SDiaDiem { get; set; }
        public string ThoiGianThucHien { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public int? SoLanDieuChinh { get; set; }
        public double? FTongTienSauDieuChinh { get; set; }
        public DateTime? DBatDauChonNhaThau { get; set; }
        public DateTime? DKetThucChonNhaThau { get; set; }
        public string STenNhomQuanLy { get; set; }
        public string SSuCanThietDauTu { get; set; }
        public string SMucTieu { get; set; }
        public string SDienTichSuDungDat { get; set; }
        public string SNguonGocSuDungDat { get; set; }
        public string SQuyMo { get; set; }
        public string STenNhomDuAn { get; set; }
        public string STenHinhThucQuanLy { get; set; }
        public string SoQuyetDinh { get; set; }
        //public string TenDuToan { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public double? FTongMucDauTu { get; set; }
        public Guid? IIdKhlcnhaThau { get; set; }
        public string SUserCreate { get; set; }
        public bool? BKhoa { get; set; }
    }
}
