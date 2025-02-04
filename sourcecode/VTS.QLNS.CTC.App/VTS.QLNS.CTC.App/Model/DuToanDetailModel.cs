using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class DuToanDetailModel : DetailModelBase
    {
        private Guid? _idDuToanHangMuc;
        public Guid? IdDuToanHangMuc
        {
            get => _idDuToanHangMuc;
            set => SetProperty(ref _idDuToanHangMuc, value);
        }

        private string _maHangMuc;
        public string MaHangMuc
        {
            get => _maHangMuc;
            set => SetProperty(ref _maHangMuc, value);
        }

        private string _tenHangMuc;
        public string TenHangMuc
        {
            get => _tenHangMuc;
            set => SetProperty(ref _tenHangMuc, value);
        }

        private Guid? _idChiPhi;
        public Guid? IdChiPhi
        {
            get => _idChiPhi;
            set => SetProperty(ref _idChiPhi, value);
        }

        private Guid? _idDuAnChiPhi;
        public Guid? IdDuAnChiPhi
        {
            get => _idDuAnChiPhi;
            set => SetProperty(ref _idDuAnChiPhi, value);
        }

        private Guid? _idDuAnHangMuc;
        public Guid? IdDuAnHangMuc
        {
            get => _idDuAnHangMuc;
            set => SetProperty(ref _idDuAnHangMuc, value);
        }

        private double? _giaTriPheDuyet;
        public double? GiaTriPheDuyet
        {
            get => _giaTriPheDuyet;
            set => SetProperty(ref _giaTriPheDuyet, value);
        }

        private double? _fGiaTriDieuChinh;
        public double? FGiaTriDieuChinh
        {
            get => _fGiaTriDieuChinh;
            set => SetProperty(ref _fGiaTriDieuChinh, value);
        }

        private double? _giaTriTruocDieuChinh;
        public double? GiaTriTruocDieuChinh
        {
            get => _giaTriTruocDieuChinh;
            set => SetProperty(ref _giaTriTruocDieuChinh, value);
        }

        private Guid? _iIdDuToanId;
        public Guid? IIdDuToanId
        {
            get => _iIdDuToanId;
            set => SetProperty(ref _iIdDuToanId, value);
        }

        private Guid? _hangMucParentId;
        public Guid? HangMucParentId
        {
            get => _hangMucParentId;
            set => SetProperty(ref _hangMucParentId, value);
        }

        public bool? IsHangMucOld { get; set; }

        public string MaOrDer { get; set; }

        private Guid? _idLoaiCongTrinh;
        public Guid? IdLoaiCongTrinh
        {
            get => _idLoaiCongTrinh;
            set => SetProperty(ref _idLoaiCongTrinh, value);
        }

        public string TenChiPhi { get; set; }
        public string TenLoaiCT { get; set; }

        private bool _isEditHangMuc;
        public bool IsEditHangMuc
        {
            get => _isEditHangMuc;
            set => SetProperty(ref _isEditHangMuc, value);
        }

        public double? FTienPheDuyetQDDT { get; set; }

        private double? _fTienChenhLech;
        public double? FTienChenhLech
        {
            get => _fTienChenhLech;
            set => SetProperty(ref _fTienChenhLech, value);
        }

        private Guid? _iIdHangMucPhanChia;
        public Guid? IIdHangMucPhanChia
        {
            get => _iIdHangMucPhanChia;
            set => SetProperty(ref _iIdHangMucPhanChia, value);
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        private double? _fGiaTriPhanChia;
        public double? FGiaTriPhanChia
        {
            get => _fGiaTriPhanChia;
            set => SetProperty(ref _fGiaTriPhanChia, value);
        }

        public string SMoTa { get; set; }

        private bool _isSaved;
        public bool IsSaved
        {
            get => _isSaved;
            set => SetProperty(ref _isSaved, value);
        }
    }
}
