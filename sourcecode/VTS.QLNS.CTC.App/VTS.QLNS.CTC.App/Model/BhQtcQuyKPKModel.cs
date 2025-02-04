using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQtcQuyKPKModel : ModelBase
    {
        public Guid Id { get; set; }
        private Guid? _iID_DonVi;

        public Guid? IID_DonVi
        {
            get => _iID_DonVi;
            set => SetProperty(ref _iID_DonVi, value);
        }
        private string _iID_MaDonVi;

        public string IID_MaDonVi
        {
            get => _iID_MaDonVi;
            set => SetProperty(ref _iID_MaDonVi, value);
        }

        public Guid IID_LoaiChi { get; set; }
        private string _sSoChungTu;

        public string SSoChungTu
        {
            get => _sSoChungTu;
            set => SetProperty(ref _sSoChungTu, value);
        }
        private DateTime? _dNgayChungTu;

        public DateTime? DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }
        private string _sSoQuyetDinh;

        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }
        private DateTime? _dNgayQuyetDinh;

        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }
        public int IQuyChungTu { get; set; }
        public int INamChungTu { get; set; }
        private string _sMoTa;

        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
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
        public Guid? IID_TongHopID { get; set; }
        public int ILoaiTongHop { get; set; }
        private string _sTongHop;

        public string STongHop
        {
            get => _sTongHop;
            set => SetProperty(ref _sTongHop, value);
        }
        public bool BIsKhoa { get; set; }
        private double? _fTongTien_DuToanNamTruocChuyenSang;

        public double? FTongTien_DuToanNamTruocChuyenSang
        {
            get => _fTongTien_DuToanNamTruocChuyenSang;
            set => SetProperty(ref _fTongTien_DuToanNamTruocChuyenSang, value);
        }
        private double? _fTongTien_DuToanGiaoNamNay;

        public double? FTongTien_DuToanGiaoNamNay
        {
            get => _fTongTien_DuToanGiaoNamNay;
            set => SetProperty(ref _fTongTien_DuToanGiaoNamNay, value);
        }
        private double? _fTongTien_TongDuToanDuocGiao;

        public double? FTongTien_TongDuToanDuocGiao
        {
            get => _fTongTien_TongDuToanDuocGiao;
            set => SetProperty(ref _fTongTien_TongDuToanDuocGiao, value);
        }
        private double? _fTongTienThucChi;
        public double? FTongTienThucChi
        {
            get => _fTongTienThucChi;
            set => SetProperty(ref _fTongTienThucChi, value);
        }
        private double? _fTongTienQuyetToanDaDuyet;

        public double? FTongTienQuyetToanDaDuyet
        {
            get => _fTongTienQuyetToanDaDuyet;
            set => SetProperty(ref _fTongTienQuyetToanDaDuyet, value);
        }
        private double? _fTongTienDeNghiQuyetToanQuyNay;

        public double? FTongTienDeNghiQuyetToanQuyNay
        {
            get => _fTongTienDeNghiQuyetToanQuyNay;
            set => SetProperty(ref _fTongTienDeNghiQuyetToanQuyNay, value);
        }
        private double? _fTongTienXacNhanQuyetToanQuyNay;

        public double? FTongTienXacNhanQuyetToanQuyNay
        {
            get => _fTongTienXacNhanQuyetToanQuyNay;
            set => SetProperty(ref _fTongTienXacNhanQuyetToanQuyNay, value);
        }
        public string SDSLNS { get; set; }

        // Another properties
        public string STenDanhMucLoaiChi { get; set; }
        private string _sTenDonVi;
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }
        private bool _isExpand;
        public bool IsExpand
        {
            get => _isExpand;
            set => SetProperty(ref _isExpand, value);
        }

        public string SoChungTuParent { get; set; }

        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set => SetProperty(ref _isCollapse, value);
        }
        public bool IsChildSummary { get; set; }

        public string FullNameDonVi => IID_MaDonVi + " - " + STenDonVi;
        public string SQuyChungTu => IQuyChungTu switch
        {
            1 => "Quý I",
            2 => "Quý II",
            3 => "Quý III",
            4 => "Quý IV",
            _ => string.Empty
        };

        public bool IsHasData => FTongTienDeNghiQuyetToanQuyNay.GetValueOrDefault() != 0
            || FTongTienXacNhanQuyetToanQuyNay.GetValueOrDefault() != 0
            || FTongTienQuyetToanDaDuyet.GetValueOrDefault() != 0
            || FTongTien_DuToanNamTruocChuyenSang.GetValueOrDefault() != 0
            || FTongTien_DuToanGiaoNamNay.GetValueOrDefault() != 0
            || FTongTienThucChi.GetValueOrDefault() != 0
            || FTongTien_TongDuToanDuocGiao.GetValueOrDefault() != 0;

        private bool _isFilter;
        public bool IsFilter
        {
            get => _isFilter;
            set => SetProperty(ref _isFilter, value);
        }
    }
}
