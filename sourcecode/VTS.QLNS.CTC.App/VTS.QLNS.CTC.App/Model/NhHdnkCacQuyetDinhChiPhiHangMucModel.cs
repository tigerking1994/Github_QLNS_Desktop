using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhHdnkCacQuyetDinhChiPhiHangMucModel : CurrencyDetailModelBase
    {
        public Guid IIdCacQuyetDinhChiPhiId { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SMaHangMuc { get; set; }
        public string STenHangMuc { get; set; }

        private double _fGiaTriNgoaiTeKhac;
        public double FGiaTriNgoaiTeKhac
        {
            get => _fGiaTriNgoaiTeKhac;
            set => SetProperty(ref _fGiaTriNgoaiTeKhac, value);
        }

        public string SMaOrder { get; set; }
        public Guid? IIdQdDauTuHangMucId { get; set; }

        // Another properties
        public bool IsNewRecord { get; set; }
        public Guid? IdOld { get; set; }

        private double? _fGiaTriNgoaiTeKhacGoiThau;
        public double? FGiaTriNgoaiTeKhacGoiThau
        {
            get => _fGiaTriNgoaiTeKhacGoiThau;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacGoiThau, value);
        }

        private double? _fGiaTriUSDGoiThau;
        public double? FGiaTriUSDGoiThau
        {
            get => _fGiaTriUSDGoiThau;
            set => SetProperty(ref _fGiaTriUSDGoiThau, value);
        }

        private double? _fGiaTriVNDGoiThau;
        public double? FGiaTriVNDGoiThau
        {
            get => _fGiaTriVNDGoiThau;
            set => SetProperty(ref _fGiaTriVNDGoiThau, value);
        }

        private double? _fGiaTriEURGoiThau;
        public double? FGiaTriEURGoiThau
        {
            get => _fGiaTriEURGoiThau;
            set => SetProperty(ref _fGiaTriEURGoiThau, value);
        }

        private double? _fGiaTriNgoaiTeKhacConLai;
        public double? FGiaTriNgoaiTeKhacConLai
        {
            get => _fGiaTriNgoaiTeKhacConLai;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacConLai, value);
        }

        private double? _fGiaTriUSDConLai;
        public double? FGiaTriUSDConLai
        {
            get => _fGiaTriUSDConLai;
            set => SetProperty(ref _fGiaTriUSDConLai, value);
        }

        private double? _fGiaTriVNDConLai;
        public double? FGiaTriVNDConLai
        {
            get => _fGiaTriVNDConLai;
            set => SetProperty(ref _fGiaTriVNDConLai, value);
        }

        private double? _fGiaTriEURConLai;
        public double? FGiaTriEURConLai
        {
            get => _fGiaTriEURConLai;
            set => SetProperty(ref _fGiaTriEURConLai, value);
        }

        public double? FGiaTriQddtNgoaiTeKhac { get; set; }
        public double? FGiaTriQddtUSD { get; set; }
        public double? FGiaTriQddtEUR { get; set; }
        public double? FGiaTriQddtVND { get; set; }
        // field này để xác định quan hệ cha con - qddt hạng mục, ánh xạ sang Qdct hạng mục để biết cha con.
        public Guid? IIdQdDauTuHangMucParentId { get; set; }

    }
}