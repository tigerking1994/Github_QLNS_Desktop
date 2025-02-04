using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtQtDenghiQuyetToanNienDoChiTietModel : DetailModelBase
    {
        private Guid? _iID_DeNghiQuyetToanNienDoId;
        public Guid? iID_DeNghiQuyetToanNienDoId
        {
            get => _iID_DeNghiQuyetToanNienDoId;
            set => SetProperty(ref _iID_DeNghiQuyetToanNienDoId, value);
        }

        private Guid? _iID_DuAnId;
        public Guid? iID_DuAnId
        {
            get => _iID_DuAnId;
            set => SetProperty(ref _iID_DuAnId, value);
        }

        private Guid? _iId_MucId;
        public Guid? iId_MucId
        {
            get => _iId_MucId;
            set => SetProperty(ref _iId_MucId, value);
        }

        private Guid? _iId_TieuMucId;
        public Guid? iId_TieuMucId
        {
            get => _iId_TieuMucId;
            set => SetProperty(ref _iId_TieuMucId, value);
        }

        private Guid? _iId_TietMucId;
        public Guid? iId_TietMucId
        {
            get => _iId_TietMucId;
            set => SetProperty(ref _iId_TietMucId, value);
        }

        private Guid? _iId_NganhId;
        public Guid? iId_NganhId
        {
            get => _iId_NganhId;
            set => SetProperty(ref _iId_NganhId, value);
        }

        private double? _fGiaTriQuyetToanNamTruocDonVi;
        public double? fGiaTriQuyetToanNamTruocDonVi
        {
            get => _fGiaTriQuyetToanNamTruocDonVi;
            set => SetProperty(ref _fGiaTriQuyetToanNamTruocDonVi, value);
        }

        private double? _fGiaTriQuyetToanNamNayDonVi;
        public double? fGiaTriQuyetToanNamNayDonVi
        {
            get => _fGiaTriQuyetToanNamNayDonVi;
            set => SetProperty(ref _fGiaTriQuyetToanNamNayDonVi, value);
        }

        private double? _fGiaTriQuyetToanNamTruoc;
        public double? fGiaTriQuyetToanNamTruoc
        {
            get => _fGiaTriQuyetToanNamTruoc;
            set => SetProperty(ref _fGiaTriQuyetToanNamTruoc, value);
        }

        private double? _fGiaTriQuyetToanNamNay;
        public double? fGiaTriQuyetToanNamNay
        {
            get => _fGiaTriQuyetToanNamNay;
            set => SetProperty(ref _fGiaTriQuyetToanNamNay, value);
        }

        private Guid? _iId_DonViTienTeId;
        public Guid? iId_DonViTienTeId
        {
            get => _iId_DonViTienTeId;
            set => SetProperty(ref _iId_DonViTienTeId, value);
        }

        private Guid? _iId_TienTeId;
        public Guid? iId_TienTeId
        {
            get => _iId_TienTeId;
            set => SetProperty(ref _iId_TienTeId, value);
        }

        private double _mTiGia;
        public double mTiGia
        {
            get => _mTiGia;
            set => SetProperty(ref _mTiGia, value);
        }

        private double _mTiGiaDonVi;
        public double mTiGiaDonVi
        {
            get => _mTiGiaDonVi;
            set => SetProperty(ref _mTiGiaDonVi, value);
        }

        private string _sTenDuAn;
        public string sTenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private double? _fTongMucDauTu;
        public double? fTongMucDauTu
        {
            get => _fTongMucDauTu;
            set => SetProperty(ref _fTongMucDauTu, value);
        }

        private string _sXauNoiMa;
        public string sXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }

        private double? _fBuTruThuaThieu;
        public double? fBuTruThuaThieu
        {
            get => _fBuTruThuaThieu;
            set => SetProperty(ref _fBuTruThuaThieu, value);
        }

        private double? _fThuLaiKeHoachNamNay;
        public double? fThuLaiKeHoachNamNay
        {
            get => _fThuLaiKeHoachNamNay;
            set => SetProperty(ref _fThuLaiKeHoachNamNay, value);
        }

        private double? _fThuUng;
        public double? fThuUng
        {
            get => _fThuUng;
            set => SetProperty(ref _fThuUng, value);
        }

        private double? _fThuThanhKhoan;
        public double? fThuThanhKhoan
        {
            get => _fThuThanhKhoan;
            set => SetProperty(ref _fThuThanhKhoan, value);
        }

        private double? _fThuLaiKeHoachNamTruoc;
        public double? fThuLaiKeHoachNamTruoc
        {
            get => _fThuLaiKeHoachNamTruoc;
            set => SetProperty(ref _fThuLaiKeHoachNamTruoc, value);
        }

        private double? _fThuThanhKhoanNamTruoc;
        public double? fThuThanhKhoanNamTruoc
        {
            get => _fThuThanhKhoanNamTruoc;
            set => SetProperty(ref _fThuThanhKhoanNamTruoc, value);
        }

        public double? fGiaTriCapPhatNamNay { get; set; }
        public double? fGiaTriCapPhatNamTruoc { get; set; }
        public double? fGiaTriLuyKe { get; set; }
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
    }
}
