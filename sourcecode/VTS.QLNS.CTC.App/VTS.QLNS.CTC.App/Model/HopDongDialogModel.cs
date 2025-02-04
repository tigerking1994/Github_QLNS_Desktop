using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.ViewModel;


namespace VTS.QLNS.CTC.App.Model
{
    public class HopDongDialogModel : ModelBase
    {
        private string _soHopDong;
        public string SoHopDong
        {
            get => _soHopDong;
            set => SetProperty(ref _soHopDong, value);
        }

        private DateTime? _ngayLap;
        public DateTime? NgayLap
        {
            get => _ngayLap;
            set => SetProperty(ref _ngayLap, value);
        }

        private int? _tGThucHienNgay;
        public int? TGThucHienNgay
        {
            get => _tGThucHienNgay;
            set => SetProperty(ref _tGThucHienNgay, value);
        }

        private DateTime? _ngayKhoiCongDuKien;
        public DateTime? NgayKhoiCongDuKien
        {
            get => _ngayKhoiCongDuKien;
            set => SetProperty(ref _ngayKhoiCongDuKien, value);
        }

        private DateTime? _ngayKetThucDuKien;
        public DateTime? NgayKetThucDuKien
        {
            get => _ngayKetThucDuKien;
            set => SetProperty(ref _ngayKetThucDuKien, value);
        }

        private string _hTHopDong;
        public string HTHopDong
        {
            get => _hTHopDong;
            set => SetProperty(ref _hTHopDong, value);
        }

        private double? _giaTriHopDong;
        public double? GiaTriHopDong
        {
            get => _giaTriHopDong;
            set => SetProperty(ref _giaTriHopDong, value);
        }

        private double _giaTrungThau;
        public double GiaTrungThau
        {
            get => _giaTrungThau;
            set => SetProperty(ref _giaTrungThau, value);
        }

        private string _soTaiKhoan;
        public string SoTaiKhoan
        {
            get => _soTaiKhoan;
            set => SetProperty(ref _soTaiKhoan, value);
        }

        private string _tenNganHang;
        public string TenNganHang
        {
            get => _tenNganHang;
            set => SetProperty(ref _tenNganHang, value);
        }

        private string _noiDungHopDong;
        public string NoiDungHopDong
        {
            get => _noiDungHopDong;
            set => SetProperty(ref _noiDungHopDong, value);
        }
    }
}
