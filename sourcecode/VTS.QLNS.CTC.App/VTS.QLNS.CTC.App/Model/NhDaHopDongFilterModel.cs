using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaHopDongFilterModel : BindableBase
    {
        public NhDaHopDongFilterModel()
        {
            SSoHopDong = string.Empty;
            STenHopDong = string.Empty;
            SLoaiHopDong = string.Empty;
            DNgayHopDong = null;
            DKhoiCongDuKien = null;
            DKetThucDuKien = null;
            STenNhiemVuChi = string.Empty;
        }

        private string _sSoHopDong;
        public string SSoHopDong
        {
            get => _sSoHopDong;
            set => SetProperty(ref _sSoHopDong, value);
        }

        private string _sTenHopDong;
        public string STenHopDong
        {
            get => _sTenHopDong;
            set => SetProperty(ref _sTenHopDong, value);
        }

        private string _sLoaiHopDong;
        public string SLoaiHopDong
        {
            get => _sLoaiHopDong;
            set => SetProperty(ref _sLoaiHopDong, value);
        }

        private string _sLoaiNhiemVuChi;
        public string SLoaiNhiemVuChi
        {
            get => _sLoaiNhiemVuChi;
            set => SetProperty(ref _sLoaiNhiemVuChi, value);
        }

        private DateTime? _dNgayHopDong;
        public DateTime? DNgayHopDong
        {
            get => _dNgayHopDong;
            set => SetProperty(ref _dNgayHopDong, value);
        }

        private DateTime? _dKhoiCongDuKien;
        public DateTime? DKhoiCongDuKien
        {
            get => _dKhoiCongDuKien;
            set => SetProperty(ref _dKhoiCongDuKien, value);
        }

        private DateTime? _dKetThucDuKien;
        public DateTime? DKetThucDuKien
        {
            get => _dKetThucDuKien;
            set => SetProperty(ref _dKetThucDuKien, value);
        }

        private string _sTenNhiemVuChi;
        public string STenNhiemVuChi
        {
            get => _sTenNhiemVuChi;
            set => SetProperty(ref _sTenNhiemVuChi, value);
        }
    }
}
