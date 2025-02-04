using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class SettlementVoucherTotalModel : BindableBase
    {
        /// <summary>
        /// Tổng dự toán
        /// </summary>
        private double _fTongDuToan;
        public double FTongDuToan
        {
            get => _fTongDuToan;
            set => SetProperty(ref _fTongDuToan, value);
        }

        /// <summary>
        /// tổng đã quyết toán
        /// </summary>
        private double _fTongDaQuyetToan;
        public double FTongDaQuyetToan
        {
            get => _fTongDaQuyetToan;
            set => SetProperty(ref _fTongDaQuyetToan, value);
        }

        /// <summary>
        /// tổng còn lại
        /// </summary>
        private double _fTongConLai;
        public double FTongConLai
        {
            get => _fTongConLai;
            set => SetProperty(ref _fTongConLai, value);
        }

        private double _fTongTuChiDeNghi;
        public double FTongTuChiDeNghi
        {
            get => _fTongTuChiDeNghi;
            set => SetProperty(ref _fTongTuChiDeNghi, value);
        }

        private double _fTongTuChiPheDuyet;
        public double FTongTuChiPheDuyet
        {
            get => _fTongTuChiPheDuyet;
            set => SetProperty(ref _fTongTuChiPheDuyet, value);
        }

        private double? _fTongTienAn;
        public double? FTongTienAn
        {
            get => _fTongTienAn;
            set => SetProperty(ref _fTongTienAn, value);
        }

        /// <summary>
        /// tổng ngày
        /// </summary>
        private double _fTongSoNgay;
        public double FTongSoNgay
        {
            get => _fTongSoNgay;
            set => SetProperty(ref _fTongSoNgay, value);
        }

        /// <summary>
        /// tổng người
        /// </summary>
        private double _fTongSoNguoi;
        public double FTongSoNguoi
        {
            get => _fTongSoNguoi;
            set => SetProperty(ref _fTongSoNguoi, value);
        }

        /// <summary>
        /// tổng số lượt
        /// </summary>
        private double _fTongSoLuot;
        public double FTongSoLuot
        {
            get => _fTongSoLuot;
            set => SetProperty(ref _fTongSoLuot, value);
        }
        
        private double _fDeNghiChuyenNamSau;
        public double FTongDeNghiChuyenNamSau
        {
            get => _fDeNghiChuyenNamSau;
            set => SetProperty(ref _fDeNghiChuyenNamSau, value);
        }

        private double? _fChuyenNamSauDaCap;
        public double? FTongChuyenNamSauDaCap
        {
            get
            {
                return _fChuyenNamSauDaCap;
            }
            set
            {
                SetProperty(ref _fChuyenNamSauDaCap, value);
            }
        }

        private double? _fChuyenNamSauChuaCap;
        public double? FTongChuyenNamSauChuaCap
        {
            get
            {
                return FTongDeNghiChuyenNamSau - FTongChuyenNamSauDaCap.GetValueOrDefault();
            }
            set
            {
                SetProperty(ref _fChuyenNamSauChuaCap, value);
            }
        }

        public SettlementVoucherTotalModel()
        {
            _fTongDuToan = 0;
            _fTongDaQuyetToan = 0;
            _fTongConLai = 0;
            _fTongTuChiDeNghi = 0;
            _fTongTuChiPheDuyet = 0;
            _fTongTienAn = 0;
            _fTongSoNgay = 0;
            _fTongSoNguoi = 0;
            _fTongSoLuot = 0;
            _fDeNghiChuyenNamSau = 0;
            _fChuyenNamSauDaCap = 0;
            _fChuyenNamSauChuaCap = 0;
        }
    }
}
