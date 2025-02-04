using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 5, 1)]
    public class KhtmBhytDetailImportModel : BindableBase
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
        [ColumnAttribute("Nội dung", 4)]
        public string STenMLNS
        {
            get => _sTenMLNS;
            set => SetProperty(ref _sTenMLNS, value);
        }
        private string _iSoNguoi;
        [ColumnAttribute("Số người", 5, ValidateType.IsNumber)]
        public string ISoNguoi
        {
            get => _iSoNguoi;
            set => SetProperty(ref _iSoNguoi, value);
        }
        private string _iSoThang;
        [ColumnAttribute("Số tháng", 6, ValidateType.IsNumber)]
        public string ISoThang
        {
            get => _iSoThang;
            set => SetProperty(ref _iSoThang, value);
        }
        private string _fDinhMuc;
        [ColumnAttribute("Định mức", 7, ValidateType.IsNumber)]
        public string FDinhMuc
        {
            get => _fDinhMuc;
            set => SetProperty(ref _fDinhMuc, value);
        }
        private string _fThanhTien;
        [ColumnAttribute("Thành tiền", 8, ValidateType.IsNumber)]
        public string FThanhTien
        {
            get => _fThanhTien;
            set => SetProperty(ref _fThanhTien, value);
        }
        private string _sGhiChu;
        [ColumnAttribute("Ghi chú", 9)]
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }
    }
}
