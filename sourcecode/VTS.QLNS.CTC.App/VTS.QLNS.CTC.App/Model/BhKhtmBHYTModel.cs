using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhKhtmBHYTModel : BindableBase
    {
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string IIDMaDonVi { get; set; }
        public Guid? IIDDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SMoTa { get; set; }
        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }
        private bool? _bDaTongHop;
        public bool? BDaTongHop
        {
            get => _bDaTongHop.GetValueOrDefault(false);
            set => SetProperty(ref _bDaTongHop, value);
        }
        public double? FTongKeHoach { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DChungTu { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public string STongHop { get; set; }
        public Guid? IIDTongHopID { get; set; }
        public int? ILoaiTongHop { get; set; }
        public bool IsChildSummary { get; set; }
        public string SoChungTuParent { get; set; }

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
        public int Index { get; set; }
        public int? ILoaiChungTu { get; set; }
        public string SDssoChungTuTongHop { get; set; }
        public bool IsChildSumary { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
        private int? _iTongSoNguoi;
        public int? ITongSoNguoi
        {
            get => _iTongSoNguoi;
            set
            {
                SetProperty(ref _iTongSoNguoi, value);
            }
        }
        private int? _iTongSoThang;
        public int? ITongSoThang
        {
            get => _iTongSoThang;
            set
            {
                SetProperty(ref _iTongSoThang, value);
            }
        }
        private double? _fTongDinhMuc;
        public double? FTongDinhMuc
        {
            get => _fTongDinhMuc;
            set
            {
                SetProperty(ref _fTongDinhMuc, value);
            }
        }
        private double? _fTongThanhTien;
        public double? FTongThanhTien
        {
            get => _fTongThanhTien;
            set
            {
                SetProperty(ref _fTongThanhTien, value);
            }
        }
        private bool _isSummaryVocher;
        public bool IsSummaryVocher
        {
            get => _isSummaryVocher;
            set => SetProperty(ref _isSummaryVocher, value);
        }
    }
}
