using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQtcnBHXHChiTietModel : DetailModelBase
    {
        public Guid Id { get; set; }
        public Guid IIdQTCNamCheDoBHXH { get; set; }
        public Guid IIdMucLucNganSach { get; set; }
        public string SLoaiTroCap { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }

        public int? _iSoDuToanDuocDuyet;
        public int? ISoDuToanDuocDuyet { get => _iSoDuToanDuocDuyet; set => SetProperty(ref _iSoDuToanDuocDuyet, value); }

        public Double? _fienDuToanDuyet;
        public Double? FTienDuToanDuyet { get => _fienDuToanDuyet; set => SetProperty(ref _fienDuToanDuyet, value); }

        public Double? fTongTienThuChiCaNam;
        public Double? FTongTienThuChiCaNam { get => fTongTienThuChiCaNam; set => SetProperty(ref fTongTienThuChiCaNam, value); }

        public int? _iTongSoThucChi;
        public int? ITongSoThucChi { get => _iTongSoThucChi; set => SetProperty(ref _iTongSoThucChi, value); }

        public Double? _fTongTienThucChi;
        public Double? FTongTienThucChi
        {
            get => _fTongTienThucChi;
            set => SetProperty(ref _fTongTienThucChi, value);
        }

        public int? _iSoSQThucChi;
        public int? ISoSQThucChi { get => _iSoSQThucChi; set => SetProperty(ref _iSoSQThucChi, value); }

        public Double? _fTienSQThucChi;
        public Double? FTienSQThucChi
        {
            get => _fTienSQThucChi;
            set
            {
                SetProperty(ref _fTienSQThucChi, value);
                OnPropertyChanged(nameof(FTienThieu));
                OnPropertyChanged(nameof(FTienThua));
                OnPropertyChanged(nameof(FTiLeThucHienTrenDuToan));
            }

        }

        public int? _iSoQNCNThucChi;
        public int? ISoQNCNThucChi
        {
            get => _iSoQNCNThucChi;
            set => SetProperty(ref _iSoQNCNThucChi, value);
        }

        public Double? _fTienQNCNThucChi;
        public Double? FTienQNCNThucChi
        {
            get => _fTienQNCNThucChi;
            set
            {
                SetProperty(ref _fTienQNCNThucChi, value);
                OnPropertyChanged(nameof(FTienThieu));
                OnPropertyChanged(nameof(FTienThua));
                OnPropertyChanged(nameof(FTiLeThucHienTrenDuToan));
            }
        }

        public int? _iSoCNVCQPThucChi;
        public int? ISoCNVCQPThucChi { get => _iSoCNVCQPThucChi; set => SetProperty(ref _iSoCNVCQPThucChi, value); }

        public Double? _fTienCNVCQPThucChi;
        public Double? FTienCNVCQPThucChi
        {
            get => _fTienCNVCQPThucChi;
            set
            {
                SetProperty(ref _fTienCNVCQPThucChi, value);
                OnPropertyChanged(nameof(FTienThieu));
                OnPropertyChanged(nameof(FTienThua));
                OnPropertyChanged(nameof(FTiLeThucHienTrenDuToan));
            }
        }

        public int? _iSoHSQBSThucChi;
        public int? ISoHSQBSThucChi { get => _iSoHSQBSThucChi; set => SetProperty(ref _iSoHSQBSThucChi, value); }

        public Double? _fTienHSQBSThucChi;
        public Double? FTienHSQBSThucChi
        {
            get => _fTienHSQBSThucChi;
            set
            {
                SetProperty(ref _fTienHSQBSThucChi, value);
                OnPropertyChanged(nameof(FTienThieu));
                OnPropertyChanged(nameof(FTienThua));
                OnPropertyChanged(nameof(FTiLeThucHienTrenDuToan));
            }
        }

        public Double? _fTienThua;
        public Double? FTienThua
        {
            get => _fTienThua = (BHangCha && (!string.IsNullOrEmpty(SDuToanChiTietToi) || string.IsNullOrEmpty(SL))) ? (FTienDuToanDuyet.GetValueOrDefault(0) > FTongTienThucChi.GetValueOrDefault(0) ? FTienDuToanDuyet.GetValueOrDefault(0) - FTongTienThucChi.GetValueOrDefault(0) : 0) : 0;
            set => SetProperty(ref _fTienThua, value);
        }

        public Double? _fTienThieu;
        public Double? FTienThieu
        {
            get => _fTienThieu = (BHangCha && (!string.IsNullOrEmpty(SDuToanChiTietToi) || string.IsNullOrEmpty(SL))) ? (FTienDuToanDuyet.GetValueOrDefault(0) < FTongTienThucChi.GetValueOrDefault(0) ? FTongTienThucChi.GetValueOrDefault(0) - FTienDuToanDuyet.GetValueOrDefault(0) : 0) : 0;
            set => SetProperty(ref _fTienThieu, value);
        }

        public Double? _fTiLeThucHienTrenDuToan;
        public Double? FTiLeThucHienTrenDuToan
        {
            get => _fTiLeThucHienTrenDuToan = (BHangCha && (!string.IsNullOrEmpty(SDuToanChiTietToi) || string.IsNullOrEmpty(SL))) ? (FTienDuToanDuyet.GetValueOrDefault(0) > 0 ? (FTongTienThucChi.GetValueOrDefault(0) / FTienDuToanDuyet.GetValueOrDefault(0)) * 100 : 0) : 0;
            set => SetProperty(ref _fTiLeThucHienTrenDuToan, value);
        }
        public Guid? IID_MLNS { get; set; }
        public Guid? IID_MLNS_Cha { get; set; }
        public string SLNS { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
        public string SXauNoiMa { get; set; }

        public int? _iSoLDHDThucChi;
        public int? ISoLDHDThucChi { get => _iSoLDHDThucChi; set => SetProperty(ref _iSoLDHDThucChi, value); }

        public Double? _fTienLDHDThucChi;
        public Double? FTienLDHDThucChi { get => _fTienLDHDThucChi; set => SetProperty(ref _fTienLDHDThucChi, value); }
        public bool BHangCha { get; set; }
        public override bool IsHangCha => BHangCha;
        //public override bool IsEditable => !BHangCha && !IsDeleted;

        public string SDuToanChiTietToi { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public int INamLamViec { get; set; }
        public bool IsHadData => FTongTienThucChi.GetValueOrDefault(0) != 0 || FTienDuToanDuyet.GetValueOrDefault(0) != 0;

        public bool IsHadDataChil
        {
            get
            {
                if (!BHangCha)
                {
                    return FTongTienThucChi.GetValueOrDefault(0) != 0 || ITongSoThucChi.GetValueOrDefault(0) != 0 || FTienDuToanDuyet.GetValueOrDefault(0) != 0;
                }
                return false;
            }
        }
    }
}
