using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class PhanBoVonDonViChiTietModel : DetailModelBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }
        public int? iID_MaNguonNganSach { get; set; }
        public Guid iID_DuAnID { get; set; }
        public string sTenDuAn { get; set; }
        public string sMaDuAn { get; set; }
        public string sThoiGianThucHien { get; set; }
        public Guid? iID_LoaiCongTrinhID { get; set; }
        public string sTenLoaiCongTrinh { get; set; }
        public Guid? iID_CapPheDuyetID { get; set; }
        public string sTenCapPheDuyet { get; set; }
        public string sTenChuDauTu { get; set; }

        private double? _fTongMucDauTuDuocDuyet;
        public double? fTongMucDauTuDuocDuyet
        {
            get => _fTongMucDauTuDuocDuyet;
            set => SetProperty(ref _fTongMucDauTuDuocDuyet, value);
        }

        private double? _fLuyKeVonNamTruoc;
        public double? fLuyKeVonNamTruoc
        {
            get => _fLuyKeVonNamTruoc;
            set
            {
                SetProperty(ref _fLuyKeVonNamTruoc, value);
                OnPropertyChanged(nameof(FLuyKeVonDaBoTriHetNam));
            }
        }

        private double? _fKeHoachVonDuocDuyetNamNay;
        public double? fKeHoachVonDuocDuyetNamNay
        {
            get => _fKeHoachVonDuocDuyetNamNay;
            set
            {
                SetProperty(ref _fKeHoachVonDuocDuyetNamNay, value);
                OnPropertyChanged(nameof(FLuyKeVonDaBoTriHetNam));
            }
        }

        private double? _fVonKeoDaiCacNamTruoc;
        public double? fVonKeoDaiCacNamTruoc
        {
            get => _fVonKeoDaiCacNamTruoc;
            set
            {
                SetProperty(ref _fVonKeoDaiCacNamTruoc, value);
                OnPropertyChanged(nameof(FLuyKeVonDaBoTriHetNam));
            }
        }

        private double? _fUocThucHien;
        public double? fUocThucHien
        {
            get => _fUocThucHien;
            set => SetProperty(ref _fUocThucHien, value);
        }

        private double? _fUocThucHienSauDc;
        public double? FUocThucHienSauDc
        {
            get => _fUocThucHienSauDc;
            set => SetProperty(ref _fUocThucHienSauDc, value);
        }

        private double? _fThuHoiVonUngTruoc;
        public double? fThuHoiVonUngTruoc
        {
            get => _fThuHoiVonUngTruoc;
            set => SetProperty(ref _fThuHoiVonUngTruoc, value);
        }

        private double? _fThuHoiVonUngTruocSauDc;
        public double? FThuHoiVonUngTruocSauDc
        {
            get => _fThuHoiVonUngTruocSauDc;
            set => SetProperty(ref _fThuHoiVonUngTruocSauDc, value);
        }

        private double? _fThanhToan;
        public double? fThanhToan
        {
            get => _fThanhToan;
            set => SetProperty(ref _fThanhToan, value);
        }

        private double? _fThanToanSauDc;
        public double? FThanhToanSauDc
        {
            get => _fThanToanSauDc;
            set => SetProperty(ref _fThanToanSauDc, value);
        }

        private double? _fKeHoachTrungHanDuocDuyet;
        public double? FKeHoachTrungHanDuocDuyet
        {
            get => _fKeHoachTrungHanDuocDuyet;
            set => SetProperty(ref _fKeHoachTrungHanDuocDuyet, value);
        }

        private double _fLuyKeVonDaBoTriHetNam;
        public double FLuyKeVonDaBoTriHetNam
        {
            get
            {
                _fLuyKeVonDaBoTriHetNam = (fLuyKeVonNamTruoc ?? 0) + (fKeHoachVonDuocDuyetNamNay ?? 0) + (fVonKeoDaiCacNamTruoc ?? 0);
                return _fLuyKeVonDaBoTriHetNam;
            }
            set => SetProperty(ref _fLuyKeVonDaBoTriHetNam, value);
        }

        public Guid? iID_DonViTienTeID { get; set; }
        public Guid? iID_TienTeID { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public double? fTiGia { get; set; }
        public string sTrangThaiDuAnDangKy { get; set; }
        public double fTongKeHoachVon { get; set; }
        public double fTongKeHoachVonNam { get; set; }
        public string sMaChuDauTu { get; set; }
        public string sTenDonViQuanLy { get; set; }
        public string sMaDonViQuanLy { get; set; }
        public string STT { get; set; }
        public Guid? IIDParentId { get; set; }
        public bool? BActive { get; set; }
        public int? ILoaiDuAn { get; set; }
        public bool IsGoc { get; set; }
        public Guid? IdChungTuParent { get; set; }
        public Guid? IdChungTu { get; set; }
        public string STenDonViThucHienDuAn { get; set; }
        public Guid? IID_DuAn_HangMucID { get; set; }
    }
}
