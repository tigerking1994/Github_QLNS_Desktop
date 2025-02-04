using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(2, "Chi Phi", 14, 1)]
    public class DeNghiQuyetToanChiPhiImportModel : BindableBase
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

        private string _ma;
        [ColumnAttribute("Mã", 3)]
        public string Ma
        {
            get => _ma;
            set => SetProperty(ref _ma, value);
        }

        private string _theoQuyetDinhPheDuyet;
        [ColumnAttribute("Theo quyết định phê duyệt", 4, ValidateType.IsNumber)]
        public string TheoQuyetDinhPheDuyet
        {
            get => _theoQuyetDinhPheDuyet;
            set => SetProperty(ref _theoQuyetDinhPheDuyet, value);
        }

        private string _deNghiQuyetToan;
        [ColumnAttribute("Đề nghị quyết toán", 5, ValidateType.IsNumber)]
        public string DeNghiQuyetToan
        {
            get => _deNghiQuyetToan;
            set => SetProperty(ref _deNghiQuyetToan, value);
        }

        private string _quyetToanAB;
        [ColumnAttribute("Quyết toán A - B", 6, ValidateType.IsNumber)]
        public string QuyetToanAB
        {
            get => _quyetToanAB;
            set => SetProperty(ref _quyetToanAB, value);
        }

        private string _ketQuaThanhTra;
        [ColumnAttribute("Kết quả thanh tra", 7, ValidateType.IsNumber)]
        public string KetQuaThanhTra
        {
            get => _ketQuaThanhTra;
            set => SetProperty(ref _ketQuaThanhTra, value);
        }

        public double TheoQuyetDinhPheDuyetValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(TheoQuyetDinhPheDuyet, out value);
                return value;
            }
        }

        public double DeNghiQuyetToanValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(DeNghiQuyetToan, out value);
                return value;
            }
        }

        public double KetQuaThanhTraValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(KetQuaThanhTra, out value);
                return value;
            }
        }

        public double QuyetToanABValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(QuyetToanAB, out value);
                return value;
            }
        }
    }
}

