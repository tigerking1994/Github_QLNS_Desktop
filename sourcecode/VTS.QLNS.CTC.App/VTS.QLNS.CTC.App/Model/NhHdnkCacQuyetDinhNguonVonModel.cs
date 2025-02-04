using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhHdnkCacQuyetDinhNguonVonModel : CurrencyDetailModelBase
    {
        public Guid IIdCacQuyetDinhId { get; set; }

        private int _iIdNguonVonId;
        public int IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }

        public string STenNguonVon { get; set; }
        public Guid? IIdQdDauTuNguonVonId { get; set; }

        // Another properties
        private double _fGiaTriNgoaiTeKhacGoiThau;
        public double FGiaTriNgoaiTeKhacGoiThau
        {
            get => _fGiaTriNgoaiTeKhacGoiThau;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacGoiThau, value);
        }

        private double _fGiaTriUSDGoiThau;
        public double FGiaTriUSDGoiThau
        {
            get => _fGiaTriUSDGoiThau;
            set => SetProperty(ref _fGiaTriUSDGoiThau, value);
        }

        private double _fGiaTriVNDGoiThau;
        public double FGiaTriVNDGoiThau
        {
            get => _fGiaTriVNDGoiThau;
            set => SetProperty(ref _fGiaTriVNDGoiThau, value);
        }

        private double _fGiaTriEURGoiThau;
        public double FGiaTriEURGoiThau
        {
            get => _fGiaTriEURGoiThau;
            set => SetProperty(ref _fGiaTriEURGoiThau, value);
        }

        private double _fGiaTriNgoaiTeKhacConLai;
        public double FGiaTriNgoaiTeKhacConLai
        {
            get => _fGiaTriNgoaiTeKhacConLai;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacConLai, value);
        }

        private double _fGiaTriUSDConLai;
        public double FGiaTriUSDConLai
        {
            get => _fGiaTriUSDConLai;
            set => SetProperty(ref _fGiaTriUSDConLai, value);
        }

        private double _fGiaTriVNDConLai;
        public double FGiaTriVNDConLai
        {
            get => _fGiaTriVNDConLai;
            set => SetProperty(ref _fGiaTriVNDConLai, value);
        }
        private double _fGiaTriEURConLai;
        public double FGiaTriEURConLai
        {
            get => _fGiaTriEURConLai;
            set => SetProperty(ref _fGiaTriEURConLai, value);
        }

        private double? _fGiaTriQddtNgoaiTeKhac;
        public double? FGiaTriQddtNgoaiTeKhac
        {
            get => _fGiaTriQddtNgoaiTeKhac;
            set => SetProperty(ref _fGiaTriQddtNgoaiTeKhac, value);
        }

        private double? _fGiaTriQddtUSD;
        public double? FGiaTriQddtUSD
        {
            get => _fGiaTriQddtUSD;
            set => SetProperty(ref _fGiaTriQddtUSD, value);
        }

        private double? _fGiaTriQddtVND;
        public double? FGiaTriQddtVND
        {
            get => _fGiaTriQddtVND;
            set => SetProperty(ref _fGiaTriQddtVND, value);
        }
        private double? _fGiaTriQddtEUR;
        public double? FGiaTriQddtEUR
        {
            get => _fGiaTriQddtEUR;
            set => SetProperty(ref _fGiaTriQddtEUR, value);
        }

        public Guid? IIdDuToanNguonVonId { get; set; }
        public Guid? IIdCacQuyetDinhNguonVonId { get; set; }

    }
}