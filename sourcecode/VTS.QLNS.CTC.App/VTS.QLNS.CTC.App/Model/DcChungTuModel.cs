using System;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class DcChungTuModel : BindableBase
    {
        public Guid Id { get; set; }
        private string _sSoChungTu;
        public string SSoChungTu
        {
            get => _sSoChungTu;
            set => SetProperty(ref _sSoChungTu, value);
        }
        private int? _iSoChungTuIndex;
        public int? ISoChungTuIndex
        {
            get => _iSoChungTuIndex;
            set => SetProperty(ref _iSoChungTuIndex, value);
        }

        private DateTime? _dNgayChungTu;
        public DateTime? DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }
        public string SMoTa { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SDslns { get; set; }
        public int ILoaiChungTu { get; set; }
        public int ILoaiDuKien { get; set; }
        public string SLoaiDuKien
        {
            get
            {
                EstimateSettlementType estimateSettlementType;
                if (Enum.TryParse(ILoaiDuKien.ToString(), out estimateSettlementType))
                {
                    return VoucherType.EstimateSettlementTypeName[estimateSettlementType];
                }
                return string.Empty;
            }
        }
        public int? INamNganSach { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public int? INamLamViec { get; set; }

        public double _fDieuChinh;
        public double FDieuChinh
        {
            get => _fDieuChinh;
            set => SetProperty(ref _fDieuChinh, value);
        }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }
        public string SGhiChu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private string _sTongHop;
        public string STongHop
        {
            get => _sTongHop;
            set => SetProperty(ref _sTongHop, value);
        }

        public string STrangThai => string.IsNullOrEmpty(STongHop) ? string.Empty : "Tổng hợp";

        public bool IsChildSummary { get; set; }

        private bool? _bDaTongHop;
        public bool? BDaTongHop
        {
            get => _bDaTongHop.GetValueOrDefault(false);
            set => SetProperty(ref _bDaTongHop, value);
        }

        private bool? _bTatCaDonVi;
        public bool? BTatCaDonVi
        {
            get => _bTatCaDonVi.GetValueOrDefault(false);
            set => SetProperty(ref _bTatCaDonVi, value);
        }

        public string BDaTongHopString => BDaTongHop.GetValueOrDefault(false) ? "Đã tổng hợp" : "Chưa tổng hợp";

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

        public bool IsHangCha { get; set; }
        public bool IsDeleted { get; set; }
        public bool BHangCha { get; set; }

        public string STrangThaiDieuChinh { get; set; }
    }
}
