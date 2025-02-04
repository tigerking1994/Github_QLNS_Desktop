using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.App.Model.NsSktChungTuChiTietModel;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 8, 0)]
    public class KhtBhxhDetailImportModel : BindableBase
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
        private Guid _idMLNS;
        public Guid IdMLNS
        {
            get => _idMLNS;
            set => SetProperty(ref _idMLNS, value);
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
        private string _iQSBQNam;
        [ColumnAttribute("QS BQ Năm", 2, ValidateType.IsNumber)]
        public string IQSBQNam
        {
            get => _iQSBQNam;
            set => SetProperty(ref _iQSBQNam, value);
        }
        private string _fLuongChinh;
        [ColumnAttribute("Lương Chính", 3, ValidateType.IsNumber)]
        public string FLuongChinh
        {
            get => _fLuongChinh;
            set => SetProperty(ref _fLuongChinh, value);
        }
        private string _fPhuCapChucVu;
        [ColumnAttribute("PC chức vụ", 4, ValidateType.IsNumber)]
        public string FPhuCapChucVu
        {
            get => _fPhuCapChucVu;
            set => SetProperty(ref _fPhuCapChucVu, value);
        }
        private string _fPCTNNghe;
        [ColumnAttribute("PC TN nghề", 5, ValidateType.IsNumber)]
        public string FPCTNNghe
        {
            get => _fPCTNNghe;
            set => SetProperty(ref _fPCTNNghe, value);
        }
        private string _fPCTNVuotKhung;
        [ColumnAttribute("PC TN VK", 6, ValidateType.IsNumber)]
        public string FPCTNVuotKhung
        {
            get => _fPCTNVuotKhung;
            set => SetProperty(ref _fPCTNVuotKhung, value);
        }
        private string _fNghiOm;
        [ColumnAttribute("Nghỉ ốm < 14  ngày / tháng", 7, ValidateType.IsNumber)]
        public string FNghiOm
        {
            get => _fNghiOm;
            set => SetProperty(ref _fNghiOm, value);
        }
        private string _fHSBL;
        [ColumnAttribute("HSBL", 8, ValidateType.IsNumber)]
        public string FHSBL
        {
            get => _fHSBL;
            set => SetProperty(ref _fHSBL, value);
        }
        private string _fTongQuyTienLuongNam;
        [ColumnAttribute("Cộng", 9, ValidateType.IsNumber)]
        public string FTongQuyTienLuongNam
        {
            get => _fTongQuyTienLuongNam;
            set => SetProperty(ref _fTongQuyTienLuongNam, value);
        }
    }
}
