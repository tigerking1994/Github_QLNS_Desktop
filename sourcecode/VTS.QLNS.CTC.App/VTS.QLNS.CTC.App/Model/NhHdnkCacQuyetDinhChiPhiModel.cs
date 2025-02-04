using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhHdnkCacQuyetDinhChiPhiModel : CurrencyDetailModelBase
    {
        public Guid IIdCacQuyetDinhId { get; set; }

        private Guid? _iIdChiPhiId;
        public Guid? IIdChiPhiId
        {
            get => _iIdChiPhiId;
            set => SetProperty(ref _iIdChiPhiId, value);
        }

        public Guid? IIdParentId { get; set; }
        public string STenChiPhi { get; set; }
        public string SMaOrder { get; set; }
        public Guid? IIdQdDauTuChiPhiId { get; set; }

        // Another properties
        private bool _isGoc;
        public bool IsGoc
        {
            get => _isGoc;
            set => SetProperty(ref _isGoc, value);
        }
        public bool IsNewRecord { get; set; }

        private bool _isEditHangMuc;
        public bool IsEditHangMuc
        {
            get => _isEditHangMuc;
            set => SetProperty(ref _isEditHangMuc, value);
        }
        public List<NhHdnkCacQuyetDinhChiPhiHangMucModel> ListHangMuc { get; set; }

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


        // Another properties
        public Guid? IIdQuyetDinhChiPhiId { get; set; }
        public Guid? IIdGoiThauChiPhiId { get; set; }
        public Guid? IIdHopDongCacQuyetDinhId { get; set; }
        private double _fGiaTriNgoaiTeKhacHopDong;
        public double FGiaTriNgoaiTeKhacHopDong
        {
            get => _fGiaTriNgoaiTeKhacHopDong;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacHopDong, value);
        }
        private double _fGiaTriUSDHopDong;
        public double FGiaTriUSDHopDong
        {
            get => _fGiaTriUSDHopDong;
            set => SetProperty(ref _fGiaTriUSDHopDong, value);
        }
        private double _fGiaTriEURHopDong;
        public double FGiaTriEURHopDong
        {
            get => _fGiaTriEURHopDong;
            set => SetProperty(ref _fGiaTriEURHopDong, value);
        }
        private double _fGiaTriVNDHopDong;
        public double FGiaTriVNDHopDong
        {
            get => _fGiaTriVNDHopDong;
            set => SetProperty(ref _fGiaTriVNDHopDong, value);
        }
        public List<NhDaHopDongHangMucModel> ListHopDongHangMuc { get; set; }

        public Guid? IIdDuToanChiPhiId { get; set; }
        public Guid? IIdCacQuyetDinhChiPhiId { get; set; }

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

    }
}