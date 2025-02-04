using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvKeHoachVonUngDxChiTietQuery
    {
        public string sMaDuAn { get; set; }
        public string sTenDuAn { get; set; }
        public Guid? iID_DuAnID { get; set; }
        public string sTrangThaiDuAnDangKy { get; set; }
        public double? fTongMucDauTuPheDuyet { get; set; }
        public double fGiaTriDeNghi { get; set; }
        public Guid? iID_DonViTienTeID { get; set; }
        public Guid? iID_TienTeID { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public double? fTiGia { get; set; }
        public string sGhiChu { get; set; }
        public string sTenDonVi { get; set; }
        public Guid? iID_DonViQuanLyID { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public string sTenHangMuc { get; set; }
        public Guid? ID_DuAn_HangMuc { get; set; }
        public string iID_NguonVonID { get; set; }
    }
}
