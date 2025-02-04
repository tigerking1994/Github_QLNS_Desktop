using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDtTmBHYTTNModel : ModelBase
    {
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        private Guid? _iIdDonViId;
        public Guid? IIdDonViId { get => _iIdDonViId; set => SetProperty(ref _iIdDonViId, value); }
        private string _iID_MaDonVi;
        public string IIDMaDonVi { get => _iID_MaDonVi; set => SetProperty(ref _iID_MaDonVi, value); }
        public string SMoTa { get; set; }
        private bool _bIsKhoa;
        public bool BIsKhoa
        {
            get => _bIsKhoa;
            set => SetProperty(ref _bIsKhoa, value);
        }

        private string _sDSLNS;
        public string SDSLNS
        {
            get => _sDSLNS;
            set => SetProperty(ref _sDSLNS, value);
        }


        private double? _fDuToan;
        public double? FDuToan
        {
            get => _fDuToan;
            set => SetProperty(ref _fDuToan, value);
        }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? ILoaiDuToan { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }

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

        public string SDonVi => IIDMaDonVi + " - " + STenDonVi;

        public Double? FSoPhanBo { get; set; }
        public Double? FDaPhanBo { get; set; }
        public Double? FSoChuaPhanBo { get; set; }
        public bool IsAdjusted { get; set; }

        private bool _isSummaryVocher;
        public bool IsSummaryVocher
        {
            get => _isSummaryVocher;
            set => SetProperty(ref _isSummaryVocher, value);
        }
        public string SLoaiDuToan
        {
            get
            {
                if (ILoaiDuToan.Equals((int)EstimateTypeNum.YEAR))
                {
                    return EstimateType.EstimateTypeName[EstimateTypeNum.YEAR];
                }
                else if (ILoaiDuToan.Equals((int)EstimateTypeNum.ADDITIONAL))
                {
                    return EstimateType.EstimateTypeName[EstimateTypeNum.ADDITIONAL];
                }
                else
                {
                    return EstimateType.EstimateTypeName[EstimateTypeNum.ADJUSTED];
                }
            }
        }
    }
}
