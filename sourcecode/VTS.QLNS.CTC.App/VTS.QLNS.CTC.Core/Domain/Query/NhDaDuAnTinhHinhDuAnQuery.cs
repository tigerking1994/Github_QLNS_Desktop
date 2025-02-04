using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaDuAnTinhHinhDuAnQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        public string SMaDuAn { get; set; }
        public string STenDuAn { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public Guid? IIdChuDauTuId { get; set; }
        public string IIdMaChuDauTu { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdCapPheDuyetId { get; set; }
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
        public string sTenChuDauTu { get; set; }
        public string sSoQuyetDinhDauTu { get; set; }
        public string sPhanCap { get; set; }       
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public double? TongMucDauTuEur { get; set; }
        public double? TongMucDauTuUsd { get; set; }
        public double? TongMucDauTuVnd { get; set; }
        public double? TongMucDauTuNgoaiTeKhac { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayThangNam { get; set; }
        public DateTime? dNgayQuyetDinhDauTu { get; set; }

    }
}
