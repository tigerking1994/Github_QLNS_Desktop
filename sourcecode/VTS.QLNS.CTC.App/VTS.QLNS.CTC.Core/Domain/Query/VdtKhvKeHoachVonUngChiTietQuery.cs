using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvKeHoachVonUngChiTietQuery
    {
        public Guid? iID_DuAnID { get; set; }
        public string sTrangThaiDuAnDangKy { get; set; }
        public Guid? iID_DonViTienTeID { get; set; }
        public Guid? iID_TienTeID { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public double? fTiGia { get; set; }
        public string sGhiChu { get; set; }
        public string sMaKetNoi { get; set; }
        public string sTenDuAn { get; set; }
        public string sMaDuAn { get; set; }
        public Guid? iID_MucID { get; set; }
        public Guid? iID_TieuMucID { get; set; }
        public Guid? iID_TietMucID { get; set; }
        public Guid? iID_NganhID { get; set; }
        public double? fTonKhoanTaiDonVi { get; set; }
        public double? fCapPhatBangLenhChi { get; set; }
        public double? fCapPhatTaiKhoBac { get; set; }
        public double? fTongMucDauTuPheDuyet { get; set; }
        public double fGiaTriDeNghi { get; set; }
        public string sLNS { get; set; }
        public string sL { get; set; }
        public string sK { get; set; }
        public string sM { get; set; }
        public string sTM { get; set; }
        public string sTTM { get; set; }
        public string sNG { get; set; }
        public Guid? ID_DuAn_HangMuc { get; set; }
        public string sTenHangMuc { get; set; }
    }
}
