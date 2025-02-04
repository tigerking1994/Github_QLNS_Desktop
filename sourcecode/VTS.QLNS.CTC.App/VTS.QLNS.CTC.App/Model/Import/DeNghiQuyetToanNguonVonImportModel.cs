using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(1, "Mau 02", 6, 1)]
    public class DeNghiQuyetToanNguonVonImportModel : BindableBase
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

        private string _noiDung;
        [ColumnAttribute("Nội dung", 2)]
        public string NoiDung
        {
            get => _noiDung;
            set => SetProperty(ref _noiDung, value);
        }

        private string _maNguonVon;
        [ColumnAttribute("Mã nguồn vốn", 3, ValidateType.IsNumber)]
        public string MaNguonVon
        {
            get => _maNguonVon;
            set => SetProperty(ref _maNguonVon, value);
        }

        private string _quyetDinhDauTu;
        [ColumnAttribute("Quyết định đầu tư", 4, ValidateType.IsNumber)]
        public string QuyetDinhDauTu
        {
            get => _quyetDinhDauTu;
            set => SetProperty(ref _quyetDinhDauTu, value);
        }

        private string _keHoach;
        [ColumnAttribute("Kế hoạch", 5, ValidateType.IsNumber)]
        public string KeHoach
        {
            get => _keHoach;
            set => SetProperty(ref _keHoach, value);
        }

        private string _daThanhToan;
        [ColumnAttribute("Đã thanh toán", 6, ValidateType.IsNumber)]
        public string DaThanhToan
        {
            get => _daThanhToan;
            set => SetProperty(ref _daThanhToan, value);
        }

        public double QuyetDinhDauTuValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(QuyetDinhDauTu, out value);
                return value;
            }
        }

        public double KeHoachValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(KeHoach, out value);
                return value;
            }
        }

        public double DaThanhToanValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(DaThanhToan, out value);
                return value;
            }
        }
    }
}
