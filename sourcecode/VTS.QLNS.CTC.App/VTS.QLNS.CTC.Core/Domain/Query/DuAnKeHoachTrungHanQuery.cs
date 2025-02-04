using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class DuAnKeHoachTrungHanQuery
    {
        public Guid? IIdKeHoach5NamChiTietId { get; set; }
        public Guid? IIdKeHoach5NamId { get; set; }
        public Guid IIdDuAnId { get; set; }
        public string SMaDuAn { get; set; }
        public string STenDuAn { get; set; }
        public string SKhoiCong { get; set; }
        public string SKetThuc { get; set; }
        public string SMaKetNoi { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public string IIdMaChuDauTu { get; set; }
        public string STenChuDauTu { get; set; }
        public string SDiaDiem { get; set; }
        public double? FHanMucDauTu { get; set; }
        public int ILoaiDuAn { get; set; }
        public string STenLoaiDuAn => LoaiDuAnEnum.Get(this.ILoaiDuAn);
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public Guid? IdDuAnNguonVon { get; set; }
        public int? IIdNguonVonId { get; set; }
        public string STenNguonVon { get; set; }
        public double? FGiaTriNamThuNhat { get; set; }
        public double? FGiaTriNamThuHai { get; set; }
        public double? FGiaTriNamThuBa { get; set; }
        public double? FGiaTriNamThuTu { get; set; }
        public double? FGiaTriNamThuNam { get; set; }
        public double? FGiaTriSau5Nam { get; set; }
        public double? FGiaTriKeHoach { get; set; }
        public string IIdMaDonVi { get; set; }
        public Guid? IIdDonViId { get; set; }
        public string STenDonVi { get; set; }
        public Guid? IIdDuAnHangMucId { get; set; }
        public Guid? IIdParentHangMuc { get; set; }
        public int? Loai { get; set; }
        public bool IsDuAnHangMuc { get; set; }
        public Guid? IdDuAnKhthDeXuat { get; set; }
        public string SMaOrder { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DDateCreate { get; set; }
    }
}
