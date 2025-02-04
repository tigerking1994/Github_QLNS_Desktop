using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKtKhoiTaoDuLieuChiTiet : EntityBase
    {
        public Guid Id { get; set; }
        public Guid IIdKhoiTaoDuLieuId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public int? ICoQuanThanhToan { get; set; }
        public string SMaDuAn { get; set; }
        public int? IIDNguonVonID { get; set; }
        // col 1
        public double? FKhvnVonBoTriHetNamTruoc { get; set; }
        // col 2
        public double? FKhvnLkvonDaThanhToanTuKhoiCongDenHetNamTruoc { get; set; }
        // col 3
        public double? FKhvnTrongDoVonTamUngTheoCheDoChuaThuHoi { get; set; }
        // col 4
        public double? FKhvnLkvonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc { get; set; }
        // col 5
        public double? FKhvnKeHoachVonKeoDaiSangNam { get; set; }
        // col 6
        public double? FKhutVonBoTriHetNamTruoc { get; set; }
        // col 7
        public double? FKhutLkvonDaThanhToanTuKhoiCongDenHetNamTruoc { get; set; }
        // col 8
        public double? FKhutTrongDoVonTamUngTheoCheDoChuaThuHoi { get; set; }
        // col 9
        public double? FKhutKeHoachUngTruocKeoDaiSangNam { get; set; }
        // col 10
        public double? FKhutKeHoachUngTruocChuaThuHoi { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        [NotMapped]
        public List<VdtKtKhoiTaoDuLieuChiTietThanhToan> lstContract { get; set; }
    }
}
