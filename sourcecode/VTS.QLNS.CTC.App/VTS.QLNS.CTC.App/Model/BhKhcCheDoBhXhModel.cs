using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhKhcCheDoBhXhModel : ModelBase
    {
        private Guid _iID_BH_KHC_CheDoBHXH;
        public Guid IID_BH_KHC_CheDoBHXH { get=> _iID_BH_KHC_CheDoBHXH; set=>SetProperty(ref _iID_BH_KHC_CheDoBHXH, value); }
        private string _sSoChungTu;
        public string SSoChungTu { get=>_sSoChungTu; set => SetProperty(ref _sSoChungTu, value); }
        public DateTime? DNgayChungTu { get; set; }
        private string _sSoQuyetDinh;
        public string SSoQuyetDinh { get => _sSoQuyetDinh; set => SetProperty(ref _sSoQuyetDinh, value); }
        public DateTime? DNgayQuyetDinh { get; set; }
        private int? _iNamLamViec;
        public int? INamLamViec { get => _iNamLamViec; set => SetProperty(ref _iNamLamViec, value); }
        private Guid? _IIdDonViId;
        public Guid? IIdDonViId { get => _IIdDonViId; set => SetProperty(ref _IIdDonViId, value); }
        private string _iID_MaDonVi;
        public string IID_MaDonVi { get=> _iID_MaDonVi; set => SetProperty(ref _iID_MaDonVi, value); }
        private string _sMoTa;
        public string SMoTa { get => _sMoTa;set=>SetProperty(ref _sMoTa, value); }
        private int? _iTongSoDaThucHienNamTruoc;
        public int? ITongSoDaThucHienNamTruoc { get=> _iTongSoDaThucHienNamTruoc; set=>SetProperty(ref _iTongSoDaThucHienNamTruoc,value); }
        private int? _iTongSoUocThucHienNamTruoc;
        public int? ITongSoUocThucHienNamTruoc { get => _iTongSoUocThucHienNamTruoc; set => SetProperty(ref _iTongSoUocThucHienNamTruoc, value); }
        private int? _iTongSoKeHoachThucHienNamNay;
        public int? ITongSoKeHoachThucHienNamNay { get => _iTongSoKeHoachThucHienNamNay; set => SetProperty(ref _iTongSoKeHoachThucHienNamNay, value); }
        private int? _iTongSoSQ;
        public int? ITongSoSQ { get => _iTongSoSQ; set => SetProperty(ref _iTongSoSQ, value); }
        private int? _iTongSoQNCN;
        public int? ITongSoQNCN { get => _iTongSoQNCN; set => SetProperty(ref _iTongSoQNCN, value); }

        private int? _iTongSoCNVQP;
        public int? ITongSoCNVQP { get => _iTongSoCNVQP; set => SetProperty(ref _iTongSoCNVQP, value); }
        private int? _iTongSoLDHD;
        public int? ITongSoLDHD { get => _iTongSoLDHD; set => SetProperty(ref _iTongSoLDHD, value); }
        private int? _iTongSoHSQBS;
        public int? ITongSoHSQBS { get => _iTongSoHSQBS; set => SetProperty(ref _iTongSoHSQBS, value); }
        private double? _fTongTienDaThucHienNamTruoc;
        public double? FTongTienDaThucHienNamTruoc { get => _fTongTienDaThucHienNamTruoc; set => SetProperty(ref _fTongTienDaThucHienNamTruoc, value); }
        private double? _fTongTienUocThucHienNamTruoc;
        public double? FTongTienUocThucHienNamTruoc { get => _fTongTienUocThucHienNamTruoc; set => SetProperty(ref _fTongTienUocThucHienNamTruoc, value); }
        private double? _fTongTienKeHoachThucHienNamNay;
        public double? FTongTienKeHoachThucHienNamNay { get => _fTongTienKeHoachThucHienNamNay; set => SetProperty(ref _fTongTienKeHoachThucHienNamNay, value); }
        private double? _fTongTienSQ;
        public double? FTongTienSQ { get => _fTongTienSQ; set => SetProperty(ref _fTongTienSQ, value); }
        private double? _fTongTienCNVQP;
        public double? FTongTienCNVQP { get=> _fTongTienCNVQP; set=>SetProperty(ref _fTongTienCNVQP, value); }
        private double? _fTongTienLDHD;
        public double? FTongTienLDHD { get=> _fTongTienLDHD; set=>SetProperty( ref _fTongTienLDHD,value); }
        private double? _fTongTienHSQBS;
        public double? FTongTienHSQBS { get=> _fTongTienHSQBS; set=>SetProperty( ref _fTongTienHSQBS,value); }
        private double? _fTongTienQNCN;
        public double? FTongTienQNCN { get => _fTongTienQNCN;set=>SetProperty(ref _fTongTienQNCN, value); }
        private string _sTongHop;
        public string STongHop { get=> _sTongHop; set=>SetProperty(ref _sTongHop,value); }
        private Guid? _iID_TongHopID;
        public Guid? IID_TongHopID { get => _iID_TongHopID; set => SetProperty(ref _iID_TongHopID, value); }
        private int? _iLoaiTongHop;
        public int? ILoaiTongHop { get=> _iLoaiTongHop; set=>SetProperty(ref _iLoaiTongHop,value); }
        private bool _bIsKhoa;
        public bool BIsKhoa
        {
            get => _bIsKhoa;
            set => SetProperty(ref _bIsKhoa, value);
        }
        public bool BDaTongHop { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        private string _sNguoiSua;
        public string SNguoiSua { get=> _sNguoiSua; set=>SetProperty(ref _sNguoiSua,value); }
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
        public double? IsTongTien { get => _isTongTien; set => SetProperty(ref _isTongTien, value);}

        public string SoChungTuParent { get; set; }

        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set => SetProperty(ref _isCollapse, value);
        }
        public bool IsChildSummary { get; set; }
    }
}
