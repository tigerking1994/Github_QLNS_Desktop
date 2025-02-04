using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 7, 1)]
    public class QuyetToanThuMuaImportModel : BindableBase
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
        private string _sTenMLNS;
        [ColumnAttribute("Nội dung", 1)]
        public string STenMLNS
        {
            get => _sTenMLNS;
            set => SetProperty(ref _sTenMLNS, value);
        }
        
        private string _fDuToan;
        [ColumnAttribute("Dự toán", 2, ValidateType.IsNumber)]
        public string FDuToan
        {
            get => _fDuToan;
            set
            {
                SetProperty(ref _fDuToan, value);
            }
        }

        private string _fDaQuyetToan;
        [ColumnAttribute("Đã quyết toán", 3, ValidateType.IsNumber)]
        public string FDaQuyetToan
        {
            get => _fDaQuyetToan;
            set
            {
                SetProperty(ref _fDaQuyetToan, value);
            }
        }

        private string _fConLai;
        [ColumnAttribute("Còn lại", 4, ValidateType.IsNumber)]
        public string FConLai
        {
            get => _fConLai;
            set
            {
                SetProperty(ref _fConLai, value);
            }
        }

        private string _fSoPhaiThu;
        [ColumnAttribute("Còn lại", 5, ValidateType.IsNumber)]
        public string FSoPhaiThu
        {
            get => _fSoPhaiThu;
            set
            {
                SetProperty(ref _fSoPhaiThu, value);
            }
        }

        private string _sGhiChu;
        [ColumnAttribute("Ghi chú", 6)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set
            {
                SetProperty(ref _sGhiChu, value);
            }
        }

        public bool IsHasData => !string.IsNullOrEmpty(SXauNoiMa) || !string.IsNullOrEmpty(STenMLNS) || !string.IsNullOrEmpty(SGhiChu)
            || FDuToan != "" || FDaQuyetToan != "" || FConLai != "" || FSoPhaiThu != ""
            || FDuToan != "0" || FDaQuyetToan != "0" || FConLai != "0" || FSoPhaiThu != "0";
    }
}
