using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 6, 1)]
    public class ExpertiseNTDImportModel : BindableBase
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

        private string _kyHieu;
        [ColumnAttribute("Ký hiệu", 0, ValidateType.KyHieu)]
        public string KyHieu
        {
            get => _kyHieu;
            set => SetProperty(ref _kyHieu, value);
        }

        private string _stt;
        [ColumnAttribute("STT", 1)]
        public string STT
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }

        private string _nganh;
        [ColumnAttribute("Ngành", 2)]
        public string Nganh
        {
            get => _nganh;
            set => SetProperty(ref _nganh, value);
        }

        private string _nganhCha;
        [ColumnAttribute("Ngành cha", 3)]
        public string NganhCha
        {
            get => _nganhCha;
            set => SetProperty(ref _nganhCha, value);
        }

        private string _maDonVi;
        [ColumnAttribute("Mã đơn vị", 4)]
        public string MaDonVi
        {
            get => _maDonVi;
            set => SetProperty(ref _maDonVi, value);
        }

        private string _tenDonVi;
        [ColumnAttribute("Tên đơn vị", 5)]
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private string _moTa;
        [ColumnAttribute("Mô tả", 6)]
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private string _tuChi;
        [ColumnAttribute("Tự chi", 9, ValidateType.IsNumber)]
        public string TuChi
        {
            get => _tuChi;
            set => SetProperty(ref _tuChi, value);
        }

        private string _suDungTonKho;
        [ColumnAttribute("Sử dụng tồn kho", 11, ValidateType.IsNumber)]
        public string SuDungTonKho
        {
            get => _suDungTonKho;
            set => SetProperty(ref _suDungTonKho, value);
        }

        private string _chiDacThuNganhPhanCap;
        [ColumnAttribute("Chi đặc thù ngành phân cấp", 13, ValidateType.IsNumber)]
        public string ChiDacThuNganhPhanCap
        {
            get => _chiDacThuNganhPhanCap;
            set => SetProperty(ref _chiDacThuNganhPhanCap, value);
        }

        public double TuChiValue
        {
            get
            {
                double value;
                bool check = double.TryParse(TuChi, out value);
                return value;
            }
        }

        public double SuDungTonKhoValue
        {
            get
            {
                double value;
                bool check = double.TryParse(SuDungTonKho, out value);
                return value;
            }
        }

        public double ChiDacThuNganhPhanCapValue
        {
            get
            {
                double value;
                bool check = double.TryParse(ChiDacThuNganhPhanCap, out value);
                return value;
            }
        }

        public string KyHieuParent { get; set; }

        public List<string> ListKyHieuChild { get; set; }
    }
}
