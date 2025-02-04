using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{

    [SheetAttribute(0, "Chứng từ chi tiết", 6, 0)]
    public class CapPhatBoSungBHYTImportModel : BindableBase
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
        private string _iIDMaCoSoYTe;

        [ColumnAttribute("Mã cơ sở y tế", 2)]
        public string IIDMaCoSoYTe
        {
            get => _iIDMaCoSoYTe;
            set
            {
                SetProperty(ref _iIDMaCoSoYTe, value);
            }
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

        private string _fDaQuyetToan;
        [ColumnAttribute("Đã quyết toán quý này", 4, ValidateType.IsNumber)]
        public string FDaQuyetToan
        {
            get => _fDaQuyetToan;
            set
            {
                SetProperty(ref _fDaQuyetToan, value);
            }
        }

        private string _fDaCapUng;
        [ColumnAttribute("Số cấp tạm ứng", 5, ValidateType.IsNumber)]
        public string FDaCapUng
        {
            get => _fDaCapUng;
            set
            {
                SetProperty(ref _fDaCapUng, value);
            }
        }

        private string _fThua;
        [ColumnAttribute("Thừa", 6, ValidateType.IsNumber)]
        public string FThua
        {
            get => _fThua;
            set
            {
                SetProperty(ref _fThua, value);
            }
        }

        private string _fThieu;
        [ColumnAttribute("Thiếu", 7, ValidateType.IsNumber)]
        public string FThieu
        {
            get => _fThieu;
            set
            {
                SetProperty(ref _fThieu, value);
            }
        }

        private string _fSoCapBoSung;
        [ColumnAttribute("Số cấp bổ sung", 8, ValidateType.IsNumber)]
        public string FSoCapBoSung
        {
            get => _fSoCapBoSung;
            set
            {
                SetProperty(ref _fSoCapBoSung, value);
            }
        }

        private string _sGhiChu;
        [ColumnAttribute("Ghi chú", 9)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set
            {
                SetProperty(ref _sGhiChu, value);
            }
        }
        
        public bool IsHasData => STenCSYT != "" || FDaQuyetToan != "" || FDaCapUng != "" || FThua != "" || FThieu != "" || FSoCapBoSung != "" || SGhiChu != ""
            || FDaQuyetToan != "0" || FDaCapUng != "0" || FThua != "" || FThieu != "" || FSoCapBoSung != "0";
    }
}
