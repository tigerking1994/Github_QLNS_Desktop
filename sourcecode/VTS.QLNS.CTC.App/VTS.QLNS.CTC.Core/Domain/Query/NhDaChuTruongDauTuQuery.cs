using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaChuTruongDauTuQuery
    {
        public virtual Guid Id { get; set; }
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public int? ILoai { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public Guid? IIdCapPheDuyetId { get; set; }
        public Guid? IIdTiGiaUsdVndId { get; set; }
        public Guid? IIdTiGiaUsdEurId { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public string SKhoiCong { get; set; }
        public string SKetThuc { get; set; }
        public string SDiaDiem { get; set; }
        public string SMucTieu { get; set; }
        public string SQuyMo { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool? BIsActive { get; set; }
        public bool? BIsGoc { get; set; }
        public bool? BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public bool? BIsXoa { get; set; }
        public string STenDonVi { get; set; }
        public string STenDuAn { get; set; }
        public int TotalFiles { get; set; }
        public string SDieuChinhTu { get; set; }
        public Guid? iIDNhiemVuChiID { get; set; }
        public Guid? IIdKHTongTheID { get; set; }
        public string STenChuongTrinh { get; set; }
    }
}
