using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 6, 0)]
    public class DtTmBhytTnDetailImportModel : BindableBase
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

        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }
        private string _sXauNoiMa;
        [ColumnAttribute("Xâu nối mã", 0)]
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }

        private string _sNoiDung;
        [ColumnAttribute("Nội dung", 1)]
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }

        private string _sDuToan;
        [ColumnAttribute("Dự toán", 2, ValidateType.IsNumber)]
        public string SDuToan
        {
            get => _sDuToan;
            set => SetProperty(ref _sDuToan, value);
        }

        private string _sGhiChu;
        [ColumnAttribute("Ghi chú", 3)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }
        public bool IsHasData => _sDuToan != "" || SGhiChu != "";
        public double? FDuToan
        {
            get
            {
                double fduToan = 0;
                if (double.TryParse(SDuToan, out fduToan))
                {
                    return fduToan;
                }
                return 0;
            }
        }
    }
}
