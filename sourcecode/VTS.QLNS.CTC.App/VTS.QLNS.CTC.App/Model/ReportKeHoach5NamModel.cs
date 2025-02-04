using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ReportKeHoach5NamModel
    {
        public bool IsHangCha { get; set; }
        public int? iStt { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public string sTenDonVi { get; set; }
        public int iGiaiDoanTu { get; set; }
        public int iGiaiDoanDen { get; set; }
        public int ILoai { get; set; }
        public Guid iID_DuAnID { get; set; }
        public string sTenDuAn { get; set; }
        public Guid? iID_LoaiCongTrinhID { get; set; }
        public int iID_NguonVonID { get; set; }
        public string sTrangThai { get; set; }
        public string sGhiChu { get; set; }
        public string sTienDoThucHien { get; set; }
        public string sTenLoaiCongTrinh { get; set; }
        public double? fGiaTriNamThuNhat { get; set; }
        public double? fGiaTriNamThuHai { get; set; }
        public double? fGiaTriNamThuBa { get; set; }
        public double? fGiaTriNamThuTu { get; set; }
        public double? fGiaTriNamThuNam { get; set; }
        public double fTongDuToan
        {
            get
            {
                return fGiaTriNamThuNhat ?? 0 + fGiaTriNamThuHai ?? 0 + fGiaTriNamThuBa ?? 0
                    + fGiaTriNamThuTu ?? 0 + fGiaTriNamThuNam ?? 0;
            }
        }
    }
}
