using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class DivisionTotalModel : BindableBase
    {
        private double _fTongTuChi;
        public double FTongTuChi
        {
            get => _fTongTuChi;
            set
            {
                SetProperty(ref _fTongTuChi, value);
                OnPropertyChanged(nameof(FTongCapBangTien));
            }
        }

        private double _fTongRutKBNN;
        public double FTongRutKBNN
        {
            get => _fTongRutKBNN;
            set
            {
                SetProperty(ref _fTongRutKBNN, value);
                OnPropertyChanged(nameof(FTongCapBangTien));
            }
        }

        public double FTongCapBangTien
        {
            get => _fTongTuChi - _fTongRutKBNN;
            set
            {
                
            }
        }

        private double _fTongHienVat;
        public double FTongHienVat
        {
            get => _fTongHienVat;
            set => SetProperty(ref _fTongHienVat, value);
        }

        private double _fTongHangNhap;
        public double FTongHangNhap
        {
            get => _fTongHangNhap;
            set => SetProperty(ref _fTongHangNhap, value);
        }

        private double _fTongHangMua;
        public double FTongHangMua
        {
            get => _fTongHangMua;
            set => SetProperty(ref _fTongHangMua, value);
        } 
        
        private double _fTongPhanCap;
        public double FTongPhanCap
        {
            get => _fTongPhanCap;
            set => SetProperty(ref _fTongPhanCap, value);
        }

        private double _fTongDuPhong;
        public double FTongDuPhong
        {
            get => _fTongDuPhong;
            set => SetProperty(ref _fTongDuPhong, value);
        }

        public DivisionTotalModel()
        {
            _fTongTuChi = 0;
            _fTongRutKBNN = 0;
            _fTongHienVat = 0;
            _fTongHangNhap = 0;
            _fTongHangMua = 0;
            _fTongPhanCap = 0;
            _fTongDuPhong = 0;
        }
    }
}
