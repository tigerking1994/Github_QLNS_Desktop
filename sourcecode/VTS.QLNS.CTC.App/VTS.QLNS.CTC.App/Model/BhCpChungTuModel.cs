using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhCpChungTuModel : ModelBase
    {
        public Guid Id { get; set; }
        private string _sSoChungTu;
        public string SSoChungTu { get => _sSoChungTu; set => SetProperty(ref _sSoChungTu, value); }
        private DateTime? _dNgayChungTu;
        public DateTime? DNgayChungTu { get => _dNgayChungTu; set => SetProperty(ref _dNgayChungTu, value); }
        private string _sSoQuyetDinh;
        public string SSoQuyetDinh { get => _sSoQuyetDinh; set => SetProperty(ref _sSoQuyetDinh, value); }
        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh { get => _dNgayQuyetDinh; set => SetProperty(ref _dNgayQuyetDinh, value); }
        private int? _iNamChungTu;
        public int? INamChungTu { get => _iNamChungTu; set => SetProperty(ref _iNamChungTu, value); }
        private string _sMoTa;
        public string SMoTa { get => _sMoTa; set => SetProperty(ref _sMoTa, value); }
        private string _sID_MaDonVi;
        public string SID_MaDonVi { get => _sID_MaDonVi; set => SetProperty(ref _sID_MaDonVi, value); }
        private string _sLNS;
        public string SLNS { get => _sLNS; set => SetProperty(ref _sLNS, value); }
        private Guid? _iID_LoaiCap;
        public Guid? IID_LoaiCap { get => _iID_LoaiCap; set => SetProperty(ref _iID_LoaiCap, value); }
        private string _sTenDanhMucLoaiChi;
        public string STenDanhMucLoaiChi { get => _sTenDanhMucLoaiChi; set => SetProperty(ref _sTenDanhMucLoaiChi, value); }
        private double? _fTienDaCap;
        public double? FTienDaCap { get => _fTienDaCap; set => SetProperty(ref _fTienDaCap, value); }
        private double? _fTienKeHoachCap;
        public double? FTienKeHoachCap { get => _fTienKeHoachCap; set => SetProperty(ref _fTienKeHoachCap, value); }
        private double? _fTienDuToan;
        public double? FTienDuToan { get => _fTienDuToan; set => SetProperty(ref _fTienDuToan, value); }
        private DateTime? _dNgaySua;
        public DateTime? DNgaySua { get => _dNgaySua; set => SetProperty(ref _dNgaySua, value); }
        private DateTime? _dNgayTao;
        public DateTime? DNgayTao { get => _dNgayTao; set => SetProperty(ref _dNgayTao, value); }
        private string _sNguoiSua;
        public string SNguoiSua { get => _sNguoiSua; set => SetProperty(ref _sNguoiSua, value); }
        private string _sNguoiTao;
        public string SNguoiTao { get => _sNguoiTao; set => SetProperty(ref _sNguoiTao, value); }
        private bool _bIsKhoa;
        public bool BIsKhoa { get => _bIsKhoa; set => SetProperty(ref _bIsKhoa, value); }
        public Guid IID_TongHop { get; set; }
        public int ILoaiTongHop { get; set; }
        private string _sTongHop;
        public string STongHop { get => _sTongHop; set => SetProperty(ref _sTongHop, value); }
        public int IQuy { get; set; }
        private bool _isSummaryVocher;
        public bool IsSummaryVocher
        {
            get => _isSummaryVocher;
            set => SetProperty(ref _isSummaryVocher, value);
        }

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

        public string SQuy => IQuy switch
        {
            1 => "Quý I",
            2 => "Quý II",
            3 => "Quý III",
            4 => "Quý IV",
            _ => string.Empty
        };

        public string SMaLoaiChi { get; set; }
        private double? _fTyLeThu;
        public double? FTyLeThu
        {
            get => _fTyLeThu;
            set
            {
                SetProperty(ref _fTyLeThu, value);
            }
        }
        private double? _fTongTienConLai;
        public double? FTongTienConLai
        {
            get=> _fTongTienConLai; 
            set=>SetProperty(ref _fTongTienConLai,value);
        }
    }
}
