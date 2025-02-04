using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhCptuBHYTModel : ModelBase
    {
        public Guid Id { get; set; }

        public string SSoChungTu { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public string IIDMaDonVi { get; set; }
        public DateTime DNgayQuyetDinh { get; set; }
        public int IQuy { get; set; }
        public int ILoaiKinhPhi { get; set; }
        public string SMoTa { get; set; }
        public string SDSID_CoSoYTe { get; set; }
        public string SDSLNS { get; set; }
        public bool BIsKhoa { get; set; }
        public bool BIsTongHop { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        private Double? _fQTQuyTruoc;
        public Double? FQTQuyTruoc { get => _fQTQuyTruoc; set { SetProperty(ref _fQTQuyTruoc, value); } }
        private Double? _fTamUngQuyNay;
        public Double? FTamUngQuyNay { get => _fTamUngQuyNay; set { SetProperty(ref _fTamUngQuyNay, value); } }

        private Double? _fLuyKeCapDenCuoiQuy;
        public Double? FLuyKeCapDenCuoiQuy { get => _fLuyKeCapDenCuoiQuy; set { SetProperty(ref _fLuyKeCapDenCuoiQuy, value); } }

        private Double? _fPhaiCapTamUngQuyNay;
        public Double? FPhaiCapTamUngQuyNay { get => _fPhaiCapTamUngQuyNay; set { SetProperty(ref _fPhaiCapTamUngQuyNay, value); } }

        private Double? _fCapThuaQuyTruocChuyenSang;
        public Double? FCapThuaQuyTruocChuyenSang { get => _fCapThuaQuyTruocChuyenSang; set { SetProperty(ref _fCapThuaQuyTruocChuyenSang, value); } }
        public int INamLamViec { get; set; }
        public string SDSSoChungTuTongHop { get; set; }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public string SSoChungTuParent { get; set; }

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

        public bool IsChildSummary { get; set; }

        private bool _isSummaryVocher;
        public bool IsSummaryVocher
        {
            get => _isSummaryVocher;
            set => SetProperty(ref _isSummaryVocher, value);
        }

        public string DisplayQuarter
        {
            get
            {
                if (!string.IsNullOrEmpty(IQuy.ToString()))
                {
                    QuarterEnum Quarter;
                    if (Enum.TryParse(IQuy.ToString(), out Quarter))
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
                    } else
                    {
                        return CapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN;
                    }
                }
                return string.Empty;
            }
        }
    }

}
