using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class SettlementVoucherModel : BindableBase
    {
        private bool _isFilter;
        public bool IsFilter
        {
            get => _isFilter;
            set => SetProperty(ref _isFilter, value);
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _sSoChungTu;
        public string SSoChungTu
        {
            get => _sSoChungTu;
            set => SetProperty(ref _sSoChungTu, value);
        }

        private int _iVoucherNoIndex;
        public int ISoChungTuIndex
        {
            get => _iVoucherNoIndex;
            set => SetProperty(ref _iVoucherNoIndex, value);
        }

        private DateTime _dNgayChungTu;
        public DateTime DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }

        private string _sDslns;
        public string SDslns
        {
            get => _sDslns;
            set => SetProperty(ref _sDslns, value);
        }

        private int _iThangQuyLoai;
        public int IThangQuyLoai
        {
            get => _iThangQuyLoai;
            set => SetProperty(ref _iThangQuyLoai, value);
        }

        private int _iThangQuy;
        public int IThangQuy
        {
            get => _iThangQuy;
            set => SetProperty(ref _iThangQuy, value);
        }

        private string _sThangQuyMoTa;
        public string SThangQuyMoTa
        {
            get => _sThangQuyMoTa;
            set => SetProperty(ref _sThangQuyMoTa, value);
        }

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private string _iIdMaDonVi;
        public string IIdMaDonVi
        {
            get => _iIdMaDonVi;
            set => SetProperty(ref _iIdMaDonVi, value);
        }

        private string _sTenDonVi;
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        public string STenDonViDisplay => (ILoaiChungTu == 2) ? $"{STenDonVi} (Điều chỉnh)" : STenDonVi;

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }

        private double _fTongTuChiDeNghi;
        public double FTongTuChiDeNghi
        {
            get => _fTongTuChiDeNghi;
            set => SetProperty(ref _fTongTuChiDeNghi, value);
        }

        private double _fTongTuChiPheDuyet;
        public double FTongTuChiPheDuyet
        {
            get => _fTongTuChiPheDuyet;
            set => SetProperty(ref _fTongTuChiPheDuyet, value);
        }

        private int _iNamLamViec;
        public int INamLamViec
        {
            get => _iNamLamViec;
            set => SetProperty(ref _iNamLamViec, value);
        }

        private int _iNamNganSach;
        public int INamNganSach
        {
            get => _iNamNganSach;
            set => SetProperty(ref _iNamNganSach, value);
        }

        private int _iIdMaNguonNganSach;
        public int IIdMaNguonNganSach
        {
            get => _iIdMaNguonNganSach;
            set => SetProperty(ref _iIdMaNguonNganSach, value);
        }

        private string _sTongHop;
        public string STongHop
        {
            get => _sTongHop;
            set => SetProperty(ref _sTongHop, value);
        }

        public string STrangThai => string.IsNullOrEmpty(STongHop) ? string.Empty : "Tổng hợp";

        private string _sNguoiTao;
        public string SNguoiTao
        {
            get => _sNguoiTao;
            set => SetProperty(ref _sNguoiTao, value);
        }

        private DateTime? _dNgayTao;
        public DateTime? DNgayTao
        {
            get => _dNgayTao;
            set => SetProperty(ref _dNgayTao, value);
        }


        private int _iLoaiChungTu;
        public int ILoaiChungTu
        {
            get => _iLoaiChungTu;
            set => SetProperty(ref _iLoaiChungTu, value);
        }

        private int _iLanDieuChinh;
        public int ILanDieuChinh
        {
            get => _iLanDieuChinh;
            set => SetProperty(ref _iLanDieuChinh, value);
        }

        private string _sNguoiSua;
        public string SNguoiSua
        {
            get => _sNguoiSua;
            set => SetProperty(ref _sNguoiSua, value);
        }

        private DateTime? _dNgaySua;
        public DateTime? DNgaySua
        {
            get => _dNgaySua;
            set => SetProperty(ref _dNgaySua, value);
        }

        private string _sLoai;
        public string SLoai
        {
            get => _sLoai;
            set => SetProperty(ref _sLoai, value);
        }

        public bool IsChildSummary { get; set; }

        private bool? _bDaTongHop;
        public bool? BDaTongHop
        {
            get => _bDaTongHop.GetValueOrDefault(false);
            set => SetProperty(ref _bDaTongHop, value);
        }

        public string BDaTongHopString => BDaTongHop.GetValueOrDefault(false) ? "Đã tổng hợp" : "";

        private bool _isExpand;
        public bool IsExpand
        {
            get => _isExpand;
            set => SetProperty(ref _isExpand, value);
        }

        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set => SetProperty(ref _isCollapse, value);
        }

        public string SoChungTuParent { get; set; }
        public bool IsSent { get; set; }
        public string TypeIcon { get; set; }
    }
}
