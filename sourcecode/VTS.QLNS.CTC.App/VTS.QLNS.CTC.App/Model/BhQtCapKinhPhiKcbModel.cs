using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQtCapKinhPhiKcbModel : ModelBase
    {
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public int? IQuy { get; set; }
        public string SQuyNamMoTa { get; set; }
        public int? ILoaiKinhPhi { get; set; }
        public string SCoSoYTe { get; set; }
        public string SDslns { get; set; }
        public string SMoTa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }
        private double? _fKeHoachCap;
        public double? FKeHoachCap
        {
            get => _fKeHoachCap;
            set => SetProperty(ref _fKeHoachCap, value);
        }
        private double? _fDaQuyetToan;
        public double? FDaQuyetToan
        {
            get => _fDaQuyetToan;
            set => SetProperty(ref _fDaQuyetToan, value);
        }
        private double? _fConLai;
        public double? FConLai
        {
            get => _fConLai;
            set => SetProperty(ref _fConLai, value);
        }
        private double? _fQuyetToanQuyNay;
        public double? FQuyetToanQuyNay
        {
            get => _fQuyetToanQuyNay;
            set => SetProperty(ref _fQuyetToanQuyNay, value);
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
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
