using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(2, "Chi Phi", 14, 1)]
    public class PheDuyetQuyetToanChiPhiImportModel : BindableBase
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

        //private string _theoQuyetDinhPheDuyet;
        //[ColumnAttribute("Theo quyết định phê duyệt", 4, ValidateType.IsNumber)]
        //public string TheoQuyetDinhPheDuyet
        //{
        //    get => _theoQuyetDinhPheDuyet;
        //    set => SetProperty(ref _theoQuyetDinhPheDuyet, value);
        //}

        private string _giaTriThamTra;
        [ColumnAttribute("Giá trị thẩm tra", 5, ValidateType.IsNumber)]
        public string GiaTriThamTra
        {
            get => _giaTriThamTra;
            set => SetProperty(ref _giaTriThamTra, value);
        }

        private string _giaTriQuyetToan;
        [ColumnAttribute("Giá trị quyết toán", 6, ValidateType.IsNumber)]
        public string GiaTriQuyetToan
        {
            get => _giaTriQuyetToan;
            set => SetProperty(ref _giaTriQuyetToan, value);
        }

        //public double TheoQuyetDinhPheDuyetValue
        //{
        //    get
        //    {
        //        double value = 0;
        //        bool check = double.TryParse(TheoQuyetDinhPheDuyet, out value);
        //        return value;
        //    }
        //}

        public double GiaTriThamTraValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(GiaTriThamTra, out value);
                return value;
            }
        }

        public double GiaTriQuyetToanValue
        {
            get
            {
                double value = 0;
                bool check = double.TryParse(GiaTriQuyetToan, out value);
                return value;
            }
        }
    }
}


