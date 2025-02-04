using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(0, "Chứng từ chi tiết", 5, 1)]
    public class ExpertiseDistributionDetailImportModel : BindableBase
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

        public List<string> ListKyHieuChild { get; set; }
        public string KyHieuParent { get; set; }

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

        private string _description;
        [ColumnAttribute("Nội dung", 6)]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
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

        private string _tuChi;
        [ColumnAttribute("Tự chi", 8, ValidateType.IsNumber)]
        public string TuChi
        {
            get => _tuChi;
            set => SetProperty(ref _tuChi, value);
        }
    }
}
