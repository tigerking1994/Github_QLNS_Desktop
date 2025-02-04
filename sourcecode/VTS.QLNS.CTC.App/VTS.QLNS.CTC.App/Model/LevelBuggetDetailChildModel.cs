using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class LevelBuggetDetailChildModel : DetailModelBase
    {
        private bool _isRoot;
        public bool IsRoot
        {
            get => _isRoot;
            set
            {
                SetProperty(ref _isRoot, value);
            }
        }

        private bool _isTotal;
        public bool IsTotal
        {
            get => _isTotal;
            set
            {
                SetProperty(ref _isTotal, value);
            }
        }

        private Guid _mucLucID;
        public Guid MucLucID
        {
            get => _mucLucID;
            set => SetProperty(ref _mucLucID, value);
        }

        private Guid _mucLucParentId;
        public Guid MucLucParentId
        {
            get => _mucLucParentId;
            set => SetProperty(ref _mucLucParentId, value);
        }

        private string _lNS;
        public string LNS
        {
            get => _lNS;
            set => SetProperty(ref _lNS, value);
        }

        private string _l;
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tM;
        public string TM
        {
            get => _tM;
            set => SetProperty(ref _tM, value);
        }

        private string _tTM;
        public string TTM
        {
            get => _tTM;
            set => SetProperty(ref _tTM, value);
        }

        private string _nG;
        public string NG
        {
            get => _nG;
            set => SetProperty(ref _nG, value);
        }

        private string _tNG;
        public string TNG
        {
            get => _tNG;
            set => SetProperty(ref _tNG, value);
        }

        private string _tNG1;
        public string TNG1
        {
            get => _tNG1;
            set => SetProperty(ref _tNG1, value);
        }

        private string _tNG2;
        public string TNG2
        {
            get => _tNG2;
            set => SetProperty(ref _tNG2, value);
        }

        private string _tNG3;
        public string TNG3
        {
            get => _tNG3;
            set => SetProperty(ref _tNG3, value);
        }

        private string _moTa;
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private string _xauNoiMa;
        public string XauNoiMa
        {
            get => _xauNoiMa;
            set => SetProperty(ref _xauNoiMa, value);
        }

        private string _idDonViMLNS;
        public string IdDonViMLNS
        {
            get => _idDonViMLNS;
            set => SetProperty(ref _idDonViMLNS, value);
        }

        private Guid? _id;
        public Guid? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private Guid? _soLieuChiTietId;
        public Guid? SoLieuChiTietId
        {
            get => _soLieuChiTietId;
            set => SetProperty(ref _soLieuChiTietId, value);
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

        private Guid? _mLNSId;
        public Guid? MLNSId
        {
            get => _mLNSId;
            set => SetProperty(ref _mLNSId, value);
        }

        private double _phanCap;
        public double PhanCap
        {
            get => _phanCap;
            set => SetProperty(ref _phanCap, value);
        }

        private double _hienVat;
        public double HienVat
        {
            get => _hienVat;
            set => SetProperty(ref _hienVat, value);
        }

        private string _ghiChu;
        public string GhiChu
        {
            get => _ghiChu;
            set => SetProperty(ref _ghiChu, value);
        }

        public bool HasData => !IsHangCha && (PhanCap != 0 || HienVat != 0 || !string.IsNullOrEmpty(GhiChu));
    }
}
