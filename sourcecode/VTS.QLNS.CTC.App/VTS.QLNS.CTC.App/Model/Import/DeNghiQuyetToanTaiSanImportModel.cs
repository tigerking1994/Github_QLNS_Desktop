using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(3, "Tai San", 6, 1)]
    public class DeNghiQuyetToanTaiSanImportModel : BindableBase
    {
        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
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

        private string _tenDuAn;
        [ColumnAttribute("Tên dự án", 0)]
        public string TenDuAn
        {
            get => _tenDuAn;
            set => SetProperty(ref _tenDuAn, value);
        }

        private string _maDuAn;
        [ColumnAttribute("Mã dự án", 1)]
        public string MaDuAn
        {
            get => _maDuAn;
            set => SetProperty(ref _maDuAn, value);
        }

        private string _nhom;
        [ColumnAttribute("Nhóm", 2)]
        public string Nhom
        {
            get => _nhom;
            set => SetProperty(ref _nhom, value);
        }

        private string _thuocCDTQuanLy;
        [ColumnAttribute("Thuộc CDT quản lý", 4, ValidateType.IsNumber)]
        public string ThuocCDTQuanLy
        {
            get => _thuocCDTQuanLy;
            set => SetProperty(ref _thuocCDTQuanLy, value);
        }

        private string _donViKhacQuanLy;
        [ColumnAttribute("Đơn vị khác quản lý", 5, ValidateType.IsNumber)]
        public string DonViKhacQuanLy
        {
            get => _donViKhacQuanLy;
            set => SetProperty(ref _donViKhacQuanLy, value);
        }

        public double ThuocCDTQuanLyValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(ThuocCDTQuanLy, out value);
                return value;
            }
        }

        public double DonViKhacQuanLyValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(DonViKhacQuanLy, out value);
                return value;
            }
        }
    }
}

