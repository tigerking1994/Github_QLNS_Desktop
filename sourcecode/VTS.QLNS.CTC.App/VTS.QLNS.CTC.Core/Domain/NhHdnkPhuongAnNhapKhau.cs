using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhHdnkPhuongAnNhapKhau : EntityBase
    {
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        public Guid? IIdQddauTuId { get; set; }
        public Guid? IIdChuTruongDauTuId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public Guid? IIdPhuongAnNhapKhauGocId { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool BIsActive { get; set; }
        public bool BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public bool BIsGoc { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool BIsXoa { get; set; }
        public int? ILoai { get; set; }
        public string SLoaiSoCu { get; set; }

        //Another properties
        [NotMapped]
        public string STenDonVi { get; set; }
        [NotMapped]
        public string STenChuongTrinh { get; set; }
        [NotMapped]
        public string SDieuChinhTu { get; set; }
        [NotMapped]
        public Guid IIdKhTongTheId { get; set; }
        [NotMapped]
        public IEnumerable<NhDaGoiThau> NhDaGoiThaus { get; set; }
    }
}
