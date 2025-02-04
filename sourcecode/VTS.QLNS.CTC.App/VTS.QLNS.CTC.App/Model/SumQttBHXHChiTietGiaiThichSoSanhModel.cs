using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class SumQttBHXHChiTietGiaiThichSoSanhModel : BindableBase
    {
        private double? _fTongSoPhaiThuNop;
        public double? FTongSoPhaiThuNop
        {
            get => _fTongSoPhaiThuNop.GetValueOrDefault();
            set => SetProperty(ref _fTongSoPhaiThuNop, value);
        }

        private double? _fTongSoDaNopTrongNam;
        public double? FTongSoDaNopTrongNam
        {
            get => _fTongSoDaNopTrongNam.GetValueOrDefault();
            set => SetProperty(ref _fTongSoDaNopTrongNam, value);
        }

        private double? _fTongSoDaNopSau3112;
        public double? FTongSoDaNopSau3112
        {
            get => _fTongSoDaNopSau3112.GetValueOrDefault();
            set => SetProperty(ref _fTongSoDaNopSau3112, value);
        }

        private double? _fTongCongSoDaNop;
        public double? FTongCongSoDaNop
        {
            get => _fTongCongSoDaNop.GetValueOrDefault();
            set => SetProperty(ref _fTongCongSoDaNop, value);
        }

        private double? _fTongSoConPhaiNop;
        public double? FTongSoConPhaiNop
        {
            get => _fTongSoConPhaiNop.GetValueOrDefault();
            set => SetProperty(ref _fTongSoConPhaiNop, value);
        }
    }
}
