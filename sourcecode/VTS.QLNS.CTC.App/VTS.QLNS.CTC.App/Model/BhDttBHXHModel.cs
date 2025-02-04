using System;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDttBHXHModel : ModelBase
    {
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string IIDMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SMoTa { get; set; }
        private bool _bIsKhoa;
        public bool BIsKhoa
        {
            get => _bIsKhoa;
            set => SetProperty(ref _bIsKhoa, value);
        }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? ILoaiDuToan { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public string STongHop { get; set; }
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
        public bool IsChildSumary { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
        private double? _fThuBHXHNLDDong;
        public double? FThuBHXHNLDDong
        {
            get => _fThuBHXHNLDDong;
            set
            {
                SetProperty(ref _fThuBHXHNLDDong, value);
            }
        }
        private double? _fThuBHXHNSDDong;
        public double? FThuBHXHNSDDong
        {
            get => _fThuBHXHNSDDong;
            set
            {
                SetProperty(ref _fThuBHXHNSDDong, value);
            }
        }
        private double? _fThuBHXH;
        public double? FThuBHXH
        {
            get => _fThuBHXH;
            set
            {
                SetProperty(ref _fThuBHXH, value);
            }
        }
        private double? _fThuBHYTNLDDong;
        public double? FThuBHYTNLDDong
        {
            get => _fThuBHYTNLDDong;
            set
            {
                SetProperty(ref _fThuBHYTNLDDong, value);
            }
        }
        private double? _fThuBHYTNSDDong;
        public double? FThuBHYTNSDDong
        {
            get => _fThuBHYTNSDDong;
            set
            {
                SetProperty(ref _fThuBHYTNSDDong, value);
            }
        }
        private double? _fTongBHYT;
        public double? FTongBHYT
        {
            get => _fTongBHYT;
            set
            {
                SetProperty(ref _fTongBHYT, value);
            }
        }
        private double? _fThuBHTNNLDDong;
        public double? FThuBHTNNLDDong
        {
            get => _fThuBHTNNLDDong;
            set
            {
                SetProperty(ref _fThuBHTNNLDDong, value);
            }
        }
        private double? _fThuBHTNNSDDong;
        public double? FThuBHTNNSDDong
        {
            get => _fThuBHTNNSDDong;
            set
            {
                SetProperty(ref _fThuBHTNNSDDong, value);
            }
        }
        private double? _fThuBHTN;
        public double? FThuBHTN
        {
            get => _fThuBHTN;
            set
            {
                SetProperty(ref _fThuBHTN, value);
            }
        }
        private double? _fDuToan;
        public double? FDuToan
        {
            get => _fDuToan;
            set
            {
                SetProperty(ref _fDuToan, value);
            }
        }
        public string SDslns { get; set; }

        public string SLoaiDuToan
        {
            get
            {
                if (ILoaiDuToan.HasValue)
                {
                    EstimateTypeNum EtmType;
                    if (Enum.TryParse(ILoaiDuToan.Value.ToString(), out EtmType))
                    {
                        return EstimateType.EstimateTypeName[EtmType];
                    }
                }
                return string.Empty;
            }
        }

        public Double? FSoPhanBo { get; set; }
        public Double? FDaPhanBo { get; set; }
        public Double? FSoChuaPhanBo { get; set; }
    }
}
