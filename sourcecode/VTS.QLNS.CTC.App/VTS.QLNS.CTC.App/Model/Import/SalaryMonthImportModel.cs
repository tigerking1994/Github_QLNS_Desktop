using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Bảng lương tháng", 4, 0)]
    public class SalaryMonthImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
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

        private string _tenBangLuong;
        [ColumnAttribute("Tên bảng lương", 0)]
        public string TenBangLuong
        {
            get => _tenBangLuong;
            set => SetProperty(ref _tenBangLuong, value);
        }

        private string _tuNgay;
        [ColumnAttribute("Từ ngày", 1, ValidateType.IsDateTime)]
        public string TuNgay
        {
            get => _tuNgay;
            set => SetProperty(ref _tuNgay, value);
        }

        private string _denNgay;
        [ColumnAttribute("Đến ngày", 2, ValidateType.IsDateTime)]
        public string DenNgay
        {
            get => _denNgay;
            set => SetProperty(ref _denNgay, value);
        }

        private string _maDonVi;
        [ColumnAttribute("Mã đơn vị", 3)]
        public string MaDonVi
        {
            get => _maDonVi;
            set => SetProperty(ref _maDonVi, value);
        }

        private string _thang;
        [ColumnAttribute("Tháng", 4, ValidateType.IsNumber)]
        public string Thang
        {
            get => _thang;
            set => SetProperty(ref _thang, value);
        }

        private string _nam;
        [ColumnAttribute("Năm", 5, ValidateType.IsNumber)]
        public string Nam
        {
            get => _nam;
            set => SetProperty(ref _nam, value);
        }

        private string _maCachTinhLuong;
        [ColumnAttribute("Mã cách tính lương", 6)]
        public string MaCachTinhLuong
        {
            get => _maCachTinhLuong;
            set => SetProperty(ref _maCachTinhLuong, value);
        }
    }
}
