using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQtcQuyKPKChiTietModel : ModelBase
    {
        public Guid Id { get; set; }
        private Guid? _iID_QTC_Quy_KPK;

        public Guid? IID_QTC_Quy_KPK
        {
            get => _iID_QTC_Quy_KPK;
            set => SetProperty(ref _iID_QTC_Quy_KPK, value);
        }
        private Guid? _iID_MucLucNganSach;

        public Guid? IID_MucLucNganSach
        {
            get => _iID_MucLucNganSach;
            set => SetProperty(ref _iID_MucLucNganSach, value);
        }
        private string _sM;

        public string SM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }
        private string _sTM;

        public string STM
        {
            get => _sTM;
            set => SetProperty(ref _sTM, value);
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

            }
        }
        private double? _fTien_DuToanGiaoNamNay;

        public double? FTien_DuToanGiaoNamNay
        {
            get => _fTien_DuToanGiaoNamNay;
            set
            {
                SetProperty(ref _fTien_DuToanGiaoNamNay, value);
            }
        }
        private double? _fTien_TongDuToanDuocGiao;

        public double? FTien_TongDuToanDuocGiao
        {
            get => _fTien_TongDuToanDuocGiao = FTien_DuToanGiaoNamNay + FTien_DuToanNamTruocChuyenSang;
            set
            {
                SetProperty(ref _fTien_TongDuToanDuocGiao, value);
            }
        }
        private double? _fTienThucChi;

        public double? FTienThucChi
        {
            get => _fTienThucChi = FTienQuyetToanDaDuyet + FTienDeNghiQuyetToanQuyNay;
            set
            {
                SetProperty(ref _fTienThucChi, value);
            }
        }
        private double? _fTienQuyetToanDaDuyet;

        public double? FTienQuyetToanDaDuyet
        {
            get => _fTienQuyetToanDaDuyet;
            set
            {
                SetProperty(ref _fTienQuyetToanDaDuyet, value);
            }
        }
        private double? _fTienDeNghiQuyetToanQuyNay;

        public double? FTienDeNghiQuyetToanQuyNay
        {
            get => _fTienDeNghiQuyetToanQuyNay;
            set
            {
                SetProperty(ref _fTienDeNghiQuyetToanQuyNay, value);
                OnPropertyChanged(nameof(FTienThucChi));
            }
        }
        private double? _fTienXacNhanQuyetToanQuyNay;

        public double? FTienXacNhanQuyetToanQuyNay
        {
            get => _fTienXacNhanQuyetToanQuyNay;
            set
            {
                SetProperty(ref _fTienXacNhanQuyetToanQuyNay, value);
            }
        }
        public string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set
            {
                SetProperty(ref _sGhiChu, value);
            }
        }
        public bool IsAuToFillTuChi { get; set; }
        public string SXauNoiMa { get; set; }
        public string SMoTa { get; set; }
        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }
        public Guid IdParent { get; set; }
        public string IID_MaDonVi { get; set; }
        public int INamLamViec { get; set; }
        public string STenDonVi { get; set; }

        public string STTM { get; set; }
        public bool BHangCha { get; set; }
        public bool IsHasData => FTien_TongDuToanDuocGiao.GetValueOrDefault() != 0
                            || FTienQuyetToanDaDuyet.GetValueOrDefault() != 0
                            || FTienThucChi.GetValueOrDefault() != 0
                            || FTien_DuToanNamTruocChuyenSang.GetValueOrDefault() != 0
                            || FTien_DuToanGiaoNamNay.GetValueOrDefault() != 0
                            || FTienDeNghiQuyetToanQuyNay.GetValueOrDefault() != 0
                            || FTien_TongDuToanDuocGiao.GetValueOrDefault() != 0;
        public string SDuToanChiTietToi { get; set; }
        public string IIdMaDonVi { get; set; }
    }
}
