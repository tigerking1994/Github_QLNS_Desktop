using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQtcNamKinhPhiKhacChiTietModel : ModelBase
    {
        public Guid Id { get; set; }
        private Guid? _iID_QTC_Nam_KPK;

        public Guid? IID_QTC_Nam_KPK
        {
            get => _iID_QTC_Nam_KPK;
            set => SetProperty(ref _iID_QTC_Nam_KPK, value);
        }
        private Guid? _iID_MucLucNganSach;

        public Guid? IID_MucLucNganSach
        {
            get => _iID_MucLucNganSach;
            set => SetProperty(ref _iID_MucLucNganSach, value);
        }
        private string _sNoiDung;

        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }
        private DateTime? _dNgaySua;

        public DateTime? DNgaySua
        {
            get => _dNgaySua;
            set => SetProperty(ref _dNgaySua, value);
        }
        private DateTime? _dNgayTao;

        public DateTime? DNgayTao
        {
            get => _dNgayTao;
            set => SetProperty(ref _dNgayTao, value);
        }
        private string _sNguoiSua;

        public string SNguoiSua
        {
            get => _sNguoiSua;
            set => SetProperty(ref _sNguoiSua, value);
        }
        private string _sNguoiTao;

        public string SNguoiTao
        {
            get => _sNguoiTao;
            set => SetProperty(ref _sNguoiTao, value);
        }
        private double? _fTien_DuToanNamTruocChuyenSang;

        public double? FTien_DuToanNamTruocChuyenSang
        {
            get => _fTien_DuToanNamTruocChuyenSang;
            set
            {
                SetProperty(ref _fTien_DuToanNamTruocChuyenSang, value);
                OnPropertyChanged(nameof(FTien_TongDuToanDuocGiao));
                OnPropertyChanged(nameof(FTienThua));
                OnPropertyChanged(nameof(FTienThieu));
                OnPropertyChanged(nameof(FTiLeThucHienTrenDuToan));
            }
        }

        private double? _fDuToanNamTruocChuyenSang;
        public double? FDuToanNamTruocChuyenSang
        {
            get => _fDuToanNamTruocChuyenSang;
            set
            {
                SetProperty(ref _fDuToanNamTruocChuyenSang, value);
            }
        }

        private double? _fTien_DuToanGiaoNamNay;

        public double? FTien_DuToanGiaoNamNay
        {
            get => _fTien_DuToanGiaoNamNay;
            set
            {
                SetProperty(ref _fTien_DuToanGiaoNamNay, value);
                OnPropertyChanged(nameof(FTien_TongDuToanDuocGiao));
                OnPropertyChanged(nameof(FTienThua));
                OnPropertyChanged(nameof(FTienThieu));
                OnPropertyChanged(nameof(FTiLeThucHienTrenDuToan));

            }
        }
        private double? _fTien_TongDuToanDuocGiao;

        public double? FTien_TongDuToanDuocGiao
        {
            get => FTien_DuToanNamTruocChuyenSang.GetValueOrDefault(0) + FTien_DuToanGiaoNamNay.GetValueOrDefault(0);
            set
            {
                SetProperty(ref _fTien_TongDuToanDuocGiao, value);
            }
        }
        private double? _fTien_ThucChi;

        public double? FTien_ThucChi
        {
            get => _fTien_ThucChi;
            set
            {
                SetProperty(ref _fTien_ThucChi, value);
                OnPropertyChanged(nameof(FTienThua));
                OnPropertyChanged(nameof(FTienThieu));
                OnPropertyChanged(nameof(FTiLeThucHienTrenDuToan));
            }
        }
        private double? _fTienThua;
        public double? FTienThua
        {
            get => _fTienThua = !string.IsNullOrEmpty(SDuToanChiTietToi) ? (FTien_TongDuToanDuocGiao > FTien_ThucChi ? FTien_TongDuToanDuocGiao - FTien_ThucChi : 0) : 0;
            set
            {
                SetProperty(ref _fTienThua, value);
            }
        }
        private double? _fTienThieu;
        public double? FTienThieu
        {
            get => _fTienThieu = !string.IsNullOrEmpty(SDuToanChiTietToi) ? (FTien_TongDuToanDuocGiao < FTien_ThucChi ? FTien_ThucChi - FTien_TongDuToanDuocGiao : 0) : 0;
            set
            {
                SetProperty(ref _fTienThieu, value);
            }
        }
        public double? FTiLeThucHienTrenDuToan => !string.IsNullOrEmpty(SDuToanChiTietToi) ? (FTien_TongDuToanDuocGiao > 0 ? (FTien_ThucChi / FTien_TongDuToanDuocGiao) * 100 : 0) : 0;

        public int STT { get; set; }
        public string SGhiChu { get; set; }
        public bool IsAuToFillTuChi { get; set; }
        public string SXauNoiMa { get; set; }
        public string IIdMaDonVi { get; set; }
        public int? INamLamViec { get; set; }
        public string SMoTa { get; set; }
        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }
        public Guid IdParent { get; set; }
        public string STenDonVi { get; set; }
        public string STTM { get; set; }
        public bool BHangCha { get; set; }
        public bool IsHasData => FTien_TongDuToanDuocGiao.GetValueOrDefault() != 0
                                || FTiLeThucHienTrenDuToan.GetValueOrDefault() != 0
                                || FTien_ThucChi.GetValueOrDefault() != 0
                                || FTien_DuToanNamTruocChuyenSang.GetValueOrDefault() != 0
                                || FTien_DuToanGiaoNamNay.GetValueOrDefault() != 0
                                || FTienThua.GetValueOrDefault() != 0
                                || FTienThieu.GetValueOrDefault() != 0;

        public string SDuToanChiTietToi { get; set; }
    }
}
