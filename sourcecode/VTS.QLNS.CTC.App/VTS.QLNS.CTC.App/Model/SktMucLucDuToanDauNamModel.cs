using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class SktMucLucDuToanDauNamModel : DetailModelBase
    {
        public Guid IdMucLuc { get; set; }

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

        private string _nganh;
        public string Nganh
        {
            get => _nganh;
            set => SetProperty(ref _nganh, value);
        }

        private string _stt;
        public string Stt
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _sttBC;
        public string SttBC
        {
            get => _sttBC;
            set => SetProperty(ref _sttBC, value);
        }

        private string _kyHieu;
        public string KyHieu
        {
            get => _kyHieu;
            set => SetProperty(ref _kyHieu, value);
        }

        private string _loaiNhap;
        public string LoaiNhap
        {
            get => _loaiNhap;
            set => SetProperty(ref _loaiNhap, value);
        }

        private string _mota;
        public string MoTa
        {
            get => _mota;
            set => SetProperty(ref _mota, value);
        }

        public bool BHangCha { get; set; }

        public bool IsHangCha => BHangCha;

        public int ITrangThai { get; set; }
        public int NamLamViec { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public string Muc { get; set; }
        public string Lns { get; set; }
        public double TuChi { get; set; }

        private string _kyHieuCha;
        public string KyHieuCha
        {
            get => _kyHieuCha;
            set => SetProperty(ref _kyHieuCha, value);
        }

        //public virtual ICollection<SktChungTuChiTietModel> SktChungTuChiTiets { get; set; }

        //private SktChungTuChiTietModel _sktChungTuChiTietModel;
        //public SktChungTuChiTietModel SktChungTuChiTietModel
        //{
        //    get => SktChungTuChiTiets.FirstOrDefault();
        //    set => SetProperty(ref _sktChungTuChiTietModel, value);
        //}

        public override string DetailInfoModalTitle => "Chi tiết MLSKT " + M;

        public override bool IsEditable => !IsDeleted;

        public Guid? IdParent { get; set; }

        private double _tongSo;
        public double TongSo
        {
            get => _tongSo;
            set
            {
                SetProperty(ref _tongSo, value);
                OnPropertyChanged(nameof(ConLai));
            }
        }

        private double _conLai;
        public double ConLai
        {
            get
            {
                return TuChi - TongSo - DtTuChi;
            }
            set => SetProperty(ref _conLai, value);
        }



        private double _tongSoHang;
        public double TongSoHang
        {
            get => _tongSoHang;
            set
            {
                SetProperty(ref _tongSoHang, value);
                OnPropertyChanged(nameof(ConLaiHang));
            }
        }

        public double TongHangDuToan => DtHangNhap + DtHangMua;

        private double _conLaiHang;
        public double ConLaiHang
        {
            get
            {
                return MuaHangHienVat - TongSoHang - DtHangNhap - DtHangMua;
            }
            set => SetProperty(ref _conLaiHang, value);
        }

        private double _tongDacThu;
        public double TongDacThu
        {
            get => _tongDacThu;
            set
            {
                SetProperty(ref _tongDacThu, value);
                OnPropertyChanged(nameof(ConLaiDacThu));
            }
        }

        private double _conLaiDacThu;
        public double ConLaiDacThu
        {
            get
            {
                return DacThu - TongDacThu - DtPhanCap;
            }
            set => SetProperty(ref _conLaiDacThu, value);
        }

        public double HangMua { get; set; }
        public double HangNhap { get; set; }
        public double PhanCap { get; set; }

        private double _muaHangHienVat;
        public double MuaHangHienVat
        {
            get => _muaHangHienVat;
            set => SetProperty(ref _muaHangHienVat, value);
        }
        public double DacThu { get; set; }

        private string _mlns;
        public string MLNS
        {
            get => _mlns;
            set => SetProperty(ref _mlns, value);
        }

        private double _dtTuChi;
        public double DtTuChi
        {
            get => _dtTuChi;
            set => SetProperty(ref _dtTuChi, value);
        }

        private double _dtHangNhap;
        public double DtHangNhap
        {
            get => _dtHangNhap;
            set => SetProperty(ref _dtHangNhap, value);
        }

        private double _dtHangMua;
        public double DtHangMua
        {
            get => _dtHangMua;
            set => SetProperty(ref _dtHangMua, value);
        }

        private double _dtPhanCap;
        public double DtPhanCap
        {
            get => _dtPhanCap;
            set => SetProperty(ref _dtPhanCap, value);
        }

        private double _dtDuPhong;
        public double DtDuPhong
        {
            get => _dtDuPhong;
            set => SetProperty(ref _dtDuPhong, value);
        }

        private double _dtChuaPhanCap;
        public double DtChuaPhanCap
        {
            get => _dtChuaPhanCap;
            set => SetProperty(ref _dtChuaPhanCap, value);
        }

        private bool _isVisibilily;
        public bool IsVisibilily
        {
            get => _isVisibilily;
            set => SetProperty(ref _isVisibilily, value);
        }
    }
}
