using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQtcNamKinhPhiKhacModel : ModelBase
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
        private bool? _bThucChiTheo4Quy;
        public bool? BThucChiTheo4Quy
        {
            get => _bThucChiTheo4Quy;
            set => SetProperty(ref _bThucChiTheo4Quy, value);
        }
        private int? _iNamLamViec;

        public int? INamLamViec
        {
            get => _iNamLamViec;
            set => SetProperty(ref _iNamLamViec, value);
        }
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
        public bool BDaTongHop { get; set; }
        public string SDSLNS { get; set; }
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
        private double? _fTongTien_ThucChi;

        public double? FTongTien_ThucChi
        {
            get => _fTongTien_ThucChi;
            set => SetProperty(ref _fTongTien_ThucChi, value);
        }
        private double? _fTongTienThua;

        public double? FTongTienThua
        {
            get => _fTongTienThua;
            set => SetProperty(ref _fTongTienThua, value);
        }
        private double? _fTongTienThieu;

        public double? FTongTienThieu
        {
            get => _fTongTienThieu;
            set => SetProperty(ref _fTongTienThieu, value);
        }
        private double? _fTiLeThucHienTrenDuToan;

        public double? FTiLeThucHienTrenDuToan
        {
            get => _fTiLeThucHienTrenDuToan;
            set => SetProperty(ref _fTiLeThucHienTrenDuToan, value);
        }
        public Guid IID_LoaiChi { get; set; }
        public string STenDanhMucLoaiChi { get; set; }
        // Another properties
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
    }
}
