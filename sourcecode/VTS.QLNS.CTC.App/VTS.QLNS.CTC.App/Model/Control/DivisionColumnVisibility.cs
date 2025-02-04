using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Control
{
    public class DivisionColumnVisibility : BindableBase
    {
        private bool _isDisplayTuChiDieuChinh;
        public bool IsDisplayTuChiDieuChinh
        {
            get => _isDisplayTuChiDieuChinh;
            set => SetProperty(ref _isDisplayTuChiDieuChinh, value);
        }

        private bool _isDisplayTuChi;
        public bool IsDisplayTuChi
        {
            get => _isDisplayTuChi;
            set => SetProperty(ref _isDisplayTuChi, value);
        }

        private bool _isDisplayHienVatDieuChinh;
        public bool IsDisplayHienVatDieuChinh
        {
            get => _isDisplayHienVatDieuChinh;
            set => SetProperty(ref _isDisplayHienVatDieuChinh, value);
        }

        private bool _isDisplayHienVat;
        public bool IsDisplayHienVat
        {
            get => _isDisplayHienVat;
            set => SetProperty(ref _isDisplayHienVat, value);
        }

        private bool _isDisplayHangNhapDieuChinh;
        public bool IsDisplayHangNhapDieuChinh
        {
            get => _isDisplayHangNhapDieuChinh;
            set => SetProperty(ref _isDisplayHangNhapDieuChinh, value);
        }

        private bool _isDisplayHangNhap;
        public bool IsDisplayHangNhap
        {
            get => _isDisplayHangNhap;
            set => SetProperty(ref _isDisplayHangNhap, value);
        }

        private bool _isDisplayHangMuaDieuChinh;
        public bool IsDisplayHangMuaDieuChinh
        {
            get => _isDisplayHangMuaDieuChinh;
            set => SetProperty(ref _isDisplayHangMuaDieuChinh, value);
        }

        private bool _isDisplayHangMua;
        public bool IsDisplayHangMua
        {
            get => _isDisplayHangMua;
            set => SetProperty(ref _isDisplayHangMua, value);
        }

        private bool _isDisplayPhanCapDieuChinh;
        public bool IsDisplayPhanCapDieuChinh
        {
            get => _isDisplayPhanCapDieuChinh;
            set => SetProperty(ref _isDisplayPhanCapDieuChinh, value);
        }

        private bool _isDisplayPhanCap;
        public bool IsDisplayPhanCap
        {
            get => _isDisplayPhanCap;
            set => SetProperty(ref _isDisplayPhanCap, value);
        }

        private bool _isDisplayDuPhong;
        public bool IsDisplayDuPhong
        {
            get => _isDisplayDuPhong;
            set => SetProperty(ref _isDisplayDuPhong, value);
        }

        private bool _isDisplayTonKho;
        public bool IsDisplayTonKho
        {
            get => _isDisplayTonKho;
            set => SetProperty(ref _isDisplayTonKho, value);
        }
    }
}
