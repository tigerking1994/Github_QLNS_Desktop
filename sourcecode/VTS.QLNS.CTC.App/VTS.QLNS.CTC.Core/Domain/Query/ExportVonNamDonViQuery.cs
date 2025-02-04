using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ExportVonNamDonViQuery
    {
        public Guid iID_DuAnID { get; set; }
        public string sTenDuAn { get; set; }
        public string sMaDuAn { get; set; }
        public string sThoiGianThucHien { get; set; }
        public Guid? iID_CapPheDuyetID { get; set; }
        public string sTenCapPheDuyet { get; set; }
        public Guid? iID_LoaiCongTrinhID { get; set; }
        public string sTenLoaiCongTrinh { get; set; }
        public string sTenChuDauTu { get; set; }
        public double? fTongMucDauTuDuocDuyet { get; set; }
        public double? fLuyKeVonNamTruoc { get; set; }
        public double? fKeHoachVonDuocDuyetNamNay { get; set; }
        public double? fVonKeoDaiCacNamTruoc { get; set; }
        public double? fUocThucHien { get; set; }
        public double? fThuHoiVonUngTruoc { get; set; }
        public double? fThanhToan { get; set; }
        public Guid? iID_DonViTienTeID { get; set; }
        public Guid? iID_TienTeID { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public double? fTiGia { get; set; }
        public string sTrangThaiDuAnDangKy { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public int iID_NguonVonID { get; set; }
        public Guid? iID_DonViQuanLyID { get; set; }

        public double fTongKeHoachVon
        {
            get
            {
                return (fKeHoachVonDuocDuyetNamNay ?? 0) + (fVonKeoDaiCacNamTruoc ?? 0);
            }
        }

        public double fLuyKeVonDaBoTriHetNamNay
        {
            get
            {
                return (fLuyKeVonNamTruoc ?? 0) + fTongKeHoachVon;
            }
        }

        public double fTongKeHoachVonNam
        {
            get
            {
                return (fThuHoiVonUngTruoc ?? 0) + (fThanhToan ?? 0);
            }
        }

        public string sMaChuDauTu { get; set; }
        public string sTenDonViQuanLy { get; set; }
        public string sMaDonViQuanLy { get; set; }
        public int iCongTrinh { get; set; }
        public bool? IsGoc { get; set; }
        public bool? BActive { get; set; }
        public Guid? IdChungTu { get; set; }
        public Guid? IdChungTuParent { get; set; }
        public int? ILoaiDuAn { get; set; }

        [NotMapped]
        public string iStt { get; set; }

        [NotMapped]
        public bool IsHangCha { get; set; }
    }
}
