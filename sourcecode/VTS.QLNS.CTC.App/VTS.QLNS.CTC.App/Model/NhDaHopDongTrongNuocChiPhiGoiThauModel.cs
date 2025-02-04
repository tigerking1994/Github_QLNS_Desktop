using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaHopDongTrongNuocChiPhiGoiThauModel : CurrencyDetailModelBase
    {
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdCacQuyetDinhChiPhiId { get; set; }
        public Guid? IIdGoiThauChiPhiId { get; set; }
        public Guid? IIdHopDongGoiThauNhaThauId { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIdHopDongNguonVonId { get; set; }
        public string STenChiPhi { get; set; }
        private double? _fTienHopDongEUR;
        public double? FTienHopDongEUR 
        {
            get => _fTienHopDongEUR;
            set => SetProperty(ref _fTienHopDongEUR, value);
        }

        private double? _fTienHopDongUSD;
        public double? FTienHopDongUSD
        {
            get => _fTienHopDongUSD;
            set => SetProperty(ref _fTienHopDongUSD, value);
        }

        private double? _fTienHopDongVND;
        public double? FTienHopDongVND
        {
            get => _fTienHopDongVND;
            set => SetProperty(ref _fTienHopDongVND, value);
        }

        private double? _fTienHopDongNgoaiTeKhac;
        public double? FTienHopDongNgoaiTeKhac
        {
            get => _fTienHopDongNgoaiTeKhac;
            set => SetProperty(ref _fTienHopDongNgoaiTeKhac, value);
        }

        private double? _fTienGoiThauEUR;
        public double? FTienGoiThauEUR
        {
            get => _fTienGoiThauEUR;
            set => SetProperty(ref _fTienGoiThauEUR, value);
        }

        private double? _fTienGoiThauUSD;
        public double? FTienGoiThauUSD
        {
            get => _fTienGoiThauUSD;
            set => SetProperty(ref _fTienGoiThauUSD, value);
        }

        private double? _fTienGoiThauVND;
        public double? FTienGoiThauVND
        {
            get => _fTienGoiThauVND;
            set => SetProperty(ref _fTienGoiThauVND, value);
        }

        private double? _fTienGoiThauNgoaiTeKhac;
        public double? FTienGoiThauNgoaiTeKhac
        {
            get => _fTienGoiThauNgoaiTeKhac;
            set => SetProperty(ref _fTienGoiThauNgoaiTeKhac, value);
        }
        public bool EditChiPhi { get; set; }
        public string STenNguonVon { get; set; }
        public string SMaOrder { get; set; }
    }
}
