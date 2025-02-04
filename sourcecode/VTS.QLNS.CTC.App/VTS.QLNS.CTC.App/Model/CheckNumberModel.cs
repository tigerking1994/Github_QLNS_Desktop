using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class CheckNumberModel : BindableBase
    {
        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private string _soThuTu;
        public string SoThuTu
        {
            get => _soThuTu;
            set => SetProperty(ref _soThuTu, value);
        }
        private string _soChungTu;
        public string SoChungTu
        {
            get => _soChungTu;
            set => SetProperty(ref _soChungTu, value);
        }

        private string _thangQuy;
        public string ThangQuy
        {
            get => _thangQuy;
            set => SetProperty(ref _thangQuy, value);
        }

        private string _donVi;
        public string DonVi
        {
            get => _donVi;
            set => SetProperty(ref _donVi, value);
        }

        private bool _locked;
        public bool Locked
        {
            get => _locked;
            set => SetProperty(ref _locked, value);
        }

        private string _tuChi;
        public string TuChi
        {
            get => _tuChi;
            set => SetProperty(ref _tuChi, value);
        }

        private string _hienVat;
        public string HienVat
        {
            get => _hienVat;
            set => SetProperty(ref _hienVat, value);
        }

        private string _lns;
        public string LNS
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _soQD;
        public string SoQD
        {
            get => _soQD;
            set => SetProperty(ref _soQD, value);
        }

        private string _ngayQD;
        public string NgayQD
        {
            get => _ngayQD;
            set => SetProperty(ref _ngayQD, value);
        }

        private string _moTa;
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private string _ngayChungTu;
        public string NgayChungTu
        {
            get => _ngayChungTu;
            set => SetProperty(ref _ngayChungTu, value);
        }

        private string _nguoiTao;
        public string NguoiTao
        {
            get => _nguoiTao;
            set => SetProperty(ref _nguoiTao, value);
        }

        private string _ngayTao;
        public string NgayTao
        {
            get => _ngayTao;
            set => SetProperty(ref _ngayTao, value);
        }

        private string _suaCuoi;
        public string SuaCuoi
        {
            get => _suaCuoi;
            set => SetProperty(ref _suaCuoi, value);
        }
    }
}
