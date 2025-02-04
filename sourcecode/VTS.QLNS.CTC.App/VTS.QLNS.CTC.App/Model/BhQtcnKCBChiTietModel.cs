using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQtcnKCBChiTietModel : DetailModelBase
    {
        public Guid Id { get; set; }
        public Guid IIdQTCNamKCBQuanYDonVi { get; set; }
        public Guid IIdMucLucNganSach { get; set; }
        public string SNoiDung { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }

        private Double? _fTienDuToanNamTruocChuyenSang;
        public Double? FTienDuToanNamTruocChuyenSang
        {
            get => _fTienDuToanNamTruocChuyenSang;
            set => SetProperty(ref _fTienDuToanNamTruocChuyenSang, value);
        }

        private Double? _fDuToanNamTruocChuyenSang;
        public Double? FDuToanNamTruocChuyenSang
        {
            get => _fDuToanNamTruocChuyenSang;
            set => SetProperty(ref _fDuToanNamTruocChuyenSang, value);
        }

        private Double? _fTienDuToanGiaoNamNay;
        public Double? FTienDuToanGiaoNamNay
        {
            get => _fTienDuToanGiaoNamNay;
            set => SetProperty(ref _fTienDuToanGiaoNamNay, value);
        }

        private Double? _fTienTongDuToanDuocGiao;
        public Double? FTienTongDuToanDuocGiao
        {
            get => _fTienTongDuToanDuocGiao = FTienDuToanNamTruocChuyenSang.GetValueOrDefault(0) + FTienDuToanGiaoNamNay.GetValueOrDefault(0);
            set
            {
                SetProperty(ref _fTienTongDuToanDuocGiao, value);
                //OnPropertyChanged(nameof(FTiLeThucHienTrenDuToan));
            }
        }

        private Double? _fTienThucChi;
        public Double? FTienThucChi
        {
            get => _fTienThucChi;
            set
            {
                SetProperty(ref _fTienThucChi, value);
                OnPropertyChanged(nameof(FTienThua));
                OnPropertyChanged(nameof(FTienThieu));
                OnPropertyChanged(nameof(FTiLeThucHienTrenDuToan));
            }
        }

        private Double? _fTienThua;
        public Double? FTienThua
        {
            get => _fTienThua = !string.IsNullOrEmpty(SDuToanChiTietToi) ? ((FTienThucChi.GetValueOrDefault(0) < FTienTongDuToanDuocGiao.GetValueOrDefault(0)) ? FTienTongDuToanDuocGiao.GetValueOrDefault(0) - FTienThucChi.GetValueOrDefault(0) : 0) : 0;
            set => SetProperty(ref _fTienThua, value);
        }

        private Double? _fTienThieu;
        public Double? FTienThieu
        {
            get => _fTienThieu = !string.IsNullOrEmpty(SDuToanChiTietToi) ? ((FTienThucChi.GetValueOrDefault(0) > FTienTongDuToanDuocGiao.GetValueOrDefault(0)) ? FTienThucChi.GetValueOrDefault(0) - FTienTongDuToanDuocGiao.GetValueOrDefault(0) : 0) : 0;
            set => SetProperty(ref _fTienThieu, value);
        }

        private Double? _fTiLeThucHienTrenDuToan;
        public Double? FTiLeThucHienTrenDuToan
        {
            get => _fTiLeThucHienTrenDuToan = !string.IsNullOrEmpty(SDuToanChiTietToi) ? ((FTienTongDuToanDuocGiao.GetValueOrDefault(0) > 0) ? (FTienThucChi.GetValueOrDefault(0) / FTienTongDuToanDuocGiao.GetValueOrDefault(0)) * 100 : 0) : 0;
            set => SetProperty(ref _fTiLeThucHienTrenDuToan, value);
        }
        public Guid? IID_MLNS { get; set; }
        public Guid? IID_MLNS_Cha { get; set; }
        public string SLNS { get; set; }
        public string SL { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
        public string SXauNoiMa { get; set; }
        public string STenDonVi { get; set; }
        public string IIdMaDonVi { get; set; }
        public int? INamLamViec { get; set; }
        public bool BHangCha { get; set; }
        public override bool IsEditable => !BHangCha && !IsDeleted;
        public string SDuToanChiTietToi { get; set; }

        public bool IsHasData => FTienThucChi.GetValueOrDefault(0) != 0 || FTienTongDuToanDuocGiao.GetValueOrDefault(0) != 0 || FDuToanNamTruocChuyenSang.GetValueOrDefault(0) != 0 || FTienDuToanNamTruocChuyenSang.GetValueOrDefault(0) != 0;
    }
}
