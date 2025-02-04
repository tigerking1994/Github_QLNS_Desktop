using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtKhvKeHoachVonUngDxChiTietModel : DetailModelBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }
        public Guid? IIDKeHoachUngID { get; set; }
        public string SMaDuAn { get; set; }
        public string STenDuAn { get; set; }
        public Guid? IIDDuAnID { get; set; }
        public string STrangThaiDuAnDangKy { get; set; }
        public double? FTongMucDauTuPheDuyet { get; set; }

        private double _fGiaTriDeNghi;
        public double FGiaTriDeNghi
        {
            get => _fGiaTriDeNghi;
            set => SetProperty(ref _fGiaTriDeNghi, value);
        }

        public Guid? IIDDonViTienTeID { get; set; }
        public Guid? IIDTienTeID { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public string STenDonVi { get; set; }
        public Guid? IIDDonViQuanLyID { get; set; }
        public string IIDMaDonViQuanLy { get; set; }

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private double _fGiaTriDeNghiTruocDieuChinh;
        public double FGiaTriDeNghiTruocDieuChinh
        {
            get => _fGiaTriDeNghiTruocDieuChinh;
            set => SetProperty(ref _fGiaTriDeNghiTruocDieuChinh, value);
        }
        public string STT { get; set; }
        public Guid? ID_DuAn_HangMuc { get; set; }
        public string sTenHangMuc { get; set; }

    }
}
