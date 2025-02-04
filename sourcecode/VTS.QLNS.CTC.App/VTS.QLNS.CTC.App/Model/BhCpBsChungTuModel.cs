using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhCpBsChungTuModel : ModelBase
    {
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public string IIDMaDonVi { get; set; }
        public int? IQuy { get; set; }
        public string STenDonVi { get; set; }
        public string SDslns { get; set; }
        public string SCoSoYTe { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }
        private bool _isSummaryVocher;
        public bool IsSummaryVocher
        {
            get => _isSummaryVocher;
            set => SetProperty(ref _isSummaryVocher, value);
        }
        public string IdDonViTao { get; set; }
        public string TenLoai { get; set; }

        private double? _fTongDaQuyetToan;
        public double? FTongDaQuyetToan
        {
            get => _fTongDaQuyetToan;
            set => SetProperty(ref _fTongDaQuyetToan, value);
        }
        private double? _fTongDaCapUng;
        public double? FTongDaCapUng
        {
            get => _fTongDaCapUng;
            set => SetProperty(ref _fTongDaCapUng, value);
        }
        private double? _fTongThuaThieu;
        public double? FTongThuaThieu
        {
            get => _fTongThuaThieu;
            set => SetProperty(ref _fTongThuaThieu, value);
        }
        private double? _fTongThua;
        public double? FTongThua
        {
            get => _fTongThua;
            set => SetProperty(ref _fTongThua, value);
        }
        private double? _fTongThieu;
        public double? FTongThieu
        {
            get => _fTongThieu;
            set => SetProperty(ref _fTongThieu, value);
        }
        private double? _fTongSoCapBoSung;
        public double? FTongSoCapBoSung
        {
            get => _fTongSoCapBoSung;
            set => SetProperty(ref _fTongSoCapBoSung, value);
        }
        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
        public string SDSSoChungTuTongHop { get; set; }
        public bool IsChildSumary { get; set; }

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

        private bool? _bDaTongHop;
        public bool? BDaTongHop
        {
            get => _bDaTongHop.GetValueOrDefault(false);
            set => SetProperty(ref _bDaTongHop, value);
        }

        private int? _iLoaiKinhPhi;
        public int? ILoaiKinhPhi
        {
            get => _iLoaiKinhPhi;
            set => SetProperty(ref _iLoaiKinhPhi, value);
        }

        public string BDaTongHopString => BDaTongHop.GetValueOrDefault(false) ? "Đã tổng hợp" : "";
        public bool IsChildSummary { get; set; }
        public int? ILoaiTongHop { get; set; }
        public string DisplayQuarter
        {
            get
            {
                if (IQuy.HasValue)
                {
                    QuarterEnum Quarter;
                    if (Enum.TryParse(IQuy.Value.ToString(), out Quarter))
                    {
                        return Quarters.QuarterName[Quarter];
                    }
                }
                return string.Empty;
            }
        }

        public string STenLoaiKinhPhi
        {
            get
            {
                if (!string.IsNullOrEmpty(ILoaiKinhPhi.ToString()))
                {
                    if (ILoaiKinhPhi == 2)
                    {
                        return CapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD;
                    }
                    else
                    {
                        return CapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN;
                    }
                }
                return string.Empty;
            }
        }
    }
}
