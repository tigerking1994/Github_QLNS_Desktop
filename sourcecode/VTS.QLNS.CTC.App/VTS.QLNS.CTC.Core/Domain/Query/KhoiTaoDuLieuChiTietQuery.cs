using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class KhoiTaoDuLieuChiTietQuery
    {
        public Guid Id { get; set; }
        public Guid IdDb { get; set; }
        public Guid IID_KhoiTaoDuLieuID { get; set; }
        public Guid IID_DuAnID { get; set; }
        public int? IIDNguonVonID { get; set; }
        public int? ICoQuanThanhToan { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public string SMaLoaiCongTrinh { get; set; }
        public string SMaDuAn { get; set; }
        public string TenDuAn { get; set; }
        public double? FKHVN_VonBoTriHetNamTruoc { get; set; }
        public double? FKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc { get; set; }
        public double? FKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi { get; set; }
        public double? FKHVN_LKVonTamUngTheoCheDoChuaThuHoiNopDieuChinhGiamDenHetNamTruoc { get; set; }
        public double? FKHVN_KeHoachVonKeoDaiSangNam { get; set; }

        public double? FKHUT_VonBoTriHetNamTruoc { get; set; }
        public double? FKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc { get; set; }
        public double? FKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi { get; set; }
        public double? FKHUT_KeHoachUngTruocKeoDaiSangNam { get; set; }
        public double? FKHUT_KeHoachUngTruocChuaThuHoi { get; set; }
    }
}
