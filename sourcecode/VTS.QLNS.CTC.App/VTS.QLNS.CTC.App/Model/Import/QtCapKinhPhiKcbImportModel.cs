using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 6, 0)]
    public class QtCapKinhPhiKcbImportModel : BindableBase
    {
        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value);
        }
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }

        private bool _bHangcha;
        public bool BHangCha
        {
            get => _bHangcha;
            set => SetProperty(ref _bHangcha, value);
        }
        private string _sXauNoiMa;
        [ColumnAttribute("Xâu nối mã", 0)]
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }
        private string _sLNS;
        [ColumnAttribute("LNS", 1)]
        public string SLNS
        {
            get => _sLNS;
            set => SetProperty(ref _sLNS, value);
        }
        private string _sTenMLNS;
        [ColumnAttribute("Nội dung", 2)]
        public string STenMLNS
        {
            get => _sTenMLNS;
            set => SetProperty(ref _sTenMLNS, value);
        }
        private string _sTenCSYT;
        [ColumnAttribute("Tên cơ sở Y tế", 3)]
        public string STenCSYT
        {
            get => _sTenCSYT;
            set
            {
                SetProperty(ref _sTenCSYT, value);
            }
        }

        private string _fKeHoachCap;
        [ColumnAttribute("Kế hoạch cấp", 4, ValidateType.IsNumber)]
        public string FKeHoachCap
        {
            get => _fKeHoachCap;
            set
            {
                SetProperty(ref _fKeHoachCap, value);
            }
        }

        private string _fQuyetToan4Quy;
        [ColumnAttribute("Tổng số quyết toán 4 quý", 4, ValidateType.IsNumber)]
        public string FQuyetToan4Quy
        {
            get => _fQuyetToan4Quy;
            set
            {
                SetProperty(ref _fQuyetToan4Quy, value);
            }
        }

        private string _fQuyetToanQuyNay;
        [ColumnAttribute("Quyết toán quý này", 5, ValidateType.IsNumber)]
        public string FQuyetToanQuyNay
        {
            get => _fQuyetToanQuyNay;
            set
            {
                SetProperty(ref _fQuyetToanQuyNay, value);
            }
        }

        private string _fConLai;
        [ColumnAttribute("Còn lại", 6, ValidateType.IsNumber)]
        public string FConLai
        {
            get => _fConLai;
            set
            {
                SetProperty(ref _fConLai, value);
            }
        }

        private string _fDaQuyetToan;
        [ColumnAttribute("Đã quyết toán", 7, ValidateType.IsNumber)]
        public string FDaQuyetToan
        {
            get => _fDaQuyetToan;
            set
            {
                SetProperty(ref _fDaQuyetToan, value);
            }
        }

        private string _sGhiChu;
        [ColumnAttribute("Ghi chú", 8)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set
            {
                SetProperty(ref _sGhiChu, value);
            }
        }

        public bool IsHasData => STenCSYT != "" || FDaQuyetToan != "" || FKeHoachCap != "" || FConLai != "" || FQuyetToanQuyNay != "" || SGhiChu != ""
            || FDaQuyetToan != "0" || FKeHoachCap != "0" || FConLai != "0" || FQuyetToanQuyNay != "0";
    }
}
