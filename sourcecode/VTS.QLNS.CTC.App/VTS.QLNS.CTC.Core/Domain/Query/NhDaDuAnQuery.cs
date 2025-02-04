using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaDuAnQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        public string SMaDuAn { get; set; }
        public string STenDuAn { get; set; }
        public int? ILoai { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public Guid? IIdCapPheDuyetId { get; set; }
        public string IIdMaChuDauTu { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public string SKhoiCong { get; set; }
        public string SKetThuc { get; set; }
        public bool? BIsDuPhong { get; set; }
        public string SDiaDiem { get; set; }
        public string SMucTieu { get; set; }
        public string SQuyMo { get; set; }
        public Guid? IIdTiGiaUsdEurid { get; set; }
        public Guid? IIdTiGiaUsdVndid { get; set; }
        public Guid? IIdTiGiaUsdNgoaiTeKhacId { get; set; }
        public double? FUsd { get; set; }
        public double? FNgoaiTeKhac { get; set; }
        public double? FVnd { get; set; }
        public double? FEur { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public Guid? IIdChuTruongDauTuId { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }

        public string STenDonVi { get; set; }
        public string STenPheDuyet { get; set; }
        public string STenChuDauTu { get; set; }
        public int? TotalFiles { get; set; }
        public Guid? IIdKHTTNhiemVuChiId { get; set; }
        public Guid? iIDNhiemVuChiID { get; set; }
        public Guid? IIdKHTongTheID { get; set; }
        public string STenChuongTrinh { get; set; }
    }
}
