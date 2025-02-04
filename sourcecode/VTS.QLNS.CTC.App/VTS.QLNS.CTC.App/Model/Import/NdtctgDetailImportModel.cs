using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using System;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 8, 0)]
    public class NdtctgDetailImportModel : BaseImportModel
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
        private bool _isHangcha;
        public bool IsHangCha
        {
            get => _isHangcha;
            set => SetProperty(ref _isHangcha, value);
        }
        private Guid _idMLNS;
        public Guid IdMLNS
        {
            get => _idMLNS;
            set => SetProperty(ref _idMLNS, value);
        }

        private string _sXauNoiMa;
        [ColumnAttribute("Xâu nối mã", 0, ValidateType.IsXauNoiMaBH)]
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }
        private string _sTT;
        [ColumnAttribute("STT", 1)]
        public string STT
        {
            get => _sTT;
            set => SetProperty(ref _sTT, value);
        }
        private string _sTenMLNS;
        [ColumnAttribute("Nội dung", 2)]
        public string STenMLNS
        {
            get => _sTenMLNS;
            set => SetProperty(ref _sTenMLNS, value);
        }
        private string _sDuToan;
        [ColumnAttribute("Dự toán", 3, ValidateType.IsNumber)]
        public string SDuToan
        {
            get => _sDuToan;
            set => SetProperty(ref _sDuToan, value);
        }

        private string _sMaLoaiChi;
        [ColumnAttribute("Mã loại chi", 4)]
        public string SMaLoaiChi
        {
            get => _sMaLoaiChi;
            set => SetProperty(ref _sMaLoaiChi, value);
        }
        public double? FTienDuToan => string.IsNullOrEmpty(SDuToan) ? 0 : double.Parse(SDuToan);
        public bool IsDataNotNull => !string.IsNullOrWhiteSpace(SDuToan);
    }
}
