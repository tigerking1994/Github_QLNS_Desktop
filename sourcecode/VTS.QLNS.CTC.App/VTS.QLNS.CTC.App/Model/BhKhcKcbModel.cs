using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhKhcKcbModel: ModelBase
    {
        private Guid _iID_BH_KHC_KCB;
        public Guid IID_BH_KHC_KCB { get => _iID_BH_KHC_KCB; set => SetProperty(ref _iID_BH_KHC_KCB, value); }
        private string _sSoChungTu;
        public string SSoChungTu { get => _sSoChungTu; set => SetProperty(ref _sSoChungTu, value); }
        public DateTime? DNgayChungTu { get; set; }
        private string _sSoQuyetDinh;
        public string SSoQuyetDinh { get => _sSoQuyetDinh; set => SetProperty(ref _sSoQuyetDinh, value); }
        public DateTime? DNgayQuyetDinh { get; set; }
        private int? _iNamLamViec;
        public int? INamLamViec { get => _iNamLamViec; set => SetProperty(ref _iNamLamViec, value); }
        private Guid? _IIdDonViId;
        public Guid? IIdDonViId { get => _IIdDonViId; set => SetProperty(ref _IIdDonViId, value); }
        private string _iID_MaDonVi;
        public string IID_MaDonVi { get => _iID_MaDonVi; set => SetProperty(ref _iID_MaDonVi, value); }
        private string _sMoTa;
        public string SMoTa { get => _sMoTa; set => SetProperty(ref _sMoTa, value); }

        private double? _fTongTienDaThucHienNamTruoc;
        public double? FTongTienDaThucHienNamTruoc { get => _fTongTienDaThucHienNamTruoc; set => SetProperty(ref _fTongTienDaThucHienNamTruoc, value); }
        private double? _fTongTienUocThucHienNamTruoc;
        public double? FTongTienUocThucHienNamTruoc { get => _fTongTienUocThucHienNamTruoc; set => SetProperty(ref _fTongTienUocThucHienNamTruoc, value); }
        private double? _fTongTienKeHoachThucHienNamNay;
        public double? FTongTienKeHoachThucHienNamNay { get => _fTongTienKeHoachThucHienNamNay; set => SetProperty(ref _fTongTienKeHoachThucHienNamNay, value); }

        private string _sTongHop;
        public string STongHop { get => _sTongHop; set => SetProperty(ref _sTongHop, value); }
        private Guid? _iID_TongHopID;
        public Guid? IID_TongHopID { get => _iID_TongHopID; set => SetProperty(ref _iID_TongHopID, value); }
        private int? _iLoaiTongHop;
        public int? ILoaiTongHop { get => _iLoaiTongHop; set => SetProperty(ref _iLoaiTongHop, value); }
        private bool _bIsKhoa;
        public bool BIsKhoa
        {
            get => _bIsKhoa;
            set => SetProperty(ref _bIsKhoa, value);
        }
        public bool BDaTongHop { get;set; }
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

        public string SDonVi => IID_MaDonVi + " - " + STenDonVi;

        private double? _fTyLeThu;
        public double? FTyLeThu { get => _fTyLeThu; set => SetProperty(ref _fTyLeThu, value); }

    }
}
