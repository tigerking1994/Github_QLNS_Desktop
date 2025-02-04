using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class InitializationProjectDetailModel : DetailModelBase
    {
        private Guid? _idDb;
        public Guid? IdDb
        {
            get => _idDb;
            set => SetProperty(ref _idDb, value);
        }

        private string _lns;
        public string LNS
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
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

        private string _tm;
        public string TM
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        public string TTM
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }

        private string _ng;
        public string NG
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        private int _maNguonNganSach;
        public int MaNguonNganSach
        {
            get => _maNguonNganSach;
            set => SetProperty(ref _maNguonNganSach, value);
        }

        private string _tenNganSach;
        public string TenNganSach
        {
            get => _tenNganSach;
            set => SetProperty(ref _tenNganSach, value);
        }

        private string _moTaNganSach;
        public string MoTaNganSach
        {
            get => _moTaNganSach;
            set => SetProperty(ref _moTaNganSach, value);
        }

        private Guid? _idKhoiTaoID;
        public Guid? IdKhoiTaoID
        {
            get => _idKhoiTaoID;
            set => SetProperty(ref _idKhoiTaoID, value);
        }

        private int? _idNguonVonID;
        public int? IdNguonVonID
        {
            get => _idNguonVonID;
            set => SetProperty(ref _idNguonVonID, value);
        }

        private Guid? _idLoaiNguonVonID;
        public Guid? IdLoaiNguonVonID
        {
            get => _idLoaiNguonVonID;
            set => SetProperty(ref _idLoaiNguonVonID, value);
        }

        private double _kHVonHetNamTruoc;
        public double KHVonHetNamTruoc
        {
            get => _kHVonHetNamTruoc;
            set
            {
                SetProperty(ref _kHVonHetNamTruoc, value);
                OnPropertyChanged(nameof(KeHoachVonChuaCap));
            }
        }

        private double _luyKeThanhToanKLHT;
        public double LuyKeThanhToanKLHT
        {
            get => _luyKeThanhToanKLHT;
            set
            {
                SetProperty(ref _luyKeThanhToanKLHT, value);
                OnPropertyChanged(nameof(KeHoachVonChuaCap));
            }
        }

        private double _luyKeThanhToanTamUng;
        public double LuyKeThanhToanTamUng
        {
            get => _luyKeThanhToanTamUng;
            set
            {
                SetProperty(ref _luyKeThanhToanTamUng, value);
                OnPropertyChanged(nameof(KeHoachVonChuaCap));
            }
        }

        public double KeHoachVonChuaCap => KHVonHetNamTruoc - LuyKeThanhToanKLHT - LuyKeThanhToanTamUng;

        private double _thanhToanQuaKB;
        public double ThanhToanQuaKB
        {
            get => _thanhToanQuaKB;
            set => SetProperty(ref _thanhToanQuaKB, value);
        }

        private double _tamUngQuaKB;
        public double TamUngQuaKB
        {
            get => _tamUngQuaKB;
            set => SetProperty(ref _tamUngQuaKB, value);
        }

        private Guid? _idDonViTienTeID;
        public Guid? IdDonViTienTeID
        {
            get => _idDonViTienTeID;
            set => SetProperty(ref _idDonViTienTeID, value);
        }

        private double _tiGiaDonVi;
        public double TiGiaDonVi
        {
            get => _tiGiaDonVi;
            set => SetProperty(ref _tiGiaDonVi, value);
        }

        private Guid? _idTienTeID;
        public Guid? IdTienTeID
        {
            get => _idTienTeID;
            set => SetProperty(ref _idTienTeID, value);
        }

        private double _tiGia;
        public double TiGia
        {
            get => _tiGia;
            set => SetProperty(ref _tiGia, value);
        }

        private string _idMucID;
        public string IdMucID
        {
            get => _idMucID;
            set => SetProperty(ref _idMucID, value);
        }

        private string _idTieuMucID;
        public string IdTieuMucID
        {
            get => _idTieuMucID;
            set => SetProperty(ref _idTieuMucID, value);
        }

        private string _idTietMucID;
        public string IdTietMucID
        {
            get => _idTietMucID;
            set => SetProperty(ref _idTietMucID, value);
        }

        private string _idNganhID;
        public string IdNganhID
        {
            get => _idNganhID;
            set => SetProperty(ref _idNganhID, value);
        }

        private double _soChuyenChiTieuDaCap;
        public double SoChuyenChiTieuDaCap
        {
            get => _soChuyenChiTieuDaCap;
            set => SetProperty(ref _soChuyenChiTieuDaCap, value);
        }

        private double _soChuyenChiTieuChuaCap;
        public double SoChuyenChiTieuChuaCap
        {
            get => _soChuyenChiTieuChuaCap;
            set => SetProperty(ref _soChuyenChiTieuChuaCap, value);
        }
    }
}
