using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDcDuToanThuModel : ModelBase
    {
        private Guid _iIDDttDieuChinh;
        public Guid IIDDttDieuChinh { get => _iIDDttDieuChinh; set => SetProperty(ref _iIDDttDieuChinh, value); }

        private string _sSoChungTu;
        public string SSoChungTu { get => _sSoChungTu; set => SetProperty(ref _sSoChungTu, value); }

        private DateTime? _dNgayChungTu;
        public DateTime? DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }

        private int? _iNamLamViec;
        public int? INamLamViec { get => _iNamLamViec; set => SetProperty(ref _iNamLamViec, value); }

        private Guid? _iIdDonViId;
        public Guid? IIdDonViId { get => _iIdDonViId; set => SetProperty(ref _iIdDonViId, value); }
        private string _iIDMaDonVi;
        public string IIDMaDonVi { get => _iIDMaDonVi; set => SetProperty(ref _iIDMaDonVi, value); }
        private string _sLNS;
        public string SLNS
        {
            get => _sLNS;
            set => SetProperty(ref _sLNS, value);
        }
        private string _sMoTa;
        public string SMoTa { get => _sMoTa; set => SetProperty(ref _sMoTa, value); }

        private double? _fThuBHXHNLD;
        public double? FThuBHXHNLD
        { 
            get => _fThuBHXHNLD;
            set => SetProperty(ref _fThuBHXHNLD, value); 
        }

        private double? _fThuBHXHNSD;
        public double? FThuBHXHNSD
        {
            get => _fThuBHXHNSD;
            set => SetProperty(ref _fThuBHXHNSD, value);
        }

        private double? _fThuBHYTNLD;
        public double? FThuBHYTNLD
        {
            get => _fThuBHYTNLD;
            set => SetProperty(ref _fThuBHYTNLD, value);
        }

        private double? _fThuBHYTNSD;
        public double? FThuBHYTNSD
        {
            get => _fThuBHYTNSD;
            set => SetProperty(ref _fThuBHYTNSD, value);
        }

        private double? _fThuBHTNNLD;
        public double? FThuBHTNNLD
        {
            get => _fThuBHTNNLD;
            set => SetProperty(ref _fThuBHTNNLD, value);
        }

        private double? _fThuBHTNNSD;
        public double? FThuBHTNNSD
        {
            get => _fThuBHTNNSD;
            set => SetProperty(ref _fThuBHTNNSD, value);
        }

        private double? _fThuBHXHNLDQTDauNam;
        public double? FThuBHXHNLDQTDauNam
        {
            get => _fThuBHXHNLDQTDauNam;
            set => SetProperty(ref _fThuBHXHNLDQTDauNam, value);
        }

        private double? _fThuBHXHNSDQTDauNam;
        public double? FThuBHXHNSDQTDauNam
        {
            get => _fThuBHXHNSDQTDauNam;
            set => SetProperty(ref _fThuBHXHNSDQTDauNam, value);
        }

        private double? _fThuBHYTNLDQTDauNam;
        public double? FThuBHYTNLDQTDauNam
        {
            get => _fThuBHYTNLDQTDauNam;
            set => SetProperty(ref _fThuBHYTNLDQTDauNam, value);
        }

        private double? _fThuBHYTNSDQTDauNam;
        public double? FThuBHYTNSDQTDauNam
        {
            get => _fThuBHYTNSDQTDauNam;
            set => SetProperty(ref _fThuBHYTNSDQTDauNam, value);
        }

        private double? _fThuBHTNNLDQTDauNam;
        public double? FThuBHTNNLDQTDauNam
        {
            get => _fThuBHTNNLDQTDauNam;
            set => SetProperty(ref _fThuBHTNNLDQTDauNam, value);
        }

        private double? _fThuBHTNNSDQTDauNam;
        public double? FThuBHTNNSDQTDauNam
        {
            get => _fThuBHTNNSDQTDauNam;
            set => SetProperty(ref _fThuBHTNNSDQTDauNam, value);
        }

        private double? _fThuBHXHNLDQTCuoiNam;
        public double? FThuBHXHNLDQTCuoiNam
        {
            get => _fThuBHXHNLDQTCuoiNam;
            set => SetProperty(ref _fThuBHXHNLDQTCuoiNam, value);
        }

        private double? _fThuBHXHNSDQTCuoiNam;
        public double? FThuBHXHNSDQTCuoiNam
        {
            get => _fThuBHXHNSDQTCuoiNam;
            set => SetProperty(ref _fThuBHXHNSDQTCuoiNam, value);
        }

        private double? _fThuBHYTNLDQTCuoiNam;
        public double? FThuBHYTNLDQTCuoiNam
        {
            get => _fThuBHYTNLDQTCuoiNam;
            set => SetProperty(ref _fThuBHYTNLDQTCuoiNam, value);
        }

        private double? _fThuBHYTNSDQTCuoiNam;
        public double? FThuBHYTNSDQTCuoiNam
        {
            get => _fThuBHYTNSDQTCuoiNam;
            set => SetProperty(ref _fThuBHYTNSDQTCuoiNam, value);
        }

        private double? _fThuBHTNNLDQTCuoiNam;
        public double? FThuBHTNNLDQTCuoiNam
        {
            get => _fThuBHTNNLDQTCuoiNam;
            set => SetProperty(ref _fThuBHTNNLDQTCuoiNam, value);
        }

        private double? _fThuBHTNNSDQTCuoiNam;
        public double? FThuBHTNNSDQTCuoiNam
        {
            get => _fThuBHTNNSDQTCuoiNam;
            set => SetProperty(ref _fThuBHTNNSDQTCuoiNam, value);
        }

        private double? _fTongThuBHXHNLD;
        public double? FTongThuBHXHNLD
        {
            get => _fTongThuBHXHNLD;
            set => SetProperty(ref _fTongThuBHXHNLD, value);
        }

        private double? _fTongThuBHXHNSD;
        public double? FTongThuBHXHNSD
        {
            get => _fTongThuBHXHNSD;
            set => SetProperty(ref _fTongThuBHXHNSD, value);
        }

        private double? _fTongThuBHYTNLD;
        public double? FTongThuBHYTNLD
        {
            get => _fTongThuBHYTNLD;
            set => SetProperty(ref _fTongThuBHYTNLD, value);
        }

        private double? _fTongThuBHYTNSD;
        public double? FTongThuBHYTNSD
        {
            get => _fTongThuBHYTNSD;
            set => SetProperty(ref _fTongThuBHYTNSD, value);
        }

        private double? _fTongThuBHTNNLD;
        public double? FTongThuBHTNNLD
        {
            get => _fTongThuBHTNNLD;
            set => SetProperty(ref _fTongThuBHTNNLD, value);
        }

        private double? _fTongThuBHTNNSD;
        public double? FTongThuBHTNNSD
        {
            get => _fTongThuBHTNNSD;
            set => SetProperty(ref _fTongThuBHTNNSD, value);
        }

        private double? _fTongCong;
        public double? FTongCong
        {
            get => _fTongCong;
            set => SetProperty(ref _fTongCong, value);
        }

        private double? _fThuBHXHNLDTang;
        public double? FThuBHXHNLDTang
        {
            get => _fThuBHXHNLDTang;
            set => SetProperty(ref _fThuBHXHNLDTang, value);
        }

        private double? _fThuBHXHNSDTang;
        public double? FThuBHXHNSDTang
        {
            get => _fThuBHXHNSDTang;
            set => SetProperty(ref _fThuBHXHNSDTang, value);
        }

        private double? _fThuBHXHTang;
        public double? FThuBHXHTang
        {
            get => _fThuBHXHTang;
            set => SetProperty(ref _fThuBHXHTang, value);
        }

        private double? _fThuBHYTNLDTang;
        public double? FThuBHYTNLDTang
        {
            get => _fThuBHYTNLDTang;
            set => SetProperty(ref _fThuBHYTNLDTang, value);
        }

        private double? _fThuBHYTNSDTang;
        public double? FThuBHYTNSDTang
        {
            get => _fThuBHYTNSDTang;
            set => SetProperty(ref _fThuBHYTNSDTang, value);
        }

        private double? _fThuBHYTTang;
        public double? FThuBHYTTang
        {
            get => _fThuBHYTTang;
            set => SetProperty(ref _fThuBHYTTang, value);
        }

        private double? _fThuBHTNNLDTang;
        public double? FThuBHTNNLDTang
        {
            get => _fThuBHTNNLDTang;
            set => SetProperty(ref _fThuBHTNNLDTang, value);
        }

        private double? _fThuBHTNNSDTang;
        public double? FThuBHTNNSDTang
        {
            get => _fThuBHTNNSDTang;
            set => SetProperty(ref _fThuBHTNNSDTang, value);
        }

        private double? _fThuBHTNTang;
        public double? FThuBHTNTang
        {
            get => _fThuBHTNTang;
            set => SetProperty(ref _fThuBHTNTang, value);
        }

        private double? _fThuBHXHNLDGiam;
        public double? FThuBHXHNLDGiam
        {
            get => _fThuBHXHNLDGiam;
            set => SetProperty(ref _fThuBHXHNLDGiam, value);
        }

        private double? _fThuBHXHNSDGiam;
        public double? FThuBHXHNSDGiam
        {
            get => _fThuBHXHNSDGiam;
            set => SetProperty(ref _fThuBHXHNSDGiam, value);
        }

        private double? _fThuBHXHGiam;
        public double? FThuBHXHGiam
        {
            get => _fThuBHXHGiam;
            set => SetProperty(ref _fThuBHXHGiam, value);
        }

        private double? _fThuBHYTNLDGiam;
        public double? FThuBHYTNLDGiam
        {
            get => _fThuBHYTNLDGiam;
            set => SetProperty(ref _fThuBHYTNLDGiam, value);
        }

        private double? _fThuBHYTNSDGiam;
        public double? FThuBHYTNSDGiam
        {
            get => _fThuBHYTNSDGiam;
            set => SetProperty(ref _fThuBHYTNSDGiam, value);
        }

        private double? _fThuBHYTGiam;
        public double? FThuBHYTGiam
        {
            get => _fThuBHYTGiam;
            set => SetProperty(ref _fThuBHYTGiam, value);
        }

        private double? _fThuBHTNNLDGiam;
        public double? FThuBHTNNLDGiam
        {
            get => _fThuBHTNNLDGiam;
            set => SetProperty(ref _fThuBHTNNLDGiam, value);
        }

        private double? _fThuBHTNNSDGiam;
        public double? FThuBHTNNSDGiam
        {
            get => _fThuBHTNNSDGiam;
            set => SetProperty(ref _fThuBHTNNSDGiam, value);
        }

        private double? _fThuBHTNGiam;
        public double? FThuBHTNGiam
        {
            get => _fThuBHTNGiam;
            set => SetProperty(ref _fThuBHTNGiam, value);
        }

        private string _sTongHop;
        public string STongHop { get => _sTongHop; set => SetProperty(ref _sTongHop, value); }
        private Guid? _iIDTongHopID;
        public Guid? IIDTongHopID { get => _iIDTongHopID; set => SetProperty(ref _iIDTongHopID, value); }
        private int? _iLoaiTongHop;
        public int? ILoaiTongHop { get => _iLoaiTongHop; set => SetProperty(ref _iLoaiTongHop, value); }
        private bool _bIsKhoa;
        public bool BIsKhoa
        {
            get => _bIsKhoa;
            set => SetProperty(ref _bIsKhoa, value);
        }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        private string _sNguoiSua;
        public string SNguoiSua { get => _sNguoiSua; set => SetProperty(ref _sNguoiSua, value); }
        private string _sNguoiTao;
        public string SNguoiTao { get => _sNguoiTao; set => SetProperty(ref _sNguoiTao, value); }

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

        private double? _isTongTien;
        public double? IsTongTien { get => _isTongTien; set => SetProperty(ref _isTongTien, value); }

        public string SoChungTuParent { get; set; }

        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set => SetProperty(ref _isCollapse, value);
        }
        public bool IsChildSummary { get; set; }
        public string SDonVi => IIDMaDonVi + " - " + STenDonVi;
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
        private bool? _bDaTongHop;
        public bool? BDaTongHop
        {
            get => _bDaTongHop.GetValueOrDefault(false);
            set => SetProperty(ref _bDaTongHop, value);
        }
    }
}
