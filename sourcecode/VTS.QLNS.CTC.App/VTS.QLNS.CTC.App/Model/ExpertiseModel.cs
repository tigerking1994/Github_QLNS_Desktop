using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ExpertiseModel : BindableBase
    {
        public Guid Id { get; set; }

        private string _soChungTu;
        public string SoChungTu
        {
            get => _soChungTu;
            set => SetProperty(ref _soChungTu, value);
        }

        private bool _isLocked;
        public bool IsLocked
        {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }

        private string _moTa;
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private string _soQuyetDinh;
        public string SoQuyetDinh
        {
            get => _soQuyetDinh;
            set => SetProperty(ref _soQuyetDinh, value);
        }

        private DateTime? _ngayQuyetDinh;
        public DateTime? NgayQuyetDinh
        {
            get => _ngayQuyetDinh;
            set => SetProperty(ref _ngayQuyetDinh, value);
        }

        private DateTime? _ngayChungTu;
        public DateTime? NgayChungTu
        {
            get => _ngayChungTu;
            set => SetProperty(ref _ngayChungTu, value);
        }

        private string _idDonVi;
        public string IdDonVi
        {
            get => _idDonVi;
            set => SetProperty(ref _idDonVi, value);
        }

        public int? NamLamViec { get; set; }
        public int? NamNganSach { get; set; }
        public int? NguonNganSach { get; set; }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        public string TenDonViDisplay
        {
            get => string.Format("{0} - {1}", IdDonVi, TenDonVi);
        }

        private double _tongTuChiCTC;
        public double TongTuChiCTC
        {
            get => _tongTuChiCTC;
            set => SetProperty(ref _tongTuChiCTC, value);
        }

        private double _tongTuChiPrev;
        public double TongTuChiPrev
        {
            get => _tongTuChiPrev;
            set => SetProperty(ref _tongTuChiPrev, value);
        }

        private double _tongTuChiNganh;
        public double TongTuChiNganh
        {
            get => _tongTuChiNganh;
            set => SetProperty(ref _tongTuChiNganh, value);
        }

        private double _tongSuDungTonKhoNganh;
        public double TongSuDungTonKhoNganh
        {
            get => _tongSuDungTonKhoNganh;
            set => SetProperty(ref _tongSuDungTonKhoNganh, value);
        }

        private double _tongSuDungTonKhoPrev;
        public double TongSuDungTonKhoPrev
        {
            get => _tongSuDungTonKhoPrev;
            set => SetProperty(ref _tongSuDungTonKhoPrev, value);
        }

        private double _tongChiDacThuNganhPhanCapNganh;
        public double TongChiDacThuNganhPhanCapNganh
        {
            get => _tongChiDacThuNganhPhanCapNganh;
            set => SetProperty(ref _tongChiDacThuNganhPhanCapNganh, value);
        }

        private double _tongTuChi;
        public double TongTuChi
        {
            get => _tongTuChi;
            set => SetProperty(ref _tongTuChi, value);
        }

        private double _tongSuDungTonKho;
        public double TongSuDungTonKho
        {
            get => _tongSuDungTonKho;
            set => SetProperty(ref _tongSuDungTonKho, value);
        }

        private double _tongChiDacThuNganhPhanCap;
        public double TongChiDacThuNganhPhanCap
        {
            get => _tongChiDacThuNganhPhanCap;
            set => SetProperty(ref _tongChiDacThuNganhPhanCap, value);
        }

        private double _tongChiDacThuNganhPhanCapPrev;
        public double TongChiDacThuNganhPhanCapPrev
        {
            get => _tongChiDacThuNganhPhanCapPrev;
            set => SetProperty(ref _tongChiDacThuNganhPhanCapPrev, value);
        }

        private double _tongHuyDongCTC;
        public double TongHuyDongCTC
        {
            get => _tongHuyDongCTC;
            set => SetProperty(ref _tongHuyDongCTC, value);
        }

        private double _tongHuyDongNganh;
        public double TongHuyDongNganh
        {
            get => _tongHuyDongNganh;
            set => SetProperty(ref _tongHuyDongNganh, value);
        }

        private double _tongTang;
        public double TongTang
        {
            get => _tongTang;
            set => SetProperty(ref _tongTang, value);
        }

        private double _tongGiam;
        public double TongGiam
        {
            get => _tongGiam;
            set => SetProperty(ref _tongGiam, value);
        }
        
        private double? _tuChi;
        public double? TuChi
        {
            get => _tuChi;
            set => SetProperty(ref _tuChi, value);
        }

        //private double? _tuChiNganh;
        //public double? TuChiNganh
        //{
        //    get => _tuChiNganh;
        //    set => SetProperty(ref _tuChiNganh, value);
        //}

        private string _userCreator;
        public string UserCreator
        {
            get => _userCreator;
            set => SetProperty(ref _userCreator, value);
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public int? _iLoai;
        public int? ILoai
        {
            get => _iLoai;
            set => SetProperty(ref _iLoai, value);
        }

        public int? _iLoaiChungTu;
        public int? ILoaiChungTu
        {
            get => _iLoaiChungTu;
            set => SetProperty(ref _iLoaiChungTu, value);
        }
    }
}
