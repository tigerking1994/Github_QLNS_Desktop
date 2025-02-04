using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQttmBHYTModel : BindableBase
    {
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string IIDMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SMoTa { get; set; }
        private bool _bIsKhoa;
        public bool BIsKhoa
        {
            get => _bIsKhoa;
            set => SetProperty(ref _bIsKhoa, value);
        }
        public int IQuyNam { get; set; }
        public int IQuyNamLoai { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SQuyNamMoTa { get; set; }
        public string SNguoiSua { get; set; }
        public string STongHop { get; set; }
        public string sDSMLNS { get; set; }
        public int? ILoaiTongHop { get; set; }
        private bool? _bDaTongHop;
        public bool? BDaTongHop
        {
            get => _bDaTongHop.GetValueOrDefault(false);
            set => SetProperty(ref _bDaTongHop, value);
        }
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
        public bool IsChildSumary { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
        private bool _isSummaryVocher;
        public bool IsSummaryVocher
        {
            get => _isSummaryVocher;
            set => SetProperty(ref _isSummaryVocher, value);
        }
        public bool IsChildSummary { get; set; }
        public string SoChungTuParent { get; set; }
        
        private double? _fDuToan;
        public double? FDuToan
        {
            get => _fDuToan;
            set
            {
                SetProperty(ref _fDuToan, value);
            }
        }
        private double? _fDaQuyetToan;
        public double? FDaQuyetToan
        {
            get => _fDaQuyetToan;
            set
            {
                SetProperty(ref _fDaQuyetToan, value);
            }
        }
        private double? _fConLai;
        public double? FConLai
        {
            get => _fConLai;
            set
            {
                SetProperty(ref _fConLai, value);
            }
        }
        private double? _fSoPhaiThu;
        public double? FSoPhaiThu
        {
            get => _fSoPhaiThu;
            set
            {
                SetProperty(ref _fSoPhaiThu, value);
            }
        }
        private bool _isFilter;
        public bool IsFilter
        {
            get => _isFilter;
            set => SetProperty(ref _isFilter, value);
        }
    }
}
