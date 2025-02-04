using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.ViewModel;

namespace VTS.QLNS.CTC.App.Model
{
    public class RequestSettlementDialogModel : ViewModelBase
    {
        public Guid Id { get; set; }

        private string _soBaoCao;
        public string SoBaoCao
        {
            get => _soBaoCao;
            set => SetProperty(ref _soBaoCao, value);
        }

        private DateTime? _ngayDuyet;
        public DateTime? NgayDuyet
        {
            get => _ngayDuyet;
            set => SetProperty(ref _ngayDuyet, value);
        }

        private string _nguoiDuyet;
        public string NguoiDuyet
        {
            get => _nguoiDuyet;
            set => SetProperty(ref _nguoiDuyet, value);
        }

        private DateTime? _ngayNhan;
        public DateTime? NgayNhan
        {
            get => _ngayNhan;
            set => SetProperty(ref _ngayNhan, value);
        }

        private string _nguoiNhan;
        public string NguoiNhan
        {
            get => _nguoiNhan;
            set => SetProperty(ref _nguoiNhan, value);
        }

        private string _tenDuAn;
        public string TenDuAn
        {
            get => _tenDuAn;
            set => SetProperty(ref _tenDuAn, value);
        }

        private string _chuDauTu;
        public string ChuDauTu
        {
            get => _chuDauTu;
            set => SetProperty(ref _chuDauTu, value);
        }

        private DateTime? _thoiGianKhoiCong;
        public DateTime? ThoiGianKhoiCong
        {
            get => _thoiGianKhoiCong;
            set => SetProperty(ref _thoiGianKhoiCong, value);
        }

        private DateTime? _thoiGianHoanThanh;
        public DateTime? ThoiGianHoanThanh
        {
            get => _thoiGianHoanThanh;
            set => SetProperty(ref _thoiGianHoanThanh, value);
        }

        private double? _giaTriQuyetToan;
        public double? GiaTriQuyetToan
        {
            get => _giaTriQuyetToan;
            set => SetProperty(ref _giaTriQuyetToan, value);
        }

        private string _ghiChu;
        public string GhiChu
        {
            get => _ghiChu;
            set => SetProperty(ref _ghiChu, value);
        }

        private double? _chiPhiThietHai;
        public double? ChiPhiThietHai
        {
            get => _chiPhiThietHai;
            set => SetProperty(ref _chiPhiThietHai, value);
        }

        private double? _chiPhiKhongTaoTaiSan;
        public double? ChiPhiKhongTaoTaiSan
        {
            get => _chiPhiKhongTaoTaiSan;
            set => SetProperty(ref _chiPhiKhongTaoTaiSan, value);
        }

        private double? _daiHanThuocQuanLy;
        public double? DaiHanThuocQuanLy
        {
            get => _daiHanThuocQuanLy;
            set => SetProperty(ref _daiHanThuocQuanLy, value);
        }

        private double? _daiHanDonViKhacQuanLy;
        public double? DaiHanDonViKhacQuanLy
        {
            get => _daiHanDonViKhacQuanLy;
            set => SetProperty(ref _daiHanDonViKhacQuanLy, value);
        }

        private double? _nganHanThuocQuanLy;
        public double? NganHanThuocQuanLy
        {
            get => _nganHanThuocQuanLy;
            set => SetProperty(ref _nganHanThuocQuanLy, value);
        }

        private double? _nganHanDonViKhacQuanLy;
        public double? NganHanDonViKhacQuanLy
        {
            get => _nganHanDonViKhacQuanLy;
            set => SetProperty(ref _nganHanDonViKhacQuanLy, value);
        }
    }
}
