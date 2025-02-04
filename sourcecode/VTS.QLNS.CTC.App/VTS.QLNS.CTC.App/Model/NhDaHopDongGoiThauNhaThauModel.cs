using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaHopDongGoiThauNhaThauModel : ModelBase
    {
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIdNhaThauId { get; set; }

        private double? _fGiaTriNgoaiTeKhac;
        public double? FGiaTriNgoaiTeKhac 
        {
            get => _fGiaTriNgoaiTeKhac;
            set => SetProperty(ref _fGiaTriNgoaiTeKhac, value);
        }
        public int IsCheck { get; set; }

        private double? _fGiaTriUsd;
        public double? FGiaTriUsd
        {
            get => _fGiaTriUsd;
            set => SetProperty(ref _fGiaTriUsd, value);
        }

        private double? _fGiaTriVnd;
        public double? FGiaTriVnd
        {
            get => _fGiaTriVnd;
            set => SetProperty(ref _fGiaTriVnd, value);
        }

        private double? _fGiaTriEur;
        public double? FGiaTriEur
        {
            get => _fGiaTriEur;
            set => SetProperty(ref _fGiaTriEur, value);
        }

        private double? _fGiaTriHopDong_NgoaiTeKhac;
        public double? FGiaTriHopDong_NgoaiTeKhac
        {
            get => _fGiaTriHopDong_NgoaiTeKhac;
            set => SetProperty(ref _fGiaTriHopDong_NgoaiTeKhac, value);
        }

        private double? _fGiaTriHopDong_Usd;
        public double? FGiaTriHopDong_Usd
        {
            get => _fGiaTriHopDong_Usd;
            set => SetProperty(ref _fGiaTriHopDong_Usd, value);
        }

        private double? _fGiaTriHopDong_Vnd;
        public double? FGiaTriHopDong_Vnd
        {
            get => _fGiaTriHopDong_Vnd;
            set => SetProperty(ref _fGiaTriHopDong_Vnd, value);
        }

        private double? _fGiaTriHopDong_Eur;
        public double? FGiaTriHopDong_Eur
        {
            get => _fGiaTriHopDong_Eur;
            set => SetProperty(ref _fGiaTriHopDong_Eur, value);
        }
        private bool _isDisable;
        public bool IsDisable
        {
            get => _isDisable;
            set => SetProperty(ref _isDisable, value);
        }
        public string STenNhaThau { get; set; }

        private double? _fGiaTriGoiThauNgoaiTeKhac;
        public double? FGiaTriGoiThauNgoaiTeKhac
        {
            get => _fGiaTriGoiThauNgoaiTeKhac;
            set => SetProperty(ref _fGiaTriGoiThauNgoaiTeKhac, value);
        }

        private double? _fGiaTriGoiThauUsd;
        public double? FGiaTriGoiThauUsd
        {
            get => _fGiaTriGoiThauUsd;
            set => SetProperty(ref _fGiaTriGoiThauUsd, value);
        }

        private double? _fGiaTriGoiThauVnd;
        public double? FGiaTriGoiThauVnd
        {
            get => _fGiaTriGoiThauVnd;
            set => SetProperty(ref _fGiaTriGoiThauVnd, value);
        }

        private double? _fGiaTriGoiThauEur;
        public double? FGiaTriGoiThauEur
        {
            get => _fGiaTriGoiThauEur;
            set => SetProperty(ref _fGiaTriGoiThauEur, value);
        }
        public Guid? IIdDuAnId { get; set; }
        public string STenGoiThau { get; set; }
        private double? _fGiaTriGoiThauConLaiUsd;
        public double? FGiaTriGoiThauConLaiUsd
        {
            get => _fGiaTriGoiThauConLaiUsd;
            set => SetProperty(ref _fGiaTriGoiThauConLaiUsd, value);
        }

        private double? _fGiaTriGoiThauConLaiVnd;
        public double? FGiaTriGoiThauConLaiVnd
        {
            get => _fGiaTriGoiThauConLaiVnd;
            set => SetProperty(ref _fGiaTriGoiThauConLaiVnd, value);
        }

        private double? _fGiaTriGoiThauConLaiEur;
        public double? FGiaTriGoiThauConLaiEur
        {
            get => _fGiaTriGoiThauConLaiEur;
            set => SetProperty(ref _fGiaTriGoiThauConLaiEur, value);
        }

        private double? _fGiaTriGoiThauConLaiNgoaiTeKhac;
        public double? FGiaTriGoiThauConLaiNgoaiTeKhac
        {
            get => _fGiaTriGoiThauConLaiNgoaiTeKhac;
            set => SetProperty(ref _fGiaTriGoiThauConLaiNgoaiTeKhac, value);
        }

        public bool EditChiPhi { get; set; }
        public int? IThoiGianThucHien { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid IIdHopDongGoiThauNhaThauId { get; set; } = Guid.NewGuid();
    }
}
