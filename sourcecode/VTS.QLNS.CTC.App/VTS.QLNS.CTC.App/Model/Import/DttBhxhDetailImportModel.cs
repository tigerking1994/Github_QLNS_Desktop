using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 8, 0)]
    public class DttBhxhDetailImportModel : BindableBase
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
        private string _sTenMLNS;
        [ColumnAttribute("Nội dung", 1)]
        public string STenMLNS
        {
            get => _sTenMLNS;
            set => SetProperty(ref _sTenMLNS, value);
        }
        private string _fNldBHXH;
        [ColumnAttribute("NLĐ đóng", 2, ValidateType.IsNumber)]
        public string FNldBHXH
        {
            get => _fNldBHXH;
            set => SetProperty(ref _fNldBHXH, value);
        }
        private string _fNsdBHXH;
        [ColumnAttribute("NSD đóng", 3, ValidateType.IsNumber)]
        public string FNsdBHXH
        {
            get => _fNsdBHXH;
            set => SetProperty(ref _fNsdBHXH, value);
        }
        private string _fCongBHXH;
        [ColumnAttribute("Cộng", 4, ValidateType.IsNumber)]
        public string FCongBHXH
        {
            get => _fCongBHXH;
            set => SetProperty(ref _fCongBHXH, value);
        }
        private string _fNldBHYT;
        [ColumnAttribute("NLĐ đóng", 5, ValidateType.IsNumber)]
        public string FNldBHYT
        {
            get => _fNldBHYT;
            set => SetProperty(ref _fNldBHYT, value);
        }
        private string _fNsdBHYT;
        [ColumnAttribute("NSD đóng", 6, ValidateType.IsNumber)]
        public string FNsdBHYT
        {
            get => _fNsdBHYT;
            set => SetProperty(ref _fNsdBHYT, value);
        }
        private string _fCongBHYT;
        [ColumnAttribute("Cộng", 7, ValidateType.IsNumber)]
        public string FCongBHYT
        {
            get => _fCongBHYT;
            set => SetProperty(ref _fCongBHYT, value);
        }
        private string _fNldBHTN;
        [ColumnAttribute("NLĐ đóng", 8, ValidateType.IsNumber)]
        public string FNldBHTN
        {
            get => _fNldBHTN;
            set => SetProperty(ref _fNldBHTN, value);
        }
        private string _fNsdBHTN;
        [ColumnAttribute("NSD đóng", 9, ValidateType.IsNumber)]
        public string FNsdBHTN
        {
            get => _fNsdBHTN;
            set => SetProperty(ref _fNsdBHTN, value);
        }
        private string _fCongBHTN;
        [ColumnAttribute("Cộng", 10, ValidateType.IsNumber)]
        public string FCongBHTN
        {
            get => _fCongBHTN;
            set => SetProperty(ref _fCongBHTN, value);
        }
        private string _fTongCong;
        [ColumnAttribute("Cộng", 11, ValidateType.IsNumber)]
        public string FTongCong
        {
            get => _fTongCong;
            set => SetProperty(ref _fTongCong, value);
        }
        private string _sGhiChu;
        [ColumnAttribute("Ghi chú", 12, ValidateType.IsNumber)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }
        public bool IsHasDttData => FNldBHXH != "" || FNsdBHXH != "" || FNldBHYT != "" || FNsdBHYT != "" || FNldBHTN != "" || FNsdBHTN != "" || SGhiChu != "";
    }
}
