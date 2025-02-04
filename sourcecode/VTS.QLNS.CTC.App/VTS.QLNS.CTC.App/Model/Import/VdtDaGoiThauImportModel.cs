using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Thông tin gói thầu", 3, 0)]
    public class VdtDaGoiThauImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value);
        }

        private string _iStt;
        [ColumnAttribute("STT", 0)]
        public string IStt
        {
            get => _iStt;
            set => SetProperty(ref _iStt, value);
        }

        private string _sMaDuAn;
        [ColumnAttribute("Mã dự án", 1)]
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _sSoQuyetDinh;
        [ColumnAttribute("Số quyết định", 2)]
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private string _dNgayQuyetDinh;
        [ColumnAttribute("Ngày quyết định", 3)]
        public string DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }

        private string _sTenGoiThau;
        [ColumnAttribute("Tên gói thầu", 4)]
        public string STenGoiThau
        {
            get => _sTenGoiThau;
            set => SetProperty(ref _sTenGoiThau, value);
        }

        private string _fTienTrungThau;
        [ColumnAttribute("Giá gói thầu", 5, Utility.Enum.ValidateType.IsNumber)]
        public string FTienTrungThau
        {
            get => _fTienTrungThau; 
            set => SetProperty(ref _fTienTrungThau, value);
        }

        private string _sThoiGianThucHien;
        [ColumnAttribute("Thời gian thực hiện", 6)]
        public string SThoiGianThucHien
        {
            get => _sThoiGianThucHien;
            set => SetProperty(ref _sThoiGianThucHien, value);
        }

        private string _sMaNhaThau;
        [ColumnAttribute("Mã nhà thầu", 7)]
        public string SMaNhaThau
        {
            get => _sMaNhaThau; 
            set => SetProperty(ref _sMaNhaThau, value);
        }
    }
}
