using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(2, "Bảng lương tháng chi tiết", 4, 0)]
    public class SalaryMonthDetailImportModel : BindableBase
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

        public List<SalaryMonthDetailImportModel> LstPhuCap { get; set; }

        private string _tenCanBo;
        [ColumnAttribute("Tên cán bộ", 0)]
        public string TenCanBo
        {
            get => _tenCanBo;
            set => SetProperty(ref _tenCanBo, value);
        }

        private string _maCanBo;
        [ColumnAttribute("Mã cán bộ", 1)]
        public string MaCanBo
        {
            get => _maCanBo;
            set => SetProperty(ref _maCanBo, value);
        }

        private string _thang;
        [ColumnAttribute("Tháng", 2, ValidateType.IsNumber, true)]
        public string Thang
        {
            get => _thang;
            set => SetProperty(ref _thang, value);
        }

        private string _nam;
        [ColumnAttribute("Năm", 3, ValidateType.IsNumber, true)]
        public string Nam
        {
            get => _nam;
            set => SetProperty(ref _nam, value);
        }

        private string _maDonVi;
        [ColumnAttribute("Mã đơn vị", 4)]
        public string MaDonVi
        {
            get => _maDonVi;
            set => SetProperty(ref _maDonVi, value);
        }

        private string _maCachTinhLuong;
        [ColumnAttribute("Mã cách tính lương", 5)]
        public string MaCachTinhLuong
        {
            get => _maCachTinhLuong;
            set => SetProperty(ref _maCachTinhLuong, value);
        }

        private string _maCapBac;
        [ColumnAttribute("Mã cấp bậc", 6)]
        public string MaCapBac
        {
            get => _maCapBac;
            set => SetProperty(ref _maCapBac, value);
        }

        private string _maPhuCap;
        [ColumnAttribute("Mã phụ cấp", 7)]
        public string MaPhuCap
        {
            get => _maPhuCap;
            set => SetProperty(ref _maPhuCap, value);
        }

        private string _giaTri;
        [ColumnAttribute("Giá trị", 8, ValidateType.IsNumber)]
        public string GiaTri
        {
            get => _giaTri;
            set => SetProperty(ref _giaTri, value);
        }

        private string _maHieuCanBo;
        [ColumnAttribute("Mã hiệu cán bộ", 9)]
        public string MaHieuCanBo
        {
            get => _maHieuCanBo;
            set => SetProperty(ref _maHieuCanBo, value);
        }
    }
}
