using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ExpertiseModelDetailModel : DetailModelBase
    {
        private Guid _idMucLuc;
        public Guid IdMucLuc
        {
            get => _idMucLuc;
            set => SetProperty(ref _idMucLuc, value);
        }

        private string _kyHieu;
        public string KyHieu
        {
            get => _kyHieu;
            set => SetProperty(ref _kyHieu, value);
        }

        private string _m;
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _nganhParent;
        public string NganhParent
        {
            get => _nganhParent;
            set => SetProperty(ref _nganhParent, value);
        }

        private string _sTT;
        public string STT
        {
            get => _sTT;
            set => SetProperty(ref _sTT, value);
        }

        private string _sTTBC;
        public string STTBC
        {
            get => _sTTBC;
            set => SetProperty(ref _sTTBC, value);
        }

        private string _moTa;
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        public int ITrangThai { get; set; }
        public int NamLamViec { get; set; }
        public int NamNganSach { get; set; }
        public int NguonNganSach { get; set; }

        private Guid _idParent;
        public Guid IdParent
        {
            get => _idParent;
            set => SetProperty(ref _idParent, value);
        }

        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private Guid? _idDb;
        public Guid? IdDb
        {
            get => _idDb;
            set => SetProperty(ref _idDb, value);
        }

        private Guid? _idChungTu;
        public Guid? IdChungTu
        {
            get => _idChungTu;
            set => SetProperty(ref _idChungTu, value);
        }

        private string _idDonVi;
        public string IdDonVi
        {
            get => _idDonVi;
            set => SetProperty(ref _idDonVi, value);
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private double _tuChiCTC;
        public double TuChiCTC
        {
            get => _tuChiCTC;
            set
            {
                SetProperty(ref _tuChiCTC, value);
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
            }
        }

        private double _tuChiNganh;
        public double TuChiNganh
        {
            get => _tuChiNganh;
            set
            {
                SetProperty(ref _tuChiNganh, value);
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
            }
        }

        private double _tuChi;
        public double TuChi
        {
            get => _tuChi;
            set
            {
                SetProperty(ref _tuChi, value);
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
            }
        }

        private double _tuChiPrev;
        public double TuChiPrev
        {
            get => _tuChiPrev;
            set
            {
                SetProperty(ref _tuChiPrev, value);
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
            }
        }

        private double _suDungTonKhoPrev;
        public double SuDungTonKhoPrev
        {
            get => _suDungTonKhoPrev;
            set
            {
                SetProperty(ref _suDungTonKhoPrev, value);
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
            }
        }

        private double _tang;
        public double Tang
        {
            get => ((TuChiNganh + SuDungTonKhoNganh + ChiDacThuNganhPhanCapNganh) > (TuChiPrev + SuDungTonKhoPrev + ChiDacThuNganhPhanCapPrev) && !IsHangCha) ?
                ((TuChiNganh + SuDungTonKhoNganh + ChiDacThuNganhPhanCapNganh) - (TuChiPrev + SuDungTonKhoPrev + ChiDacThuNganhPhanCapPrev)) : _tang;
            set => SetProperty(ref _tang, value);
        }

        private double _giam;
        public double Giam
        {
            get => ((TuChiPrev + SuDungTonKhoPrev + ChiDacThuNganhPhanCapPrev) > (TuChiNganh + SuDungTonKhoNganh + ChiDacThuNganhPhanCapNganh) && !IsHangCha) ?
                ((TuChiPrev + SuDungTonKhoPrev + ChiDacThuNganhPhanCapPrev) - (TuChiNganh + SuDungTonKhoNganh + ChiDacThuNganhPhanCapNganh)) : _giam;
            set => SetProperty(ref _giam, value);
        }

        private double _suDungTonKho;
        public double SuDungTonKho
        {
            get => _suDungTonKho;
            set
            {
                SetProperty(ref _suDungTonKho, value);
            }
        }

        private double _chiDacThuNganhPhanCap;
        public double ChiDacThuNganhPhanCap
        {
            get => _chiDacThuNganhPhanCap;
            set
            {
                SetProperty(ref _chiDacThuNganhPhanCap, value);
            }
        }

        private double _suDungTonKhoNganh;
        public double SuDungTonKhoNganh
        {
            get => _suDungTonKhoNganh;
            set
            {
                SetProperty(ref _suDungTonKhoNganh, value);
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
            }
        }

        private double _chiDacThuNganhPhanCapNganh;
        public double ChiDacThuNganhPhanCapNganh
        {
            get => _chiDacThuNganhPhanCapNganh;
            set
            {
                SetProperty(ref _chiDacThuNganhPhanCapNganh, value);
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
            }
        }

        private double _chiDacThuNganhPhanCapPrev;
        public double ChiDacThuNganhPhanCapPrev
        {
            get => _chiDacThuNganhPhanCapPrev;
            set
            {
                SetProperty(ref _chiDacThuNganhPhanCapPrev, value);
                OnPropertyChanged(nameof(Tang));
                OnPropertyChanged(nameof(Giam));
            }
        }

        private double _huyDongCTC;
        public double HuyDongCTC
        {
            get => _huyDongCTC;
            set => SetProperty(ref _huyDongCTC, value);
        }

        private double _huyDongNganh;
        public double HuyDongNganh
        {
            get => _huyDongNganh;
            set => SetProperty(ref _huyDongNganh, value);
        }

        private DateTime? _dateCreated;
        public DateTime? DateCreated
        {
            get => _dateCreated;
            set => SetProperty(ref _dateCreated, value);
        }

        private string _userCreator;
        public string UserCreator
        {
            get => _userCreator;
            set => SetProperty(ref _userCreator, value);
        }

        private string _ghiChu;
        public string GhiChu
        {
            get => _ghiChu;
            set => SetProperty(ref _ghiChu, value);
        }

        private string _nganh;
        public string Nganh
        {
            get => _nganh;
            set => SetProperty(ref _nganh, value);
        }

        public bool HasData => !IsHangCha && (TuChi != 0 || TuChiNganh != 0 || TuChiCTC != 0 || !string.IsNullOrEmpty(GhiChu));
    }
}